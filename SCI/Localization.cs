using System;
using System.Collections.Generic;
using System.Linq;

// Localization utility functions that get used by decompiler and annotators

namespace SCI
{
    static class Localization
    {
        // ignore case, because Japanese games will use #J and #j in the same game.
        static IReadOnlyCollection<string> KnownSeparators = new string[]
        {
            "%F",
            "%G",
            "%J",
            "#F",
            "#G",
            "#I",
            "#J",
            "#S",
        };

        public static string DetectSeparator(IEnumerable<string> strings, int minimum = 3)
        {
            var totals = new Dictionary<string, int>();
            foreach (var s in strings)
            {
                foreach (var sep in KnownSeparators)
                {
                    int index = s.IndexOf(sep, StringComparison.OrdinalIgnoreCase);
                    if (index > 0)
                    {
                        if (!totals.ContainsKey(sep))
                        {
                            totals[sep] = 0;
                        }
                        totals[sep]++;
                    }
                }
            }

            string detectedSep = null;
            int max = 0;
            foreach (var kv in totals)
            {
                if (kv.Value > max)
                {
                    detectedSep = kv.Key;
                    max = kv.Value;
                }
            }

            return (max >= minimum) ? detectedSep : null;
        }

        public static string RemoveSecondLanguage(string text, string separator)
        {
            int index = text.IndexOf(separator, StringComparison.OrdinalIgnoreCase);
            if (index != -1)
            {
                text = text.Substring(0, index);
                text = ApplyMissingQuote(text);

                return text;
            }
            return text;
        }

        // KQ5 French has wrong quotes in lots of messages within scripts:
        //      {"Let's move on, Graham!#FAllons, Graham, en avant!"}
        // They quoted the entire string instead of quoting each language string.
        // This causes the game to not display the trailing quote in English and
        // not display the leading quote in French. This thwarts diffing against
        // the English version, so I detect and add the missing trailing quote.
        public static string ApplyMissingQuote(string s)
        {
            if (s.StartsWith("\"") &&
                !s.EndsWith("\"") &&
                (s.Count(c => c == '\"') % 2) == 1)
            {
                return s + "\"";
            }
            return s;
        }
    }
}
