using System;
using System.Collections.Generic;
using System.Linq;

// Graph Dominance Calculation
//
// Graph dominance is powerful information about a control flow graph that
// lets a dummy like me reason about things and invent working control flow
// analysis algorithms out of thin air. I had never heard of graph dominance
// until Phil's articles, but it was clear that this was the foundation to
// reasoning about and reconstructing high level control flow. I either took
// the wrong computer science classes or slept through the right ones.
//
// Graph dominance is about knowing what nodes you have to go through to get
// between nodes A and B. The nodes that you have to go through to reach a
// node are its dominators. The nodes that you have to go through after a
// node are its post dominators. The closest node in each set is called the
// immediate dominator or the immediate post dominator.
//
// The study of how to calculate dominators efficiently has a long history.
// I'm proud to have used a naive algorithm that was good enough so that
// I could move on to more important things. One year later, I switched
// to the Semi-NCA algorithm to fix pathologically slow cases, but I still
// use the first algorithm in a debugging feature because the input graphs
// don't meet Semi-NCA's preconditions.

namespace SCI.Decompile.Cfg
{
    // for testing SemiNCA vs Naive
    public static class DominanceConfig
    {
        public static bool SemiNCA = true;
    }

    // Dominance base class so that I can try different algorithms.
    // Algorithms should compute what they need as lazily as possible;
    // it was a year later that I realized how much unnecessary work
    // I'd being doing up front in the naive algorithm.
    abstract class Dominance
    {
        protected Graph g;

        public abstract HashSet<Node> Dominators(Node n);
        public abstract HashSet<Node> PostDominators(Node n);

        public abstract Node ImmediateDominator(Node n);
        public abstract Node ImmediatePostDominator(Node n);

        public static Dominance Create(Graph g)
        {
            if (DominanceConfig.SemiNCA)
            {
                return new SemiNCA(g);
            }
            else
            {
                return new NaiveDominance(g);
            }
        }

        // call this if the graph is updated so that recalculation occurs
        public abstract void Update();

        protected static IEnumerable<Node> Predecessors(Graph g, Node node)
        {
            return g.Predecessors[node].Select(e => e.A);
        }

        protected static IEnumerable<Node> Successors(Graph g, Node node)
        {
            return g.Successors[node].Select(e => e.B);
        }
    }

    // "Naive algorithm" from the only readable dominator presentation I found:
    // https://sbaziotis.com/compilers/visualizing-dominators.html
    //
    // This was the first dominator algorithm that I used for the decompiler.
    // One year later, I replaced it with Semi-NCA. I still use it for graphviz
    // dumping, because this algorithm doesn't require nodes to be reachable,
    // and I want unreachable SCI nodes to appear on graphs so I can see them.
    //
    // I award Stefanos Baziotis and this algorithm: Heroes Of The Decompilation
    //
    // I didn't want to add any code that I didn't understand to my decompiler.
    // That would defeat the point and take the fun out of it. The traditional
    // algorithm to use for this is Lengauer-Tarjan from 1979, and that's what
    // Phil used in SCI Companion. I couldn't find a comprehensible explanation
    // or a pleasant reference implementation. I also knew that I was prone to
    // distractions on this stuff, and I was still so far away from actually
    // decompiling anything, so nerding out over this seemed like yet another
    // threat to ever finishing.
    //
    // Thanks to Stefanos' excellent presentation and pseudo code I was able to
    // understand the algorithm, implement it quickly, and verify the results.
    // Then I was able to easily reverse it for calculating post dominance,
    // although now I realize you can just reverse the graph first.
    // All this let me move on to the zillions of other tasks at hand, confident
    // that this one was in the bag.
    //
    // The speed gets exponentially worse with larger graphs, but in SCI that's
    // not a real problem except in the hoyle4-5 games, where their unbelievably
    // huge functions cause decompiling to take thirty seconds. Pretty bad!
    // Still, most games were in the 2-8 second range on this slow laptop,
    // and even the few pathological cases don't matter when mass decompiling
    // 300+ games at a time; you're going to wait no matter what.

