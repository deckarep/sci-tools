using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // "Class_255_0" => "DItem"
    //
    // Some class names weren't compiled in. If we know them, name them.
    // Relies on the naming convention from my decompiler, not sure if
    // it matches Companion's convention; they kinda look the same.
    //
    // There aren't many of these so it doesn't need to be fancy.
    //
    // Like Three Six Mafia, these are the Most Known Unknowns.

    static class ClassNamer
    {
        static ClassDef[] ClassDefs = new[]
        {
            new ClassDef(255, 0, 2, "MenuBar"),
            new ClassDef(255, 1, 2, "Item"),
            new ClassDef(255, 0, 1, "DItem"),

            new ClassDef(943, 1, 1, "_EditablePoint"),
            new ClassDef(948, 0, 2, "WriteCode"),
            new ClassDef(948, 1, 2, "CreateObject"),
            new ClassDef(993, 0, 1, "File"),

            // qfg2
            new ClassDef(84, 1, 1, "Ware"),
            new ClassDef(86, 0, 1, "GuardCode"),
            new ClassDef(310, 0, 1, "GetEarth"),

            new ClassDef(64916, 0, 1, "DItem"),
            new ClassDef(64943, 1, 2, "_EditablePoint"),
            new ClassDef(64943, 2, 2, "_EditablePolygon"),
        };

        public static void Run(Game game)
        {
            // build map of renames.
            var renames = new Dictionary<string, string>();
            foreach (var script in game.Scripts)
            {
                int count = (from c in script.Classes
                             where ClassDefs.Any(cd => cd.Script == script.Number &&
                                                       c.Name == cd.OldName)
                             select c).Count();
                if (count == 0) continue;

                var classDefsToUse = ClassDefs.Where(cd => cd.Script == script.Number && 
                                                           cd.Count == count);
                foreach (var classDef in classDefsToUse)
                {
                    renames.Add(classDef.OldName, classDef.NewName);
                }
            }
            if (!renames.Any()) return;

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                string className;
                if (node.Text.Length > 0 && // opt
                    renames.TryGetValue(node.Text, out className))
                {
                    (node as Atom).SetText(className);
                }
            }
        }

        class ClassDef
        {
            public readonly int Script;
            public readonly int Index;
            public readonly int Count;
            public readonly string OldName;
            public readonly string NewName;

            public ClassDef(int s, int i, int c, string n)
            {
                Script = s;
                Index = i;
                Count = c;
                NewName = n;
                OldName = "Class_" + Script + "_" + Index;
            }
        }
    }
}
