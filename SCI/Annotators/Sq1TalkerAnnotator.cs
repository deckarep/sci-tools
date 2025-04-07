using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // 1. find all Talker instances
    // 2. annotate their init: and say: calls

    static class Sq1TalkerAnnotator
    {
        public static void Run(Game game, TextMessageFinder messageFinder)
        {
            var talkers = game.GetObjectsBySuper("Talker").Select(o => o.Name).ToList();

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {

                if (node.Children.Count >= 4 && // optimization
                    talkers.Contains(node.At(0).Text))
                {
                    for (int i = 1; i < node.Children.Count; ++i)
                    {
                        int modNumIndex;
                        int numberIndex;
                        if (node.At(i).Text == "init:")
                        {
                            modNumIndex = i + 4;
                            numberIndex = i + 5;
                        }
                        else if (node.At(i).Text == "say:")
                        {
                            modNumIndex = i + 1;
                            numberIndex = i + 2;
                        }
                        else
                        {
                            continue;
                        }

                        if (node.At(modNumIndex) is Integer &&
                            node.At(numberIndex) is Integer)
                        {
                            var message = messageFinder.GetMessage(node.At(modNumIndex).Number, node.At(numberIndex).Number);
                            if (message != null)
                            {
                                node.At(i).Annotate(message.Text.QuoteMessageText());
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
