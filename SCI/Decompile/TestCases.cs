// Test case functions. When Decompiler.RunTests is set, all others are skipped.
//
// These are development artifacts, but they were fun so I'm leaving them in.

namespace SCI.Decompile
{
    public static class Test
    {
        public static TestCase[] Cases =
        {
            // SOLVED
            // WHOA! A (continue 2) that proves the outer loop is a for loop,
            // but i'm incorrectly putting a break at the start of the
            // reinitializer. The break should be the last thing in the body.
            // Not the first weird loop I've seen whose body ends in break.
            new TestCase("hoyle3-dos-1.000", 100, "DominoHand:setNextPosn"),

            // SOLVED
            // crashes, two loops, big function. Companion asm's.
            new TestCase("kq7-dos-2.00b", 23, "localproc_09b6"),

            // SOLVED
            // crashes, one loop, med function. Companion asm's.
            new TestCase("longbow-dos-1.1", 160, "series:changeState"),

            // SOLVED: SolveLoopBnt() detects this and deoptimizes.
            // These loops had bnt optimizations where creating an else-continue
            // would create something like "if A then B else Continue" inside
            // the if test. it was an obvious bnt => bnt => latch optimization.
            new TestCase("longbow-dos-1.1", 541, "Morris:minimax"),
            new TestCase("qfg2-dos-1.105", 695, "jackalMan:initCombat"),

            // SOLVED: SolveLoopBnt() now does smart latch-loop splitting.
            // Small function with a for loop whose body ends in a break,
            // so it never iterates more than once. Companion outputs an
            // if statement instead of a loop which is... not wrong??
            // Still, I want the loop, and I'd really like it to pass the
            // for-loop heuristic. Splitting latch-loop on the latch-instruction
            // scooped up the re-init (++ temp) in the If's Then. Not wrong,
            // but ugly and even with AST cleanup, probably means not detecting
            // it as a for loop. Now it splits on the break and doesn't create
            // an else-break, so the if statement is followed by (break) (++ temp),
            // which is correct, and can be for-looped. Other functions got cleaner
            // too, although I expect AST cleanup would have handled them.
            new TestCase("gk1-cd-dos-1.1", 480, "abortCartoon:changeState"),

            // "SOLVED" (not really!) - hard-code the answers in Workarounds.cs.
            // For-loops with continues (when the continue isn't in a switch).
            // CFA throws because this is effectively a graph-breaking goto.
            new TestCase("brain2-dos-1.0", 293,"weightsPuzzle:buyClue"),
            new TestCase("lsl7-dos-1.01", 64892, "NewHandlerList:handleEvent"),

            // Another for-loop with continue. INN CheckersGame. localproc_9.
            // Original proc name is CanDoJump. Different offset in different
            // INN versions.
            new TestCase("cc", 400, "localproc_2a31"),

            // SOLVED
            // Two expressions are used as a single And operand. A mistake,
            // they probably accidentally surrounded both with parenthesis.
            // I now allow and/or operands to evaluate to multiple expressions.
            // When AstBuilder sees this, it wraps them all in a List, and then
            // FunctionWriter slaps parenthesis around them. Companion asm's.
            // I was silently producing the wrong code, but I just happened to
            // notice when spot checking asm's. This led to fixes in optimization
            // handling when popping send params. (SQ3, Lighthouse deoptimized)
            new TestCase("pepper-dos-1.000", 230, "sTalkPoorRich:changeState"),

            // SOLVED
            // Optimized first selector push. "pushi 3 [y]" => "push".
            // This exposed that my optimization handler in PopSendMessages()
            // was wrong and was placing the "3" back, leaving a stray "3" in
            // the code. Companion silently fails and uses "species" (0).
            new TestCase("sq3-dos-1.0p", 280, "rm280:handleEvent"),

            // SOLVED
            // Lighthouse 2.0 scripts were compiled without optimizations??
            // I haven't checked to see if it's all of them or just some.
            // Like SQ3 above, this caused my "first selector" optimization
            // handler to incorrectly put the selector back on the expression
            // list. Fixing that fixed this too.
            new TestCase("lighthouse-dos-2.0", 800, "sShowGateClose:changeState"),

            // SOLVED
            // switch-continue within for loop. there are only two functions that do this,
            // making them the only two for loops i can prove.
            new TestCase("gk1-cd-dos-1.1", 710, "stuffArray:doit"),

            // SOLVED
            // dagger copy protection, my first decompiled loop (it has two)
            new TestCase("ra-cd-dos-1.1", 18, "rm18:init"),

            // SOLVED
            // Hoyle4 doesn't work unless i remove dead branches, but Hoyle5
            // breaks when i do that. (This is a huge gross function.)
            // SOLUTION: I had misdiagnosed why hoyle4 was failing and updated
            // dead-branches to delete the jmp from bnt-target,jmp-same-target,
            // aka empty Then statements. But that was wrong, and I've reverted.
            // The real reason hoyle4 was failing was that Sierra used a return
            // statement as an If Test (or And operator). AstBuilder now handles
            // this by setting acc to the Return node when creating it, so that
            // the Return can be consumed, even though that's crazy.
            // AstBuilder now also recognizes when an If's Then is a Return, and
            // if Return doesn't have a value because the previous expression
            // wasn't obviously a return value, it moves it over.
            // Companion silently fails on this; it doesn't emit the return.
            new TestCase("hoyle4-dos-2.000", 752, "LeadReturn_NoTrump:checkKxQxx"),

            // SOLVED
            // Hoyle 5 doesn't work when i remove the dead jmp
            // Expected If-Then-Else to merge, but it has no else: If.
            // SOLUTION: Undo DeadBranches deleting the jmp from empty-If Tests.
            // It was the wrong solution to Hoyle4, now it's properly solved.
            new TestCase("hoyle5-cd-win", 1301, "BGPlayer:moveBack"),

            // SOLVED - BuggyBranches regression (Hoyle4)
            // Expected If-Then-Else to merge, but it has no else: If
            new TestCase("lighthouse-dos-1.0c", 351, "slider4Level3:doVerb"),

            // SOLVED - BuggyBranches regression (Hoyle4)
            // Expected If-Then-Else to merge, but it has no else: If
            new TestCase("pq4-cd-dos-1.100.000", 315, "showShoe:changeState"),

            // SOLVED - BuggyBranches regression (Hoyle4)
            // Expected If-Then-Else to merge, but it has no else: If
            new TestCase("pqswat-dos-1.000", 197, "Popper:doVerb"),

            // SOLVED - switch with a case test that dup's switch head.
            // Peek() called on empty stack
            // SOLUTION: AstBuilder leaves switch head on the stack while
            // processing cases/else and removes it afterwards.
            // This is a good example of nonsense allowed by the compiler;
            // they switched on a verb but accidentally put an entire "and"
            // expression as a case value, which the verb can never equal.
            // Companion asm's this.
            new TestCase("qfg4-dos-1.0", 670, "pMainDoor:doVerb"),

            // SOLVED - regression from first attempt at fixing pMainDoor:doVerb
            new TestCase("longbow-dos-1.1", 24, "yeoScript:changeState"),

            // SOLVED - Accidentally fixed while fixing pMainDoor:doVerb.
            // SQ4 sequel police script bug + leaked stack items
            // Parameter count: 1 does not match pushed count: 376
            // SOLUTION: after processing a switch, i now throw away any stack
            // items that were leaked by the cases. i wasn't doing that to fix
            // this, that was just a consequence of popping items on the way
            // to popping the switch head to fix pMainDoor:doVerb's switch.
            new TestCase("sq4-dos-1.052", 376, "sp1:doVerb"),

            // SOLVED - Toss is now added to leaders even when no branches.
            // Sequence contains no elements
            // SOLUTION: the switch is (switch (value) (0)) so there is no
            // bnt, so toss is part of the dup block instead of its own node.
            // that fucked my graph code up. that's fine, toss instructions
            // are now explicitly included as leaders so it gets its own node.
            // Companion produces a weird switch/else and duplicates nodes.
            new TestCase("pq4-cd-dos-1.100.000", 100, "egoScript:changeState"),

            // "SOLVED" - Patch the the garbage instruction in Workarounds.cs.
            // Switch detection fails due to imbalanced stack due to a missing
            // Print statement. Compiler turned the text parameter littering
            // the source file into a text tuple, and turned one of the integers
            // into a meaningless pushi that leaks stack.
            // Value cannot be null.
            new TestCase("pq3-amiga-german-1.000", 53, "floor:doVerb"),

            // "SOLVED" - Patch the broken instructions in Workarounds.cs
            // KQ5 Japanese script/compiler bug (functions are identical)
            // Parameter count: 1 does not match pushed count: 216
            new TestCase("kq5-pc98-0.000.015", 216, "fire:handleEvent"),
            new TestCase("kq5-pc98-0.000.015", 216, "fireRing:handleEvent"),
        };
    }

    public class TestCase
    {
        public string Game;
        public int Script;
        public string Function;

        public TestCase(string game, int script, string function)
        {
            Game = game;
            Script = script;
            Function = function;
        }

        public override string ToString()
        {
            return "[" + Game + "] " + Script + " " + Function;
        }
    }
}
