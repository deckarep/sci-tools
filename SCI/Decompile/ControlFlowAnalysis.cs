using System;
using System.Collections.Generic;
using System.Linq;
using SCI.Decompile.Cfg;
using SCI.Resource;

////////////////////////////////////////////
//                                        //
//               YOU'RE NOT               //
//                SUPPOSED                //
//               TO BE HERE               //
//                                        //
////////////////////////////////////////////

namespace SCI.Decompile
{
    class ControlFlowAnalysis
    {
        Function f;
        HashSet<Node> Visited = new HashSet<Node>();

        public Graph SolveFunction(Function function, Graph g,
                                   IReadOnlyList<SwitchSummary> switches,
                                   IReadOnlyList<LoopSummary> loops)
        {
            f = function;
            Visited.Clear();

            // sanity check
            if (loops.Any(l => l.UnsolvedBranches.Any(i => i.Operation != Operation.bnt)))
                throw new Exception("SolveFunction received loop with unsolved bt or jmp");

            // delete all target edges from unsolved loop bnt's.
            // they are either back edges or they target the loop's follow.
            // they will be replaced, but i get confused thinking about how
            // these edges affect things in the meantime, so just get rid of
            // them. the graph utility functions weren't written to expect this
            // and i don't want to tear them up.
            foreach (var unsolvedBnt in loops.SelectMany(l => l.UnsolvedBranches))
            {
                var bntNode = g.GetByInstruction(unsolvedBnt);
                var bntTargetEdge = g.Successors[bntNode].First(e => e.Type == EdgeType.BntTarget);
                Log.Debug(f, "Removing unsolved edge: " + bntTargetEdge);
                g.RemoveEdge(bntTargetEdge);
            }

            // solve inner scopes to outer scopes (switches/loops)
            var switchesAndLoops = new Dictionary<int, object>();
            foreach (var s in switches)
                switchesAndLoops.Add(s.Toss.Position, s);
            foreach (var l in loops)
                switchesAndLoops.Add(l.Latch.Position, l);
            foreach (var switchOrLoop in switchesAndLoops.OrderBy(kv => kv.Key))
            {
                if (switchOrLoop.Value is SwitchSummary)
                {
                    var s = (SwitchSummary)switchOrLoop.Value;
                    var switchNode = new Node();
                    switchNode.Type = NodeType.Switch;
                    var outerLoop = loops.FirstOrDefault(l => l.Contains(s.Toss));
                    switchNode.Subgraph = SolveSwitch(g, s, outerLoop);
                    switchNode.First = g.GetByInstruction(s.Push).First;
                    switchNode.Last = s.Toss;
                    Replace(g, switchNode);
                }
                else
                {
                    var l = (LoopSummary)switchOrLoop.Value;

                    // sloppily doing this first because SolveLoop() mutates
                    // blocks by truncating branches and latches

                    // delete edges from loop to follow.
                    // Replace() might handle this, i just want to be explicit.
                    // since this is a solved loop, there should be zero or one of these.
                    var followNode = g.GetByInstruction(l.Follow);
                    var escapeEdge = g.Predecessors[followNode].SingleOrDefault(e => l.Contains(e.A.First));
                    if (escapeEdge != null)
                        g.RemoveEdge(escapeEdge);

                    // delete back edge. there should be exactly one.
                    // Replace() definitely expects this to have already been removed.
                    var headNode = g.GetByInstruction(l.Start);
                    var latchNode = g.GetByInstruction(l.Latch);
                    var backEdge = g.Successors[latchNode].Single(e => e.B == headNode);
                    g.RemoveEdge(backEdge);

                    // now solve the loop; this would be more readable if it went first,
                    // but it mutates blocks so delete those other edges first.
                    var loopNode = new Node();
                    loopNode.Type = NodeType.Loop;
                    loopNode.Subgraph = SolveLoop(g, l);
                    loopNode.First = l.Start;
                    loopNode.Last = l.Latch;
                    loopNode.LoopType = l.Type;

                    // with the loopy edges removed, we can generically replace the nodes.
                    // this adds the follow edge from the loop to the next instruction.
                    Replace(g, loopNode);
                }
            }

            // all switches and loops have been solved.
            // now to solve the function body.
            return Solve(g);
        }

