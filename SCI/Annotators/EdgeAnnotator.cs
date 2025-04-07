using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // This could be generalized into a constant annotator that takes property selectors,
    // or contains groups of known ones, instead of just dealing with edges.

    static class EdgeAnnotator
    {
        static IReadOnlyDictionary<int, string> edges = new Dictionary<int, string>
        {
            { 0, "EDGE_NONE" },
            { 1, "EDGE_TOP" },
            { 2, "EDGE_RIGHT" },
            { 3, "EDGE_BOTTOM" },
            { 4, "EDGE_LEFT" },
        };

        public static void Run(Game game, IEnumerable<string> moreEdgeSelectors = null)
        {
            var edgeSelectors = new List<string>
            {
                "edgeHit",
                "prevEdgeHit", // kq6
            };
            if (moreEdgeSelectors != null)
            {
                edgeSelectors.AddRange(moreEdgeSelectors);
            }

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (edgeSelectors.Any(s => node.IsSelector(s)))
                {
                    // edgeHit: edge
                    string edge;
                    if (node.Next() is Integer &&
                        edges.TryGetValue(node.Next().Number, out edge))
                    {
                        (node.Next() as Integer).SetDefineText(edge);
                        continue;
                    }

                    // (== (something edgeHit:) edge)
                    if (node.Next() is Nil &&
                        node.Parent.Parent.At(0).Text == "==" &&
                        node.Parent.Parent.At(2) is Integer &&
                        edges.TryGetValue(node.Parent.Parent.At(2).Number, out edge))
                    {
                        (node.Parent.Parent.At(2) as Integer).SetDefineText(edge);
                        continue;
                    }

                    // (switch (something edgeHit:) (edge ... ) (edge ...)
                    if (node.Next() is Nil &&
                        node.Parent.Parent.At(0).Text == "switch")
                    {
                        var switchGroup = node.Parent.Parent;
                        for (int i = 2; i < switchGroup.Children.Count; ++i)
                        {
                            var edgeNode = switchGroup.At(i).At(0);
                            if (edgeNode is Integer &&
                                edges.TryGetValue(edgeNode.Number, out edge))
                            {
                                (edgeNode as Integer).SetDefineText(edge);
                            }
                        }
                    }
                }
            }
        }
    }
}
