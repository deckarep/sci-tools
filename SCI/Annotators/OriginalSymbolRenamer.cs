using System;
using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    static class OriginalSymbolRenamer
    {
        [Flags]
        enum Match
        {
            None = 0,
            LocalVariables = 1,   // Count
            LocalProcedures = 2,  // Count and variables (all local procs)
            PublicProcedures = 4, // Count and variables (all public procs)
            Exports = 8,          // Count and type
            All = Exports | PublicProcedures | LocalProcedures | LocalVariables,
        }

        [Flags]
        enum MethodMatch
        {
            None = 0,
            Temps = 1,
            Params = 2,
            All = Params | Temps,
        }

        public static void Run(Game game, Dictionary<int, Original.Script[]> allHeaders)
        {
            var exportRenames = new Dictionary<Tuple<int, int>, string>();

            var results = new Dictionary<Original.Script, Match>();
            foreach (var scriptNumber in allHeaders.Keys)
            {
                // get game script
                var script = game.Scripts.FirstOrDefault(s => s.Number == scriptNumber);
                if (script == null) continue;

                // compare each candidate header to game script
                var headers = allHeaders[scriptNumber];
                results.Clear();
                foreach (var header in headers)
                {
                    results.Add(header, CompareHeader(game, script, header));
                }

                // select the best one, if any, and apply it
                var bestResult = results.OrderByDescending(kv => kv.Value).First();
                if (bestResult.Value != Match.None)
                {
                    Apply(script, bestResult.Key, bestResult.Value, exportRenames);
                }

                // now do object methods
                DoObjectMethods(script, headers);
            }

            // rename all the exports we've learned about
            ExportRenamer.Run(game, exportRenames);
        }

        static void Apply(Script script, Original.Script header, Match result, Dictionary<Tuple<int, int>, string> exportRenames)
        {
            // exports match so rename public procs
            if (result.HasFlag(Match.Exports) && header.ExportCount > 0)
            {
                foreach (var headerExport in header.Exports)
                {
                    if (header.Functions.Any(f => f.Type == FunctionType.Procedure &&
                                                  f.Name == headerExport.Value))
                    {
                        exportRenames.Add(Tuple.Create(header.Number, headerExport.Key), headerExport.Value);
                    }
                }
            }

            // exports match, so any public procs whose variables match can have variables renamed.
            // that is, i don't require every public proc's variables to match.
            if (result.HasFlag(Match.Exports) && header.ExportCount > 0)
            {
                foreach (var headerExport in header.Exports)
                {
                    var headerProc = header.Functions.FirstOrDefault(f => f.Type == FunctionType.Procedure &&
                                                                          f.Name == headerExport.Value);
                    if (headerProc == null) continue;
                    var scriptProc = script.Procedures.First(p => p.Name == script.Exports[headerExport.Key]);
                    RenameFunctionVariables(scriptProc, headerProc);
                }
            }

            var scriptwideRenames = new Dictionary<string, string>();
            if (result.HasFlag(Match.LocalProcedures))
            {
                var scriptProcs = script.Procedures.Where(p => !script.Exports.ContainsValue(p.Name)).ToList();
                var headerProcs = header.Functions.Where(f => f.Type == FunctionType.Procedure &&
                                                              (header.Exports == null ||
                                                               !header.Exports.ContainsValue(f.Name))).ToList();
                for (int i = 0; i < scriptProcs.Count; i++)
                {
                    // add local proc to list of renames
                    if (scriptProcs[i].Name != headerProcs[i].Name)
                    {
                        scriptwideRenames.Add(scriptProcs[i].Name, headerProcs[i].Name);
                    }

                    // rename variables immediately
                    RenameFunctionVariables(scriptProcs[i], headerProcs[i]);
                }
            }

            if (result.HasFlag(Match.LocalVariables) && header.Locals != null)
            {
                // add local variables to list of renames.
                // (there may be more header locals than script locals)
                foreach (var i in script.Locals.Keys)
                {
                    if (script.Locals[i].Name != header.Locals[i].Name)
                    {
                        scriptwideRenames.Add(script.Locals[i].Name, header.Locals[i].Name);
                    }
                }
            }

            // kinda inappropriate to use this for renaming,
            // but i don't have a script-specific GlobalRenamer.
            VariableRenamer.Run(script.Root, scriptwideRenames);
        }

        static void RenameFunctionVariables(Function script, Original.Function header)
        {
            var renames = new Dictionary<string, string>();
            if (script.Parameters.Count == header.ParameterCount)
            {
                for (int i = 0; i < script.Parameters.Count; i++)
                {
                    if (script.Parameters[i].Name != header.Parameters[i])
                    {
                        renames.Add(script.Parameters[i].Name, header.Parameters[i]);
                    }
                }
            }
            if (script.Parameters.Count == header.ParameterCount &&
                script.Temps.Count == header.TempCount)
            {
                for (int i = 0; i < script.Temps.Count; i++)
                {
                    if (script.Temps[i].Name != header.Temps[i].Name)
                    {
                        renames.Add(script.Temps[i].Name, header.Temps[i].Name);
                    }
                }
            }
            VariableRenamer.Run(script.Node, renames);
        }

        static Match CompareHeader(Game game, Script script, Original.Script header)
        {
            var result = Match.None;
            if (DoExportsMatch(game, script, header))
            {
                result |= Match.Exports;
            }
            if (DoFunctionsMatch(game, script, header, true))
            {
                result |= Match.PublicProcedures;
            }
            if (DoFunctionsMatch(game, script, header, false))
            {
                result |= Match.LocalProcedures;
            }
            if (script.Number != 0 && DoLocalVariablesMatch(script, header))
            {
                result |= Match.LocalVariables;
            }
            return result;
        }

        static bool DoExportsMatch(Game game, Script script, Original.Script header)
        {
            if (script.Exports.Count != header.ExportCount)
            {
                return false;
            }

            foreach (var scriptExport in script.Exports)
            {
                if (!header.Exports.ContainsKey(scriptExport.Key))
                {
                    if (Log.Level > LogLevel.Debug)
                    {
                        Log.Debug(game, script + " has export " + scriptExport.Key + ", header " + header + " does not");
                    }
                    return false;
                }

                // ignore game name in script 0. it changes between versions
                if (script.Number == 0 && scriptExport.Key == 0) continue;

                string sName = scriptExport.Value;
                var sObj = script.Objects.FirstOrDefault(o => o.Name == sName);
                string hName = header.Exports[scriptExport.Key];

                if (sObj != null)
                {
                    // export is an object. names must match
                    if (sObj.PrintName != hName)
                    {
                        if (Log.Level > LogLevel.Debug)
                        {
                            Log.Debug(game, script + ": export " + scriptExport.Key + " named " + sName +
                                ", header " + header + " named " + hName);
                        }
                        return false;
                    }
                }
                else
                {
                    // export is a procedure
                    var sProc = script.Procedures.First(p => p.Name == sName);
                    var hProc = header.Functions.FirstOrDefault(f => f.Type == FunctionType.Procedure &&
                                                                     f.Name == hName);
                    if (hProc == null)
                    {
                        if (Log.Level > LogLevel.Debug)
                        {
                            Log.Debug(game, script + ": export " + scriptExport.Key + " is " + sName +
                                ", header " + header + " is object " + hName);
                        }
                        return false;
                    }

                    // parameter counts must match
                    if (sProc.Parameters.Count != hProc.ParameterCount)
                    {
                        if (Log.Level > LogLevel.Debug)
                        {
                            Log.Debug(game, script + ": export " + scriptExport.Key + " is " + sName +
                               " with " + sProc.Parameters.Count + ", header " + header + " is " + hName +
                               " with " + hProc.ParameterCount);
                        }
                        return false;
                    }
                }
            }

            return true;
        }

        static bool DoFunctionsMatch(Game game, Script script, Original.Script header, bool usePublic)
        {
            var scriptFunctions = new List<Function>(script.Procedures.Count);
            foreach (var f in script.Procedures)
            {
                bool isPublic = script.Exports.ContainsValue(f.Name);
                if (usePublic == isPublic)
                {
                    scriptFunctions.Add(f);
                }
            }
            var headerFunctions = new List<Original.Function>();
            foreach (var f in header.Functions.Where(f => f.Type == FunctionType.Procedure))
            {
                bool isPublic = header.ExportCount > 0 && header.Exports.ContainsValue(f.Name);
                if (usePublic == isPublic)
                {
                    headerFunctions.Add(f);
                }
            }
            if (scriptFunctions.Count != headerFunctions.Count)
            {
                return false;
            }
            for (int i = 0; i < scriptFunctions.Count; i++)
            {
                if (!DoFunctionsMatch(scriptFunctions[i], headerFunctions[i]))
                {
                    if (Log.Level >= LogLevel.Debug)
                    {
                        Log.Debug(game, script + ": function mismatch: " + scriptFunctions[i].Name +
                            "(p: " + scriptFunctions[i].Parameters.Count + " t: " + scriptFunctions[i].Temps.Count + ") " +
                            "!= " +
                            headerFunctions[i].Name +
                            "(p: " + headerFunctions[i].ParameterCount + " t: " + headerFunctions[i].TempCount + ")");
                    }

                    return false;
                }
            }
            return true;
        }

        // for now, not taking temp arrays into account
        static bool DoFunctionsMatch(Function a, Original.Function b)
        {
            return a.Parameters.Count == b.ParameterCount &&
                   a.Temps.Count == b.TempCount;
        }

        static bool DoLocalVariablesMatch(Script script, Original.Script header)
        {
            return script.Locals.Count == header.LocalCount &&
                   script.Locals.Keys.All(k => header.Locals.ContainsKey(k));
        }

        static void DoObjectMethods(Script script, Original.Script[] headers)
        {
            var results = new Dictionary<Original.Function, MethodMatch>();
            var methodScriptNumbers = new Dictionary<Original.Function, int>();
            foreach (var sObject in script.Objects)
            {
                string sObjectName = sObject.PrintName;
                foreach (var sMethod in sObject.Methods)
                {
                    results.Clear();
                    methodScriptNumbers.Clear();

                    foreach (var header in headers)
                    {
                        var hMethod = (from f in header.Functions
                                       where f.Type == FunctionType.Method &&
                                             f.Object == sObjectName &&
                                             f.Name == sMethod.Name
                                       select f).FirstOrDefault();
                        if (hMethod == null) continue;

                        var result = MethodMatch.None;
                        if (sMethod.Parameters.Count == hMethod.ParameterCount)
                        {
                            result |= MethodMatch.Params;
                        }
                        if (sMethod.Temps.Count == hMethod.TempCount)
                        {
                            result |= MethodMatch.Temps;
                        }
                        results[hMethod] = result;
                        methodScriptNumbers[hMethod] = header.Number;
                    }
                    if (results.Any())
                    {
                        var bestResult = (from r in results
                                          orderby r.Value descending,
                                                  methodScriptNumbers[r.Key] == script.Number descending,
                                                  methodScriptNumbers[r.Key] // last minute tie breaker just in case
                                          select r).First();
                        if (bestResult.Value.HasFlag(MethodMatch.Params))
                        {
                            RenameFunctionVariables(sMethod, bestResult.Key);
                        }
                    }
                }
            }
        }
    }
}
