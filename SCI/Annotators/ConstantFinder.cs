using System;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // ConstantFinder is an attempt to factor out a common annotator operation:
    // 1. Find integers that are being compared to (or assigned to) things
    // 2. If I know what those integers are, annotate them or replace with define text
    //
    // Verbs are a good example:
    //
    //  (method(doVerb theVerb)
    //	    (switch theVerb
    //          (1 ; Look
    //              ...
    //		    )
    //		    (4 ; Do
    //              ...
    //          )
    //      )
    //
    // Or:
    //
    // (== theVerb 1) ; Look
    //
    //
    // root - node that gets foreach'ed
    // compare - returns true if this node being compared/assigned matches what we're interested in
    // onConstant - do the annotation,etc here

    static class ConstantFinder
    {
        public static void Run(Node root, Func<Node, bool> compare, Action<Integer> onConstant)
        {
            foreach (var node in root)
            {
                // assignment or comparison
                if (node.Children.Count >= 3)
                {
                    switch (node.At(0).Text)
                    {
                        case "=":
                        case "==":
                        case "!=":
                        case ">":
                        case ">=":
                        case "u>":
                        case "u>=":
                        case "<":
                        case "<=":
                        case "u<":
                        case "u<=":
                        case "&":
                        case "&=":
                        case "|":
                        case "|=":
                        case "+":
                        case "+=":
                            // scan for a matching operand
                            foreach (var expression in node.Children.Skip(1))
                            {
                                if (compare(expression))
                                {
                                    // notify about all of the integers
                                    foreach (var child in node.Children)
                                    {
                                        var number = child as Integer;
                                        if (number != null)
                                        {
                                            onConstant(number);
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }

                // switch
                if (node.Children.Count > 2 &&
                    node.At(0).Text == "switch")
                {
                    if (compare(node.At(1)))
                    {
                        // (constant ...)
                        for (int i = 2; i < node.Children.Count; i++)
                        {
                            var constantInteger = node.At(i).At(0) as Integer;
                            if (constantInteger != null) // ignores "else"
                            {
                                onConstant(constantInteger);
                            }
                        }
                    }
                }

                // OneOf
                if (node.Children.Count >= 3 &&
                    node.At(0).Text == "OneOf")
                {
                    if (compare(node.At(1)))
                    {
                        foreach (var parameter in node.Children.Skip(2))
                        {
                            var parameterInteger = parameter as Integer;
                            if (parameterInteger != null)
                            {
                                onConstant(parameterInteger);
                            }
                        }
                    }
                }
            }
        }
    }
}
