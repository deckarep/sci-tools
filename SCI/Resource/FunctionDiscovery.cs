using System;
using System.Collections.Generic;
using System.Linq;

// Function Discovery is complicated enough to warrant its own file.
//
// Phases:
// 1. Scan object methods and the procs they reach
// 2. Scan exported code offsets and the procs they reach
// 3. Scan the gaps for unused localprocs and the localprocs they reach
//
// Results:
// - Sets Script.Procedures and Script.Functions
// - Sets Function.Code and Function.Errors
// - Sets individual Script.Exports to Invalid
//
// The phases reflect the things I trust, from most trustworthy to least:
// 1. Object methods: Code offsets in object structures are impeccable.
// 2. Procedures called by object methods: call instructions can be
//    trusted within the context of a trusted function.
//    This includes procedures called by those procedures, etc...
// 3. Export offsets: So many bogus values, but at this point in
//    processing so much has been established that detecting invalid
//    offsets is reliable, to the point where I can't find any more.
// ------ Now we're talking about unreachable code ------
// 4. Unreachable procedures for which a call instruction has been
//    discovered in an unreachable gap of a code block.
// 5. Unreachable procedures with no references found by parsing
//    unreachable gaps in code blocks.
//
// This is different from how most SCI tools find functions.
// First I'm scanning methods and all the procedures they reach, and only
// then do I look at the exports with great skepticism and validation.
// Because exports can't be trusted!
//
// As a result, this code finds functions that other SCI tools don't,
// and it doesn't hallucinate function from bogus (empty) exports.

