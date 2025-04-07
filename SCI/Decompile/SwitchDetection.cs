using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCI.Resource;

// Switch Detection finds all switches and their cases from the instruction list.
//
// I wrote this in Asheville when I was writing all kinds of things I thought
// I might need, and it's the largest one that survived. It's a chore to do this
// with just an instruction list, but it enables my Control Flow Analysis algorithm.
//
// Detecting switches and their contours prior to building a control flow graph is
// fundamental to my decompiler design. That sounds wrong! Switches are supposed to
// be easy: they have a distinct pattern, they end in a distinct instruction,
// and graph dominance is supposed to solve the flow. What's the problem?
//
// * Gestures wildly to all the compiler bugs and decompiler failures *
//
// I guess it's not so easy! There are complex overlapping issues:
//
// - Sierra's compiler had a bug that emitted bad switch branches until it crashed SQ1.
//   Practically every game until then has at least one of these broken switches.
//   When they fixed this bug, they stopped emitting the branch instruction at all,
//   so that's another pattern to check for.
//
// - Compiler optimizations break almost every assumption you can make about a switch
//   structure at least once; usually by deleting or replacing a key instruction.
//   Rare branch-to-branch optimizations also break the expected structure.
//
// - Compiler optimizations have bugs; there's one switch in all of SCI (qfg3) where
//   the optimizer altered the case instructions and broke the animation.
//
// - Case test values can contain literally anything, including entire if statements,
//   and there could even be a loop; sure why not?
//
// - Switches within loops can contain breaks or continues, and that breaks graph
//   assumptions, and that's why Companion can't decompile any of these; it's a
//   common cause of asm functions. Breaks and continues within switches also
//   leak stack because they skip the toss instruction.
//
// - SCI scripts leak stack like crazy because the compiler would happily accept
//   a bunch of numbers you typo'd into the middle of code and turn some into
//   push instructions. This is unfortunate because it makes stack balancing an
//   unreliable signal to determine expression boundaries.
//
// - SCI Studio compiled broken switches when the "default" (else) case wasn't last.
//   Instead of placing the bytecode for the default case last, it would place it where
//   it appeared in code, causing it to always execute before evaluating subsequent
//   cases. There are quite a few in Betrayed Alliance and other fan games. Companion
//   can't decompile these; I can as long as the expression being switched is on has
//   a known value, otherwise the code couldn't be represented in script.
//   SwitchDetection's algorithm naturally handles Studio switches. First, it
//   identifies the top of switch in a way that involves evaluating as little of the
//   switch instructions as possible, because it assumes there might be damage,
//   and then it climbs back down to identify each effective case. When it hits the
//   default case, that's the end of the switch. Normal decompilation will interpret
//   the case comparisons as conditionals within the default case, which is accurate
//   control flow.
//
// - SCI Studio has a pretty funny bug where it always corrupts the 17th case of
//   a switch, and possibly later cases, because it's a C program that used a fixed
//   array of 16 elements for cases with no boundary checks. "16 oughta be enough!"
//   This causes the 17th case to always execute when evaluated, and then the switch
//   resumes testing the subsequent cases as usual. SwitchDetection detects this
//   broken instruction sequence.
//
// Switch Detection should handle all forms, including degenerates:
// 1. switch (value) else (...)
// 2. switch (value) - theoretical
// 3. switch (value) ... else break-loop (pqswat, optimizer breaks switch structure)
// 4. switch () ... no value in first case because optimizer removed the ldi (brain2)
//
// Detection patches two forms of optimization:
// 1. optimized bnt before else/continue-or-break. patch target to point to else.
// 2. branches inside cases that point to toss instead of the case's final jmp.
//    * I have lost track if CFA relies on this behavior, but even if it doesn't,
//      it's important to me so that the graphviz files are easier to understand.
//
// The detection algorithm:
// 1. Process each toss in order (inner switches handled before outers)
// 2. Reverse consume from toss to push (if there are at least two cases,
//    take that path, otherwise keep taking the earliest branch.)
// 3. Work down from push to find each case (if any) and else (if any)
//    a. Scan from dup to next eq? that can be reverse fulfilled back to dup.
//    b. Follow bnt to next dup or else
//
// Known failures:
// - pq3-amiga-german-1.000 has a switch with garbage pushed to the stack that
//   breaks my assumption that the stack will balance. I obtained this version
//   at the end of the project, so I just patch it in Workarounds.cs.
//
// Potential failures:
// - SCI Studio will produce an undecompilable switch if the default case doesn't
//   appear last *and* the expression being switched on has an unknown value, like
//   a function call. That code can't be represented in script, and it wouldn't
//   have done what the author intended. Apparently no games contain this.

