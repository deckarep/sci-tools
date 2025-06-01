using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class SlaterAnnotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            ExportRenamer.Run(Game, exports);
            RunLate();
        }

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "SetFlag" },
            { Tuple.Create(0, 2), "ClearFlag" },
            { Tuple.Create(0, 3), "IsFlag" },
        };
    }
}
