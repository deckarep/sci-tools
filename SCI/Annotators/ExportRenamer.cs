using System;
using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // Renames exports. Only used for procedures.

    public static class ExportRenamer
    {
        public static void Run(Game game, IReadOnlyDictionary<Tuple<int, int>, string> newNames)
        {
            var renames = new Dictionary<string, string>(newNames.Count);
            foreach (var newName in newNames)
            {
                var export = game.GetExport(newName.Key.Item1, newName.Key.Item2);
                if (export != null && export != newName.Value)
                {
                    // ignore duplicates. see GK1 which has multiple versions of flag functions
                    // and i name them all to the same, which means on the next parse there
                    // are multiple exports with the same name.
                    string existingRename;
                    if (renames.TryGetValue(export, out existingRename) && existingRename == newName.Value)
                    {
                        continue;
                    }

                    // rename export in Script.Exports
                    var script = game.Scripts.FirstOrDefault(s => s.Number == newName.Key.Item1);
                    script.Exports[newName.Key.Item2] = newName.Value;

                    renames.Add(export, newName.Value);
                }
            }

            if (!renames.Any())
            {
                return;
            }

            var atoms = from s in game.Scripts
                        from n in s.Root
                        where n is Atom
                        select n;
            foreach (var atom in atoms)
            {
                // skip method definitions. this shouldn't be necessary,
                // but just in case there's a method selector with the name.
                // it happened once during development and was confusing.
                if (atom.Parent.Next(-1).Text == "method")
                {
                    continue;
                }

                string newName;
                if (renames.TryGetValue(atom.Text, out newName))
                {
                    (atom as Atom).SetText(newName);
                }
            }
        }
    }
}
