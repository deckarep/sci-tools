using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Hoyle4Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            RunLate();
        }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "RedrawCast" },
            { Tuple.Create(0, 2), "IsEnter" },
            { Tuple.Create(0, 3), "HandsOn" },
            { Tuple.Create(0, 4), "HandsOff" },
            { Tuple.Create(0, 7), "EatMouseEvents" },
        };
    }
}
