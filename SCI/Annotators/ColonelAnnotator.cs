using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class ColonelAnnotator : GameAnnotator
    {
        public override void Run()
        {
            bool demo = (Game.GetExport(0, 0) == "MM1");
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, demo ? demoExports : exports);
            InventoryAnnotator.Run(Game, items);
            RunLate();

            GlobalEnumAnnotator.Run(Game, 123, corpseFlags);
        }

        static IReadOnlyCollection<PrintTextDef> printTextFunctions = new[]
        {
            new PrintTextDef(0, 1, 1), // say
            new PrintTextDef(0, 19), // death dialogs
        };
        protected override IReadOnlyCollection<PrintTextDef> PrintTextFunctions { get { return printTextFunctions; } }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
            { 109, "gElevatorState" },
            { 115, "gJeevesChoresState" },
            { 118, "gMustDos" },
            { 119, "gCaneLocation" }, // not quite a room #
            { 121, "gWilburCorpseRoomNum" },
            { 123, "gCorpseFlags" },
            { 138, "gTombBarred" },
            { 154, "gClarenceWilburState" },
            { 165, "gAct" },
            { 168, "gCigarButtLocation" }, // not quite a room #
            { 170, "gEthelCorpseRoomNum" },
            { 177, "gGertieRoomState" },
            { 173, "gSpyFlags" },
            { 186, "gHour" },
            { 187, "gMinute" },
            { 192, "gFifiState" },
            { 200, "gEthelState" },
            { 223, "gDetailLevel" },
            { 368, "gCycleTimers" }, // as opposed to gTimers, the `timers` collection
            { 388, "gAtticFirstTime" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "Say" },
            { Tuple.Create(0, 2), "LookAt" },
            { Tuple.Create(0, 3), "HandsOff" },
            { Tuple.Create(0, 4), "HandsOn" },

            { Tuple.Create(0, 5), "SetFlag" },
            { Tuple.Create(0, 6), "ClearFlag" },
            { Tuple.Create(0, 7), "IsFlag" },

            { Tuple.Create(0, 8), "HaveMem" },
            { Tuple.Create(0, 9), "NotClose" },
            { Tuple.Create(0, 10), "AlreadyTook" },
            { Tuple.Create(0, 11), "SeeNothing" },
            { Tuple.Create(0, 12), "CantDo" },
            { Tuple.Create(0, 13), "DontHave" },
            { Tuple.Create(0, 14), "RedrawCast" },
            { Tuple.Create(0, 15), "cls" },
            { Tuple.Create(0, 16), "AlreadyOpen" },
            { Tuple.Create(0, 17), "AlreadyClosed" },
            { Tuple.Create(0, 18), "NotHere" },
            { Tuple.Create(0, 19), "EgoDead" },
            { Tuple.Create(0, 20), "IsFirstTimeInRoom" },
            { Tuple.Create(0, 21), "LoadMany" }, // it's a make-shift LoadMany

            { Tuple.Create(0, 22), "Ok" },

            { Tuple.Create(0, 25), "DoLook" }, // for shift clicking objects
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> demoExports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 10), "HandsOff" },
            { Tuple.Create(0, 11), "HandsOn" },
            { Tuple.Create(0, 20), "RedrawCast" },
            { Tuple.Create(0, 23), "cls" },
            { Tuple.Create(0, 35), "Teleport" },
        };

        static string[] items =
        {
            "necklace",
            "monocle",
            "lantern",
            "oilcan",
            "rolling_pin",
            "skeleton_key",
            "poker",
            "crowbar",
            "cigar_butt",
            "broken_record",
            "notebook_and_pencil",
            "crackers",
            "soup_bone",
            "valve_handle",
            "bullet",
            "derringer",
            "matches",
            "carrot",
            "brass_key",
            "diary",
            "crank",
            "cane",
            "pouch",
            "handkerchief",
        };

        static Dictionary<int, string> corpseFlags = new Dictionary<int, string>
        {
            { 0x01, "Gertie" },
            { 0x02, "Wilbur" },
            { 0x04, "Gloria" },
            { 0x08, "Ethel" },
            { 0x10, "Jeeves & Fifi" },
            { 0x20, "Clarence" },
            { 0x3f, "Everyone but Lillian" },
            { 0x40, "Lillian" },
        };
    }
}
