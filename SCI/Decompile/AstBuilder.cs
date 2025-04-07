using System;
using System.Collections.Generic;
using System.Linq;
using SCI.Decompile.Ast;
using SCI.Resource;

// Abstract Syntax Tree Builder (a.k.a. Instruction Consumption)
//
// Input:  Control Flow Graph
// Output: Abstract Syntax Tree
//
// AstBuilder walks the control flow graph in order and builds tree nodes.
// When it encounters a basic block, it processes the instructions in order and
// builds tree nodes. The goal is to build a correct AST, not necessarily a
// pretty one. Afterwards, many visitors will transform the tree into a pleasant
// structure. But if it's easy to make a good decision here, or if postponing
// a decision causes problems, then sure do it here. AstBuilder has a lot of
// context to make good decisions and correct mistakes.
//
// AstBuilder contains a stack and accumulator similar to the SCI VM, except
// that they hold Nodes, so they can contain arbitrary expressions as well as
// simple values. Nodes are added to the current expression list, a List of type
// Node. New scopes are entered by creating a new current expression list and
// recursing. For example, an "If" node in the CFG will result in an "If" AST
// node being stored in the accumulator and added to the current list, but first
// a new current list will be created to store the result of recursing into
// "Then" nodes. (And again if there is an optional "Else".)
//
// Whenever an expression is written to the accumulator, it is added to the
// current expression list. When the accumulator is read, the expression is
// consumed by removing it from the list. The expectation is that it will be
// added back eventually as a child of the newly created node, or a child of its
// child, etc. Compiler optimizations complicate this, but they are The Big Bad
// that AstBuilder was designed for.
//
// Compiler optimizations handled by AstBuilder:
//
// 1. Deleted instructions. Optimizer deletes instructions that set acc when acc
// is known to already contain that value. The value can be a number, variable,
// or property. AstBuilder handles this by mimicking the SCI VM structure and
// recording the accumulator's current expression along with the its value, if
// known. This allows the accumulator to be consumed multiple times. The first
// read returns the original expression. Subsequent reads return the value.
//
// 2. "push" instructions. Optimizer replaces instructions that push a number,
// variable, or property with "push" when acc is known to already contain that
// value. As with deleted instructions, this is handled by allowing acc to be
// consumed multiple times. The stack creates more complications, because the
// "push" instruction isn't only used for optimizations, and because ambiguities
// occur that aren't apparent until the stack is consumed by later instructions.
// This is handled by storing extra context with each stack item, including the
// expression's value (if known) and the source instruction type. This gives
// consumers the context they need to detect and resolve optimizations.
//
// 3. "Dup" instructions. Optimizer replaces instructions that push a number,
// variable, or property with "dup" when the top of the stack is known to
// contain that value. This is handled by storing the full context with each
// stack item. Instead of duplicating the expression on the top of the stack,
// its known value is duplicated. "Dup" is also used in switch case statements,
// but that is irrelevant here because those instructions are excluded when
// building the control flow graph.
//
// There are some other tricks, but I think they're well documented. As with all
// parts of the decompiler, it's designed for the output of real SCI compilers
// and not theoretical ones.
//
// AstBuilder might be my favorite piece of the decompiler. It was fun to write,
// it's kinda fun to read, and it ended up absorbing a lot of complexities while
// remaining comprehensible. I can still reason about it!
//
// All of this is completely different than SCI Companion. Companion consumes
// instructions in reverse order and locates their dependencies to fulfill them,
// also in reverse. Phil's thorough descriptions made it clear that this was too
// much to hold in my head alongside all the other decompiler models, so I hoped
// that I could get away with forward consumption. I also suspected that reverse
// consumption wasn't well suited to handling compiler optimizations. I am now
// convinced that reverse instruction consumption is responsible for most of
// Companion's silent failures that produce incorrect code. I don't know if
// these bugs are shallow or not, and I doubt we'll ever find out, but it's
// still neat that two such different approaches can yield good results.

namespace SCI.Decompile
{
    class AstBuilder
    {
        Script script;
        Function function;
        Symbols symbols;
        int parameterCount;

        [Flags]
        enum StackItemFlags
        {
            None = 0, // stack
            Push = 1, // created by a push or dup instruction
            Dup = 2,  // created by a dup instruction (currently unused, but it helps me)
            UnknownExpression = 4 // unknown expression from compiler bug; throw if consumed
        }

        class StackItem
        {
            public StackItemFlags Flags; // set by Push and Dup instructions
            public Node Expression;      // the expression!
            public Number Number;        // accNumber when expression was added
            public Node Symbol;          // accSymbol when expression was added

            public override string ToString() { return Expression.ToString(); }
        }

        Stack<StackItem> stack = new Stack<StackItem>();
        Node accExpression; // anything          (initially null)
        Number accNumber;   // number            (null when unknown)
        Node accSymbol;     // variable/property (null when neither)
        bool accCommitted;  // when true, accExpression can't be consumed
        Node currentExpressionList;  // expressions are added to this node
                                     // when setting acc. when consuming acc,
                                     // if accExpression is uncommitted and
                                     // the last one in the list, it is removed.
        Stack<Node> expressionLists; // stack of expression lists for scope.
        Number one; // i have to prime accNumber with this sometimes

        public static Ast.Node Run(Cfg.Graph cfg, InstructionList il, Function function, Symbols symbols, int parameterCount)
        {
            var builder = new AstBuilder();
            builder.script = function.Script;
            builder.function = function;
            builder.symbols = symbols;
            builder.parameterCount = parameterCount;
            var functionNode = new Ast.Node(NodeType.List);
            builder.currentExpressionList = functionNode;
            builder.expressionLists = new Stack<Ast.Node>();
            builder.one = new Number(1);
            builder.ProcessNode(cfg, cfg.Start);
            return functionNode;
        }

