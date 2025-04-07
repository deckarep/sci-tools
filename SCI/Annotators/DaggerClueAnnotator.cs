using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // addCluesCode:
    // ((ScriptID 21 0) ... doit: 1030 ...)
    // dropCluesCode:
    // ((ScriptID 21 1) ... doit: 1030 ...)

    static class DaggerClueAnnotator
    {
        public static void Run(Game game, MessageFinder messageFinder)
        {
            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (!(node.Text == "doit:" &&
                      node.Next() is Integer &&
                      node.Parent.At(0).At(0).Text == "ScriptID" &&
                      node.Parent.At(0).At(1).Text == "21" && // lol
                      node.Parent.At(0).At(2) is Integer))
                {
                    continue;
                }

                // sanity check
                int exportNumber = node.Parent.At(0).At(2).Number;
                if (exportNumber != 0 && exportNumber != 1) continue;

                // with the clue number i can get the clue text straight
                // from message resource 20.
                // Noun  Verb  Cond  Seq
                // 1     1     0     1     Sam Augustini
                // 1     1     0     2     Dr. Pippin Carter
                //  ...
                // 4     1     0     1     Hieroglyphs
                // 4     1     0     2     1926
                //  ...
                //
                // NoteBookItem:display draws hieroglyphs for codes > 1088.
                var clueNode = node.Next();
                int clueNumber = clueNode.Number;
                if (clueNumber > 1088) continue;

                // NoteBookItem:display draws text:
                // (= temp0 (/ subject 256))
                // (= temp1 (mod subject 256))
                // (Message msgGET 20 temp0 1 0 temp1 @temp2)
                int modNum = 20;
                int noun = clueNumber / 256; // page 1-4
                int verb = 1;
                int cond = 0;
                int seq = clueNumber % 256;
                var message = messageFinder.GetFirstMessage(modNum, modNum, noun, verb, cond, seq);
                if (message != null)
                {
                    clueNode.Annotate(message.Text.SanitizeMessageText());
                }
            }
        }
    }
}
