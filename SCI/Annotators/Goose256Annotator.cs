using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Goose256Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            RunLate();
        }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
            { 179, "gSaveSlot" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {

            { Tuple.Create(0, 1), "NormalEgo" },
            { Tuple.Create(0, 2), "HandsOff" },
            { Tuple.Create(0, 3), "HandsOn" },
            { Tuple.Create(0, 4), "HaveMem" },
            { Tuple.Create(0, 5), "RedrawCast" },
            { Tuple.Create(0, 6), "clr" },
        };
    }
}
