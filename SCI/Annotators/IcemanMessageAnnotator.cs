using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // PrintTextAnnotator handles most of Iceman but there are a few
    // wrapper procedures that take a lot of parameters but all end
    // in modNum textNum.
    //
    // from my notes from the previous annotator:
    //
    //   openingScene (1) localproc_0612 8 modNum textNum
    //   volleyRm (3) proc3_7 modNum textNum
    //   volleyRm (3) proc3_8 num modNum textNum
    //   n396 proc396_0 num modNum textNum
    //   n821 proc821_0 num num num modNum textNum (i renamed this to Death)

    static class IcemanMessageAnnotator
    {
        public static void Run(Game game, TextMessageFinder messageFinder)
        {
            // global functions that take modNum and textNum as their last two params
            var globalFunctions = new List<string>();
            globalFunctions.Add(game.GetExport(821, 0)); // Death

            // detect the rest. Death doesn't decompile.
            var formatPrintFunctions = from s in game.Scripts
                                       from p in s.Procedures
                                       where s.Exports.ContainsValue(p.Name) &&
                                             IsFormatPrintFunction(p)
                                       select p.Name;
            globalFunctions.AddRange(formatPrintFunctions);

            foreach (var script in game.Scripts)
            {
                // detect local format print functions
                var localFunctions = (from p in script.Procedures
                                      where IsFormatPrintFunction(p)
                                      select p.Name).ToList();

                foreach (var node in script.Root)
                {
                    if (node.Children.Count >= 3 &&
                        node.Children[node.Children.Count - 2] is Integer &&
                        node.Children[node.Children.Count - 1] is Integer &&
                        (globalFunctions.Contains(node.At(0).Text) ||
                         localFunctions.Contains(node.At(0).Text)))
                    {
                        int modNum = node.Children[node.Children.Count - 2].Number;
                        int textNum = node.Children[node.Children.Count - 1].Number;
                        var message = messageFinder.GetMessage(modNum, textNum);
                        if (message != null)
                        {
                            node.At(0).Annotate(message.Text.QuoteMessageText());
                        }
                    }
                }
            }
        }

        static bool IsFormatPrintFunction(Function function)
        {
            // only evaluating the top-level code statements.
            string textParam = null;
            foreach (var node in function.Code)
            {
                if (node.At(0).Text == "Format" &&
                    node.At(2).Text == "&rest")
                {
                    textParam = node.At(1).Text;
                    continue;
                }
                if (node.At(0).Text == "Print" &&
                    node.At(1).Text == textParam)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
