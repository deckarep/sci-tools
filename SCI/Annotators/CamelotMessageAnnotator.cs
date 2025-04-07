using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // There is overlap with PrintTextAnnotator

    static class CamelotMessageAnnotator
    {
        public static void Run(Game game, TextMessageFinder messageFinder)
        {
            // order matters, see below when i remove the first three.
            // functions that take modNum and textNum as their first two params
            var globalFunctions = new List<PrintTextDef>();
            globalFunctions.Add(new PrintTextDef("Display")); // kernel function
            globalFunctions.Add(new PrintTextDef("GetFarText")); // kernel function
            globalFunctions.Add(new PrintTextDef("Print"));
            globalFunctions.Add(new PrintTextDef("Printf"));
            globalFunctions.Add(new PrintTextDef(128, 0)); // death dialog
            globalFunctions.Add(new PrintTextDef(0, 14)); // Camelot print function, used a lot
            globalFunctions.Add(new PrintTextDef(152, 1)); // jeru scenes

            // lookup names
            foreach (var globalFunction in globalFunctions)
            {
                if (globalFunction.Name == null)
                {
                    globalFunction.Name = game.GetExport(globalFunction.ScriptNumber, globalFunction.ExportNumber);
                }
            }

            // scan for all global functions that are print wrappers
            foreach (var script in game.Scripts)
            {
                var exportedFunctions = from p in script.Procedures
                                        where script.Exports.Values.Contains(p.Name)
                                        select p;
                foreach (var exportedFunction in exportedFunctions)
                {
                    var printTextDef = IsPrintWrapperFunction(exportedFunction, globalFunctions);
                    if (printTextDef != null)
                    {
                        globalFunctions.Add(printTextDef);
                    }
                }
            }

            // scan for all local functions that are print wrappers
            var localFunctions = new Dictionary<Script, List<PrintTextDef>>();
            foreach (var script in game.Scripts)
            {
                var functions = from p in script.Procedures
                                where !script.Exports.Values.Contains(p.Name)
                                select p;
                foreach (var function in functions)
                {
                    var printTextDef = IsPrintWrapperFunction(function, globalFunctions);
                    if (printTextDef != null)
                    {
                        if (!localFunctions.ContainsKey(script))
                        {
                            localFunctions.Add(script, new List<PrintTextDef>());
                        }
                        localFunctions[script].Add(printTextDef);
                    }
                }
            }

            // remove Display, Print, and Printf, i was only using those to detect
            // wrapper functions. PrintTextAnnotator handles those.
            globalFunctions.RemoveRange(0, 4);

            var globalFunctionNames = new HashSet<string>(globalFunctions.Select(f => f.Name));
            foreach (var script in game.Scripts)
            {
                if (PrintTextAnnotator.IsBadScript(script)) continue;

                foreach (var node in script.Root)
                {
                    // (function modNum number
                    if (node.Children.Count >= 3 && // optimization
                        //node.At(1) is Integer &&
                        //node.At(2) is Integer &&
                        (globalFunctionNames.Contains(node.At(0).Text) ||
                         (localFunctions.ContainsKey(script) &&
                          localFunctions[script].Any(f => f.Name == node.At(0).Text))))
                    {
                        var printFunction = globalFunctions.FirstOrDefault(f => f.Name == node.At(0).Text);
                        if (printFunction == null)
                        {
                            printFunction = localFunctions[script].First(f => f.Name == node.At(0).Text);
                        }
                        var modNum = node.At(printFunction.ParamIndex + 1);
                        var textNum = node.At(printFunction.ParamIndex + 2);
                        if (modNum is Integer && textNum is Integer)
                        {
                            var message = messageFinder.GetMessage(modNum.Number, textNum.Number);
                            if (message != null)
                            {
                                node.At(0).Annotate(message.Text.QuoteMessageText());
                            }
                            else
                            {
                                node.At(0).Annotate("MISSING MESSAGE");
                            }
                        }
                    }
                }
            }
        }

        // originally copied from LongbowMessageAnnotator. now does PrintTextDef
        static PrintTextDef IsPrintWrapperFunction(Function function, IReadOnlyList<PrintTextDef> printProcs)
        {
            // only evaluating the top-level code statements.
            // if function calls printProc &rest or printProc param1 param2 then it's a wrapper
            foreach (var node in function.Code)
            {
                if (printProcs.Any(pf => pf.Name == node.At(0).Text))
                {
                    if (node.At(1).Text == "&rest")
                    {
                        return new PrintTextDef(function.Name, function.Parameters.Count);
                    }
                    if (function.Parameters.Count >= 2)
                    {
                        if (node.At(1).Text == function.Parameters[0].Name &&
                            node.At(2).Text == function.Parameters[1].Name)
                        {
                            return new PrintTextDef(function.Name);
                        }
                    }
                }
            }
            return null;
        }
    }
}
