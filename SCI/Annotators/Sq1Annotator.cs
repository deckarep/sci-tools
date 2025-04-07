using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Sq1Annotator : GameAnnotator
    {
        public override void Run()
        {
            bool demo = (Game.GetExport(0, 0) == "SQ1Demo");
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, demo ? demoExports : exports);
            VerbAnnotator.Run(Game, verbs, ArrayToDictionary(0, inventoryVerbs));
            InventoryAnnotator.Run(Game, items);
            RunLate();

            Sq1TalkerAnnotator.Run(Game, TextMessageFinder);
            Sq1DeathAnnotator.Run(Game, TextMessageFinder);
        }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
            { 165, "gBuckazoidCount" },
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

            { Tuple.Create(0, 10), "EgoDead" },
            { Tuple.Create(0, 11), "SetScore" },

            { Tuple.Create(0, 13), "Face" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> demoExports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "HandsOff" },
            { Tuple.Create(0, 2), "HandsOn" },
        };

        static IReadOnlyDictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 2, "Look" },
            { 3, "Do" },
            { 4, "Inventory" },
            { 5, "Talk" },
            { 11, "Taste" },
            { 12, "Smell" },
        };

        static string[] inventoryVerbs =
        {
            "Cartridge",
            "keyCard",
            "Gadget",
            "Survival_Kit",
            "Knife",
            "Dehydrated_Water",
            "Broken_Glass",
            "Rock",
            "Orat_Part",
            "Skimmer_Key",
            "buckazoid",
            "Jetpack",
            "Pulseray_Laser_Pistol",
            "Grenade",
            "Remote",
            "Widget",
            "Plant",
            "Bar_Coupon",
            "Droids-B-Us_coupon",
            "Sarien_ID_Card",
        };

        // same as verbs
        static string[] items =
        {
            "Cartridge",
            "keyCard",
            "Gadget",
            "Survival_Kit",
            "Knife",
            "Dehydrated_Water",
            "Broken_Glass",
            "Rock",
            "Orat_Part",
            "Skimmer_Key",
            "buckazoid",
            "Jetpack",
            "Pulseray_Laser_Pistol",
            "Grenade",
            "Remote",
            "Widget",
            "Plant",
            "Bar_Coupon",
            "Droids-B-Us_coupon",
            "Sarien_ID_Card",
        };
    }
}
