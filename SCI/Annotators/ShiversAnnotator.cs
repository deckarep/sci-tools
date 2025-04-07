using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class ShiversAnnotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            ExportRenamer.Run(Game, exports);
            RunLate();
        }

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(951, 3), "SetFlag" },
            { Tuple.Create(951, 4), "ClearFlag" },
            { Tuple.Create(951, 5), "IsFlag" },
        };
    }
}
