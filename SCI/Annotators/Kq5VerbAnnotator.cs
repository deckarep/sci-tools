using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // KQ5's verbs are primitive (it was their first try!) so it needs its own verb annotator.
    //
    // Verbs are handled in handleEvent methods. The verb id is pEvent:message.
    // SCI Companion doesn't know this and annotated all of these integers as joystick enums.
    // To add to the confusion, the enum values changed between english floppy versions
    // and later versions.
    //
    // Inventory is handled by first testing the message for Inventory, and then testing
    // the currently selected inventory cursor. In early versions (english floppy) this
    // is global69 and in later versions it's (gInventory indexOf: (gIconBar curInvIcon?).
    // I'm annotating all numeric comparisons against either

    static class Kq5VerbAnnotator
    {
        // english floppy
        static IReadOnlyDictionary<int, string> earlyVerbs = new Dictionary<int, string>
        {
            { 2, "Look" },
            { 3, "Do" },
            { 4, "Talk" },
            { 5, "Inventory" },
        };

        // CD, french floppy (i'm assuming all non-english)
        static IReadOnlyDictionary<int, string> lateVerbs = new Dictionary<int, string>
        {
            { 2, "Look" },
            { 3, "Do" },
            { 4, "Inventory" },
            { 5, "Talk" },
        };

        public static void Run(Game game, bool isEarly, IReadOnlyDictionary<int, string> inventoryVerbs)
        {
            var verbs = isEarly ? earlyVerbs : lateVerbs;

            string global9 = game.GetGlobal(9).Name; // gInventory
            string global69 = game.GetGlobal(69).Name; // gIconBar in later games

            var methods = from s in game.Scripts
                          from o in s.Objects
                          from m in o.Methods
                          where m.Name == "handleEvent" &&
                                s.Number < 900 && // don't touch system scripts
                                s.Number != 255   // don't touch system scripts
                          select m;
            foreach (var method in methods)
            {
                // 1. My EventAnnotator knows to not touch KQ5 Event:message values,
                //    so I need to detect comparisons and annotate the numbers.
                ConstantFinder.Run(method.Node,
                    n => IsEventMessage(n),
                    n =>
                    {
                        int verbId = n.Number;
                        if (verbs.ContainsKey(verbId)) // ignore unknown verbs, those are real messages
                        {
                            n.Annotate(verbs[verbId]);
                        }
                    });

                // 2. annotate inventory comparisons
                // early: (== global69 inventoryIndex)
                // late:  (== (global69 indexOf: (global9 curInvIcon?) inventoryIndex)
                ConstantFinder.Run(method.Node,
                    n => IsInventoryNode(n, global9, global69),
                    n => VerbAnnotator.Annotate(n, inventoryVerbs));
            }
        }

        static bool IsEventMessage(Node node)
        {
            return node.Children.Count >= 2 && node.Last().Text == "message:";
        }

        // early: global69
        // late:  (global69 indexOf: (global9 curInvIcon?)
        static bool IsInventoryNode(Node node, string global9, string global69)
        {
            if (node.Text == global69) return true;

            if (node.At(0).Text == global9 &&
                node.At(1).Text == "indexOf:" &&
                node.At(2).At(0).Text == global69 &&
                node.At(2).At(1).Text == "curInvIcon?")
            {
                return true;
            }

            return false;
        }
    }
}
