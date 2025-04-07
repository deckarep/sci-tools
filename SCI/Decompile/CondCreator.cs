// CondCreator detects If-Else-If chains and turns them into Cond nodes.
//
// There are two patterns it's looking for:
// A. If with an Else that's just an If.  Replace this with a new Cond node.
// B. If with an Else that's just a Cond. Replace this with the Cond and absorb the If.

namespace SCI.Decompile.Ast
{
    class CondCreator : Visitor
    {
        public override void Visit(If me)
        {
            var else_ = me.Else;
            if (else_ == null) return;

            // Else is a list if there are multiple statements, but i don't remember
            // if it's a list if there's only one, and i don't want to depend on either.
            // so if it's a list with one node just extract it and use that.
            if (else_.Type == NodeType.List && else_.Children.Count == 1)
            {
                else_ = else_.Children[0];
            }

            // if A              (cond
            // then B               A B
            // else          =>     C D
            //   if C               [ else E ]
            //   then D          )
            //   [ else E ]
            if (else_.Type == NodeType.If)
            {
                var elseIf = (If)else_;

                // birth a Cond
                var cond = new Cond();
                var case1 = new Case();
                case1.Add(me.Test);
                case1.Add(me.Then);
                cond.Add(case1);
                var case2 = new Case();
                case2.Add(elseIf.Test);
                case2.Add(elseIf.Then);
                cond.Add(case2);
                if (elseIf.Else != null)
                {
                    var condElse = new Else();
                    condElse.Add(elseIf.Else);
                    cond.Add(condElse);
                }

                // replace the If node with the new Cond
                me.Parent.Replace(me, cond);
            }
            // if A              (cond
            // then B       =>      A B
            // else cond            rest of cond)
            else if (else_.Type == NodeType.Cond)
            {
                // add our Test and Then as the new first Case
                var cond = (Cond)else_;
                var case_ = new Case();
                case_.Add(me.Test);
                case_.Add(me.Then);
                cond.Insert(0, case_);

                // replace the If node with the Cond
                me.Parent.Replace(me, cond);
            }
        }
    }
}
