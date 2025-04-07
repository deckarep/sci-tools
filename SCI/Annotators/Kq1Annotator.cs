using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Kq1Annotator : GameAnnotator
    {
        public override void Run()
        {
            bool demo = (Game.GetExport(0, 0) == "Demo000");
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, demo ? demoExports : exports);
            InventoryAnnotator.Run(Game, items);
            RunLate();
        }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 2), "HandsOff" },
            { Tuple.Create(0, 3), "HandsOn" },

            { Tuple.Create(0, 9), "LogText" },

            { Tuple.Create(0, 10), "SetFlag" },
            { Tuple.Create(0, 11), "ClearFlag" },
            { Tuple.Create(0, 12), "IsFlag" },

            { Tuple.Create(0, 16), "EgoDead" },
            { Tuple.Create(0, 19), "SetScore" },

            { Tuple.Create(0, 20), "UpdateWaterBucket" },
            { Tuple.Create(0, 22), "PrintTooBusy" },
            { Tuple.Create(0, 25), "PlayBackSound" },
            { Tuple.Create(0, 26), "FadeBackSound" },
            { Tuple.Create(0, 27), "UpdatePebbles" },
            { Tuple.Create(0, 28), "AmigaPauseBackSound" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> demoExports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "HandsOff" },
            { Tuple.Create(0, 2), "cls" },
            { Tuple.Create(0, 3), "RedrawCast" },
            { Tuple.Create(0, 4), "NormalEgo" },
        };

        static string[] items =
        {
            "Dagger",
            "Chest",
            "Carrot",
            "Key",
            "Note",
            "Magic_Ring",
            "Four-leaf_Clover",
            "Ceramic_Bowl",
            "Empty_Water_Bucket",
            "Pebbles",
            "Leather_Slingshot",
            "Pouch",
            "Sceptre",
            "Cheese",
            "Magic_Mirror",
            "Gold_Egg",
            "Magic_Shield",
            "Fiddle",
            "Walnut",
            "Mushroom",
            "Beans",
        };
    }
}
