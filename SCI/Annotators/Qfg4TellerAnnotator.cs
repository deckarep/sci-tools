using System.Collections.Generic;
using System.Linq;
using SCI.Language;
using SCI.Annotators.Visitors;

namespace SCI.Annotators
{
    // QFG4 Teller Annotator handles Teller:showCases and Teller:sayMessage.
    //
    // There are over 100 Teller instances. Here's how to use one:
    //
    // 1.  Initialize it by passing a client object and noun/verb/cond values.
    //     Example: (myTeller init: client modNum sayNoun verb [cond])
    //     If cond isn't passed, then client:noun is used.
    // 2.  Filter messages by implementing showCases and calling super:showCases:
    //     Example: (super showCases cond1 (true-or-false) cond2 (true-or-false) ...)
    // 3a. Add interesting behavior in response to specific messages by
    //     implementing sayMessage and testing the iconValue property to see
    //     which cond was selected.
    //     Example: (if (== iconValue 3) ...)
    // 3b. Call super:sayMessage with no parameters, or &rest, for default message.
    //     Example: (super sayMessage: &rest) ; message depends on iconValue
    //     super:sayMessage can also be called with explicit tuple values.
    //
    // I annotate cond values with the teller's prompt, and super:sayMessage with
    // the response message. To do this, the teller's init call has to be found
    // and parsed for tuple values. If the cond parameter isn't present, then the
    // client's noun value from the heap is used. If a teller is initialized in
    // multiple places then this can be ambiguous and fail. I'm sure there's room
    // for improvement, but it handles much more than I expected, so I'm happy.

    /* Goals:

    (method (showCases)
        (super
            showCases:
                26 ; Open door
                (IsFlag 108)
                27 ; Break door
                (IsFlag 109)

    (method (sayMessage)
        (switch iconValue
            (26 ; Open door
                (if (IsFlag 108)
                    (self clean:)
                    (gRoom setScript: sInGuildDoor)
                else
                    (super sayMessage: &rest) ; "The door is locked."
    */

    class Qfg4TellerAnnotator
    {
        public static void Run(Game game,MessageFinder messageFinder)
        {
            foreach (var script in game.Scripts)
            {
                Run(game, script, messageFinder);
            }
        }

        static void Run(Game game, Script script, MessageFinder messageFinder)
        {
            var tellers = GetTellers(script);
            if (tellers.Count == 0) return;

            ScanForLocalTellerInits(script, tellers);
            foreach (var teller in tellers)
            {
                FigureOutTellerAttributes(game, teller);
                AnnotateTeller(game, teller, messageFinder);
            }
        }

        // returns all instances of Teller in a script that have sayMessage or showCases methods
        static List<Teller> GetTellers(Script script)
        {
            var tellers = new List<Teller>();
            foreach (var obj in script.Objects.Where(o => o.Super == "Teller"))
            {
                // skip tellers that don't have sayMessage or showCases; there's nothing to annotate.
                if (!obj.Methods.Any(m => m.Name == "sayMessage" || m.Name == "showCases")) continue;

                var teller = new Teller { Object = obj };
                tellers.Add(teller);
            }
            return tellers;
        }

        // scans for all `init:` calls to each teller within the script.
        // populates Teller.Inits
        static void ScanForLocalTellerInits(Script script, IReadOnlyList<Teller> tellers)
        {
            // build map
            var map = new Dictionary<string, Teller>();
            foreach (var teller in tellers)
            {
                map.Add(teller.Name, teller);
            }

            // scan all functions in script
            foreach (var function in script.GetFunctions())
            {
                foreach (var node in function.Node.Where(n => n.Text == "init:"))
                {
                    Teller teller;
                    if (!map.TryGetValue(node.Parent.At(0).Text, out teller)) continue;

                    // found init: call to teller
                    var init = new TellerInit();
                    init.Function = function;
                    init.Selector = node;
                    teller.Inits.Add(init);
                }
            }
        }

