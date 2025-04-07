using System.Collections.Generic;
using System.Linq;
using SCI.Language;

// ModelExtensions: a bunch of functions i didn't want cluttering Game.cs, etc

namespace SCI.Annotators
{
    public static class ModelExtensions
    {
        // returns all room scripts. key: script, value: room name.
        // i might walk back this dictionary since it's always export 0,
        // but this is the form that RoomFileRenamer first used.
        // rooms are usually instances but sometimes they are a class.
        public static Dictionary<Script, string> GetRooms(this Game game)
        {
            var roomClasses = GetRoomClasses(game);

            // build a dictionary of all room scripts and their export0 name
            var roomScriptNames = new Dictionary<Script, string>();
            foreach (var script in game.Scripts)
            {
                // skip scripts without export 0
                string export0;
                if (!script.Exports.TryGetValue(0, out export0)) continue;

                // a room script is one whose export 0 is an instance of a room class or is a room class.
                bool isRoomScript = script.Instances.Any(i => i.Name == export0 && roomClasses.Contains(i.Super)) ||
                                    script.Classes.Any(c => c.Name == export0 && roomClasses.Contains(c.Name));

                if (isRoomScript)
                {
                    roomScriptNames.Add(script, export0);
                }
            }

            return roomScriptNames;
        }

        // returns all classes that derive from Rm or Room including
        // Rm or Room (depending on which exists in the game)
        public static HashSet<string> GetRoomClasses(this Game game)
        {
            var allRoomClasses = new HashSet<string>();

            // Rm in some games, Room in others
            var baseRoomClassNames = new[] { "Rm", "Room" };

            // find all classes that derive from one of the base room classes.
            // when a new class is discovered, search again.
            bool moreClassesFound;
            do
            {
                moreClassesFound = false;

                var roomClasses = (from s in game.Scripts
                                   from c in s.Classes
                                   where baseRoomClassNames.Contains(c.Name) ||
                                         allRoomClasses.Contains(c.Super)
                                   select c).ToList();

                foreach (var roomClass in roomClasses)
                {
                    if (allRoomClasses.Add(roomClass.Name))
                    {
                        moreClassesFound = true;
                    }
                }

            } while (moreClassesFound);

            return allRoomClasses;
        }

        // returns all instances and classes that derive from a
        // class with a given name, including that class.
        public static HashSet<Object> GetObjectsBySuper(this Game game, string super)
        {
            var childObjects = new HashSet<Object>();

            bool objectAdded;
            do
            {
                objectAdded = false;
                foreach (var obj in game.Scripts.SelectMany(s => s.Objects))
                {
                    if ((obj.Name == super && obj.Type == ObjectType.Class) ||
                         childObjects.Any(t => t.Name == obj.Super && t.Type == ObjectType.Class))
                    {
                        if (childObjects.Add(obj))
                        {
                            objectAdded = true;
                        }
                    }
                }
            } while (objectAdded);

            return childObjects;
        }

        // hack, could be part of Tree.cs
        public static bool IsSelector(this Node node)
        {
            return node is Atom && (node.Text.EndsWith(":") || node.Text.EndsWith("?"));
        }

        // hack, could be part of Tree.cs
        public static bool IsSelector(this Node node, string selector)
        {
            return node is Atom && (node.Text.EndsWith(":") || node.Text.EndsWith("?")) &&
                node.Text.Length == (selector.Length + 1) && node.Text.StartsWith(selector);
        }

        // hack, same story...
        public static IEnumerable<Node> GetSelectorParameters(this Node selector)
        {
            int selectorIndex = selector.Parent.Children.IndexOf(selector);
            int index = selectorIndex + 1;
            int parameterCount = 0;
            while (true)
            {
                var node = selector.Parent.At(index);
                if (node is Nil || node.IsSelector())
                {
                    break;
                }
                parameterCount++;
                index++;
            }
            return selector.Parent.Children.Skip(selectorIndex + 1).Take(parameterCount);
        }

        public static IEnumerable<Function> GetFunctions(this Game game)
        {
            var procedures = from s in game.Scripts
                             from p in s.Procedures
                             select p;
            var methods = from s in game.Scripts
                          from o in s.Objects
                          from m in o.Methods
                          select m;
            return procedures.Union(methods);
        }

        public static IEnumerable<Function> GetFunctions(this Script s)
        {
            var methods = from o in s.Objects
                          from m in o.Methods
                          select m;
            return s.Procedures.Union(methods);
        }

        public static IEnumerable<Function> GetAsmFunctions(this Game game)
        {
            var asmFunctions = from f in GetFunctions(game)
                               where f.Code.Any() &&
                                     f.Code.First().At(0).Text == "asm"
                               select f;
            return asmFunctions;
        }

        //
        // Dependency resolutions so that SayAnnotator can figure out the room number
        // that a non-room script is called from. These are some real diminishing
        // returns, but going overboard is fun, and how am I going to leave messages
        // on the table that could have been annotated?
        //
        // I made most of these public but GetUniqueRoomCallers() is the point of this.
        // QFG1VGA scripts tooGood.sc and opponentFight.sc are extreme examples of
        // this working. I gave up tracing through all the relationships on that
        // one, the algorithm worked, I'll take it!
        //