        void ProcessNode(Cfg.Graph cfg, Cfg.Node cfgNode)
        {
            while (cfgNode != null)
            {
                switch (cfgNode.Type)
                {
                    case Cfg.NodeType.Block:
                        // Create expressions from the instructions within this
                        // basic block and add them to the expression list.
                        CreateExpressions(cfgNode);
                        break;

                    case Cfg.NodeType.Subgraph:
                        ProcessNode(cfgNode.Subgraph, cfgNode.Subgraph.First());
                        break;

                    case Cfg.NodeType.And:
                    case Cfg.NodeType.Or:
                        {
                            // Each operand produces an acc expression to consume... usually.
                            // This one should be simple, and was, until I started logging weird things.
                            var operation = new Ast.Node(cfgNode.Type == Cfg.NodeType.And ? Ast.NodeType.And : Ast.NodeType.Or);
                            var operandNodes = cfg.SuccessorNodes(cfgNode, Cfg.EdgeType.Operand);
                            bool isFirstOperandNode = true;
                            foreach (var operandNode in operandNodes)
                            {
                                // each operand "should" be one expression, so i should be able to just
                                // process the node on the current expression list and consume acc.
                                // that's how the first operand has to work since it may contain all kinds
                                // of CFG nodes that CFA absorbed as If tests.
                                // but the others, in rare cases, will evaluate to multiple expressions,
                                // so they need to be processed into their own list. if processing produces
                                // just one expression, great, discard the list and use the expression.
                                // otherwise, use the list. it was a permissive compiler!
                                // Pepper example: (and a b (c d)), where d is what affects the logic
                                if (isFirstOperandNode)
                                {
                                    isFirstOperandNode = false;
                                    ProcessNode(cfg, operandNode);
                                    operation.Add(ConsumeAcc());
                                }
                                else
                                {
                                    var operandList = new Ast.Node(NodeType.List);
                                    PushCurrentExpressionList(operandList);
                                    ProcessNode(cfg, operandNode);
                                    PopCurrentExpressionList();
                                    if (operandList.Children.Count == 1)
                                    {
                                        operation.Add(operandList.Children[0]);
                                    }
                                    else if (operandList.Children.Count == 0)
                                    {
                                        // TODO: try disabling the "no blank bnt nodes" ignorer in CFA and see if
                                        // this catches them. if so, i could allow them and just create an (and ) with
                                        // only one node, which is what i think might be happening. could be more accurate.
                                        // i'm keen to remove any CFA code that i can.
                                        throw new Exception(operation.Type.ToString() + " has operand with no nodes");
                                    }
                                    else
                                    {
                                        // i'm very interested in these so log them for now.
                                        // - pepper where they did ((= local1 1) (gCurRoom setScript: ...)) as an And operand
                                        // - qfg1vga HandsOff (not (for ...)) as an And operand until i make a heuristic
                                        //   that creates obvious for loops and absorbs the leading assignment node.
                                        // - NRS assembled stuff (qfg3, at least)
                                        Log.Warn(function, operation.Type + " operand is a list of " + operandList.Children.Count + " nodes");
                                        operation.Add(operandList);
                                    }
                                }
                            }
                            SetAcc(operation);
                        }
                        break;

                    case Cfg.NodeType.If:
                        {
                            // Process the Test node by building expressions and adding
                            // them to the current expression list. Many will be unrelated
                            // to the if statement, they just come before it.
                            // Finally, consume the expression in acc; that's the Test.
                            var testNode = cfg.Successor(cfgNode, Cfg.EdgeType.IfTest);
                            int preTestExpCount = currentExpressionList.Children.Count;
                            ProcessNode(cfg, testNode);
                            int postTestExpCount = currentExpressionList.Children.Count;

                            // A weird situation: a return expression could have been used
                            // as a test. hoyle4 #752 LeadReturn_NoTrump:checkKxQxx.
                            // The return value is an expression so it wasn't obvious that
                            // it should consume the previous expression; i plan on figuring
                            // that out after the AST is built. but in this case, yikes.
                            // if multiple expressions were created and the last one is
                            // a Return that doesn't have a value, give the predecessor to Return.
                            if (postTestExpCount - preTestExpCount > 1 &&
                                currentExpressionList.Children.Last().Type == NodeType.Return &&
                                currentExpressionList.Children.Last().Children.Count == 0)
                            {
                                var return_ = currentExpressionList.Children[postTestExpCount - 1];
                                var returnValue = currentExpressionList.Children[postTestExpCount - 2];
                                currentExpressionList.Remove(returnValue);
                                return_.Add(returnValue);
                                postTestExpCount--;
                            }

                            // okay now we can consume acc
                            var test = ConsumeAcc();

                            // Process the Then node by creating a new node and building
                            // expressions into that list.
                            var thenNode = cfg.Successor(cfgNode, Cfg.EdgeType.IfThen);
                            var then = new Ast.Node(NodeType.List);
                            PushCurrentExpressionList(then);
                            ProcessNode(cfg, thenNode);
                            PopCurrentExpressionList();

                            // "Then" can be empty for two reasons:
                            // 1. There really was no text in the source.
                            // 2. The optimizer removed the instruction that set the accumulator.
                            // I'm assuming that the optimizer removed it, so if we can consume
                            // a symbol then do that. Maybe I should tag it so that a visitor
                            // can remove it later if it doesn't make sense in the AST.
                            // UPDATE: Yes, that is a good justification for node flags and the
                            // Copy flag i briefly added earlier for something else. It's easy
                            // to tag copies, and a visitor will have all the context to make
                            // good decisions.
                            // InvI:showSelf()
                            //     source: (if description description else name)
                            //  companion: (if description else name)
                            if (then.Children.Count == 0)
                            {
                                // it's probably going to be a symbol; if it's a number then
                                // the code is weird, something like: "if 1 then 1".
                                // brain2 bookCase:dispatchEvent does this: "if (= temp5 0)"
                                // and no body. it's part of a cond, they meant (== temp5 0).
                                //
                                // don't call ConsumeAcc() because that throws when acc
                                // is unknown, and it can be unknown when this is a genuine
                                // empty Then statement.
                                if (accNumber != null)
                                {
                                    then.Add(accNumber.Copy());
                                }
                                else if (accSymbol != null)
                                {
                                    then.Add(accSymbol.Copy());
                                }
                            }

                            // Process the optional Else node just like Then
                            var elseNode = cfg.Successor(cfgNode, Cfg.EdgeType.IfElse);
                            Ast.Node else_ = null;
                            if (elseNode != null)
                            {
                                if ((cfgNode.Flags & (Cfg.NodeFlag.AddElseBreak | Cfg.NodeFlag.AddElseContinue)) != Cfg.NodeFlag.None)
                                {
                                    throw new Exception("CFA gave an If-Then-Else the flag for adding an Else-Break/Continue");
                                }
                                ClearAcc(); // see function comments for why this is needed on If-Else
                                else_ = new Ast.Node(NodeType.List);
                                PushCurrentExpressionList(else_);
                                ProcessNode(cfg, elseNode);
                                PopCurrentExpressionList();
                            }
                            else if ((cfgNode.Flags & (Cfg.NodeFlag.AddElseBreak | Cfg.NodeFlag.AddElseContinue)) != Cfg.NodeFlag.None)
                            {
                                // generate an else containing a break or continue
                                else_ = new Ast.Node(NodeType.List);
                                var type = cfgNode.Flags.HasFlag(Cfg.NodeFlag.AddElseBreak) ? NodeType.Break : NodeType.Continue;
                                var breakOrContinue = new Ast.Node(type);
                                if (cfgNode.LoopLevel > 1)
                                {
                                    breakOrContinue.Add(new Ast.Number(cfgNode.LoopLevel));
                                }
                                else_.Add(breakOrContinue);
                            }

                            // Add the If statement to the expression list
                            SetAcc(new Ast.If(test, then, else_));
                        }
                        break;

                    case Cfg.NodeType.Switch:
                        {
                            var switch_ = new Ast.Switch();

                            // switch nodes hold everything in a subgraph.
                            var switchGraph = cfgNode.Subgraph;

                            // process the head node
                            var headNode = switchGraph.Successor(switchGraph.Start, Cfg.EdgeType.SwitchHead);
                            ProcessNode(switchGraph, headNode);

                            // i *want* to pop the head off the stack here before processing cases,
                            // but i have to leave it, because a case test could have been optimized
                            // to include a dup, so it depends on the current value on the stack.
                            // insane. and it only happens once! (qfg4 floppy # 670 pMainDoor:changeState)
                            // i'll remove it from the stack later, along with any leaked stack items.
                            switch_.Add(Peek());

                            // process every case node.
                            // case nodes hang off of the head node.
                            foreach (var caseCfgNode in switchGraph.SuccessorNodes(headNode, Cfg.EdgeType.SwitchCase))
                            {
                                var case_ = new Case();
                                if (caseCfgNode.Flags.HasFlag(Cfg.NodeFlag.CompilerBug))
                                {
                                    // empty-case compiler bug
                                    case_.Flags |= Ast.NodeFlags.CompilerBug;
                                }

                                // switch case test
                                var testCfgNode = switchGraph.Successor(caseCfgNode, Cfg.EdgeType.CaseTest);
                                if (testCfgNode != null)
                                {
                                    // this should only produce ONE expression which gets immediately consumed
                                    // to remove from the current expression list.
                                    // or maybe i need to create a new expression list here, recurse, and then
                                    // extract the one expression and assert that there's only one.
                                    ProcessNode(switchGraph, testCfgNode);
                                    case_.Add(ConsumeAcc());
                                }
                                else
                                {
                                    // the test was optimized out. consume from acc to deoptimize.
                                    case_.Add(ConsumeAcc());
                                }

                                // switch case body
                                var bodyCfgNode = switchGraph.Successor(caseCfgNode, Cfg.EdgeType.CaseBody);
                                if (bodyCfgNode != null)
                                {
                                    // case tests end in eq?, which caused the optimizer to reset acc to unknown.
                                    ClearAcc();

                                    // EXCEPT: qfg3 uhuraCompete:changeState(3) starts with a push before setting acc!!
                                    // this was a bug in the compiler's optimizer. from context i can see that there were
                                    // originally multiple ldi 03's and pushi 03's, then it deleted one of the ldi's and
                                    // optimized the wrong pushi. the result is: eq?+bnt+...+push. this causes the stale
                                    // acc value 1 to be pushed as a view loop instead of the intended 3, causing a very
                                    // brief animation glitch that you have to be looking for to really notice.
                                    // sure. fine. whatever. set acc to 1, which is indeed what it is at runtime at
                                    // the start of a case body, even though that's an implementation detail that can't
                                    // affect correctly compiled code. this allows qfg3 to decompile and produce the
                                    // the code that shipped. Companion emits: "loop: (== 1 3)". not 3, not 1, but 0!
                                    accExpression = one;
                                    accNumber = one;

                                    var caseBody = new Node(NodeType.List);
                                    PushCurrentExpressionList(caseBody);
                                    ProcessNode(switchGraph, bodyCfgNode);
                                    PopCurrentExpressionList();
                                    case_.Add(caseBody);
                                }
                                else
                                {
                                    // no case body; just put an empty list in.
                                    case_.Add(new Node(NodeType.List));
                                }

                                switch_.Add(case_);
                            }

                            // switch else
                            var elseCfgNode = switchGraph.Successor(headNode, Cfg.EdgeType.SwitchElse);
                            if (elseCfgNode != null)
                            {
                                ClearAcc();
                                var else_ = new Else();
                                var elseBody = new Node(NodeType.List);
                                else_.Add(elseBody);
                                PushCurrentExpressionList(elseBody);
                                ProcessNode(switchGraph, elseCfgNode);
                                PopCurrentExpressionList();
                                switch_.Add(else_);
                            }

                            // okay now it's safe to pop the head.
                            // but the head may not be the top of the stack,
                            // because script bugs can leak stack elements.
                            // (loose numbers in source code created pushi's, ldi's, pushi's...)
                            // pop everything until the head is popped; any extra is
                            // garbage, and it can screw up already screwy situations.
                            // throwing it all away lets the infamous sq4 sequel police item
                            // bug decompile. it passes a switch as a parameter to Print,
                            // and that switch has cases that leak stack.
                            while (PopItemWithoutValidation().Expression != switch_.Head) { }

                            SetAcc(switch_);
                        }
                        break;

                    case Cfg.NodeType.Loop:
                        {
                            // We come into loop-building with several pieces of information.
                            // Some info is always right, some is ambiguous, some can be wrong.
                            //
                            // LoopType: The type (or types) of loops that this could be.
                            //
                            // Subgraph nodes: All the parts of the loop that have been identified.
                            // Each node hangs off of Subgraph's Start node, even though I don't
                            // think that's how I intended Start to ever be used. oops.
                            // It doesn't seem to be a problem, but if it ever becomes one, just
                            // hang the others off of Body since it's the only mandatory one.
                            var loopGraph = cfgNode.Subgraph;
                            var bodyNode = loopGraph.Successor(loopGraph.Start, Cfg.EdgeType.LoopBody);
                            var testNode = loopGraph.Successor(loopGraph.Start, Cfg.EdgeType.LoopTest);
                            var forReinitNode = loopGraph.Successor(loopGraph.Start, Cfg.EdgeType.ForLoopReinit);

                            ClearAcc();

                            // Loop Test - Do this first because this may be wrong; CFA may have misidentified
                            // a bnt as a while test when really it's an optimized bnt. If that happened,
                            // it's easy to detect, because the test will evaluate to multiple expressions
                            // instead of one. Just change course; it's a repeat. Now we know!
                            Ast.Node test = null;
                            Ast.Node tempList = null; // if this gets set then Oops, it's a repeat
                            if (testNode != null)
                            {
                                tempList = new Ast.Node(NodeType.List);
                                PushCurrentExpressionList(tempList);
                                ProcessNode(loopGraph, testNode);
                                if (tempList.Children.Count <= 1)
                                {
                                    test = ConsumeAcc();
                                    tempList = null;
                                }
                                else
                                {
                                    if (cfgNode.LoopType == LoopType.For) throw new Exception("For loop test contained multiple expressions: " + tempList.Children.Count);
                                    Log.Debug(function, "While loop is really a repeat, multiple test expressions: " + tempList.Children.Count);
                                    cfgNode.LoopType = LoopType.Repeat;
                                }
                                PopCurrentExpressionList();
                            }

                            // Loop Body
                            Ast.Node body;
                            if (tempList == null)
                            {
                                // normal loop body processing
                                body = new Ast.Node(NodeType.List);
                                PushCurrentExpressionList(body);
                                ProcessNode(loopGraph, bodyNode);
                                if (cfgNode.LoopType == LoopType.DoWhile)
                                {
                                    // Loop was detected as a DoWhile from a third party compiler.
                                    // This could be a false positive though, it could just be
                                    // a while loop with a test but no body.
                                    if (body.Children.Count == 1)
                                    {
                                        // Transfer the test from the otherwise empty loop body.
                                        test = body.Children[0];
                                        body.Remove(0);
                                    }
                                    else
                                    {
                                        // The DoWhile test is at the end of the loop body.
                                        // Just consume it and wrap it in a breakif/not,
                                        // unless that would create an ugly double-not.
                                        Ast.Node doWhileTest = ConsumeAcc();
                                        var breakif = new Ast.Node(NodeType.BreakIf);
                                        if (doWhileTest.Type != NodeType.Not)
                                        {
                                            breakif.Add(new Ast.Node(NodeType.Not, doWhileTest));
                                        }
                                        else
                                        {
                                            breakif.Add(doWhileTest.Children[0]);
                                        }
                                        Add(breakif);
                                    }
                                }
                                PopCurrentExpressionList();
                            }
                            else
                            {
                                // "Oops" loop body processing
                                // CFA thought this was a while loop but upon consuming expressions,
                                // it turns out that there are expressions before what it thought
                                // was the while test, so the bnt was from an if test and optimized.
                                // Fix this by instead creating a repeat loop whose body begins with
                                // whatever expressions we built, followed by an If whose Test is
                                // the last expression we built, and whose Then is the rest of the
                                // loop body, and whose Else is a Break.
                                body = tempList;
                                tempList = null;
                                PushCurrentExpressionList(body);

                                // consume last expression as an If Test
                                var ifTest = ConsumeAcc();

                                // process the rest of the loop body as an If Then
                                var ifThen = new Ast.Node(NodeType.List);
                                PushCurrentExpressionList(ifThen);
                                ProcessNode(loopGraph, bodyNode);
                                PopCurrentExpressionList();

                                // create an If with an Else-Break and add it to the loop body
                                var ifElse = new Ast.Node(NodeType.List);
                                ifElse.Add(new Ast.Node(NodeType.Break));
                                SetAcc(new Ast.If(ifTest, ifThen, ifElse));
                                PopCurrentExpressionList();
                            }

                            // Before we go any further, if this *could* be a for loop,
                            // check to see if it has an obvious for loop structure.
                            // This is a heuristic. If so, great, it's a for loop.
                            if (cfgNode.LoopType.HasFlag(LoopType.ForOrWhile))
                            {
                                if (LooksLikeForLoop(
                                        currentExpressionList.Children.LastOrDefault(),
                                        body.Children.LastOrDefault()))
                                {
                                    cfgNode.LoopType = LoopType.For;
                                }
                            }

                            // For Init
                            Ast.Node forInit = null;
                            if (cfgNode.LoopType == LoopType.For)
                            {
                                // for init is a list of expressions and we can never know the
                                // true start of that list, so just take the one previous expression.
                                // i am not modeling init as a list since it will never be one
                                // when decompiling.

                                forInit = currentExpressionList.Children.Last();
                                currentExpressionList.Remove(forInit);
                            }

                            // For Reinit
                            Ast.Node forReinit = null;
                            if (cfgNode.LoopType == LoopType.For)
                            {
                                // if CFA identified the reinit, use that.
                                // otherwise, pull the last expression in body.
                                // reinit is a list of expressions.
                                if (forReinitNode != null)
                                {
                                    forReinit = new Ast.Node(NodeType.List);
                                    PushCurrentExpressionList(forReinit);
                                    ProcessNode(loopGraph, forReinitNode);
                                    PopCurrentExpressionList();
                                }
                                else
                                {
                                    forReinit = new Ast.Node(NodeType.List);
                                    var lastBodyNode = body.Children.Last();
                                    body.Remove(lastBodyNode);
                                    forReinit.Add(lastBodyNode);
                                }
                            }

                            // done!
                            var loop = new Ast.Loop();
                            loop.Add(body);
                            if (test != null) loop.Add(test);
                            if (forInit != null) loop.Add(forInit);
                            if (forReinit != null) loop.Add(forReinit);

                            SetAcc(loop);
                        }
                        break;

                    case Cfg.NodeType.Start:
                    case Cfg.NodeType.End:
                        break;

                    default: throw new Exception("Unsupported cfg node type: " + cfgNode.Type);
                }
                cfgNode = cfg.Follower(cfgNode);
            }
        }