        static void FigureOutTellerAttributes(Game game, Teller teller)
        {
            // teller init method forms i can use are:
            // (myTeller init: client modNum sayNoun verb curNoun)
            // (myTeller init: client modNum sayNoun verb)
            // if curNoun isn't present then client:noun is curNoun
            string ego = game.GetGlobal(0).Name;
            for (int i = 0; i < teller.Inits.Count; i++)
            {
                TellerInit init = teller.Inits[i];

                // gather init parameters
                var parameters = init.Selector.GetSelectorParameters().ToList();

                // skip inits where there's not enough parameters
                if (parameters.Count != 4 && parameters.Count != 5) continue;

                int modNum = -1;
                int sayNoun = -1;
                int verb = -1;
                int curNoun = -1;

                if (parameters[1] is Integer) modNum = parameters[1].Number;
                if (parameters[2] is Integer) sayNoun = parameters[2].Number;
                if (parameters[3] is Integer) verb = parameters[3].Number;

                if (parameters.Count == 4)
                {
                    // figure out curNoun from client
                    string client = parameters[0].Text;
                    if (client == ego)
                    {
                        curNoun = 1;
                    }
                    else
                    {
                        Language.Object obj;
                        if (client == "self")
                        {
                            obj = init.Function.Object;
                        }
                        else
                        {
                            obj = init.Function.Script.Objects.FirstOrDefault(o => o.Name == client);
                        }
                        if (obj != null)
                        {
                            int value = obj.GetIntegerProperty("noun");
                            if (value != int.MinValue)
                            {
                                curNoun = value;
                            }
                        }
                    }
                }
                else
                {
                    if (parameters[4] is Integer)
                    {
                        curNoun = parameters[4].Number;
                    }
                }

                // apply
                if (i == 0)
                {
                    if (modNum != -1 &&
                        sayNoun != -1 &&
                        verb != -1)
                    {
                        teller.ModNum = modNum;
                        teller.SayNoun = sayNoun;
                        teller.Verb = verb;
                        teller.CurNoun = curNoun;
                    }
                    else
                    {
                        // skip it!
                        teller.Ambiguous = true;
                        break;
                    }
                }
                else
                {
                    if (teller.ModNum != modNum ||
                        teller.SayNoun != sayNoun ||
                        teller.Verb != verb)
                    {
                        // multiple inits to same teller with
                        // different modnum or saynoun or verb;
                        // this teller is ambiguous, skip it.
                        teller.Ambiguous = true;
                        break;
                    }
                    else
                    {
                        // multiple inits to same teller and only
                        // difference is curnoun. fine, set it to
                        // unknown, we might still be able to annotate.
                        if (teller.CurNoun != curNoun)
                        {
                            teller.CurNoun = -1;
                        }
                    }
                }
            }
        }

        static void AnnotateTeller(Game game, Teller teller, MessageFinder messageFinder)
        {
            if (teller.Ambiguous)
            {
                Log.Warn(game, "Skipping ambiguous teller: " + teller);
                return;
            }

            var db = new DeepBeige();
            db.OnNode = (Node node) =>
            {
                Visit(db, node, teller, messageFinder);
            };
            var methods = teller.Object.Methods.Where(m => m.Name == "showCases" ||
                                                            m.Name == "sayMessage");
            foreach (var method in methods)
            {
                db.Visit(method);
            }
        }

        // using the same visitor on both showCases and sayMessage
        static void Visit(DeepBeige db, Node node, Teller teller, MessageFinder messageFinder)
        {
            // annotate three things:
            //   sayMessage:
            //     1. iconValue comparisons
            //     2. super:sayMessage calls
            //        * currently only super:sayMessage(&rest) supported,
            //          TODO: figure out parameters passed to others.
            //   showCases:
            //     3. super:showCases parameters (every other, starting with first)

            var iconValueNodes = GetIntegerNodesComparedToSymbol(node, "iconValue");
            if (iconValueNodes != null)
            {
                foreach (var iconValueNode in iconValueNodes)
                {
                    AnnotateCondNode(iconValueNode, teller, messageFinder);
                }
            }

            // super:sayMessage
            if (node.At(0).Text == "super" &&
                node.At(1).Text == "sayMessage:")
            {
                // get parameters, remove &rest
                var parameters = node.At(1).GetSelectorParameters().ToList();
                if (parameters.Any() && parameters.Last().Text == "&rest")
                {
                    parameters.RemoveAt(parameters.Count - 1);
                }

                // argc   params
                // 0
                // 1      cond
                // 2      verb cond
                // 3      sayNoun verb cond
                int sayNoun = teller.SayNoun;
                int verb = teller.Verb;
                int cond = db.Resolve("iconValue");
                bool abort = false;
                if (parameters.Count == 1)
                {
                    if (parameters[0] is Integer)
                    {
                        cond = parameters[0].Number;
                    }
                    else
                    {
                        abort = true;
                    }
                }
                else if (parameters.Count == 2)
                {
                    if (parameters[0] is Integer &&
                        parameters[1] is Integer)
                    {
                        verb = parameters[0].Number;
                        cond = parameters[1].Number;
                    }
                    else
                    {
                        abort = true;
                    }
                }
                else if (parameters.Count >= 3)
                {
                    if (parameters[0] is Integer &&
                        parameters[1] is Integer &&
                        parameters[2] is Integer)
                    {
                        sayNoun = parameters[0].Number;
                        verb = parameters[1].Number;
                        cond = parameters[2].Number;
                    }
                    else
                    {
                        abort = true;
                    }
                }

                if (!abort)
                {
                    var message = messageFinder.GetFirstMessage(teller.Script, teller.ModNum, sayNoun, verb, cond, 1, false);
                    if (message != null)
                    {
                        node.At(1).Annotate(message.Text.SanitizeMessageText());
                    }
                }
            }

            // super:showCases cond1 param cond2 param ...
            if (node.At(0).Text == "super" &&
                node.At(1).Text == "showCases:")
            {
                var condNodes = new List<Node>();
                int i = 0;
                foreach (var parameter in node.At(1).GetSelectorParameters())
                {
                    // even parameters are conds
                    if (i % 2 == 0)
                    {
                        if (parameter is Integer)
                        {
                            condNodes.Add(parameter);
                        }
                    }
                    i++;
                }

                foreach (var condNode in condNodes)
                {
                    AnnotateCondNode(condNode, teller, messageFinder);
                }
            }

        }

