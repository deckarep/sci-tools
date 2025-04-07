using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Phant2Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);

            // handleEvent tests verbs using a global variable
            VerbAnnotator.Run(Game, verbs, null, 208);

            InventoryAnnotator.Run(Game, items);
            RunLate();
        }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
            { 205, "gChapter" },
            { 208, "gVerb" }, // decompiler already doing this, but it's important so force it
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(63002, 0), "IsFlag" },
            { Tuple.Create(63002, 1), "SetFlag" },
            { Tuple.Create(63002, 2), "ClearFlag" },
        };

        static IReadOnlyDictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 1, "buttonI" },
            { 2, "braceletI" },
            { 3, "printoutI" },

            { 4, "Do" },

            { 5, "walletI" },
            { 6, "cardKeyI" },
            { 7, "candyI" },
            { 9, "parentPhotoI" },
            { 10, "xmasPhotoI" },
            { 11, "sexyCardI" },
            { 12, "bondageCardI" },
            { 13, "harburgCardI" },
            { 14, "orderlyKeyI or paulKeyI" },
            { 15, "screwdriverI" },
            { 21, "lockerI" },
            { 23, "hammerI" },
            { 24, "folderI" },
            { 25, "hairpinI" },
            { 27, "paulBookI" },
            { 28, "dadLetterI" },
            { 30, "mailBundleI" },
            { 31, "ratI" },
            { 32, "laceI" },
            { 33, "anagramI" },
            { 34, "thereseNoteI" },
            { 35, "paulSpeechI" },
            { 36, "trevCardKeyI" },
            { 37, "paperclipI" },
            { 38, "extinguisherI" },
            { 39, "alienFoodI" },
            { 40, "alienI" },
            { 41, "slimeI" },
            { 43, "alienKeyI" },
            { 144, "alien2I" },
            { 145, "slime2I" },
            { 146, "electricAlienI" },
            { 150, "alien3I" },
            { 151, "glopI" },
        };

        static string[] items =
        {
            "buttonI",
            "walletI",
            "cardKeyI",
            "candyI",
            "parentPhotoI",
            "xmasPhotoI",
            "sexyCardI",
            "bondageCardI",
            "harburgCardI",
            "orderlyKeyI",
            "screwdriverI",
            "dadLetterI",
            "lockerI",
            "paulKeyI",
            "hammerI",
            "folderI",
            "hairpinI",
            "paulBookI",
            "printoutI",
            "braceletI",
            "mailBundleI",
            "ratI",
            "laceI",
            "anagramI",
            "thereseNoteI",
            "paulSpeechI",
            "trevCardKeyI",
            "paperclipI",
            "extinguisherI",
            "alienFoodI",
            "alienI",
            "slimeI",
            "alien2I",
            "slime2I",
            "alienKeyI",
            "electricAlienI",
            "alien3I",
            "glopI",
        };
    }
}