using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class FairyTalesAnnotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            VerbAnnotator.Run(Game, verbs, inventoryVerbs);
            InventoryAnnotator.Run(Game, items);
            RunLate();
        }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(10, 1), "HandsOff" },
            { Tuple.Create(10, 2), "HandsOn" },
            { Tuple.Create(10, 3), "SetFlag" },
            { Tuple.Create(10, 4), "ClearFlag" },
            { Tuple.Create(10, 5), "IsFlag" },
            { Tuple.Create(10, 6), "NormalEgo" },

            { Tuple.Create(10, 9), "Face" },
        };

        static IReadOnlyDictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 2, "Look" },
            { 5, "Do" },
        };

        static IReadOnlyDictionary<int, string> inventoryVerbs = new Dictionary<int, string>
        {
        };

        static string[] items =
        {
        };
    }
}
