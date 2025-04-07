using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    static class Sq6DeathAnnotator
    {
        public static void Run(Game game, MessageFinder messageFinder)
        {
            string deathFunction = game.GetExport(666, 0);

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (node.At(0).Text == deathFunction &&
                    node.At(1) is Integer)
                {
                    // if seq is 17 then it may be randomly changed to 21 or 22
                    int seq = node.At(1).Number;
                    var message = messageFinder.GetFirstMessage(666, 666, 3, 0, 0, seq);
                    if (message != null)
                    {
                        node.At(0).Annotate(message.Text.QuoteMessageText());
                    }
                }
            }
        }
    }
}
