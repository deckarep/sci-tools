using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Brain1Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            ExportRenamer.Run(Game, exports);
            RunLate();
        }

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(5, 5), "IsFlag" },
            { Tuple.Create(5, 6), "SetFlag" },
            { Tuple.Create(5, 7), "ClearFlag" },
        };

        static IReadOnlyCollection<PrintTextDef> printTextFunctions = new[]
        {
            new PrintTextDef(5, 8),
            new PrintTextDef(5, 9),
            new PrintTextDef(5, 10, 1),
            new PrintTextDef(5, 11),
            new PrintTextDef(5, 14),
            new PrintTextDef(5, 21),
        };
        protected override IReadOnlyCollection<PrintTextDef> PrintTextFunctions { get { return printTextFunctions; } }
    }
}
