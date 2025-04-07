using System;
using System.Linq;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Eco1Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            bool isFloppy = !Game.Scripts.Any(s => s.Classes.Any(c => c.Name == "EcoNarrator"));
            bool isSci11 = Game.Scripts.Any(s => s.Number == 142); // askRiddles script in floppy 1.1, CD
            if (isFloppy)
            {
                // Floppy
                ExportRenamer.Run(Game, floppyExports);
                VerbAnnotator.Run(Game, floppyVerbs, ArrayToDictionary(0, floppyItems));
                InventoryAnnotator.Run(Game, floppyItems);
            }
            else
            {
                // CD
                ExportRenamer.Run(Game, cdExports);
                VerbAnnotator.Run(Game, cdVerbs, cdInventoryVerbs);
                InventoryAnnotator.Run(Game, cdItems);
            }
            RunLate();

            if (!isSci11)
            {
                // floppy 1.0 uses a primitive message format and scripts
                Eco1FloppyMessageAnnotator.Run(Game, MessageFinder);
            }
            else
            {
                // floppy 1.1 and CD use (almost) the same format and scripts
                Eco1CDMessageAnnotator.Run(Game, isFloppy, MessageFinder);
            }
        }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "HandsOff" },
            { Tuple.Create(0, 2), "HandsOn" },
            { Tuple.Create(0, 3), "InitIconBar" },
            { Tuple.Create(0, 4), "SetScore" },

            { Tuple.Create(819, 3), "SetFlag" },
            { Tuple.Create(819, 4), "ClearFlag" },
            { Tuple.Create(819, 5), "IsFlag" },
            { Tuple.Create(819, 6), "NormalEgo" },
            { Tuple.Create(819, 7), "NormalDelph" },
            { Tuple.Create(819, 9), "Face" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> floppyExports = new Dictionary<Tuple<int, int>, string>()
        {
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> cdExports = new Dictionary<Tuple<int, int>, string>()
        {
        };

        static IReadOnlyDictionary<int, string> floppyVerbs = new Dictionary<int, string>
        {
            { 1, "Walk" },
            { 2, "Look" },
            { 3, "Do" },
            { 5, "Talk" },
            { 10, "Recycle" },
            { 4, "Inventory" },
        };

        static IReadOnlyDictionary<int, string> cdVerbs = new Dictionary<int, string>
        {
            { 3, "Walk" },
            { 1, "Look" },
            { 4, "Do" },
            { 2, "Talk" },
            { 6, "Recycle" },
            { 44, "Inventory" },
        };

        static IReadOnlyDictionary<int, string> cdInventoryVerbs = new Dictionary<int, string>
        {
            { 9, "bikeCage" },
            { 10, "starFish" },
            { 11, "goldMask" },
            { 12, "healingPotion" },
            { 13, "screws" },
            { 14, "waterPump" },
            { 15, "tweezers" },
            { 16, "urchins" },
            { 17, "sharpShell" },
            { 18, "conchShell" },
            { 19, "sodaCan" },
            { 20, "certificate" },
            { 21, "card" },
            { 22, "beaker" },
            { 23, "waterBottle" },
            { 24, "frisbee" },
            { 25, "mackeral" },
            { 26, "scubaGear" },
            { 27, "dishSoap" },
            { 28, "airTanks" },
            { 29, "trident" },
            { 30, "spine" },
            { 31, "hermetShell" },
            { 32, "mirror" },
            { 33, "boxKey" },
            { 34, "scroll" },
            { 35, "sawFishHead" },
            { 36, "fishLure" },
            { 37, "hackSaw" },
            { 38, "transmitter" },
            { 39, "float" },
            { 40, "steelCable" },
            { 41, "jar" },
            { 42, "rag" },
            { 43, "sixPackRing" },
        };

        static string[] floppyItems =
        {
            "sodaCan",
            "certificate",
            "card",
            "beaker",
            "rag",
            "dishSoap",
            "frisbee",
            "mackeral",
            "scubaGear",
            "airTanks",
            "bikeCage",
            "conchShell",
            "trident",
            "sharpShell",
            "tweezers",
            "urchins",
            "waterPump",
            "spine",
            "screws",
            "goldMask",
            "starFish",
            "healingPotion",
            "hermetShell",
            "jar",
            "mirror",
            "boxKey",
            "steelCable",
            "float",
            "sawFishHead",
            "fishLure",
            "hackSaw",
            "sixPackRing",
            "transmitter",
            "scroll",
            "waterBottle",
        };

        static string[] cdItems =
        {
            "bikeCage",
            "starFish",
            "goldMask",
            "healingPotion",
            "screws",
            "waterPump",
            "tweezers",
            "urchins",
            "sharpShell",
            "conchShell",
            "sodaCan",
            "certificate",
            "card",
            "beaker",
            "waterBottle",
            "frisbee",
            "mackeral",
            "scubaGear",
            "dishSoap",
            "airTanks",
            "trident",
            "spine",
            "hermetShell",
            "mirror",
            "boxKey",
            "scroll",
            "sawFishHead",
            "fishLure",
            "hackSaw",
            "transmitter",
            "float",
            "steelCable",
            "jar",
            "rag",
            "sixPackRing",
        };
    }
}
