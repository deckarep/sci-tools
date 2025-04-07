using SCI.Resource;

// There are sequences of consecutive branch instructions that have no effect.
// They convey no information I wish to capture in decompiled scripts.
// They create edge cases I would need to handle in control flow analysis.
// They are easy to identify and delete from the simple instruction list.
//
// "KILL THEM" --Potato Judge
//
// It is quite possible that this is unnecessary were it not for avoidable
// shortcoming in my control flow analysis. We'll never know!
//
// Prerequisite: Remove all debug instructions (file,line)

namespace SCI.Decompile
{
    static class DeadBranches
    {
        public static void Remove(InstructionList instructions)
        {
            for (var i = instructions.First; i != null; i = i.Next)
            {
                // bnt target
                // jmp next instruction (jmp 0000, unless debug statements)
                // bnt same target
                while (i.Operation == Operation.bnt &&
                       i.Next.Operation == Operation.jmp &&
                       i.Next.Next.Operation == Operation.bnt &&
                       i.BranchTarget == i.Next.Next.BranchTarget &&
                       i.Next.BranchTarget == i.Next.Next.Position)
                {
                    Log.Debug(instructions.Function + "Deleting two dead branches: " + i.Next + ", " + i.Next.Next);
                    DeleteAndUpdateBranches(instructions, i.Next.Next, i.Position);
                    DeleteAndUpdateBranches(instructions, i.Next, i.Position);
                }

                // bnt target
                // bnt same-target
                // ... and if there's a third?? we'll get that too!
                // bnt same-target ?? i don't know?!
                while (i.Operation == Operation.bnt &&
                       i.Next.Operation == Operation.bnt &&
                       i.BranchTarget == i.Next.BranchTarget)
                {
                    Log.Debug(instructions.Function, "Deleting dead branch: " + i.Next);
                    DeleteAndUpdateBranches(instructions, i.Next, i.Position);
                }
            }
        }

        static void DeleteAndUpdateBranches(InstructionList instructions, Instruction i, int newPosition)
        {
            instructions.Remove(i);

            // update anyone who targets the deleted instruction
            foreach (var j in instructions)
            {
                if (j.IsBranch && j.BranchTarget == i.Position)
                {
                    j.BranchTarget = newPosition;
                }
            }
        }
    }
}
