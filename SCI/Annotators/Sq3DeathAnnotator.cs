using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    static class Sq3DeathAnnotator
    {
        public static void Run(Game game)
        {
            string deathProc = game.GetExport(0, 17); // EgoDead
            Dictionary<int, string> deathMessages = GetDeathMessages(game);

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (node.Children.Count == 5 &&
                    node.At(0).Text == deathProc &&
                    node.At(4) is Integer)
                {
                    int number = node.At(4).Number;
                    if (!deathMessages.ContainsKey(number))
                    {
                        number = 0; // default message
                    }
                    string text;
                    if (deathMessages.TryGetValue(number, out text))
                    {
                        node.At(0).Annotate(text.QuoteMessageText());
                    }
                }
            }
        }

        static Dictionary<int, string> GetDeathMessages(Game game)
        {
            var deathMessages = new Dictionary<int, string>();
            Object gameObject = game.GetExportedObject(0, 0);
            Function sq3Doit = gameObject.Methods.FirstOrDefault(m => m.Name == "doit");
            if (sq3Doit == null) return deathMessages;

            var deathGlobal = game.GetGlobal(188);
            if (deathGlobal == null) return deathMessages;

            foreach (var node in sq3Doit.Node)
            {
                if (node.At(0).Text == "switch" &&
                    node.At(1).Text == deathGlobal.Name)
                {
                    for (int i = 2; i < node.Children.Count; i++)
                    {
                        // (#    (= global320 {caption}) (= global259 {message}))
                        // ...
                        // (else (= global320 {caption}) (= global259 {message}))
                        var case_ = node.At(i);
                        if (case_.At(2).At(2) is String)
                        {
                            int number = 0; // zero for default message
                            if (case_.At(0) is Integer)
                            {
                                number = case_.At(0).Number;
                            }
                            deathMessages[number] = case_.At(2).At(2).Value.ToString();
                        }
                    }
                    break;
                }
            }
            return deathMessages;
        }
    }
}