        // Preconditions:
        // 1. The BntTarget edge has already been removed
        // 2. There are no unsolved bnt's in between this one and latchOrCaseEnd.
        //    That is, if there were, they've already been solved.
        // 3. The only back edges in the graph are loop latches, if that.
        // Postconditions:
        // 1. bnt's BranchTarget will be updated and its node's BntTarget will be created.
        // 2. bnt's node *may* be flagged so that AstBuilder creates an Else-Break/Continue
        // 3. if bnt's bnt-follow node was the latch node, the latch node has been split.
        //    the old node is removed from the graph and replaced with two new ones,
        //    and bnt now targets the first one, with the new latch node as its follow.
        void SolveLoopBnt(Graph g, Instruction bnt, LoopSummary loop, Instruction latchOrCaseEnd)
        {
            // the Semi-NCA graph dominance algorithm requires that all nodes be
            // reachable, so first delete any unreachable nodes.
            g.RemoveUnreachableNodes();

            bool isBreak = bnt.BranchTarget >= bnt.Position;

            // get the last node dominated by bnt. (not counting the End node)
            var bntNode = g.GetByInstruction(bnt);
            var doms = Dominance.Create(g);
            var lastDominated = (from n in g.Nodes
                                 where n.IsNotStartOrEnd &&
                                       doms.Dominators(n).Contains(bntNode)
                                 orderby n.First.Position descending
                                 select n).FirstOrDefault();
            if (lastDominated == bntNode) throw new Exception("couldn't find node dominated by bnt: " + bnt);

            // i want to use the last dominated node to create a range of nodes to scoop up
            // and put in an If-Then-Else as the Then. the Test will be the bnt node, and the
            // Else will be invented by AstBuilder and a Break/Continue will be placed in it.
            //
            // it's the end boundary of this range that's tricky.
            // if it doesn't involve the latch, then great, include the last dominated node.
            // if it does involve the latch, exclude the last dominated node, as long as
            // there are other nodes in between our bnt and the latch node.
            // if there are NO nodes in between bnt and latch, uh-oh, we have to split the
            // latch node so that we can scoop up its instructions.
            Node bntTargetNode;
            bool addElseBreakContinue = true;
            if (!lastDominated.Contains(latchOrCaseEnd))
            {
                // ah, the easy case. the last-dominated node will be part of Then,
                // so the new bnt target is last-dominated's follow node.
                bntTargetNode = g.Follower(lastDominated);
                if (bntTargetNode == null) throw new Exception("Loop bnt's last dominated node has no follower: " + lastDominated);

                // okay but actually, what if this is an obvious optimization?
                // if the target node is a bnt that points to the latch, and we're a continue,
                // then just deoptimize our bnt to point to the next one. no need for creating
                // an else-continue. see qfg2 jackalMan:initCombat; this cleans up a few functions
                // that would otherwise have optimized or/and logic that generates continues.
                if (!isBreak &&
                    bntTargetNode.First?.Operation == Operation.bnt &&
                    bntTargetNode.First.BranchTarget == loop.Latch.Position)
                {
                    Log.Debug(f, "Graph-Solving loop bnt with deoptimization: " + bnt);
                    addElseBreakContinue = false;
                }
            }
            else
            {
                // target the latch node. everything up until it will be placed in the If-Then body.
                // unless...
                bntTargetNode = lastDominated;

                // ...there are no nodes in between us and the latch node.
                // we've run out of graph, so split the latch node into two,
                // if we can, and target the second node.
                //
                // there is probably (always?) a jmp (break) in the latch node, and if so, it makes
                // for cleaner output to split on that and scoop up everything in front of it and
                // not fabricate an else. the weird for-loop in gk1-cd bayouRitual abortCartoon:changeState
                // is a good example of this. it's not strictly necessary to find a better place to
                // split than the latch, it just makes for less else/breaks and makes it possible
                // to identify the for re-init in weird cases. not many functions are affected either way.
                if (g.Successor(bntNode, EdgeType.BntNext) == bntTargetNode)
                {
                    if (bntTargetNode.First == latchOrCaseEnd) throw new Exception("I don't know what to do!");

                    // we're going to split on the latch instruction unless we can do better (we probably can)
                    var instructionToSplitOn = latchOrCaseEnd;

                    // inspect each instruction, if there are any breaks/continues going my way then
                    // use the last one as the target and opt out of creating an else-break/continue.
                    for (var i = bntTargetNode.First; i != latchOrCaseEnd; i = i.Next)
                    {
                        if (isBreak && i.Operation == Operation.jmp && i.HasFlag(InstructionFlag.Break))
                        {
                            instructionToSplitOn = i;
                            addElseBreakContinue = false;
                        }
                        else if (!isBreak && i.Operation == Operation.jmp && i.HasFlag(InstructionFlag.Continue))
                        {
                            // this never happens
                            instructionToSplitOn = i;
                            addElseBreakContinue = false;
                        }
                    }
                    Log.Debug(f, "Splitting node: " + bntTargetNode + " on: " + instructionToSplitOn);
                    bntTargetNode = g.Split(instructionToSplitOn);
                }
            }

            bnt.BranchTarget = bntTargetNode.First.Position;
            g.Add(EdgeType.BntTarget, bntNode, bntTargetNode);
            if (addElseBreakContinue)
            {
                if (isBreak)
                {
                    bntNode.Flags |= NodeFlag.AddElseBreak;
                }
                else
                {
                    bntNode.Flags |= NodeFlag.AddElseContinue;
                }
                bntNode.LoopLevel = bnt.LoopLevel;
            }
        }

