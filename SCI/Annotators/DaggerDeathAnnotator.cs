using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    static class DaggerDeathAnnotator
    {
        // before going to death room, scripts set a global
        // for which death message / animation to display.
        // (= global145 deathNumber)
        //
        // message is 99 1 45 (deathNum + 1) 1

        public static void Run(Game game, MessageFinder messageFinder)
        {
            var deathGlobal = game.GetGlobal(145).Name;

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (node.At(0).Text == "=" &&
                    node.At(1).Text == deathGlobal &&
                    node.At(2) is Integer)
                {
                    int cond = node.At(2).Number + 1;
                    var message = messageFinder.GetFirstMessage(99, 99, 1, 45, cond, 1);
                    if (message != null)
                    {
                        node.At(2).Annotate(message.Text.QuoteMessageText());
                    }
                }
            }
        }
    }
}
