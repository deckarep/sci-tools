using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SCI.Resource;

// Writes Script (.sc) text files.
//
// ScriptWriter handles everything except function bodies.
// FunctionWriter handles function bodies.
//
// ScriptWriter is relatively simple, it's just a lot of StringBuilder.
// I only AST'd the function bodies; this isn't a compiler.

namespace SCI.Decompile
{
    class ScriptWriter
    {
        Game game;
        Script script;
        ScriptSummary scriptSummary;
        Symbols symbols;

        StringBuilder output;

        public ScriptWriter(ScriptSummary scriptSummary, Symbols symbols)
        {
            this.game = scriptSummary.Script.Game;
            this.script = scriptSummary.Script;
            this.scriptSummary = scriptSummary;
            this.symbols = symbols;
        }

        public void Write(string scriptFile)
        {
            output = new StringBuilder();
            Write();
            File.WriteAllText(scriptFile, output.ToString());
        }

        void Write()
        {
            // the first line is used by companion as a marker, and it expects most
            // of it to be this specific string, so just do exactly what it does.
            output.AppendLine(";;; Sierra Script 1.0 - (do not remove this comment)");
            output.AppendLine(";;; Decompiled by sluicebox"); // vanity, but also helps me keep track
            output.Append("(script# ");
            output.Append(script.Number);
            output.AppendLine(")");
            output.AppendLine("(include sci.sh)");
            WriteUsings();
            output.AppendLine();

            WriteExports();
            WriteSynonyms();
            WriteLocals();

            // like companion, i'm writing standalone procedures first.
            // procedures that belong to objects appear within them.
            WriteProcedures();
            WriteObjects();
        }

        void WriteUsings()
        {
            foreach (var usingScriptNumber in scriptSummary.UsingScripts.OrderBy(s => s))
            {
                // skip self reference
                if (usingScriptNumber == script.Number) continue;

                var usingScript = game.Scripts.FirstOrDefault(s => s.Number == usingScriptNumber);
                if (usingScript != null)
                {
                    output.Append("(use ");
                    output.Append(symbols.Script(usingScript));
                    output.AppendLine(")");
                }
            }
        }

        void WriteExports()
        {
            if (script.Exports.All(e => e.Type == ExportType.Invalid)) return;

            output.AppendLine("(public");
            for (int i = 0; i < script.Exports.Count; i++)
            {
                var obj = script.GetExportedObject(i);
                if (obj != null)
                {
                    output.Append("\t");
                    output.Append(symbols.Instance(obj));
                    output.Append(" ");
                    output.Append(i);
                    output.AppendLine();
                    continue;
                }
                var proc = script.GetExportedProcedure(i);
                if (proc != null)
                {
                    output.Append("\t");
                    output.Append(proc.Name);
                    output.Append(" ");
                    output.Append(i);
                    output.AppendLine();
                }
            }
            output.AppendLine(")");
            output.AppendLine();
        }

        void WriteSynonyms()
        {
            if (script.Synonyms.Count == 0) return;
            var vocab = script.Game.Vocab;

            output.AppendLine("(synonyms");
            foreach (var synonym in script.Synonyms)
            {
                output.Append("\t(");
                output.Append(vocab.GetPreferredWord(synonym.Group));
                foreach (var group in synonym.Groups)
                {
                    output.Append(" ");
                    output.Append(vocab.GetPreferredWord(group));
                }
                output.AppendLine(")");
            }
            output.AppendLine(")");
            output.AppendLine();
        }

