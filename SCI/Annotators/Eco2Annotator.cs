using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Eco2Annotator : GameAnnotator
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
            { 150, "gCurrentRegionFlags" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 2), "IsFlag" },
            { Tuple.Create(0, 3), "SetFlag" },
            { Tuple.Create(0, 4), "ClearFlag" },

            { Tuple.Create(0, 11), "SetEcorderFlag" },
            { Tuple.Create(0, 12), "IsEcorderFlag" },
        };

        static IReadOnlyDictionary<int, string> verbs = new Dictionary<int, string>
        {
            // there's a verb 3 when walking around.
            // verb 2 as well, grandpa:doVerb

            { 1, "Look" },
            { 3, "Walk" }, // i guess?
            { 4, "Do" },
            { 2, "Talk" },
            { 53, "Recycle" },

            // items that become other items
            { 5, "rope" }, // bed sheets become this
            { 26, "sap" }, // was a cup
            { 30, "medicine" },
            { 32, "butter" },

            { 7, "buckazoid" },
            { 8, "passport" },
            { 9, "discPile" },
            { 10, "magnifier" },
            { 12, "fruit" },
            { 13, "amulet" },
            { 14, "vine" },
            { 15, "leaf" },
            { 16, "flowers" },
            { 17, "drum" },
            { 18, "bough" },
            { 19, "barkCup" },
            { 20, "pods" },
            { 21, "charm" },
            { 22, "goldDust" },
            { 23, "machete" },
            { 24, "clayCup" },
            { 25, "berries" },
            { 27, "goldenBlossom" },
            { 28, "organizer" },
            { 29, "padlockKey" },
            { 31, "wallet" },
            { 34, "photo" },
            { 35, "slaughter_s_letter" },
            { 36, "fax" },
            { 37, "topSheet" },
            { 38, "bottomSheet" },
            { 39, "birdSeed" },
            { 40, "metalAx" },
            { 41, "racquet" },
            { 42, "tentPass" },
            { 43, "truthStone" },
            { 45, "whistle" },
            { 46, "golden_feather" },
            { 47, "sharpRock" },
            { 49, "diadem" },
            { 50, "pipes" },
            { 51, "seedling" },
            { 52, "Princess__Mask" },
            { 55, "E-corder" },
            { 56, "mazeNecklace" },
            { 57, "goldDisc" },
            { 58, "punkPass" },
            { 59, "freeTailPass" },
            { 60, "fishPass" },
            { 61, "fruitPass" },
            { 62, "adamBatPass" },
            { 63, "roots" },
            { 64, "chalice" },
            { 65, "rainforest_fruit" },
            { 66, "password" },
            { 68, "dustBuster" },
            { 71, "suspenders" },
            { 72, "sweetDrink" },
        };

        static string[] items =
        {
            // this game does the thing where inventory item constants
            // are re-used in different areas. those four are constant,
            // and i don't care enough to do the rest at the moment.

            "buckazoid",
            "passport",
            "E-corder",
            "amulet",
        };
    }
}
