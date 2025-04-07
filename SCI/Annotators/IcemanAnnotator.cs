using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class IcemanAnnotator : GameAnnotator
    {
        public override void Run()
        {
            bool demo = (Game.GetExport(0, 0) == "iceDemo");
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, demo ? demoExports : exports);
            InventoryAnnotator.Run(Game, items);
            RunLate();

            IcemanMessageAnnotator.Run(Game, TextMessageFinder);
        }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "HandsOff" },
            { Tuple.Create(0, 2), "HandsOn" },

            { Tuple.Create(821, 0), "EgoDead" },
            { Tuple.Create(828, 0), "SetScore" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> demoExports = new Dictionary<Tuple<int, int>, string>()
        {
        };

        // this game re-uses inventory numbers in different areas
        static string[] items =
        {
            // 0-2 common
            "Envelope",
            "Microfilm",
            "ID_Card",
            // 4 Tahiti items, bunch of Sub, bunch of Tunisia
            "Tahiti: Black_Book | Sub: Explosive | Tunisia: Fish",
            "Tahiti: Change | Sub: Rum_Bottle | Tunisia: Capsule",
            "Tahiti: Key | Sub: Storage_Compartment_Key | Tunisia: Map",
            "Tahiti: Earring | Sub: Diver | Tunisia: Key",
            "Sub: Flare | Tunisia: Duct_Tape",
            "Sub: Device | Tunisia: Tranquilizer_Gun",
            "Sub: Cotter_pin | Tunisia: Sugar_Canister",
            "Sub: Washer | Tunisia: Flour_Canister",
            "Sub: Nut | Tunisia: Coffee_Canister",
            "Sub: Metal_Cylinder | Tunisia: Business_Card",
            "Sub: Vernier_Caliper | Tunisia: Food_Platter",
            "Sub: Code_Book | Tunisia: Note",
            // Sub only
            "Hammer",
            "Open_End_Wrench",
            "sheared_cylinder",
        };
    }
}
