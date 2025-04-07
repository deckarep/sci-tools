using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SCI.Resource;

// So You Want To Decompile Everything...

namespace SCI.Decompile
{
    public class Decompiler
    {
        // Settings
        public bool CatchExceptions = true;
        public bool RunTests = false;
        public bool DumpGraphs = false;

        public List<string> AllowedFunctions = new List<string>();
        public List<string> BannedFunctions = new List<string>();

        public bool CreateGameIni = false;
        public bool CreateScoFiles = false;
        public string ScriptSubDirectory = "src";

        // Statistics
        public int ScriptSuccessCount;
        public int ScriptCount;

        public int FunctionSuccessCount;
        public int FunctionCount;

        public int SolvedLoopCount;
        public int LoopCount;

        public int SolvedLoopFunctionCount;
        public int LoopFunctionCount;

        Game game;

        public void Run(Game game, string outputDirectory = null)
        {
            this.game = game;

            var symbols = new Symbols(game);
            var scripts = new List<ScriptSummary>();
            var cfa = new ControlFlowAnalysis();
            foreach (var script in game.Scripts)
            {
                // When running tests, skip scripts that don't have a test case
                if (RunTests && EligibleTestCases.All(t => t.Script != script.Number))
                    continue;

                // Include script
                ScriptCount++;
                var scriptSummary = new ScriptSummary(script);
                scripts.Add(scriptSummary);

                // Detect object super classes for generating "use" statements
                DetectSuperClassUsage(scriptSummary);

                // Decompile each function
                bool anyFunctionFailures = false;
                foreach (var function in script.Functions)
                {
                    // When running tests, skip functions that don't have a test case
                    if (RunTests && EligibleTestCases.All(t => t.Function != function.FullName))
                        continue;

                    // Skip this function if it's not allowed (for testing)
                    if ((AllowedFunctions.Any() && !AllowedFunctions.Contains(function.FullName)) ||
                        BannedFunctions.Contains(function.FullName))
                        continue;

                    // Include function
                    FunctionCount++;
                    var functionSummary = new FunctionSummary(function);
                    scriptSummary.Functions.Add(function, functionSummary);

                    // Preprocessing
                    List<SwitchSummary> switches = null;
                    List<LoopSummary> loops = null;
                    int preprocessingUnsolvedLoopCount = 0;
                    try
                    {
                        // Create InstructionList from bytecode
                        functionSummary.Instructions = InstructionList.Parse(function);

                        // Delete debug instructions and patch branches that pointed to them
                        DebugInstructions.Remove(functionSummary.Instructions);

                        // Apply workarounds if this is a known problematic function
                        Workarounds.Patch(functionSummary.Instructions);

                        // Detect variable usage and external script usage from instructions.
                        // These are two unrelated things; maybe I should separate them.
                        // Variable usage is necessary for the function header and array
                        // identification, script usage is necessary for "use" statements.
                        DetectScriptAndVariableUsage(scriptSummary, functionSummary);

                        // SCI Companion / SCI Studio compatibility hacks
                        ThirdPartyHacks.BtFixups(functionSummary.Instructions);
                        bool hasPprev = ThirdPartyHacks.FixupMathAssignments(functionSummary.Instructions);

                        // Delete pprev's bnt instructions.
                        // Makes n-ary comparisons opaque to Control Flow Analysis;
                        // that means a cleaner graph and less confusion.
                        if (hasPprev)
                        {
                            PprevFixups.Fixup(functionSummary.Instructions);
                        }

                        // Fix buggy branches created by Sierra's compiler bugs.
                        BuggyBranches.Patch(functionSummary.Instructions);

                        // Deoptimize *many* bnt instructions, using bt as part of the detection.
                        // Control Flow Analysis relies on this to not get confused. (Me too!)
                        BtFixups.Fixup(functionSummary.Instructions);

                        // Detect all switches, and deoptimize branches that escape case blocks.
                        switches = SwitchDetection.Detect(functionSummary.Instructions);

                        // Delete dead branches caused by weird code. They add nothing, but do
                        // confuse Control Flow Analysis. I prefer preprocessing cleanup over
                        // adding more complexity to Control Flow Analysis.
                        // Must do this after switch detection; switch structure contains branches
                        // that look dead due to multiple compiler bugs.
                        DeadBranches.Remove(functionSummary.Instructions);

                        // Detect all loops. This does no analysis; just back-jump detection.
                        loops = LoopDetection.Detect(function, functionSummary.Instructions);

                        // Another Companion / Studio hack, but this one requires knowledge of loops.
                        ThirdPartyHacks.BntFixups(functionSummary.Instructions, loops);

                        // Solve loops. (CFA will take care of any remaining unsolved bnt instructions)
                        if (loops.Any())
                        {
                            // Record loop counts first, in case loop deduction or seduction throws
                            LoopCount += loops.Count;
                            LoopFunctionCount++;

                            // Deduce provable things about all loops. For 88% of SCI loops, this solves everything.
                            LoopDeduction.Deduce(loops, switches, functionSummary.Instructions);

                            // Make plausible guesses about most remaining unsolved elements in all loops.
                            LoopSeduction.Seduce(loops, switches);

                            // Record the number of solved loops.
                            int solvedLoops = loops.Count(l => l.IsSolved());
                            SolvedLoopCount += solvedLoops;

                            // Record this loop function as solved if all its loops were solved.
                            // Otherwise, record the number of loops for CFA to solve with a graph.
                            if (solvedLoops == loops.Count)
                            {
                                SolvedLoopFunctionCount++;
                            }
                            else
                            {
                                preprocessingUnsolvedLoopCount = loops.Count - solvedLoops;
                            }
                        }
                    }
                    catch (Exception ex) when (CatchExceptions)
                    {
                        Log.Error(function, "Decompiling failed during preprocessing: " + ex.Message);
                        functionSummary.Error = ex;
                        anyFunctionFailures = true;
                        // if we couldn't even create an instruction list, nothing more to do
                        if (functionSummary.Instructions == null) continue;
                    }

                    // Find all the leaders.
                    // Do this even if preprocessing failed, so that the graph can be dumped.
                    functionSummary.Leaders = ControlFlowAnalysis.GetLeaders(functionSummary.Instructions, switches, loops);

                    // Dump the function to a graphviz file if we're in graph dumping mode
                    if (DumpGraphs)
                    {
                        DumpGraph(functionSummary, loops, symbols, outputDirectory);
                        continue;
                    }

                    // If preprocessing failed then there's nothing left to do.
                    if (functionSummary.Error != null) continue;

                    try
                    {
                        // Control Flow Analysis
                        functionSummary.Cfg = ControlFlowAnalysis.CreateGraph(functionSummary.Instructions, functionSummary.Leaders);
                        functionSummary.SolvedCfg = cfa.SolveFunction(function, functionSummary.Cfg, switches, loops);

                        // Abstract Syntax Tree Building / Instruction Consumption
                        functionSummary.Ast = AstBuilder.Run(functionSummary.SolvedCfg, functionSummary.Instructions, function, symbols, functionSummary.ParameterCount);

                        // Abstract Syntax Tree Cleanup
                        if (loops.Any()) // optimization, no need for loop visitors on 95% of functions
                        {
                            Ast.LoopCleanup.Run(function, functionSummary.Ast);
                        }
                        var ifThenToAndConverter = new Ast.IfThenToAndConverter();
                        functionSummary.Ast.Accept(ifThenToAndConverter);
                        functionSummary.Ast.Accept(new Ast.NaryReducer());
                        functionSummary.Ast.Accept(new Ast.CondCreator());
                        functionSummary.Ast.Accept(new Ast.MathAssignmentCreator());
                        var returnCleaner = new Ast.ReturnCleaner(function.Name);
                        functionSummary.Ast.Accept(returnCleaner);
                        returnCleaner.FinishCleaning(functionSummary.Ast.Children.LastOrDefault());
                        if (returnCleaner.HasReturnValues())
                        {
                            // run IfThenToAndConverter again to handle newly created:
                            //     (return (if a then b)) => (return (and a b))
                            functionSummary.Ast.Accept(ifThenToAndConverter);
                        }
                        functionSummary.Ast.Accept(new Ast.CopyPruner());
                        functionSummary.Ast.Accept(new Ast.ForContIfFinder());

                        FunctionSuccessCount++;

                        // If we made it this far, CFA solved the remaining loops
                        if (preprocessingUnsolvedLoopCount > 0)
                        {
                            SolvedLoopCount += preprocessingUnsolvedLoopCount;
                            SolvedLoopFunctionCount++;
                        }
                    }
                    catch (Exception ex) when (CatchExceptions)
                    {
                        Log.Error(function, "Decompiling failed: " + ex.Message);
                        functionSummary.Error = ex;
                        anyFunctionFailures = true;
                    }
                }
                if (!anyFunctionFailures) ScriptSuccessCount++;
            }

            // Don't write any decompiled files if we dumped graphs
            if (DumpGraphs) return;

            // Create output directories
            if (string.IsNullOrWhiteSpace(outputDirectory))
            {
                outputDirectory = game.Directory;
            }
            string scriptDirectory = Path.Combine(outputDirectory, ScriptSubDirectory);
            Directory.CreateDirectory(scriptDirectory);

            // Write files
            foreach (var scriptSummary in scripts)
            {
                // figure out which locals are arrays. used by ScriptWriter and ScoWriter.
                scriptSummary.CalculateLocalArrayLengths();

                var scriptWriter = new ScriptWriter(scriptSummary, symbols);
                string scriptFile = Path.Combine(scriptDirectory, symbols.Script(scriptSummary.Script) + ".sc");
                scriptWriter.Write(scriptFile);
                if (CreateScoFiles)
                {
                    ScoWriter.Write(scriptSummary, symbols, scriptFile + "o");
                }
            }
            if (CreateGameIni)
            {
                GameIniWriter.Write(game, symbols, outputDirectory);
            }
        }

