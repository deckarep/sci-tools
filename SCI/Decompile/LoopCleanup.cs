using System.Linq;
using SCI.Resource;

namespace SCI.Decompile.Ast
{
    // Loop Cleanup consists of visitors that are continually applied
    // until no changes are made to the AST.
    //
    // Loop CFA produces a lot of continues and breaks that didn't
    // really exist. It also may not have detected the entirety of
    // the loop test. Also, loops that ended in conds produce a distinct
    // series of "if x then y / continue" that i'd like turned back
    // into if-else-if chains with no continue so that they can
    // later be turned into conds.

    static class LoopCleanup
    {
        public static void Run(Function function, Node ast)
        {
            var loopCleanupVisitors = new LoopCleanupVisitor[]
            {
                new BreakIfCombiner(),
                new LoopTestAbsorber(),
                new BreakContinueFactorOut(),
                new IfThenElseBreakContinueTrim(),
                new ContinueTrimer(),
                new IfContinueRefactor(),
                new FinalBreakContinueTrim(),
                new BreakIfCleaner(),
            };
            foreach (var visitor in loopCleanupVisitors)
            {
                visitor.Function = function;
            }

            do
            {
                foreach (var visitor in loopCleanupVisitors)
                {
                    visitor.Used = false;
                    ast.Accept(visitor);
                }
            } while (loopCleanupVisitors.Any(v => v.Used));
        }
    }

    abstract class LoopCleanupVisitor : Visitor
    {
        public Function Function;
        public bool Used;

        protected static bool IsSingleLevel(Node breakOrContinue, NodeType type)
        {
            return breakOrContinue != null &&
                   breakOrContinue.Type == type &&
                   breakOrContinue.Children.Count == 0; // no Number with level
        }
    }

