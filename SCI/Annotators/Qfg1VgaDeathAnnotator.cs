using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    static class Qfg1VgaDeathAnnotator
    {
        public static void Run(Game game, MessageFinder messageFinder)
        {
            string deathProc = game.GetExport(814, 0);

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (node.At(0).Text == deathProc &&
                    node.At(1) is Integer)
                {
                    var message = messageFinder.GetFirstMessage(815, 815, 1, 0, node.At(1).Number, 1);
                    if (message != null)
                    {
                        node.At(0).Annotate(message.Text.QuoteMessageText());
                    }
                }
            }
        }
    }
}
