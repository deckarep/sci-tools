using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // Only KQ5 floppy can be message annotated because the CD version doesn't
    // have a correlation between text messages and audio. Text messages were
    // abandoned, and though the old resources exist, the CD Say function only
    // takes audio IDs and not text IDs. Even if there were a way to map between
    // them, they're too different, almost every audio message i've grepped
    // doesn't appear in the text resources.
    //
    // KQ5 prints a "drop cap" letter at the start of most messages. "Drop cap"
    // is the typography term for the fancy big first letter.
    // Originally, they added this to the Interface system script 255 as export 6,
    // but then moved it to KQ5 script 759 in later floppy versions (localized).
    // I don't have any original source for this function so I don't know its name,
    // so I call it PrintDC and rename it with ExportRenamer.

    static class Kq5FloppyMessageAnnotator
    {
        public static void Run(Game game, bool isEarly, TextMessageFinder messageFinder)
        {
            // i think the difference is that the second one doesn't take a
            // view for drawing a face. i didn't look closely, but the code
            // is similar with one less param. didn't see that 255 export in
            // original scripts and it's not in CD version.
            var say = game.GetExport(0, isEarly ? 29 : 28);
            var printDropCap = "PrintDC";

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (node.Children.Count >= 4 &&
                    node.At(0).Text == say &&
                    node.At(2) is Integer &&
                    node.At(2).Number < 1000 &&
                    node.At(3) is Integer)
                {
                    int modNum = node.At(2).Number;
                    int textNum = node.At(3).Number;
                    var message = messageFinder.GetMessage(modNum, textNum);
                    if (message != null)
                    {
                        node.At(0).Annotate(message.Text.QuoteMessageText());
                    }
                    else
                    {
                        node.At(0).Annotate("MISSING MESSAGE");
                    }
                }

                else if (node.Children.Count >= 3 &&
                         (node.At(0).Text == printDropCap) &&
                         node.At(1) is Integer &&
                         node.At(1).Number < 1000 &&
                         node.At(2) is Integer)
                {
                    int modNum = node.At(1).Number;
                    int textNum = node.At(2).Number;
                    var message = messageFinder.GetMessage(modNum, textNum);
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
