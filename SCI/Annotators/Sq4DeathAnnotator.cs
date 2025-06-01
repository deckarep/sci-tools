using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // Sq4DeathAnnotator is for floppy. Sq4CDMessageAnnotator handles CD.

    static class Sq4DeathAnnotator
    {
        public static void Run(Game game, TextMessageFinder messageFinder)
        {
            string deathProc = game.GetExport(0, 10); // EgoDead
            Dictionary<int, string> deathMessages = GetDeathMessages(game, messageFinder);

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                // (EgoDead)      message zero
                // (EgoDead *)    message zero
                // (EgoDead * #)  message #
                if (node.At(0).Text == deathProc &&
                    (node.Children.Count < 3 || node.At(2) is Integer))
                {
                    int number = 0; // zero for default message
                    if (node.At(2) is Integer)
                    {
                        number = node.At(2).Number;
                    }
                    string text;
                    if (deathMessages.TryGetValue(number, out text))
                    {
                        node.At(0).Annotate(text.QuoteMessageText());
                    }
                }
            }
        }

        static Dictionary<int, string> GetDeathMessages(Game game, TextMessageFinder messageFinder)
        {
            var deathMessages = new Dictionary<int, string>();
            Object rm900 = game.GetExportedObject(900, 0);
            if (rm900 == null) return deathMessages;
            Function rm900init = rm900.Methods.FirstOrDefault(m => m.Name == "init");
            if (rm900init == null) return deathMessages;

            var deathGlobal = game.GetGlobal(187);
            if (deathGlobal == null) return deathMessages;

            foreach (var node in rm900init.Node)
            {
                if (node.At(0).Text == "switch" &&
                    node.At(1).Text == deathGlobal.Name)
                {
                    for (int i = 2; i < node.Children.Count; i++)
                    {
                        // English and some localizations:
                        // (# {message})
                        // Other localizations:
                        // (# (localproc modNum textNum))
                        var case_ = node.At(i);
                        if (case_.At(0) is Integer &&
                            case_.At(1) is String)
                        {
                            deathMessages[case_.At(0).Number] = case_.At(1).Value.ToString();
                        }
                        else if (case_.At(0) is Integer &&
                                (case_.At(1).At(0).Text.StartsWith("localproc")) &&
                                (case_.At(1).At(1) is Integer) &&
                                (case_.At(1).At(2) is Integer))
                        {
                            int modNum = case_.At(1).At(1).Number;
                            int textNum = case_.At(1).At(2).Number;
                            var message = messageFinder.GetMessage(modNum, textNum);
                            if (message != null)
                            {
                                deathMessages[case_.At(0).Number] = message.Text;
                            }
                        }
                    }
                    break;
                }
            }
            return deathMessages;
        }
    }
}
