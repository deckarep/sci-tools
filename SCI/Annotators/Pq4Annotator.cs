using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Pq4Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            VerbAnnotator.Run(Game, verbs);
            InventoryAnnotator.Run(Game, items);
            RunLate();

            Pq4DeathAnnotator.Run(Game, MessageFinder);
        }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
            { 120, "gDay" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 3), "Face" },
            { Tuple.Create(30, 0), "EgoDead" },
        };

        static Dictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 1, "Look" },
            { 2, "Talk" },
            { 4, "Do" },

            { 8, "hypoBaggie" },
            { 10, "ammoBaggie" },
            { 11, "washStuff" },
            { 12, "evidenceCase" },
            { 13, "caseBaggies" },
            { 14, "hickmanStuff" },
            { 15, "vest" },
            { 16, "stick" },
            { 17, "glue" },
            { 18, "mirror" },
            { 19, "apple" },
            { 20, "wallet" },
            { 22, "handgun" },
            { 23, "shotgun" },
            { 24, "fullClip" },
            { 25, "emptyClip" },
            { 26, "shells" },
            { 27, "badge" },
            { 28, "newspaper" },
            { 29, "coins" },
            { 30, "cigarette" },
            { 31, "shoe" },
            { 32, "pretzels" },
            { 33, "folders" },
            { 34, "boneBaggie" },
            { 36, "tape" },
            { 38, "caseChalk" },
            { 39, "pills" },
            { 40, "ball" },
            { 41, "lighter" },
            { 42, "hairspray" },
            { 43, "caseFlashlight" },
            { 44, "rope" },
            { 45, "matches" },
            { 46, "keys" },
            { 47, "handcuffs" },
            { 48, "torch" },
            { 49, "stickMirror" },
            { 52, "notebook" },
            { 54, "ammoBox" },
            { 55, "protectors" },
            { 57, "photo" },
            { 61, "casePuttyKnife" },
            { 62, "beer" },
            { 63, "casePryBar" },
            { 64, "caseGloves" },
            { 65, "caseGlassJar" },
            { 66, "parkerID" },
            { 67, "actionReports" },
            { 68, "funeralMemo" },
            { 69, "greenTicket" },
            { 70, "eyeProtector" },
            { 71, "qualifyMemo" },
            { 72, "manilaTicket" },
            { 73, "crimeSceneReport" },
            { 74, "candyBar" },
            { 79, "sodaCan" },
            { 80, "skeletonKey" },
        };

        static string[] items =
        {
            "funeralMemo",
            "ammoBaggie",
            "greenTicket",
            "evidenceCase",
            "hickmanStuff",
            "vest",
            "stick",
            "glue",
            "mirror",
            "apple",
            "wallet",
            "handgun",
            "shotgun",
            "fullClip",
            "emptyClip",
            "shells",
            "badge",
            "newspaper",
            "coins",
            "cigarette",
            "shoe",
            "pretzels",
            "folders",
            "boneBaggie",
            "eyeProtector",
            "tape",
            "pills",
            "ball",
            "lighter",
            "hairspray",
            "rope",
            "matches",
            "keys",
            "handcuffs",
            "torch",
            "stickMirror",
            "notebook",
            "protectors",
            "ammoBox",
            "hypoBaggie",
            "washStuff",
            "photo",
            "beer",
            "parkerID",
            "actionReports",
            "qualifyMemo",
            "manilaTicket",
            "crimeSceneReport",
            "candyBar",
            "sodaCan",
            "skeletonKey",
        };
    }
}
