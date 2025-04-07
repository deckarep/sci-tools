using System;
using System.Collections.Generic;
using SCI.Annotators.Original;

namespace SCI.Annotators
{
    class Lsl3Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            InventoryAnnotator.Run(Game, items);
            FlagAnnotator.Run(Game, flags);
            RunLate();
        }

        static IReadOnlyCollection<PrintTextDef> printTextFunctions = new[]
        {
            new PrintTextDef("PrintL"),
            new PrintTextDef("PrintP"),
        };
        protected override IReadOnlyCollection<PrintTextDef> PrintTextFunctions { get { return printTextFunctions; } }

        protected override Dictionary<int, Original.Script[]> GameHeaders { get { return Lsl3Symbols.Headers; } }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
            { 100, "gDebugging" },
            { 101, "gEgoState" },
            { 102, "gNormalEgoView" },
            { 103, "gGameSeconds" },
            { 104, "gGameMinutes" },
            { 105, "gGameHours" },
            { 106, "gOldTime" },
            { 107, "gSecondsInRoom" },
            { 108, "gBgMusicLoops" },
            { 109, "gRgTimer" },
            { 110, "gCurTimer" },
            { 111, "gFlagArray" },
            { 112, "gCreditsFinished" },
            { 117, "gLastFlag" },
            { 118, "gEgoName" },
            { 119, "gEgoNameBuffer" },
            { 122, "gNearPerson" },
            { 123, "gMachineSpeed" },
            { 124, "gFilthLevel" },
            { 125, "gKeyDownHandler" },
            { 126, "gMouseDownHandler" },
            { 127, "gDirectionHandler" },
            { 128, "gBambooCount" },
            { 129, "gScoreDisplayed" },
            { 130, "gDollars" },
            { 131, "gMusic" },
            { 132, "gBeachState" },
            { 133, "gDemo" },
            { 134, "gNewspaperState" },
            { 135, "gLawyerState" },
            { 136, "gCurVendor" },
            { 137, "gOldGameSpeed" },
            { 138, "gPrevRoomX" },
            { 139, "gPrevRoomY" },
            { 140, "gShowroomState" },
            { 141, "gTextColor" },
            { 142, "gBackgroundColor" },
            { 143, "gSomeObject" },
            { 144, "gEgoIsPatti" },
            { 145, "gSoundFX" },
            { 146, "gStringDelay" },
            { 147, "gLockerCombination1" },
            { 148, "gLockerCombination2" },
            { 149, "gLockerCombination3" },
            { 150, "gBenchPressCount" },
            { 151, "gLegCurlsCount" },
            { 152, "gPullupsCount" },
            { 153, "gBarPullCount" },
            { 154, "gEgoIsHunk" },
            { 155, "gPrevLoop" },
            { 156, "gOldEgoState" },
            { 157, "gNextVar" },
            { 170, "gFilthLevelBuffer" },
            { 200, "gExpletiveBuffer" },
            { 219, "gExpletive" },
            { 220, "gLaffer" },
            { 221, "gAutoSaveMinutes" },
            { 222, "gAsMinutes" },
            { 223, "gAsSeconds" },
            { 224, "gDateBuffer" },
            { 229, "gInitialsBuffer" },
            { 232, "gNoteCounter" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
        };

        static string[] items =
        {
            "Nothing",
            "Credit_Card",
            "Ginsu_Knife",
            "Granadilla_Wood",
            "Native_Grass",
            "Soap-On-A-Rope",
            "A_Twenty_Dollar_Bill",
            "Land_Deed",
            "Beach_Towel",
            "Spa_Keycard",
            "Divorce_Decree",
            "some_Orchids",
            "Penthouse_Key",
            "Bottle_of_Wine",
            "Panties",
            "Pantyhose",
            "Bra",
            "Dress",
            "Magic_Marker",
            "Coconuts",
            "Marijuana",
        };

        static Dictionary<int, string> flags = new Dictionary<int, string>
        {
            { 0, "logging" },
            { 1, "loadDebugNext" },
            { 2, "showFrag" },
            { 3, "cantSave" },
            { 4, "preventAutoSave" },
            { 5, "noCursor" },
            { 6, "drankRiverWater" },
            { 7, "drankSinkWater" },
            { 8, "needsShower" },
            { 9, "seenPatti" },
            { 10, "needsSoap" },
            { 11, "scoredTan" },
            { 12, "lookedInMirror" },
            { 13, "sawAl&BillPoof" },
            { 14, "inQA" },
            { 15, "forceAtest" },
            { 16, "beenIn206" },
            { 17, "beenIn200" },
            { 18, "beenIn203" },
            { 19, "beenIn210" },
            { 20, "beenIn216" },
            { 21, "beenIn220" },
            { 22, "beenIn250" },
            { 23, "beenIn350" },
            { 24, "sawCredits200" },
            { 25, "sawCredits210" },
            { 26, "sawCredits213" },
            { 27, "sawCredits216" },
            { 28, "sawCredits220" },
            { 29, "sawCredits250" },
            { 30, "sawCredits253" },
            { 31, "sawCredits260" },
            { 32, "beenIn266" },
            { 33, "beenIn360" },
            { 34, "beenIn395" },
            { 35, "beenIn440" },
            { 36, "beenIn330" },
            { 37, "beenIn510" },
            { 38, "beenIn323" },
            { 39, "scoredTowel" },
            { 40, "scoredLocker" },
            { 41, "scoredSweats" },
            { 42, "scoredWater" },
            { 43, "scoredDuckPoints" },
            { 44, "scoredCombination" },
            { 45, "scoredSuzi" },
            { 46, "passedSRcopyCheck" },
            { 47, "tippedMaitreD" },
            { 48, "seenCherri" },
            { 49, "showerRunning" },
            { 50, "wetBody" },
            { 51, "lockerRippedOff" },
            { 52, "hadBambi" },
            { 53, "missedBambi" },
            { 54, "scoredOrchids" },
            { 55, "madeLei" },
            { 56, "seenDale" },
            { 57, "scoredDale" },
            { 58, "pickedPot" },
            { 59, "woreGrassSkirt" },
            { 60, "sprayedDeodorant" },
            { 61, "saidHiToTawni" },
            { 62, "needsDeodorant" },
            { 63, "usedElevator" },
            { 64, "seenJodiStrip" },
            { 65, "gaveHead" },
            { 66, "beenIn480" },
            { 67, "tipsIn450" },
            { 68, "killedPorky" },
            { 69, "tookShortcut" },
            { 70, "missedKeycardPoints" },
            { 71, "seenBambi" },
            { 72, "pantyhoseOff" },
            { 73, "braLess" },
            { 74, "braLoaded" },
            { 75, "scoredBraLess" },
            { 76, "scoredBraLoaded" },
            { 77, "skippedLogRide" },
            { 78, "nextFlag" },
        };
    }
}
