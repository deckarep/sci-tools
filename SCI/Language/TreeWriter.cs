using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SCI.Language
{
    // Writes a script file from a tree.
    // Also writes an entire game's script files.

    public static class TreeWriter
    {
        public static void Write(Game game, string scriptDirectory = null)
        {
            scriptDirectory = scriptDirectory ?? game.ScriptDirectory;

            foreach (var script in game.Scripts)
            {
                int estimatedNewScriptLength = script.OriginalText.Length * 5 / 4;
                var output = new StringBuilder(estimatedNewScriptLength);
                Write(script.Root, output);
                string scriptText = output.ToString();
                // this check made more sense back when the annotators were just
                // post-processing companion; i ran them all the time with minor
                // updates that would change a subset of files. now that they're
                // used against fresh results from my decompiler, the results are
                // almost always different. maybe there's still a scenario where
                // someone's ssd will appreciate this.
                if (scriptText != script.OriginalText)
                {
                    File.WriteAllText(Path.Combine(scriptDirectory, script.FileName), scriptText);
                }
            }
        }

        public static void Write(Node root, string scriptFile)
        {
            var output = new StringBuilder();
            Write(root, output);
            File.WriteAllText(scriptFile, output.ToString());
        }

        public static void Write(Node node, StringBuilder output)
        {
            var lineAnnotations = new List<string>();
            Write(node, lineAnnotations, output);
        }

        static void Write(Node node, List<string> lineAnnotations, StringBuilder output)
        {
            if (node.Annotations != null)
            {
                lineAnnotations.AddRange(node.Annotations);
            }

            if (node is Root)
            {
                foreach (var child in node.Children)
                {
                    Write(child, lineAnnotations, output);
                }
            }
            else if (node is AddressOf)
            {
                WriteTrivias(node.LeftTrivia, lineAnnotations, output);
                output.Append(node.Text);
                WriteTrivias(node.RightTrivia, lineAnnotations, output);
                foreach (var child in node.Children)
                {
                    Write(child, lineAnnotations, output);
                }
            }
            else if (node is Collection)
            {
                var collection = node as Collection;

                WriteTrivias(collection.LeftTrivia, lineAnnotations, output);
                output.Append(collection is List ? '(' : '[');
                WriteTrivias(collection.RightTrivia, lineAnnotations, output);

                foreach (var child in collection.Children)
                {
                    Write(child, lineAnnotations, output);
                }

                WriteTrivias(collection.EndLeftTrivia, lineAnnotations, output);
                output.Append(collection is List ? ')' : ']');
                WriteTrivias(collection.EndRightTrivia, lineAnnotations, output);
            }
            else
            {
                WriteTrivias(node.LeftTrivia, lineAnnotations, output);
                output.Append(node.Text);
                WriteTrivias(node.RightTrivia, lineAnnotations, output);
            }
        }

        static void WriteTrivias(IEnumerable<Trivia> trivias, List<string> listAnnotations, StringBuilder output)
        {
            foreach (var trivia in trivias)
            {
                // if there are line comments queued then ignore any existing
                // comment on this line because it's about to be overwritten.
                // once a newline is reached, write out the line comments,
                // then clear the list.
                if (listAnnotations.Count > 0)
                {
                    if (trivia.Type == TriviaType.Comment)
                    {
                        continue;
                    }
                    if (trivia.Type == TriviaType.NewLine)
                    {
                        WriteLineComments(listAnnotations, output);
                        listAnnotations.Clear();
                    }
                }

                output.Append(trivia.Text);
            }
        }

        static void WriteLineComments(List<string> lineAnnotations, StringBuilder output)
        {
            // whitespace may have been written by previous trivia
            // so backspace until there is none
            while (output.Length > 0 && char.IsWhiteSpace(output[output.Length - 1]))
            {
                output.Length--;
            }

            // ignore blank annotations. if all were blank then do nothing.
            lineAnnotations.RemoveAll(a => string.IsNullOrWhiteSpace(a));
            if (!lineAnnotations.Any())
            {
                return;
            }

            output.Append(" ; ");
            for (int i = 0; i < lineAnnotations.Count; i++)
            {
                if (i != 0)
                {
                    output.Append(", ");
                }
                foreach (var c in lineAnnotations[i])
                {
                    // comments could have newlines in them if a {string-literal} with
                    // a newline ended up in a comment. don't allow in an eol-comment.
                    // this started showing up when annotating companion's asm blocks.
                    if (c != '\r' && c != '\n')
                    {
                        output.Append(c);
                    }
                }
            }
        }
    }
}
