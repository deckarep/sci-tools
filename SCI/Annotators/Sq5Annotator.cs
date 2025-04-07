using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Sq5Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            VerbAnnotator.Run(Game, verbs);
            InventoryAnnotator.Run(Game, items);
            RunLate();

            Sq5DeathAnnotator.Run(Game, MessageFinder);
            Sq5LocationAnnotator.Run(Game);
        }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
            { 113, "gEurekaLocation" },
            { 126, "gSpikeState" },
            { 127, "gGarbagePickupCount" }, // first act, 0-3, then you can go to spacebar
            { 130, "gCliffyState" },
            { 133, "gGoliathFloorNum" },
            { 142, "gAct" }, // 0 = initial, 1 = distress call, 2 = goliath attacks
            { 164, "gBeaState" },
            { 170, "gWD40State" }, // 0 = initial, 1 = repairing, 2 = science officer
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "IsFlag" },
            { Tuple.Create(0, 2), "SetFlag" },
            { Tuple.Create(0, 3), "ClearFlag" },

            { Tuple.Create(0, 5), "StepOn" },
            { Tuple.Create(0, 6), "NormalEgo" },
            { Tuple.Create(0, 8), "Face" },
            { Tuple.Create(0, 9), "EgoDead" },
            { Tuple.Create(0, 10), "SetScore" },
            { Tuple.Create(0, 12), "Localize" },
        };

        static IReadOnlyDictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 3, "Walk" },
            { 1, "Look" },
            { 4, "Do" },
            { 2, "Talk" },
            { 24, "Order" },

            { 6, "Ship_Opener" },
            { 17, "Buckazoids" },
            { 18, "Floor_Scrubber" },
            { 19, "Distributor_Cap" },
            { 20, "Safety_Cones" },
            { 21, "Kiz_Branch" },
            { 22, "Kiz_Fruit" },
            { 23, "frock" },
            { 25, "Oxygen_Tank" },
            { 26, "Cloaking_Device" },
            { 28, "Transporter_Fuse" },
            { 29, "Antacid" },
            { 30, "Cutting_Torch" },
            { 31, "Spike" },
            { 32, "Communicator" },
            { 33, "Hole_Punch" },
            { 34, "Space_Monkeys_Package" },
            { 35, "Business_Card" },
            { 36, "Genetix_Canister" },
            { 37, "Liquid_Nitro_Tank" },
            { 38, "WD40_Head" },
            { 39, "Oxygen_Mask" },
            { 42, "Paper" },
        };

        static string[] items =
        {
            "Buckazoids",
            "Floor_Scrubber",
            "Safety_Cones",
            "Distributor_Cap",
            "Transporter_Fuse",
            "Antacid",
            "Ship_Opener",
            "Cutting_Torch",
            "Spike",
            "Oxygen_Tank",
            "Communicator",
            "Hole_Punch",
            "Cloaking_Device",
            "Space_Monkeys_Package",
            "Business_Card",
            "Genetix_Canister",
            "Liquid_Nitro_Tank",
            "Kiz_Branch",
            "Kiz_Fruit",
            "frock",
            "WD40_Head",
            "Oxygen_Mask",
            "Paper",
        };
    }
}
