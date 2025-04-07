using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // QFG3 and QFG4 are identical on the gait thing so just do it here
    static class Qfg34GaitAnnotator
    {
        public static void Run(Game game)
        {
            // annotate global usage
            GlobalEnumAnnotator.Run(game, 100, gaits);

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (node.Parent.Children.Count >= 3 &&
                    node.Text == "changeGait:")
                {
                    var gaitNode = node.Next();
                    if (gaitNode is Integer && gaits.ContainsKey(gaitNode.Number))
                    {
                        gaitNode.Annotate(gaits[gaitNode.Number]);
                    }
                }
            }
        }

        static Dictionary<int, string> gaits = new Dictionary<int, string>
        {
            { 0, "walking" },
            { 1, "running" },
            { 2, "sneaking" },
        };
    }
}
