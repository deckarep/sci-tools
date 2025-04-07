using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // SQ4CD narration lines look like:
    //  (gSq4GlobalNarrator say: 6)
    //  (gSq4GlobalNarrator say: 6 self)
    //  (gSq4GlobalNarrator modNum: 30 say: 11)
    //  (tCredits say: 9 self)
    // say's param is the cond.
    // narration nouns are always 99. i haven't seen a single non-zero verb in the game.
    //
    // unlike the later messagers, this one doesn't rely on any context.
    //
    // Prop
    //   Narrator
    //     Sq4Narrator
    //       FaceTalker
    //
    // (die picNum cond) cond is from message 99, with a slight adjustment
    //
    // All of Floppy's text is embedded in the scripts as string literals, so there's nothing to
    // annotate. it's already there!
    static class Sq4CDMessageAnnotator
    {
        public static void Run(Game game, MessageFinder messageFinder)
        {
            var die = game.GetExport(0, 10); // death function

            foreach (var script in game.Scripts)
            {
                foreach (var node in script.Root)
                {
                    if (node.Text == "say:")
                    {
                        AnnotateSay(game, script, node.Parent, messageFinder);
                    }
                    else if (node.At(0).Text == die)
                    {
                        AnnotateDie(script, node, messageFinder);
                    }
                }
            }

            AnnotateLookStrings(game, messageFinder);
        }

        static void AnnotateSay(Game game, Script script, Node node, MessageFinder messageFinder)
        {
            var narratorName = node.At(0).Text;
            var globalNarratorName = game.GetGlobal(89).Name;

            // get modNum from line if present.
            // otherwise, get it from the narrator, falling back on current script.
            // use the current script when it's the global narrator
            int modNum = script.Number; // default
            var modNumNode = node.FindChild("modNum:").Next();
            if (modNumNode is Integer)
            {
                modNum = modNumNode.Number;
            }
            else if (narratorName != globalNarratorName)
            {
                var narrator = script.Objects.FirstOrDefault(o => o.Name == narratorName);
                if (narrator == null)
                {
                    // abort if unknown narrator
                    return;
                }
                var modNumProperty = narrator.Properties.FirstOrDefault(p => p.Name == "modNum");
                if (modNumProperty != null)
                {
                    modNum = modNumProperty.ValueNode.Number;
                }
            }

            // get noun from narrator.
            // if it's the global narrator then noun is 99,
            // otherwise use the talkerNum from the object
            int noun = 99;
            if (narratorName != globalNarratorName)
            {
                // talkerNum: might be in the send call, otherwise use the property
                var talkerNumNode = node.FindChild("talkerNum:").Next();
                if (talkerNumNode is Integer)
                {
                    noun = talkerNumNode.Number;
                }
                else
                {
                    var narrator = script.Objects.FirstOrDefault(o => o.Name == narratorName);
                    if (narrator != null) // it should always exist
                    {
                        var nounProperty = narrator.Properties.FirstOrDefault(p => p.Name == "talkerNum");
                        if (nounProperty != null)
                        {
                            noun = nounProperty.ValueNode.Number;
                        }
                    }
                }
            }

            int verb = 0;
            var condNode = node.FindChild("say:").Next();
            if (condNode is Nil)
            {
                // there are some broken scripts [ i fixed them ]
                node.At(0).Annotate("MISSING MESSAGE");
                return;
            }
            else if (!(condNode is Integer))
            {
                // abort if say: non-integer
                return;
            }
            int cond = condNode.Number;

            var message = messageFinder.GetFirstMessage(script.Number, modNum, noun, verb, cond);
            if (message != null)
            {
                node.At(0).Annotate(message.Text.QuoteMessageText());
            }
        }

        // (die [pictureNumber] [messageNumber])
        static void AnnotateDie(Script script, Node node, MessageFinder messageFinder)
        {
            // ignore declaration of procedure, and also, there's astro:die
            if (node.Parent.At(0).Text == "procedure" ||
                node.Parent.At(0).Text == "method")
            {
                return;
            }

            int modNum = 900;
            int noun = 99;
            int verb = 0;

            // if no param 2 then it defaults to first death message.
            // rm900:init maps incoming cond to actual cond.
            // almost all are the same except for incoming values 6 or less
            int cond;
            if (node.At(2) is Integer)
            {
                cond = node.At(2).Number;
            }
            else if (node.At(2) is Nil)
            {
                cond = 0;
            }
            else
            {
                // abort
                return;
            }
            if (cond <= 5)
            {
                cond += 1;
            }
            else if (cond == 6)
            {
                cond = 21;
            }

            var message = messageFinder.GetFirstMessage(script.Number, modNum, noun, verb, cond);
            if (message != null)
            {
                node.At(0).Annotate(message.Text.QuoteMessageText());
            }
        }

        // a separate sq4 annotator i've bolted on for their lookStr properties
        static void AnnotateLookStrings(Game game, MessageFinder messageFinder)
        {
            var roomScripts = game.GetRooms();
            foreach (var script in roomScripts.Keys)
            {
                var lookStrs = from p in script.Objects.SelectMany(o => o.Properties)
                               where p.Name == "lookStr" &&
                                     p.ValueNode is Integer &&
                                     p.ValueNode.Number > 0
                               select p;
                foreach (var lookStr in lookStrs)
                {
                    int noun = 99;
                    int verb = 0;
                    int cond = lookStr.ValueNode.Number;
                    var message = messageFinder.GetFirstMessage(script.Number, script.Number, noun, verb, cond);
                    if (message != null)
                    {
                        lookStr.ValueNode.Annotate(message.Text.QuoteMessageText());
                    }
                    else
                    {
                        lookStr.ValueNode.Annotate("MISSING MESSAGE");
                    }
                }
            }
        }
    }
}