        void WriteLocals()
        {
            if (!script.Locals.Any()) return;

            bool printGlobalNumberComments = (script.Number == 0);
            const int commentGroupSize = 5;

            output.AppendLine("(local");
            for (int i = 0; i < script.Locals.Count; i++)
            {
                // LSCI injects parameter count as local0.
                // I don't want this to appear in decompiler output.
                // Skip printing it, and adjust the index for the
                // global number comments, then restore it.
                int localIndex = i;
                if (script.Game.ScriptFormat == ScriptFormat.LSCI)
                {
                    if (i == 0) continue;
                    localIndex--;
                }

                if (printGlobalNumberComments && (localIndex % commentGroupSize == 0))
                {
                    output.Append("\t; ");
                    output.Append(localIndex);
                    output.AppendLine();
                }

                localIndex = i; // restore, see above
                var local = script.Locals[i];

                // determine if this is an array or a single.
                // consecutive unused locals are treated as arrays.
                bool isArray;
                if (script.Number == 0)
                {
                    // globals are always singles
                    isArray = false;
                }
                else if (scriptSummary.ComplexLocals.Contains(i))
                {
                    // array access makes this an array
                    isArray = true;
                }
                else if (scriptSummary.Locals.Contains(i))
                {
                    // it's a single
                    isArray = false;
                }
                else
                {
                    // unused; treat as an array
                    isArray = true;
                }

                // how big is the array?
                int arraySize = 1;
                if (isArray)
                {
                    // consume all subsequent unreferenced locals
                    for (int j = i + 1; j < script.Locals.Count; j++)
                    {
                        if (!scriptSummary.Locals.Contains(j) &&
                            !scriptSummary.ComplexLocals.Contains(j))
                        {
                            arraySize++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    // skip the consumed locals
                    i += (arraySize - 1);
                }

                // treat a one-element array as a single
                if (arraySize == 1)
                {
                    isArray = false;
                }

                // locate the last non-zero local in an array.
                // if they're all zero then they won't be output.
                // if only some are trailing, then output them.
                // i get confused when SCI Companion skips the terminating zero.
                int lastNonZeroIndex = arraySize - 1; // index into this local array
                for (; lastNonZeroIndex >= 0; lastNonZeroIndex--)
                {
                    var l = script.Locals[localIndex + lastNonZeroIndex];
                    if (!(l.Type == LocalType.Number && l.Value == 0))
                    {
                        break;
                    }
                }

                output.Append("\t");
                if (!isArray)
                {
                    output.Append(symbols.Variable(script, null, VariableType.Local, localIndex));
                    if (lastNonZeroIndex == 0)
                    {
                        output.Append(" = ");
                        WriteLocalValue(local);
                    }
                }
                else
                {
                    output.Append("[");
                    output.Append(symbols.Variable(script, null, VariableType.Local, localIndex));
                    output.Append(" ");
                    output.Append(arraySize);
                    output.Append("]");
                    if (lastNonZeroIndex != -1)
                    {
                        output.Append(" = [");
                        for (int j = 0; j < arraySize; j++)
                        {
                            if (j != 0) output.Append(" ");
                            WriteLocalValue(script.Locals[localIndex + j]);
                        }
                        output.Append("]");
                    }
                }
                output.AppendLine();
            }
            output.AppendLine(")");
            output.AppendLine();
        }

        void WriteLocalValue(Local local)
        {
            if (local.Type == LocalType.String)
            {
                // SCI3 relocations require looking up string pos first
                UInt32 stringPos;
                if (!script.Relocations.TryGetValue(local.Position, out stringPos))
                {
                    stringPos = local.Value;
                }

                var str = script.Strings.First(s => s.Position == stringPos);
                FormatStringLiteral(str.Text, output);
            }
            else if (local.Type == LocalType.Said)
            {
                var said = script.Saids.First(s => s.Position == local.Value);
                output.Append("'");
                output.Append(said.Text);
                output.Append("'");
            }
            else
            {
                output.Append(local.Value.GetSignedOrUnsigned(game.ByteCodeVersion));
            }
        }

        void WriteProcedures()
        {
            foreach (var procedure in script.Procedures.OrderBy(p => p.CodePosition))
            {
                // skip procedures that have objects, i'll do those with methods.
                if (procedure.Object != null) continue;

                // if there's no function summary then skip this; we're in a test mode
                FunctionSummary functionSummary;
                if (!scriptSummary.Functions.TryGetValue(procedure, out functionSummary))
                    continue;

                WriteCompilerErrorIfInvalidTempCount(functionSummary, "");
                output.Append("(procedure (");
                output.Append(symbols.LocalProcedure(script, (int)procedure.CodePosition));
                WriteFunctionHeader(procedure);
                output.Append(")");

                bool emptyFunction = true;
                if (functionSummary.Error != null)
                {
                    output.AppendLine();
                    output.Append("\t; DECOMPILER EXCEPTION: ");
                    output.Append(string.Join(", ", functionSummary.Error.Message.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)));
                    emptyFunction = false;
                }
                var ast = functionSummary.Ast;
                if (ast != null)
                {
                    if (ast.Children.Count != 0)
                    {
                        output.AppendLine();
                        output.Append("\t");
                        var astWriter = new FunctionWriter(output, 1, symbols, procedure);
                        astWriter.WriteTree(ast);
                        emptyFunction = false;
                    }
                }
                else if (functionSummary.Instructions != null)
                {
                    output.AppendLine();
                    output.Append("\t");
                    ZeAssemblyWriter.Write(output, 1, symbols, procedure);
                    emptyFunction = false;
                }
                if (!emptyFunction) // so that an empty function is a one-liner
                {
                    output.AppendLine();
                }
                output.AppendLine(")");
                output.AppendLine();
            }
        }

        // Writes "param1 ... &tmp temp1 ..."
        void WriteFunctionHeader(Function function)
        {
            var summary = scriptSummary.Functions[function];
            for (int p = 1; p <= summary.ParameterCount; p++)
            {
                output.Append(" ");
                output.Append(symbols.Variable(script, summary.Function, VariableType.Parameter, p));
            }
            // third party compilers can declare the wrong temp count in the link instruction,
            // so as far as we're concerned, whichever number is higher is the temp count.
            int tempCount = Math.Max(summary.DeclaredTempCount, summary.MaxUsedTempIndex + 1);
            if (tempCount > 0)
            {
                output.Append(" &tmp");
            }
            for (int t = 0; t < tempCount; t++)
            {
                if (summary.Temps.Contains(t) && !summary.ComplexTemps.Contains(t))
                {
                    // this temp is only individually accessed
                    output.Append(" ");
                    output.Append(symbols.Variable(script, summary.Function, VariableType.Temp, t));
                }
                else
                {
                    // this temp is array accessed OR not individually accessed.
                    // i don't care which, but i do care how many subsequent temps aren't accessed,
                    // because they will be combined into this one.
                    int t2;
                    for (t2 = t; t2 < tempCount - 1; t2++)
                    {
                        if (summary.Temps.Contains(t2 + 1) ||
                            summary.ComplexTemps.Contains(t2 + 1))
                        {
                            break;
                        }
                    }
                    int arraySize = t2 - t + 1;
                    if (arraySize == 1)
                    {
                        // normal
                        output.Append(" ");
                        output.Append(symbols.Variable(script, summary.Function, VariableType.Temp, t));
                    }
                    else
                    {
                        // array
                        output.Append(" [");
                        output.Append(symbols.Variable(script, summary.Function, VariableType.Temp, t));
                        output.Append(" ");
                        output.Append(arraySize);
                        output.Append("]");
                        t = t2;
                    }
                }
            }
        }

        void WriteCompilerErrorIfInvalidTempCount(FunctionSummary function, string header)
        {
            if (function.MaxUsedTempIndex >= function.DeclaredTempCount)
            {
                output.Append(header);
                output.AppendLine("; COMPILER BUG: INCORRECT TEMP VARIABLE COUNT: " + function.DeclaredTempCount);
            }
        }

        void WriteObjects()
        {
            foreach (var obj in script.Objects)
            {
                WriteObject(obj);
            }
        }

        void WriteObject(Resource.Object obj)
        {
            var game = obj.Script.Game;
            output.Append(obj.IsClass ? "(class " : "(instance ");
            output.Append(symbols.Object(obj));
            var super = game.GetClass(obj);
            if (super != null)
            {
                output.Append(" of ");
                output.Append(symbols.Class(super));
            }
            output.AppendLine();

            int firstPropertyIndex;
            if (obj.Script.Source is Script0)
            {
                firstPropertyIndex = (int)PropertyIndex0.NameHeapOffset;
            }
            else if (obj.Script.Source is ScriptL)
            {
                firstPropertyIndex = (int)PropertyIndexL.NameHeapOffset;
            }
            else if (obj.Script.Source is Script11)
            {
                firstPropertyIndex = (int)PropertyIndex11.NameHeapOffset;
            }
            else // Script3
            {
                firstPropertyIndex = 0; // name is the first property
            }
            output.Append("\t(properties");
            bool emptyProperties = true; // "(properties)" when empty
            for (int i = firstPropertyIndex; i < obj.Properties.Count; i++)
            {
                var p = obj.Properties[i];

                // the "name" property is only rendered when it's different than the
                // name of the object. that happens when the name string isn't a
                // legal object name and i have to create a sanitized version for code.
                //
                // exceptions:
                // - if the name string has a dual language string then strip it
                // - if the name string is empty then it will be an integer value that
                //   doesn't point to a string; don't bother rendering this number.
                //   we will either know that there is no name by the generated
                //   code name, or it's a mac nameless game and i've supplied the names.
                if (p.Name == "name")
                {
                    // strip second language if this is a dual language game
                    string name = p.String;
                    if (symbols.LanguageSeparator != null && !string.IsNullOrEmpty(name))
                    {
                        name = Localization.RemoveSecondLanguage(name, symbols.LanguageSeparator);
                    }

                    // don't render name if it matches the object's name in code
                    if (name == symbols.Object(obj))
                    {
                        continue;
                    }

                    // don't render name if it's just a number that doesn't point to a string
                    if (p.String == null)
                    {
                        continue;
                    }
                }

                // skip property if its value matches its super.
                if (super != null)
                {
                    // sierra's compiler allowed string/said properties to be inherited, but they
                    // didn't really work unless the object and the super were in the same script.
                    // otherwise, the super's property offset was copied into the object's property,
                    // along with creating a relocation, even though the offset was almost certainly
                    // (but *not* certainly!) invalid in the other script's context.
                    // even when used within the same script, companion's compiler didn't handle this
                    // correctly until i fixed it in july 2024.
                    bool isReferenceFromOtherScript = (p.String != null || p.Said != null) &&
                                                      (obj.Script.Number != super.Script.Number);
                    if (!isReferenceFromOtherScript)
                    {
                        var superProperty = super.Properties.FirstOrDefault(p2 => p2.Selector == p.Selector);
                        if (superProperty != null) // shouldn't happen, but who knows
                        {
                            if (p.Value == superProperty.Value)
                            {
                                continue;
                            }
                        }
                    }
                }

                emptyProperties = false;
                output.AppendLine();
                output.Append("\t\t");
                output.Append(p.Name);
                output.Append(" ");
                if (p.String != null)
                {
                    FormatStringLiteral(p.String, output);
                }
                else if (p.Said != null)
                {
                    output.Append("'");
                    output.Append(p.Said.Text);
                    output.Append("'");
                }
                else
                {
                    UInt16 value = (UInt16)p.Value;
                    output.Append(value.GetSignedOrUnsigned(game.ByteCodeVersion));
                }
            }
            if (!emptyProperties)
            {
                output.AppendLine();
                output.Append("\t");
            }
            output.AppendLine(")");

            // combine object's methods and procedures
            var functions = new List<Function>(obj.Methods.Count);
            functions.AddRange(obj.Methods);
            functions.AddRange(script.Procedures.Where(p => p.Object == obj));

            // methods/obj-procedures will do leading lines
            foreach (var function in functions.OrderBy(f => f.CodePosition))
            {
                // if there's no function summary then skip this; we're in a test mode
                FunctionSummary functionSummary;
                if (!scriptSummary.Functions.TryGetValue(function, out functionSummary))
                    continue;

                output.AppendLine();
                WriteCompilerErrorIfInvalidTempCount(functionSummary, "\t");
                if (function is Method)
                {
                    output.Append("\t(method (");
                    output.Append(function.Name);
                }
                else
                {
                    output.Append("\t(procedure (");
                    output.Append(symbols.LocalProcedure(script, (int)function.CodePosition));
                }
                WriteFunctionHeader(function);
                output.Append(")");

                bool emptyFunction = true;
                if (functionSummary.Error != null)
                {
                    output.AppendLine();
                    output.Append("\t\t; DECOMPILER EXCEPTION: ");
                    output.Append(string.Join(", ", functionSummary.Error.Message.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)));
                    emptyFunction = false;
                }
                var ast = functionSummary.Ast;
                if (ast != null)
                {
                    if (ast.Children.Count != 0)
                    {
                        output.AppendLine();
                        output.Append("\t\t");
                        var astWriter = new FunctionWriter(output, 2, symbols, function);
                        astWriter.WriteTree(ast);
                        emptyFunction = false;
                    }
                }
                else if (functionSummary.Instructions != null)
                {
                    output.AppendLine();
                    output.Append("\t\t");
                    ZeAssemblyWriter.Write(output, 2, symbols, function);
                    emptyFunction = false;
                }
                if (!emptyFunction) // so that an empty function is a one-liner
                {
                    output.AppendLine();
                    output.Append("\t");
                }
                output.AppendLine(")");
            }

            output.AppendLine(")");
            output.AppendLine();
        }