        IEnumerable<TestCase> EligibleTestCases
        {
            get
            {
                return 
                    from t in Test.Cases
                    where t.Game == game.Name &&
                          !BannedFunctions.Contains(t.Function) &&
                          (!AllowedFunctions.Any() ||
                           AllowedFunctions.Contains(t.Function))
                    select t;
            }
        }

        void DetectSuperClassUsage(ScriptSummary scriptSummary)
        {
            foreach (var obj in scriptSummary.Script.Objects)
            {
                var superClass = game.GetClass(obj);
                if (superClass != null)
                {
                    scriptSummary.UsingScripts.Add(superClass.Script.Number);
                }
            }
        }

        // Scans a function's bytecode for ScriptSummary and FunctionSummary things:
        // - External script usage via public procedure calls
        // - External script usage via the `class` opcode
        // - Local variable usage for identifying arrays and unused variables
        // - Temp variable count via link opcode
        // - Temp variable usage for identifying arrays and unused variables
        // - Parameter usage for identifying the parameter count
        void DetectScriptAndVariableUsage(ScriptSummary script, FunctionSummary function)
        {
            foreach (var i in function.Instructions)
            {
                // number of temp parameters = link operand
                if (i.Operation == Operation.link)
                {
                    if (function.DeclaredTempCount != 0) throw new Exception("Multiple link instructions");
                    function.DeclaredTempCount = i.Parameters[0];
                    continue;
                }

                // external proc calls require using statements.
                if (i.Operation == Operation.calle)
                {
                    script.UsingScripts.Add(i.Parameters[0]);
                    continue;
                }
                if (i.Operation == Operation.callb)
                {
                    script.UsingScripts.Add(0);
                    continue;
                }

                // class instructions require using statements.
                // it might be a non-existent class.
                if (i.Operation == Operation.class_)
                {
                    var cls = game.GetClass(script.Script, i.Parameters[0]);
                    if (cls != null)
                    {
                        script.UsingScripts.Add(cls.Script.Number);
                    }
                    continue;
                }

                // &rest instructions reveal the true declared parameter count.
                // this is needed if a parameter is declared but never used.
                // that doesn't sound like it would matter, but if &rest is
                // used, then that unused parameter declaration affects how
                // many parameters get passed to the next function.
                // This causes subtle script bugs! Dagger's dino bone doesn't
                // respond to clicks because of this.
                // &rest can also have an operand lower than param-count-plus-argc
                // if used in the (&rest param1) format that i've only see in Realm.
                if (i.Operation == Operation.rest)
                {
                    // &rest operand can never be zero
                    if (i.Parameters[0] <= 0) throw new Exception("&rest instruction has invalid operand: " + i.Parameters[0]);

                    int parameterCount = i.Parameters[0] - 1;
                    if (parameterCount > function.ParameterCount)
                    {
                        function.ParameterCount = parameterCount;
                    }
                    continue;
                }

                // record local/temp/parameter usage
                var type = i.GetVariableType();
                if (script.Script.Number != 0 && type == VariableType.Local)
                {
                    int index = i.GetVariableIndex();
                    if (index >= script.Script.Locals.Count) throw new Exception("Out of bounds local index: " + i);

                    // treat lea as a complex variable here, because @local1 and @[local1 ...]
                    // have the same implication; local1 is the head of an array.
                    if (i.Operation == Operation.lea || i.IsComplexVariable())
                    {
                        script.ComplexLocals.Add(index);
                    }
                    else
                    {
                        script.Locals.Add(index);
                    }
                }
                else if (type == VariableType.Temp)
                {
                    int index = i.GetVariableIndex();

                    // third party compilers can declare too few temp variables.
                    // track the max used temp so that ScriptWriter can annotate the bug.
                    function.MaxUsedTempIndex = Math.Max(index, function.MaxUsedTempIndex);

                    // treat lea as a complex variable here, because @temp1 and @[temp1 ...]
                    // have the same implication; temp1 is the head of an array.
                    if (i.Operation == Operation.lea || i.IsComplexVariable())
                    {
                        function.ComplexTemps.Add(index);
                    }
                    else
                    {
                        function.Temps.Add(index);
                    }
                }
                else if (type == VariableType.Parameter)
                {
                    // parameter count is the highest index in a parameter instruction
                    function.ParameterCount = Math.Max(function.ParameterCount, i.GetVariableIndex());
                }
                else if (type == VariableType.Global)
                {
                    // using a global requires using script 0
                    script.UsingScripts.Add(0);
                }
            }
        }

