using System;
using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators.Visitors
{
    // DeepBeige packages up the code traversing and state evaluation that I
    // wrote for SayAnnotator.
    //
    // The dream was for this to become something that tracks a ton of state for
    // annotators to subscribe to. Years later, I see that we kinda ran out of
    // complex things to annotate, and most don't need this so... job done!

    class DeepBeige
    {
        Dictionary<string, Stack<int>> constants = new Dictionary<string, Stack<int>>();

        public Function Function { get; private set; }

        public delegate void NodeHandler(Node node);
        public NodeHandler OnNode;

        public void Visit(Game game)
        {
            foreach (var script in game.Scripts)
            {
                Visit(script);
            }
        }

        public void Visit(Script script)
        {
            foreach (var procedure in script.Procedures)
            {
                Visit(procedure);
            }

            foreach (var obj in script.Objects)
            {
                foreach (var method in obj.Methods)
                {
                    Visit(method);
                }
            }
        }

        public void Visit(Function function)
        {
            // so that client can query
            Function = function;

            foreach (var node in function.Code)
            {
                Visit(node);

                // sanity check
                if (constants.Any(c => c.Value.Any())) throw new Exception("constants not empty");
            }
        }

        void Visit(Node node)
        {
            // send an event to the client. node on!
            OnNode?.Invoke(node);

            // (if (== symbol integer) ... [else ...])
            //
            if (node.At(0).Text == "if" &&
                node.At(1).At(0).Text == "==" &&
                node.At(1).At(1) is Atom &&
                node.At(1).At(2) is Integer)
            {
                // update state with the known value of this symbol
                string symbol = node.At(1).At(1).Text;
                int symbolValue = node.At(1).At(2).Number;
                Push(symbol, symbolValue);

                // recurse on all nodes within the if statement.
                // stop when else is reached or there are no more.
                int i = 1;
                for (; i < node.Children.Count; ++i)
                {
                    if (node.Children[i].Text == "else")
                    {
                        i += 1;
                        break;
                    }
                    Visit(node.Children[i]);
                }

                // pop symbol off state since we've left scope
                Pop(symbol);

                // continue recursing on nodes within else if it exists
                for (; i < node.Children.Count; ++i)
                {
                    Visit(node.Children[i]);
                }

                return;
            }

            // (switch symbol (integer [node...]) (integer [node...]) [(else [node ...])
            //
            if (node.At(0).Text == "switch" &&
                node.At(1) is Atom)
            {
                string symbol = node.At(1).Text;

                // process all cases
                for (int i = 2; i < node.Children.Count; ++i)
                {
                    var caseList = node.Children[i];

                    // update state with known value of this symbol.
                    // this won't be an integer if it's an "else".
                    // possibly other situations too, i don't know.
                    bool isIntegerCase = caseList.At(0) is Integer;
                    if (isIntegerCase)
                    {
                        int symbolValue = caseList.At(0).Number;
                        Push(symbol, symbolValue);
                    }

                    // visit case children
                    foreach (var caseChild in caseList.Skip(1))
                    {
                        Visit(caseChild);
                    }

                    // pop case symbol now that we're out of scope
                    if (isIntegerCase)
                    {
                        Pop(symbol);
                    }
                }
                return;
            }

            // (cond ((== symbol integer) [node...]) [(== symbol integer) [node...]) (else [node...]))
            if (node.At(0).Text == "cond")
            {
                foreach (var condList in node.Children.Skip(1))
                {
                    // visit the condition; it could be a Print statement. (SQ5 rm730)
                    Visit(condList.At(0));

                    string symbol = null;
                    if (condList.At(0).At(0).Text == "==" &&
                        condList.At(0).At(1) is Atom &&
                        condList.At(0).At(2) is Integer)
                    {
                        symbol = condList.At(0).At(1).Text;
                        int symbolValue = condList.At(0).At(2).Number;
                        Push(symbol, symbolValue);
                    }

                    // visit this condition's children
                    foreach (var condListChild in condList.Children.Skip(1))
                    {
                        Visit(condListChild);
                    }

                    // pop cond symbol now that we're out of scope
                    if (symbol != null)
                    {
                        Pop(symbol);
                    }
                }
                return;
            }

            // recurse
            foreach (var child in node.Children)
            {
                Visit(child);
            }
        }

        void Push(string symbol, int number)
        {
            Stack<int> stack;
            if (!constants.TryGetValue(symbol, out stack))
            {
                stack = new Stack<int>();
                constants.Add(symbol, stack);
            }
            stack.Push(number);
        }

        int Pop(string symbol)
        {
            return constants[symbol].Pop();
        }

        public int Resolve(string symbol)
        {
            Stack<int> stack;
            if (constants.TryGetValue(symbol, out stack))
            {
                if (stack.Any())
                {
                    return stack.Peek();
                }
            }
            return -1;
        }
    }
}
