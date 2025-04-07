using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Lsl6Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            VerbAnnotator.Run(Game, verbs);
            InventoryAnnotator.Run(Game, items);
            RunLate();
        }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
            // lo-res: start of the flags globals
            // hi-res: instance of "flags" class
            // either way, wrapped in the usual IsFlag/SetFlag/ClearFlag method interface
            { 137, "gFlags" },

            { 194, "gMasterVolume" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(79, 0), "IsFlag" },
            { Tuple.Create(79, 1), "SetFlag" },
            { Tuple.Create(79, 2), "ClearFlag" },
            { Tuple.Create(79, 3), "Face" },

            { Tuple.Create(82, 0), "EgoDead" },
        };

        static IReadOnlyDictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 1, "Look" },
            { 2, "Talk" },
            { 3, "Walk" },
            { 4, "Do" },
            { 5, "Take" },
            { 6, "Zipper" },

            // dynamic items
            { 17, "lamp [ lubed ]" },
            { 20, "champagne [ chilled ]" },
            { 27, "cord [ stripped ]" },
            { 33, "filter [ clean ]" },
            { 36, "washCloth [ wet ]" },
            { 37, "washCloth [ chilled ]" },
            { 40, "lamp [ filled ]" },
            { 41, "lamp [ lit ]" },
            { 43, "match [ lit ]" },
            { 49, "beaver [ inflated ]" },
            { 52, "randomKey [ filed ]" },
            { 58, "soap [ impressed ]" },

            { 7, "roomkey" },
            { 9, "beer" },
            { 10, "badge" },
            { 11, "belt" },
            { 12, "handcuffs" },
            { 13, "orchid" },
            { 16, "bracelet" },
            { 18, "gown" },
            { 19, "champagne" },
            { 22, "collar" },
            { 23, "diamond" },
            { 24, "flashlight" },
            { 25, "batteries" },
            { 26, "cord" },
            { 28, "sculpture" },
            { 29, "pearl" },
            { 30, "lard" },
            { 32, "filter" },
            { 34, "orange" },
            { 35, "washcloth" },
            { 38, "minwater" },
            { 39, "lamp" },
            { 42, "match" },
            { 44, "glasscase" },
            { 45, "sunglasses" },
            { 46, "polishcloth" },
            { 47, "swimsuit" },
            { 48, "beaver" },
            { 50, "towerkey" },
            { 51, "randomKey" },
            { 53, "wordsOWisdom" },
            { 54, "brochure" },
            { 56, "towel" },
            { 57, "soap" },
            { 59, "handcreme" },
            { 60, "floss" },
            { 61, "toiletcover" },
            { 62, "toiletpaper" },
            { 63, "bastardfile" },
            { 64, "wrench" },
            { 65, "condom" },
            { 66, "flowers" },
        };

        static string[] items =
        {
            "badge",
            "batteries",
            "beaver",
            "beer",
            "belt",
            "bracelet",
            "brochure",
            "champagne",
            "condom",
            "cord",
            "diamond",
            "collar",
            "bastardfile",
            "filter",
            "flashlight",
            "floss",
            "flowers",
            "gown",
            "handcuffs",
            "handcreme",
            "towerkey",
            "randomKey",
            "roomkey",
            "lamp",
            "lard",
            "match",
            "minwater",
            "orange",
            "orchid",
            "pearl",
            "glasscase",
            "sunglasses",
            "polishcloth",
            "sculpture",
            "soap",
            "swimsuit",
            "toiletcover",
            "toiletpaper",
            "towel",
            "washcloth",
            "wordsOWisdom",
            "wrench",
        };
    }
}
