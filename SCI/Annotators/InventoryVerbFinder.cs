using System;
using System.Collections.Generic;
using System.Linq;
using SCI.Language;

// InventoryVerbFinder code-gens a c# dictionary to paste into verb annotators.
// This isn't an annotator, it's a thing that helped me write then.
//
// These values could be determined at run-time, but non-inventory verbs need to
// be manually figured out in each game. There are no standard enum values for
// common SCI verbs like "Look" or "Do". Some games make up their own verbs
// like SQ4's taste/smell  and Longbow's, um, longbow. And then QFG adds spells
// into the mix. For all these reasons, plus my own opinions on verb names, it's
// easiest to just manually set the non-inventory verbs in a dictionary and then
// code-gen the inventory verbs, paste them in, and adjust as needed.

namespace SCI.Annotators
{
    public static class InventoryVerbFinder
    {
        public static Dictionary<int, string> Run(Game game, string inventoryItemClassName)
        {
            var verbs = new Dictionary<int, string>();

            string[] verbProperties = {
                "message", // most
                "verb",    // kq7, probably other sci32
                "myVerb"   // lighthouse
            };

            foreach (var script in game.Scripts)
            {
                foreach (var instance in script.Instances)
                {
                    if (instance.Super == inventoryItemClassName)
                    {
                        var property = instance.Properties.FirstOrDefault(i => verbProperties.Contains(i.Name));
                        if (property != null && property.ValueNode is Integer)
                        {
                            int number = property.ValueNode.Number;
                            if (verbs.ContainsKey(number))
                            {
                                verbs[number] = verbs[number] += " or " + instance.Name;
                            }
                            else
                            {
                                verbs.Add(number, instance.Name);
                            }
                        }
                    }
                }
            }

            return verbs;
        }

        public static void Print(string inventoryItemClassName, string gameDirectory)
        {
            var game = new Game(gameDirectory);
            var verbs = Run(game, inventoryItemClassName);
            foreach (var number in verbs.Keys.OrderBy(k => k))
            {
                Console.WriteLine("{ " + number + ", \"" + verbs[number] + "\" }, ");
            }
        }
    }
}