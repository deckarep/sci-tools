using System.Collections.Generic;
using System.Linq;
using SCI.Language;

// This experiment got out of hand and is now an annotator. Oops.
//
// I wanted to play around with a Visitor pattern against the parser tree,
// and a practical use was to try making a good uninitialized variable hunter,
// so that's what this is now.
//
// This should probably just be an inspiration for another approach and not used
// for anything else, it was just for fun and there's not much to it.
//
// An event-based subscription, instead of subclassing, might be better.
// I'd like a code visitor where I can subscribe to assignment events, function
// calls, operators, and also have access to known state, like what symbols are
// known to be what values based on earlier comparisons, so that i can factor
// all that code out of SayAnnotator and use it everywhere.

namespace SCI.Annotators.Visitors
{
    abstract class Visitor
    {
        public void Visit(IEnumerable<Node> nodes)
        {
            foreach (var node in nodes)
            {
                Visit(node);
            }
        }

        public void Visit(Node node)
        {
            if (!PreorderVisit(node))
            {
                return;
            }

            foreach (var child in node.Children)
            {
                Visit(child);
            }

            PostorderVisit(node);
        }

        bool PreorderVisit(Node node)
        {
            switch (node.At(0).Text)
            {
                case "asm":
                    return VisitAsm(node);
                case "if":
                    {
                        int elseStatementIndex = node.Children.IndexWhere(n => n.Text == "else");
                        IEnumerable<Node> ifExpressionNodes;
                        Node elseNode;
                        IEnumerable<Node> elseExpressionNodes;
                        if (elseStatementIndex == -1)
                        {
                            ifExpressionNodes = node.Children.Skip(1);
                            elseNode = Node.Nil;
                            elseExpressionNodes = new Node[0];
                        }
                        else
                        {
                            ifExpressionNodes = node.Children.Skip(1).Take(elseStatementIndex - 1);
                            elseNode = node.Children.Skip(elseStatementIndex).First();
                            elseExpressionNodes = node.Children.Skip(elseStatementIndex + 1);
                        }
                        return VisitIf(node, node.At(1), ifExpressionNodes, elseNode, elseExpressionNodes);
                    }
               case "switch":
                    {
                        return VisitSwitch(node, node.At(1), node.Children.Skip(2));
                    }
                case "cond":
                    {
                        return VisitCond(node, node.Children.Skip(1));
                    }
                case "while":
                    {
                        return VisitWhile(node, node.At(1), node.Children.Skip(2));
                    }
                case "for":
                    {
                        return VisitFor(node, node.At(1), node.At(2), node.At(3), node.Children.Skip(4));
                    }
                case "and":
                    return VisitAnd(node.Children.Skip(1));
                case "or":
                    return VisitOr(node.Children.Skip(1));
                case "=":
                    if (node.Children.Count != 3) throw new System.Exception("Assignment node has " + node.Children.Count + " children");
                    return VisitAssign(node.Children[1], node.Children[2]);
            }
            return true; // continue
        }

        protected virtual bool VisitAsm(Node node) { return true; }
        protected virtual bool VisitIf(Node node, Node condNode, IEnumerable<Node> expressionNodes, Node elseNode, IEnumerable<Node> elseExpressionNodes) { return true; }
        protected virtual bool VisitSwitch(Node node, Node condNode, IEnumerable<Node> expressionsNodes) { return true; }
        protected virtual bool VisitCond(Node node, IEnumerable<Node> expressionsNodes) { return true; }
        protected virtual bool VisitWhile(Node node, Node condNode, IEnumerable<Node> expressionsNodes) { return true; }
        protected virtual bool VisitFor(Node node, Node init, Node test, Node reinit, IEnumerable<Node> body) { return true; }
        protected virtual bool VisitAnd(IEnumerable<Node> condNodes) { return true; }
        protected virtual bool VisitOr(IEnumerable<Node> condNodes) { return true; }
        protected virtual bool VisitAssign(Node target, Node value) { return true; }
        protected virtual void PostorderVisit(Node node) { }
    }
}