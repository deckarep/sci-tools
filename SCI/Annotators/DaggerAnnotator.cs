using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class DaggerAnnotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            VerbAnnotator.Run(Game, verbs);
            InventoryAnnotator.Run(Game, items);
            RunLate();

            DaggerClueAnnotator.Run(Game, MessageFinder);
            DaggerTimeAnnotator.Run(Game);
            DaggerDeathAnnotator.Run(Game, MessageFinder);
        }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
            {  102, "gGameMusic1" }, // gets called gWrapMusic
            {  103, "gGameMusic2" }, // make sure floppy gets it too

            {  123, "gAct" },
            {  124, "gMustDos" },
            {  125, "gClockTimeMustDos" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "StepOn" },
            { Tuple.Create(0, 2), "IsFlag" },
            { Tuple.Create(0, 3), "SetFlag" },
            { Tuple.Create(0, 4), "ClearFlag" },
            { Tuple.Create(0, 5), "Face" },

            { Tuple.Create(0, 10), "TimeCheck" },
            { Tuple.Create(0, 11), "Localize" },
        };

        static Dictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 1, "Look" },
            { 2, "Talk" },
            { 3, "Walk" },
            { 4, "Do" },
            { 6, "Ask" },
            { 13, "Exit Icon" },

            { 5, "claimTicket" },
            { 7, "baseball" },
            { 8, "magnifier" },
            { 9, "mummy" },
            { 10, "coupon" },
            { 11, "pressPass" },
            { 14, "notebook" },
            { 15, "sandwich" },
            { 16, "deskKey" },
            { 17, "pocketWatch" },
            { 18, "skeletonKey" },
            { 19, "meat" },
            { 21, "wireCutters" },
            { 22, "daggerOfRa" },
            { 23, "workBoot" },
            { 24, "smellingSalts" },
            { 25, "snakeOil" },
            { 26, "lantern" },
            { 27, "cheese" },
            { 28, "garter" },
            { 29, "dinoBone" },
            { 30, "snakeLasso" },
            { 31, "ankhMedallion" },
            { 32, "pippin_sPad" },
            { 33, "lightBulb" },
            { 34, "watney_sFile" },
            { 35, "warthogHairs" },
            { 36, "bifocals" },
            { 37, "redHair" },
            { 38, "waterGlass" },
            { 39, "carbonPaper" },
            { 40, "yvette_sShoe" },
            { 41, "grapes" },
            { 42, "eveningGown" },
            { 43, "charcoal" },
            { 44, "wire" },
        };

        static string[] items =
        {
            "coupon",
            "claimTicket",
            "notebook",
            "sandwich",
            "baseball",
            "deskKey",
            "pressPass",
            "pocketWatch",
            "skeletonKey",
            "meat",
            "wireCutters",
            "daggerOfRa",
            "workBoot",
            "smellingSalts",
            "snakeOil",
            "lantern",
            "cheese",
            "garter",
            "dinoBone",
            "snakeLasso",
            "ankhMedallion",
            "pippin_sPad",
            "magnifier",
            "lightBulb",
            "watney_sFile",
            "warthogHairs",
            "bifocals",
            "redHair",
            "waterGlass",
            "carbonPaper",
            "yvette_sShoe",
            "grapes",
            "eveningGown",
            "charcoal",
            "wire",
            "mummy",
        };
    }
}
