using System;
using System.Collections.Generic;
using System.Linq;

namespace SCI.Annotators
{
    class Kq4Annotator : GameAnnotator
    {
        // The first two KQ4's are SCI0 early, and we need to identify
        // them so that their selector table is handled correctly.
        protected override bool IsSci0Early
        {
            get
            {
                // "Avoid" script 985 appears after the early games
                return (Game.GetScript(985) == null);
            }
        }

        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            // this is different than the IsSci0Early test above.
            // this is about which exports to use.
            bool isEarly = Game.GetScript(0).Instances.Any(i => i.Name == "timer5");
            ExportRenamer.Run(Game, isEarly ? earlyExports : lateExports);
            InventoryAnnotator.Run(Game, items);
            RunLate();
        }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
            { 100, "gNight" },
            { 101, "gIndoors" },
            { 109, "gAct" },
            { 113, "gShovelCount" },
            { 118, "gMinstrelRoom" },
            { 119, "gMinstrelActor" },
            { 122, "gDwarfEscortOut" },
            { 123, "gUnicornState" },
            { 124, "gUnicornRoom" },
            { 125, "gUnicornActor" },
            { 126, "gTrollChasing" },
            { 127, "gDeathFlag" },
            { 163, "gSeenOgressDeerFlag" },
            { 169, "gLolotteAlive" },
            { 203, "gRavenActor" },
            { 220, "gLolotteDoorUnlocked" },
            { 227, "gLolotteDoorOpen" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> earlyExports = new Dictionary<Tuple<int, int>, string>()
        {
            // original method names
            { Tuple.Create(0, 9), "NormalEgo" },
            { Tuple.Create(0, 10), "HandsOff" },
            { Tuple.Create(0, 11), "HandsOn" },
            { Tuple.Create(0, 12), "Notify" },
            { Tuple.Create(0, 14), "HaveMem" },
            { Tuple.Create(0, 15), "NotClose" },
            { Tuple.Create(0, 16), "AlreadyTook" },
            { Tuple.Create(0, 17), "SeeNothing" },
            { Tuple.Create(0, 18), "CantDo" },
            { Tuple.Create(0, 19), "DontHave" },
            { Tuple.Create(0, 20), "RedrawCast" },
            { Tuple.Create(0, 21), "SoundLoops" },
            { Tuple.Create(0, 23), "cls" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> lateExports = new Dictionary<Tuple<int, int>, string>()
        {
            // original method names
            { Tuple.Create(0, 7), "NormalEgo" },
            { Tuple.Create(0, 8), "HandsOff" },
            { Tuple.Create(0, 9), "HandsOn" },
            { Tuple.Create(0, 10), "Notify" },
            { Tuple.Create(0, 12), "HaveMem" },
            { Tuple.Create(0, 13), "NotClose" },
            { Tuple.Create(0, 14), "AlreadyTook" },
            { Tuple.Create(0, 15), "SeeNothing" },
            { Tuple.Create(0, 16), "CantDo" },
            { Tuple.Create(0, 17), "DontHave" },
            { Tuple.Create(0, 18), "RedrawCast" },
            { Tuple.Create(0, 19), "SoundLoops" },
            { Tuple.Create(0, 21), "cls" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 2), "NearControl" }, // original method name
            { Tuple.Create(0, 3), "LookAt" }, // original method name
        };

        static string[] items =
        {
            "Silver_Flute",
            "Diamond_Pouch",
            "Talisman",
            "Lantern__unlit",
            "Pandora_s_Box",
            "Gold_Ball",
            "Witches__Glass_Eye",
            "Obsidian_Scarab",
            "Peacock_Feather",
            "Lute",
            "Small_Crown",
            "Frog",
            "Silver_Baby_Rattle",
            "Gold_Coins",
            "Cupid_s_Bow",
            "Shovel",
            "Axe",
            "Fishing_Pole",
            "Shakespeare_Book",
            "Worm",
            "Skeleton_Key",
            "Golden_Bridle",
            "Board",
            "Bone",
            "Dead_Fish",
            "Magic_Fruit",
            "Sheet_Music",
            "Silver_Whistle",
            "Locket",
            "Medal",
            "Toy_Horse",
            "Glass_Bottle",
            "Gold_Key",
            "Magic_Hen",
            "Rose",
            "Note",
        };
    }
}
