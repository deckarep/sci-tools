using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    static class DoAudioMessageAnnotator
    {
        public static void Run(Game game, MessageFinder messageFinder)
        {
            foreach (var node in game.GetFunctions().SelectMany(f => f.Node))
            {
                if (node.At(0).Text == "DoAudio" &&
                    (node.At(1).Text == "audPLAY" || node.At(1).Text == "audSTOP") &&
                    node.At(2) is Integer &&
                    node.At(3) is Integer &&
                    node.At(4) is Integer &&
                    node.At(5) is Integer &&
                    node.At(6) is Integer)
                {
                    int modNum = node.At(2).Number;
                    int noun = node.At(3).Number;
                    int verb = node.At(4).Number;
                    int cond = node.At(5).Number;
                    int seq = node.At(6).Number;
                    var message = messageFinder.GetFirstMessage(modNum, modNum, noun, verb, cond, seq, false);
                    if (message != null)
                    {
                        node.At(0).Annotate(message.Text.QuoteMessageText());
                    }
                }
            }
        }
    }
}
