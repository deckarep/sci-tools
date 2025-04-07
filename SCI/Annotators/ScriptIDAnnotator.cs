using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // (ScriptID 406 0) => (ScriptID 406 0) ; Clock
    //
    // Annotating ScriptID calls was one of the first things I did.

    static class ScriptIDAnnotator
    {
        public static void Run(Game game)
        {
            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                // scope creep: setScript: # is the first export of script #.
                // ignore zero, since that clears a script.
                if (node.Text == "setScript:")
                {
                    var scriptNumberNode = node.Next(1);
                    if (scriptNumberNode is Integer)
                    {
                        if (scriptNumberNode.Number == 0) continue;

                        var setScriptScript = game.GetScript(scriptNumberNode.Number);
                        if (setScriptScript != null && setScriptScript.Exports.Any())
                        {
                            scriptNumberNode.Annotate(setScriptScript.Exports.First().Value);
                        }
                    }
                    continue;
                }

                // (ScriptID script [export])
                if (!(node.At(0).Text == "ScriptID" && node.At(1) is Integer))
                {
                    continue;
                }

                // fix the script number if decompiler output it as negative
                (node.At(1) as Integer).MakeUnsigned();

                var script = game.Scripts.FirstOrDefault(s => s.Number == node.At(1).Number);
                if (script == null)
                {
                    node.At(0).Annotate("MISSING SCRIPT");
                    continue;
                }

                int exportNumber = 0;
                if (node.Children.Count > 2)
                {
                    if (node.At(2) is Integer)
                    {
                        exportNumber = node.At(2).Number;
                    }
                    else
                    {
                        // export parameter exists but isn't an integer, do nothing
                        continue;
                    }
                }

                string export;
                if (script.Exports.TryGetValue(exportNumber, out export) ||
                    (exportNumber == 0 && // if 0 is passed but there's no zero, use the first one
                     script.Exports.Any() &&
                     script.Exports.TryGetValue(script.Exports.Keys.Min(), out export)))
                {
                    // only annotate if export is an object.
                    // for example, skip (ScriptID #) when export 0 is proc.
                    if (script.Objects.Any(o => o.Name == export))
                    {
                        node.At(0).Annotate(export);
                    }
                }
                else
                {
                    node.At(0).Annotate("MISSING EXPORT");
                }
            }
        }
    }
}
