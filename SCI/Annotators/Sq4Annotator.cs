using System;
using System.Collections.Generic;
using System.Linq;

namespace SCI.Annotators
{
    // SQ4 is complicated because of differences between floppy versions
    // and severe differences between those and CD version.
    // and there's a demo that's just miss astrochicken, it counts as "new floppy"

    class Sq4Annotator : GameAnnotator
    {
        public override void Run()
        {
            // detect floppy or cd as they are very different
            bool isFloppy = !Game.Scripts.Any(s => s.Classes.Any(c => c.Name == "Messager"));
            bool isBeta = isFloppy && Game.Scripts.Any(s => s.Instances.Any(c => c.Name == "spCard"));
            bool isNewFloppy = isFloppy && (Game.GetScript(932) != null);

            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);

            // verbs and messages are version specific
            if (isFloppy)
            {
                // Floppy
                ExportRenamer.Run(Game, floppyExports);
                if (isBeta)
                {
                    InventoryAnnotator.Run(Game, betaItems);
                    VerbAnnotator.Run(Game, betaFloppyVerbs, ArrayToDictionary(0, betaItems));
                }
                else if (!isNewFloppy)
                {
                    InventoryAnnotator.Run(Game, floppyItems);
                    VerbAnnotator.Run(Game, oldFloppyVerbs, ArrayToDictionary(0, floppyItems));
                }
                else
                {
                    InventoryAnnotator.Run(Game, floppyItems);
                    VerbAnnotator.Run(Game, newFloppyVerbs, ArrayToDictionary(0, floppyItems));
                }

                Sq4DeathAnnotator.Run(Game, TextMessageFinder); // floppy only
            }
            else
            {
                // CD
                InventoryAnnotator.Run(Game, cdItems);
                ExportRenamer.Run(Game, cdExports);
                VerbAnnotator.Run(Game, cdVerbs);
                Sq4CDMessageAnnotator.Run(Game, MessageFinder);
            }

            Sq4MallFlagAnnotator.Run(Game, Selectors);

            RunLate();
        }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
            { 90, "gMessageMode" },

            { 159, "gBuckazoidCount" },
            { 169, "gATMBuckazoidCount" },

            { 199, "gGameSpeed" }, // CD only
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "NormalEgo" },
            { Tuple.Create(0, 2), "HandsOff" },
            { Tuple.Create(0, 3), "HandsOn" },
            { Tuple.Create(0, 4), "HaveMem" },
            { Tuple.Create(0, 5), "StepOn" },
            { Tuple.Create(0, 6), "IsFlag" },
            { Tuple.Create(0, 7), "SetFlag" },
            { Tuple.Create(0, 8), "ClearFlag" },
            { Tuple.Create(0, 9), "AnimateEgoHead" },
            { Tuple.Create(0, 10), "EgoDead" },
            { Tuple.Create(0, 11), "SetScore" },

            { Tuple.Create(0, 13), "Face" },

            { Tuple.Create(700, 3), "TestMallFlag" }, // unused
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> floppyExports = new Dictionary<Tuple<int, int>, string>()
        {
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> cdExports = new Dictionary<Tuple<int, int>, string>()
        {
        };

        static IReadOnlyDictionary<int, string> betaFloppyVerbs = new Dictionary<int, string>
        {
            { 1, "Look" },
            { 4, "Do" },
            { 6, "Inventory" },
            { 5, "Talk" },
            { 2, "Smell" },
            { 3, "Taste" },
        };

        // 1.051, 1.1
        static IReadOnlyDictionary<int, string> oldFloppyVerbs = new Dictionary<int, string>
        {
            { 1, "Look" },
            { 2, "Do" },
            { 3, "Inventory" },
            { 4, "Talk" },
            { 11, "Smell" },
            { 10, "Taste" },
        };

        // anything with script 932. amiga & localized versions
        static IReadOnlyDictionary<int, string> newFloppyVerbs = new Dictionary<int, string>
        {
            { 1, "Walk" },
            { 2, "Look" },
            { 3, "Do" },
            { 4, "Inventory" },
            { 5, "Talk" },
            { 11, "Smell" },
            { 10, "Taste" },
        };

        static IReadOnlyDictionary<int, string> cdVerbs = new Dictionary<int, string>
        {
            { 1, "Look" },
            { 4, "Do" },
            { 2, "Talk" },
            { 6, "Smell" },
            { 7, "Taste" },

            // verb finder couldn't find these, no message property,
            // it's just their inventory order + 8
            { 8, "buckazoid" },
            { 9, "rope" },
            { 10, "bomb" },
            { 11, "rabbit" },
            { 12, "battery" },
            { 13, "jar" },
            { 14, "gum" },
            { 15, "tank" },
            { 16, "hintbook" },
            { 17, "pen" },
            { 18, "atmCard" },
            { 19, "plug" },
            { 20, "cigar" },
            { 21, "matches" },
            { 22, "diskette" },
            { 23, "laptop" },

            { 24, "Inventory" },
        };

        static string[] betaItems =
        {
            "buckazoid",
            "rope",
            "bomb",
            "rabbit",
            "battery",
            "jar",
            "paper_with_gum",
            "oxygen_tank",
            "spCard",
            "hintbook",
            "pen",
            "atmCard",
            "plug",
            "cigar",
            "matches",
            "diskette",
            "laptop_computer",
        };

        static string[] floppyItems =
        {
            "buckazoid",
            "rope",
            "bomb",
            "rabbit",
            "battery",
            "jar",
            "paper_with_gum",
            "oxygen_tank",
            "hintbook",
            "pen",
            "atmCard",
            "plug",
            "cigar",
            "matches",
            "diskette",
            "laptop_computer",
        };

        static string[] cdItems =
        {
            "buckazoid",
            "rope",
            "bomb",
            "rabbit",
            "battery",
            "jar",
            "gum",
            "tank",
            "hintbook",
            "pen",
            "atmCard",
            "plug",
            "cigar",
            "matches",
            "diskette",
            "laptop",
        };
    }
}