        public void ResetStatistics()
        {
            FunctionSuccessCount = 0;
            FunctionCount = 0;
            SolvedLoopCount = 0;
            LoopCount = 0;
            LoopFunctionCount = 0;
            SolvedLoopFunctionCount = 0;
        }

        void DumpGraph(FunctionSummary functionSummary, List<LoopSummary> loops, Symbols symbols, string outputDirectory)
        {
            // uncomment to only dump functions with unsolved loops
            //if (loops == null || !loops.Any() || loops.All(l => l.IsSolved())) continue;

            // don't dump boring functions; they have nothing to teach
            if (functionSummary.Leaders.Count == 1) return;

            Function function = functionSummary.Function;
            string graphFileName = Path.Combine(outputDirectory, symbols.Script(function.Script) + "_" + symbols.Sanitize(function.FullName) + ".gv");
            try
            {
                Cfg.Graph cfg = ControlFlowAnalysis.CreateGraph(functionSummary.Instructions, functionSummary.Leaders);

                // run BranchGraphFixup so I can see what effect it has.
                // this is normally a part of CFA so i'm doing a few things that CFA would have done.
                // this can only be done on the entire function if there are no loops.
                if (loops != null && !loops.Any()) // loops can be null if preprocessing failed
                {
                    // add an End node to the CFG; this will happen during CFA on every subgraph
                    var ret = cfg.GetByInstruction(functionSummary.Instructions.Last);
                    cfg.Add(Cfg.EdgeType.Follow, ret, cfg.End);

                    // calculate domination.
                    // use the naive algorithm for this because it allows unreachable nodes.
                    // otherwise i'd have to remove them, but the point of graph dumping
                    // is to show everything.
                    var doms = new Cfg.NaiveDominance(cfg);

                    // deoptimize instructions with a graph
                    BranchGraphFixup.Fixup(cfg, doms, function);
                }

                Directory.CreateDirectory(outputDirectory);
                GraphDump.Write(function, cfg, graphFileName);
            }
            catch (Exception ex) when (CatchExceptions)
            {
                Log.Error(function, "Dumping graph failed: " + ex.Message);
            }
        }
    }

