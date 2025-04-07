using System.Linq;
using SCI.Resource;

// BtFixups: Deoptimizes bnt's using bt's to do so.
//
// This fixup deoptimizes a *lot* and doesn't require a graph.
// I now wonder if BranchGraphFixup would handle all of these;
// it certainly handles more than just these. But again, this
// can be done pre-graph.
//
// 1. Locate each bt that targets a bnt. These form a range.
// 2. Locate any bnt's in that range that also target the
//    instruction that the tail bnt targets.
// 3. Patch those bnt's to target the tail bnt instead;
//    they are all optimized branch-to-branches.
//
// I have no science behind this; I just looked at a lot of
// graphs and saw this as an easily detected situation that
// fixed a lot of edges that broke the CFA algorithm in my head.
//
// When this is all over, revisit if this is necessary now that
// BranchGraphFixup exists. Alternatively, never touch this again.

namespace SCI.Decompile
{
    static class BtFixups
    {
        public static void Fixup(InstructionList instructions)
        {
            // locate bt => bnt
            foreach (var bt in instructions.Where(i => i.Operation == Operation.bt &&
                                                       i.Parameters[0] >= 0)) // no negative bts
            {
                var bntAnd = instructions[bt.BranchTarget];
                if (bntAnd.Operation != Operation.bnt) continue;
                if (bntAnd.Parameters[0] < 0) continue; // ignore bt => negative bnt

                var bntLongTarget = instructions[bntAnd.BranchTarget];
                for (var i = bt.Next; i != bntAnd; i = i.Next)
                {
                    if (i.Operation == Operation.bnt &&
                        i.BranchTarget == bntLongTarget.Position)
                    {
                        string before = i.ToString();
                        i.BranchTarget = bntAnd.Position;
                        i.Flags |= InstructionFlag.Deoptimized;
                        string after = i.ToString();

                        Log.Debug(instructions.Function, string.Format("bt deoptimizing {0} => {1}", before, after));
                    }
                }
            }
        }
    }
}
