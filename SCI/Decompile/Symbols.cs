using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCI.Resource;
using SCI.Decompile.Mac;

// All symbol lookups go through here.
//
// Handles file names, including known ones.
// Handles default values for unknown symbols.
// Handles the three Mac games without object names, uses known values.
//
// Sanitizes names so that they are legal code symbols.
// Strips dual language from object names.

namespace SCI.Decompile
{
    class Symbols
    {
        Game game;
        Dictionary<int, string> scriptNames;
        Dictionary<Resource.Object, string> objectNames;
        string languageSeparator;
        MacSymbols macSymbols;

        public string LanguageSeparator { get { return languageSeparator; } }

        public Symbols(Game game)
        {
            this.game = game;
            if (game.ScriptFormat == ScriptFormat.SCI0)
            {
                // if there are any script.0 objects with a language separator
                // in their name, use that for sanitizing all object names.
                var script0 = game.Scripts.First(s => s.Number == 0);
                var script0ObjectNames = script0.Objects.Select(o => o.Name);
                languageSeparator = Localization.DetectSeparator(script0ObjectNames, 1);
                if (languageSeparator == null)
                {
                    // that didn't work for lsl5, so try the strings too
                    var script0Strings = script0.Strings.Select(s => s.Text);
                    languageSeparator = Localization.DetectSeparator(script0Strings);
                }
            }

            macSymbols = new MacSymbols(game);

            GenerateScriptNames(game);
            GenerateObjectNames(game);
        }

        public string KernelFunction(int number)
        {
            return (number < game.KernelTable.Length) ?
                   game.KernelTable[number] :
                   ("kernel_" + number);
        }

        public string Selector(int number)
        {
            // Don't need to handle SCI0 early selectors here, that's handled
            // by SelectorVocab when building the selector table.

            // If the selector isn't in the table because it's too large, or
            // because it points to the "BAD SELECTOR" string in the table,
            // then use Companion's "sel_####" convention.
            if (number < game.Selectors.Length &&
                game.Selectors[number] != "BAD SELECTOR")
            {
                return game.Selectors[number];
            }
            return "sel_" + number;
        }

        public string Class(Script script, int classNumber)
        {
            var cls = game.GetClass(script, classNumber);
            if (cls == null)
            {
                // Hoyle3 references unknown classes in Character:startAudio,
                // a method that never executes. I guess they were going to
                // have the characters talk but got rid of it and didn't
                // include the unnecessary supporting classes.
                // cnick-lb and cnick-kq also do this; same code.
                Log.Debug(script.Game, "Unknown class " + classNumber + " referenced in script #" + script.Number);
                return "Unknown_Class_" + classNumber;
            }
            return Class(cls);
        }

        public string Class(Resource.Object cls)
        {
            // check cache first
            string name;
            if (objectNames != null && objectNames.TryGetValue(cls, out name))
            {
                return name;
            }

            if (!string.IsNullOrWhiteSpace(cls.Name))
            {
                // sanitize the name, but if every character is unusable then
                // the results are pure underscores, and that's unacceptable.
                // treat it has having no name. (russian fan translations, etc)
                string sanitized = Sanitize(cls.Name);
                if (sanitized.Any(c => c != '_'))
                {
                    return sanitized;
                }
            }

            // class has no name. figure out index, like SCI Companion
            int index = 0;
            foreach (var c in cls.Script.Objects.Where(o => o.IsClass))
            {
                if (c == cls) break;
                index++;
            }

            // check if MacSymbols knows the name of this class
            if (macSymbols.Classes != null)
            {
                var id = new Id(cls.Script.Number, index);
                if (macSymbols.Classes.TryGetValue(id, out name))
                {
                    return name;
                }
            }

            return "Class_" + cls.Script.Number + "_" + index;
        }

