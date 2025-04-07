using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // Annotates unused procedures and instances with UNUSED:
    // - local object instances
    // - local procedures
    // - exported procedures
    //
    // Can't detect unused exported objects because kScriptID
    // could be dynamic.

    static class UnusedAnnotator
    {
        public static void Run(Game game)
        {
            var candidates = new Dictionary<string, Node>();
            foreach (var script in game.Scripts)
            {
                //
                // build a dictionary of candidates (local procedures and instances)
                //
                candidates.Clear();

                var localprocs = from p in script.Procedures
                                 where !script.Exports.ContainsValue(p.Name)
                                 select p;
                foreach (var localproc in localprocs)
                {
                    candidates.Add(localproc.Name, localproc.Node);
                }

                var instances = from i in script.Instances
                                where !script.Exports.ContainsValue(i.Name)
                                select i;
                foreach (var instance in instances)
                {
                    candidates.Add(instance.Name, instance.Node);
                }

                if (!candidates.Any()) continue; // optimization

                //
                // remove any candidates that appear in the script
                //

                var code = (from f in script.GetFunctions()
                            from c in f.Code
                            select c);
                foreach (var node in code.SelectMany(n => n))
                {
                    // does this node reference a candidate?
                    if (candidates.ContainsKey(node.Text))
                    {
                        // remove candidate from the list, it has been referenced
                        candidates.Remove(node.Text);

                        // optimization: stop if no candidates remain
                        if (!candidates.Any()) break;
                    }
                }

                //
                // any remaining candidates are unused
                //

                foreach (var candidate in candidates.Values)
                {
                    candidate.At(1).Annotate("UNUSED");
                }
            }

            AnnotateUnusedExportProcedures(game);
        }

        static void AnnotateUnusedExportProcedures(Game game)
        {
            // build set of export function names (candidates).
            var candidates = new HashSet<string>();
            var duplicates = new HashSet<string>();
            foreach (var script in game.Scripts)
            {
                foreach (var procedure in script.Procedures)
                {
                    if (script.Exports.ContainsValue(procedure.Name))
                    {
                        if (!candidates.Add(procedure.Name))
                        {
                            duplicates.Add(procedure.Name);
                        }
                    }
                }
            }

            // duplicates will confuse things so ignore them
            foreach (var duplicate in duplicates)
            {
                candidates.Remove(duplicate);
            }

            // scan the code in all functions for references
            foreach (var function in game.GetFunctions())
            {
                foreach (var node in function.Code.SelectMany(n => n))
                {
                    if (node is Atom && // optimization i hope
                        candidates.Contains(node.Text))
                    {
                        candidates.Remove(node.Text);
                    }
                }
            }

            // tag all the candidates
            foreach (var function in game.GetFunctions())
            {
                if (function.Object == null &&
                    candidates.Contains(function.Name))
                {
                    function.Node.Annotate("UNUSED");
                }
            }
        }
    }
}
