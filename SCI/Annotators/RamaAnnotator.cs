using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class RamaAnnotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            ExportRenamer.Run(Game, exports);
            RunLate();
        }

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(1111, 18), "SetFlag" },
            { Tuple.Create(1111, 19), "ClearFlag" },
            { Tuple.Create(1111, 20), "IsFlag" },
        };
    }
}
