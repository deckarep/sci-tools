using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // If a property is known to be a bit mask, then any integer
    // that is assigned or compared to it should be in hexadecimal.
    //
    // Unfortunately... there's only one property I can think of that
    // this is useful for, and that's illegalBits. `signal` would be
    // okay too except that it's a common name and also used in Sound.

    static class PropertyHexer
    {
        static string[] HexProperties = { "illegalBits" };

        public static void Run(Game game)
        {
            // property declarations:
            //
            // (properties
            //    property -32768
            var properties = from s in game.Scripts
                             from o in s.Objects
                             from p in o.Properties
                             where HexProperties.Contains(p.Name)
                             select p;
            foreach (var property in properties)
            {
                var value = property.ValueNode as Integer;
                if (value != null)
                {
                    Format(value);
                }
            }

            // property use in function bodies
            foreach (var function in game.GetFunctions())
            {
                ConstantFinder.Run(function.Node,
                    n =>
                    {
                        // (== illegalBits -32768)
                        if (HexProperties.Any(p => n.IsSelector(p)))
                        {
                            return true;
                        }

                        // (== (object illegalBits:) -32768)
                        if (n.Children.Count > 0)
                        {
                            Node lastChild = n.Children[n.Children.Count - 1];
                            if (HexProperties.Any(p => lastChild.IsSelector(p)))
                            {
                                return true;
                            }
                        }
                        return false;
                    },
                    n => Format(n));

                // illegalBits: -32768
                foreach (var node in function.Node)
                {
                    if (node.Text.EndsWith(":") &&
                        HexProperties.Any(p => node.IsSelector(p)))
                    {
                        var value = node.Next() as Integer;
                        if (value != null)
                        {
                            Format(value);
                        }
                    }
                }
            }
        }

        static void Format(Integer n)
        {
            // leave zeros alone, because there are so many illegalBits
            // that changing them all from 0 => $0000 will modify 1/6th of
            // the files in sci-scripts, and that would be a dumb commit.
            if (n.Number != 0)
            {
                n.SetHexFormat();
            }
        }
    }
}
