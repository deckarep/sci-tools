using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // GlobalNameDeducer renames global variables based off their usage.
    //
    // SCI Companion does this for all variable types. In my opinion,
    // it's better to only do globals with conservative rules.
    // Otherwise, you're guaranteed to generate at least some confusing
    // names. A little auto-naming goes a long way, and any confusing
    // name is worse than none. It's better to not risk it.
    //
    // Example:
    // (= global100 myObject)  =>   gMyObject
    //
    // A global is only renamed if there is only one distinct name it
    // could be. Any other assignments disqualify it from being renamed.
    // A global is only renamed if it is the only global with that name.

    static class GlobalNameDeducer
    {
        public static void Run(Game game, IReadOnlyDictionary<int, string> systemGlobals)
        {
            // create map of global names to set of potential names
            var allPotentialNames = new Dictionary<string, HashSet<string>>();
            foreach (var global in game.Globals)
            {
                // skip system globals, i don't want this annotator overriding them.
                if (!systemGlobals.ContainsKey(global.Key))
                {
                    allPotentialNames.Add(global.Value.Name, new HashSet<string>());
                }
            }

            // create set of all class names
            var classNames = new HashSet<string>(game.GetClasses().Select(c => c.Name));

            // collect potential names for each global
            foreach (var function in game.GetFunctions())
            {
                foreach (var node in function.Node)
                {
                    HashSet<string> potentialNames;
                    if (node.Children.Count == 3 &&
                        node.At(0).Text == "=" &&
                        allPotentialNames.TryGetValue(node.At(1).Text, out potentialNames))
                    {

                        string name = GetName(game, function, classNames, node);
                        if (name != null)
                            potentialNames.Add(name);
                    }
                }
            }

            // build rename map
            var renames = new Dictionary<string, string>();
            foreach (var potentialNames in allPotentialNames)
            {
                if (potentialNames.Value.Count == 1)
                {
                    string name = potentialNames.Value.First();
                    if (name != "")
                    {
                        name = "g" + char.ToUpper(name[0]) + name.Substring(1);
                        renames.Add(potentialNames.Key, name);
                    }
                }
            }

            // eliminate duplicates. maybe i'll append digits later, but for now, no dupes.
            Dictionary<string, HashSet<string>> reverse = ReverseMap(renames);
            foreach (var r in reverse)
            {
                if (r.Value.Count > 1)
                {
                    foreach (var global in r.Value)
                    {
                        renames.Remove(global);
                    }
                }
            }

            // make an int-string map like GlobalRenamer wants
            var finalRenames = new Dictionary<int, string>(renames.Count);
            foreach (var global in game.Globals)
            {
                string newName;
                if (renames.TryGetValue(global.Value.Name, out newName))
                {
                    finalRenames.Add(global.Key, newName);
                }
            }
            GlobalRenamer.Run(game, finalRenames);
        }

        static HashSet<string> assignmentSelectors = new HashSet<string>
        {
            "new:",
            "clone:",
            "yourself:",
        };

        static string GetName(Game game, Function function, HashSet<string> classNames, Node node)
        {
            // assignment? recurse
            if (node.At(0).Text == "=")
            {
                // but before we recurse, look for a "name: {string}" being sent to this assignment node
                string stringName = GetStringName(node);
                if (!string.IsNullOrWhiteSpace(stringName))
                {
                    return stringName;
                }

                // okay now we can recurse
                return GetName(game, function, classNames, node.At(2));
            }
            // kScriptID? object name
            if (node.At(0).Text == "ScriptID" &&
                node.At(1) is Integer &&
                node.At(2) is Integer)
            {
                var obj = game.GetExportedObject(node.At(1).Number, node.At(2).Number);
                return ((obj != null) ? obj.Name : "");
            }
            // number?
            if (node is Integer)
            {
                if (node.Number == 0)
                {
                    return null; // ignore assigning zero
                }
                else
                {
                    return ""; // disqualified
                }
            }
            // atom? only if it's an object or class
            if (node is Atom)
            {
                if (node.Text == "self")
                {
                    if (function.Type == FunctionType.Method)
                    {
                        return function.Object.Name;
                    }
                    return ""; // disqualified
                }
                if (function.Script.Objects.Any(o => o.Name == node.Text) ||
                    classNames.Contains(node.Text))
                {
                    return node.Text;
                }
                return ""; // disqualified
            }
            // list that ends in "new:", "clone:", "yourself:"...?
            // recurse on first node
            if (node.Children.Count > 1 &&
                assignmentSelectors.Contains(node.Children.Last().Text))
            {
                return GetName(game, function, classNames, node.At(0));
            }
            return ""; // disqualified
        }

        static string GetStringName(Node node)
        {
            // looking for:
            // (node ... name: {string} ...)
            var parent = node.Parent;
            if (parent.At(0) != node) return null;

            for (int i = 1; i < parent.Children.Count; i++)
            {
                if (parent.At(i).Text == "name:")
                {
                    if (parent.At(i + 1) is Language.String)
                    {
                        return parent.At(i + 1).Value.ToString().SanitizeSymbol();
                    }
                }
            }
            return null;
        }

        // reverses a map so that the values become the keys and the old keys
        // become a hashset value.
        static Dictionary<V, HashSet<K>> ReverseMap<K, V>(Dictionary<K, V> map)
        {
            var rev = new Dictionary<V, HashSet<K>>(map.Count);
            foreach (var kv in map)
            {
                HashSet<K> set;
                if (!rev.TryGetValue(kv.Value, out set))
                {
                    set = new HashSet<K>();
                    rev.Add(kv.Value, set);
                }
                set.Add(kv.Key);
            }
            return rev;
        }
    }
}
