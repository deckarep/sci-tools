using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // Annotate flag names in IsFlag/SetFlagClearFlag calls
    static class FlagAnnotator
    {
        public static void Run(Game game, IReadOnlyDictionary<int, string> flags)
        {
            if (!flags.Any()) return;

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (node.Children.Count >= 2 &&
                    (node.At(0).Text == "IsFlag" ||
                     node.At(0).Text == "SetFlag" ||
                     node.At(0).Text == "ClearFlag") &&
                    node.At(1) is Integer)
                {
                    string flagName;
                    if (flags.TryGetValue(node.At(1).Number, out flagName))
                    {
                        node.At(1).Annotate(flagName);
                    }
                }
            }
        }
    }
}
