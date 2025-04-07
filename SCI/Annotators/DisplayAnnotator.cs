using System.Collections.Generic;
using SCI.Language;

namespace SCI.Annotators
{
    class DisplayAnnotator
    {
        public static void Run(Game game)
        {
            foreach (var function in game.GetFunctions())
            {
                foreach (var node in function.Node)
                {
                    if (node.At(0).Text == "Display" && node.Children.Count >= 3)
                    {
                        Annotate(node);
                    }
                }
            }
        }

        enum StringType { Unknown, Near, Far }

        static void Annotate(Node node)
        {
            // try to figure out the string type.
            // near: 1 parameter
            // far:  2 parameters that evaluate to integers
            var type = StringType.Unknown;
            var param1 = node.At(1);
            var param2 = node.At(2);

            if (param1 is Integer)
            {
                type = StringType.Far;
            }
            else if (param1 is String || param2 is AddressOf)
            {
                type = StringType.Near;
            }
            else if (param2 is Integer && Ops.ContainsKey(param2.Number))
            {
                type = StringType.Near;
            }
            else if (node.At(3) is Integer && Ops.ContainsKey(node.At(3).Number))
            {
                type = StringType.Far;
            }

            // if we couldn't figure it out, forget it
            if (type == StringType.Unknown) return;

            // attempt to solve this by building a map.
            // only apply these changes if there are no problems.
            var map = new Dictionary<Node, Op>();
            for (int i = (type == StringType.Near ? 2 : 3); i < node.Children.Count; i++)
            {
                // all ops have to be integers (but skip &rest)
                var param = node.Children[i];
                if (param.Text == "&rest") continue;
                if (!(param is Integer)) return;

                // all ops have to be known
                Op op;
                if (!Ops.TryGetValue(param.Number, out op)) return;

                // there must be enough parameters
                if (!(i + op.Params < node.Children.Count)) return;

                map.Add(param, op);
                i += op.Params;
            }

            // apply the changes, everything is good
            foreach (var action in map)
            {
                var param = action.Key;
                var op = action.Value;
                if (op.Annotate)
                {
                    param.Annotate(op.Name);
                }
                else
                {
                    (param as Integer).SetDefineText(op.Name);
                }
                if (op.Name == "dsALIGN")
                {
                    var alignNode = param.Next();
                    if (alignNode is Integer)
                    {
                        string alignment;
                        if (Alignments.TryGetValue(alignNode.Number, out alignment))
                        {
                            (alignNode as Integer).SetDefineText(alignment);
                        }
                    }
                }
            }
        }

        static Dictionary<int, Op> Ops = new Dictionary<int, Op>
        {
            { 100, new Op("dsCOORD", 2) },
            { 101, new Op("dsALIGN", 1) },
            { 102, new Op("dsCOLOR", 1) },
            { 103, new Op("dsBACKGROUND", 1) },
            { 104, new Op("dsDISABLED", 1) },
            { 105, new Op("dsFONT", 1) },
            { 106, new Op("dsWIDTH", 1) },
            { 107, new Op("dsSAVEPIXELS", 0) },
            { 108, new Op("dsRESTOREPIXELS", 1) },
            { 114, new Op("p_dispose", 0, true) }, // it was incorrect to use this, but it had no effect
            { 115, new Op("p_time", 1, true) }, // it was incorrect to use this, but it had no effect
            { 117, new Op("p_draw", 0, true) }, // it was incorrect to use this, but it had no effect
            { 121, new Op("p_noshow", 0, true) },
        };

        class Op
        {
            public readonly string Name;
            public readonly int Params;
            public readonly bool Annotate;
            public Op(string n, int p, bool a = false) { Name = n; Params = p; Annotate = a; }
        }

        static Dictionary<int, string> Alignments = new Dictionary<int, string>
        {
            { -1, "alRIGHT" },
            {  0, "alLEFT" },
            {  1, "alCENTER" },
        };
    }
}
