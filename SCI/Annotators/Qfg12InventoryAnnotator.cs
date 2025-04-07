using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // Run after globals are renamed

    static class Qfg12InventoryAnnotator
    {
        static HashSet<string> globalNames = new HashSet<string>
        {
            "gInvNum",
            "gInvDropped",
            "gInvWeight",
        };

        public static void Run(Game game, string[] items)
        {
            foreach (var node in game.GetFunctions().SelectMany(f => f.Node))
            {
                if (node.Children.Count == 2 &&
                    node is Array &&
                    node.At(1) is Integer &&
                    globalNames.Contains(node.At(0).Text))
                {
                    int itemNumber = node.At(1).Number;
                    if (0 <= itemNumber && itemNumber < items.Length)
                    {
                        node.At(1).Annotate(items[itemNumber]);
                    }
                }
            }
        }
    }
}
