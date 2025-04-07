using System.Collections.Generic;

namespace SCI.Decompile.Ast
{
    // (= a (+ a b)) => (+= a b)
    // (= a (+ a b c ...)) => (+= a (+ b c ...))
    // Companion doesn't accept more than two operands
    class MathAssignmentCreator : Visitor
    {
        public static IReadOnlyDictionary<NodeType, NodeType> Map = new Dictionary<NodeType, NodeType>
        {
            { NodeType.Add,    NodeType.AssignmentAdd },
            { NodeType.Sub,    NodeType.AssignmentSub },
            { NodeType.Mul,    NodeType.AssignmentMul },
            { NodeType.Div,    NodeType.AssignmentDiv },
            { NodeType.Shl,    NodeType.AssignmentShl },
            { NodeType.Shr,    NodeType.AssignmentShr },
            { NodeType.Xor,    NodeType.AssignmentXor },
            { NodeType.BinAnd, NodeType.AssignmentBinAnd },
            { NodeType.BinOr,  NodeType.AssignmentBinOr },
        };

        public override void Visit(Assignment node)
        {
            Node source = node.Source;
            if (Map.ContainsKey(source.Type) &&
                node.Dest.Same(source.Children[0]))
            {
                var newNode = new Node(Map[source.Type]);
                newNode.Add(node.Dest);
                if (source.Children.Count == 2)
                {
                    newNode.Add(source.Children[1]);
                }
                else
                {
                    source.Remove(source.Children[0]);
                    newNode.Add(source);
                }
                node.Parent.Replace(node, newNode);
            }
        }
    }
}

