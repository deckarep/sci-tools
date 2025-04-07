using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Brain2Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            RunLate();

            GlobalEnumAnnotator.Run(Game, 114, difficultyLevels);
        }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
            { 114, "gDifficulty" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(5, 1), "HandsOff" },
            { Tuple.Create(5, 2), "HandsOn" },
            { Tuple.Create(5, 5), "IsFlag" },
            { Tuple.Create(5, 6), "SetFlag" },
            { Tuple.Create(5, 7), "ClearFlag" },

            { Tuple.Create(15, 0), "Say" },
        };

        static Dictionary<int, string> difficultyLevels = new Dictionary<int, string>
        {
            { 0, "Novice" },
            { 1, "Standard" },
            { 2, "Expert" },
        };

        static IReadOnlyCollection<PrintTextDef> sayProcs = new[]
        {
            new PrintTextDef(15, 0, 1), // Say
        };
        protected override IReadOnlyCollection<PrintTextDef> SayProcs { get { return sayProcs; } }
    }
}
