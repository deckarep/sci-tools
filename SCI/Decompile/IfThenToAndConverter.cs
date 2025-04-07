namespace SCI.Decompile.Ast
{
    class IfThenToAndConverter : Visitor
    {
        public override void Visit(If me)
        {
            // (if A then (if B then C D...)) => (if (and A B) then C D...)
            if (me.Else == null &&
                me.Then.Children.Count == 1 &&
                me.Then.Children[0].Type == NodeType.If &&
                me.Then.Children[0].Children.Count == 2)
            {
                // get or create the destination And node
                Node and;
                if (me.Test.Type == NodeType.And)
                {
                    and = me.Test;
                }
                else
                {
                    and = new Node(NodeType.And);
                    var test = me.Test;
                    me.Replace(test, and);
                    and.Add(test);
                }

                // add the child's Test
                var childIf = (If)me.Then.Children[0];
                if (childIf.Test.Type == NodeType.And)
                {
                    foreach (var operand in childIf.Test.Children)
                    {
                        and.Add(operand);
                    }
                }
                else
                {
                    and.Add(childIf.Test);
                }

                // use the child's Then
                me.Replace(me.Then, childIf.Then);

                // don't "return", keep going so that the next one can apply
            }

            // (or (if A then B))         => (or (and A B))
            // (or (if (and A B) then C)) => (or (and A B C))
            // (not ...
            // (cond ... ((if A then B) ...) => cond ... ((and A B) ...)
            // switch case too, i guess!
            // function parameter! (f (if A then B)) => (f (and A B))
            // return, breakif, continueif, loop test...
            if ((me.Parent.Type == NodeType.Or ||
                 me.Parent.Type == NodeType.Not ||
                 me.Parent.Type == NodeType.Case ||
                 me.Parent.Type == NodeType.SendMessage ||
                 me.Parent.Type == NodeType.KernelCall ||
                 me.Parent.Type == NodeType.PublicCall ||
                 me.Parent.Type == NodeType.LocalCall ||
                 me.Parent.Type == NodeType.Return ||
                 me.Parent.Type == NodeType.BreakIf ||
                 me.Parent.Type == NodeType.ContinueIf ||
                 (me.Parent.Type == NodeType.Loop &&
                 (me.Parent.Children.IndexOf(me) == 1 /*loop test*/))) &&
                me.Else == null &&
                me.Then.Children.Count == 1)
            {
                Node and;
                if (me.Test.Type == NodeType.And)
                {
                    and = me.Test;
                }
                else
                {
                    and = new Node(NodeType.And);
                    and.Add(me.Test);
                }
                if (me.Then.Children[0].Type == NodeType.And)
                {
                    foreach (var operand in me.Then.Children[0].Children)
                    {
                        and.Add(operand);
                    }
                }
                else
                {
                    and.Add(me.Then.Children[0]);
                }
                me.Parent.Replace(me, and);
                return; // have to return now, "me" is no good
            }

            // (if A then (== B C)) => (and A (== B C))
            if (me.Else == null &&
                me.Then.Children.Count == 1 &&
                IsComparison(me.Then.Children[0].Type))
            {
                var and = new Node(NodeType.And);
                and.Add(me.Test);
                and.Add(me.Then.Children[0]);
                me.Parent.Replace(me, and);
            }
        }

        bool IsComparison(NodeType type)
        {
            return NodeType.Eq <= type && type <= NodeType.Ule;
        }
    }
}