namespace SCI.Decompile
{
    class SwitchSummary
    {
        public Instruction Push;          // Instruction that pushes the value to compare.
        public List<CaseSummary> Cases;
        public Instruction Else;          // First instruction of the Else block, null if no Else
        public Instruction Toss;          // Final instruction of the switch

        public SwitchSummary() { Cases = new List<CaseSummary>(); }
    }

    class CaseSummary
    {
        public Instruction Dup;  // always
        public Instruction Cond; // optional, cond can be empty. brain2 #349 art:init
        public Instruction Eq;   // always
        public Instruction Bnt;  // not if last empty case post-compiler-fix
        public Instruction Body; // optional, body can be empty
        public Instruction Jmp;  // optional, if last case
    }

    static class SwitchDetection
    {
        public static List<SwitchSummary> Detect(InstructionList instructions)
        {
            var branchTargets = new BranchTargets(instructions);
            var switches = new List<SwitchSummary>();
            foreach (var instruction in instructions)
            {
                if (instruction.Operation == Operation.toss)
                {
                    switches.Add(ParseSwitch(instruction, instructions, branchTargets, switches));
                }
            }
            return switches;
        }

        static SwitchSummary ParseSwitch(Instruction toss, InstructionList instructions,
            BranchTargets branchTargets, IReadOnlyList<SwitchSummary> switches)
        {
            var switch_ = new SwitchSummary();
            switch_.Toss = toss;
            switch_.Push = FindPush(toss, branchTargets, switches);
            if (switch_.Push == null) throw new Exception("switch push not found");

            // find all the cases
            Instruction caseStart = switch_.Push.Next;
            while (true)
            {
                // we're done! this would handle a switch with no cases or elses. theoretical?
                if (caseStart.Operation == Operation.toss) break;

                if (caseStart.Operation == Operation.dup)
                {
                    Instruction eq = FindEq(caseStart, toss, branchTargets);
                    if (eq == null)
                    {
                        // this is an else that happens to start with a dup.
                        // theoretical.
                        switch_.Else = caseStart;
                        break;
                    }

                    // we have a case!
                    var case_ = new CaseSummary();
                    switch_.Cases.Add(case_);
                    case_.Dup = caseStart;
                    if (case_.Dup.Next != eq)
                    {
                        // it can be optimized out
                        case_.Cond = case_.Dup.Next;
                    }
                    case_.Eq = eq;

                    // is empty last case? (post-bug-fix)
                    if (eq.Next == toss)
                    {
                        break;
                    }

                    // eq? must be followed by bnt
                    if (eq.Next.Operation != Operation.bnt)
                        throw new Exception("case eq? not followed by bnt: " + eq.Next);
                    case_.Bnt = eq.Next;

                    // is this bnt 00?
                    if (case_.Bnt.Parameters[0] == 0)
                    {
                        // bnt 00 could mean a couple things:
                        // 1. sierra's empty-last-case compiler bug had this pointing to
                        //    an invalid location, and BuggyBranches detected this and
                        //    rewrote the target to 0000 and flagged it as as bug.
                        // 2. it's the last case of a switch and it's empty and there
                        //    was no compiler bug? i don't think sierra would do that,
                        //    because when they fixed the bug, they also stopped emitting
                        //    the bnt completely.
                        // 3. sci studio corrupts the 17th case (and possibly later ones)
                        //    and emits bnt 0000, and later for the end of the case, jmp 0000.

                        // if there is no real code between bnt 00 and toss,
                        // then we've reached the end of the switch.
                        Instruction next = case_.Bnt.Next;
                        if (next == switch_.Toss ||
                            (next.Operation == Operation.jmp &&
                             next.Parameters[0] == 0 &&
                             next.Next == switch_.Toss))
                        {
                            break;
                        }

                        // oh hell, there is still code after bnt 00.
                        // this is sci studio case 17 corruption. stop treating this
                        // as a case and instead treat everything from dup onwards as
                        // the "else" since that's what the control flow really is.
                        Log.Info(instructions.Function, "SCI Studio corrupted switch-case: " + case_.Dup.Next);
                        switch_.Cases.RemoveAt(switch_.Cases.Count - 1);
                        switch_.Else = case_.Dup;
                        break;
                    }

                    // is this the last case?
                    if (case_.Bnt.BranchTarget == toss.Position)
                    {
                        // we know body isn't empty
                        case_.Body = case_.Bnt.Next;
                        break;
                    }

                    // this is a case, but it's not the last (or there's an else).
                    // bnt must point to an instruction after a jmp to toss.
                    //
                    // BUT: there is a terrible exception to this. if the else
                    // starts with a (break) or (continue) then the else is a jmp
                    // and the bnt will be optimized to target the jmp target
                    // instead of the else.
                    // PQSWAT is the only one i've seen do this.
                    // eq? bnt-escapes case-body jmp-to-toss jmp-escape toss.
                    if (!(switch_.Push.Position < case_.Bnt.BranchTarget && case_.Bnt.BranchTarget < toss.Position))
                    {
                        // we've escaped the switch! this is an optimized bnt that originally
                        // pointed to a jmp with this target before the toss. lets find that jmp
                        // and patch the instruction to point to it instead.
                        var elseJmp = toss.Prev;
                        if (elseJmp.Operation == Operation.jmp && elseJmp.BranchTarget == case_.Bnt.BranchTarget)
                        {
                            int oldTarget = case_.Bnt.BranchTarget;
                            case_.Bnt.BranchTarget = elseJmp.Position;
                            case_.Bnt.Flags |= InstructionFlag.Deoptimized;
                            branchTargets.UpdateBranch(case_.Bnt, oldTarget);
                        }
                    }

                    var nextCaseStart = instructions[case_.Bnt.BranchTarget];
                    case_.Jmp = nextCaseStart.Prev;
                    if (!(case_.Jmp.Operation == Operation.jmp && case_.Jmp.BranchTarget == toss.Position))
                        throw new Exception("case jmp isn't a jmp to toss: " + case_.Jmp);

                    // only set body if it isn't empty (nothing between bnt and jmp)
                    if (case_.Bnt.Next != case_.Jmp)
                    {
                        case_.Body = case_.Bnt.Next;
                    }

                    caseStart = nextCaseStart;
                }
                else if (caseStart.Operation == Operation.toss)
                {
                    // we're done before we started!
                    // the switch has no cases and doesn't have an else.
                    // so far this is theoretical. push and straight to toss.
                    break;
                }
                else
                {
                    // we've reached an else. this handles normal elses and
                    // also when there are no cases, which companion has
                    // to look for separately.
                    switch_.Else = caseStart;
                    break;
                }
            }

            // deoptimize branches from a case to toss. they should go to their jump instead.
            foreach (var case_ in switch_.Cases)
            {
                // skip empty cases or cases without jmps
                if (case_.Body == null || case_.Jmp == null) continue;

                for (var i = case_.Body; i != case_.Jmp; i = i.Next)
                {
                    if (i.IsBranch && i.BranchTarget == toss.Position)
                    {
                        i.BranchTarget = case_.Jmp.Position;
                        branchTargets.UpdateBranch(i, toss.Position);
                        i.Flags |= InstructionFlag.Deoptimized;
                        Log.Debug(instructions.Function, "switch deoptimized: " + i);
                    }
                }
            }

            return switch_;
        }