    class ScriptSummary
    {
        public Script Script;
        public HashSet<int> UsingScripts;  // script numbers that this script depends on
        public HashSet<int> Locals;        // indexes referenced by non-array instructions
        public HashSet<int> ComplexLocals; // indexes referenced by array instructions
        public Dictionary<int, int> LocalArrayLengths; // lengths of indexes referenced by instructions
        public Dictionary<Function, FunctionSummary> Functions;

        public ScriptSummary(Script script)
        {
            Script = script;
            UsingScripts = new HashSet<int>();
            Locals = new HashSet<int>();
            ComplexLocals = new HashSet<int>();
            Functions = new Dictionary<Function, FunctionSummary>();

            // LSCI injects local0 in every script with argument count.
            // flag it as an individual here so that it's never treated
            // as part of an array. ScriptWriter will exclude it.
            if (script.Game.ScriptFormat == ScriptFormat.LSCI && script.Locals.Count > 0)
            {
                Locals.Add(0);
            }
        }

        public override string ToString() { return Script.ToString(); }

        public void CalculateLocalArrayLengths()
        {
            // Calculate the length of every local that will be emitted in code.
            // A normal local will have a length of 1, an array > 1.
            // For example, if a script has 5 locals compiled in, but analyzing
            // reveals that only the first two are used, and the second is used
            // as an array, then LocalArrayLengths will contain:
            //   0: 1     local0
            //   1: 4     [local1 4]

            LocalArrayLengths = new Dictionary<int, int>();
            for (int i = 0; i < Script.Locals.Count; i++)
            {
                int localIndex = i;

                // determine if this is an array or a single.
                // consecutive unused locals are treated as arrays.
                bool isArray;
                if (Script.Number == 0)
                {
                    // globals are always singles
                    isArray = false;
                }
                else if (ComplexLocals.Contains(i))
                {
                    // array access makes this an array
                    isArray = true;
                }
                else if (Locals.Contains(i))
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
                    for (int j = i + 1; j < Script.Locals.Count; j++)
                    {
                        if (!Locals.Contains(j) &&
                            !ComplexLocals.Contains(j))
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

                LocalArrayLengths.Add(localIndex, arraySize);
            }
        }
    }

