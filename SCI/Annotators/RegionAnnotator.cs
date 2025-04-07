using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // (... setRegions: scriptNumber [scriptNumber ...] ...)
    class RegionAnnotator
    {
        public static void Run(Game game)
        {
            var nodes = from s in game.Scripts
                        from n in s.Root
                        where n.Text == "setRegions:"
                        select n;
            foreach (var node in nodes)
            {
                Node scriptNode = node;
                while ((scriptNode = scriptNode.Next()) is Integer)
                {
                    var region = game.GetExport(scriptNode.Number, 0);
                    if (region != null)
                    {
                        scriptNode.Annotate(region);
                    }
                }
            }
        }
    }
}
