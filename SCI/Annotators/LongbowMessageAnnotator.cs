using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // TODO: detect and annotate Print wrappers like the localprocs in rm390

    static class LongbowMessageAnnotator
    {
        public static void Run(Game game, TextMessageFinder messageFinder)
        {
            AnnotatePrintFunctions(game, messageFinder);
            AnnotateConversation(game, messageFinder);
            AnnotateLookStr(game, messageFinder);
        }

        static void AnnotatePrintFunctions(Game game, TextMessageFinder messageFinder)
        {
            var printProcs = new List<string>();
            printProcs.Add(game.GetExport(13, 1));
            printProcs.Add(game.GetExport(13, 4));
            printProcs.Add(game.GetExport(13, 5));

            // detect global wrappers (are there any?)
            var printWrapperProcs = from s in game.Scripts
                                    from p in s.Procedures
                                    where s.Exports.ContainsValue(p.Name) &&
                                          IsPrintWrapperFunction(p, printProcs)
                                    select p.Name;
            printProcs.AddRange(printWrapperProcs);

            // annotate all calls to print procs and global/local wrapper procs
            foreach (var script in game.Scripts)
            {
                // detect local print procs
                var localPrintProcs = (from p in script.Procedures
                                       where IsPrintWrapperFunction(p, printProcs)
                                       select p.Name).ToList();

                foreach (var node in script.Root)
                {
                    if (node.Children.Count >= 3 &&
                        node.At(1) is Integer &&
                        node.At(2) is Integer &&
                        (printProcs.Contains(node.At(0).Text) ||
                         localPrintProcs.Contains(node.At(0).Text)))
                    {
                        var message = messageFinder.GetMessage(node.At(1).Number, node.At(2).Number);
                        if (message != null)
                        {
                            node.At(0).Annotate(message.Text.QuoteMessageText());
                        }
                    }
                }
            }
        }

        static bool IsPrintWrapperFunction(Function function, IReadOnlyList<string> printProcs)
        {
            // only evaluating the top-level code statements.
            // if function calls printProc &rest or printProc param1 param2 then it's a wrapper
            foreach (var node in function.Code)
            {
                if (printProcs.Contains(node.At(0).Text))
                {
                    if (node.At(1).Text == "&rest")
                    {
                        return true;
                    }
                    if (function.Parameters.Count >= 2)
                    {
                        if (node.At(1).Text == function.Parameters[0].Name &&
                            node.At(2).Text == function.Parameters[1].Name)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //
        // Conversations are interesting. it involves proc851_0 and local arrays.
        //
        //   (proc851_0 integer @local10 ...
        //   (proc851_0 @local10 ...
        //
        // local10 will be an array and the first two elements will be modNum
        // and textNum. There may be more elements, they influence formatting.
        //

        static void AnnotateConversation(Game game, TextMessageFinder messageFinder)
        {
            var conversationFunction = game.GetExport(851, 0);

            foreach (var script in game.Scripts)
            {
                // detect local print procs
                var localConverseProcs = (from p in script.Procedures
                                          where !script.Exports.ContainsValue(p.Name) &&
                                                IsConversationWrapperFunction(p, conversationFunction)
                                          select p.Name).ToList();

                foreach (var node in script.Root)
                {
                    if (node.Children.Count >= 2 &&
                        (node.At(0).Text == conversationFunction ||
                         localConverseProcs.Contains(node.At(0).Text)))
                    {
                        // find the first address-of node, ignoring integers
                        string localName = null;
                        for (int i = 1; i < node.Children.Count; ++i)
                        {
                            var child = node.Children[i];
                            if (child is AddressOf)
                            {
                                localName = child.At(0).Text;
                                break;
                            }
                            // abort if we hit a non-integer
                            if (!(child is Integer))
                            {
                                break;
                            }
                        }
                        if (localName == null) continue;

                        var local = script.Locals.Values.FirstOrDefault(l => l.Name == localName);
                        if (local == null) continue;

                        // there are some strings, ignore them
                        if (!(local.Values[0] is int) || !(local.Values[1] is int)) continue;

                        int modNum = (int)local.Values[0];
                        int textNum = (int)local.Values[1];
                        var message = messageFinder.GetMessage(modNum, textNum);
                        if (message != null)
                        {
                            node.At(0).Annotate(message.Text.QuoteMessageText());
                        }
                    }
                }
            }
        }

        static bool IsConversationWrapperFunction(Function function, string conversationFunction)
        {
            if (function.Parameters.Count == 0) return false;

            foreach (var node in function.Code)
            {
                if (conversationFunction == node.At(0).Text)
                {
                    if (node.At(2).Text == function.Parameters[0].Name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //
        // lookStr
        //

        static void AnnotateLookStr(Game game, TextMessageFinder messageFinder)
        {
            var rooms = game.GetRooms();
            foreach (var script in rooms.Keys)
            {
                var lookStrs = from o in script.Objects
                               from p in o.Properties
                               where p.Name == "lookStr"
                               select p;
                foreach (var lookStr in lookStrs)
                {
                    if (lookStr.ValueNode is Integer && lookStr.ValueNode.Number > 0)
                    {
                        int modNum = script.Number + 1000;
                        int textNum = lookStr.ValueNode.Number;
                        var message = messageFinder.GetMessage(modNum, textNum);
                        if (message != null)
                        {
                            lookStr.ValueNode.Annotate(message.Text.QuoteMessageText());
                        }
                    }
                }
            }
        }
    }
}
