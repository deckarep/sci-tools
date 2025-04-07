using System;
using System.Collections.Generic;
using System.Linq;
using SCI.Resource;

// Detects basic loop boundaries in a simple way that doesn't require graphs.
//
// The end of a loop is a back-jump. This is called the latch. The target of
// the latch is the start of the loop. This could be a condition or the body
// if it's a repeat loop. A back-jump can also be produced by:
// - continue (jmp), but not in a for loop
// - continueif (bt), but not in a for loop
// - optimized jmp
// - optimized bnt
// * I am 90% sure that a bt can never be optimized into a back-jump
//
// Fortunately, the last back-jump is always the latch. This is always a
// jmp instruction, except for DoWhile loops emitted by third party compilers.
// They use bt, so we have to scan for those too.
//
// Loops are the only thing that produce back-jumps, so there is no ambiguity.
//
// I keep having to remind myself of this but Yes, this handles (continue 2).
//
// For loops put the increment expression at the bottom of the loop.
// For loops can only have *one* back-branch to the head, from the latch.

namespace SCI.Decompile
{
    [Flags]
    enum LoopType
    {
        Repeat = 1,
        While = 2,
        For = 4,
        DoWhile = 8, // third party compilers
        WhileOrRepeat = While | Repeat,
        ForOrWhile = For | While,
        Any = For | While | Repeat
    }

    class LoopSummary
    {
        public Instruction Start;
        public Instruction Latch;
        public Instruction Follow;

        public LoopType Type = LoopType.Any;
        public Instruction TestDominator;
        public Instruction ForReinit;
        public HashSet<Instruction> UnsolvedBranches = new HashSet<Instruction>();

        public Function Function;

        public override string ToString()
        {
            return "Loop Start: " + Start + " Latch: " + Latch + " [" + Follow + "]";
        }

        public bool Contains(Instruction i)
        {
            return Start.Position <= i.Position && i.Position <= Latch.Position;
        }

        public bool IsSolved() { return UnsolvedBranches.Count == 0; }
    }

    static class LoopDetection
    {
        public static List<LoopSummary> Detect(Function function, InstructionList instructions)
        {
            var loops = new List<LoopSummary>();
            foreach (var instruction in instructions)
            {
                // is a back-jump?
                if ((instruction.Operation == Operation.jmp ||
                     instruction.Operation == Operation.bt) && // DoWhile loops from Studio and Companion
                    instruction.Parameters[0] < 0)
                {
                    var loop = loops.FirstOrDefault(l => l.Start.Position == instruction.BranchTarget);
                    if (loop == null)
                    {
                        loop = new LoopSummary { Start = instructions[instruction.BranchTarget], Function = function };
                        loops.Add(loop);
                    }
                    loop.Latch = instruction;
                    loop.Follow = instruction.Next;
                }
            }
            // sort inner to outer (redundant?)
            loops.Sort((a, b) => a.Latch.Position.CompareTo(b.Latch.Position));
            return loops;
        }
    }
}
