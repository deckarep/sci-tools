using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class LighthouseAnnotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            VerbAnnotator.Run(Game, verbs);
            RunLate();
        }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "DoRobot" },
            { Tuple.Create(0, 2), "KillRobot" },

            { Tuple.Create(0, 5), "IsFlag" },
            { Tuple.Create(0, 6), "SetFlag" },
            { Tuple.Create(0, 7), "ClearFlag" },
        };

        static Dictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 5, "Do" },

            { 6, "invCarKeys" },
            { 7, "invDrawbridgeKey" },
            { 8, "invBabyBottle" },
            { 9, "invRocks" },
            { 10, "invUmbrella" },
            { 11, "invLighter" },
            { 12, "invBag" },
            { 13, "invCompass" },
            { 14, "invLetter" },
            { 15, "invLightHouseKey" },
            { 16, "invLetterOpener" },
            { 17, "invPuzzleBoxKey" },
            { 19, "invShell" },
            { 20, "invFish" },
            { 21, "invSparrow" },
            { 25, "invCrowbar" },
            { 26, "invSolderingIron" },
            { 27, "invTube" },
            { 28, "invWire" },
            { 29, "invWindUpKey" },
            { 30, "invShedKey" },
            { 31, "invSpring" },
            { 32, "invToySoldier" },
            { 33, "invEnvelope" },
            { 34, "invNotebook" },
            { 35, "invStudyNotes" },
            { 36, "invBedroomNotes" },
            { 37, "invKitchenNotes" },
            { 38, "invLabNotes" },
            { 40, "invIonizer" },
            { 41, "invThrottle" },
            { 42, "invDeskKey" },
            { 43, "invBatKey" },
            { 44, "invWhistle" },
            { 46, "invAlanWrench" },
            { 47, "invBottleNote" },
            { 48, "invModulator" },
            { 50, "invJems" },
            { 51, "invBaubles" },
            { 52, "invCrank" },
            { 53, "invGears" },
            { 54, "invRadioControl" },
            { 55, "invRadioBroke" },
            { 56, "invOrnithopterPart" },
            { 57, "invMachinePart" },
            { 58, "invMachineTube" },
            { 59, "invMachineBox" },
            { 60, "invCD" },
            { 61, "invLogs" },
            { 62, "invIngots" },
            { 63, "invCoal" },
            { 64, "invMold" },
            { 65, "invCircuitBoard" },
            { 66, "invCrystalBottle" },
            { 67, "invVacuumPump" },
            { 68, "invStock" },
            { 69, "invPowerSupply" },
            { 70, "invFiringMechanism" },
            { 71, "invBarrel" },
            { 72, "invBluePrints" },
            { 73, "invCannon" },
            { 74, "invWindGear" },
            { 75, "invTimeBomb" },
            { 76, "invRockHammer" },
            { 77, "invDynamite" },
            { 78, "invAlarmClock" },
            { 79, "invSmallPlanks" },
            { 80, "invLargePlanks" },
            { 81, "invTool" },
            { 82, "invPuzzleAmulet" },
            { 83, "invCannonBall" },
            { 84, "invCannonPowder" },
            { 85, "invCannonFuse" },
            { 86, "invStatueKey" },
            { 87, "invHexBar" },
            { 88, "invBoltCutter" },
            { 89, "invPliers" },
            { 90, "invWrench" },
            { 91, "invAntenna" },
            { 92, "invRail" },
            { 93, "invAmanda" },
            { 94, "invDooDads" },
            { 95, "invTorch" },
            { 96, "invBeingInBottle" },
            { 97, "invPlans" },
        };
    }
}