        static Instruction FindPush(Instruction toss, BranchTargets branchTargets, IReadOnlyList<SwitchSummary> switches)
        {
            var state = new ReverseState();
            for (var i = toss; i != null; i = i.Prev)
            {
                state.Apply(i);
                if (state.Stack == 0)
                {
                    // push is found as soon as stack balances
                    return i;
                }

                // when a toss is reached, skip ahead to the push.
                // this is an optimization, but it is also part of the
                // next check which ignores branches that come from
                // within switches, because they can be (break)s that
                // leave the stack unbalanced. (ignore our toss, obvs)
                if (i.Operation == Operation.toss && i != toss)
                {
                    var switch_ = switches.First(s => s.Toss == i);
                    i = switch_.Push.Next; // Next to balance Prev of for loops
                    continue;
                }

                // if we're just starting out then try to find the jmp from
                // the first case and use the dup from the second to monkey-bars
                // up to the first bnt without having to traverse instructions
                // in case bodies. the reason i want to avoid that? all these
                // gol' dang scripts that screw up the stack! if this isn't
                // a two-case script though, the regular strategy gets used.
                // note that this first-case detection avoids optimized branches
                // that come from within the body of the first case.
                bool firstCaseFound = false;
                if (i == toss)
                {
                    int branchPosition = -1;
                    Instruction branch;
                    while ((branch = branchTargets.GetEarliestBranch(i.Position, branchPosition)) != null)
                    {
                        branchPosition = branch.Position;
                        if (!switches.Any(s => s.Push.Position < branch.Position && branch.Position < s.Toss.Position))
                        {
                            if (branch.Operation == Operation.jmp &&
                                branch.Next.Operation == Operation.dup &&
                                branchTargets.Any(branch.Next.Position))
                            {
                                // do the dup on the next iteration
                                i = branch.Next.Next;
                                state.Stack -= 1; // adjust for dup getting applied
                                firstCaseFound = true;
                                break;
                            }
                        }
                    }
                }

                if (!firstCaseFound)
                {
                    // take the earliest branch, but ignore branches that come
                    // from within switches because those are (break)s that
                    // escape stack balancing.
                    int branchPosition = -1;
                    Instruction branch;
                    while ((branch = branchTargets.GetEarliestBranch(i.Position, branchPosition)) != null)
                    {
                        branchPosition = branch.Position;
                        if (!switches.Any(s => s.Push.Position < branch.Position && branch.Position < s.Toss.Position))
                        {
                            i = branch.Next; // Next to balance Prev of for loops
                            break;
                        }
                    }
                }
            }
            return null;
        }