        static Dictionary<Operation, NodeType> TypeMap = new Dictionary<Operation, NodeType>
        {
            // math
            { Operation.bnot, NodeType.BinNot },
            { Operation.add, NodeType.Add },
            { Operation.sub, NodeType.Sub },
            { Operation.mul, NodeType.Mul },
            { Operation.div, NodeType.Div },
            { Operation.mod, NodeType.Mod },
            { Operation.shr, NodeType.Shr },
            { Operation.shl, NodeType.Shl },
            { Operation.xor, NodeType.Xor },
            { Operation.and, NodeType.BinAnd },
            { Operation.or, NodeType.BinOr },
            { Operation.neg, NodeType.Neg },
            { Operation.not, NodeType.Not },
            // comparison
            { Operation.eq, NodeType.Eq },
            { Operation.ne, NodeType.Ne },
            { Operation.gt, NodeType.Gt },
            { Operation.ge, NodeType.Ge },
            { Operation.lt, NodeType.Lt },
            { Operation.le, NodeType.Le },
            { Operation.ugt, NodeType.Ugt },
            { Operation.uge, NodeType.Uge },
            { Operation.ult, NodeType.Ult },
            { Operation.ule, NodeType.Ule },
        };

        void CreateExpressions(Cfg.Node block)
        {
            // foreach instruction in the block.
            // kinda ugly because block range is inclusive and i don't want to rely on Position values
            // being sequential, that way i can allow inserting instructions.
            // UPDATE: i never ended up inserting instructions, and CFA ended up heavily relying on
            //         positions, so even when it would have been useful to insert, i couldn't. ha.
            bool processedLastInstruction = false;
            for (Instruction i = block.First; i != null && !processedLastInstruction; i = i.Next)
            {
                processedLastInstruction = (i == block.Last);

                switch (i.Operation)
                {
                    // math
                    case Operation.bnot:
                    case Operation.neg:
                    case Operation.not:
                        SetAcc(new Node(TypeMap[i.Operation], ConsumeAcc()));
                        break;
                    case Operation.add:
                    case Operation.sub:
                    case Operation.mul:
                    case Operation.div:
                    case Operation.mod:
                    case Operation.shr:
                    case Operation.shl:
                    case Operation.xor:
                    case Operation.and:
                    case Operation.or:
                        // WHOA: there is a unique optimization (or bug) in Longbow Table:at.
                        //   acc = big expression, value is unknown
                        //   push   ; expression result is in acc and on the stack
                        //   add    ; what?! nobody set acc, and its value is unknown at compile time!
                        // this has the effect of multiplying by two, but with no gain over push2+mul.
                        // it feels like a compiler bug, and it doesn't appear anywhere else, but i
                        // have no idea what the intent was or what the original source would look like.
                        // it thwarts my expression consumption, because the expression is consumed a
                        // second time by "add" but its value is unknown so there is nothing to return.
                        // Companion produces code that looks right, but it duplicates nodes, which i
                        // assume is not how the original source looked; had there been a function
                        // call, Companion would have emitted it twice even though it executes once.
                        // since the effect is to multiply by two, that's how i'm interpreting it.
                        if (i.Operation == Operation.add && i.Prev.Operation == Operation.push)
                        {
                            // acc = (* 2 Pop())
                            SetAcc(new Node(NodeType.Mul, new Number(2), Pop()));
                        }
                        else if (i.HasFlag(InstructionFlag.MathAssignment))
                        {
                            // Preprocessing detected a third party compiler instruction sequence
                            // for arithmetic assignment. The confusing instructions were deleted,
                            // and the math instruction was flagged, so all we have to do here is
                            // select the right node type and propagate the compiler-bug flag.
                            NodeType mathAssignment = MathAssignmentCreator.Map[TypeMap[i.Operation]];
                            Ast.Node newNode = new Node(mathAssignment, Pop(), ConsumeAcc());
                            if (i.HasFlag(InstructionFlag.CompilerBug))
                            {
                                newNode.Flags |= NodeFlags.CompilerBug;
                            }
                            if (i.HasFlag(InstructionFlag.Inconsumable))
                            {
                                // the sequence ended in a ss* instruction; it can't be consumed
                                Add(newNode);
                                accCommitted = true; // the expression can't be consumed
                            }
                            else
                            {
                                // the sequence ended in a sa* instruction; it can be consumed
                                SetAcc(newNode);
                            }
                        }
                        else
                        {
                            SetAcc(new Node(TypeMap[i.Operation], Pop(), ConsumeAcc()));
                        }
                        break;
                    // comparison
                    case Operation.eq:
                    case Operation.ne:
                    case Operation.gt:
                    case Operation.ge:
                    case Operation.lt:
                    case Operation.le:
                    case Operation.ugt:
                    case Operation.uge:
                    case Operation.ult:
                    case Operation.ule:
                        if (Peek().Type != NodeType.Prev)
                        {
                            SetAcc(new Compare(TypeMap[i.Operation], Pop(), ConsumeAcc()));
                        }
                        else
                        {
                            // There was a pprev instruction earlier.
                            // The last expression in the current expression list is
                            // a comparison expression. Consume the acc and add it as
                            // the last operand, then re-set the current expression.
                            Pop(); // throw away Prev
                            var comparisonOperand = ConsumeAcc();
                            var comparison = currentExpressionList.Children.Last();
                            currentExpressionList.Remove(currentExpressionList.Children.Count - 1);
                            comparison.Add(comparisonOperand);
                            SetAcc(comparison); // re-add to keep state good
                        }
                        break;
                    case Operation.ldi:
                        SetAcc(new Number(i.Parameters[0]));
                        break;
                    case Operation.push:
                        // "push" is a problem instruction because it *could* be an optimization.
                        // record the known acc state in case this is an optimization that needs
                        // to be untangled later.
                        Push(ConsumeAcc(), StackItemFlags.Push, accNumber, accSymbol);
                        break;
                    case Operation.pushi:
                        Push(new Number(i.Parameters[0]));
                        break;
                    case Operation.dup:
                        // "dup" is also a problem instruction, because it could be a number or a symbol.
                        // third party compiler bugs also did spurious dups, so it could be who knows what.
                        // record the state at the time of the duplicated item for untangling later.
                        // if an expression with an unknown value is dup'd due to a compiler bug, that's
                        // bad, but allow it as long as no one ever consumes the unknown stack item.
                        {
                            StackItem oldItem = PeekItemWithoutValidation();

                            Ast.Node newNode = null;
                            switch (oldItem.Expression.Type)
                            {
                                // we can happily dup numbers, variables, and properties
                                case NodeType.Number:
                                case NodeType.Variable:
                                case NodeType.Property:
                                    newNode = oldItem.Expression.Copy();
                                    break;
                                default:
                                    // dup the number (if known) that was pushed
                                    if (oldItem.Number != null)
                                    {
                                        newNode = oldItem.Number.Copy();
                                    }
                                    // dup the symbol (if known) that was pushed
                                    else if (oldItem.Symbol != null)
                                    {
                                        newNode = oldItem.Symbol.Copy();
                                    }
                                    break;
                            }
                            if (newNode != null)
                            {
                                Push(newNode, StackItemFlags.Push | StackItemFlags.Dup, oldItem.Number, oldItem.Symbol);
                            }
                            else
                            {
                                // third party compiler bug: a spurious dup that can't be evaluated for copying.
                                // i allow this as long as it's only consumed once; its only crime is leaking stack.
                                // but if anyone tries to consume an unknown item later, Pop() will throw.
                                PopItemWithoutValidation(); // remove the old item from the stack, so that we can place it at the top
                                Push(oldItem.Expression, StackItemFlags.UnknownExpression);
                                Push(oldItem.Expression, StackItemFlags.Push | StackItemFlags.Dup);
                            }
                        }
                        break;
                    case Operation.toss:
                        // ignore toss, it shows up at the end of some blocks when carving up the graph
                        break;
                    // call
                    case Operation.call:
                        SetAcc(new LocalCall(symbols.LocalProcedure(script, i.Parameters[0]), i.Parameters[0], PopCallParameters(i.Parameters[1])));
                        break;
                    case Operation.callk:
                        SetAcc(new KernelCall(symbols.KernelFunction(i.Parameters[0]), i.Parameters[0], PopCallParameters(i.Parameters[1])));
                        break;
                    case Operation.callb:
                        SetAcc(new PublicCall(symbols.PublicProcedure(0, i.Parameters[0]), 0, (UInt16)i.Parameters[0], PopCallParameters(i.Parameters[1])));
                        break;
                    case Operation.calle:
                        SetAcc(new PublicCall(symbols.PublicProcedure(i.Parameters[0], i.Parameters[1]), (UInt16)i.Parameters[0], (UInt16)i.Parameters[1], PopCallParameters(i.Parameters[2])));
                        if (i.HasFlag(InstructionFlag.CompilerBug))
                        {
                            // kq5 pc98 compiler bug
                            accExpression.Flags |= Ast.NodeFlags.CompilerBug;
                        }
                        break;
                    case Operation.ret:
                        SetAcc(new Node(NodeType.Return, ConsumeAccForReturn()));
                        break;

                    case Operation.send:
                        SetAcc(new Send(ConsumeAcc(), PopSendMessages(i.Parameters[0])));
                        break;

                    case Operation.class_:
                        SetAcc(new Class(symbols.Class(script, (UInt16)i.Parameters[0]), i.Parameters[0]));
                        break;

                    case Operation.self:
                        SetAcc(new Send(new Node(NodeType.Self), PopSendMessages(i.Parameters[0])));
                        break;

                    case Operation.pushInfo:
                        Push(new Node(NodeType.Info));
                        break;
                    case Operation.info:
                        SetAcc(new Node(NodeType.Info));
                        break;
                    case Operation.pushSuperP:
                        Push(new Node(NodeType.Super));
                        break;
                    case Operation.superP:
                        // never used
                        SetAcc(new Node(NodeType.Super));
                        break;

                    case Operation.super:
                        SetAcc(new Send(new Node(NodeType.Super), PopSendMessages(i.Parameters[1])));
                        break;

                    case Operation.rest:
                        // &rest is usually just a keyword, but it can also
                        // be used in this rarely used format: (&rest param1)
                        if (i.Parameters[0] == parameterCount + 1)
                        {
                            Push(new Node(NodeType.Rest));
                        }
                        else
                        {
                            Push(new Node(NodeType.Rest, new Variable(VariableType.Parameter, i.Parameters[0])));
                        }
                        break;

                    case Operation.lea:
                        if (!i.IsComplexVariable())
                        {
                            SetAcc(new AddressOf(new Variable(i.GetVariableType(), i.Parameters[1])));
                        }
                        else
                        {
                            SetAcc(new AddressOf(new ComplexVariable(new Variable(i.GetVariableType(), i.Parameters[1]), ConsumeAcc())));
                        }
                        break;

                    case Operation.selfID:
                        SetAcc(new Node(NodeType.Self));
                        break;

                    case Operation.pprev:
                        // Prev will be popped and discarded by the next comparison instruction
                        Push(new Node(NodeType.Prev));
                        break;

                    // properties
                    case Operation.pToa:
                        SetAcc(new Ast.Property(symbols.Property(function.Object, i.Parameters[0]), i.Parameters[0]));
                        break;
                    case Operation.aTop:
                        // Initially I tried not setting acc, but I need to, otherwise
                        // assignments of complex values to properties followed by push
                        // duplicate the complex value. but it should capture the assignment too.
                        SetAcc(new Assignment(new Ast.Property(symbols.Property(function.Object, i.Parameters[0]), i.Parameters[0]), ConsumeAcc()));
                        break;
                    case Operation.pTos:
                        Push(new Ast.Property(symbols.Property(function.Object, i.Parameters[0]), i.Parameters[0]));
                        break;
                    case Operation.sTop:
                        // Unused by Sierra's compiler, and i haven't seen it in fan games
                        Log.Warn(function, "Unused opcode detected: " + i);
                        Add(new Assignment(new Ast.Property(symbols.Property(function.Object, i.Parameters[0]), i.Parameters[0]), Pop()));
                        accCommitted = true; // the expression can't be consumed
                        break;
                    case Operation.ipToa:
                        SetAcc(new Increment(new Ast.Property(symbols.Property(function.Object, i.Parameters[0]), i.Parameters[0])));
                        break;
                    case Operation.dpToa:
                        SetAcc(new Decrement(new Ast.Property(symbols.Property(function.Object, i.Parameters[0]), i.Parameters[0])));
                        break;
                    case Operation.ipTos:
                        Push(new Increment(new Ast.Property(symbols.Property(function.Object, i.Parameters[0]), i.Parameters[0])));
                        break;
                    case Operation.dpTos:
                        Push(new Decrement(new Ast.Property(symbols.Property(function.Object, i.Parameters[0]), i.Parameters[0])));
                        break;

                    case Operation.lofsa:
                    case Operation.loadID:
                        SetAcc(GetByOffset(i.Parameters[0]));
                        break;
                    case Operation.lofss:
                    case Operation.pushID:
                        Push(GetByOffset(i.Parameters[0]));
                        break;

                    case Operation.push0:
                        Push(new Number(0));
                        break;
                    case Operation.push1:
                        Push(new Number(1));
                        break;
                    case Operation.push2:
                        Push(new Number(2));
                        break;
                    case Operation.pushSelf:
                        Push(new Node(NodeType.Self));
                        break;

                    // load variables
                    case Operation.lag:
                    case Operation.lal:
                    case Operation.lat:
                    case Operation.lap:
                        SetAcc(new Variable(i.GetVariableType(), i.Parameters[0]));
                        break;
                    case Operation.lsg:
                    case Operation.lsl:
                    case Operation.lst:
                    case Operation.lsp:
                        Push(new Variable(i.GetVariableType(), i.Parameters[0]));
                        break;
                    case Operation.lagi:
                    case Operation.lali:
                    case Operation.lati:
                    case Operation.lapi:
                        SetAcc(new ComplexVariable(new Variable(i.GetVariableType(), i.Parameters[0]), ConsumeAcc()));
                        break;
                    case Operation.lsgi:
                    case Operation.lsli:
                    case Operation.lsti:
                    case Operation.lspi:
                        Push(new ComplexVariable(new Variable(i.GetVariableType(), i.Parameters[0]), ConsumeAcc()));
                        break;
                    // store variables
                    case Operation.sag:
                    case Operation.sal:
                    case Operation.sat:
                    case Operation.sap:
                        SetAcc(new Assignment(new Variable(i.GetVariableType(), i.Parameters[0]), ConsumeAcc()));
                        break;
                    case Operation.ssg:
                    case Operation.ssl:
                    case Operation.sst:
                    case Operation.ssp:
                        // Unused by Sierra's compiler, but could appear in fan games
                        Add(new Assignment(new Variable(i.GetVariableType(), i.Parameters[0]), Pop()));
                        accCommitted = true; // the expression can't be consumed
                        break;
                    case Operation.sagi:
                    case Operation.sali:
                    case Operation.sati:
                    case Operation.sapi:
                        SetAcc(new Assignment(new ComplexVariable(new Variable(i.GetVariableType(), i.Parameters[0]), ConsumeAcc()), Pop()));
                        break;
                    case Operation.ssgi:
                    case Operation.ssli:
                    case Operation.ssti:
                    case Operation.sspi:
                        // Unused by Sierra's compiler, but it appears in fan games instead of accumulator versions
                        Add(new Assignment(new ComplexVariable(new Variable(i.GetVariableType(), i.Parameters[0]), ConsumeAcc()), Pop()));
                        accCommitted = true; // the expression can't be consumed
                        break;
                    // plus
                    case Operation.plusag:
                    case Operation.plusal:
                    case Operation.plusat:
                    case Operation.plusap:
                        SetAcc(new Increment(new Variable(i.GetVariableType(), i.Parameters[0])));
                        break;
                    case Operation.plussg:
                    case Operation.plussl:
                    case Operation.plusst:
                    case Operation.plussp:
                        Push(new Increment(new Variable(i.GetVariableType(), i.Parameters[0])));
                        break;
                    case Operation.plusagi:
                    case Operation.plusali:
                    case Operation.plusati:
                    case Operation.plusapi:
                        SetAcc(new Increment(new ComplexVariable(new Variable(i.GetVariableType(), i.Parameters[0]), ConsumeAcc())));
                        break;
                    case Operation.plussgi:
                    case Operation.plussli:
                    case Operation.plussti:
                    case Operation.plusspi:
                        //  Unused by Sierra's compiler, and i haven't seen it in fan games
                        Log.Warn(function, "Unused opcode detected: " + i);
                        Push(new Increment(new ComplexVariable(new Variable(i.GetVariableType(), i.Parameters[0]), ConsumeAcc())));
                        break;
                    // minus
                    case Operation.minusag:
                    case Operation.minusal:
                    case Operation.minusat:
                    case Operation.minusap:
                        SetAcc(new Decrement(new Variable(i.GetVariableType(), i.Parameters[0])));
                        break;
                    case Operation.minussg:
                    case Operation.minussl:
                    case Operation.minusst:
                    case Operation.minussp:
                        Push(new Decrement(new Variable(i.GetVariableType(), i.Parameters[0])));
                        break;
                    case Operation.minusagi:
                    case Operation.minusali:
                    case Operation.minusati:
                    case Operation.minusapi:
                        SetAcc(new Decrement(new ComplexVariable(new Variable(i.GetVariableType(), i.Parameters[0]), ConsumeAcc())));
                        break;
                    case Operation.minussgi:
                    case Operation.minussli:
                    case Operation.minussti:
                    case Operation.minusspi:
                        //  Unused by Sierra's compiler, and i haven't seen it in fan games
                        Log.Warn(function, "Unused opcode detected: " + i);
                        Push(new Decrement(new ComplexVariable(new Variable(i.GetVariableType(), i.Parameters[0]), ConsumeAcc())));
                        break;

                    // break/continue
                    case Operation.jmp:
                        if (i.Flags.HasFlag(InstructionFlag.Break) ||
                            i.Flags.HasFlag(InstructionFlag.Continue))
                        {
                            var type = i.Flags.HasFlag(InstructionFlag.Break) ? NodeType.Break : NodeType.Continue;
                            var breakOrContinue = new Node(type);
                            if (i.LoopLevel > 1)
                            {
                                breakOrContinue.Add(new Number(i.LoopLevel));
                            }
                            Add(breakOrContinue);
                        }
                        break;
                    // breakif/continueif
                    case Operation.bt:
                        if (i.Flags.HasFlag(InstructionFlag.Break) ||
                            i.Flags.HasFlag(InstructionFlag.Continue))
                        {
                            var type = i.Flags.HasFlag(InstructionFlag.Break) ? NodeType.BreakIf : NodeType.ContinueIf;
                            var breakIfOrContinueIf = new Node(type, ConsumeAcc());
                            if (i.LoopLevel > 1)
                            {
                                breakIfOrContinueIf.Add(new Number(i.LoopLevel));
                            }

                            Add(breakIfOrContinueIf);
                        }
                        break;
                }
            }
        }