    class FunctionSummary
    {
        public Function Function;
        public InstructionList Instructions;
        public List<int> Leaders;
        public Cfg.Graph Cfg;
        public Cfg.Graph SolvedCfg;
        public Ast.Node Ast;

        public int ParameterCount;    // detected from param instructions and &rest
        public int DeclaredTempCount; // as declared by link instruction. could be wrong!
        public int MaxUsedTempIndex;  // to handle third party compiler errors
        public HashSet<int> Temps;        // indexes referenced by non-array instructions
        public HashSet<int> ComplexTemps; // indexes referenced by array instructions

        public Exception Error;

        public FunctionSummary(Function function)
        {
            Function = function;
            MaxUsedTempIndex = -1;
            Temps = new HashSet<int>();
            ComplexTemps = new HashSet<int>();
        }

        public override string ToString() { return Function.ToString(); }
    }

    static class GameIniWriter
    {
        public static void Write(Game game, Symbols symbols, string outputDirectory)
        {
            var ini = new StringBuilder();
            ini.AppendLine("[Game]");
            ini.AppendLine("Language=sci");
            ini.AppendLine("[Script]");
            foreach (var script in game.Scripts.OrderBy(s => s.Number))
            {
                ini.AppendFormat("n{0:000}=", script.Number);
                // SCI Companion doesn't sanitize these strings, so you get
                // "Talking Bear" in game.ini instead of "Talking_Bear".
                // My symbols are sanitized, but this difference doesn't matter.
                ini.AppendLine(symbols.Script(script));
            }
            File.WriteAllText(Path.Combine(outputDirectory, "game.ini"), ini.ToString());
        }
    }

