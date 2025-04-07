using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // gEgo get: 3
    // gEgo use: 3
    // gEgo put: 3
    // different methods in different games.
    // fpfp & dagger can use -1 as its first param in which case the second one is the item.
    // qfg4 passes second param for quantity after item number

    static class InventoryAnnotator
    {
        static string[] selectors =
        {
            "get:",
            "use:",
            "put:",
            "drop:",
            "has:",
        };

        public static void Run(Game game, string[] items)
        {
            if (!items.Any()) return;

            var ego = game.GetGlobal(0).Name;
            var inventory = game.GetGlobal(9).Name;

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if ((node.Parent.At(0).Text == ego ||
                     node.Parent.At(0).Text == "client" || // sq6 uses client, this might be bad
                     node.Parent.At(0).Text == "self" ||   // qfg4 uses self, this might be bad
                     (node.Parent.At(0).At(0).Text == "ScriptID" && // pepper uses 895/0 and 895/1 for pepper and lockjaw, this is definitely bad
                      node.Parent.At(0).At(1).Text == "895" ||
                      node.Parent.At(0).At(1).Text == "64037")) && // lsl7 uses 64037/0, this is really bad
                    selectors.Contains(node.Text) &&
                    node.Next() is Integer)
                {
                    // skip -1 if it's the first parameter
                    var itemNode = node.Next();
                    if (itemNode.Number == -1)
                    {
                        itemNode = itemNode.Next();
                    }

                    if (itemNode is Integer)
                    {
                        int itemIndex = itemNode.Number;
                        if (0 <= itemIndex && itemIndex < items.Length)
                        {
                            itemNode.Annotate(items[itemIndex]);
                        }
                    }
                }

                // (gInventory ... at: itemIndex
                else if ((node.Parent.At(0).Text == inventory ||
                          node.Parent.At(0).Text == "Inv") &&
                         node.Text == "at:" &&
                         node.Next() is Integer)
                {
                    var itemNode = node.Next();
                    var itemIndex = itemNode.Number;
                    if (0 <= itemIndex && itemIndex < items.Length)
                    {
                        itemNode.Annotate(items[itemIndex]);
                    }
                }
            }
        }
    }
}