        void PushCurrentExpressionList(Node expressions)
        {
            expressionLists.Push(currentExpressionList);
            currentExpressionList = expressions;
        }

        void PopCurrentExpressionList()
        {
            currentExpressionList = expressionLists.Pop();
        }

        void SetAcc(Node expression)
        {
            currentExpressionList.Add(expression);
            accExpression = expression;
            if (accExpression.Type != NodeType.Assignment &&
                accExpression.Type != NodeType.Return)
            {
                // accNumber is unchanged by assignment or return
                accNumber = EvalNumber(accExpression);
            }
            accSymbol = EvalSymbol(accExpression);
            accCommitted = false;
        }

        // clears accumulator when crossing a boundary where i know that previous
        // acc values can't be used. given that there is no bytecode that violates
        // this, it's only needed because of ret instructions. it's ambiguous if
        // a ret was a return instruction with a value or if it was optimized out,
        // so ConsumeAccForReturn() is fuzzy, because some bodies like If-Then
        // are just a ret that really returns the value from the If-Test.
        // if i don't clear these on known boundaries, an If-Else that starts
        // with a ret can incorrectly get a copy of a node. (PseudoMouse:handleEvent)
        void ClearAcc()
        {
            accExpression = null;
            accNumber = null;
            accSymbol = null;
            accCommitted = false;
        }

