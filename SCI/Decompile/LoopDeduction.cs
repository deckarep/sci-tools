using System;
using System.Collections.Generic;
using System.Linq;
using SCI.Resource;

// Loop Deduction is the first phase of solving loops and mental decline.
//
// In this phase we figure out some things about a loop that we know to be true.
// What I never expected is that this is enough to solve 88% of all SCI loops.
// For the rest, we still have to solve them, but at least we know something.
// That sounds bad! If we still have to really solve a loop, then what's point
// of a component that only solves some of it?
//
// Loops are hard because of control flow ambiguities. SCI exacerbates this with
// branch-to-branch compiler optimizations, rarely used loop keywords that look
// like syntactic sugar but compile differently, and the even rarer multi-level
// versions of break and continue and their variants. The relationship between
// switches and loops is also weird, and a break or continue within a switch
// leaks stack and breaks graphs, which is why Companion fails on all of them.
// All of these issues overlap and get tangled together and it. is. a. lot.
//
// My takeaways after staring at many graphs:
// - I didn't know how to solve loops
// - Certainties are precious
// - Knowing anything is better than knowing nothing
// - Anything I can do before committing to a control flow graph is good
// - Control Flow Analysis was viable if I showed up with all the loop answers.
//   At this point I had decompiled every loopless function in SCI, so adding
//   loop support with minimal impact on my working CFA algorithm was paramount.
//
// Since I didn't know what I was doing, I tried something, and it unexpectedly
// solved the vast majority of loops. I dubbed the remaining loops Hard Loops.
// Now I knew their names and could study their graphs and ignore the rest.
// This allowed me to come up with the sequel, Loop Seduction, using different
// techniques with more ambiguity to solve the remaining unknowns. It was easier
// to reason about these given that Loop Deduction had figured out some parts.
//
// Loop Deduction does several things, but the bulk of it is based on my hard
// won observation that if you start at the last instruction of a SCI loop and
// go up, the first time you encounter a loop-related branch of a given type
// and given target, you can unambiguously deduce its original purpose. Keep
// going and you might get more, but things quickly become ambiguous. You're
// nibbling at one edge, but you're always getting something, and you can be
// certain it's true. For most loops, that turns out to be everything.
// Bonus: if the loop contains switches, then each switch case is a new scope
// for deduction. When you know the purpose of each loop-related branch, and
// have picked a plausible loop type, then you've solved the loop and can pass
// the answers to a CFA algorithm that assumes solved loops.