        public string Instance(Resource.Object instance)
        {
            // check cache first
            string name;
            if (objectNames != null && objectNames.TryGetValue(instance, out name))
            {
                return name;
            }

            if (!string.IsNullOrWhiteSpace(instance.Name))
            {
                // sanitize the name, but if every character is unusable then
                // the results are pure underscores, and that's unacceptable.
                // treat it has having no name. (russian fan translations, etc)
                string sanitized = Sanitize(instance.Name);
                if (sanitized.Any(c => c != '_'))
                {
                    return sanitized;
                }
            }

            // instance has no name. uncommon. it really could have no name,
            // or the entire game may have been had print names disabled.
            // qfg1vga-mac, hoyle4-mac, lsl6-mac, cnick-lb (laurabow).
            //
            // name it: unknown_scriptNumber_instanceIndex
            //
            // this gives it a distinct name that my tools can use to recover
            // original names and place in a text file that will be used.
            int index = 0;
            foreach (var i in instance.Script.Objects.Where(o => !o.IsClass))
            {
                if (i == instance) break;
                index++;
            }

            // check if MacSymbols knows the name of this instance
            if (macSymbols.Instances != null)
            {
                var id = new Id(instance.Script.Number, index);
                if (macSymbols.Instances.TryGetValue(id, out name))
                {
                    return name;
                }
            }

            return "unknown_" + instance.Script.Number +  "_" + index;
        }

        public string Object(Resource.Object obj)
        {
            return obj.IsClass ? Class(obj) : Instance(obj);
        }

        public string Script(Script script)
        {
            return scriptNames[script.Number];
        }

        public string Sanitize(string input)
        {
            // optimization: do nothing if input is fine
            if (input.All(c => IsSafeChar(c)) &&
                !char.IsDigit(input.FirstOrDefault()))
            {
                return input;
            }
            if (languageSeparator != null)
            {
                input = Localization.RemoveSecondLanguage(input, languageSeparator);
            }
            // first character can't be a digit (CB: "7 crackers    ")
            // do what companion does, slap on a leading underscore.
            var output = new StringBuilder(input.Length);
            if ('0' <= input[0] && input[0] <= '9')
            {
                output.Append("_");
            }
            foreach (var c in input)
            {
                output.Append(IsSafeChar(c) ? c : '_');
            }
            return output.ToString();
        }

        public static bool IsSafeChar(char c)
        {
            return ('A' <= c && c <= 'Z') ||
                   ('a' <= c && c <= 'z') ||
                   ('0' <= c && c <= '9') ||
                   c == '_';
        }

        public string PublicProcedure(int scriptNumber, int exportNumber)
        {
            return "proc" + scriptNumber + "_" + exportNumber;
        }

        // this could be a local call to a public procedure!
        // this makes me want to name them all up front, update them, and just
        // have these functions just return the existing Function.Name value.
        // but for now this is fine.
        public string LocalProcedure(Script script, int offset)
        {
            // SCI Companion uses the offset as the name, but that makes diffing hard.
            // I'm just going to do the index.
            int index = 0;
            bool found = false;
            foreach (var p in script.Procedures.OrderBy(p => p.CodePosition))
            {
                if (p.CodePosition == offset)
                {
                    if (p.ExportNumber != 0xffff)
                    {
                        // this is a local call to a public procedure
                        return PublicProcedure(script.Number, p.ExportNumber);
                    }
                    found = true;
                    break;
                }
                else if (!p.IsPublic)
                {
                    index++;
                }
            }
            if (!found) throw new Exception("Unknown localproc");
            return "localproc_" + index;
        }

        public string Variable(Script script, Function function, VariableType type, int number)
        {
            if (script.Number == 0 && type == VariableType.Local)
            {
                type = VariableType.Global;
            }

            switch (type)
            {
                case VariableType.Global:
                    if (script.Game.ScriptFormat == ScriptFormat.LSCI)
                    {
                        // LSCI injects parameter count as global0
                        number--;
                    }
                    return "global" + number;
                case VariableType.Local:
                    if (script.Game.ScriptFormat == ScriptFormat.LSCI)
                    {
                        // LSCI injects parameter count as local0
                        number--;
                    }
                    return "local" + number;
                case VariableType.Parameter:
                    if (number == 0)
                    {
                        return "argc";
                    }
                    else
                    {
                        return "param" + number;
                    }
                default:
                    return "temp" + number;
            }
        }

