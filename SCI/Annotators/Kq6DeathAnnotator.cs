using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // (proc0_1 deathNumber), deathNumber is cond in message 916

    static class Kq6DeathAnnotator
    {
        public static void Run(Game game, MessageFinder messageFinder)
        {
            string deathFunction = game.GetExport(0, 1);

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (node.At(0).Text == deathFunction &&
                    node.At(1) is Integer)
                {
                    var message = messageFinder.GetFirstMessage(916, 916, 0, 0, node.At(1).Number, 1);
                    if (message != null)
                    {
                        node.At(0).Annotate(message.Text.QuoteMessageText());
                    }
                }
            }
        }
    }
}
