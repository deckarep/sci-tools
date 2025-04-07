using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class CamelotAnnotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            InventoryAnnotator.Run(Game, items);
            Sci0InventoryAnnotator.Run(Game, items);
            RunLate();

            CamelotMessageAnnotator.Run(Game, TextMessageFinder);
        }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
            { 120, "gCopper" },
            { 121, "gSilver" },
            { 122, "gGold" },	
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "RedrawCast" },
            { Tuple.Create(0, 2), "clr" },
            { Tuple.Create(0, 3), "HandsOff" },
            { Tuple.Create(0, 4), "HandsOn" },
            { Tuple.Create(0, 5), "IsItemAt" },
            { Tuple.Create(0, 6), "PutItem" },
            { Tuple.Create(0, 7), "SetFlag" },
            { Tuple.Create(0, 8), "ClearFlag" },
            { Tuple.Create(0, 9), "IsFlag" },
            { Tuple.Create(0, 10), "SetScore" },
            { Tuple.Create(0, 11), "NotClose" },
            { Tuple.Create(0, 12), "DontHave" },

            { Tuple.Create(0, 14), "Talk" },
            { Tuple.Create(0, 15), "OnButton" },
            { Tuple.Create(0, 16), "MouseClaimed" },
            { Tuple.Create(0, 17), "Face" },

            { Tuple.Create(117, 0), "DoPurse" },

            { Tuple.Create(128, 0), "EgoDead" },
        };

        // some item numbers are re-used in different areas
        static string[] items =
        {
            "sword",
            "shield",
            "lodestone",
            "purse",
            "rose | apple | green_apple",
            "sleeve | elixir",
            "iron_key | broom | grail",
            "crystal_heart | charcoal | helm",
            "boar_spear | grain | medallion",
            "herbs | bone",
            "lamb | dove",
            "mirror | golden_apple",
            "relic",
            "veil",
            "felafel",
        };
    }
}