    class NaiveDominance : Dominance
    {
        // Dominators
        // Key: Node being dominated (A)
        // Val: Nodes that dominate A. (they come before A)
        // In other words, who do you have to go through to reach A?
        // Immediate is the unique node that's the closest dominator.
        Dictionary<Node, HashSet<Node>> dominators;
        Dictionary<Node, Node> immediateDominators = new Dictionary<Node, Node>();

        // Post Dominators
        // Key: Node being post-dominated (A)
        // Val: Nodes that post-dominate A (they come after A)
        // In other words, who do you have to go through A to reach the exit?
        // Immediate is the unique node that's closest.
        Dictionary<Node, HashSet<Node>> postDominators;
        Dictionary<Node, Node> immediatePostDominators = new Dictionary<Node, Node>();

        public NaiveDominance(Graph g)
        {
            this.g = g;
        }

        public override HashSet<Node> Dominators(Node n)
        {
            if (dominators == null)
            {
                dominators = CalculateDominators(g);
            }
            return dominators[n];
        }

        public override HashSet<Node> PostDominators(Node n)
        {
            if (postDominators == null)
            {
                postDominators = CalculatePostDominators(g);
            }
            return postDominators[n];
        }

        public override Node ImmediateDominator(Node node)
        {
            // HACK: everyone's immediate dominator is the one with the
            // largest instruction number that isn't themselves.
            // this would not be true if there were cycles.
            Node idom;
            if (!immediateDominators.TryGetValue(node, out idom))
            {
                if (dominators == null)
                {
                    dominators = CalculateDominators(g);
                }

                idom = (from d in dominators[node]
                        where d != node
                        orderby d.First?.Position descending
                        select d).First();
                immediateDominators.Add(node, idom);
            }
            return idom;
        }

        public override Node ImmediatePostDominator(Node node)
        {
            Node ipdom;
            if (!immediatePostDominators.TryGetValue(node, out ipdom))
            {
                if (postDominators == null)
                {
                    postDominators = CalculatePostDominators(g);
                }

                ipdom = (from d in postDominators[node]
                         where d != node
                         orderby (d.First != null ? d.First.Position : int.MaxValue)
                         select d).First();
                immediatePostDominators.Add(node, ipdom);
            }
            return ipdom;
        }

        public override void Update()
        {
            // reset state
            dominators = null;
            immediateDominators.Clear();
            postDominators = null;
            immediatePostDominators.Clear();
        }

        static Dictionary<Node, HashSet<Node>> CalculateDominators(Graph g)
        {
            var dominators = new Dictionary<Node, HashSet<Node>>();
            dominators[g.Start] = new HashSet<Node>(new[] { g.Start });
            foreach (var a in g.Nodes.Where(n => n != g.Start))
            {
                dominators.Add(a, new HashSet<Node>(g.Nodes));
            }

            bool change;
            do
            {
                change = false;
                foreach (Node block in g.Nodes.Where(n => n != g.Start))
                {
                    HashSet<Node> temp = new HashSet<Node>(g.Nodes);
                    foreach (Node pred in Predecessors(g, block))
                    {
                        temp.IntersectWith(dominators[pred]);
                    }
                    temp.Add(block);
                    if (!temp.SetEquals(dominators[block]))
                    {
                        change = true;
                        dominators[block] = temp;
                    }
                }
            }
            while (change);

            return dominators;
        }

