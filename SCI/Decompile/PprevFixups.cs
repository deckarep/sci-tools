using System;
using SCI.Resource;

// PprevFixups is part one of my two-part solution to handling pprev:
//
//   1. Delete the obligatory bnt instruction that precedes pprev,
//      making it invisible to control flow analysis, making CFA simpler.
//   2. Handle pprev during instruction consumption in AstBuilder,
//      which is very easy to do.
//
// Pprev and the "prev" register only exist to support n-ary comparisons:
//
//   (<= a b c)   ; a <= b and b <= c
//   (== a b c d) ; a == b and b == c and c == d
//
// The first comparison copies b to prev.
// The second comparison pushes b with pprev to compare with c.
// ...
// This allows b to be evaluated once and compared twice.
//
// Because pprev is only used in this specific case, it always appears
// in the same sequence of instructions:
//
//   comparison opcode
//   bnt LABEL_X [ usually optimized ]
//   pprev
//   acc = third comparison value
//   comparison opcode
//   LABEL_X
//
// The bnt complicates control flow analysis, because CFA is all about
// evaluating branches, but we already know exactly what this bnt is for,
// and it adds no information. Just delete it! That reduces this to one
// basic block, making pprev opaque to control flow analysis. Pprev is
// only relevant to instruction consumption. AstBuilder handles this by
// switching to building an n-ary comparison when it sees pprev, and
// with very little code.
//
// Companion's decompiler doesn't produce n-ary comparisons. It creates
// multiple binary comparisons, so if b is a function call then it will
// repeat b as if it is called twice, which it isn't. But I think it's
// also likely to just fail control flow analysis and asm the whole
// function; I've seen a lot of small functions fail to decompile that
// have pprev in them, and the bnt is usually branch-to-branch optimized.
// Those graphs get a lot simpler when you just rip out the branch.
//
// UPDATE: Companion's compiler emits pprev creatively in a rare case.
// This breaks my "pprev is only for n-ary" statement, so apply
// ThirdPartyHacks.FixupMathAssignments() first to handle and
// remove those distinct instruction sequences.

namespace SCI.Decompile
{
    static class PprevFixups
    {
        public static void Fixup(InstructionList instructions)
        {
            Instruction i = instructions.First;
            do
            {
                if (i.Operation == Operation.pprev)
                {
                    if (i.Prev.Operation != Operation.bnt)
                        throw new Exception("pprev preceded by: " + i.Prev);

                    instructions.Remove(i.Prev); // bnt
                }
            } while ((i = i.Next) != null);
        }
    }
}
