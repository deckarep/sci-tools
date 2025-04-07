using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Pq3Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            VerbAnnotator.Run(Game, verbs, ArrayToDictionary(0, inventoryVerbs));
            InventoryAnnotator.Run(Game, inventoryVerbs);
            RunLate();

            Pq3MessageAnnotator.Run(Game, TextMessageFinder);
        }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
            { 128, "gDay" }, // one based
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "NormalEgo" },
            { Tuple.Create(0, 2), "HandsOff" },
            { Tuple.Create(0, 3), "HandsOn" },
            { Tuple.Create(0, 4), "HaveMem" },
            { Tuple.Create(0, 5), "EgoDead" },
            { Tuple.Create(0, 6), "SetScore" },

            { Tuple.Create(0, 9), "SetFlag" },
            { Tuple.Create(0, 10), "IsFlag" },
            { Tuple.Create(0, 11), "ClearFlag" },

            { Tuple.Create(0, 14), "Face" },

            { Tuple.Create(0, 16), "Say" },

            { Tuple.Create(0, 18), "ShowClock" },

            { Tuple.Create(26, 0), "EgoDead" },
        };

        static IReadOnlyDictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 1, "Walk" },
            { 2, "Look" },
            { 3, "Do" },
            { 4, "Inventory" },
            { 5, "Talk" },
        };

        static string[] inventoryVerbs =
        {
            "compRequest",
            "gun",
            "handcuff",
            "theKeys",
            "flashlight",
            "musicBox",
            "photo",
            "camera",
            "warrant",
            "judicialOrder",
            "calibration",
            "envelope",
            "toiletPaper",
            "gunKey",
            "battery",
            "flares",
            "nightStick",
            "knife",
            "patMemo",
            "wallet",
            "ticketBook",
            "compCard",
            "tracker",
            "license",
            "ticket",
            "cultBook",
            "miltaryRecord",
            "murderFile",
            "noteBook",
            "necklace",
            "whitePaint",
            "goldPaint",
            "bronzeStar",
            "skinHair",
            "cultRing",
            "article",
            "rose",
            "locket",
            "busCard",
            "scraper",
            "toothpick",
            "bloodHair",
            "sampleEnv",
            "remoteControl",
            /*"invLook",
            "invSelect",
            "invHelp",
            "invOk"*/
        };
    }
}
