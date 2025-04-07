using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators.Visitors
{
    class UninitializedTempVisitor : Visitor
    {
        class Context
        {
            readonly HashSet<int> inits;  // variables that have been initialized
            readonly HashSet<int> maybes; // variables that may have been initialized

            public Context()
            {
                inits = new HashSet<int>();
                maybes = new HashSet<int>();
            }

            public Context(Context context)
            {
                inits = new HashSet<int>(context.inits);
                maybes = new HashSet<int>(context.maybes);
            }

            public void AddInit(int variable)
            {
                inits.Add(variable);
                maybes.Remove(variable);
            }

            public void AddMaybe(int variable)
            {
                if (!inits.Contains(variable))
                {
                    maybes.Add(variable);
                }
            }

            public bool IsInitialized(int variable)
            {
                return inits.Contains(variable);
            }

            public bool IsMaybeInitialized(int variable)
            {
                return maybes.Contains(variable);
            }

            public void Merge(IReadOnlyList<Context> contexts)
            {
                if (!contexts.Any()) return;

                // any variable initialized in all branches is an initialized variable
                var firstContext = contexts.First();
                foreach (var variable in firstContext.inits)
                {
                    if (contexts.All(c => c.inits.Contains(variable)))
                    {
                        AddInit(variable);
                    }
                }

                // anything left in any context is a maybe.
                // if only one context absolutely initialized a variable, that's a maybe
                // if only one context occasionally initialized a variable, that's a maybe
                // if all contexts occasionally initialized a variable, that's a maybe
                foreach (var context in contexts)
                {
                    foreach (var variable in context.inits)
                    {
                        // does nothing if we're already certain about this one
                        AddMaybe(variable);
                    }
                    foreach (var maybe in context.maybes)
                    {
                        // does nothing if we're already certain about this one
                        AddMaybe(maybe);
                    }
                }
            }

            public IEnumerable<int> Inits { get { return inits; } }
            public IEnumerable<int> Maybes { get { return maybes; } }
        }

        enum PathValue
        {
            Unknown,
            True,
            False
        }

        Function function;
        bool annotateMaybes;
        HashSet<string> tempNames;
        Context context;
        PathValue pathValue;

        // represents a branch in an if / cond / switch statement.
        // in a switch the condition is just an integer node or an else
        class Branch
        {
            public Node Condition { get; private set; }
            public IEnumerable<Node> Nodes { get; private set; }
            public bool IsElse { get { return Condition.Text == "else"; } }

            public bool IsDead
            {
                get
                {
                    return Nodes.Any(n => n.Text == "return" || n.At(0).Text == "return");
                }
            }

            public Branch(Node condition, IEnumerable<Node> nodes)
            {
                Condition = condition;
                Nodes = nodes;
            }
        }

        public UninitializedTempVisitor(Function function, bool annotateMaybes)
        {
            this.function = function;
            this.annotateMaybes = annotateMaybes;

            tempNames = new HashSet<string>(
                function.Temps.Select(t => t.Name)
            );
            context = new Context();
            pathValue = PathValue.Unknown;
        }

        // skip asm blocks
        protected override bool VisitAsm(Node node)
        {
            return false;
        }

        protected override bool VisitIf(Node node, Node condNode, IEnumerable<Node> expressionNodes, Node elseNode, IEnumerable<Node> elseExpressionNodes)
        {
            var branches = new List<Branch>();
            var trueBranch = new Branch(condNode, expressionNodes);
            branches.Add(trueBranch);
            if (elseExpressionNodes.Any())
            {
                var falseBranch = new Branch(elseNode, elseExpressionNodes);
                branches.Add(falseBranch);
            }
            VisitBranches(branches);
            return false; // i got it
        }

        protected override bool VisitSwitch(Node node, Node condNode, IEnumerable<Node> expressionsNodes)
        {
            // visit the conditional node like normal.
            // i am being superstitious when i force the path value to Unknown.
            var originalPathValue = pathValue;
            pathValue = PathValue.Unknown;
            Visit(condNode);
            pathValue = originalPathValue;

            // VisitBranches() handles the complexity of if/cond branches where there's conditionals with
            // and/ors which can also assign variables. Switch is simpler because all of its conditions
            // are just constants which means they're irrelevant. So I send all the switch branches to
            // VisitBranches() and let it do its complexity on condition nodes that just happen to be
            // Integers where nothing interesting will happen. VisitBranches() still handles fun things
            // like dead branches and optional else branches.
            var branches = new List<Branch>();
            foreach (var expressionNode in expressionsNodes)
            {
                if (expressionNode.Children.Any()) // probably unnecessary check
                {
                    var branch = new Branch(expressionNode.Children[0], expressionNode.Children.Skip(1));
                    branches.Add(branch);
                }
            }
            VisitBranches(branches);
            return false; // i got it
        }

        // (cond ( condition expression1 expression2 ... ) ( condition expression1 expression 2...) [ ( else expression 1...) ]
        protected override bool VisitCond(Node node, IEnumerable<Node> expressionsNodes)
        {
            var branches = new List<Branch>();
            foreach (var expressionNode in expressionsNodes)
            {
                if (expressionNode.Children.Any()) // probably unnecessary check
                {
                    var branch = new Branch(expressionNode.Children[0], expressionNode.Children.Skip(1));
                    branches.Add(branch);
                }
            }
            VisitBranches(branches);
            return false; // i got it
        }

        protected override bool VisitWhile(Node node, Node condNode, IEnumerable<Node> expressionsNodes)
        {
            // treat while loops as an if statement without an else
            var branch = new Branch(condNode, expressionsNodes);
            var branches = new List<Branch> { branch };
            VisitBranches(branches);

            return false; // i got it
        }

        // added years later now that i have a decompiler that emits for loops.
        // i'm just guessing that this is how you use this, it's been too long.
        protected override bool VisitFor(Node node, Node init, Node test, Node reinit, IEnumerable<Node> body)
        {
            Visit(init);

            var branch = new Branch(test, body.Append(reinit));
            var branches = new List<Branch> { branch };
            VisitBranches(branches);

            return false;
        }

        void VisitBranches(List<Branch> branches)
        {
            var originalContext = new Context(context);
            var branchContexts = new List<Context>();
            for (int i = 0; i < branches.Count; ++i)
            {
                // restore context to the root
                context = new Context(originalContext);

                // all conditions of all previous branches get visited
                // and evaluate to false.
                if (i > 0)
                {
                    pathValue = PathValue.False;
                    for (int j = 0; j < i; ++j)
                    {
                        Visit(branches[j].Condition);
                    }
                }

                // the current branch's condition evaluates to true
                pathValue = PathValue.True;
                Visit(branches[i].Condition);

                // visit the branch
                pathValue = PathValue.Unknown;
                var branch = branches[i];
                Visit(branch.Nodes);

                // dead branches should be annotated but their results
                // are to be ignored.
                if (!branch.IsDead)
                {
                    branchContexts.Add(context);
                }
            }

            // if there's no else statement then there is one more path where
            // every conditional evaluates to false, so create that. this avoids
            // any custom else handling in Merge() because all paths are provided.
            if (!branches.Any(b => b.IsElse))
            {
                // restore context to the root
                context = new Context(originalContext);

                // all conditions evaluate to false
                pathValue = PathValue.False;
                foreach (var branch in branches)
                {
                    Visit(branch.Condition);
                }

                // add the pseudo-branch
                branchContexts.Add(context);
            }


            // merge the contexts together
            originalContext.Merge(branchContexts);
            context = originalContext;
        }

        protected override bool VisitAnd(IEnumerable<Node> condNodes)
        {
            bool alwaysVisitAllNodes = (pathValue == PathValue.True);
            VisitLogicGate(condNodes, alwaysVisitAllNodes);
            return false; // i got it
        }

        protected override bool VisitOr(IEnumerable<Node> condNodes)
        {
            bool alwaysVisitAllNodes = (pathValue == PathValue.False);
            VisitLogicGate(condNodes, alwaysVisitAllNodes);
            return false; // i got it
        }

        void VisitLogicGate(IEnumerable<Node> nodes, bool alwaysVisitAllNodes)
        {
            // always visit the first node
            var firstNode = nodes.FirstOrDefault();
            if (firstNode == null) return;
            Visit(firstNode);

            if (alwaysVisitAllNodes)
            {
                foreach (var additionalNode in nodes.Skip(1))
                {
                    Visit(additionalNode);
                }
            }
            else
            {
                // backup context
                var originalContext = new Context(context);

                // visit all additional nodes
                foreach (var additionalNode in nodes.Skip(1))
                {
                    Visit(additionalNode);
                }

                // everything that was found is a maybe
                foreach (int init in context.Inits)
                {
                    originalContext.AddMaybe(init);
                }
                foreach (int maybe in context.Maybes)
                {
                    originalContext.AddMaybe(maybe);
                }
                context = originalContext;
            }
        }

        protected override bool VisitAssign(Node target, Node value)
        {
            // visit the value first. this is a special case where a later
            // node is visited first. if the value node contains an assignment
            // that gets referenced as an array index in the target node then
            // that's okay, but a naive traversal would see that as uninitialized.
            Visit(value);

            if (target is Array && tempNames.Contains(target.At(0).Text))
            {
                // assign with an array index
                var arrayIndexNode = target.At(1);
                if (arrayIndexNode is Integer)
                {
                    // array index is an integer constant
                    MarkAsInitialized(target.At(0).Text, arrayIndexNode.Number, false);
                }
                else
                {
                    // array index is dynamic, assume everything gets initialized.
                    // but first we have to visit it, in case it's complex!
                    Visit(arrayIndexNode);

                    MarkAsInitialized(target.At(0).Text, 0, true);
                }
            }
            else if (tempNames.Contains(target.Text))
            {
                // assign a variable
                MarkAsInitialized(target.Text, 0, false);
            }
            else
            {
                // visit the target, i guess?
                // i don't think there's any syntax that can make
                // this meaningful, it should just be a symbol, but whatever.
                Visit(target);
            }

            return false; // i got it
        }

        protected override void PostorderVisit(Node node)
        {
            // @variable
            if (node.Parent is AddressOf)
            {
                if (tempNames.Contains(node.Text))
                {
                    MarkAsInitialized(node.Text, 0, true);
                }
                return;
            }

            // @[variable XX]
            if (node.Parent.Parent is AddressOf &&
                node.Parent is Array &&
                node.Parent.At(0) == node &&
                tempNames.Contains(node.Parent.At(0).Text))
            {
                // assign with an array index
                var arrayIndexNode = node.Parent.At(1);
                if (arrayIndexNode is Integer)
                {
                    // array index is an integer constant
                    MarkAsInitialized(node.Text, arrayIndexNode.Number, true);
                }
                else
                {
                    // array index is dynamic
                    MarkAsInitialized(node.Text, 0, true);
                }
                return;
            }

            if (node is Atom && tempNames.Contains(node.Text))
            {
                // parameter symbol. ignore if this is target of an assignment.
                //  (= node ...
                //  (= [node
                //  @[node
                if ((node.Parent.At(0).Text == "=" && node.Parent.At(1) == node) ||
                    (node.Parent is Array && node.Parent.At(0) == node &&
                     ((node.Parent.Parent.At(0).Text == "=" && node.Parent.Parent.At(1) == node.Parent) ||
                      (node.Parent.Parent is AddressOf))))
                {
                    return;
                }

                if (node.Parent is Array)
                {
                    var indexNode = node.Next();
                    if (indexNode is Integer)
                    {
                        AnnotateIfUninitialized(node, node.Text, indexNode.Number);
                    }
                    else
                    {
                        // array index unknown, do nothing
                    }
                }
                else
                {
                    AnnotateIfUninitialized(node, node.Text, 0);
                }
            }
        }

        void MarkAsInitialized(string variableName, int offset, bool markTillEnd)
        {
            Variable temp = function.Temps.First(t => t.Name == variableName);

            if (!markTillEnd)
            {
                context.AddInit(temp.Number + offset);
            }
            else
            {
                // variable index is dynamic so i give up, and am assuming positive.
                // marking all variables from start of name to last variable.
                var lastVariable = function.Temps.Last();
                int totalVariableCount = lastVariable.Number + lastVariable.Count;
                int startVariableNumber = temp.Number + offset;
                for (int i = startVariableNumber; i < totalVariableCount; ++i)
                {
                    context.AddInit(i);
                }
            }
        }

        void AnnotateIfUninitialized(Node node, string variableName, int offset)
        {
            Variable temp = function.Temps.First(t => t.Name == variableName);

            int tempNumber = temp.Number + offset;
            if (!context.IsInitialized(tempNumber))
            {
                if (context.IsMaybeInitialized(tempNumber))
                {
                    if (annotateMaybes)
                    {
                        node.Annotate("MAYBE UNINIT");
                    }
                }
                else
                {
                    node.Annotate("UNINIT");
                }
            }
        }
    }
}
