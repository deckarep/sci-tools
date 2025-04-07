using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    static class Sq5DeathAnnotator
    {
        public static void Run(Game game, MessageFinder messageFinder)
        {
            string deathFunction = game.GetExport(0, 9);
            string betaFunction = game.GetExport(0, 7);
            bool isBeta = (betaFunction != null);

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (node.At(0).Text == deathFunction &&
                    (node.At(1) is Integer || node.At(1) is Nil))
                {
                    // optional param
                    int deathNumber = 1;
                    if (node.At(1) is Integer)
                    {
                        deathNumber = node.At(1).Number;
                    }

                    int modNum = 20;
                    int noun;
                    int seq;
                    if (deathNumber < (isBeta ? 37 : 36))
                    {
                        noun = 2;
                        seq = deathNumber;
                    }
                    else
                    {
                        noun = 1;
                        seq = deathNumber - (isBeta ? 36 : 35);
                    }

                    var message = messageFinder.GetFirstMessage(modNum, modNum, noun, 0, 0, seq);
                    if (message != null)
                    {
                        node.At(0).Annotate(message.Text.QuoteMessageText());
                    }
                }
            }
        }
    }
}
