using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // SCI0 games used IsItemAt and PutItem functions in script 0 to interact
    // with inventory items. Annotate both with item names.
    //
    // DEPENDENCY: ExportRenamer must be run first and rename IsItemAt and PutItem
    // methods. the alternative is passing export numbers to this method.

    static class Sci0InventoryAnnotator
    {
        public static void Run(Game game, string[] items)
        {
            if (!items.Any()) return;

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (node.Children.Count >= 2 &&
                    (node.At(0).Text == "IsItemAt" || node.At(0).Text == "PutItem") &&
                    node.At(1) is Integer)
                {
                    int itemNumber = node.At(1).Number;
                    if (0 <= itemNumber && itemNumber < items.Length)
                    {
                        node.At(1).Annotate(items[itemNumber].SanitizeMessageText());
                    }
                }
            }
        }
    }
}
