using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // (Print ... # ...) => (Print #at ...)
    //
    // Same approach as DisplayAnnotator.

    class PrintSelectorAnnotator
    {
        public static bool IsEligible(Game game)
        {
            // Print function is later replaced with a Print class
            return game.Scripts.Any(s => s.Number == 255 && s.Exports.ContainsValue("Print"));
        }

        public static void Run(Game game, string[] selectors, IEnumerable<PrintTextDef> moreFunctions = null)
        {
            var functions = new List<PrintTextDef>
            {
                new PrintTextDef("Print", 0),
                new PrintTextDef("PrintDC", 0) // i made up this name
            };
            if (moreFunctions != null)
            {
                foreach (var f in moreFunctions)
                {
                    functions.Add(f);
                }
            }
            var functionMap = new Dictionary<string, PrintTextDef>(functions.Count);
            foreach (var function in functions)
            {
                if (function.Name == null)
                {
                    function.Name = game.GetExport(function.ScriptNumber, function.ExportNumber);
                }
                functionMap.Add(function.Name, function);
            }

            // build selector map
            var selectorMap = new Dictionary<int, string>(Ops.Count);
            for (int i = 0; i < selectors.Length; i++)
            {
                if (Ops.ContainsKey(selectors[i]))
                {
                    selectorMap.Add(i, selectors[i]);
                }
            }
            // if we don't know all the selectors, abort.
            // (extra check for SCI0 early selectors with two entries for each)
            if (selectorMap.Count != Ops.Count && selectorMap.Count != (Ops.Count * 2)) return;

            foreach (var function in game.GetFunctions())
            {
                foreach (var node in function.Node)
                {
                    PrintTextDef printFunction;
                    if (node.Children.Count >= 2  &&
                        functionMap.TryGetValue(node.At(0).Text, out printFunction))
                    {
                        Annotate(function, printFunction.ParamIndex, node, selectorMap);
                    }
                }
            }
        }

        enum StringType { Unknown, Near, Far }

        private static void Annotate(Function function, int startIndex, Node node, IReadOnlyDictionary<int, string> selectorMap)
        {
            var type = StringType.Unknown;
            var param1 = node.At(startIndex + 1);
            var param2 = node.At(startIndex + 2);
            var param3 = node.At(startIndex + 3);

            if (param1 is Integer)
            {
                type = StringType.Far;
            }
            else if (param1 is String || param2 is AddressOf)
            {
                type = StringType.Near;
            }
            else if (param2 is Integer && selectorMap.ContainsKey(param2.Number))
            {
                type = StringType.Near;
            }
            else if (param3 is Integer && selectorMap.ContainsKey(param3.Number))
            {
                type = StringType.Far;
            }

            // if we couldn't figure it out, forget it
            if (type == StringType.Unknown) return;

            // attempt to solve this by building a map.
            // only apply these changes if there are no problems.
            var map = new Dictionary<Node, string>();
            for (int i = startIndex + (type == StringType.Near ? 2 : 3); i < node.Children.Count; i++)
            {
                // all ops have to be integers (but skip &rest)
                var param = node.Children[i];
                if (param.Text == "&rest") continue;
                if (!(param is Integer)) return;

                // all ops have to be known
                string selector;
                if (!selectorMap.TryGetValue(param.Number, out selector)) return;

                // there must be enough parameters
                int opParams = Ops[selector];
                if (selector == "dispose")
                {
                    // dispose takes an optional object parameter

                    // if there are no more parameters, great!
                    if (i == node.Children.Count - 1)
                    {
                        opParams = 0;
                    }
                    // if the next parameter is an integer,
                    // it can't belong to dispose.
                    else if (node.Children[i + 1] is Integer)
                    {
                        opParams = 0;
                    }
                    // non-integer parameter? assume it belongs to dispose
                    else
                    {
                        opParams = 1;
                    }
                }
                else if (selector == "icon")
                {
                    // originally always 3 parameters,
                    // then 1 if the first parameters is an object.
                    // but sometimes they kept passing three anyway...

                    // if there are less than three parameters, great!
                    if (!(i + 3 < node.Children.Count))
                    {
                        opParams = 1;
                    }
                    // if parameter 1 is an integer then it's 3
                    else if (node.Children[i + 1] is Integer)
                    {
                        opParams = 3;
                    }
                    // if parameter 2 is a non-selector integer it's 3
                    else if (node.Children[i + 2] is Integer && !selectorMap.ContainsKey(node.Children[i + 2].Number))
                    {
                        opParams = 3;
                    }
                    // if parameter 3 is a non-selector integer it's 3
                    else if (node.Children[i + 3] is Integer && !selectorMap.ContainsKey(node.Children[i + 3].Number))
                    {
                        opParams = 3;
                    }
                    // if parameter 1 is an object then it's 1.
                    // do this after the integer tests because they left the three-param form
                    // even when they shouldn't have, but non-selector integers just got ignored
                    // by the Print loop.
                    else if (node.Children[i + 1] is Atom && function.Script.Objects.Any(o => o.Name == param.Text))
                    {
                        opParams = 1;
                    }
                    // if parameter 2 is an integer and a selector, i guess it's a 1?
                    else if (node.Children[i + 2] is Integer && selectorMap.ContainsKey(node.Children[i + 2].Number))
                    {
                        opParams = 1;
                    }
                    // else i'm guessing it's a 3. odds are good!
                    else
                    {
                        opParams = 3;
                    }
                }

                // there must be enough parameters
                if (!(i + opParams < node.Children.Count)) return;

                map.Add(param, selector);
                i += opParams;
            }

            // apply the changes, everything is good
            foreach (var action in map)
            {
                var param = action.Key;
                var selector = action.Value;
                (param as Integer).SetDefineText("#" + selector);
            }
        }

        static Dictionary<string, int> Ops = new Dictionary<string, int>
        {
            { "mode", 1 },
            { "font", 1 },
            { "width", 1 },
            { "time", 1 },
            { "title", 1 },
            { "at", 2 },
            { "draw", 0 },
            { "edit", 2 },
            { "button", 2 },
            { "icon", 3 }, // 3 numbers OR 1 object (originally always 3)
            { "dispose", 1 }, // optional!
            { "window", 1 },
            { "first", 0 },
            { "setPri", 1 }, // not a real Print op, but kq4 tries to use it
            { "caller", 1 }, // LSL5 Say procedure
        };
    }
}
