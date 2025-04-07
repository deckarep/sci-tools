using System.Linq;

namespace SCI.Decompile.Ast
{
    class BreakIfCleaner : LoopCleanupVisitor
    {
        // (breakif            (if A
        //   (if A        =>       (breakif B)
        //       B
        //   else               else
        //       (break)           (break)
        //
        // Same for continueif.
        //
        // pq4 script 33, pPad:doit.
        // In pPad:doit, this allows several other
        // loop cleaners to improve further.
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

            NodeType breakOrContinue = (node.Type == NodeType.BreakIf) ?
                                       NodeType.Break :
                                       NodeType.Continue;

            if (node.Children.Count == 1 &&
                node.Children[0].Type == NodeType.If)
            {
                var if_ = (If)node.Children[0];
                if (if_.Then.Children.Any() &&
                    if_.Else != null &&
                    if_.Else.Children.Last().Type == breakOrContinue)
                {
                    // wrap the last node in Then in a Break/ContinueIf
                    var lastThenNode = if_.Then.Children.Last();
                    if_.Then.Remove(lastThenNode);
                    if_.Then.Add(new Node(node.Type, lastThenNode));

                    // replace current node with if
                    node.Parent.Replace(node, if_);

                    Used = true;
                }
            }
        }
    }
}