namespace SCI.Decompile
{
    static class LoopDeduction
    {
        public static void Deduce(IReadOnlyList<LoopSummary> loops,
                                  IReadOnlyList<SwitchSummary> switches,
                                  InstructionList instructions)
        {
            // First, deoptimize the confusing scenario where a loop's latch is followed by a branch.
            // For example, a while loop whose last expression is an inner loop:
            //
            // while A              A COND:  bnt A-follow
            //    ...         =>    B COND:  bnt A [ A back-jump! ]
            //    while B           B LATCH: jmp B
            //       ...            A LATCH: jmp A [ B's follow! ]
            //
            // B's condition/break branches will be optimized into back-branches to A's head.
            // Deoptimize B's branches so that they point to B's latch, otherwise I have to
            // code for this everywhere.
            var branchTargets = new BranchTargets(instructions);
            foreach (var loop in loops) // loops are already sorted inner to outer
            {
                // TODO:
                // NOT PERFECT: doesn't consider multi-level breaks
                // to loops whose follow node is a branch.
                // multi-level branches are rare, but still...

                if (!loop.Follow.IsBranch) continue;

                var followBranches = (from b in branchTargets[loop.Follow.BranchTarget]
                                      where loop.Contains(b)
                                      select b).ToList();
                foreach (var followBranch in followBranches)
                {
                    string old = followBranch.ToString();
                    followBranch.Flags |= InstructionFlag.DeoptimizedLoop;
                    followBranch.BranchTarget = loop.Follow.Position;
                    branchTargets.UpdateBranch(followBranch, loop.Follow.BranchTarget);
                    Log.Debug(instructions.Function, "Deoptimized confusing loop condition/break branch: " + old + " => " + followBranch);
                }
            }

            // Next, do some very basic loop-type deduction.
            // Do this before further deoptimizing, because "how many branches point to the head" is relevant.
            foreach (var loop in loops) // order shouldn't matter
            {
                // for loops can only have one back-branch to their start. any more and it's a while or repeat.
                var branchesToHead = branchTargets[loop.Start.Position];
                int backBranchesToHeadCount = branchesToHead.Count(b => b.Position > loop.Start.Position);
                if (backBranchesToHeadCount > 1)
                {
                    loop.Type &= ~LoopType.For;
                }

                // for and while loops must have at least one bnt to their follow.
                // if there are none, then this must be a repeat.
                if (branchTargets.Any(loop.Follow.Position))
                {
                    if (!branchTargets[loop.Follow.Position].Any(t => t.Operation == Operation.bnt && loop.Contains(t)))
                    {
                        loop.Type = LoopType.Repeat;
                    }
                }

                ThirdPartyHacks.DetectDoWhileLoop(loop, branchTargets);
            }

            // Finally, The Real Stuff:
            // Deduce the purpose of branch instructions that exit a loop or back jump to its head.
            // Process loops from inner to outer. If a loop contains switches, then process those
            // switches too, from inner to outer. Switches are processed one case body at a time.
            // Deduction is similar for loop bodies and switch case bodies, but with a few tweaks.
            // Switches are relevant because they can contain breaks or continues, something that
            // Sierra didn't think through because doing so leaks an element on the stack.
            // Companion doesn't handle these; it breaks the graph and is one of the main
            // causes of decompile failures. Loops and switch case bodies are scopes that can
            // contain breaks/continues, and we can similarly deduce the purpose of the last
            // instruction of each type, usually, within each of these scopes.
            foreach (var loop in loops) // loops are already sorted inner to outer
            {
                var innerSwitches = switches.Where(s => loop.Start.Position < s.Toss.Position &&
                                                        s.Toss.Position < loop.Latch.Position);
                foreach (var switch_ in innerSwitches.OrderBy(s => s.Toss.Position))
                {
                    // process each switch case body in order
                    foreach (var case_ in switch_.Cases)
                    {
                        if (case_.Body == null) continue;
                        var first = case_.Body;
                        var last = case_.Jmp ?? switch_.Toss.Prev;
                        Deduce(loop, first, last, switch_, loops, switches, instructions);
                    }
                    // process switch else
                    if (switch_.Else != null)
                    {
                        var first = switch_.Else;
                        var last = switch_.Toss.Prev;
                        Deduce(loop, first, last, switch_, loops, switches, instructions);
                    }
                }
                // process the loop
                Deduce(loop, loop.Start, loop.Latch.Prev, null, loops, switches, instructions);
            }
        }

        // these values are in their own class so that i can have multiple sets,
        // one for each loop-level that there are break/continue statements for.
        // only *seven* functions in all of SCI use multi-level break/continues!
        class Counts
        {
            public int headBnts = 0;
            public int headBts = 0;
            public int headJmps = 0;
            public int followBnts = 0;
            public int followBts = 0;
            public int followJmps = 0;
            public Instruction lastHeadJmp = null;
        }