    // while x           while x and y
    //     if y              ...
    //         ...   =>
    //     else
    //        break
    class LoopTestAbsorber : LoopCleanupVisitor
    {
        public override void Visit(Loop loop)
        {
            while (true)
            {
                if (loop.Test != null &&
                    loop.Body.Children.Count == 1 &&
                    loop.Body.Children[0].Type == NodeType.If)
                {
                    var if_ = (If)loop.Body.Children[0];
                    if (if_.Else != null &&
                        if_.Else.Children.Count == 1 &&
                        IsSingleLevel(if_.Else.Children[0], NodeType.Break))
                    {
                        Used = true;

                        // replace the loop test with (and test if-test)
                        var and = new Node(NodeType.And);
                        var op1 = loop.Test;
                        loop.Replace(loop.Test, and);
                        and.Add(op1);
                        var op2 = if_.Test;
                        and.Add(op2);

                        // replace the If with the Then
                        loop.Body.Remove(if_);
                        foreach (var node in if_.Then.Children)
                        {
                            loop.Body.Add(node);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }

    // if              if
    //    ...              ...
    //    break        else
    // else        =>     ....
    //    ...          break  (same for continue)
    //    break
    class BreakContinueFactorOut : LoopCleanupVisitor
    {
        public override void Visit(If if_)
        {
            if (if_.Else == null) return;

            var lastThen = if_.Then.Children.LastOrDefault();
            var lastElse = if_.Else.Children.LastOrDefault();
            var type = lastThen?.Type;
            if ((type == NodeType.Break || type == NodeType.Continue) &&
                IsSingleLevel(lastThen, type.Value) &&
                IsSingleLevel(lastElse, type.Value))
            {
                Used = true;
                if_.Then.Remove(lastThen);
                if_.Else.Remove(lastElse);
                int nextIndex = if_.Parent.Children.IndexOf(if_) + 1;
                if_.Parent.Insert(nextIndex, lastThen);

                // delete the else if it's empty
                if (if_.Else.Children.Count == 0)
                {
                    if_.Remove(if_.Else);
                }
            }
        }
    }

    // if             if
    //     ...    =>      ...
    //     break      break  (same for continue)
    // break
    //
    // Handles Else too, deletes empty Else, recurses on
    // last expression in Then or Else if it's an If.
    class IfThenElseBreakContinueTrim : LoopCleanupVisitor
    {
        public override void Visit(If if_)
        {
            var next = if_.Next;
            if (IsSingleLevel(next, NodeType.Break) || IsSingleLevel(next, NodeType.Continue))
            {
                Process(if_, next.Type);
            }
        }

        void Process(If if_, NodeType breakOrContinue)
        {
            // remove trailing expressions from Then, unless that would eliminate the last one
            while (if_.Then.Children.LastOrDefault()?.Type == breakOrContinue && if_.Then.Children.Count > 1)
            {
                Used = true;
                if_.Then.Remove(if_.Then.Children.Count - 1);
            }
            // if last expression in Then is an If, process it too
            if (if_.Then.Children.LastOrDefault()?.Type == NodeType.If)
            {
                Process((If)if_.Then.Children.Last(), breakOrContinue);
            }

            if (if_.Else != null)
            {
                // remove trailing expressions from Else, then remove Else if empty
                while (if_.Else.Children.LastOrDefault()?.Type == breakOrContinue)
                {
                    Used = true;
                    if_.Else.Remove(if_.Else.Children.Count - 1);
                }
                if (if_.Else.Children.Count == 0)
                {
                    if_.Remove(if_.Else);
                }
                // if last expression in Then is an If, process it too
                if (if_.Then.Children.LastOrDefault()?.Type == NodeType.If)
                {
                    Process((If)if_.Then.Children.Last(), breakOrContinue);
                }
            }
        }
    }

    // loop
    //     ...
    //     continue   <== delete this
    // end loop
    //
    // loop
    //     ...
    //     if ...
    //         ...
    //         continue  <== delete this
    // end loop
    //
    // Handles Else too, deletes empty Else, recurses on last expression
    // in Then and Else if it's an If.
    class ContinueTrimer : LoopCleanupVisitor
    {
        public override void Visit(Loop loop)
        {
            Recurse(loop.Body);
        }

        void Recurse(Node list)
        {
            while (list.Children.Any())
            {
                var last = list.Children.LastOrDefault();
                if (last == null) return;

                if (IsSingleLevel(last, NodeType.Continue))
                {
                    Used = true;
                    list.Remove(last);
                    continue;
                }

                if (last.Type == NodeType.If)
                {
                    var if_ = (If)last;
                    Recurse(if_.Then);
                    if (if_.Else != null)
                    {
                        Recurse(if_.Else);
                        if (if_.Else.Children.Count == 0)
                        {
                            Used = true; // almost certainly redundant
                            if_.Remove(if_.Else);
                        }
                    }
                }
                break;
            }
        }
    }

    // Turns "if-then ..., continue" sequences into if-then-else,
    // which is pleasant, and lets conds be recovered by CondCreator.
    class IfContinueRefactor : LoopCleanupVisitor
    {
        public override void Visit(Loop loop)
        {
            Process(loop.Body);
        }

        void Process(Node list)
        {
            // start on second to last node and work backwards.
            // keep doing this to create an if else-if else-if [ else ] chain,
            // then it can become a cond.
            for (int ifIndex = list.Children.Count - 2; ifIndex >= 0; ifIndex--)
            {
                // if                      if
                //    ...           =>         ....
                //    continue             else
                // rest of loop                rest of loop
                var if_ = list.Children[ifIndex] as If;
                if (if_ != null &&
                    if_.Else == null &&
                    IsSingleLevel(if_.Then.Children.LastOrDefault(), NodeType.Continue))
                {
                    Used = true;

                    // delete the continue
                    if_.Then.Remove(if_.Then.Children.Count - 1);

                    // create an else and move the rest of the loop body into it
                    if_.Add(new Node(NodeType.List));
                    while (ifIndex != list.Children.Count - 1)
                    {
                        var next = list.Children[ifIndex + 1];
                        list.Remove(ifIndex + 1);
                        if_.Else.Add(next);
                    }

                    // recurse into the Then, give it the same treatment
                    Process(if_.Then);
                }
            }
        }
    }

    // Delete breaks and continues that follow a return or break or continue.
    class FinalBreakContinueTrim : LoopCleanupVisitor
    {
        public override void Visit(Node node)
        {
            switch (node.Type)
            {
                case NodeType.Break:
                case NodeType.Continue:
                case NodeType.Return:
                    {
                        int nextIndex = node.Parent.Children.IndexOf(node) + 1;
                        while (nextIndex < node.Parent.Children.Count &&
                               (node.Parent.Children[nextIndex].Type == NodeType.Break||
                                node.Parent.Children[nextIndex].Type == NodeType.Continue))
                        {
                            Used = true;
                            node.Parent.Remove(nextIndex);
                        }
                    }
                    break;
            }
        }
    }
}
