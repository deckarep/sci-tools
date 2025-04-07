using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class GooseDeluxeAnnotator : GameAnnotator
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
            { 169, "gLanguage" },

            { 518, "gEgoName" },
            { 519, "gMacSaveNumber" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
        };
    }
}