    static class ScoWriter
    {
        public static void Write(ScriptSummary scriptSummary, Symbols symbols, string outputFile)
        {
            var script = scriptSummary.Script;
            if (script.Game.ScriptFormat == ScriptFormat.SCI3) return; // skip SCI3

            // header
            var sco = new ScriptObjectFile();
            sco.SeparateHeap = (script.Game.ScriptFormat == ScriptFormat.SCI11);
            sco.Number = (UInt16)script.Number;

            // exports
            sco.Exports = new List<ScriptObjectFile.ExportInfo>(script.Exports.Count);
            for (int i = 0; i < script.Exports.Count; i++)
            {
                var obj = script.GetExportedObject(i);
                if (obj != null)
                {
                    var export = new ScriptObjectFile.ExportInfo();
                    sco.Exports.Add(export);
                    export.Number = (UInt16)i;
                    export.Name = symbols.Object(obj);
                    continue;
                }
                var proc = script.GetExportedProcedure(i);
                if (proc != null)
                {
                    var export = new ScriptObjectFile.ExportInfo();
                    sco.Exports.Add(export);
                    export.Number = (UInt16)i;
                    export.Name = proc.Name;
                }
            }

            // classes
            sco.Classes = new List<ScriptObjectFile.ClassInfo>();
            foreach (var class_ in script.Objects.Where(o => o.IsClass))
            {
                var classInfo = new ScriptObjectFile.ClassInfo();
                sco.Classes.Add(classInfo);
                classInfo.Name = symbols.Class(class_);
                classInfo.Species = class_.Species;
                classInfo.Super = class_.SuperClass;
                int namePropertyOffset = script.Game.ScriptFormat == ScriptFormat.SCI0 ? 3 : 8;
                if (class_.Properties.Count > namePropertyOffset)
                {
                    classInfo.HasNameProperty = true;
                    classInfo.Properties = new List<ScriptObjectFile.PropertyInfo>(class_.Properties.Count - namePropertyOffset);
                    // don't include name property
                    for (int i = namePropertyOffset +  1; i < class_.Properties.Count; i++)
                    {
                        var property = new ScriptObjectFile.PropertyInfo();
                        classInfo.Properties.Add(property);
                        property.Selector = class_.Properties[i].Selector;
                        property.Value = (UInt16)class_.Properties[i].Value;
                    }
                }
                classInfo.Methods = class_.Methods.Select(m => m.Selector).ToList();
            }

            // locals - one for each, but leave blank if it doesn't have a symbol because it's in an array
            sco.Locals = new List<string>(script.Locals.Count);
            for (int i = 0; i < script.Locals.Count; i++)
            {
                if (scriptSummary.LocalArrayLengths.ContainsKey(i))
                {
                    sco.Locals.Add(symbols.Variable(script, null, VariableType.Local, i));
                }
                else
                {
                    sco.Locals.Add("");
                }
            }

            byte[] buffer = sco.Generate();
            File.WriteAllBytes(outputFile, buffer);
        }
    }
}
