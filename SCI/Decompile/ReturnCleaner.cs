using System;
using System.Collections.Generic;
using System.Linq;

// Return statements are annoying, so I saved this for the very last.
//
// All functions return a value through the accumulator, whether intentional or not.
// It's ambiguous whether a programmer typed "return x" or "x" followed by "return".
// Optimizations add another layer of ambiguity. "if x then return x" gets optimized
// to "if x then return". And the compiler never cared if you returned a value or not.
//
// The goal is to figure out if a return statement should include the expression (node)
// that precedes it. We can't really know, so we have to make a best guess.
// In theory this shouldn't be hard. A function either intentionally returns a value
// or it doesn't. If any return is preceded by a statement without side effects then
// that's a return value, therefore every return statement in that function is preceded
// by a return value, so just absorb each, job done. Ha!
//
// In practice, programmers were sloppy with return values. "return 0" gets tossed around
// all the time in functions that don't return values. conds and switches frequently have
// bodies that end in "0" or "1" or whatever for no reason, or a reason.
// Return statements are missing values, or have values that didn't matter, and of course
// the optimizer muddies things further. If we take all of this literally, the result is a
// lot of ugly decompiled code that's wrapped in return statements. When in doubt, it's
// usually better to do nothing. I think Companion does too much here.
//
// Where do return values come from in decompilation?
//
// 1. AstBuilder creates a Return node from each ret instruction. If acc holds an obvious
//    return value, such as a number or symbol or comparison, the Return node consumes it.
//    This might be a copy node; the "if x then return x". At this point, it's unclear if
//    x really was returned. We can never know. But if AstBuilder doesn't create a node
//    then it will be lost, so it gets created and flagged. Later, ReturnCleaner will
//    delete it if it appears that it wasn't real. AstBuilder leaves us with some Return
//    nodes with values and others without.
//
// 2. ReturnCleaner scans all Return nodes and records if they return a real node,
//    a copy node, or nothing. It also absorbs the previous node into a return if
//    it really looks like a real return value. There are a bunch of dumb heuristics
//    for this, and more for blocking attempts to absorb things that under further
//    inspection look bad. Also, ReturnCleaner is banned from absorbing on popular
//    method names where I know it does way more harm than good, due to sloppy
//    return values that give false impressions. Oh and I've got a list of a few
//    method names that I know always return a value, just to nudge it along.
//    Also SCI lets you return entire statements and conds and ifs. You can even
//    return a loop, but I ban that, because it's dumb.
//
// 3. ReturnCleaner.FinishCleaning() takes the results from the scan and does final
//    processing. If it's convinced that the function intentionally returns a value,
//    it attempts to absorb every return value's previous node. Absorb() has rules
//    to prevent silly looking nodes from ever being returned. Otherwise, if it
//    doesn't think this function intentionally returns a value, it deletes all the
//    copy nodes from return values. The function doesn't return a value, so these
//    were not instructions optimized out by the compiler. Now we know!
//
// 4. Finally, FinishCleaning() deletes the final Return node if it's empty.
//
// *J. Walter Weatherman voice*: And Thaaat's Whyyy... You Always Return A Value.

namespace SCI.Decompile.Ast
{
    class ReturnCleaner : Visitor
    {
        string functionName;
        List<Node> realReturns = new List<Node>();
        List<Node> copyReturns = new List<Node>();
        List<Node> emptyReturns = new List<Node>();

        public bool HasReturnValues() { return realReturns.Any() || copyReturns.Any(); }

        static HashSet<string> knownFunctionsThatReturn = new HashSet<string>()
        {
            "onMe",
            "onTarget",
        };

        static HashSet<string> knownFunctionsToNotAlter = new HashSet<string>()
        {
            "handleEvent",
            "changeState",
            "init",
        };

        public ReturnCleaner(string functionName) { this.functionName = functionName; }

        public override void Visit(Node ret)
        {
            // only interested in returns
            if (ret.Type != NodeType.Return) return;

            // record returns that aren't empty
            if (ret.Children.Any())
            {
                if (!ret.Children[0].Flags.HasFlag(NodeFlags.Copy))
                {
                    realReturns.Add(ret);
                }
                else
                {
                    copyReturns.Add(ret);
                }
                return;
            }

            // if the previous node looks like a return value then absorb it
            int retIndex = ret.Parent.Children.IndexOf(ret);
            if (retIndex > 0)
            {
                Node previous = ret.Parent.Children[retIndex - 1];
                if (LooksLikeReturnValue(previous))
                {
                    if (Absorb(ret, previous))
                    {
                        realReturns.Add(ret);
                        return;
                    }
                }
            }
            emptyReturns.Add(ret);
        }

        [Flags]
        enum ValueFlags
        {
            None = 0,
            NoZero = 1,
        }