        static Dictionary<Node, HashSet<Node>> CalculatePostDominators(Graph g)
        {
            var postDominators = new Dictionary<Node, HashSet<Node>>();
            postDominators[g.End] = new HashSet<Node>(new[] { g.End });
            foreach (var a in g.Nodes.Where(n => n != g.End))
            {
                postDominators.Add(a, new HashSet<Node>(g.Nodes));
            }

            bool change;
            do
            {
                change = false;
                foreach (Node block in g.Nodes.Where(n => n != g.End))
                {
                    HashSet<Node> temp = new HashSet<Node>(g.Nodes);
                    foreach (Node succ in Successors(g, block))
                    {
                        temp.IntersectWith(postDominators[succ]);
                    }
                    temp.Add(block);
                    if (!temp.SetEquals(postDominators[block]))
                    {
                        change = true;
                        postDominators[block] = temp;
                    }
                }
            }
            while (change);

            return postDominators;
        }
    }

    // Semi-NCA from "Linear-Time Algorithms for Dominators and Related Problems"
    // by Loukas Georgiadis in 2005.
    //
    // Semi: Semidominators
    // NCA:  Nearest Common Ancestor
    //
    // Semi-NCA is an improvement on the well known 1979 Lengauer-Tarjan algorithm,
    // with Tarjan as Georgiadis' advisor at Princeton. It is simpler to implement
    // and performs faster in practice. LLVM switched to it in 2017.
    //
    // Semi-NCA calculates dominators in almost-linear time. This brought pathological
    // cases like hoyle5 down from 30 seconds of decompiling to 3, and that's on the
    // slow laptop. It's rare to find a game that spends more than 200ms in this code.
    // Camelot has a lot of big functions and it went down from 7 seconds to 2.
    // Average game decompilation time on the slow laptop is now 2.22 seconds.
    //
    // I used Dart and Julia as reference implementations.
    //
    // I added this a year later because I thought it would be fun to learn more about
    // dominator algorithms and fix the pathological cases now that the pressure is off.
    // The hope is still to release the decompiler eventually, and I'm much more
    // comfortable with that idea now that it is consistently fast.
    //
    // Semi-NCA requires that all nodes on a graph be reachable. SCI has unreachable
    // nodes when Sierra's compiler optimized a branch that pointed to a break or
    // continue in a loop. You have to delete unreachable nodes first. That's fine for
    // me, because at the point I'm calculating dominance, they can't affect anything.

    class SemiNCA : Dominance
    {
        Dictionary<Node, Node> immedateDominators;
        Dictionary<Node, Node> immedatePostDominators;
        Dictionary<Node, HashSet<Node>> dominators = new Dictionary<Node, HashSet<Node>>();
        Dictionary<Node, HashSet<Node>> postDominators = new Dictionary<Node, HashSet<Node>>();

        public SemiNCA(Graph g)
        {
            this.g = g;
        }

        public override HashSet<Node> Dominators(Node n)
        {
            // check cache
            HashSet<Node> result;
            if (dominators.TryGetValue(n, out result))
            {
                return result;
            }

            // calculate immediate dominators if not done yet
            if (immedateDominators == null)
            {
                immedateDominators = CalculateImmediateDominators(g);
            }

            // build this node's dominator set from immediate dominators
            result = new HashSet<Node>();
            result.Add(n);
            Node m = n;
            while (immedateDominators.TryGetValue(m, out m))
            {
                result.Add(m);
            }

            // cache it
            dominators.Add(n, result);
            return result;
        }

        public override HashSet<Node> PostDominators(Node n)
        {
            // check cache
            HashSet<Node> result;
            if (postDominators.TryGetValue(n, out result))
            {
                return result;
            }

            // calculate immediate post dominators if not done yet
            if (immedatePostDominators == null)
            {
                immedatePostDominators = CalculateImmediatePostDominators(g);
            }

            // build this node's post dominator set from immediate post dominators
            result = new HashSet<Node>();
            result.Add(n);
            Node m = n;
            while (immedatePostDominators.TryGetValue(m, out m))
            {
                result.Add(m);
            }

            // cache it
            postDominators.Add(n, result);
            return result;
        }

        public override Node ImmediateDominator(Node n)
        {
            if (immedateDominators == null)
            {
                immedateDominators = CalculateImmediateDominators(g);
            }
            return immedateDominators[n];
        }