        public string Property(Resource.Object obj, int index)
        {
            if (game.ByteCodeVersion == ByteCodeVersion.SCI3)
            {
                return Selector(index);
            }

            // if i failed to identify a function's object. shouldn't happen.
            if (obj == null) return "--UNKNOWN-FUNC-PROPERTY--";

            // unknown properties exist due to script bugs.
            index /= 2;
            var property = obj.Properties.FirstOrDefault(p => p.Index == index);
            if (property != null)
            {
                return property.Name;
            }
            return "--UNKNOWN-PROPERTY--"; // "--UNKNOWN-PROP-NAME--" in Companion, oops
        }

        // default global names, param names, temp names,

        void GenerateScriptNames(Game game)
        {
            // generate script names and group them by lowercase,
            // because these will be used for filenames.
            var groups = new Dictionary<string, List<Script>>(game.Scripts.Count);
            foreach (var script in game.Scripts)
            {
                string name = GenerateScriptName(script).ToLower();
                if (!groups.ContainsKey(name))
                {
                    groups.Add(name, new List<Script>());
                }
                groups[name].Add(script);
            }

            scriptNames = new Dictionary<int, string>(game.Scripts.Count);
            foreach (var group in groups.Values)
            {
                foreach (var script in group)
                {
                    string name = GenerateScriptName(script);
                    if (group.Count > 1)
                    {
                        // disambiguate with script number
                        name += "_" + script.Number;
                    }
                    scriptNames.Add(script.Number, name);
                }
            }
        }

        static Dictionary<int, string> KnownScriptNames = new Dictionary<int, string>
        {
            { 0, "Main" },

            { 255, "Interface" }, // INTRFACE.SC

            { 921, "Print" },
            { 922, "Dialog" },
            { 923, "Inset" },
            { 924, "Messager" },
            { 925, "Conversation" }, // CONV.SC
            { 926, "FlipPoly" },
            { 927, "PAvoider" }, // PAVOID.SC
            { 928, "Talker" },
            { 929, "Sync" },
            { 930, "PChase" },
            { 931, "CDActor" }, // KQ5
            { 932, "PFollow" }, // previously LANGUAGE.SC, i handle this specially
            { 933, "PseudoMouse" }, // PMOUSE.SC
            // 934: Slider, SLDICON.SC
            { 935, "Scaler" },
            { 936, "BorderWindow" }, // BORDWIND.SC
            { 937, "IconBar" },
            // 938: ROsc, RANGEOSC.SC, was PCYCLE.SC
            { 939, "Osc" },
            // 940: unused_11, "was PRINTD"
            { 941, "RandCycle" }, // RANDCYC.SC
            { 942, "MCyc" }, // MOVECYC.SC
            { 943, "PolyEdit" },
            { 944, "FileSelector" }, // FILESEL.SC
            { 945, "PolyPath" },
            { 946, "Polygon" },
            { 947, "DialogEditor" }, // DLGEDIT.SC
            { 948, "WriteFeature" }, // WRITEFTR.SC
            // 949 Block, coming out "Blk"
            { 950, "Feature" },
            // these are fine
            { 958, "LoadMany" },
            // fine
            { 965, "Count" },
            { 966, "Sort" },
            // fine
            { 974, "NameFind" },
            // fine
            { 981, "Window" },
            { 982, "Sight" },
            // fine
            { 989, "Sound" },
            { 990, "Save" },
            { 991, "Jump" },
            { 992, "Motion" },
            { 993, "File" },
            { 994, "Game" },
            { 995, "Inventory" }, // INVENT.SC
            { 996, "User" },
            { 997, "Menu" },
            { 998, "Actor" },
            { 999, "System" },
        };

        static Dictionary<int, string> KnownScriptNames32 = new Dictionary<int, string>
        {
            { 64916, "DItem" }, // just has this DItem class, and name isn't included
            { 64908, "SpeedTest" }, // SEEDTST.SC, and 908 is eggScript in kq6
            { 64961, "StopWalk" },  // pq4 has two StopWalks and it prevents this file from getting named
        };