        static void AnnotateCondNode(Node condNode, Teller teller, MessageFinder messageFinder)
        {
            int cond = condNode.Number;

            // if curNoun is unknown then see if it can be unambiguously figured out by process of elimination
            int curNoun = teller.CurNoun;
            if (curNoun == -1)
            {
                curNoun = DetermineCurNoun(teller.ModNum, teller.SayNoun, teller.Verb, cond, messageFinder);

                // can't do this if curNoun is unknown
                if (curNoun == -1) return;
            }

            var message = messageFinder.GetFirstMessage(teller.Script, teller.ModNum, curNoun, teller.Verb, cond, 1, false);
            if (message == null)
            {
                // ha well this is dumb and i don't know why it works but it makes the books in the guild get annotated
                // with no other false positives. i can't find the script that adds 1 to curNoun but it must be in there.
                // maybe because it's on a second page?
                message = messageFinder.GetFirstMessage(teller.Script, teller.ModNum, curNoun + 1, teller.Verb, cond, 1, false);
            }

            if (message != null)
            {
                condNode.Annotate(message.Text.SanitizeMessageText());
            }
        }

        static int DetermineCurNoun(int modNum, int sayNoun, int verb, int cond, MessageFinder messageFinder)
        {
            List<Message> messages;
            if (!messageFinder.Messages.TryGetValue(modNum, out messages)) return -1;

            var candidates = messages.Where(m => m.Noun != sayNoun &&
                                            m.Verb == verb &&
                                            m.Cond == cond &&
                                            m.Seq == 1).ToList();
            if (candidates.Count == 0)
            {
                return -1;
            }
            else if (candidates.Count == 1)
            {
                return candidates[0].Noun;
            }
            else
            {
                // haily mary time, if all the candidates evaluate to the same non-blank
                // message text then their differences don't matter because they will
                // be evaluated the same.
                var resolvedMessages = (from m in candidates
                                        select messageFinder.GetFirstMessage(-1, m.ModNum, m.Noun, m.Verb, m.Cond, m.Seq, false)
                                            ).ToList();
                if (resolvedMessages.All(m => m != null))
                {
                    var texts = resolvedMessages.Select(m => m.Text).Distinct().ToList();
                    if (texts.Count == 1 && !string.IsNullOrWhiteSpace(texts[0]))
                    {
                        return candidates[0].Noun;
                    }
                }
                return -1;
            }
        }

        static List<Node> GetIntegerNodesComparedToSymbol(Node node, string symbol)
        {
            List<Node> results = null; // optimization, only create list if necessary

            ConstantFinder.Run(node,
                n => n.Text == symbol,
                n =>
                {
                    if (results == null)
                    {
                        results = new List<Node>();
                    }
                    results.Add(n);
                });

            return results;
        }

        class Teller
        {
            public Language.Object Object;
            public int Script { get { return Object.Script.Number; } }
            public string Name { get { return Object.Name; } }

            public List<TellerInit> Inits = new List<TellerInit>();

            public int ModNum = -1;
            public int CurNoun = -1; // -1 if unknown
            public int SayNoun = -1;
            public int Verb = -1;

            public bool Ambiguous;

            public override string ToString()
            {
                return Name + "[ script: " + Script + "]" + (Ambiguous ? " AMBIGUOUS" : "");
            }
        }

        class TellerInit
        {
            public Function Function;
            public Node Selector;
        }
    }
}
