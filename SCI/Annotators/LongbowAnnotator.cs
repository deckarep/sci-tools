using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class LongbowAnnotator : GameAnnotator
    {
        public override void Run()
        {
            bool demo = (Game.GetExport(0, 0) == "RH");
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, demo ? demoExports : exports);
            VerbAnnotator.Run(Game, verbs, inventoryVerbs);
            InventoryAnnotator.Run(Game, items);
            RunLate();

            LongbowMessageAnnotator.Run(Game, TextMessageFinder);

            GlobalEnumAnnotator.Run(Game, 126, disguiseEnums);
        }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
            { 3, "gMachineSpeedZeroOrSix" },
            { 108, "gEgoEdgeHit" },
            { 112, "gForestRoomNum" },
            { 126, "gDisguiseNum" },
            { 130, "gDay" },
            { 137, "gForestSweepRoomCount" },
            { 138, "gOutlaws" },
            { 139, "gRansom" },
            { 145, "gDeathNum" },

            // this is kind of a time-of-day global that increments when
            // changing rooms
            { 155, "gRoomCount" },

            { 160, "gBlowCount" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 2), "NormalEgo" },
            { Tuple.Create(0, 3), "HandsOff" },
            { Tuple.Create(0, 4), "HandsOn" },
            { Tuple.Create(0, 5), "IsFlag" },
            { Tuple.Create(0, 6), "SetFlag" },
            { Tuple.Create(0, 7), "ClearFlag" },
            { Tuple.Create(0, 9), "Face" },

            { Tuple.Create(13, 4), "Say" },
            { Tuple.Create(13, 5), "SayModeless" },
            { Tuple.Create(13, 6), "SetMessageColor" },

            { Tuple.Create(806, 0), "EgoDead" },
            { Tuple.Create(806, 1), "SetScore" },
            { Tuple.Create(806, 2), "AddToAddToPics" },
            { Tuple.Create(806, 3), "AddToFeatures" },
            { Tuple.Create(806, 4), "AddRansom" },
            { Tuple.Create(806, 5), "AddOutlaws" },
            { Tuple.Create(806, 6), "SetIcon" },

            { Tuple.Create(851, 0), "Converse" },
            { Tuple.Create(851, 1), "AyeOrNay" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> demoExports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "HandsOff" },
            { Tuple.Create(0, 2), "HandsOn" },
            { Tuple.Create(0, 5), "IsFlag" },
            { Tuple.Create(0, 6), "SetFlag" },
            { Tuple.Create(0, 7), "ClearFlag" },
        };

        static IReadOnlyDictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 1, "Walk" },
            { 2, "Look" },
            { 3, "Do" },
            { 4, "Inventory" },
            { 5, "Talk" },
            { 10, "Longbow" },
            { 11, "iconMap" },
        };

        static IReadOnlyDictionary<int, string> inventoryVerbs = new Dictionary<int, string>
        {
            { 0, "bucks" },
            { 1, "horn" },
            { 2, "halfHeart" },
            { 3, "slipper" },
            { 4, "net" },
            { 5, "pipe" },
            { 6, "gems" },
            { 7, "jewels" },
            { 8, "rouge" },
            { 9, "message" },
            { 10, "fireRing" },
            { 11, "cask" },
            { 12, "puzzleBox" },
            { 13, "robes" },
            { 14, "amethyst" },
            { 15, "comb" },
            { 16, "fulkScroll" },
            { 17, "handScroll" },
            { 18, "waterRing" },
            { 19, "invLook" },
            { 20, "invHand" },
            { 21, "invSelect" },
            { 22, "invHelp" },
            { 23, "ok" }
        };

        static string[] items =
        {
            "bucks",
            "horn",
            "halfHeart",
            "slipper",
            "net",
            "pipe",
            "gems",
            "jewels",
            "rouge",
            "message",
            "fireRing",
            "cask",
            "puzzleBox",
            "robes",
            "amethyst",
            "comb",
            "fulkScroll",
            "handScroll",
            "waterRing",
        };

        static IReadOnlyDictionary<int, string> disguiseEnums = new Dictionary<int, string>
        {
            { 0, "outlaw" },
            { 1, "beggar" },
            { 2, "jewler (no rouge)" },
            { 3, "jewler (rouge)" },
            { 4, "yeoman" },
            { 5, "abbey monk" },
            { 6, "fens monk" },
        };
    }
}
