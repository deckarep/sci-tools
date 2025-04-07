using System.Linq;
using System.Text;

namespace SCI.Annotators
{
    static class StringExtensions
    {
        public static string SanitizeMessageText(this string text)
        {
            text = text.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ").Replace("  ", " ").Trim();
            if (text.StartsWith("\"") && text.EndsWith("\"") && text.Length >= 2)
            {
                text = text.Substring(1, text.Length - 2).Trim();
            }
            return EscapeUnprintableCharacters(text);
        }

        static string EscapeUnprintableCharacters(string text)
        {
            // optimization
            bool needsEscaping = text.Any(c => !(32 <= c && c <= 126));
            if (!needsEscaping) return text;

            var sb = new StringBuilder(text.Length * 2);
            foreach (var c in text)
            {
                if (!(32 <= c && c <= 126))
                {
                    // \0c, \1d, etc
                    sb.AppendFormat("\\{0:x2}", (byte)c);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string QuoteMessageText(this string text)
        {
            string sanitized = SanitizeMessageText(text);
            if (!string.IsNullOrWhiteSpace(sanitized))
            {
                return string.Format("\"{0}\"", sanitized);
            }
            return "";
        }
    }
}
