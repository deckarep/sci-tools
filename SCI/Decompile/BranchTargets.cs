using System.Collections.Generic;
using System.Linq;

namespace SCI.Decompile
{
    // BranchTargets is supposed to be a convenient dictionary of
    // instructions and the branches that target them.

    class BranchTargets : Dictionary<int, List<Instruction>>
    {
        public BranchTargets(InstructionList instructions)
        {
            foreach (var instruction in instructions)
            {
                if (instruction.IsBranch)
                {
                    AddBranch(instruction);
                }
            }
        }

        public void AddBranch(Instruction instruction)
        {
            List<Instruction> list;
            if (!TryGetValue(instruction.BranchTarget, out list))
            {
                list = new List<Instruction>();
                Add(instruction.BranchTarget, list);
            }
            list.Add(instruction);
        }

        public void RemoveBranch(Instruction instruction)
        {
            this[instruction.BranchTarget].Remove(instruction);
        }

        // for when i patch an instruction's target
        public void UpdateBranch(Instruction patchedInstruction, int oldTarget)
        {
            this[oldTarget].Remove(patchedInstruction);
            AddBranch(patchedInstruction);
        }

        public Instruction GetEarliestBranch(int branchTarget, int minimumBranchPosition = -1)
        {
            List<Instruction> list;
            if (!TryGetValue(branchTarget, out list)) return null;

            return (from i in list
                    where i.Position < branchTarget &&
                          i.Position > minimumBranchPosition
                    orderby i.Position
                    select i).FirstOrDefault();
        }

        public bool Any(int branchTarget)
        {
            List<Instruction> list;
            if (!TryGetValue(branchTarget, out list)) return false;
            return list.Any();
        }
    }
}