        // get all direct script references.
        // 1. (ScriptID scriptNumber ...)
        // 2. (exportProc ...)
        // Not supporting Class stuff, too complicated, diminishing returns
        // Not including "use" statements, i don't even know what those mean
        // key: script being referenced
        // values: scripts which directly reference it
        // results do not include self-references
        public static Dictionary<int, HashSet<int>> GetDirectScriptReferences(this Game game)
        {
            var references = new Dictionary<int, HashSet<int>>(game.Scripts.Count);
            var exportProcedureDictionary = game.GetExportProcedureDictionary();

            foreach (var script in game.Scripts)
            {
                references.Add(script.Number, new HashSet<int>());
            }

            foreach (var script in game.Scripts)
            {
                foreach (var node in script.Root)
                {
                    // ScriptID
                    Script exportProcedureScript;
                    if (node.At(0).Text == "ScriptID" && node.At(1) is Integer)
                    {
                        int calle = node.At(1).Number;
                        if (calle != script.Number) // ignore self-reference
                        {
                            if (references.ContainsKey(calle)) // calle might not exist, like a debug script
                            {
                                references[calle].Add(script.Number);
                            }
                        }
                    }
                    // calling an exported proc.
                    // this is a little sloppy, it will match procedure declarations,
                    // but those will then be ignored by the filtering of self-references.
                    else if (node.At(0) is Atom &&
                             exportProcedureDictionary.TryGetValue(node.At(1).Text, out exportProcedureScript))
                    {
                        int calle = exportProcedureScript.Number;
                        if (calle != script.Number) // ignore self-reference
                        {
                            references[calle].Add(script.Number);
                        }
                    }
                }
            }

            return references;
        }

        // dictionary of all exported procedures with their script as the value.
        // if more than one exported proc has the same name then it is excluded.
        public static Dictionary<string, Script> GetExportProcedureDictionary(this Game game)
        {
            var exportProcedureDictionary = new Dictionary<string, Script>();
            var duplicateProcedures = new HashSet<string>();

            foreach (var script in game.Scripts)
            {
                foreach (var export in script.Exports)
                {
                    foreach (var procedure in script.Procedures.Where(p => p.Name == export.Value))
                    {
                        if (duplicateProcedures.Contains(procedure.Name))
                        {
                            // ignore dupes
                        }
                        else if (!exportProcedureDictionary.ContainsKey(procedure.Name))
                        {
                            // add procedure to results
                            exportProcedureDictionary.Add(procedure.Name, script);
                        }
                        else
                        {
                            // newly discovered dupe. remove it from results and add it to dupes.
                            exportProcedureDictionary.Remove(procedure.Name);
                            duplicateProcedures.Add(procedure.Name);
                        }
                    }
                }
            }
            return exportProcedureDictionary;
        }

        // returns the distinct room number that calls a script.
        // returns -1 if no room calls the script
        // returns -2 if multiple rooms call the script
        // handles transitive relationships
        // handles circular references (ignores them)
        public static int GetDistinctRoomCaller(int script, IReadOnlyDictionary<int, HashSet<int>> callers, HashSet<int> roomNumbers)
        {
            var visitedScripts = new HashSet<int>();
            return GetDistinctRoomCaller(script, callers, roomNumbers, visitedScripts);
        }

        static int GetDistinctRoomCaller(int script, IReadOnlyDictionary<int, HashSet<int>> callers, HashSet<int> roomNumbers, HashSet<int> visitedScripts)
        {
            // prevent circular references / infinite recursion by tracking all
            // visited scripts and never visiting them twice.
            if (visitedScripts.Contains(script))
            {
                return -1;
            }
            else
            {
                visitedScripts.Add(script);
            }

            int roomCaller = -1;
            foreach (var caller in callers[script])
            {
                // is caller a room?
                if (roomNumbers.Contains(caller))
                {
                    if (roomCaller == -1)
                    {
                        roomCaller = caller;
                    }
                    else if (caller != roomCaller)
                    {
                        return -2; // multiple room callers found, abort
                    }
                }
                // caller is not a room
                else
                {
                    int callerRoomCaller = GetDistinctRoomCaller(caller, callers, roomNumbers, visitedScripts);
                    if (callerRoomCaller == -2)
                    {
                        return -2; // callers has multiple room callers, abort
                    }
                    else if (callerRoomCaller != -1)
                    {
                        // caller has a unique room caller
                        if (roomCaller == -1)
                        {
                            roomCaller = callerRoomCaller;
                        }
                        else if (callerRoomCaller != roomCaller)
                        {
                            return -2; // caller's room caller is different than the found one, abort
                        }
                    }
                }
            }
            return roomCaller;
        }

        // only including roomScripts as an optimization for SayAnnotator since it already knows them.
        // it's just the results of GetRooms()
        public static Dictionary<int, int> GetUniqueRoomCallers(this Game game, IEnumerable<Script> roomScripts)
        {
            var results = new Dictionary<int, int>();
            var references = game.GetDirectScriptReferences();
            var roomNumbers = new HashSet<int>(roomScripts.Select(s => s.Number));

            foreach (var script in game.Scripts)
            {
                int caller = GetDistinctRoomCaller(script.Number, references, roomNumbers);
                if (caller >= 0)
                {
                    results.Add(script.Number, caller);
                }
            }

            return results;
        }

        public static Object GetExportedObject(this Game game, int scriptNumber, int exportNumber)
        {
            string exportName = game.GetExport(scriptNumber, exportNumber);
            if (exportName == null) return null;

            var script = game.GetScript(scriptNumber);
            return script.Objects.FirstOrDefault(o => o.Name == exportName);
        }

        public static IEnumerable<Object> GetClasses(this Game game)
        {
            return game.Scripts.SelectMany(s => s.Classes);
        }

        //
        // Property helpers
        //

        public static int GetIntegerProperty(this Object obj, string propertyName)
        {
            var property = obj.Properties.FirstOrDefault(p => p.Name == propertyName);
            if (property != null && property.ValueNode is Integer)
            {
                return property.ValueNode.Number;
            }
            return int.MinValue;
        }
    }
}