namespace SCI.Resource
{
    partial class Script
    {
        public void DiscoverFunctions()
        {
            // LSCI makes function discovery simple, so use alternate method
            if (Game.ScriptFormat == ScriptFormat.LSCI)
            {
                DiscoverFunctionsLSCI();
                return;
            }

            Procedures = new List<Procedure>();
            Functions = new List<Function>();

            // figure out the boundaries for code in the entire script
            int codeStartPosition;
            int codeEndPosition;
            CalculateScriptCodeBoundaries(out codeStartPosition, out codeEndPosition);

            //
            // Phase 1: Object methods and reachable procedures
            //

            // add all object methods to the list of functions to scan
            Functions.AddRange(Objects.SelectMany(o => o.Methods));

            // scan all methods and the localprocs that are discovered
            var parser = new ByteCodeParser();
            for (int i = 0; i < Functions.Count; ++i) // no foreach, Scan() mutates
            {
                Scan(Functions[i], codeStartPosition, codeEndPosition, parser);
            }

            //
            // Phase 2: Exports and reachable procedures
            //

            // any code export that is a duplicate of an earlier export is invalid
            for (int i = 1; i < Exports.Count; ++i)
            {
                if (Exports[i].Type == ExportType.Code)
                {
                    for (int j = 0; j < i; ++j)
                    {
                        if (Exports[i].Value == Exports[j].Value)
                        {
                            Exports[i].Type = ExportType.Invalid;
                            break;
                        }
                    }
                }
            }

            // scan code exports
            // these export offsets aren't trustworthy, but we've already scanned
            // object methods, which are trustworthy, so we know a bunch of code
            // ranges that a valid export can't start in.
            // the trick here is to go in order of offset. if a bogus export points
            // in the middle of a real exported function, we'll catch that because
            // we already processed the first one.
            foreach (var export in Exports.OrderBy(e => e.Value))
            {
                // test here so as to not alter the enumeration
                if (export.Type != ExportType.Code) continue;

                UInt32 codePosition = export.Value;
                UInt16 exportNumber = (UInt16)Exports.IndexOf(export);

                // we may already know about this procedure from an earlier scan
                var procedure = Procedures.FirstOrDefault(p => p.CodePosition == codePosition);
                if (procedure != null)
                {
                    if (!procedure.IsPublic)
                    {
                        // we knew about the procedure but didn't know it was public
                        procedure.SetExportNumber(exportNumber);
                    }
                    continue;
                }

                // if the offset lands in a known function then it's invalid.
                // this would be an incorrect conclusion if a code buffer started with a
                // localproc that we don't know about yet, but so far i haven't seen that.
                // if that's a problem then this will get more complicated, and i might
                // need to scan unused areas first and then reconcile against exports.
                if (Functions.Any(f => f.CodePosition <= codePosition &&
                                       codePosition < f.CodePosition + f.Code.Length))
                {
                    export.Type = ExportType.Invalid;
                    continue;
                }

                // create the procedure and scan it.
                int prevFuncCount = Functions.Count;
                procedure = new Procedure(this, true, exportNumber, codePosition);
                Procedures.Add(procedure);
                Functions.Add(procedure);
                Scan(procedure, codeStartPosition, codeEndPosition, parser);

                // TODO: how to handle one of these that has errors?
                // in practice, if they're all fine, then great.
                // i could roll back the functions it discovered.

                // recursively scan any newly discovered procedures
                for (int i = prevFuncCount + 1; i < Functions.Count; ++i)
                {
                    Scan(Functions[i], codeStartPosition, codeEndPosition, parser);
                }
            }

            //
            // Phase 3: Unreachable local procedures via Gap Analysis
            //

            // scan all gaps in code blocks for call instructions.
            // if a gap starts with zero padding that's okay, each byte will
            // be treated as a bnot instruction.
            // it turns out that this first step turns up very few functions
            // in practice. maybe i should replace it just scanning gaps and
            // then scanning discovered functions.
            int oldFunctionCount = Functions.Count;
            var gaps = FindGaps(codeStartPosition, codeEndPosition);
            foreach (var gap in gaps)
            {
                parser.Parse(Span, gap.Start, gap.End, Game.ByteCodeVersion);
                parser.StopOnFinalReturn = false; // do everything!
                while (parser.Next())
                {
                    // only care about calls to scripts within this script
                    if (parser.Operation != Operation.call) continue;

                    Int16 offset = parser.GetSignedOperand(0);
                    int codePosition = parser.Position + parser.GetInstructionLength() + offset;

                    // skip known procedures
                    if (Procedures.Any(p => p.CodePosition == codePosition)) continue;

                    // call offset must point into a gap
                    if (!gaps.Any(g => g.Start <= codePosition && codePosition < g.End)) continue;

                    // found an unreachable procedure!
                    var procedure = new Procedure(this, false, UInt16.MaxValue, (UInt32)codePosition);
                    Procedures.Add(procedure);
                    Functions.Add(procedure);
                }
            }

            // scan all the newly found procedures (again, very few in practice)
            for (int i = oldFunctionCount; i < Functions.Count; ++i)
            {
                Scan(Functions[i], codeStartPosition, codeEndPosition, parser);
            }

            // rescan the gaps if some of them were claimed by newly found functions
            if (oldFunctionCount != Functions.Count)
            {
                gaps = FindGaps(codeStartPosition, codeEndPosition);
            }

            // try to turn gaps into as many procedures as possible.
            // this is what really turns up functions. each gap is scanned from the start,
            // and if it can be parsed cleanly to a return then that's a function.
            oldFunctionCount = Functions.Count;
            foreach (var gap in gaps)
            {
                int curPos = gap.Start;
                while (curPos < gap.End)
                {
                    // trim leading zeros, these are padding, probably for alignment.
                    while (curPos < gap.End && Span[curPos] == 0)
                    {
                        curPos++;
                    }

                    if (curPos == gap.End) break;

                    // attempt to parse gap as a procedure.
                    // if there are no fatal errors, accept it.
                    var procedure = new Procedure(this, false, UInt16.MaxValue, (UInt32)curPos);
                    Scan(procedure, codeStartPosition, codeEndPosition, parser);
                    if (procedure.Errors == FunctionError.None ||
                        procedure.Errors == FunctionError.OutOfBoundsBranch) // it happens!
                    {
                        Procedures.Add(procedure);
                        Functions.Add(procedure);
                    }
                    else
                    {
                        // there was a fatal error while parsing this gap.
                        // don't attempt to process it any further.
                        break;
                    }

                    curPos += procedure.Code.Length;
                }
            }

            // ignore any procedures we just discovered if they are empty or invalid.
            // the last step of gap analysis is scary: just start parsing and if it
            // cleanly ends on a return statement, use it! in theory that should work
            // great but in practice there's invalid instructions floating around in
            // the gaps. several SCI32 games have empty procedures in between functions
            // that i am certain weren't in original code. phant2, for example, throws
            // line/ret instructions in between a lot of procs/methods in system
            // scripts, like 64999. then there's other garbage like starting with a
            // toss, or accessing locals or temps that don't exist.
            for (int i = oldFunctionCount; i < Functions.Count; i++)
            {
                var procedure = (Procedure)Functions[i];
                bool isEmpty = IsFunctionEmpty(procedure, parser);
                bool isInvalid = false;
                if (!isEmpty)
                {
                    isInvalid = IsProcedureByteCodeInvalid(procedure, (int)procedure.CodePosition + procedure.Code.Length, parser);
                }
                if (isEmpty || isInvalid)
                {
                    Procedures.Remove(procedure);
                    Functions.RemoveAt(i);
                    i--;
                }
            }

            // it's nice to have them sorted in position order
            Procedures.Sort((a, b) => a.CodePosition.CompareTo(b.CodePosition));
            Functions.Sort((a, b) => a.CodePosition.CompareTo(b.CodePosition));
        }

