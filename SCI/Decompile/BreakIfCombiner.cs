namespace SCI.Decompile.Ast
{
    class BreakIfCombiner : LoopCleanupVisitor
    {
        // (breakif x)    =>    (breakif (or x y))
        // (breakif y)
        //
        // Same for continueif
        public override void Visit(Node node)
        {
            switch (node.Type)
            {
                case NodeType.BreakIf:
                case NodeType.ContinueIf:
                    break;
                default:
                    return;
            }

            while (true)
            {
                int nextIndex = node.Parent.Children.IndexOf(node) + 1;
                if (nextIndex == node.Parent.Children.Count) break;
                Node next = node.Parent.Children[nextIndex];
                if (node.Type != next.Type) break;

                // we are a break/continue-if and so is the next node.
                // create an Or that contains both conditions as operands,
                // make that the new condition, and delete the next node.
                // don't worry about or's within or's, IfThenToAndConverter
                // flattens all of that out.
                Node or = new Node(NodeType.Or);
                Node op1 = node.Children[0];
                node.Remove(op1);
                node.Add(or);
                or.Add(op1);
                Node op2 = next.Children[0];
                or.Add(op2);
                node.Parent.Remove(next);

                Used = true;
            }
        }
    }
}