        // simple, but in its own function in case it gets complicated
        Number EvalNumber(Node expression)
        {
            if (expression.Type == NodeType.Number)
            {
                return (Number)expression;
            }
            return null;
        }

        Node EvalSymbol(Node expression)
        {
            switch (expression.Type)
            {
                case NodeType.Assignment:
                case NodeType.Increment:
                case NodeType.Decrement:
                    return EvalSymbol(expression.Children[0]);
                case NodeType.Property:
                case NodeType.Variable:
                    return expression;
                case NodeType.Return:
                    return expression.Children.FirstOrDefault();
                default:
                    return null;
            }
        }

        Node ConsumeAcc()
        {
            if (accExpression == null) throw new Exception("ConsumeAcc() called on empty acc");
            var previous = currentExpressionList.Children.LastOrDefault();
            if (!accCommitted && accExpression == previous)
            {
                // consume the expression in acc
                currentExpressionList.Remove(currentExpressionList.Children.Count - 1);
                return accExpression;
            }

            // accExpression has already been consumed or committed.
            // the caller can receive the number or symbol that acc contains.
            // if both are known, i prefer to use the number.
            // i think either would work, because i push both of these
            // values on the stack in StackItem. parameter popping is
            // the only one who cares about number vs symbol, and so
            // it uses StackItem.accNumber and ignores accSymbol.
            // i think that swapping these would change code only in cases
            // where both would be equivalent. (= x 2) [(Func x) vs (Func 2)]
            if (accNumber != null)
            {
                return accNumber.Copy();
            }
            if (accSymbol != null)
            {
                return accSymbol.Copy();
            }
            throw new Exception("ConsumeAcc() called but acc is unknown");
        }

