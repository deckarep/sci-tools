using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // Teller
    //  |---- GKTeller       asking someone questions at bottom of screen
    //  |---- Interrogation  full screen questioning in room 50
    //
    // showCases contains the logic for which options are available. This gets passed on to the base class
    // by calling showCases cond enabled [cond enabled...]. i annotate the "cond" nodes with the on-screen
    // text for the option. the cond is known, but the modNum, noun, and verb aren't known for certain.
    // If the "modNum" property is set then great, otherwise I go off of unique room callers.
    // noun is "curNoun" and verb is "verb" and these properties are usually (always?) set, but they can
    // also be changed at runtime. I detect that possibility and brute force all the combos.
    // noun is also tricky because Teller:respond does some weird stuff where it tries to find a seq=2
    // referenced noun and then it looks up the real noun and uses that with sequence 1.
    //
    // cue fires when text is clicked. iconValue stores the selected cond so i annotate each of those with
    // the on-screen text, falling back on possibly gabriel's response. iconValue also gets updated and
    // i'm going to try annotating that too.
    //
    // it would be nice to annotate the self:sayMessage calls in cue but iconValue often gets set and optionally
    // incremented first, so i think i'd need to traverse the logic code like in SayAnnotator.
    //
    // This is a very old annotator that I ported over to the new parser. That cleaned it up a bit,
    // but there is some kitchen-sink message resolution that may not make sense. The results are
    // pretty good so I doubt I'll ever come back to this, but the Teller annotators skew cray.

    static class Gk1TellerAnnotator
    {
        public static void Run(Game game, MessageFinder messageFinder)
        {
            HashSet<Object> tellers = game.GetObjectsBySuper("Teller");
            Dictionary<Script, string> roomScripts = game.GetRooms();
            Dictionary<int, int> callerScripts = game.GetUniqueRoomCallers(roomScripts.Keys);
            foreach (var roomScript in roomScripts.Keys)
            {
                callerScripts[roomScript.Number] = roomScript.Number;
            }

            foreach (var teller in tellers)
            {
                bool isInterrogation = (teller.Super == "Interrogation");
                int curNoun = teller.GetIntegerProperty("curNoun");
                int sayNoun = teller.GetIntegerProperty("sayNoun");
                if (sayNoun == int.MinValue && isInterrogation)
                {
                    // Interrogation:sayNoun is 7
                    sayNoun = 7;
                }
                int verb = teller.GetIntegerProperty("verb");
                int modNum = teller.GetIntegerProperty("modNum");
                var curNouns = GetAlternateCurNouns(teller);
                if (curNoun != int.MinValue)
                {
                    curNouns.Remove(curNoun);
                    curNouns.Insert(0, curNoun);
                }
                var verbs = GetAlternateVerbs(teller);
                if (verb != int.MinValue)
                {
                    verbs.Remove(verb);
                    verbs.Insert(0, verb);
                }

                // annotate showCases statements
                // (method(showCases)
                //     (super
                //         showCases:
                //             4           ; cond
                //             IsFlag(23)  ; logic that determines whether or not to show
                //             5           ; cond
                //             IsFlag(24)  ; ...
                //             ...
                var showCases = teller.Methods.FirstOrDefault(m => m.Name == "showCases");
                if (showCases != null)
                {
                    foreach (var node in showCases.Node)
                    {
                        if (node.Text == "showCases:" &&
                            node.Parent.At(0).Text == "super")
                        {
                            var condNode = node.Next();
                            while (!(condNode is Nil))
                            {
                                if (condNode is Integer)
                                {
                                    AnnotateShowCasesCond(teller, condNode, curNouns, verbs, modNum, messageFinder, callerScripts);
                                }
                                condNode = condNode.Next(2);
                            }
                        }
                    }
                }

                // annotate all iconValue tests and sets within all methods.
                foreach (var method in teller.Methods)
                {
                    ConstantFinder.Run(
                        method.Node,
                        n => n.Text == "iconValue",
                        condNode => AnnotateCueCond(teller, condNode, curNouns, sayNoun, verbs, modNum, messageFinder, callerScripts)
                    );
                }
            }
        }

        // curNoun can be dynamically temporarily updated by calling self:newNoun
        // which also pushes the old one on a stack.
        static List<int> GetAlternateCurNouns(Object teller)
        {
            var alternateCurNouns = new List<int>();
            foreach (var node in teller.Node)
            {
                if (node.Text == "newNoun:" &&
                    node.Parent.At(0).Text == "self" &&
                    node.Next() is Integer)
                {
                    int curNoun = node.Next().Number;
                    if (!alternateCurNouns.Contains(curNoun))
                    {
                        alternateCurNouns.Add(curNoun);
                    }
                }
            }
            return alternateCurNouns;
        }

        // verb can be dynamically updated by calling self:verb but unlike newNoun
        // it doesn't push the old one on a stack.
        static List<int> GetAlternateVerbs(Object teller)
        {
            var alternateVerbs = new List<int>();
            foreach (var node in teller.Node)
            {
                if (node.Text == "verb:" &&
                    node.Parent.At(0).Text == "self" &&
                    node.Next() is Integer)
                {
                    int verb = node.Next().Number;
                    if (!alternateVerbs.Contains(verb))
                    {
                        alternateVerbs.Add(verb);
                    }
                }
            }
            return alternateVerbs;
        }

        static void AnnotateShowCasesCond(Object teller, Node condNode, List<int> curNouns, List<int> verbs, int modNum, MessageFinder messageFinder, Dictionary<int, int> callerScripts)
        {
            bool isInterrogation = teller.Super == "Interrogation";
            var message = GetQuestionMessage(isInterrogation, teller.Script.Number, modNum, curNouns, verbs, condNode.Number, messageFinder, callerScripts);
            if (message != null)
            {
                condNode.Annotate(message.Text.SanitizeMessageText());
            }
        }

        static void AnnotateCueCond(Object teller, Node condNode, List<int> curNouns, int sayNoun, List<int> verbs, int modNum, MessageFinder messageFinder, Dictionary<int, int> callerScripts)
        {
            bool isInterrogation = teller.Super == "Interrogation";
            var message = GetQuestionMessage(isInterrogation, teller.Script.Number, modNum, curNouns, verbs, condNode.Number, messageFinder, callerScripts);
            if (message != null)
            {
                condNode.Annotate(message.Text.SanitizeMessageText());
                return;
            }

            // COMMENT FROM OLD ANNOTATOR:
            // if there's no message for this cond then it's probably because it's for handling what happens
            // after asking a question with multiple answers. for example, asking sarg about mosely gives
            // different answers depending on if he's at a crime scene, in his office, or disavowed.
            // the "Detective Mosely" cond, 19, sets a new iconValue and gkMessager cues the interrogation
            // again after gabriel's first line "I'm here to see detective mosely".
            //
            // for these conds, i choose to use the first line, using sayNoun, which is usually gabriel's line
            var sayNouns = new List<int>();
            sayNouns.Add(sayNoun);

            message = GetQuestionMessage(isInterrogation, teller.Script.Number, modNum, sayNouns, verbs, condNode.Number, messageFinder, callerScripts);
            if (message != null)
            {
                condNode.Annotate(message.Text.QuoteMessageText());
            }
        }

        static Message GetQuestionMessage(bool isInterrogation, int currentScript, int modNum, List<int> nouns, List<int> verbs, int cond, MessageFinder messageFinder, Dictionary<int, int> callerScripts)
        {
            // if this is an interrogation about a global question (1 through 15) then ignore noun and verb because
            // they are hard-coded to 3 0 by Interrogation:addGlobals.
            if (isInterrogation && cond <= 15)
            {
                nouns = new List<int>();
                nouns.Add(3);
                verbs = new List<int>();
                verbs.Add(0);
            }

            List<int> modNumsToTry = new List<int>();
            if (modNum != int.MinValue)
            {
                modNumsToTry.Add(modNum);
            }
            else
            {
                int callerModNum;
                if (callerScripts.TryGetValue(currentScript, out callerModNum))
                {
                    modNumsToTry.Add(callerModNum);
                }
                modNumsToTry.Add(currentScript);
            }

            // going to do some funky stuff to handle messages that are in response to other messages.
            // for example, asking gerde about wolfgang runs some text, then presents a couple individual questions.
            // the noun/verb/conds that i've scraped won't exist. instead, Teller:respond calls kernel function
            // Message MSG_GETREFNOUN modNum curNoun verb wolfgangCond 2, which looks up that message and returns
            // its ref noun, which in this case where wolfgangCond is 58 returns 6, and so 6 needs to be used
            // as the noun to find messages.

            foreach (int modNumToTry in modNumsToTry)
            {
                foreach (var noun in nouns)
                {
                    foreach (var verb in verbs)
                    {
                        // in order to not log a bunch of warnings, casually test if a message tuple exists, and it doesn't
                        // then try ref'ed nouns.
                        bool tryReffedNouns;
                        List<Message> nounReffedMessages = null;
                        if (messageFinder.GetFirstMessageNonRecursive(
                                modNumToTry,
                                modNumToTry,
                                noun,
                                verb,
                                cond,
                                1,
                                false) != null)
                        {
                            tryReffedNouns = false;
                        }
                        else
                        {
                            nounReffedMessages = GetPotentialNounReffedMessages(modNumToTry, noun, verb, cond, messageFinder);
                            tryReffedNouns = nounReffedMessages.Any();
                        }

                        Message message;
                        if (!tryReffedNouns)
                        {
                            message = messageFinder.GetFirstMessage(currentScript, modNumToTry, noun, verb, cond);
                        }
                        else
                        {
                            var nounReffedMessage = nounReffedMessages.First();
                            message = messageFinder.GetFirstMessage(currentScript, modNumToTry, nounReffedMessage.Noun, nounReffedMessage.Verb, nounReffedMessage.Cond);
                        }

                        if (message != null)
                        {
                            return message;
                        }
                    }
                }
            }
            return null;
        }

        // brought in from original annotator
        static List<Message> GetPotentialNounReffedMessages(int modNum, int curNoun, int verb, int cond, MessageFinder messageFinder)
        {
            var messages = messageFinder.GetMessagesByModNum(modNum) ?? new List<Message>();

            var refNouns = (from m in messages
                            where m.Noun == curNoun &&
                                  m.Verb == verb &&
                                  m.Seq == 2 &&
                                  m.IsRef
                            select m.RefNoun).ToList();

            var potentialMessages = (from m in messages
                                     where refNouns.Contains(m.Noun) &&
                                           m.Verb == verb &&
                                           m.Cond == cond &&
                                           m.Seq == 1
                                     select m).ToList();
            return potentialMessages;
        }
    }
}
