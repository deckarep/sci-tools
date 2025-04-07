using System.Collections.Generic;
using System.Linq;
using SCI.Resource;

// Loop Seduction is the sequel to Loop Deduction.
//
// Deduction only does things I know to be right.
// Seduction does some things I know to be wrong, but will still work out.
//
// In practice, most loops have been completely solved at this point.
//
// Loop Deduction always figures out the purpose of at least some loop
// branches, and most of the time that's all of them. That's because most
// loops have simple control flow and don't have many breaks or continues,
// if any, so there aren't that many branches to the loop head or follow.
// I call the rest Hard Loops.
//
// If there are unsolved branches, Loop Seduction takes care of the
// remaining jmp and bt instructions, and maybe a bnt if a test dominator
// hasn't been deduced yet. Afterwards, there might still be unsolved
// bnt instructions. Those will be handled by CFA with graph magic.

namespace SCI.Decompile
{
    class LoopSeduction
    {
        public static void Seduce(IReadOnlyList<LoopSummary> loops,
                                  IReadOnlyList<SwitchSummary> switches)
        {
            foreach (var loop in loops)
            {
                PickTestDominator(loop, loops, switches);
                AllBtsAndJmpsAreBreaksOrContinues(loop);
            }
        }

        static void PickTestDominator(LoopSummary loop,
                                      IReadOnlyList<LoopSummary> loops,
                                      IReadOnlyList<SwitchSummary> switches)
        {
            if (loop.TestDominator != null) return;
            if (!loop.Type.HasFlag(LoopType.For) && !loop.Type.HasFlag(LoopType.While)) return;

            // get the first bnt follow that we weren't able to deduce out.
            // that's probably the test dominator, and unless there's something
            // interesting before it, that's the assumption we're going with.
            //
            // there are two ways this could be wrong, both are okay:
            // 1. There is no test dominator. This is a repeat loop with a bunch
            //    of expressions and then an if statement whose bnt has been
            //    optimized by a break to point to follow. That's okay because
            //    AstBuilder will detect that more than one expression has been
            //    created for the test, say "oops", and switch to building a repeat.
            // 2. This is the first bnt within the while test, but there's more.
            //    The while test has an And and this is the first operand.
            //    That's okay, because it's ambiguous, and this will still pass CFA
            //    and so will the loop body, because the rest of the condition will
            //    look like an if statement with an Else Break. AST cleanup can detect
            //    this result and promote the If Test up into the While Test.
            var firstBntFollow = (from b in loop.UnsolvedBranches
                                  orderby b.Position
                                  where b.Operation == Operation.bnt &&
                                        b.IsBranch &&
                                        b.BranchTarget == loop.Follow.Position
                                  select b).FirstOrDefault();
            if (firstBntFollow == null) return;

            // if the bnt is in an inner switch, nope.
            var innerSwitches = switches.Where(s => loop.Contains(s.Toss)).ToList();
            if (innerSwitches.Any(s => s.Push.Position <= firstBntFollow.Position && firstBntFollow.Position <= s.Toss.Position)) return;

            // if there are any branches before the first bnt follow
            var innerLoops = loops.Where(l => l != loop && loop.Contains(l.Latch)).ToList();
            for (var i = loop.Start; i != firstBntFollow; i = i.Next)
            {
                // abort if we reach another loop or a switch
                if (innerLoops.Any(l => l.Contains(i))) return;
                if (innerSwitches.Any(s => s.Push.Position <= i.Position && i.Position <= s.Toss.Position)) return;

                // abort if we reach a break or continue
                if (i.HasFlag(InstructionFlag.Break) || i.HasFlag(InstructionFlag.Continue)) return;

                // abort if we reach a branch whose target isn't within the proposed while test.
                if (i.IsBranch && !(loop.Start.Position < i.BranchTarget && i.BranchTarget <= firstBntFollow.Position)) return;
            }

            // okay i'm satisfied
            loop.TestDominator = firstBntFollow;
            firstBntFollow.Flags |= InstructionFlag.LoopCondition;
            loop.UnsolvedBranches.Remove(firstBntFollow);
            Log.Debug(loop.Function, "Test Dominator found: " + firstBntFollow + ", remaining instructions: " + loop.UnsolvedBranches.Count);
        }

        // fuck it, treat all unsolved bt's and jmp's as break/continues. we'll fix it in post!
        static void AllBtsAndJmpsAreBreaksOrContinues(LoopSummary loop)
        {
            foreach (var i in loop.UnsolvedBranches.Where(b => b.Operation == Operation.bt ||
                                                               b.Operation == Operation.jmp))
            {
                if (i.BranchTarget < i.Position)
                {
                    i.Flags |= InstructionFlag.Continue;
                }
                else
                {
                    i.Flags |= InstructionFlag.Break;
                }
            }
            loop.UnsolvedBranches.RemoveWhere(i => i.Operation == Operation.bt || i.Operation == Operation.jmp);
        }
    }
}