        // Begin recursively solving a graph.
        // Graph preconditions:
        // - All switches and loops within the graph have been solved
        //   and replaced with nodes containing subgraphs.
        // - No back edges
        // - The End node has at least one predecessor (...maybe only one? idk)
        // Graph postconditions: (the return value, not the parameter)
        // - Graph consists of blocks, switches, loops, ifs, ands, ors.
        // - I didn't bother connecting any nodes to the End node.
        //   AstBuilder doesn't care, it's just going to walk the tree
        //   until it runs out of nodes.
        Graph Solve(Graph g)
        {
            // calculate dominance.
            // the Semi-NCA graph dominance algorithm requires that all nodes be
            // reachable, so first delete any unreachable nodes.
            g.RemoveUnreachableNodes();
            var doms = Dominance.Create(g);

            // fixup bnts (KQ4:handleEvent)
            // fixup bts (Phant2 curtisCubicleRoomCH1SR3:call)
            BranchGraphFixup.Fixup(g, doms, f);

            // reset
            Visited.Clear();

            return Recurse(g, doms, g.First(), g.End);
        }

        // Recursively solve a graph.
        // Solve the start node, if its a branch block then solve its children
        // up until its immediate post dominator. Then solve the immediate
        // post dominator. Keep going until the stop node is reached.
        // The stop node is *not* solved, so the first call should pass the
        // End node as the stop node so that all real nodes get solved.
        Graph Recurse(Graph g, Dominance doms, Node start, Node stop)
        {
            // create a new graph to add solved nodes to
            var newGraph = new Graph();
            var lastNewNode = newGraph.Start;

            Node current = start;
            do
            {
                if (Visited.Contains(current)) throw new Exception("node already visited: " + current);
                Visited.Add(current);
                var successors = g.Successors[current];
                switch (current.Type)
                {
                    // block handling tests successors first and instruction type second.
                    // this way it doesn't acknowledge branches that have been defanged.
                    // this includes a branch that's been turned into a break/continue, or
                    // the escape bnt at the end of a loop test that no longer has branch edges
                    // but just has a regular follow node to its subgraph's End.
                    case NodeType.Block:
                        if (successors.Count == 0) throw new Exception("block has no successors");
                        if (successors.Count > 2) throw new Exception("block has too many successors: " + successors.Count);
                        if (successors.Count == 1)
                        {
                            if (successors[0].Type != EdgeType.Follow)
                                throw new Exception("block with one successor should only have a follow edge");
                            if (successors[0].B != doms.ImmediatePostDominator(current))
                                throw new Exception("block's follow node isn't immediate post dominator");

                            // it's just a basic block, add it to the graph
                            newGraph.Add(EdgeType.Follow, lastNewNode, current);
                            lastNewNode = current;
                        }
                        else if (current.Last.Operation == Operation.bnt &&
                                 current.Last.BranchTarget == current.Last.Next.Position)
                        {
                            // ignore empty bnt's (bnt's that target the next instruction)
                            //
                            // TODO: Make a report of all functions with this so i can spot
                            // check. alternatively, delete them from the instruction list.
                            // this sure quieted a lot of errors though.
                            //
                            // TODO: i don't like this; it works but i don't like it.
                            // comment this out, run it on games i have source for, and
                            // compare to real source. i suspect this is due to an And with
                            // only one operand, or a blank operand, or i don't know, but
                            // i may be able to handle this in AstBuilder.
                            // CFA is hard and i want to remove code.
                            newGraph.Add(EdgeType.Follow, lastNewNode, current);
                            lastNewNode = current;
                        }
                        else if (current.Last.Operation == Operation.bnt)
                        {
                            // recurse to the "left", the Then node
                            Node bntNext = g.Successor(current, EdgeType.BntNext);
                            Node ipdom = doms.ImmediatePostDominator(current);
                            Graph thenGraph = Recurse(g, doms, bntNext, ipdom);

                            // is the "right" node an else? if so then recurse unless already visited
                            Node bntTarget = g.Successor(current, EdgeType.BntTarget);
                            Graph elseGraph = null;
                            bool elseWasAlreadyVisited = false;
                            bool iHaveAnElse = bntTarget != ipdom;
                            if (iHaveAnElse)
                            {
                                if (!Visited.Contains(bntTarget))
                                {
                                    // recurse to the "right", the Else node
                                    elseGraph = Recurse(g, doms, bntTarget, ipdom);
                                }
                                else
                                {
                                    elseWasAlreadyVisited = true;
                                }
                            }

                            // left and right have been visited.
                            // now it's time to make an If node and copy in Then and Else,
                            // unless Else has been visited, in which case Then *must*
                            // start with an If-Then-Else, merge by creating an And node
                            // whose first operand is everything in my graph and whose
                            // second operand is its Test.

                            if (!elseWasAlreadyVisited)
                            {
                                // create an If node and add it to the graph
                                var ifNode = new Node();
                                ifNode.Type = NodeType.If;
                                if (newGraph.First() == null)
                                {
                                    // simple: the graph is empty, just create an If
                                    // and set the current node to its Test.
                                    newGraph.Add(EdgeType.Follow, newGraph.Start, ifNode);
                                    newGraph.Add(EdgeType.IfTest, ifNode, current);
                                }
                                else
                                {
                                    // less simple: the graph isn't empty. create an If
                                    // and claim the existing graph nodes as its Test,
                                    // followed by the current node.
                                    // it sounds crazy, but AstBuilder will just consume
                                    // everything in order and use whatever expression
                                    // is finally in acc as the Test. this handles wild
                                    // cases that companion doesn't.
                                    var formerFirst = newGraph.First();
                                    Insert(newGraph, g.Start, formerFirst, ifNode, EdgeType.Follow, EdgeType.IfTest);
                                    newGraph.Add(EdgeType.Follow, lastNewNode, current);
                                }
                                lastNewNode = ifNode;

                                // add Then graph and link it
                                CopyNodes(thenGraph, newGraph);
                                newGraph.Add(EdgeType.IfThen, ifNode, thenGraph.First());

                                // add Else graph and link it
                                if (elseGraph != null)
                                {
                                    CopyNodes(elseGraph, newGraph);
                                    newGraph.Add(EdgeType.IfElse, ifNode, elseGraph.First());
                                }

                                // tag the node if the bnt says AstBuilder should generate an Else-Break/Continue
                                if (current.Flags.HasFlag(NodeFlag.AddElseBreak))
                                {
                                    ifNode.Flags |= NodeFlag.AddElseBreak;
                                    ifNode.LoopLevel = current.LoopLevel;
                                }
                                else if (current.Flags.HasFlag(NodeFlag.AddElseContinue))
                                {
                                    ifNode.Flags |= NodeFlag.AddElseContinue;
                                    ifNode.LoopLevel = current.LoopLevel;
                                }
                            }
                            else
                            {
                                // else was already visited.
                                // that means Then is an If statement who shares an Else,
                                // so we are part of an And condition.
                                var ifNode = thenGraph.First();
                                if (ifNode.Type != NodeType.If) throw new Exception("Expected If-Then-Else to merge, got: " + ifNode);
                                var elseNode = thenGraph.Successor(ifNode, EdgeType.IfElse);
                                if (elseNode == null) throw new Exception("Expected If-Then-Else to merge, but it has no else: " + ifNode);

                                // add myself to the graph
                                newGraph.Add(EdgeType.Follow, lastNewNode, current);

                                // copy Then to the graph
                                CopyNodes(thenGraph, newGraph);

                                // get the existing Test edge / node
                                var testEdge = newGraph.Successors[ifNode].Single(e => e.Type == EdgeType.IfTest);
                                var testNode = testEdge.B;

                                // if the test node is not an And, create one
                                var andNode = testNode;
                                if (andNode.Type != NodeType.And)
                                {
                                    newGraph.RemoveEdge(testEdge);
                                    andNode = new Node();
                                    andNode.Type = NodeType.And;
                                    newGraph.Add(EdgeType.IfTest, ifNode, andNode);
                                    newGraph.Add(EdgeType.Operand, andNode, testNode);
                                }

                                // move all previous nodes in the graph to be the
                                // first operand of the And node
                                var firstEdge = newGraph.Successors[g.Start].Single(e => e.Type == EdgeType.Follow);
                                newGraph.RemoveEdge(firstEdge);
                                var andOpEdge = new Edge { A = andNode, B = firstEdge.B, Type = EdgeType.Operand };
                                newGraph.Successors[andNode].Insert(0, andOpEdge); // must be first so that it's evaluated first
                                newGraph.Predecessors[firstEdge.B].Add(andOpEdge);

                                // make the If node the new first node
                                newGraph.Add(EdgeType.Follow, newGraph.Start, ifNode);
                                lastNewNode = ifNode;
                            }
                        }
                        else if (current.Last.Operation == Operation.bt)
                        {
                            // bt always means Or.
                            // BreakIf/ContinueIf bt's have already been identified (or guessed at?)
                            // so they won't be showing up here.
                            //
                            // This is easier than bnt handling, because Else isn't a factor.
                            // Two possibilities:
                            // 1. Create an If statement with the current node as the first operand
                            //    and the evaluated follow node as the second. Or....
                            // 2. Add myself as the first operand of an If whose test is Or.
                            //    (i could maybe skip this and leave it for an AST visitor to un-nest,
                            //     but i'd rather get this right here.)

                            // recurse to the "left", the second Or operand
                            Node btNext = g.Successor(current, EdgeType.BtNext);
                            Node ipdom = doms.ImmediatePostDominator(current);
                            Graph operandGraph = Recurse(g, doms, btNext, ipdom);

                            // i think the bt target is also the immediate post dominator.
                            // i'm counting on it!
                            Node btTarget = g.Successor(current, EdgeType.BtTarget);
                            if (btTarget != ipdom) throw new Exception("bt target != ipdom: " + current + " => " + btTarget + ", ipdom: " + ipdom);

                            // i don't think i need to consume the current graph as a test like bnt does.
                            // OH: only merge Ors if the incoming one doesn't have successors, otherwise
                            // it has a Not or something. Now I'm thinking I should remove Or merging from
                            // CFA since i have to do it anyway in AST for good results. make this simpler?
                            if (operandGraph.First().Type == NodeType.Or && operandGraph.Successors.Count == 0)
                            {
                                // get the relevant nodes
                                var orNode = operandGraph.First();

                                // copy everybody over, append the existing Or to the current graph
                                CopyNodes(operandGraph, newGraph);
                                newGraph.Add(EdgeType.Follow, lastNewNode, orNode);
                                lastNewNode = orNode;

                                // sneak the current node in as the first Or operand.
                                newGraph.Add(current);
                                var opEdge = new Edge { A = orNode, B = current, Type = EdgeType.Operand };
                                newGraph.Successors[orNode].Insert(0, opEdge);
                                newGraph.Predecessors[current].Add(opEdge);
                            }
                            else
                            {
                                // build an Or and append it to the current graph
                                var orNode = new Node();
                                orNode.Type = NodeType.Or;
                                newGraph.Add(EdgeType.Follow, lastNewNode, orNode);
                                lastNewNode = orNode;

                                // first operand = the current node
                                newGraph.Add(EdgeType.Operand, orNode, current);

                                // second operand = the child graph
                                var operandNode = operandGraph.First();
                                CopyNodes(operandGraph, newGraph);
                                newGraph.Add(EdgeType.Operand, orNode, operandNode);
                            }
                        }
                        else throw new Exception("impossible block: " + current);
                        break;
                    case NodeType.Switch:
                    case NodeType.Loop:
                        // treat switch/loops like a basic block:
                        // - sanity checks
                        // - add to the graph
                        if (successors.Count != 1)
                            throw new Exception("switches/loops should have one successor");
                        if (successors[0].Type != EdgeType.Follow)
                            throw new Exception("switches/loops should only have a follow edge");
                        if (successors[0].B != doms.ImmediatePostDominator(current))
                            throw new Exception("switch/loop follow node isn't immediate post dominator");

                        newGraph.Add(EdgeType.Follow, lastNewNode, current);
                        lastNewNode = current;
                        break;
                    default:
                        throw new Exception("Recursing on unexpected node type: " + current);
                }

                current = doms.ImmediatePostDominator(current);
            } while (current != stop);

            return newGraph;
        }