        bool LooksLikeReturnValue(Node node, ValueFlags flags = ValueFlags.None)
        {
            if (IsAbsolutelyReturnValue(node))
            {
                return true;
            }

            switch (node.Type)
            {
                case NodeType.List:
                    return (node.Children.Count > 0) && LooksLikeReturnValue(node.Children.Last(), flags);

                case NodeType.Number:
                    if (flags.HasFlag(ValueFlags.NoZero) && (((Number)node).Value == 0))
                    {
                        // so many false-positives due to zeros being thrown around in conds
                        return false;
                    }
                    if (node.Prev?.Type == NodeType.Number)
                    {
                        // if this number is preceded by another number then it's garbage values
                        return false;
                    }
                    // don't make decisions based on numbers that are copy nodes
                    return !node.Flags.HasFlag(NodeFlags.Copy);

                case NodeType.String:
                case NodeType.Said:
                case NodeType.Class:
                case NodeType.Object:
                case NodeType.Self:
                case NodeType.Info:
                case NodeType.Variable:
                case NodeType.Property:
                    return !node.Flags.HasFlag(NodeFlags.Copy);

                case NodeType.If:
                    {
                        var if_ = (If)node;
                        if (LooksLikeReturnValue(if_.Then, flags))
                        {
                            return true;
                        }
                        if (if_.Else != null && LooksLikeReturnValue(if_.Else, flags))
                        {
                            return true;
                        }
                        return false;
                    }

                case NodeType.Cond:
                    {
                        var cond = (Cond)node;
                        if (cond.Cases.Any(c => LooksLikeReturnValue(c.Body, flags | ValueFlags.NoZero)))
                        {
                            return true;
                        }
                        if (cond.Else != null && LooksLikeReturnValue(cond.Else.Body, flags | ValueFlags.NoZero))
                        {
                            return true;
                        }
                        return false;
                    }

                case NodeType.Switch:
                    {
                        var switch_ = (Switch)node;
                        if (switch_.Cases.Any(c => LooksLikeReturnValue(c.Body, flags | ValueFlags.NoZero)))
                        {
                            return true;
                        }
                        if (switch_.Else != null && LooksLikeReturnValue(switch_.Else.Body, flags | ValueFlags.NoZero))
                        {
                            return true;
                        }
                        return false;
                    }

                default:
                    return false;
            }
        }

        bool IsAbsolutelyReturnValue(Node node)
        {
            switch (node.Type)
            {
                // complex variables aren't just laying around by accident
                case NodeType.ComplexVariable:

                // address-of is pointless by itself
                case NodeType.AddressOf:

                // unary ops are pointless by themselves
                case NodeType.Not:
                case NodeType.BinNot:
                case NodeType.Neg:

                // math operations are pointless by themselves
                case NodeType.Add:
                case NodeType.Sub:
                case NodeType.Mul:
                case NodeType.Div:
                case NodeType.Mod:
                case NodeType.Shl:
                case NodeType.Shr:
                case NodeType.Xor:
                case NodeType.BinAnd:
                case NodeType.BinOr:
                case NodeType.And:
                case NodeType.Or:

                // comparisons are pointless by themselves
                case NodeType.Eq:
                case NodeType.Ne:
                case NodeType.Gt:
                case NodeType.Ge:
                case NodeType.Ugt:
                case NodeType.Uge:
                case NodeType.Lt:
                case NodeType.Le:
                case NodeType.Ult:
                case NodeType.Ule:
                    return true;

                default:
                    return false;
            }
        }

        bool Absorb(Node ret, Node value)
        {
            // don't absorb nodes in functions that we shouldn't alter
            // unless the value is *absolutely* a return value.
            // also, don't absorb loops or structures that return values,
            // because that looks silly.
            if ((knownFunctionsToNotAlter.Contains(functionName) &&
                !IsAbsolutelyReturnValue(value)) ||
                value.Type == NodeType.Loop ||
                IsReturnStatement(value))
            {
                return false;
            }

            ret.Parent.Remove(value);
            ret.Add(value);
            return true;
        }

        bool IsReturnStatement(Node node)
        {
            if (node.Type == NodeType.Return)
            {
                return true;
            }
            else if (node.Type == NodeType.List)
            {
                var last = node.Children.LastOrDefault();
                if (last != null)
                {
                    return IsReturnStatement(last);
                }
            }
            else if (node.Type == NodeType.If)
            {
                var if_ = (If)node;
                if (IsReturnStatement(if_.Then))
                {
                    return true;
                }
                if (if_.Else != null && IsReturnStatement(if_.Else))
                {
                    return true;
                }
            }
            else if (node.Type == NodeType.Switch)
            {
                var switch_ = (Switch)node;
                if (switch_.Cases.Any(c => IsReturnStatement(c.Body)))
                {
                    return true;
                }
                if (switch_.Else != null && IsReturnStatement(switch_.Else.Body))
                {
                    return true;
                }
            }
            else if (node.Type == NodeType.Cond)
            {
                var cond = (Cond)node;
                if (cond.Cases.Any(c => IsReturnStatement(c.Body)))
                {
                    return true;
                }
                if (cond.Else != null && IsReturnStatement(cond.Else.Body))
                {
                    return true;
                }
            }
            return false;
        }

        public void FinishCleaning(Node lastNode)
        {
            if (knownFunctionsThatReturn.Contains(functionName) ||
                realReturns.Any())
            {
                // this function definitely returns a function, so absorb
                // the predecessor of all empty returns.
                foreach (var ret in emptyReturns)
                {
                    int retIndex = ret.Parent.Children.IndexOf(ret);
                    if (retIndex > 0)
                    {
                        Node previous = ret.Parent.Children[retIndex - 1];
                        if (Absorb(ret, previous))
                        {
                            realReturns.Add(ret);
                        }
                    }
                }
                emptyReturns.RemoveAll(r => realReturns.Contains(r));

                // transfer copyReturns to realReturns
                foreach (var ret in copyReturns)
                {
                    realReturns.Add(ret);
                }
                copyReturns.Clear();
            }
            else
            {
                // couldn't prove that this function returns anything.
                // delete copy nodes from return statements, i'm assuming
                // they were not in original source.
                foreach (var ret in copyReturns)
                {
                    ret.Remove(0);
                    emptyReturns.Add(ret);
                }
                copyReturns.Clear();
            }

            // remove the last return if empty
            if (lastNode != null &&
                lastNode.Type == NodeType.Return &&
                lastNode.Children.Count == 0)
            {
                lastNode.Parent.Remove(lastNode);
                emptyReturns.Remove(lastNode);
            }
        }
    }
}
