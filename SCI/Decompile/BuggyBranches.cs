using SCI.Resource;

// Patches buggy bnt instructions:
//
// 1. Switches whose last case/else body is empty (compiler bug, SQ1VGA radar1:doVerb)
// 2. The one Camelot method that ends in a bnt that targets a bogus location
// 3. Any out of bounds bnt (should catch compiler bug for last empty cond case)

namespace SCI.Decompile
{
    static class BuggyBranches
    {
        public static void Patch(InstructionList instructions)
        {
            PatchSwitches(instructions);

            // this isn't perfect; it doesn't handle bogus branches that look good.
            // that's why PatchSwitches() is called first.
            // we'll see how many buggy conds this misses, but it might get all of them.
            foreach (var i in instructions)
            {
                if (i.Operation == Operation.bnt &&
                    (i.BranchTarget < 0 ||
                     i.BranchTarget > instructions.Last.Position))
                {
                    Log.Debug(instructions.Function, "Patching out of bounds branch: " + i);
                    i.Parameters[0] = 0;
                    i.Flags |= InstructionFlag.CompilerBug;
                }
            }

            PatchCamelotRm40HandleEvent(instructions);
        }

        // Camelot Rm40:handleEvent ends in:
        // toss
        // bnt f*** [ somewhere random, atari st targets the middle of an instruction ]
        // ret
        // I don't know why, but it doesn't crash because acc is always true.
        // According to my notes, I proved that, and also someone would have noticed.
        // bnt should never be the second to last instruction, it could only crash the
        // game or have no affect, unless the offset points to the next instruction.
        // (so bnt 0000, or if debugging statements like Rama SpinProp:serialize, more)
        // For now, I'm just deleting it.
        static void PatchCamelotRm40HandleEvent(InstructionList instructions)
        {
            if (instructions.Count > 1 &&
                instructions.Last.Prev.Operation == Operation.bnt &&
                instructions.Last.Prev.BranchTarget != instructions.Last.Position)
            {
                var i = instructions.Last.Prev;
                Log.Debug(instructions.Function, "Deleting Camelot broken branch: " + i);
                instructions.Remove(i);
            }
        }

        // Switches with broken branches due to compiler bug are unambiguous.
        // Just look for the sequence:
        //   eq
        //   bnt ????
        //   toss

        static Operation[] SwitchCaseCompilerBugSequence =
        {
            Operation.eq, Operation.bnt, Operation.toss
        };

        static void PatchSwitches(InstructionList instructions)
        {
            int counter = 0;
            Instruction bnt = null;
            foreach (var instruction in instructions)
            {
                if (instruction.Operation == SwitchCaseCompilerBugSequence[counter])
                {
                    counter++;
                    if (instruction.Operation == Operation.bnt)
                    {
                        bnt = instruction;
                    }
                    if (counter == SwitchCaseCompilerBugSequence.Length)
                    {
                        if (bnt.Parameters[0] != 0)
                        {
                            Log.Debug(instructions.Function, "Patching buggy switch branch: " + bnt);
                            bnt.Parameters[0] = 0;
                            bnt.Flags |= InstructionFlag.CompilerBug;
                        }
                        counter = 0;
                    }
                }
                else
                {
                    counter = 0;
                }
            }
        }
    }
}