        static void Deduce(LoopSummary loop, Instruction first, Instruction last, SwitchSummary switch_,
                           IReadOnlyList<LoopSummary> loops,
                           IReadOnlyList<SwitchSummary> switches,
                           InstructionList instructions)
        {
            // inSwitch means that we're deducing a switch case within a loop.
            // it does *not* mean that we're deducing a loop within a switch case. sorry!!
            bool inSwitch = switch_ != null;
            var allCounts = new Dictionary<LoopSummary, Counts>();

            // process instructions in reverse order.
            // caller should have excluded the current loop's latch
            // and the current switch's toss.
            for (Instruction i = last; i != null && i.Position >= first.Position; i = i.Prev)
            {
                // end of an inner switch? skip the whole thing
                if (i.Operation == Operation.toss)
                {
                    var innerSwitch = switches.First(s => s.Toss == i);
                    i = innerSwitch.Push;
                    if (i.Position < first.Position) throw new Exception("inner switch not contained by outer loop");
                    continue;
                }
                // end of an inner loop? skip the whole thing
                if (i.IsBranch)
                {
                    var innerLoop = loops.FirstOrDefault(l => i == l.Latch);
                    if (innerLoop != null)
                    {
                        i = innerLoop.Start;
                        if (i.Position < first.Position) throw new Exception("inner loop not contained by outer loop");
                        continue;
                    }
                }

                // HACK: Workarounds.cs tags some hard-coded instructions.
                // if one of those is found, set the loop type.
                // CFG picks up LoopType and AstBuilder will make a for loop.
                if (i.HasFlag(InstructionFlag.TrustMeItsAFor))
                {
                    loop.Type = LoopType.For;
                }

                // only care about branches
                if (!i.IsBranch) continue;
                // only care about branches to the loop boundaries or beyond.
                // * but if we're in a switch and this is a for loop, then a
                //   continue will escape the switch and be before the latch.
                // * a branch shouldn't target the latch, due to optimizations,
                //   but third party compilers don't do that, and potentially
                //   there could be a sierra script compiled without optimizations.
                if (!inSwitch)
                {
                    if (loop.Start.Position < i.BranchTarget && i.BranchTarget <= loop.Latch.Position) continue;
                }
                else
                {
                    if (loop.Start.Position < i.BranchTarget && i.BranchTarget <= switch_.Toss.Position) continue;
                }
                // which loop does this branch target?
                var targetLoop = loops.FirstOrDefault(l => (l.Start.Position == i.BranchTarget ||
                                                            i.BranchTarget <= l.Follow.Position) &&
                                                            l.Contains(i)); // safety
                if (targetLoop == null) throw new Exception("branch target escapes all loops");
                int level = 1;
                if (loop != targetLoop)
                {
                    // how fun, a rare multi-level branch.
                    // count how many loops contain this loop latch,
                    // stop when the target loop is reached.
                    foreach (var l in loops)
                    {
                        if (l != loop && l.Contains(loop.Latch))
                        {
                            level++;
                        }
                        if (l == targetLoop) break;
                    }
                }
                i.LoopLevel = level;

                // is this a branch to a head or a follow?
                bool head = false;
                bool follow = false;
                if (i.BranchTarget == targetLoop.Start.Position)
                {
                    head = true;
                }
                else if (i.BranchTarget == targetLoop.Follow.Position)
                {
                    follow = true;
                }
                else if (i.BranchTarget < loop.Start.Position)
                {
                    // this was just a sanity check, but it caught hand-written asm in zork-demo
                    throw new Exception("branch target escapes loop (and isn't multi-level)");
                }
                else
                {
                    // exciting! this is a continue within a for loop.
                    // this is the only way i know to *prove* a for loop.
                    // gk1 710 stuffArray:doit
                    // hoyle3 script 100 DominoHand:setNextPosn (multi-level?)
                    head = true;
                    targetLoop.Type = LoopType.For;
                    Log.Debug(instructions.Function, "proved a for loop!");
                    targetLoop.ForReinit = instructions[i.BranchTarget];
                }

                // handle multilevel branches
                if (level > 1)
                {
                    // in case we can't deduce this instruction's purpose,
                    // point it at the current loop so that the graph is clean.
                    if (head)
                    {
                        // not wild about this if loop is a for, but may never happen
                        i.BranchTarget = loop.Start.Position;
                    }
                    else
                    {
                        i.BranchTarget = loop.Follow.Position;
                    }
                }

                // load the counts for the target loop
                Counts c;
                if (!allCounts.TryGetValue(targetLoop, out c))
                {
                    c = new Counts();
                    allCounts.Add(targetLoop, c);
                }

                // six combos of head/follow + bnt/bt/jmp to handle
                if (head)
                {
                    if (i.Operation == Operation.bnt)
                    {
                        // bnt => head of loop
                        if (c.headBnts == 0 && c.headJmps == 0)
                        {
                            // unambiguous optimization to latch
                            // (but impossible within a switch)
                            if (inSwitch) throw new Exception("impossible, bnt escapes switch");
                            if (level != 1) throw new Exception("impossible");
                            i.BranchTarget = loop.Latch.Position;
                            i.Flags |= InstructionFlag.DeoptimizedLoop;
                        }
                        else if (c.headBnts == 0 && c.headJmps == 1 && inSwitch)
                        {
                            // switch: unambiguous optimization to the earlier head jmp (a Continue)
                            i.BranchTarget = c.lastHeadJmp.Position;
                            i.Flags |= InstructionFlag.DeoptimizedLoop;
                        }
                        else
                        {
                            loop.UnsolvedBranches.Add(i);
                        }
                        c.headBnts++;
                    }
                    else if (i.Operation == Operation.bt)
                    {
                        // bt => head of loop
                        if (c.headBts == 0 && c.headJmps == 0)
                        {
                            // unambiguous ContinueIf
                            i.Flags |= InstructionFlag.Continue;
                        }
                        else
                        {
                            loop.UnsolvedBranches.Add(i);
                        }
                        c.headBts++;
                    }
                    else if (i.Operation == Operation.jmp)
                    {
                        // jmp => head of loop
                        if (c.headJmps == 0 && (inSwitch || level > 1))
                        {
                            // switch: unambiguous Continue
                            // loop at level > 1: unambiguous Continue
                            // loop at level 1: always ambiguous.
                            // it's a continue or it's an optimized jmp that went to the latch.
                            i.Flags |= InstructionFlag.Continue;
                        }
                        else
                        {
                            loop.UnsolvedBranches.Add(i);
                        }
                        c.lastHeadJmp = i;
                        c.headJmps++;
                    }
                }
                else if (follow)
                {
                    if (i.Operation == Operation.bnt)
                    {
                        // bnt => loop follow
                        if (c.followBnts == 0 && c.followJmps == 0)
                        {
                            // unambiguous dominator of the for/while condition
                            // (but impossible within a switch)
                            if (inSwitch) throw new Exception("impossible, bnt escapes switch");
                            if (level > 1) throw new Exception("also impossible");
                            loop.TestDominator = i;
                            i.Flags |= InstructionFlag.LoopCondition;
                        }
                        else if (loop.TestDominator != null && level == 1)
                        {
                            // unambiguous optimization to the condition dominator,
                            // or to someone who targeted him. either way, clean this up
                            // for a nice graph. i don't know if this matters, but why not.
                            i.BranchTarget = loop.TestDominator.Position;
                            i.Flags |= InstructionFlag.DeoptimizedLoop;
                        }
                        else
                        {
                            loop.UnsolvedBranches.Add(i);
                        }
                        c.followBnts++;
                    }
                    else if (i.Operation == Operation.bt)
                    {
                        // bt => loop follow
                        if (c.followBts == 0 && c.followJmps == 0)
                        {
                            // unambiguous BreakIf
                            i.Flags |= InstructionFlag.Break;
                        }
                        else
                        {
                            loop.UnsolvedBranches.Add(i);
                        }
                        c.followBts++;
                    }
                    else if (i.Operation == Operation.jmp)
                    {
                        if (c.followJmps == 0)
                        {
                            // unambiguous Break
                            i.Flags |= InstructionFlag.Break;
                        }
                        else
                        {
                            loop.UnsolvedBranches.Add(i);
                        }
                        c.followJmps++;
                    }
                }
            }
        }
    }
}