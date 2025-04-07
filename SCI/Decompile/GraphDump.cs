using System;
using System.IO;
using System.Linq;
using System.Text;
using SCI.Resource;
using SCI.Decompile.Cfg;

// Writes a basic control flow graph to a graphviz file.
// This humble little function saved the whole project.

namespace SCI.Decompile
{
    static class GraphDump
    {
        // ordered by priority (importance to me).
        // an instruction can have multiple flags but only one color.
        static Tuple<InstructionFlag, string>[] FlagColors =
        {
            // flags that affect decompiler behavior/output
            Tuple.Create(InstructionFlag.CompilerBug,      "red"),
            Tuple.Create(InstructionFlag.Continue,         "pink"),
            Tuple.Create(InstructionFlag.Break,            "brown"),
            // flags that only exist to color graphviz
            // so i can see what the decompiler has done
            Tuple.Create(InstructionFlag.LoopCondition,    "green"),
            Tuple.Create(InstructionFlag.DeoptimizedLoop,  "purple"),
            Tuple.Create(InstructionFlag.DeoptimizedGraph, "purple"),
            Tuple.Create(InstructionFlag.Deoptimized,      "blue"),
            Tuple.Create(InstructionFlag.ThirdPartyBranch, "orange"),
        };

        public static void Write(Function function, Graph cfg, string graphFile)
        {
            var file = new StringBuilder();
            file.AppendLine("digraph g {");
            file.AppendLine("\tlabelloc=\"t\""); // label location top
            file.AppendLine("\tlabel=\"" + function.FullName + "\"");

            // just basic blocks
            foreach (var block in cfg.Nodes.Where(n => n.Type == NodeType.Block).OrderBy(b => b.First.Position))
            {
                var name = block.First.Position.ToString();
                file.AppendLine("\t" + name + "[");
                file.Append("\t\tlabel=<");
                for (Instruction i = block.First; i != block.Last.Next; i = i.Next)
                {
                    string color = "";
                    if (i.Flags != InstructionFlag.None)
                    {
                        foreach (var flagColor in FlagColors)
                        {
                            if (i.HasFlag(flagColor.Item1))
                            {
                                color = flagColor.Item2;
                                break;
                            }
                        }
                    }
                    if (color != "")
                    {
                        file.Append("<font color=\"");
                        file.Append(color);
                        file.Append("\">");
                    }
                    // have to escape &rest
                    file.Append(i.ToString().Replace("&", "&amp;"));
                    if (color != "")
                    {
                        file.Append("</font>");
                    }
                    file.Append("<br align=\"left\"/>");
                }
                file.AppendLine(">");
                file.AppendLine("\t];");
            }
            file.AppendLine();

            foreach (var edge in cfg.Edges)
            {
                var a = edge.A;
                var b = edge.B;
                if (a.Type != NodeType.Block || b.Type != NodeType.Block) continue;

                var aName = a.First.Position.ToString();
                var bName = b.First.Position.ToString();

                file.AppendLine("\t" + aName + " -> " + bName);
            }

            file.AppendLine("}");
            File.WriteAllText(graphFile, file.ToString());
        }
    }
}