        void Scan(Function function,
                  int codeStartPosition,
                  int codeEndPosition,
                  ByteCodeParser parser)
        {
            // it's crucial to detect functions that branch outside of themselves.
            // SCI's (switch) compiler bug produces many of these, but there's
            // also NRS' deliberate cross-method branches in SQ4 Update easter
            // egg room room 720. we also still have more invalid exports to detect!
            // the first step is to establish a conservative end position for where
            // we know each function can't legally go beyond, so that BCP can
            // detect problems. how we do that depends on the script format.
            //
            // 1. endPosition = the end of the bytecode buffer the function came from.
            int endPosition;
            if (Game.ScriptFormat == ScriptFormat.SCI0)
            {
                // SCI0: the bytecode buffer is the function's code block
                var blocks = ((Script0)Source).Blocks;
                var block = blocks.FirstOrDefault(b => b.Position <= function.CodePosition &&
                                                       function.CodePosition < b.Position + b.Length &&
                                                       b.Type == BlockType.Code);
                if (block == null)
                {
                    // this would mean a localproc was the result of an invalid calle address?
                    throw new Exception("SCI0 procedure " + function.FullName + " doesn't exist within a code block");
                }
                endPosition = block.Position + block.Length;
            }
            else
            {
                // SCI11 / SCI3: there's only one bytecode buffer and we already recorded where it ends
                endPosition = codeEndPosition;
            }

            // 2. whittle endPosition down further by trusting the closest subsequent
            //    method address or local procedure address. we *don't* trust public
            //    procedure addresses for this.
            for (int j = 0; j < Functions.Count; ++j)
            {
                if (Functions[j].CodePosition > function.CodePosition &&
                    Functions[j].CodePosition < endPosition &&
                    (Functions[j] is Method || !((Procedure)Functions[j]).IsPublic))
                {
                    endPosition = (int)Functions[j].CodePosition;
                }
            }

            // parse the function for `call` instructions (also we'll record length)
            parser.Parse(Span, (int)function.CodePosition, endPosition, Game.ByteCodeVersion);
            while (parser.Next())
            {
                // only care about calls to scripts within this script
                if (parser.Operation != Operation.call) continue;

                // TODO: make sure there are no sci3 relocations on these
                Int16 offset = parser.GetSignedOperand(0);
                int codePosition = parser.Position + parser.GetInstructionLength() + offset;

                // validate that the the call offset is within script code boundaries.
                // this could only happen with a deeply broken script. i'm unaware of it ever happening.
                if (codePosition < codeStartPosition || codePosition >= codeEndPosition)
                {
                    // GK1 floppy has sysLogger with apparently truncated bytecode, mismatched selectors
                    Log.Warn(function, "localproc call is out of bounds");
                    function.Errors |= FunctionError.OutOfBoundsCall;
                    continue;
                }

                // skip known procedures
                if (Procedures.Any(p => p.CodePosition == codePosition)) continue;

                // found a procedure!
                var procedure = new Procedure(this, false, UInt16.MaxValue, (UInt32)codePosition);
                Procedures.Add(procedure);
                Functions.Add(procedure);
            }

            // record function size (create Function.Code)
            int codeSize = parser.Position - (int)function.CodePosition;
            function.Code = Span.Slice((int)function.CodePosition, codeSize);

            // record any bytecode parsing errors as function errors
            if (parser.Status.HasFlag(ByteCodeParserStatus.NoReturn)) function.Errors |= FunctionError.Truncated;
            if (parser.Status.HasFlag(ByteCodeParserStatus.OutOfBoundsBranch)) function.Errors |= FunctionError.OutOfBoundsBranch;
            if (parser.Status.HasFlag(ByteCodeParserStatus.UnknownOpcode) ||
                parser.Status.HasFlag(ByteCodeParserStatus.TruncatedInstruction)) function.Errors |= FunctionError.IllegalOpcode;

            /*if (function.Errors != FunctionError.None && function.Errors != FunctionError.OutOfBoundsBranch)
            {
                Log.Warn(function, "Scan errors: " + parser.Status + ", " + function.Errors);
            }*/
        }