        static void Insert(Graph g, Node a, Node b, Node newNode, EdgeType newEdgeA, EdgeType newEdgeB)
        {
            g.Successors[a].RemoveAll(e => e.B == b);
            g.Predecessors[b].RemoveAll(e => e.A == a);
            g.Add(newEdgeA, a, newNode);
            g.Add(newEdgeB, newNode, b);
        }

        Graph SolveSwitch(Graph g, SwitchSummary s, LoopSummary l)
        {
            var switchGraph = new Graph();

            // add the head node to the new graph.
            // the head node contains the "push" and has already been
            // split into its own block when calculating leaders.
            // it won't contain the first dup or anything extra.
            // that's why there's no solving to do here.
            var headNode = g.GetByInstruction(s.Push);
            switchGraph.Add(EdgeType.SwitchHead, g.Start, headNode);

            // solve each case and add it to the switch graph
            foreach (var c in s.Cases)
            {
                var caseNode = new Node();
                caseNode.Type = NodeType.Case;
                switchGraph.Add(EdgeType.SwitchCase, headNode, caseNode);

                // switch case test.
                // null if the optimizer removed the ldi instruction.
                // AstBuilder will just consume acc when there is no CaseTest.
                if (c.Cond != null)
                {
                    // case tests can be complex, although they are usually a number.
                    // companion doesn't expect this; kq5 rm003 river:handleEvent.
                    var caseTestNode = new Node();
                    caseTestNode.Type = NodeType.Subgraph;

                    // copy the graph from dup to eq
                    var caseTestInput = CopyGraph(g, c.Dup, c.Eq);

                    // trim the graph: delete the dup.
                    // node that this alters the node, so the change will be seen in all graphs.
                    var dupNode = caseTestInput.GetByInstruction(c.Dup);
                    dupNode.First = dupNode.First.Next;

                    // trim the graph: delete the eq and anything that follows it (bnt)
                    var eqNode = caseTestInput.GetByInstruction(c.Eq);
                    if (eqNode.First != c.Eq)
                    {
                        // usually there is only one block and it ends in eq
                        // and probably bnt, so just truncate at eq.
                        eqNode.Last = c.Eq.Prev;
                    }
                    else
                    {
                        // oh how exciting, there are branches in the test
                        // and eq is its own node. remove the whole eq node,
                        // pointing all of its predecessors at its follower (End).
                        caseTestInput.RemoveNodeAndRedirectToFollow(eqNode);
                    }

                    // sanity check that the graph now starts with Cond instruction
                    var condNode = caseTestInput.GetByInstruction(c.Cond);
                    var firstNode = caseTestInput.First();
                    if (condNode != firstNode) throw new Exception("switch case graph does not start with condition node");

                    caseTestNode.Subgraph = Solve(caseTestInput);
                    switchGraph.Add(EdgeType.CaseTest, caseNode, caseTestNode);
                }

                // switch case body.
                // null if the body is empty.
                if (c.Body != null)
                {
                    var caseBodyNode = new Node();
                    caseBodyNode.Type = NodeType.Subgraph;
                    var lastInstruction = c.Jmp ?? s.Toss;
                    var caseBodyInput = CopyGraph(g, c.Body, lastInstruction);
                    if (l != null)
                    {
                        var unsolvedBnts = (from b in l.UnsolvedBranches
                                            where c.Body.Position <= b.Position &&
                                                  b.Position < lastInstruction.Position
                                            orderby b.Position descending
                                            select b).ToList();
                        foreach (var unsolvedBnt in unsolvedBnts)
                        {
                            SolveLoopBnt(caseBodyInput, unsolvedBnt, l, lastInstruction);
                            l.UnsolvedBranches.Remove(unsolvedBnt);
                        }
                    }
                    caseBodyNode.Subgraph = Solve(caseBodyInput);
                    switchGraph.Add(EdgeType.CaseBody, caseNode, caseBodyNode);
                }

                // god speed, little flag! may you someday reach sq1
                if (c.Bnt != null && c.Bnt.HasFlag(InstructionFlag.CompilerBug))
                {
                    caseNode.Flags |= NodeFlag.CompilerBug;
                }
            }

            // switch else
            if (s.Else != null)
            {
                var elseNode = new Node();
                elseNode.Type = NodeType.Subgraph;
                var elseInput = CopyGraph(g, s.Else, s.Toss);
                if (l != null)
                {
                    var unsolvedBnts = (from b in l.UnsolvedBranches
                                        where s.Else.Position <= b.Position &&
                                              b.Position < s.Toss.Position
                                        orderby b.Position descending
                                        select b).ToList();
                    foreach (var unsolvedBnt in unsolvedBnts)
                    {
                        SolveLoopBnt(elseInput, unsolvedBnt, l, s.Toss);
                        l.UnsolvedBranches.Remove(unsolvedBnt);
                    }
                }
                elseNode.Subgraph = Solve(elseInput);
                switchGraph.Add(EdgeType.SwitchElse, headNode, elseNode);
            }

            return switchGraph;
        }

