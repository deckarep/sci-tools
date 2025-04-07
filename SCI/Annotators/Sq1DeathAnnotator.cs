using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // (proc0_10 940 1 0 700 10), modNum 700 number 10

    static class Sq1DeathAnnotator
    {
        public static void Run(Game game, TextMessageFinder messageFinder)
        {
            string deathFunction = game.GetExport(0, 10);

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (node.At(0).Text == deathFunction &&
                    node.At(4) is Integer &&
                    node.At(5) is Integer)
                {
                    var message = messageFinder.GetMessage(node.At(4).Number, node.At(5).Number);
                    if (message != null)
                    {
                        node.At(0).Annotate(message.Text.QuoteMessageText());
                    }
                }
            }
        }
    }
}
