using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SCI.Decompile.Cfg
{
    enum NodeType
    {
        Block,
        Start,
        End,

        If,
        And,
        Or,

        Switch,
        Case,
        Subgraph, // case test, case body, switch else, loop test, loop body

        Loop,
    }

    enum EdgeType
    {
        // Initial values when everything is a block
        Follow,    // default, applies to jmps
        BntTarget,
        BntNext,
        BtTarget,
        BtNext,

        IfTest,
        IfThen,
        IfElse,

        Operand, // Ands and Ors

        SwitchHead, // the push statement
        SwitchCase,
        CaseTest,
        CaseBody,
        SwitchElse,

        LoopTest,
        LoopBody,
        ForLoopInit,
        ForLoopReinit,
    }

    [Flags]
    enum NodeFlag
    {
        None = 0,
        CompilerBug = 1,
        AddElseBreak = 2,
        AddElseContinue = 4,
    }

    class Node
    {
        public NodeType Type;
        public Instruction First;  // set on blocks, switches, and loops
        public Instruction Last;   // set on blocks, switches, and loops
        public NodeFlag Flags;
        public LoopType LoopType;
        public int LoopLevel; // set when AddElseBreak/Continue is set

        public Graph Subgraph;

        public override string ToString()
        {
            if (First == null)
            {
                return Type.ToString();
            }
            else if (First == Last || Last == null)
            {
                return Type.ToString() + ": " + First;
            }
            else
            {
                return Type.ToString() + ": " + First + " - " + Last;
            }
        }

        // i should just add RealNodes() to Graph, huh?
        public bool IsNotStartOrEnd { get { return !(Type == NodeType.Start || Type == NodeType.End); } }

        public bool Contains(Instruction i)
        {
            return First != null && Last != null && First.Position <= i.Position && i.Position <= Last.Position;
        }
    }

    class Edge
    {
        public EdgeType Type;
        public Node A; // Source
        public Node B; // Destination

        public override string ToString()
        {
            return Type.ToString() + ": [" + A + "] => [" + B + "]";
        }
    }

    class Graph
    {
        public Dictionary<Node, List<Edge>> Successors { get; private set; }
        public Dictionary<Node, List<Edge>> Predecessors { get; private set; }

        public IEnumerable<Node> Nodes { get { return Successors.Keys; } }
        public IEnumerable<Edge> Edges { get { return Successors.Values.SelectMany(v => v); } }

        static Node start = new Node() { Type = NodeType.Start };
        public Node Start { get; private set; } = start;

        static Node end = new Node() { Type = NodeType.End };
        public Node End { get; private set; } = end;

        public override string ToString()
        {
            return "CFG: " + Nodes.Count() + " nodes, " + Edges.Count() + " edges";
        }

        public Graph()
        {
            Successors = new Dictionary<Node, List<Edge>>();
            Predecessors = new Dictionary<Node, List<Edge>>();
            Add(Start);
        }

        public void Add(Node node)
        {
            Successors.Add(node, new List<Edge>(2));
            Predecessors.Add(node, new List<Edge>());
        }

        public void Add(EdgeType type, Node parent, Node node)
        {
            if (!Nodes.Contains(node))
            {
                Add(node);
            }

            var edge = new Edge { Type = type, A = parent, B = node };
            Successors[parent].Add(edge);
            Predecessors[node].Add(edge);
        }

        public void RemoveEdge(Edge edge)
        {
            Successors[edge.A].Remove(edge);
            Predecessors[edge.B].Remove(edge);
        }

        public Node First()
        {
            return Successor(Start, EdgeType.Follow);
        }

        public Node Successor(Node node, EdgeType type)
        {
            return SuccessorNodes(node, type).SingleOrDefault();
        }

        public IEnumerable<Node> SuccessorNodes(Node node, EdgeType type)
        {
            return Successors[node].Where(e => e.Type == type).Select(e => e.B);
        }

        public Node Follower(Node node)
        {
            return Successor(node, EdgeType.Follow);
        }

        public Node GetByInstruction(Instruction i)
        {
            return Nodes.First(n => n.Contains(i));
        }

        public void RemoveNodeAndRedirectToFollow(Node node)
        {
            var predEdges = Predecessors[node].ToList();
            var succEdges = Successors[node].ToList();
            //var leaderEdge = predEdges.First(e => e.Type == EdgeType.Follow);
            var followerEdge = succEdges.First(e => e.Type == EdgeType.Follow);

            // remove successor edges
            foreach (var e in succEdges)
            {
                RemoveEdge(e);
            }

            // remove node
            Predecessors.Remove(node);
            Successors.Remove(node);

            // edges that pointed to the removed node must now point to the follower
            foreach (var e in predEdges)
            {
                e.B = followerEdge.B;
                Predecessors[followerEdge.B].Add(e);
            }
        }

        public Node Split(Instruction i)
        {
            // nothing to do if i's node starts with i.
            var node = GetByInstruction(i);
            if (node.Type != NodeType.Block) throw new Exception("Attempted to split a non-block");
            if (node.First == i) return node;

            // copy all the old edges
            var oldSuccessorEdges = Successors[node].ToList();
            var oldPredecessorEdges = Predecessors[node].ToList();

            // remove the node from the graph. two nodes will be created in its stead.
            // (sigh, because i can't truncate one of the nodes because that will screw
            //  up other graphs that use this node)
            foreach (var e in oldSuccessorEdges)
                RemoveEdge(e);
            foreach (var e in oldPredecessorEdges)
                RemoveEdge(e);
            Successors.Remove(node);
            Predecessors.Remove(node);

            // create two new nodes and link them
            var newNode1 = new Cfg.Node();
            newNode1.First = node.First;
            newNode1.Last = i.Prev;
            var newNode2 = new Cfg.Node();
            newNode2.First = i;
            newNode2.Last = node.Last;
            Add(newNode1);
            Add(EdgeType.Follow, newNode1, newNode2);

            foreach (var successorEdge in oldSuccessorEdges)
            {
                Add(successorEdge.Type, newNode2, successorEdge.B);
            }
            foreach (var predecessorEdge in oldPredecessorEdges)
            {
                Add(predecessorEdge.Type, predecessorEdge.A, newNode1);
            }

            // done!
            return newNode2;
        }

        // Reverse the graph for calculating post dominance.
        // Swaps start and end, swaps edge directions.
        public void Reverse()
        {
            var t1 = Start;
            Start = End;
            End = t1;

            foreach (var edge in Edges)
            {
                edge.B = Interlocked.Exchange(ref edge.A, edge.B);
            }

            var t2 = Predecessors;
            Predecessors = Successors;
            Successors = t2;
        }

        // Removes unreachable nodes from the graph.
        // The Semi-NCA algorithm that I use to calculate dominators requires
        // that all nodes be reachable from the root. Even if it didn't, I
        // reverse the graph to calculate post dominators, and that would mean
        // that some nodes wouldn't reach the new "end".
        //
        // Unreachable nodes are rare, but they can occur in loops due to
        // a break/continue (jmp) and branch-to-branch optimization.
        // Loop preprocessing detects and "repairs" most of these, but
        // some are left behind and recreated if necessary by CFA.
        // CFA walks the graph from the top down so it never sees
        // unreachable nodes; deleting them before calculating dominance
        // has no other effect on the results.
        public void RemoveUnreachableNodes()
        {
            Queue<Node> queue = null;
            do
            {
                // delete queued nodes
                while (queue?.Count > 0)
                {
                    Node n = queue.Dequeue();
                    foreach (var succEdge in Successors[n])
                    {
                        Predecessors[succEdge.B].Remove(succEdge);
                    }
                    Successors.Remove(n);
                    Predecessors.Remove(n);
                }

                // enqueue nodes without predecessors for deletion
                foreach (var kv in Predecessors)
                {
                    if (kv.Value.Count == 0 && kv.Key != Start)
                    {
                        if (queue == null)
                        {
                            queue = new Queue<Node>();
                        }
                        queue.Enqueue(kv.Key);
                    }
                }
            } while (queue?.Count > 0);
        }
    }
}
