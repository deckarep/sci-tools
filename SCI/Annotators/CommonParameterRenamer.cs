using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // Rename parameters in commonly overridden methods.
    // changeState, doVerb, etc.
    //
    // Don't use this on Companion scripts, only on mine.
    //
    // Originally I did this in VariableNormalizer, along with other
    // methods that appear in system classes, as part of undoing
    // all of Companion's parameter naming.
    //
    // For my decompiler, I only need to rename the few that aren't
    // in system scripts, as OriginalSymbolRenamer handles the rest.

    static class CommonParameterRenamer
    {
        static Dictionary<string, string[]> methodKnownNames = new Dictionary<string, string[]>
        {
            { "changeState", new [] { "newState" } },
            { "doVerb", new [] { "theVerb", "invItem" } },
            { "handleEvent", new [] { "event" } },
            { "dispatchEvent", new [] { "event" } },
            { "newRoom", new [] { "newRoomNumber", "style" } }, // "n" in original source
            { "startRoom", new [] { "roomNum"} },
        };

        public static void Run(Game game)
        {
            var renames = new Dictionary<string, string>();

            var methods = from s in game.Scripts
                          from o in s.Objects
                          from m in o.Methods
                          select m;
            foreach (var method in methods)
            {
                string[] knownNames;
                if (!methodKnownNames.TryGetValue(method.Name, out knownNames)) continue;

                // build dictionary of renames
                renames.Clear();
                for (int i = 0; i < method.Parameters.Count; i++)
                {
                    var parameterName = method.Parameters[i].Name;
                    if (i < knownNames.Length && parameterName != knownNames[i])
                    {
                        renames.Add(parameterName, knownNames[i]);
                    }
                }

                VariableRenamer.Run(method.Node, renames);
            }
        }
    }
}