        Node ConsumeAccForReturn()
        {
            // try to consume acc expression if it's available.
            // it doesn't get cleared when entering a new scope,
            // so only try it if it's in the current expression list.
            if (accExpression != null && currentExpressionList.Children.LastOrDefault() == accExpression)
            {
                if (IsObviousReturnValue(accExpression))
                {
                    return ConsumeAcc();
                }
            }
            else if (accNumber != null)
            {
                if (accNumber != one) // don't be fooled by this workaround for qfg3 compiler bug
                {
                    return accNumber.Copy();
                }
            }
            else if (accSymbol != null)
            {
                return accSymbol.Copy();
            }
            return null;
        }

        static bool IsObviousReturnValue(Node node)
        {
            if (node == null) return false;
            switch (node.Type)
            {
                case NodeType.Number:
                case NodeType.String:
                case NodeType.Said:
                case NodeType.Class:
                case NodeType.Object:
                case NodeType.Self:
                case NodeType.Info:
                case NodeType.Variable:
                case NodeType.ComplexVariable:
                case NodeType.Property:
                case NodeType.AddressOf:
                    return true;
            }
            // unary / math / compare
            if (NodeType.Not <= node.Type && node.Type <= NodeType.Ule)
            {
                return true;
            }
            return false;
        }

        void Push(Node expression, StackItemFlags flags = StackItemFlags.None, Number number = null, Node symbol = null)
        {
            var item = new StackItem { Expression = expression, Flags = flags, Number = number, Symbol = symbol };
            stack.Push(item);

            // acc expression can no longer be consumed
            accCommitted = true;
        }