        void CalculateScriptCodeBoundaries(out int codeStartPosition, out int codeEndPosition)
        {
            codeStartPosition = 0;
            if (Game.ScriptFormat == ScriptFormat.SCI0)
            {
                // SCI0: set the code boundary to the entire script.
                // it doesn't really matter because SCI0 has code blocks that
                // are per function. that gives me much better boundary checks
                // than SCI11 and SCI3 where there's just one big code buffer.
                codeEndPosition = Span.Length;
            }
            else if (Game.ScriptFormat == ScriptFormat.SCI11)
            {
                var source = (Script11)Source;
                codeStartPosition = source.ByteCodePosition;
                codeEndPosition = source.ByteCodePosition + source.ByteCodeLength;
            }
            else /*if (Game.ScriptFormat == ScriptFormat.SCI3)*/
            {
                var source = (Script3)Source;
                codeStartPosition = (int)source.ByteCodePosition;
                codeEndPosition = (int)(source.ByteCodePosition + source.ByteCodeLength);
            }
        }

        // Validate the bytecode of a procedure, because it might not be real.
        // This function is large and probably overkill; it was first written
        // for the old function discovery algo where it validated public procs
        // from the export table. Invalid export entries often point to the
        // middle of real object methods, so you would get exciting sequences
        // of instructions with a subtle "tell" that they were impossible.
        // The new algo handles exports much better and without instructions.
        // Now this function only validates potential local procs discovered by
        // gap analysis. There aren't many of these, and they tend to be boring.
        // Keep in mind that procedures can exist within objects, so object
        // instructions like "self" and property accessors are indeed valid.
        bool IsProcedureByteCodeInvalid(Procedure procedure, int endPosition, ByteCodeParser parser)
        {
            bool invalid = false;
            bool branchReached = false;
            bool acc = false;
            bool pprev = false;
            int stack = 0;
            UInt16 tempCount = 0;
            parser.Parse(Span, (int)procedure.CodePosition, endPosition, Game.ByteCodeVersion);
            while (!invalid && parser.Next())
            {
                switch (parser.Operation)
                {
                    // invalid opcode: invalid bytecode
                    case Operation.unused:
                        invalid = true;
                        break;

                    // multiple link instructions: invalid bytecode
                    case Operation.link:
                        if (tempCount != 0)
                        {
                            invalid = true;
                        }
                        else
                        {
                            tempCount = parser.GetOperand(0);
                        }
                        break;
                }

                // check for accessing an invalid local or temp.
                // occurs in truncated hoyle3 dos script 995, instructions in gap in rama
                if (Operation.lag <= parser.Operation && parser.Operation <= Operation.minusspi)
                {
                    int variableType = (parser.Operation - Operation.lag) % 4;
                    if (variableType == 1)
                    {
                        // locals
                        int local = parser.GetOperand(0);
                        if (!(local < Locals.Count))
                        {
                            invalid = true;
                        }
                    }
                    else if (variableType == 2)
                    {
                        // temps
                        int temp = parser.GetOperand(0);
                        if (!(temp < tempCount))
                        {
                            // one way this happens is when someone slaps in "return false"
                            // at the start of a function to disable it, and the function has
                            // temp variables. the function will get correctly parsed as:
                            // "link 01, ldi 00, ret" and leave a lot of unreachable code.
                            // then, FunctionDiscovery sees gap as a potential unused function,
                            // but if it references a temp variable then it will fail to decompile
                            // because there is no link instruction. this happens three times
                            // in the realm demo. for extra credit, i could detect when an
                            // unused localproc has this exact error, and only this error, and
                            // expand its predecessors range.
                            invalid = true;
                        }
                    }
                }

                // some things can only be easily validated up until a branch is reached
                if (!branchReached)
                {
                    var flags = parser.Operation.GetFlags();
                    if (flags.HasFlag(OpFlags.ReadsAcc) && !acc) invalid = true;
                    if (flags.HasFlag(OpFlags.WritesAcc)) acc = true;
                    if (flags.HasFlag(OpFlags.ReadsPprev) && !pprev) invalid = true;
                    if (flags.HasFlag(OpFlags.WritesPprev)) pprev = true;
                    if (flags.HasFlag(OpFlags.PeeksStack) && stack == 0) invalid = true;
                    if (flags.HasFlag(OpFlags.PopsStack))
                    {
                        int popAmount = 1; // default
                        switch (parser.Operation)
                        {
                            case Operation.call:
                            case Operation.callk:
                            case Operation.callb:
                            case Operation.calle:
                                // + 1 for param-count parameter that's pushed first
                                popAmount = (parser.GetOperand(parser.Operands.Count - 1) / 2) + 1;
                                break;
                            case Operation.send:
                            case Operation.self:
                            case Operation.super:
                                popAmount = (parser.GetOperand(parser.Operands.Count - 1) / 2);
                                break;
                        }
                        if (popAmount > stack)
                        {
                            invalid = true;
                        }
                        else
                        {
                            stack -= popAmount;
                        }
                    }
                    if (flags.HasFlag(OpFlags.PushesStack)) stack += 1;
                    if (flags.HasFlag(OpFlags.Branches)) branchReached = true;
                }
            }
            return invalid;
        }

