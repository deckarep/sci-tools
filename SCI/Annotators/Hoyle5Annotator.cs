using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    // Two versions of Hoyle5: Classic Games and Solitaire.
    // Solitaire has room scripts in the 6000's.

    class Hoyle5Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            bool isSolitaire = (Game.GetScript(6001) != null);
            GlobalRenamer.Run(Game, globals);
            if (!isSolitaire)
            {
                GlobalRenamer.Run(Game, gamesGlobals);
            }
            ExportRenamer.Run(Game, exports);
            if (!isSolitaire)
            {
                ExportRenamer.Run(Game, gamesExports);
            }
            RunLate();
        }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
            { 193, "gCardGameScriptNumber" },
            { 194, "gPlayerCount" },
            { 196, "gSkill" },
        };

        static Dictionary<int, string> gamesGlobals = new Dictionary<int, string>
        {
            // i was working a gin rummy bug!
            { 241, "gGinRummyOptionSortMode" },  // how the cards are sorted
            { 242, "gGinRummyOptionIsVariant" }, // true if Oklahoma Style where max deadwood depends on upcard
            { 243, "gGinRummyTurnCount" },

            { 394, "gSortMode" }, // general version, gets set to gGinRummyOptionSortMode

            { 896, "gAttitudeSetting" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "RedrawCast" },

            { Tuple.Create(0, 3), "EnableCursor" },
            { Tuple.Create(0, 4), "DisableCursor" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> gamesExports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(400, 1), "ShowGinRummyScore" },
        };
    }
}
