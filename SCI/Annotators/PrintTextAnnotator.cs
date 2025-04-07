using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // (Print  modNum textNum ...
    // (Printf modNum textNum ...
    // (Format @variable modNum textNum ... [ kernel function ]
    // (Display modNum textNum ...
    // (GetFarText modNum textNum ...
    //
    // (single-message-exported-function) where the contents are just (Print modNum textNum)
    //
    // (single-message-local-function) where the contents are just (Print &rest)

    public class PrintTextDef // HACK: i re-used this class for passing SayProcs to SayAnnotator
    {
        public string Name;
        public int ScriptNumber;
        public int ExportNumber;
        public int ParamIndex;

        public PrintTextDef(string name, int paramIndex = 0) { Name = name; ParamIndex = paramIndex; }
        public PrintTextDef(int script, int export, int paramIndex = 0) { ScriptNumber = script; ExportNumber = export; ParamIndex = paramIndex; }
    }

    static class PrintTextAnnotator
    {
        public static void Run(Game game, TextMessageFinder messageFinder, IReadOnlyCollection<PrintTextDef> moreGlobalFunctions = null)
        {
            // functions that take modNum and textNum as their first two params
            var globalFunctions = new List<PrintTextDef>();
            globalFunctions.Add(new PrintTextDef("Display")); // kernel function
            globalFunctions.Add(new PrintTextDef("GetFarText")); // kernel function
            globalFunctions.Add(new PrintTextDef("Print"));
            globalFunctions.Add(new PrintTextDef("Printf"));
            globalFunctions.Add(new PrintTextDef("PrintSplit")); // sq3 amiga german, at least
            if (moreGlobalFunctions != null)
            {
                globalFunctions.AddRange(moreGlobalFunctions);
            }

            // lookup names
            foreach (var globalFunction in globalFunctions)
            {
                if (globalFunction.Name == null)
                {
                    globalFunction.Name = game.GetExport(globalFunction.ScriptNumber, globalFunction.ExportNumber);
                }
            }

            var singleMessageFunctions = GetSingleMessageFunctions(game, messageFinder);

            foreach (var script in game.Scripts)
            {
                if (IsBadScript(script)) continue;

                var allFunctions = GetLocalMessageFunctions(script, globalFunctions);
                allFunctions.AddRange(globalFunctions);
                var allFunctionNames = new HashSet<string>(allFunctions.Select(f => f.Name));

                foreach (var node in script.Root)
                {
                    // print / display
                    if (node.Children.Count >= 3 && // optimization
                        allFunctionNames.Contains(node.At(0).Text))
                    {
                        var printFunction = allFunctions.First(f => f.Name == node.At(0).Text);
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

                    // format
                    if (node.Children.Count >= 4 && // optimization
                        node.At(0).Text == "Format" &&
                        node.At(2) is Integer &&
                        node.At(3) is Integer)
                    {
                        int modNum = node.At(2).Number;
                        int textNum = node.At(3).Number;
                        var message = messageFinder.GetMessage(modNum, textNum);
                        if (message != null)
                        {
                            node.At(0).Annotate(message.Text.QuoteMessageText());
                        }
                    }

                    // (single-message-function)
                    if (node.Children.Count == 1 &&
                        singleMessageFunctions.ContainsKey(node.At(0).Text) &&
                        node.Next(-1).Text != "procedure") // ignore declaration
                    {
                        node.At(0).Annotate(singleMessageFunctions[node.At(0).Text]);
                    }
                }
            }
        }

        // GetLocalMessageFunctions() is now better than this.
        // TODO: make this do PrintTextDef so that it finds CB proc0_1 (Say), instead of setting that manually
        public static Dictionary<string, string> GetSingleMessageFunctions(Game game, TextMessageFinder messageFinder)
        {
            var printFunctions = new List<string>();
            printFunctions.Add("Print");
            printFunctions.Add("Printf");

            var singleMessageFunctions = new Dictionary<string, string>();

            foreach (var script in game.Scripts)
            {
                foreach (var export in script.Exports.Values)
                {
                    var procedure = script.Procedures.FirstOrDefault(p => p.Name == export);
                    if (procedure != null)
                    {
                        var code = procedure.Code.FirstOrDefault();
                        if (code != null &&
                            code.Children.Count == 3 &&
                            printFunctions.Contains(code.At(0).Text) &&
                            code.At(1) is Integer &&
                            code.At(2) is Integer)
                        {
                            int modNum = code.At(1).Number;
                            int number = code.At(2).Number;
                            var message = messageFinder.GetMessage(modNum, number);
                            if (message != null)
                            {
                                singleMessageFunctions.Add(procedure.Name, message.Text.QuoteMessageText());
                            }
                        }
                    }
                }
            }

            return singleMessageFunctions;
        }

        // this is supposed to find local procedures that call global message functions.
        // those local procs are themselves message functions, so let's annotate them.
        // we're looking for calls to a global function followed by &rest.
        // but, that rest might have an offset associated with it.
        // if parameters are explicitly mentioned then we have to detect those from
        // where rest starts and return that in the function definition.
        //
        // for example:
        // localproc param1 param2
        //   Display &rest param1 param2
        //
        // we need to set param index at 2 (param3) for this local proc
        static List<PrintTextDef> GetLocalMessageFunctions(Script script, IEnumerable<PrintTextDef> globalFunctions)
        {
            var localFunctions = new List<PrintTextDef>();
            foreach (var procedure in script.Procedures)
            {
                // locals only
                if (script.Exports.ContainsValue(procedure.Name)) continue;

                foreach (var node in procedure.Node)
                {
                    if (node.At(1).Text == "&rest" &&
                        globalFunctions.Any(gf => gf.Name == node.At(0).Text))
                    {
                        // this is a local message function.
                        // count the number of defined parameters so we
                        // know when "&rest" starts so we know which
                        // parameters are the text tuple.
                        localFunctions.Add(new PrintTextDef(procedure.Name, procedure.Parameters.Count));
                        break;
                    }
                    else if (node.At(2).Text == "&rest" &&
                             procedure.Parameters.Any(p => p.Name == node.At(1).Text) &&
                             globalFunctions.Any(gf => gf.Name == node.At(0).Text))
                    {
                        // (Print param &rest ...)
                        var param = procedure.Parameters.First(p => p.Name == node.At(1).Text);
                        int paramIndex = procedure.Parameters.IndexOf(param);
                        localFunctions.Add(new PrintTextDef(procedure.Name, paramIndex));
                        break;
                    }
                    else if (node.Children.Count >= 3 && procedure.Parameters.Count >= 2)
                    {
                        // try to find a simple (Print param1 param2 ...).
                        // longbow room 390.
                        // TODO: make this smarter so that it handles other params.
                        var globalFunction = globalFunctions.FirstOrDefault(f => f.Name == node.At(0).Text);
                        if (globalFunction != null &&
                            node.At(1).Text == procedure.Parameters[0].Name &&
                            node.At(2).Text == procedure.Parameters[1].Name)
                        {
                            localFunctions.Add(new PrintTextDef(procedure.Name, 0));
                            break;
                        }
                    }
                }
            }
            return localFunctions;
        }

        public static bool IsBadScript(Script script)
        {
            return script.Objects.Any(o => o.Name == "sysLogger");
        }
    }
}
