using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCI.Language;

// Renames a set of globals based on their global number.
// Any new names that conflict with existing global names are ignored.
//
// - Does not check to see if the new global name would conflict with any other
//   symbol types; only globals. In practice, it's fine, because the "gName"
//   convention doesn't collide with other things.
//
// - Does not handle if a global is used as a selector, for example:
//     (object global100:)
//   There's no reason that couldn't happen, but in practice it never does,
//   and it would complicate this to add that. (Double the dictionary with
//   a second "global100:" entry for each "global100")
//
// TODO: Remove "buggy characters" workaround if I remove all companion handling.

namespace SCI.Annotators
{
    public static class GlobalRenamer
    {
        public static void Run(Game game, IReadOnlyDictionary<int, string> newGlobals)
        {
            if (!newGlobals.Any()) return;

            // exclude renames if they would conflict with an existing global
            newGlobals = RemoveIllegalRenames(game, newGlobals);

            // build rename map. key: old name, value: new name
            var renames = new Dictionary<string, string>();
            var globals = game.Globals;
            foreach (var newGlobal in newGlobals)
            {
                Local global;
                if (globals.TryGetValue(newGlobal.Key, out global))
                {
                    // only use ones that haven't already been done
                    if (global.Name != newGlobal.Value)
                    {
                        renames.Add(global.Name, newGlobal.Value);

                        // handle buggy characters
                        string otherGlobalName = StripBuggyCharacters(global.Name);
                        if (otherGlobalName != global.Name)
                        {
                            renames.Add(otherGlobalName, newGlobal.Value);
                        }
                    }
                }
            }

            // fix all remaining globals that have buggy characters in them
            foreach (var global in globals)
            {
                string globalName = global.Value.Name;
                // only do globals that weren't handled above
                if (!renames.ContainsKey(globalName))
                {
                    string otherGlobalName = StripBuggyCharacters(global.Value.Name);
                    if (globalName != otherGlobalName)
                    {
                        // this global has buggy characters and is represented two ways.
                        // rename both representations to the default global name.
                        string defaultGlobalName = "global" + global.Key;
                        renames.Add(globalName, defaultGlobalName);
                        renames.Add(otherGlobalName, defaultGlobalName);
                    }
                }
            }

            // abort if there's nothing to do
            if (!renames.Any())
            {
                return;
            }

            foreach (var node in game.Scripts.SelectMany(s => s.Root).Where(n => n is Atom))
            {
                string newGlobalName;
                if (renames.TryGetValue(node.Text, out newGlobalName))
                {
                    (node as Atom).SetText(newGlobalName);
                }
            }
        }

        static IReadOnlyDictionary<int, string> RemoveIllegalRenames(Game game, IReadOnlyDictionary<int, string> newGlobals)
        {
            var newGlobalsToNotRename = new List<int>();

            foreach (var global in game.Globals.Values)
            {
                foreach (var newName in newGlobals)
                {
                    if (global.Name == newName.Value &&
                        global.Number != newName.Key)
                    {
                        Log.Debug(game, "Global " + global.Number + " already named: " + global.Name +
                                        ", can't rename global " + newName.Key);
                        newGlobalsToNotRename.Add(newName.Key);
                    }
                }
            }

            if (newGlobalsToNotRename.Any())
            {
                var newGlobalsMinusBad = new Dictionary<int, string>();
                foreach (var newGlobal in newGlobals)
                {
                    if (!newGlobalsToNotRename.Contains(newGlobal.Key))
                    {
                        newGlobalsMinusBad.Add(newGlobal.Key, newGlobal.Value);
                    }
                }
                return newGlobalsMinusBad;
            }
            else
            {
                return newGlobals;
            }
        }

        // sci companion sometimes names globals (and locals) with tricky
        // characters that it then doesn't use when naming references to them.
        // for example, global 1 (ego) in KQ5 French is named gEgo#FFEgo, but
        // all references are gEgo_FEgo
        public static IReadOnlyCollection<char> BuggyCharacters = new char[]
        {
            '#',
        };

        public static string StripBuggyCharacters(string s)
        {
            if (!s.Any(c => BuggyCharacters.Contains(c)))
            {
                return s;
            }

            var sb = new StringBuilder(s.Length);
            foreach (char c in s)
            {
                if (BuggyCharacters.Contains(c))
                {
                    sb.Append('_'); // use underscore instead
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