        Graph SolveLoop(Graph g, LoopSummary l)
        {
            // Loop subgraph holds one or multiple nodes; order doesn't matter.
            // AstBuilder will be explicitly looking for each.
            // For now I'm just going to hang them all off of Start.
            // That might break some assumptions in other graph code, hope not.
            var loopGraph = new Graph();

            // defang the branches that are part of loop structure so that when
            // passing off subgraphs to Solve(), nothing gets confused by them.
            // really, i would like to exclude them, but that means mutating the
            // block nodes and that screws up the caller. so i'm just flaggin.
            l.Latch.Flags |= InstructionFlag.DefangedBranch;
            if (l.TestDominator != null)
            {
                l.TestDominator.Flags |= InstructionFlag.DefangedBranch;
            }

            // loop test
            if (l.TestDominator != null)
            {
                var testNode = new Node();
                testNode.Type = NodeType.Subgraph;

                // copy the test into a new graph.
                // excludes:
                // - back edge from loop body
                // - bnt edges from the test dominator (last bnt)
                var testInput = CopyGraph(g, l.Start, l.TestDominator);

                // solve the test; there's nothing special about it anymore.
                // AstBuilder will just process this and consume acc.
                testNode.Subgraph = Solve(testInput);
                loopGraph.Add(EdgeType.LoopTest, loopGraph.Start, testNode);
            }

            // for reinit
            if (l.ForReinit != null)
            {
                var incNode = new Node();
                incNode.Type = NodeType.Subgraph;
                // ASSUMPTION: ForReinit is the leader of a node.
                // i am doing that in GetLeaders() because that's easy, and the
                // only place that figures out ForReinit is loop deduction,
                // which comes first. could also split the node here on the
                // input graph before copying.
                var incInput = CopyGraph(g, l.ForReinit, l.Latch);
                incNode.Subgraph = Solve(incInput);
                loopGraph.Add(EdgeType.ForLoopReinit, loopGraph.Start, incNode);
            }

            // loop body
            var bodyNode = new Node();
            bodyNode.Type = NodeType.Subgraph;
            var bodyStart = (l.TestDominator == null) ? l.Start : l.TestDominator.Next;
            var bodyEnd = (l.ForReinit == null) ? l.Latch : l.ForReinit;
            var bodyInput = CopyGraph(g, bodyStart, bodyEnd, true); // ignoreBackEdges = true
            // solve any remaining bnt's that go to the head or follow.
            // this mutates the input graph and makes it suitable for solving.
            // must do this in reverse order.
            foreach (var unsolvedBnt in l.UnsolvedBranches.OrderByDescending(b => b.Position))
            {
                SolveLoopBnt(bodyInput, unsolvedBnt, l, l.Latch);
            }
            l.UnsolvedBranches.Clear();
            if (l.ForReinit != null)
            {
                bodyInput.RemoveNodeAndRedirectToFollow(bodyInput.GetByInstruction(l.ForReinit));
            }
            bodyNode.Subgraph = Solve(bodyInput);
            loopGraph.Add(EdgeType.LoopBody, loopGraph.Start, bodyNode);

            return loopGraph;
        }

