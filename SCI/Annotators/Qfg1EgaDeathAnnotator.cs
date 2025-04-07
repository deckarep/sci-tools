using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    static class Qfg1EgaDeathAnnotator
    {
        public static void Run(Game game, TextMessageFinder messageFinder)
        {
            string deathProc = game.GetExport(0, 1); // EgoDead

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (node.At(0).Text == deathProc &&
                    node.At(1) is Integer &&
                    node.At(2) is Integer)
                {
                    var message = messageFinder.GetMessage(node.At(1).Number, node.At(2).Number);
                    if (message != null)
                    {
                        node.At(0).Annotate(message.Text.QuoteMessageText());
                    }
                }
            }
        }
    }
}
