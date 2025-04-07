using System;
using System.Linq;

// When a For loop has a ContinueIf directly in its body then that
// will get decompiled as an Or, where the last condition is the
// rest of the function body. Detect that here.
//
// This is dumb, and it only affects one function in one INN game.
// A real For-Continue CFA algorithm would handle this.
// (See diatribe in Workarounds.cs)

namespace SCI.Decompile.Ast
{
    class ForContIfFinder : Visitor
    {
        public override void Visit(Loop loop)
        {
            if (loop.ForInit == null) return;

            while (loop.Body.Children.LastOrDefault()?.Type == NodeType.Or)
            {
                var or = loop.Body.Children.Last();
                if (or.Children.Count > 2) throw new Exception("ForContIfFinder: unexpected children");
                loop.Body.Remove(or);

                var contifTest = or.Children[0];
                or.Remove(contifTest);
                var contif = new Node(NodeType.ContinueIf, contifTest);
                loop.Body.Add(contif);

                if (or.Children.Count == 1)
                {
                    var restOfLoopBody = or.Children[0];
                    or.Remove(restOfLoopBody);
                    if (restOfLoopBody.Type == NodeType.List)
                    {
                        while (restOfLoopBody.Children.Any())
                        {
                            var node = restOfLoopBody.Children[0];
                            restOfLoopBody.Remove(node);
                            loop.Body.Add(node);
                        }
                    }
                    else
                    {
                        loop.Body.Add(restOfLoopBody);
                    }
                }
            }
        }
    }
}