        string GenerateScriptName(Script script)
        {
            // if it's a known name, use it
            string originalName;
            if (script.Number == 0 || script.Game.ByteCodeVersion == ByteCodeVersion.SCI0_11)
            {
                // 932 was originally Language, with a bunch of localization procs,
                // and later became PFollow. If no objects then assume Language
                if (script.Number == 932 && !script.Objects.Any())
                {
                    return "Language";
                }

                // SCI16
                if (KnownScriptNames.TryGetValue(script.Number, out originalName)) return originalName;
            }
            else if (script.Number > 64000)
            {
                // SCI32
                if (KnownScriptNames.TryGetValue(script.Number - 64000, out originalName)) return originalName;
                if (KnownScriptNames32.TryGetValue(script.Number, out originalName)) return originalName;
            }

            // Export 0 Object has next highest priority.
            // This handles rooms, which are the most important.
            // If the object is a class, then it must appear in the class table.
            // I'm not giving rogue/dupe classes their own file name, they are
            // likely to cause file name collisions and they are junk.
            var export0Object = script.GetExportedObject(0);
            if (export0Object != null)
            {
                if (!export0Object.IsClass || IsClassInTable(export0Object))
                {
                    return Object(export0Object);
                }
            }

            // First class with a known name, as long as it appears in the class table.
            // if the name is blank then we must be able to figure out its real name (mac symbols).
            var cls = script.Objects.FirstOrDefault(o => o.IsClass &&
                                                         (!string.IsNullOrWhiteSpace(o.Name) ||
                                                          !Class(o).StartsWith("Class_")) &&
                                                         IsClassInTable(o));
            if (cls != null)
            {
                return Class(cls);
            }

            // Okay what's the *next* exported object?
            // Again, classes must appear in the table. (No way this matters here)
            for (int i = 1; i < script.Exports.Count; i++)
            {
                var exportObject = script.GetExportedObject(i);
                if (exportObject != null)
                {
                    if (!exportObject.IsClass || IsClassInTable(exportObject))
                    {
                        return Object(exportObject);
                    }
                }
            }

            // Script number
            return "n" + script.Number.ToString("000");
        }

        static bool IsClassInTable(Resource.Object cls)
        {
            var table = cls.Script.Game.ClassTable;
            return cls.Species < table.Length &&
                   table[cls.Species] == cls.Script.Number;
        }

        // sigh, have to do this all at once up front because there
        // cab be duplicate object names in the same script, so fix
        // collisions here by appending _a, _b, _c like companion does.
        // see pepper inventory items, multiple named "Wood Carving".
        void GenerateObjectNames(Game game)
        {
            objectNames = new Dictionary<Resource.Object, string>();
            var scriptObjectNames = new Dictionary<Resource.Object, string>();
            var dupeNames = new HashSet<string>();
            foreach (var script in game.Scripts)
            {
                // generate each name
                scriptObjectNames.Clear();
                foreach (var obj in script.Objects)
                {
                    scriptObjectNames.Add(obj, Object(obj));
                }

                // find dupes
                dupeNames.Clear();
                foreach (var name in scriptObjectNames.Values)
                {
                    if (scriptObjectNames.Values.Count(s => s == name) > 1)
                    {
                        dupeNames.Add(name);
                    }
                }

                // foreach dupe, update all of its objects, and pray this doesn't collide
                foreach (var dupeName in dupeNames)
                {
                    char letter = 'a';
                    foreach (var obj in script.Objects)
                    {
                        if (scriptObjectNames[obj] == dupeName)
                        {
                            string newName = dupeName + "_" + letter;
                            scriptObjectNames[obj] = newName;
                            letter++;
                        }
                    }
                }

                // this will never happen
                if (scriptObjectNames.Values.Distinct().Count() != scriptObjectNames.Count)
                    throw new Exception("Unable to deduplicate object names in script " + script);

                // okay add them all
                foreach (var kv in scriptObjectNames)
                {
                    objectNames.Add(kv.Key, kv.Value);
                }
            }
        }
    }
}