        StackItem PopItem()
        {
            var item = stack.Pop();
            if (item.Flags.HasFlag(StackItemFlags.UnknownExpression))
            {
                throw new Exception("Consuming a dup'd expression twice: " + item.Expression);
            }
            return item;
        }

        StackItem PopItemWithoutValidation()
        {
            return stack.Pop();
        }

        Node Pop()
        {
            return PopItem().Expression;
        }

        // Peek() lets me peek the stack without getting confused by &rest
        Node Peek()
        {
            var item = PeekItemWithoutValidation();
            if (item.Flags.HasFlag(StackItemFlags.UnknownExpression))
            {
                throw new Exception("Consuming a dup'd expression twice: " + item.Expression);
            }
            return item.Expression;
        }

        StackItem PeekItemWithoutValidation()
        {
            var item = stack.FirstOrDefault(i => i.Expression.Type != NodeType.Rest);
            if (item == null) throw new Exception("Peek called on empty stack");
            return item;
        }

        Node[] PopCallParameters(int frameSize)
        {
            var parameters = new List<Node>((frameSize / 2) + 1);
            int parametersToPop = frameSize / 2;
            while (parametersToPop > 0)
            {
                var p = PopItem();

                // &rest doesn't count against the frame size,
                // and it can appear anywhere in the parameter list.
                if (p.Expression.Type == NodeType.Rest)
                {
                    parametersToPop++;
                }

                parameters.Add(p.Expression);
                parametersToPop--;
            }

            // pop parameter count. this could also be &rest, so handle that
            var parameterCountItem = PopItem();
            while (parameterCountItem.Expression.Type == NodeType.Rest)
            {
                parameters.Add(parameterCountItem.Expression);
                parameterCountItem = PopItem();
            }

            // the parameter count pushi may have been optimized into a push.
            // if that happened then we consumed an expression that is not a part
            // of this call. undo that by adding the consumed expression back to
            // the expression list and use the numeric value at the time of the push.
            Number parameterCountNode;
            if (parameterCountItem.Expression.Type == NodeType.Number)
            {
                parameterCountNode = (Number)parameterCountItem.Expression;
            }
            else
            {
                currentExpressionList.Add(parameterCountItem.Expression);
                parameterCountNode = parameterCountItem.Number;
                if (parameterCountNode == null) throw new Exception("Can't figure out call parameter count: " + parameterCountItem.Expression);
            }

            // validate that parameter count matches frame size
            int pushedParameterCount = parameterCountNode.Value;
            if (pushedParameterCount != frameSize / 2) throw new Exception("Parameter count: " + (frameSize / 2) + " does not match pushed count: " + pushedParameterCount);

            parameters.Reverse();
            return parameters.ToArray();
        }