        // TODO: document this better
        // copies all nodes and edges from source to destination, except for start/end
        static void CopyNodes(Graph src, Graph dst)
        {
            foreach (var node in src.Nodes.Where(n => n.IsNotStartOrEnd))
            {
                dst.Add(node);
            }
            foreach (var edge in src.Edges.Where(e => e.A.IsNotStartOrEnd && e.B.IsNotStartOrEnd))
            {
                dst.Successors[edge.A].Add(edge);
                dst.Predecessors[edge.B].Add(edge);
            }
        }

        // TODO: document this critical function, i was deep down the rabbit hole at the time
        static Graph CopyGraph(Graph g, Instruction firstInstruction, Instruction lastInstruction, bool ignoreBackEdges = false)
        {
            var newGraph = new Graph();
            var startNode = g.GetByInstruction(firstInstruction);
            var endNode = g.GetByInstruction(lastInstruction);

            // copy all nodes within range
            foreach (var node in g.Nodes.Where(n => n.First != null))
            {
                if ((firstInstruction.Position <= node.First.Position && node.Last.Position <= lastInstruction.Position) ||
                    node.Contains(firstInstruction) ||
                    node.Contains(lastInstruction))
                {
                    newGraph.Add(node);
                }
            }

            // copy all edges between nodes that have been copied. (ignoring start and end)
            // assumption: this is a self contained subgraph and nothing escapes it.
            // TODO: how will this work with switches with unsolved break/continues?
            foreach (var node in newGraph.Nodes.Where(n => n.First != null))
            {
                foreach (var edge in g.Successors[node].Where(e => e.B.First != null))
                {
                    if (newGraph.Nodes.Contains(edge.B))
                    {
                        if (ignoreBackEdges && edge.A.First.Position > edge.B.First.Position) continue;

                        newGraph.Add(edge.Type, edge.A, edge.B);
                    }
                }
            }

            // add start and end edges
            newGraph.Add(EdgeType.Follow, g.Start, startNode);
            newGraph.Add(EdgeType.Follow, endNode, g.End);
            return newGraph;
        }

