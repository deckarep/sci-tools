using System;
using System.Collections.Generic;
using System.Linq;

namespace SCI.Annotators
{
    class Kq5Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            InventoryAnnotator.Run(Game, items);
            RunLate();

            // is early: < 30 exports in script 0
            bool isEarly = (Game.GetExport(0, 30) == null);

            // KQ5 does verbs differently than others
            Kq5VerbAnnotator.Run(Game, isEarly, ArrayToDictionary(0, items));

            ExportRenamer.Run(Game, isEarly ? earlyExports : lateExports);

            // AudioScript is CD only but Mac has an unused copy laying around so test for an instance
            bool isCD = Game.Scripts.Any(s => s.Instances.Any(i => i.Super == "AudioScript"));
            if (!isCD)
            {
                Kq5FloppyMessageAnnotator.Run(Game, isEarly, TextMessageFinder);
            }
        }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "NormalEgo" },
            { Tuple.Create(0, 2), "HandsOff" },
            { Tuple.Create(0, 3), "HandsOn" },
            { Tuple.Create(0, 4), "HaveMem" },
            { Tuple.Create(0, 5), "RedrawCast" },
            { Tuple.Create(0, 6), "cls" },

            // cd passes audio ids, floppy passes text ids.
            // they aren't the same function, CD has the
            // old floppy one laying around but it doesn't
            // get called, but they just so happen to end
            // up with export 29 in both versions.
            { Tuple.Create(0, 29), "Say" },

            // Print function that handles drop caps, as used in most messages.
            // Originally this was in interface system script 255, export 6, but
            // was then moved to script 759 for foreign language versions.
            // OriginalSymbolRenamer handles script 255.
            // Kq5FloppyMessageAnnotator depends on this name (that I made up).
            { Tuple.Create(759, 0), "PrintDC" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> earlyExports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 10), "SetFlag" },
            { Tuple.Create(0, 11), "ClearFlag" },
            { Tuple.Create(0, 13), "IsFlag" },

            { Tuple.Create(0, 27), "EgoDead" },
            { Tuple.Create(0, 28), "SetScore" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> lateExports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 9), "SetFlag" },
            { Tuple.Create(0, 10), "ClearFlag" },
            { Tuple.Create(0, 12), "IsFlag" },

            { Tuple.Create(0, 26), "EgoDead" },
            { Tuple.Create(0, 27), "SetScore" },
        };

        // verbs are handled by Kq5VerbAnnotator

        static string[] items =
        {
            "Ok",
            "Key",
            "Pie",
            "Golden_Needle",
            "Coin",
            "Fish",
            "Brass_Bottle",
            "Staff",
            "Shoe",
            "Heart",
            "Harp",
            "Gold_Coin",
            "Marionette",
            "Pouch",
            "Emeralds",
            "Spinning_Wheel",
            "Stick",
            "Honeycomb",
            "Beeswax",
            "Leg_of_Lamb",
            "Rope",
            "Crystal",
            "Hammer",
            "Shell",
            "Bag_of_Peas",
            "Locket",
            "Cloak",
            "Amulet",
            "Wand",
            "Sled",
            "Iron_Bar",
            "Fishhook",
            "Moldy_Cheese",
            "Elf_Shoes",
            "Tambourine",
            "Mordack_s_Wand",
            "Hairpin",
            "Cat_Fish",
            "Mongoose_Spell",
            "Bunny_Spell",
            "Rain_Spell",
            "Tiger_Spell",
            "invLook",
            "invHand",
            "invSelect",
            "invHelp",
            "ok",
        };
    }
}
