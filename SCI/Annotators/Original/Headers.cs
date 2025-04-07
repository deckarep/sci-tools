using System.Collections.Generic;
using FunctionType = SCI.Language.FunctionType;

namespace SCI.Annotators.Original
{
    public class Script
    {
        public int Number;
        public Dictionary<int, string> Exports;
        public Dictionary<int, Variable> Locals;
        public Function[] Functions;

        public int ExportCount { get { return Exports?.Count ?? 0; } }
        public int LocalCount { get { return Locals?.Count ?? 0; } }

        public override string ToString()
        {
            return string.Format("Script {0} -- Exports: {1}, Locals: {2}, Functions: {3}",
                Number, ExportCount, LocalCount, Functions.Length);
        }
    }

    public class Function
    {
        public FunctionType Type;
        public string Name;
        public string Object;
        public string[] Parameters;
        public Variable[] Temps;

        public int ParameterCount { get { return Parameters?.Length ?? 0; } }
        public int TempCount { get { return Temps?.Length ?? 0; } }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Object) ? Name : (Object + ":" + Name);
        }
    }

    public class Variable
    {
        public int Index;
        public int Length;
        public string Name;

        public Variable(int index, int length, string name) { Index = index; Length = length; Name = name; }

        public override string ToString()
        {
            return string.Format("{0:02} {1}", Index, (Length == 1) ? Name : ("[" + Name + " " + Length + "]"));
        }
    }
}