        static bool IsFunctionEmpty(Function function, ByteCodeParser parser)
        {
            parser.Parse(function);
            while (parser.Next())
            {
                switch (parser.Operation)
                {
                    case Operation.ret:
                    case Operation.line:
                    case Operation.file:
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }

        class Gap
        {
            public int Start;
            public int End;

            public override string ToString()
            {
                return "Start: " + Start + ", End: " + End;
            }
        }

        List<Gap> FindGaps(int codeStartPosition, int codeEndPosition)
        {
            var gaps = new List<Gap>();
            if (Game.ScriptFormat == ScriptFormat.SCI0)
            {
                foreach (var block in ((Script0)Source).Blocks)
                {
                    if (block.Type == BlockType.Code)
                    {
                        FindGapsInRange(block.Position, block.Position + block.Length, Functions, gaps);
                    }
                }
            }
            else
            {
                FindGapsInRange(codeStartPosition, codeEndPosition, Functions, gaps);
            }
            return gaps;
        }

        void FindGapsInRange(int startPosition, int endPosition, IReadOnlyList<Function> functions, List<Gap> gaps)
        {
            var functionsInRange = from f in functions
                                   where startPosition <=f.CodePosition  &&
                                         f.CodePosition < endPosition
                                   orderby f.CodePosition
                                   select f;

            int curPosition = startPosition;
            foreach (var function in functionsInRange)
            {
                if (function.CodePosition != curPosition)
                {
                    gaps.Add(new Gap { Start = curPosition, End = (int)function.CodePosition });
                }
                curPosition = (int)function.CodePosition + function.Code.Length;
            }
            if (curPosition < endPosition)
            {
                gaps.Add(new Gap { Start = curPosition, End = endPosition });
            }
        }

        // LSCI places exactly one function in every Code block, making it simple to find every
        // function and its boundaries. Even unreferenced localprocs! I didn't bother trying
        // to detect function errors here. I don't even parse the bytecode.
        void DiscoverFunctionsLSCI()
        {
            Procedures = new List<Procedure>();
            Functions = new List<Function>();

            var blocks = ((ScriptL)Source).Blocks;
            var codeBlocks = new HashSet<BlockL>(blocks.Where(b => b.Type == BlockTypeL.Code));

            // create Code span for each object method using block length, remove block from set
            foreach (var method in Objects.SelectMany(o => o.Methods))
            {
                var methodBlock = codeBlocks.First(b => b.Position == method.CodePosition);
                method.Code = Span.Slice((int)method.CodePosition, methodBlock.Length);
                codeBlocks.Remove(methodBlock);
                Functions.Add(method);
            }

            // all remaining code blocks are procedures
            foreach (var block in codeBlocks)
            {
                // find export number
                bool isPublic = false;
                UInt16 exportNumber = UInt16.MaxValue;
                for (UInt16 i = 0; i < Exports.Count; i++)
                {
                    if (Exports[i].Type == ExportType.Code &&
                        Exports[i].Value == block.Position)
                    {
                        isPublic = true;
                        exportNumber = i;
                        break;
                    }
                }

                var procedure = new Procedure(this, isPublic, exportNumber, block.Position);
                procedure.Code = Span.Slice((int)block.Position, block.Length);
                Procedures.Add(procedure);
                Functions.Add(procedure);
            }

            // it's nice to have them sorted in position order
            Procedures.Sort((a, b) => a.CodePosition.CompareTo(b.CodePosition));
            Functions.Sort((a, b) => a.CodePosition.CompareTo(b.CodePosition));
        }
    }
}
