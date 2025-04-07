using System.Collections.Generic;
using SCI.Resource;

// Deletes debug instructions from the instruction list:
//
//   file   name of the original source file
//   line   line number in the original source file
//
// Even though these two instructions are no-ops, it's not as simple
// as just deleting them, because they're also the targets of branches.
//
// Instead, branches that target debug instructions must also be patched
// to point to the next real instruction.
//
// Deleting debug instructions means giving up context that "could" help the
// decompiler make better decisions. I've heard that a .NET decompiler uses
// similar info to identify for loops, and I think that would work in SCI too.
// The problem is that debug instructions only appear in SCI32, and Sierra
// inconsistently included them. The decompiler still has to make due without
// them. If they altered the output, then that would hinder comparing versions
// because the scripts would have differences even though the original code was
// unchanged. Diffing scripts is one of the primary reasons for decompiling.
//
// The presence of debug instructions caused other changes in the compiler's
// output, and this subtly affects both decompilers' output. You can see this
// when diffing game versions and seeing two different, but equivalent, control
// flow structures. It's because the debug instructions in one version prevented
// the compiler from applying a branch to branch optimization that it otherwise
// would have. In practice, it doesn't happen much. A crazy idea would be to
// apply b2b optimizations here after removing the debug instructions, but the
// idea of implementing the decompiler's Number One Enemy... lol Hard Pass!
// But it's pretty funny?? Might ta try it some day.

namespace SCI.Decompile
{
    static class DebugInstructions
    {
        public static void Remove(InstructionList instructions)
        {
            // build a map of debug instructions
            // key: debug instruction position
            // val: next non-debug instruction position
            var map = new Dictionary<int, int>();
            var activeDebugInstructions = new List<int>();
            foreach (var instruction in instructions)
            {
                if (instruction.Operation == Operation.file ||
                    instruction.Operation == Operation.line)
                {
                    activeDebugInstructions.Add(instruction.Position);
                }
                else
                {
                    if (activeDebugInstructions.Count > 0) // optimization
                    {
                        foreach (var debugInstruction in activeDebugInstructions)
                        {
                            map.Add(debugInstruction, instruction.Position);
                        }
                        activeDebugInstructions.Clear();
                    }
                }
            }

            // abort if no debug instructions
            if (map.Count == 0) return;

            // remove debug instructions
            foreach (var debugInstructionPos in map.Keys)
            {
                instructions.Remove(debugInstructionPos);
            }

            // patch branch instructions
            foreach (var instruction in instructions)
            {
                if (instruction.IsBranch)
                {
                    int newTarget;
                    if (map.TryGetValue(instruction.BranchTarget, out newTarget))
                    {
                        // increase the branch target if it points to a debug
                        instruction.Parameters[0] += (newTarget - instruction.BranchTarget);
                    }
                }
            }
        }
    }
}
