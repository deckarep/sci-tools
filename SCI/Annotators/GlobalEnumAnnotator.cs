using System.Collections.Generic;
using SCI.Language;

namespace SCI.Annotators
{
    // This class used to be bigger, now it's a thin wrapper over ConstantFinder.

    static class GlobalEnumAnnotator
    {
        // comparisons
        // (== global int) or (== int global), but for all operators
        // (OneOf global int...)
        // (switch global (int ...) (int ...)

        // assignment
        // (= global int)

        public static void Run(Game game, int globalNumber, IReadOnlyDictionary<int, string> values)
        {
            string globalName = game.GetGlobal(globalNumber).Name;
            Run(game, globalName, values);
        }

        public static void Run(Game game, string globalName, IReadOnlyDictionary<int, string> values)
        {
            foreach (var function in game.GetFunctions())
            {
                ConstantFinder.Run(function.Node,
                    n => n.Text == globalName,
                    n =>
                    {
                        string valueName;
                        if (values.TryGetValue(n.Number, out valueName))
                        {
                            n.Annotate(valueName);
                        }
                    });
            }
        }
    }
}