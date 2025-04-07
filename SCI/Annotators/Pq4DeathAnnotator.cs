using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // (EgoDead noun verb seq cond modNum) ; proc30_0
    // (((ScriptID 32 0) doit: noun verb seq cond modNum) ; ShootHandgun

    static class Pq4DeathAnnotator
    {
        public static void Run(Game game, MessageFinder messageFinder)
        {
            string egoDead = game.GetExport(30, 0);
            foreach (var node in game.GetFunctions().SelectMany(f => f.Node))
            {
                int nounPos;
                int annotatePos;
                if (node.At(0).Text == egoDead)
                {
                    nounPos = 1;
                    annotatePos = 0;
                }
                else if (node.At(0).At(0).Text == "ScriptID" &&
                         node.At(0).At(1) is Integer &&
                         node.At(0).At(1).Number == 32 &&
                         node.At(0).At(2) is Integer &&
                         node.At(0).At(2).Number == 0 &&
                         node.At(1).Text == "doit:")
                {
                    nounPos = 2;
                    annotatePos = 1;
                }
                else
                {
                    continue;
                }

                if (!(node.At(nounPos + 0) is Integer)) continue;
                if (!(node.At(nounPos + 1) is Integer)) continue;
                if (!(node.At(nounPos + 2) is Integer)) continue;
                if (!(node.At(nounPos + 3) is Integer)) continue;
                int noun = node.At(nounPos + 0).Number;
                int verb = node.At(nounPos + 1).Number;
                int cond = node.At(nounPos + 2).Number;
                int seq = node.At(nounPos + 3).Number;
                int modNum;
                if (node.At(nounPos + 4) is Integer)
                {
                    modNum = node.At(nounPos + 4).Number;
                }
                else
                {
                    modNum = seq;
                    seq = 1;
                }
                var message = messageFinder.GetFirstMessage(modNum, modNum, noun, verb, cond, seq, false);
                if (message != null)
                {
                    node.At(annotatePos).Annotate(message.Text.QuoteMessageText());
                }
            }
        }
    }
}
