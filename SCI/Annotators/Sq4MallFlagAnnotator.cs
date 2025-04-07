using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // "(proc700_3 (ScriptID 700 0) 678 ..."
    // "678" is an rFlagX selector, so replace the integer with a selector
    //
    // These selector values change between versions, which is why I use
    // the real selector table now that I have Sierra.Resource.

    static class Sq4MallFlagAnnotator
    {
        public static void Run(Game game, string[] selectors)
        {
            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (node.Children.Count == 3 &&
                    node.At(0).Text == "ScriptID" &&
                    node.At(1).Text == "700" &&
                    node.At(2).Text == "0" &&
                    node.Next() is Integer)
                {
                    var selectorNode = node.Next() as Integer;
                    if (selectorNode.Number < selectors.Length &&
                        selectors[selectorNode.Number].StartsWith("rFlag"))
                    {
                        selectorNode.SetDefineText("#" + selectors[selectorNode.Number]);
                    }
                }
            }
        }
    }
}
