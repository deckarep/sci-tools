using System.Collections.Generic;
using System.Linq;
using SCI.Language;

//  ; most games
//  (method(doVerb theVerb)
//	    (switch theVerb
//          (1 ; Look
//              ...
//		    )
//		    (4 ; Do
//              ...
//          )
//      )
//
// ; early games like longbow
// (method(doVerb theVerb param2)
//      (switch theVerb
//      	(4 ; Inventory
//			    (switch param2
//                  (17 ; handScroll
//
// (== theVerb 1) ; Look
// (OneOf theVerb 1 4) ; Look, Do
//
// (... approachVerbs: 1 4 ...) ; Look, Do
//
//
// this also handles phant2 style, in which a global verb is set
// and tested in various methods (handleEvent, doit)

namespace SCI.Annotators
{
    static class VerbAnnotator
    {
        public static void Run(Game game, IReadOnlyDictionary<int, string> verbs,
                               IReadOnlyDictionary<int, string> inventoryVerbs = null,
                               int phant2GlobalVerb = -1)
        {
            if (!verbs.Any()) return;

            var methods = from s in game.Scripts
                          from o in s.Objects
                          from m in o.Methods
                          select m;

            // phant2-style: annotate handleEvent instead of doVerb against
            // a global variable instead of a parameter.
            bool doVerbStyle = phant2GlobalVerb == -1;
            if (doVerbStyle)
            {
                methods = methods.Where(m => m.Name == "doVerb");
            }

            foreach (var method in methods)
            {
                string verbName;
                if (doVerbStyle)
                {
                    verbName = method.Parameters.FirstOrDefault(p => p.Number == 1)?.Name;
                }
                else
                {
                    verbName = game.GetGlobal(phant2GlobalVerb)?.Name;
                }
                if (verbName != null)
                {
                    ProcessDoVerb(method, verbName, verbs);
                }
                if (inventoryVerbs != null)
                {
                    var inventoryVerbParam = method.Parameters.FirstOrDefault(p => p.Number == 2);
                    if (inventoryVerbParam != null)
                    {
                        ProcessDoVerb(method, inventoryVerbParam.Name, inventoryVerbs);
                    }
                }
            }

            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (node.Text == "approachVerbs:" ||
                    node.Text == "addRespondVerb:" || // kq7
                    node.Text == "deleteHotVerb:" ||  // kq7
                    node.Text == "setHotspot:")       // kq7
                {
                    var verbNode = node.Next();
                    while (!verbNode.IsSelector() && !(verbNode is Nil))
                    {
                        if (verbNode is Integer)
                        {
                            if (verbNode.Number != 9998 && // kq7
                                !(node.Text == "setHotspot:" && verbNode.Number == 0)) // kq7
                            {
                                Annotate(verbNode, verbs);
                            }
                        }
                        verbNode = verbNode.Next();
                    }
                }
            }
        }

        static void ProcessDoVerb(Function doVerb, string paramName, IReadOnlyDictionary<int, string> verbs)
        {
            ConstantFinder.Run(
                doVerb.Node,
                n => n.Text == paramName,
                n => Annotate(n, verbs));
        }

        // public so that Kq5VerbAnnotator can use this
        public static void Annotate(Node node, IReadOnlyDictionary<int, string> verbs)
        {
            string name;
            if (!verbs.TryGetValue(node.Number, out name))
            {
                if (node.Number == 0) return; // no "???" on 0, it's null and common
                name = "???";
            }
            node.Annotate(name);
        }
    }
}