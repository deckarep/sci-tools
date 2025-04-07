using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // Removes the second language from dual-language games,
    // leaving only English. This makes diffing versions easy.
    //
    // Dual-language strings appear in literals and text resources.
    // English comes first, followed by a two character separator,
    // followed by the second language.
    //
    // KQ5 French examples, both good and bad:
    // GOOD: {"There must be another way!"#F"Il doit y avoir un autre passage"}
    // BAD:  {"Let's move on, Graham!#FAllons, Graham, en avant!"}
    // See ApplyMissingQuote() for details on the bad
    //
    // Assumption: Decompiler already handled object names. Otherwise, decompiler
    // would see the name string "Ego#FEgo" and make an ugly Ego_FEgo symbol.
    // My decompiler uses LanguageUtility to detect this and strip those up front.
    //
    // Remaining:
    // Case sensitivity, there's a " %j " PQ2 literal
    // Get rid of both kinds when a language is found?
    //   KQ5 has unused %F string even though it's a #F game

    static class SecondLanguageRemover
    {
        public static void Run(Game game, Dictionary<int, List<TextMessage>> textMessages)
        {
            // detect dual-language separator from text.0 strings
            List<TextMessage> text0;
            if (!textMessages.TryGetValue(0, out text0)) return;
            string separator = Localization.DetectSeparator(text0.Select(t => t.Text));
            if (separator == null) return;

            // remove second language from text resources
            foreach (TextMessage message in textMessages.Values.SelectMany(v => v))
            {
                message.SetText(Localization.RemoveSecondLanguage(message.Text, separator));
            }

            // remove second language fro\\m string literals
            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                var stringNode = node as String;
                if (stringNode == null) continue;

                int index = node.Text.IndexOf(separator, System.StringComparison.OrdinalIgnoreCase);
                if (index != -1)
                {
                    // strip the outer {} or ""
                    string innerText = node.Text.Substring(1, node.Text.Length - 2);

                    // chop off everything after language separator
                    string english = innerText.Substring(0, index - 1);

                    // kq5 hack (possibly others)
                    english = Localization.ApplyMissingQuote(english);

                    string result = node.Text[0] + english + node.Text[node.Text.Length - 1];

                    stringNode.SetText(result);
                }
            }
        }
    }
}