        // TODO: document this critical function, i was deep down the rabbit hole at the time
        static void Replace(Graph g, Node newNode)
        {
            var startNode = g.GetByInstruction(newNode.First);
            var endNode = g.GetByInstruction(newNode.Last);
            var edgesToStart = g.Predecessors[startNode].ToList(); // copy
            var edgesFromEnd = g.Successors[endNode].ToList(); // copy [ and i'm assuming loop latch back edge has been removed ]
            if (edgesFromEnd.Count > 1) throw new Exception("Nope!"); // usually 1, 0 if infinite loop

            // delete all nodes and edges within instruction range
            foreach (var node in g.Nodes.Where(n => n.IsNotStartOrEnd).ToList()) // copy
            {
                if (newNode.First.Position <= node.First.Position &&
                    node.Last.Position <= newNode.Last.Position)
                {
                    foreach (var successor in g.Successors[node])
                    {
                        g.Predecessors[successor.B].RemoveAll(e => e.A == node);
                    }
                    foreach (var predecessor in g.Predecessors[node])
                    {
                        g.Successors[predecessor.A].RemoveAll(e => e.B == node);
                    }
                    g.Successors.Remove(node);
                    g.Predecessors.Remove(node);
                }
            }

            // add the new node
            g.Add(newNode);
            // replace the edges to the start
            foreach (var edge in edgesToStart)
            {
                g.Add(edge.Type, edge.A, newNode);
            }
            // replace (or add!) a follow edge to the next instruction's node
            var followNode = g.GetByInstruction(newNode.Last.Next);
            g.Add(EdgeType.Follow, newNode, followNode);
        }

