using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // Annotates calls to EgoGait procedure and use of the gEgoGait global.
    // Requires globals to already being renamed.
    // Applies to Qfg1Ega, Qfg1Vga, Qfg2. (Global number changes in Qfg1Vga-Mac)

    static class Qfg12GaitAnnotator
    {
        static Dictionary<int, string> gaits = new Dictionary<int, string>
        {
            { 0, "walking" },
            { 1, "running" },
            { 2, "sneaking" },
            { 3, "riding" }, // qfg2
            { 4, "holdingLamp" }, // qfg2
        };

        public static void Run(Game game)
        {
            GlobalEnumAnnotator.Run(game, "gEgoGait", gaits);

            foreach (var node in game.GetFunctions().SelectMany(f => f.Node))
            {
                if (node.At(0).Text == "EgoGait" &&
                    node.At(1) is Integer)
                {
                    string gait;
                    if (gaits.TryGetValue(node.At(1).Number, out gait))
                    {
                        node.At(1).Annotate(gait);
                    }
                }
            }
        }
    }
}
