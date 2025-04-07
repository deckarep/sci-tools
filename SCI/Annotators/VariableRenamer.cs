using System.Collections.Generic;
using SCI.Language;

namespace SCI.Annotators
{
    static class VariableRenamer
    {
        public static void Run(Node root, Dictionary<string, string> renames)
        {
            if (renames.Count == 0) return;

            // apply the renames
            foreach (var node in root)
            {
                var atom = node as Atom;
                if (atom == null) continue;

                // hack: renaming variables needs to take into account
                // when they get used as selectors with a ":" at the end.
                // this really needs to be in the AST.
                // i am also allowing ? to be companion compatible.
                string text = atom.Text;
                if (text.EndsWith(":") || text.EndsWith("?"))
                {
                    text = text.Substring(0, text.Length - 1);
                }
                string rename;
                if (renames.TryGetValue(text, out rename))
                {
                    if (atom.Text.EndsWith(":"))
                    {
                        rename += ":";
                    }
                    else if (atom.Text.EndsWith("?"))
                    {
                        rename += "?";
                    }
                    atom.SetText(rename);
                }
            }
        }
    }
}
