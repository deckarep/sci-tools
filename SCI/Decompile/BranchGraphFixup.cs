using System.Collections.Generic;
using System.Linq;
using SCI.Decompile.Cfg;
using SCI.Resource;

// Deoptimizes bnts/bts using a graph. (bnt is more common)
//
// This doesn't happen very often, but when it does, it creates a funky graph
// that breaks the structure my CFA algorithm expects. Companion can't handle
// these either; it falls back to assembly on all of them.
//
// KQ4 #0 KQ4:handleEvent (massive)
// KQ4 #78 Room78:handleEvent (kinda big)
// Longbow #25 threat:changeState (it has two!)
// phant2 #4031 curtisCubicleRoomCH1SR3:call (bt instead of bnt)
//              (Wow this function has an or-and-or-and-or nesting)
//
// For every bnt/bt, take the immediate dominator, walk the graph up to it,
// and if there are any blocks with the same branch type and target as the
// original bnt/bt, that's a branch-to-branch optimization that breaks
// the graph. Deoptimize!

namespace SCI.Decompile
{
    static class BranchGraphFixup
    {
        public static void Fixup(Graph g, Dominance doms, Function f)
        {
            bool changed = false;
            do
            {
                // recalculate dominance whenever there's a change.
                // it's rare for there to be any changes.
                if (changed)
                {
                    doms.Update();
                    changed = false;
                }

                var queue = new Queue<Node>();
                var visited = new HashSet<Node>();

                var branchBlocks = from n in g.Nodes
                                   where n.Type == NodeType.Block &&
                                         n.Last.IsBranch && // ignore breakif/continueif
                                         n.Last.Operation != Operation.jmp
                                   select n;
                foreach (var branchBlock in branchBlocks)
                {
                    var op = branchBlock.Last.Operation;
                    var edgeType = (op == Operation.bnt) ? EdgeType.BntTarget : EdgeType.BtTarget;
                    var idom = doms.ImmediateDominator(branchBlock);

                    // optimization: if the branch target only has one predecessor
                    // then there's nothing to do.
                    var targetBlock = g.Successor(branchBlock, edgeType);
                    if (g.Predecessors[targetBlock].Count == 1) continue;

                    // walk the graph up to the immediate dom
                    queue.Clear();
                    visited.Clear();
                    queue.Enqueue(branchBlock);
                    while (queue.Any())
                    {
                        // dequeue, skip if we've reached dominator
                        var node = queue.Dequeue();
                        if (node == idom) continue;

                        // don't visit the same node twice
                        if (visited.Contains(node)) continue;
                        visited.Add(node);

                        // is this node a branch like mine that targets my target?
                        // if so, reel 'er in!
                        if (node != branchBlock &&
                            node.Type == NodeType.Block &&
                            node.Last.Operation == op &&
                            node.Last.BranchTarget == branchBlock.Last.BranchTarget)
                        {
                            var badBranch = node.Last;
                            string old = badBranch.ToString();

                            // patch the instruction
                            badBranch.Flags |= InstructionFlag.DeoptimizedGraph;
                            badBranch.BranchTarget = branchBlock.Last.Position;

                            // patch the graph
                            var edge = g.Successors[node].First(e => e.Type == edgeType);
                            g.Predecessors[edge.B].Remove(edge);
                            edge.B = branchBlock;
                            g.Predecessors[edge.B].Add(edge);

                            Log.Debug(f, "Deoptimizing branch with a graph: " + old + " => " + badBranch);

                            changed = true;
                        }

                        // queue the predecessors
                        foreach (var e in g.Predecessors[node])
                        {
                            queue.Enqueue(e.A);
                        }
                    }
                    if (changed) break;
                }
            } while (changed);
        }
    }
}
