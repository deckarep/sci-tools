using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SCI.Language
{
    // The model is a set of high level classes that describe a script's structure.
    // The Game class wraps all the scripts in a game. The model exists on top of the
    // parsed tree. Parser produces the tree and then the model is built from it.
    // Model classes contain links to the trees, and some of their properties are just getters
    // that return text from their source node. For example, Function.Name returns the text
    // of its second child's first child. If an annotator renames that node then the Name
    // property will be in sync.
    //
    // The Game class kicks off the parsing process. The constructor takes a game directory
    // and parses every script file in the "src" subdirectory.
    //
    // The model classes only need to capture things I'm interested in. Everything is still
    // captured in the tree, so even things the model doesn't care about get written back
    // to script files by TreeWriter.

    public class Game
    {
        public string Directory { get; private set; }
        public string ScriptDirectory { get; private set; }
        public string Name { get; private set; }

        public List<Script> Scripts { get; private set; }

        public Game(string gameDirectory)
        {
            Directory = gameDirectory;
            ScriptDirectory = Path.Combine(Directory, "src");
            Name = Path.GetFileName(Path.GetDirectoryName(ScriptDirectory));

            Scripts = new List<Script>();
            foreach (var scriptFile in System.IO.Directory.GetFiles(ScriptDirectory, "*.sc"))
            {
                var script = new Script(scriptFile);
                Scripts.Add(script);
            }
        }

        public override string ToString()
        {
            return Name + " [" + Scripts.Count + " scripts]";
        }

        public IReadOnlyDictionary<int, Local> Globals
        {
            get
            {
                return Scripts.First(s => s.Number == 0).Locals;
            }
        }

        public Script GetScript(int number)
        {
            return Scripts.FirstOrDefault(s => s.Number == number);
        }

        public Local GetGlobal(int number)
        {
            return GetScript(0).Locals[number];
        }

        public string GetExport(int scriptNumber, int exportNumber)
        {
            var script = Scripts.FirstOrDefault(s => s.Number == scriptNumber);
            if (script != null)
            {
                string exportName;
                if (script.Exports.TryGetValue(exportNumber, out exportName))
                {
                    return exportName;
                }
            }
            return null;
        }
    }

    public class Script
    {
        public string FullPath { get; private set; }
        public string FileName { get; private set; }
        public string OriginalText { get; private set; }

        public Root Root { get; private set; }

        public int Number { get; private set; }
        public Dictionary<int, string> Exports { get; private set; }
        public Dictionary<int, Local> Locals { get; private set; }
        public List<Function> Procedures { get; private set; }
        public List<Object> Classes { get; private set; }
        public List<Object> Instances { get; private set; }
        public List<Object> Objects { get; private set; }

        public Script(string scriptFile, Dictionary<string, int> scriptNumberMap = null)
        {
            FullPath = scriptFile;
            FileName = Path.GetFileName(FullPath);

            OriginalText = File.ReadAllText(FullPath);
            var lispParser = new Parser(OriginalText);
            Root = lispParser.Parse();

            Number = ParseScriptNumber(Root, scriptNumberMap);
            Exports = ParseExports(Root);
            Locals = ParseLocals(Root);
            Procedures = ParseProcedures(Root);
            Objects = ParseObjects(Root);
            foreach (var obj in Objects)
            {
                Procedures.AddRange(ParseProcedures(obj.Node, obj));
            }
            Procedures.Sort((a, b) => a.Node.Pos.CompareTo(b.Node.Pos));
            Classes = Objects.Where(o => o.Type == ObjectType.Class).ToList();
            Instances = Objects.Where(o => o.Type == ObjectType.Instance).ToList();
        }

        public override string ToString()
        {
            return Number + ": " + FileName;
        }

        static int ParseScriptNumber(List root, Dictionary<string, int> scriptNumberMap)
        {
            var script = root.FindChildList("script#") ??
                         root.FindChildList("module#");
            var node = script.At(1);
            if (node is Integer) return node.Number;
            if (node is Atom && scriptNumberMap != null)
            {
                int number;
                if (scriptNumberMap.TryGetValue(node.Text, out number)) return number;
            }
            throw new System.Exception("No script number: " + node);
        }

        static Dictionary<int, string> ParseExports(List root)
        {
            var exports = new Dictionary<int, string>();
            var list = root.FindChildList("public");
            if (list == null)
            {
                return exports;
            }

            for (int i = 1; i < list.Children.Count; ++i)
            {
                // qfg1mac broken decompiles didn't have names for exports,
                // so i am allowing blank exports, which are lines that just
                // have integers.
                string exportName = "";
                if (list.Children[i] is Atom)
                {
                    exportName = list.Children[i].Text;
                    i++;
                }
                int exportNumber = list.Children[i].Number;
                exports.Add(exportNumber, exportName);
            }
            return exports;
        }

        static Dictionary<int, Local> ParseLocals(List root)
        {
            var locals = new Dictionary<int, Local>();
            var list = root.FindChildList("local");
            if (list == null)
            {
                return locals;
            }

            int localNumber = 0;
            for (int i = 1; i < list.Children.Count; ++i)
            {
                var nameNode = list.Children[i];

                // skip (define ...) and (enum ...) blocks in original source
                if (nameNode is List &&
                    nameNode.At(0).Text == "define" ||
                    nameNode.At(0).Text == "enum")
                {
                    continue;
                }

                Node valueNode = null;
                if (i + 1 < list.Children.Count && list.Children[i + 1].Text == "=")
                {
                    i += 2;
                    valueNode = list.Children[i];
                }

                var local = new Local(localNumber, nameNode, valueNode);
                locals[localNumber] = local;
                localNumber += local.Count;
            }

            return locals;
        }

        // ParseProcedures is used to parse top-level procedures and also
        // rare procedures that are declared within an object.
        // I am still including those in Script.Procedures, with their Object set.
        List<Function> ParseProcedures(Node root, Object obj = null)
        {
            var procedures = new List<Function>();
            foreach (var node in root.Children)
            {
                if (node.At(0).Text == "procedure")
                {
                    // original source has a (procedure) forward declaration block.
                    // detect and ignore if all children are atoms/integers.
                    // this can also occur within objects.
                    if (node.Children.All(c => c is Atom || c is Integer)) continue;

                    procedures.Add(new Function(this, obj, node));
                }
            }
            return procedures;
        }

        List<Object> ParseObjects(List root)
        {
            var objects = new List<Object>();
            foreach (var node in root.Children)
            {
                if (node.At(0).Text == "class")
                {
                    objects.Add(new Object(this, ObjectType.Class, node));
                }
                else if (node.At(0).Text == "instance")
                {
                    objects.Add(new Object(this, ObjectType.Instance, node));
                }
            }
            return objects;
        }
    }

    public class Local
    {
        public int Number { get; private set; }
        public Node NameNode { get; private set; }
        public string Name { get { return (NameNode is Array) ? NameNode.At(0).Text : NameNode.Text; } }
        public int Count { get; private set; } // arrays are > 1
        public object[] Values { get; private set; }
        public object Value { get { return Values[0]; } }

        public Local(int number, Node nameNode, Node valueNode)
        {
            Number = number;
            NameNode = nameNode;
            if (nameNode is Array)
            {
                // decompiler output will always include count
                Count = nameNode.At(1).Number;
            }
            else
            {
                // original source may not include count, have to infer
                Count = 1;
                if (valueNode is Array)
                {
                    Count = valueNode.Children.Count;
                }
            }
            Values = new object[Count];
            if (valueNode == null)
            {
                for (int i = 0; i < Count; ++i)
                {
                    Values[i] = 0;
                }
            }
            else if (valueNode is Array)
            {
                // decompiler doesn't render trailing zeros in arrays
                // so there may be fewer nodes than the count. just
                // put zeros for those.
                int i;
                for (i = 0; i < valueNode.Children.Count; ++i)
                {
                    Values[i] = valueNode.Children[i].Value;
                }
                for (; i < Count; ++i)
                {
                    Values[i] = 0;
                }
            }
            else
            {
                Values[0] = valueNode.Value;
            }
        }

        public override string ToString()
        {
            return Name + (Count > 1 ? ("[" + Count + "]") : "");
        }
    }

    public enum FunctionType
    {
        Method,
        Procedure
    }

    public class Function
    {
        public Script Script { get; private set; }
        public Object Object { get; private set; } // null if Procedure
        public FunctionType Type { get { return (Object != null) ? FunctionType.Method : FunctionType.Procedure; } }
        public bool IsExported { get { return (Object == null) && Script.Exports.ContainsValue(Name); } }
        public Node Node { get; private set; }
        public string Name { get { return Node.At(1).At(0).Text; } }
        public string FullName { get { return (Object != null) ? (Object.Name + ":" + Name) : Name ; } }
        public List<Variable> Parameters { get; private set; }
        public List<Variable> Temps { get; private set; }
        public IEnumerable<Node> Code { get; private set; }

        public Function(Script script, Object obj, Node node)
        {
            Script = script;
            Object = obj;
            Node = node;
            var header = node.At(1);
            Parameters = new List<Variable>();
            Temps = new List<Variable>();
            int variableNumber = 1; // start parameters at 1 since argc is 0
            var variableType = VariableType.Parameter;
            for (int i = 1; i < header.Children.Count; ++i)
            {
                if (header.Children[i].Text == "&tmp")
                {
                    variableNumber = 0;
                    variableType = VariableType.Temp;
                    continue;
                }
                var variable = new Variable(variableType, variableNumber, header.Children[i]);
                if (variableType == VariableType.Parameter)
                {
                    Parameters.Add(variable);
                }
                else
                {
                    Temps.Add(variable);
                }
                variableNumber += variable.Count;
            }
            Code = node.Children.Skip(2);
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum VariableType
    {
        Parameter,
        Temp
    }

    public class Variable
    {
        public VariableType Type { get; private set; }
        public int Number { get; private set; }
        public Node Node { get; private set; }
        public string Name { get { return (Node is Array) ? Node.At(0).Text   : Node.Text; } }
        public int Count   { get { return (Node is Array) ? Node.At(1).Number : 1; } }

        public Variable(VariableType type, int number, Node node)
        {
            Type = type;
            Node = node;
            Number = number;
        }

        public override string ToString()
        {
            return Name + (Count > 1 ? ("[" + Count + "]") : "");
        }
    }

    public enum ObjectType
    {
        Class,
        Instance
    }

    public class Object
    {
        public Script Script { get; private set; }
        public ObjectType Type { get; private set; }
        public Node Node { get; private set; }
        public string Name { get { return Node.At(1).Text; } }
        public string PrintName
        {
            get
            {
                var nameProp = Properties.FirstOrDefault(n => n.Name == "name");
                if (nameProp != null && nameProp.ValueNode is String)
                {
                    return nameProp.Value.ToString();
                }
                return Name;
            }
        }
        public string Super
        {
            get
            {
                if (Node.At(2).Text == "of" ||
                    Node.At(2).Text == "kindof") // original source
                {
                    return Node.At(3).Text;
                }
                return "";
            }
        }
        public List<Property> Properties { get; private set; }
        public List<Function> Methods { get; private set; }

        public Object(Script script, ObjectType type, Node node)
        {
            Script = script;
            Type = type;
            Node = node;
            Properties = ParseProperties(node);
            Methods = ParseMethods(node);
        }

        List<Function> ParseMethods(Node root)
        {
            var procedures = new List<Function>();
            foreach (var node in root.Children)
            {
                if (node.At(0).Text == "method")
                {
                    procedures.Add(new Function(Script, this, node));
                }
            }
            return procedures;
        }

        static List<Property> ParseProperties(Node root)
        {
            var properties = new List<Property>();
            var propertiesNode = root.FindChildList("properties");
            if (propertiesNode != null)
            {
                // LSCI property lists can specify int or id on each.
                // it's optional, but if they do one property, i think
                // they have to do all of them in the list.
                // (properties
                //   id|int name value
                bool lsci = false;
                if ((propertiesNode.Children.Count - 1) % 3 == 0)
                {
                    lsci = true;
                    for (int i = 1; i < propertiesNode.Children.Count; i += 3)
                    {
                        if (propertiesNode.Children[i].Text != "id" &&
                            propertiesNode.Children[i].Text != "int")
                        {
                            lsci = false;
                            break;
                        }
                    }
                }

                for (int i = 1; i < propertiesNode.Children.Count; i += 2)
                {
                    if (lsci) i++; // skip id|int
                    properties.Add(new Property(propertiesNode.Children[i]));
                }
            }
            return properties;
        }

        public override string ToString()
        {
            return Name + (Super == "" ? "" : (" of " + Super)) +
                (Type == ObjectType.Class ? " [class]" : " [instance]");
        }
    }

    public class Property
    {
        public Node NameNode { get; private set; }
        public Node ValueNode { get { return NameNode.Next(1); } }
        public string Name { get { return NameNode.Text; } }
        public object Value { get { return ValueNode.Value; } }

        public Property(Node node)
        {
            NameNode = node;
        }

        public override string ToString()
        {
            return NameNode.Text + ": " + ValueNode.Text;
        }
    }
}