        // finds the next eq? instruction that can reverse fulfill a dup
        static Instruction FindEq(Instruction dup, Instruction toss, BranchTargets branchTargets)
        {
            // if the condition is empty (!!) then the eq? immediately follows dup.
            // brain2 art:init in script #349. the optimizer removed ldi 00 because
            // the switch was preceded by ldi 00 / aTop.
            if (dup.Next.Operation == Operation.eq) return dup.Next;

            for (var eq = dup.Next; eq != toss; eq = eq.Next)
            {
                if (eq.Operation != Operation.eq) continue;

                var state = new ReverseState();
                for (var i = eq; i.Position >= dup.Position ; i = i.Prev)
                {
                    state.Apply(i);

                    // when we reach dup we should be fulfilled except for
                    // needing stack, because dup needs that.
                    if (i == dup)
                    {
                        if (state.NeedsAcc == false &&
                            state.NeedsPprev == false &&
                            state.Stack == 0)
                        {
                            return eq;
                        }
                    }

                    // take the highest branch
                    var branch = branchTargets.GetEarliestBranch(i.Position);
                    if (branch != null)
                    {
                        i = branch.Next; // next to balance Prev of for loops
                    }
                }
            }
            return null;
        }
    }
    
    // ReverseState: A Thing We Should Not Dwell Upon
    //
    // I wrote this early on, I thought it would get used a lot, but no.
    // It's only used by switch detection, also written early on, and if
    // I had to do it again I would try to do switch detection differently.
    //
    // I that having a thing to traverse instructions in reverse to identify and
    // resolve their dependencies was going to be critical to decompiling.
    // Instead, I went the other direction. This class used to be huge, now only
    // the fragment used by switch detection remains. That's why it hides here.

    class ReverseState
    {
        public bool NeedsAcc;
        public bool NeedsPprev;
        public bool NeedsStack;
        public int Stack;

        public bool HasNeeds { get { return NeedsAcc || NeedsPprev || NeedsStack; } }
        public bool IsFulfilled { get { return !HasNeeds && Stack == 0; } }

        public void Apply(Instruction i)
        {
            var flags = i.Operation.GetFlags();

            // acc
            if (flags.HasFlag(OpFlags.WritesAcc))
                NeedsAcc = false;
            if (flags.HasFlag(OpFlags.ReadsAcc))
                NeedsAcc = true;

            // pprev
            if (flags.HasFlag(OpFlags.WritesPprev))
                NeedsPprev = false;
            if (flags.HasFlag(OpFlags.ReadsPprev))
                NeedsPprev = true;

            // stack
            if (flags.HasFlag(OpFlags.PushesStack))
            {
                Stack += 1;
                NeedsStack = false;
            }
            if (flags.HasFlag(OpFlags.PopsStack))
                Stack -= i.PopStackAmount;
            if (flags.HasFlag(OpFlags.PeeksStack))
                NeedsStack = true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (NeedsAcc) sb.AppendFormat("NeedsAcc ");
            if (NeedsPprev) sb.AppendFormat("NeedsPprev ");
            if (NeedsPprev) sb.AppendFormat("NeedsStack ");
            sb.Append("[ " + Stack + " ]");
            return sb.ToString();
        }

        public void Reset()
        {
            NeedsAcc = NeedsPprev = NeedsStack = false;
            Stack = 0;
        }
    }
}
