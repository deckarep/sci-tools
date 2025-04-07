using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // Regular dialogs are displayed with Print/Printf, which PrintTextAnnotator handles.
    //
    // Character dialogs go through proc0_16.
    //   (proc0_16 characterObject modNum textNum)

    static class Pq3MessageAnnotator
    {
        public static void Run(Game game, TextMessageFinder messageFinder)
        {
            string speechProc = game.GetExport(0, 16);
            string deathProc = game.GetExport(0, 5);

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                // speech
                if (node.At(0).Text == speechProc &&
                    node.At(2) is Integer &&
                    node.At(3) is Integer)
                {
                    int modNum = node.At(2).Number;
                    int textNum = node.At(3).Number;
                    var message = messageFinder.GetMessage(modNum, textNum);
                    if (message != null)
                    {
                        // if it weren't for this tweak i could just pass this
                        // to PrintTextAnnotator and set the param index.

                        // param 1 is often a ScriptID call so i want that annotation
                        // to appear on the line first
                        node.At(2).Annotate(message.Text.QuoteMessageText());
                    }
                }

                // death
                else if (node.At(0).Text == deathProc)
                {
                    int modNum;
                    int textNum;
                    if (node.Children.Count == 2 && node.At(1) is Integer)
                    {
                        modNum = 506;
                        textNum = node.At(1).Number;
                        // i don't know, this was from my old annotator
                        if (textNum <= 5)
                        {
                        }
                        else if (textNum <= 16)
                        {
                            textNum = textNum + 1;
                        }
                        else if (textNum <= 25)
                        {
                            textNum = textNum + 2;
                        }
                        else if (textNum <= 27)
                        {
                            textNum += 3;
                        }
                        else
                        {
                            textNum += 4;
                        }
                    }
                    else if (node.Children.Count == 4 &&
                             node.At(2) is Integer &&
                             node.At(3) is Integer)
                    {
                        modNum = node.At(2).Number;
                        textNum = node.At(3).Number;
                    }
                    else
                    {
                        continue;
                    }
                    var message = messageFinder.GetMessage(modNum, textNum);
                    if (message != null)
                    {
                        node.At(0).Annotate(message.Text.QuoteMessageText());
                    }
                }
            }
        }
    }
}
