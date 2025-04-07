using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // KQ6 castle has a zillion region flags; both for short and long path.
    // The first parameter to the flag methods is a selector, so rename
    // that, then make the flag number unsigned. Maybe hex would be better?

    static class Kq6FlagAnnotator
    {
        public static void Run(Game game, string[] selectors)
        {
            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if ((node.Text == "setFlag:" ||
                     node.Text == "clrFlag:" ||
                     node.Text == "tstFlag:"))
                {
                    var selectorNode = node.Next(1) as Integer;
                    if (selectorNode != null) // after first run it won't be a number
                    {
                        string selector = selectors[selectorNode.Number];
                        selectorNode.SetDefineText("#" + selector);
                    }

                    var flagNumNode = node.Next(2) as Integer;
                    if (flagNumNode != null)
                    {
                        flagNumNode.MakeUnsigned();
                    }
                }
            }
        }
    }
}
