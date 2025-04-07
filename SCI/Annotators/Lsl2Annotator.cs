using System;
using System.Collections.Generic;
using System.Linq;
using SCI.Annotators.Original;

namespace SCI.Annotators
{
    class Lsl2Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            InventoryAnnotator.Run(Game, items);
            RunLate();
        }

        protected override Dictionary<int, Original.Script[]> GameHeaders { get { return Lsl2Symbols.Headers; } }

        protected override bool IsSci0Early
        {
            get
            {
                // Sound:fade appears after SCI0-early
                return !Game.GetScript(989).Classes.Any(c => c.Methods.Any(m => m.Name == "fade"));
            }
        }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
            { 100, "gDebugging" },
            { 101, "gCurrentStatus" },
            { 102, "gCurrentEgoView" },
            { 103, "gCurrentHenchView" },
            { 104, "gGameSeconds" },
            { 105, "gGameMinutes" },
            { 106, "gGameHours" },
            { 107, "gRank" },
            { 108, "gRgTimer" },
            { 110, "gCurrentTimer" },
            { 111, "gForceAtest" },
            { 113, "gOldTime" },
            { 115, "gSecondsInRoom" },
            { 116, "gLoadDebugNext" },
            { 117, "gDebugMenu" },
            { 118, "gShowFrag" },
            { 119, "gLogging" },
            { 120, "gMachineSpeed" },
            { 121, "gLAhaircut" },
            { 122, "gLAhenchAfterEgo" },
            { 123, "gFilthLevel" },
            { 124, "gScoredSunscreen" },
            { 125, "gWearingSunscreen" },
            { 126, "gLoweredLifeboats" },
            { 127, "gHenchOnScreen" },
            { 128, "gTimesInRm33" },
            { 129, "gWearingWig" },
            { 130, "gBlondHair" },
            { 131, "gBodyWaxed" },
            { 132, "gRmAfter40" },
            { 133, "gBraContents" },
            { 134, "gTimesInRm40" },
            { 135, "gTalkedToMD" },
            { 136, "gConfusedKrishnas" },
            { 137, "gSeenCustomsJoke" },
            { 138, "gBombStatus" },
            { 139, "gMissedPlane" },
            { 140, "gTimesInRm50" },
            { 141, "gBoreStatus" },
            { 142, "gWearingParachute" },
            { 143, "gAirplaneDoorStatus" },
            { 144, "gPastBees" },
            { 145, "gSnakeState" },
            { 146, "gPastQuicksand" },
            { 147, "gPastPiranha" },
            { 148, "gIslandStatus" },
            { 150, "gTpBuffer" },
            { 169, "gTritePhrase" },
            { 170, "gString" },
            { 470, "gLaffer" },
            { 471, "gScoredKnothole" },
            { 472, "gScoredChaise" },
            { 473, "gScoredLifeboat" },
            { 474, "gScoredRm102Sit" },
            { 475, "gScoredRm43Sit" },
            { 476, "gScoredJogger" },
            { 477, "gScoredWoreSunscreen" },
            { 478, "gScoredRosella" },
            { 479, "gReappliedSunscreen" },
            { 480, "gWornChute" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
        };

        static string[] items =
        {
            "NoInv",
            "Dollar_Bill",
            "Lottery_Ticket",
            "Cruise_Ticket",
            "Million_Dollar_Bill",
            "Swimsuit",
            "Wad_O__Dough",
            "Passport",
            "Grotesque_Gulp",
            "Sunscreen",
            "Onklunk",
            "Fruit",
            "Sewing_Kit",
            "Spinach_Dip",
            "Wig",
            "Bikini_Top",
            "Bikini_Bottom",
            "Knife",
            "Soap",
            "Matches",
            "Flower",
            "Hair_Rejuvenator",
            "Suitcase",
            "Airline_Ticket",
            "Parachute",
            "Bobby_Pin",
            "Pamphlet",
            "Airsick_Bag",
            "Stout_Stick",
            "Vine",
            "Ashes",
            "Sand",
        };
    }
}
