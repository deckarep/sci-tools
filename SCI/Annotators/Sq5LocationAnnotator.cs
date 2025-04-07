using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // global 113 (holds eureka current location)
    // eureka:destination
    // eureka:curLocation

    static class Sq5LocationAnnotator
    {
        // these are internal strings from script 201
        static Dictionary<int, string> Locations = new Dictionary<int, string>
        {
            { 0,  "Nowhere" },
            { 1,  "garbage1" },
            { 2,  "garbage2" },
            { 3,  "ku" },
            { 4,  "spacebar" },
            { 5,  "clorox2" },
            { 6,  "thrakus" },
            { 7,  "genetix Space Lab" },
            { 8,  "genetix environdome" },
            { 9,  "generic planet 1" },
            { 10, "genceric planet 2" },
            { 11, "generic planet 3" },
            { 12, "generic planet 4" },
            { 13, "generic planet 5" },
            { 14, "goliath" },
            { 15, "empty space" },
            { 16, "empty space" }, // 16 also gets used, so dupe it
            { 17, "empty space" }, // 17 too!
        };

        public static void Run(Game game)
        {
            // current location
            string globalName = game.GetGlobal(113).Name;

            // handle the basics
            foreach (var function in game.GetFunctions())
            {
                ConstantFinder.Run(function.Node,
                    // comparison
                    n => n.Text == globalName ||
                         (n.At(0).Text == "eureka" &&
                         ((n.Children.Last().Text == "destination:") ||
                          (n.Children.Last().Text == "curLocation:"))),
                    // onConstant
                    n => Annotate(n)
                );
            }

            // selector assignment
            foreach (var node in game.GetFunctions().SelectMany(f => f.Node))
            {
                if ((node.Text == "destination:" || node.Text == "curLocation:") &&
                    node.Parent.At(0).Text == "eureka" &&
                    node.Next() is Integer)
                {
                    Annotate(node.Next());
                }
            }
        }

        static void Annotate(Node node)
        {
            string location;
            if (Locations.TryGetValue(node.Number, out location))
            {
                node.Annotate(location);
            }
        }
    }
}