        public override Node ImmediatePostDominator(Node n)
        {
            if (immedatePostDominators == null)
            {
                immedatePostDominators = CalculateImmediatePostDominators(g);
            }
            return immedatePostDominators[n];
        }

        public override void Update()
        {
            // reset state
            immedateDominators = null;
            immedatePostDominators = null;
            dominators.Clear();
            postDominators.Clear();
        }

        static Dictionary<Node, Node> CalculateImmediateDominators(Graph g)
        {
            return DoSemiNCA(g);
        }

        static Dictionary<Node, Node> CalculateImmediatePostDominators(Graph g)
        {
            // Calculate immediate post dominators by reversing the graph,
            // running Semi-NCA, and then reversing the graph back.
            g.Reverse();
            var results = DoSemiNCA(g);
            g.Reverse();
            return results;
        }

        static Dictionary<Node, Node> DoSemiNCA(Graph g)
        {
            int size = g.Nodes.Count();

            //
            // Depth First Search: assign every node a preorder number and store
            // the preorder number of their parent.
            //

            Node[] preorders = new Node[size];
            Dictionary<Node, int> preorderNumbers = new Dictionary<Node, int>(size);
            int[] parents = new int[size];

            dfs(g, g.Start, -1, preorders, preorderNumbers, parents);

            //
            // First pass: compute semidominators as in Lengauer-Tarjan.
            //

            int[] idoms = new int[size];
            int[] semis = new int[size];
            int[] labels = new int[size];
            for (int i = 0; i < size; i++)
            {
                idoms[i] = parents[i];
                semis[i] = i;
                labels[i] = i;
            }

            // Loop over nodes in reverse preorder, except the start
            for (int blockNumber = size - 1; blockNumber >= 1; blockNumber--)
            {
                Node node = preorders[blockNumber];

                // Loop over node's predecessors
                foreach (Node predecessor in Predecessors(g, node))
                {
                    // Look for the semidominator by ascending the semidominator path
                    // starting from predecessor.
                    int predecessorNumber = preorderNumbers[predecessor];
                    int best = predecessorNumber;
                    if (predecessorNumber > blockNumber)
                    {
                        compressPath(blockNumber, predecessorNumber, parents, labels);
                        best = labels[predecessorNumber];
                    }

                    // Update the semidominator if we've found a better one
                    semis[blockNumber] = Math.Min(semis[blockNumber], semis[best]);
                }

                // Now use the label for the semidominator
                labels[blockNumber] = semis[blockNumber];
            }

            //
            // Second pass: compute the immediate dominators as the nearest common ancestor
            // of spanning tree parent and semidominator, for all blocks except the start.
            //

            Dictionary<Node, Node> results = new Dictionary<Node, Node>(size - 1);
            for (int blockNumber = 1; blockNumber < size; blockNumber++)
            {
                int domNumber = idoms[blockNumber];
                while (domNumber > semis[blockNumber])
                {
                    domNumber = idoms[domNumber];
                }
                idoms[blockNumber] = domNumber;

                Node node = preorders[blockNumber];
                Node idomNode = preorders[domNumber];
                results.Add(node, idomNode);
            }
            return results;
        }

        static void dfs(Graph g, Node r, int parent,
                        Node[] preorders, Dictionary<Node, int> preorderNumbers, int[] parents)
        {
            int number = preorderNumbers.Count;

            preorders[number] = r;
            preorderNumbers[r] = number;
            parents[number] = parent;

            foreach (var successor in Successors(g, r))
            {
                if (!preorderNumbers.ContainsKey(successor))
                {
                    // successor hasn't been visited yet
                    dfs(g, successor, number, preorders, preorderNumbers, parents);
                }
            }
        }

        static void compressPath(int start, int current, int[] parents, int[] labels)
        {
            int next = parents[current];
            if (next > start)
            {
                compressPath(start, next, parents, labels);
                labels[current] = Math.Min(labels[current], labels[next]);
                parents[current] = parents[next];
            }
        }
    }
}
