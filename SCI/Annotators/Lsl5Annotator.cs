using System;
using System.Collections.Generic;
using SCI.Annotators.Original;

namespace SCI.Annotators
{
    class Lsl5Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            VerbAnnotator.Run(Game, verbs, ArrayToDictionary(0, items));
            InventoryAnnotator.Run(Game, items);
            FlagAnnotator.Run(Game, flags);
            RunLate();

            PrintSelectorAnnotator.Run(Game, Selectors,
                new[] { new PrintTextDef("Say", 1), new PrintTextDef("TPrint") });
        }

        protected override Dictionary<int, Original.Script[]> GameHeaders { get { return Lsl5Symbols.Headers; } }

        static IReadOnlyCollection<PrintTextDef> printTextFunctions = new[]
        {
            new PrintTextDef(0, 14), // TPrint
            new PrintTextDef(0, 18, 1), // Say
        };
        protected override IReadOnlyCollection<PrintTextDef> PrintTextFunctions { get { return printTextFunctions; } }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
            { 100, "gSGrooper" },
            { 101, "gGameCode" },
            { 102, "gTheMusic" },
            { 103, "gTheMusic2" },
            { 104, "gCursorType" },
            { 105, "gNumColors" },
            { 106, "gNumVoices" },
            { 107, "gRestartRoom" },
            { 108, "gSkateAbility" },
            { 109, "gSaveCursorX" },
            { 110, "gSaveCursorY" },
            { 111, "gDebugging" },
            { 112, "gVersionIntPhone" },
            { 113, "gTapesDegaussed" },
            { 122, "gColBlack" },
            { 123, "gColGray1" },
            { 124, "gColGray2" },
            { 125, "gColGray3" },
            { 126, "gColGray4" },
            { 127, "gColGray5" },
            { 128, "gColWhite" },
            { 129, "gColDRed" },
            { 130, "gColLRed" },
            { 131, "gColVLRed" },
            { 132, "gColDYellow" },
            { 133, "gColYellow" },
            { 134, "gColLYellow" },
            { 135, "gColVDGreen" },
            { 136, "gColDGreen" },
            { 137, "gColLGreen" },
            { 138, "gColVLGreen" },
            { 139, "gColDBlue" },
            { 140, "gColBlue" },
            { 141, "gColLBlue" },
            { 142, "gColVLBlue" },
            { 143, "gColMagenta" },
            { 144, "gColLMagenta" },
            { 145, "gColCyan" },
            { 146, "gColLCyan" },
            { 147, "gColWindow" },
            { 150, "gCamcorderCharge" },
            { 151, "gFfRoom" },
            { 152, "gLarryDollars" },
            { 153, "gSilvDollars" },
            { 154, "gTPed" },
            { 155, "gTextSpeed" },
            { 156, "gREMState" },
            { 157, "gPattiDest" },
            { 158, "gFfCueObj" },
            { 159, "gBoardWalkDist" },
            { 160, "gLarryLoc" },
            { 161, "gPattiLoc" },
            { 162, "gPattiDream" },
            { 163, "gPokerJackpot" },
            { 164, "gBlondeX" },
            { 165, "gBlondeLoop" },
            { 166, "gRedHeadX" },
            { 167, "gRedHeadLoop" },
            { 168, "gQuarters" },
            { 169, "gIconSettings" },
            { 170, "gTheTimer" },
            { 171, "gRoomBNumber" },
            { 172, "gMeanWhiles" },
            { 173, "gMonoFont" },
            { 174, "gGiantFont" },
            { 175, "gNiceFont" },
            { 176, "gDestination" },
            { 177, "gAirplaneSeat" },
            { 178, "gDentistState" },
            { 179, "gEgoIsLarry" },
            { 180, "gTalkersOnScreen" },
            { 181, "gVersionDate" },
            { 182, "gVersionPhone" },
            { 183, "gChargeTimer" },
            { 184, "gSaveCharge" },
            { 185, "gTheCurIcon" },
            { 186, "gGameFlags" },
            { 190, "gPointFlags" },
            { 198, "gEndGameFlags" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
        };

        static IReadOnlyDictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 1, "Walk" },
            { 2, "Look" },
            { 3, "Do" },
            { 4, "Inventory" },
            { 5, "Talk" },
            { 10, "Zipper" },
        };

        static string[] items =
        {
            "Camcorder",
            "Battery_Charger",
            "A_Blank_Videotape_a",
            "A_Blank_Videotape_b",
            "A_Blank_Videotape_c",
            "Michelle_Milken_s_Resume",
            "Hard_Disk_Cafe_Napkin",
            "AeroDork_Gold_Card",
            "Boarding_Pass",
            "AeroDork_s_In-Flight_Magazine",
            "Some_Change",
            "DayTrotter",
            "Money",
            "Credit_Cards",
            "Membership_Tape",
            "Lana_Luscious__Resume",
            "Tramp_Casino_Matchbook",
            "Silver_Dollar",
            "Roller-skates",
            "Chi_Chi_Lambada_s_Resume",
            "Doc_Pulliam_s_Card",
            "Green_Card",
            "Doily",
        };

        static IReadOnlyDictionary<int, string> flags = new Dictionary<int, string>
        {
            { 0, "isVga" },
            { 1, "fCalledLimo" },
            { 2, "fSeenMM" },
            { 3, "fSeenLL" },
            { 4, "fSeenCC" },
            { 5, "fMudWrestled" },
            { 6, "fSkated" },
            { 7, "fBeenIn150" },
            { 8, "fBeenToTown" },
            { 9, "fBeenInNewYork" },
            { 10, "fBeenInAtlanticCity" },
            { 11, "fBeenInMiami" },
            { 12, "fCalledGreenCard" },
            { 13, "fGotQuarterNY" },
            { 14, "fUsedTape" },
            { 15, "fMMadeTape" },
            { 16, "fMCloseUp" },
            { 17, "fSeenRBOffice" },
            { 18, "fCalledDoc" },
            { 19, "fBeenIn660" },
            { 20, "fChampagneSolution" },
            { 21, "fWarned" },
            { 22, "fRecordOnTurntable" },
            { 23, "fSeenGlint" },
            { 24, "fDrawerOpen" },
            { 25, "fHasNumber" },
            { 26, "fNotFirstTimeInKRAP" },
            { 27, "fPattiBlackface" },
            { 28, "fBeenInPhilly" },
            { 29, "fBeenInBaltimore" },
            { 30, "fTookBottle" },
            { 31, "fTookDayTrotter" },
            { 32, "fOkToBoard" },
            { 33, "fUsedDoily" },
            { 34, "fTookQuarters260" },
            { 35, "fWearingBra" },
            { 36, "fPulledSlots" },
            { 37, "fCasinoSide" },
            { 38, "fLimoParked" },
            { 39, "fWorkedOutWithChiChi" },
            { 40, "fCherryGone" },
            { 41, "fVibratorMan" },
            { 42, "fBraMan" },
            { 43, "fFartMan" },
            { 44, "fHasFBINumber" },
            { 45, "fGuardKnows" },
            { 46, "fPrintedPass" },
            { 47, "fFFto480" },
            { 48, "fDirectorySol" },
            { 49, "fDidLana" },
            { 50, "fBrokeSlots" },
            { 51, "fScoredCC" },
            { 52, "fDumpedMagazine" },
            { 53, "fDegaussedTape" },
        };
    }
}