        // given a list of instructions and a *sorted* list of leaders,
        // build a cfg with basic block nodes.
        // assumptions: all branch targets are legal. zero is okay.
        // 1. create each basic block, add to graph, place in dictionary with instruction pos as key.
        // 2. foreach basic block in order
        //      if first, add edge from start
        //      last instruction:
        //        ret: do nothing
        //        jmp: add edge from here to block[target]
        //        bnt/bt: add two edges from here to block[target]
        //        else: add edge to next block
        //
        // this is a generic cfg algorithm, with the small exception that it ignores
        // branch instructions that have been identified as breaks/continues or have
        // had their defanged flag set so that they're not treated as control flow.
        public static Graph CreateGraph(InstructionList instructions, IReadOnlyList<int> leaders)
        {
            var g = new Graph();

            // create a basic block for each leader and add it to the graph
            var blocks = new Dictionary<int, Node>();
            for (int i = 0; i < leaders.Count; ++i)
            {
                var block = new Node();
                block.Type = NodeType.Block;
                block.First = instructions[leaders[i]];
                if (i + 1 < leaders.Count)
                {
                    block.Last = instructions[leaders[i + 1]].Prev;
                }
                else
                {
                    block.Last = instructions.Last;
                }
                blocks.Add(block.First.Position, block);
                g.Add(block);
            }

            // add edge between start node and first block
            g.Add(EdgeType.Follow, g.Start, blocks[leaders[0]]);

            // add edge between last block and end node
            g.Add(EdgeType.Follow, blocks[leaders[leaders.Count - 1]], g.End);

            // add edges between blocks based on last instruction in block.
            // skip the last block because it won't have any outgoing edges.
            for (int i = 0; i + 1 < leaders.Count; ++i)
            {
                var block = blocks[leaders[i]];
                var next = blocks[leaders[i + 1]];
                var lastOperation = block.Last.Operation;
                if (!block.Last.IsBranch)
                {
                    // hack for how bts/bnts/jmps flagged Break/Continue are no longer branches
                    lastOperation = Operation.unused;
                }
                switch (lastOperation)
                {
                    case Operation.bnt:
                        g.Add(EdgeType.BntNext, block, next);
                        g.Add(EdgeType.BntTarget, block, blocks[block.Last.BranchTarget]);
                        break;

                    case Operation.bt:
                        g.Add(EdgeType.BtNext, block, next);
                        g.Add(EdgeType.BtTarget, block, blocks[block.Last.BranchTarget]);
                        break;

                    case Operation.jmp:
                        g.Add(EdgeType.Follow, block, blocks[block.Last.BranchTarget]);
                        break;

                    default:
                        g.Add(EdgeType.Follow, block, next);
                        break;
                }
            }

            return g;
        }

        // this a leaders algorithm that includes SCI specifics
        public static List<int> GetLeaders(InstructionList instructions,
                                           IReadOnlyList<SwitchSummary> switches,
                                           IReadOnlyList<LoopSummary> loops)
        {
            // get leaders the normal way
            HashSet<int> leaders = GetLeadersUnsorted(instructions);

            // I have five switch-specific leader tweaks:
            //
            // 1. The instruction after a toss is a leader. Companion does this too.
            // 2. The toss instruction is a leader. That sounds redundant because every
            //    case has at least one branch to the toss statement, so the generic
            //    algorithm should have gotten them. But there are empty switches with
            //    no branches, like pq4 100 egoScript:changeState:
            //       push
            //       dup
            //       ldi 00
            //       eq
            //       toss
            //    (switch (= state param1) (0))
            //    Companion doesn't do this right. Not surprising; there's no branch!
            // 3. The push of a switch is a leader. I did this to make the graph pleasant
            //    to look at; I don't know if anything grew to depend on this. I hope not?
            // 4. The first dup in a switch is a leader. This separates it from the head
            //    node, and while I also did this to make a pleasant graph, CFA now depends
            //    on this. SolveSwitch() needs to be updated if this changes.
            //    And if there is no dup, the lone else is a leader.
            // 5. For loop Re-initializers are leaders. Rare, but these may have been
            //    discovered during loop deduction. Split them into their own node here,
            //    where it's easy.
            if (switches != null) // switches is null when decompiling failed, but dumping graphs
            {
                foreach (var switch_ in switches)
                {
                    leaders.Add(switch_.Push.Position);

                    if (switch_.Cases.Any())
                    {
                        var firstDup = switch_.Cases[0].Dup;
                        leaders.Add(firstDup.Position);
                    }
                    else if (switch_.Else != null)
                    {
                        leaders.Add(switch_.Else.Position);
                    }
                    leaders.Add(switch_.Toss.Position);
                    leaders.Add(switch_.Toss.Next.Position);
                }
            }
            if (loops != null) // loops is null when decompiling failed, but dumping graphs
            {
                foreach (var loop in loops.Where(l => l.ForReinit != null))
                {
                    leaders.Add(loop.ForReinit.Position);
                }
            }
            return leaders.OrderBy(l => l).ToList();
        }

        // this is a generic leaders algorithm, minus sorting
        static HashSet<int> GetLeadersUnsorted(InstructionList instructions)
        {
            // leader rules:
            // 1. the first instruction is a leader
            // 2. branch targets are leaders
            // 3. instructions following branches are leaders
            var leaders = new HashSet<int>();
            if (instructions.Any())
            {
                leaders.Add(instructions.First.Position);
            }
            foreach (var instruction in instructions)
            {
                if (instruction.IsBranch)
                {
                    leaders.Add(instruction.BranchTarget);
                    if (instruction.Next != null) // there will always be a next
                    {
                        leaders.Add(instruction.Next.Position);
                    }
                }
            }
            return leaders;
        }
    }
}
