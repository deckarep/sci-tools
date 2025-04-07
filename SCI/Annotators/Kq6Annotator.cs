using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Kq6Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            VerbAnnotator.Run(Game, verbs);
            InventoryAnnotator.Run(Game, items);
            RunLate();

            Kq6DeathAnnotator.Run(Game, MessageFinder);
            Kq6FlagAnnotator.Run(Game, Selectors);
        }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
            // This game has Acts! 1-5. They're not advertised, but script names confirm this.
            { 153, "gAct" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(913, 0), "IsFlag" },
            { Tuple.Create(913, 1), "SetFlag" },
            { Tuple.Create(913, 2), "ClearFlag" },

            { Tuple.Create(913, 3), "NextAct" },
            { Tuple.Create(913, 4), "Face" },

            { Tuple.Create(0, 1), "EgoDead" },
        };

        static IReadOnlyDictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 1, "Look" },
            { 2, "Talk" },
            { 3, "Walk" },
            { 5, "Do" },

            { 7, "deadMansCoin" },
            { 8, "dagger" },
            { 12, "map" },
            { 13, "mirror" },
            { 14, "potion" },
            { 15, "hair" },
            { 16, "scythe" },
            { 17, "shield" },
            { 18, "cassimaHair" },
            { 19, "egg" },
            { 20, "tinderBox" },
            { 24, "sacredWater" },
            { 25, "holeInTheWall" },
            { 27, "riddleBook" },
            { 28, "spellBook" },
            { 29, "brush" },
            { 30, "feather" },
            { 31, "flute" },
            { 32, "poem" },
            { 33, "ribbon" },
            { 34, "tomato" },
            { 35, "skeletonKey" },
            { 37, "nightingale" },
            { 39, "brick" },
            { 40, "coin" },
            { 42, "boringBook" },
            { 43, "huntersLamp" },
            { 44, "teaCup" },
            { 45, "clothes" },
            { 46, "coal" },
            { 47, "flower" },
            { 48, "gauntlet" },
            { 49, "ticket" },
            { 50, "handkerchief" },
            { 51, "skull" },
            { 52, "lettuce" },
            { 53, "lettuce (melting)" },
            { 54, "lettuce (melted)" },

            { 96, "fakeLamp1" },
            { 60, "fakeLamp2" },
            { 59, "fakeLamp3" },
            { 58, "fakeLamp4" },
            { 56, "fakeLamp5" }, // looks like the real lamp
            { 57, "fakeLamp6" },
            { 92, "realLamp" },  // real lamp that jollo gives you

            { 61, "letter" },
            { 62, "milk" },
            { 63, "mint" },
            { 64, "nail" },
            { 65, "note" },
            { 66, "pearl" },
            { 67, "peppermint" },
            { 68, "rabbitFoot" },
            { 69, "ring" },
            { 70, "royalRing" },
            { 71, "rose" },
            { 72, "scarf" },
            { 83, "ink" },
            { 85, "sentence" },
            { 94, "participle" },
        };

        static string[] items =
        {
            "map",
            "boringBook",
            "brick",
            "brush",
            "hair",
            "clothes",
            "coal",
            "deadMansCoin",
            "dagger",
            "coin",
            "egg",
            "skull",
            "feather",
            "flower",
            "flute",
            "gauntlet",
            "cassimaHair",
            "handkerchief",
            "holeInTheWall",
            "huntersLamp",
            "letter",
            "lettuce",
            "milk",
            "mint",
            "mirror",
            "newLamp",
            "nail",
            "nightingale",
            "ticket",
            "participle",
            "pearl",
            "peppermint",
            "note",
            "potion",
            "rabbitFoot",
            "ribbon",
            "riddleBook",
            "ring",
            "rose",
            "royalRing",
            "sacredWater",
            "scarf",
            "scythe",
            "shield",
            "skeletonKey",
            "spellBook",
            "teaCup",
            "poem",
            "tinderBox",
            "tomato",
            "sentence",
            "ink",
        };
    }
}
