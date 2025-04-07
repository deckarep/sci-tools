using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // Polygon:init gets called with a long string of coordinates that get formatted
    // into taking up many lines in room scripts. It's a lot of scrolling for info
    // that nobody cares about. And when you do care, one x or y value per line isn't
    // a good way to look at it.
    //
    // ((Polygon new:)        ((Polygon new:)
    //   init:                  init: 1 2 ...
    //     1             =>
    //     2
    //     ...
    //
    // and also any createPoly: ... calls.
    //
    // This is the first annotator I've written that manipulates trivia.

    static class PolygonFormatter
    {
        public static void Run(Game game)
        {
            var scriptPolygons = new HashSet<string>();
            foreach (var script in game.Scripts)
            {
                // reset
                scriptPolygons.Clear();
                foreach (var obj in script.Objects.Where(o => o.Super == "Polygon" ||
                                                              o.Name == "Polygon"))
                {
                    var type = obj.Properties.FirstOrDefault(p => p.Name == "type");
                    if (type != null && type.ValueNode is Integer)
                    {
                        // instance p of Polygon
                        //   type 2
                        KernelCallAnnotator.MakeSymbol(type.ValueNode, PolyTypes);
                    }

                    scriptPolygons.Add(obj.Name);

                    // polyon methods (most likely init)
                    foreach (var method in obj.Methods)
                    {
                        foreach (var node in method.Node)
                        {
                            if (node.At(0).Text == "self")
                            {
                                // (self ...)
                                Process(node);
                            }
                            else if (node.At(0).Text == "super")
                            {
                                // (super ...)
                                Process(node);
                            }
                        }
                    }
                }

                foreach (var function in script.GetFunctions())
                {
                    foreach (var node in function.Node)
                    {
                        if (node.At(0).Text == "Polygon" &&
                            node.Children.Last().Text == "new:")
                        {
                            // (Polygon new:)
                            if (node.Parent.At(0) == node)
                            {
                                Process(node.Parent);
                            }
                            // (= temp (Polygon new:))
                            else if (node.Parent.At(0).Text == "=")
                            {
                                Process(node.Parent.Parent);
                            }
                        }
                        else if (scriptPolygons.Contains(node.At(0).Text))
                        {
                            // (scriptPolygon ...)
                            Process(node);
                        }
                        else if (node.Text == "createPoly:" ||
                                 node.Text == "makePolygon:") // sq4
                        {
                            Reformat(node);
                        }
                    }
                }
            }
        }

        static void Process(Node node)
        {
            // (polygon ... init: ...)
            var init = node.Children.FirstOrDefault(n => n.Text == "init:");
            if (init != null)
            {
                Reformat(init);
            }

            var type = node.Children.FirstOrDefault(n => n.Text == "type:");
            if (type != null)
            {
                KernelCallAnnotator.MakeSymbol(type.Next(), PolyTypes);
            }
        }

        // selector = "init:" or "createPoly:"
        static void Reformat(Node selector)
        {
            var parameters = selector.GetSelectorParameters().ToList();
            if (!parameters.Any()) return;
            if (!parameters.All(p => p is Integer)) return;

            // init followed by no trivia
            selector.RightTrivia.Clear();

            // all parameters are now prefaced with one space
            var space = new Trivia(TriviaType.Whitespace, " ");
            foreach (var parameter in parameters)
            {
                parameter.LeftTrivia.Clear();
                parameter.LeftTrivia.Add(space);
                if (parameter != parameters.Last())
                {
                    parameter.RightTrivia.Clear();
                }
            }
        }

        static Dictionary<int, string> PolyTypes = new Dictionary<int, string>
        {
            {0, "PTotalAccess"},
            {1, "PNearestAccess"},
            {2, "PBarredAccess"},
            {3, "PContainedAccess"},
        };
    }
}
