namespace SCI.Decompile.Ast
{
    // (op A B (op C D) E) => (op A B C D E)
    class NaryReducer : Visitor
    {
        public override void Visit(Node me)
        {
            switch (me.Type)
            {
                case NodeType.And:
                case NodeType.Or:
                case NodeType.Add:
                //case NodeType.Sub: // not n-ary, not accepted by companion
                case NodeType.Mul:
                    break;
                default:
                    return;
            }

            // operands whose type matches parent get replaced with their operands
            for (int i = 0; i < me.Children.Count; i++)
            {
                var op = me.Children[i];
                if (op.Type != me.Type) continue;

                // remove the operand
                me.Remove(i);

                // add its operands
                for (int j = 0; j < op.Children.Count; j++)
                {
                    me.Insert(i + j, op.Children[j]);
                }
            }
        }
    }
}