        // Format a string literal for one-line output in source
        public static void FormatStringLiteral(string text, StringBuilder output)
        {
            output.Append('{');
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                switch (c)
                {
                    // escape brace, since i always use brace output.
                    // must not escape the opening brace.
                    case '}': output.Append(@"\}"); break;

                    // escape slashes (see below for companion workaround on final slash)
                    case '\\': output.Append(@"\\"); break;

                    // new line and tab can just be output as escaped chars
                    case '\n': output.Append(@"\n"); break;
                    case '\t': output.Append(@"\t"); break;

                    // carriage return is a little weird because sierra's compiler
                    // would expand the text "\r" to new line and carriage return,
                    // while companion just treats it as carriage return.
                    // i'm too old for this; write it as hex and dodge the issue.
                    case '\r': output.Append(@"\0d"); break;

                    // underscore has to be escaped; compiler treats it as space
                    case '_': output.Append(@"\_"); break;

                    // space character: the widow maker
                    case ' ':
                        // if the previous character is newline or the next character is space,
                        // then this space and subsequent spaces must be underscores
                        if ((i > 0 && text[i - 1] == '\n') || (i < text.Length - 1 && text[i + 1] == ' '))
                        {
                            while (i < text.Length && text[i] == ' ')
                            {
                                output.Append('_');
                                i++;
                            }
                            i--;
                        }
                        else
                        {
                            output.Append(' ');
                        }
                        break;

                    default:
                        if (32 <= c && c <= 126)
                        {
                            // normal
                            output.Append(c);
                        }
                        else
                        {
                            // \0c, \1d, etc
                            output.AppendFormat("\\{0:x2}", (byte)c);
                        }
                        break;
                }
            }

            // WORKAROUND: Companion has bugs when strings end in a backslash.
            // 1. the binary script parser misses the last character.
            //    a:\ is treated as a: even in the disassembly.
            // 2. the compiler and syntax highlighter think you're escaping
            //    the last brace, causing the rest of the file to be treated
            //    the string, even when escaping the slash.
            // I work around all this by escaping the last slash in hex form.
            if (text.Length > 0 && text[text.Length -1] == '\\')
            {
                // text ends in \ so i already output two slashes.
                // replace the second so that the output is \5c
                output.Length--;
                output.AppendFormat("{0:x2}", (byte)'\\');
            }

            output.Append('}');
        }
    }
}