        SendMessage[] PopSendMessages(int frameSize)
        {
            // pop all send parameters and reverse
            var parameters = new List<StackItem>((frameSize / 2) + 1);
            int parametersToPop = frameSize / 2;
            while (parametersToPop > 0)
            {
                var p = PopItem();

                // &rest doesn't count against the frame size,
                // and it can appear anywhere in the parameter list.
                if (p.Expression.Type == NodeType.Rest)
                {
                    parametersToPop++;
                }

                parameters.Add(p);
                parametersToPop--;
            }
            parameters.Reverse();

            // parse send parameters into messages
            var messages = new List<SendMessage>();
            for (int i = 0; i < parameters.Count; i += 2)
            {
                StackItem selectorItem = parameters[i];
                Node selector;

                // HARD MODE: handling optimizations.
                // If the first selector is the result of a "push" then it could
                // be an optimization OR a dynamic selector from an expression.
                // If it's an optimization and that's caused us to consume an entire
                // expression, then we need to add that back on the expression list
                // and just use the known value at the time of the push.
                if (selectorItem.Flags.HasFlag(StackItemFlags.Push))
                {
                    // we only worry about "push" being used for the first selector.
                    // after that, the commit feature takes care of not consuming
                    // an expression that should be left in the expression list.
                    if (i == 0)
                    {
                        if (selectorItem.Expression.Type == NodeType.Number ||
                            selectorItem.Expression.Type == NodeType.Property ||
                            selectorItem.Expression.Type == NodeType.Variable)
                        {
                            // simple, it's a number/variable/property, just use it
                            selector = selectorItem.Expression;
                        }
                        else if (selectorItem.Number != null)
                        {
                            // this is an expression, and we know its numeric value
                            // at the time of the push. we have mistakenly consumed
                            // this expression; put it back and use the number.
                            currentExpressionList.Add(selectorItem.Expression);
                            selector = selectorItem.Number.Copy();
                        }
                        else if (selectorItem.Symbol != null)
                        {
                            // same as above, but it is a symbol
                            currentExpressionList.Add(selectorItem.Expression);
                            selector = selectorItem.Symbol.Copy();
                        }
                        else
                        {
                            // great, this is an expression that was legitimately
                            // pushed as a selector value, not an optimization.
                            // use the expression!
                            selector = selectorItem.Expression;
                        }
                    }
                    else
                    {
                        // subsequent selectors. the commit feature takes care of this,
                        // we didn't steal an expression we need to put back.
                        // we already consumed a copy of the evaluated value.
                        selector = selectorItem.Expression;
                    }
                }
                else
                {
                    // this wasn't a push or a dup so just take what they gave us.
                    selector = selectorItem.Expression;
                }

                if (selector.Type == NodeType.Number)
                {
                    // immediately promote numeric selector: nodes to Selector
                    int selectorValue = ((Number)selector).Value;
                    string selectorName = symbols.Selector(selectorValue);
                    selector = new Selector(selectorName, selectorValue);
                }

                // parameter count can also be a victim of optimization.
                // this one is easier because it always started out as a pushi
                // and follows a selector push, which commits acc, so all i need
                // to do is see if it's a number, and if it's not, use the number
                // that was known to be in acc at the time of the push.
                // if acc number was unknown then throw.
                StackItem parameterCountItem = parameters[i + 1];
                Number parameterCountNode;
                if (parameterCountItem.Expression.Type == NodeType.Number)
                {
                    parameterCountNode = (Number)parameterCountItem.Expression;
                }
                else
                {
                    parameterCountNode = parameterCountItem.Number;
                    if (parameterCountNode == null) throw new Exception("Can't figure out selector parameter count: " + parameterCountItem.Expression);
                }

                var message = new SendMessage(selector);
                int selectorParameterCountIncludingRest = parameterCountNode.Value;
                for (int j = 0; j < selectorParameterCountIncludingRest; j++)
                {
                    Node parameter = parameters[i + 2 + j].Expression;
                    message.Add(parameter);
                    // &rest isn't included in the parameter count
                    if (parameter.Type == NodeType.Rest)
                    {
                        selectorParameterCountIncludingRest++;
                    }
                }
                // there might be a trailing &rest
                int potentialRestIndex = i + 2 + selectorParameterCountIncludingRest;
                if (potentialRestIndex < parameters.Count)
                {
                    var potentialRest = parameters[potentialRestIndex].Expression;
                    if (potentialRest.Type == NodeType.Rest)
                    {
                        message.Add(potentialRest);
                    }
                }

                messages.Add(message);
                i += message.Parameters.Count();
            }
            return messages.ToArray();
        }

        void Add(Node expression)
        {
            currentExpressionList.Add(expression);
        }

        Node GetByOffset(int offset)
        {
            var obj = script.Objects.FirstOrDefault(o => o.Position == offset);
            if (obj != null)
            {
                return new Ast.Obj(symbols.Object(obj), offset);
            }
            var str = script.Strings.FirstOrDefault(s => s.Position == offset);
            if (str != null)
            {
                return new Ast.String(str.Text);
            }
            var said = script.Saids.FirstOrDefault(s => s.Position == offset);
            if (said != null)
            {
                return new Ast.Said(said.Text);
            }

            // SCI Companion string
            return new Ast.Obj("LOOKUP_ERROR", offset);
        }

        static bool LooksLikeForLoop(Node init, Node reinit)
        {
            if (init == null || reinit == null) return false;

            if (init.Type == NodeType.Assignment &&
                (reinit.Type == NodeType.Assignment ||
                 reinit.Type == NodeType.Increment ||
                 reinit.Type == NodeType.Decrement))
            {
                return init.Children[0].Same(reinit.Children[0]);
            }
            return false;
        }
    }
}
