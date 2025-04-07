using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // UPDATE: Long after writing all that text below, I found floppy version 1.1,
    //         which is SCI1.1 like the CD version. It uses the full message format
    //         and was significantly reprogrammed for SCI1.1 on the way to the CD version.
    //         This annotator is now used for floppy 1.1, where the only tweak is that
    //         "Talker" is used as the base class instead of "EcoTalker", because
    //         EcoTalker and EcoNarrator are only in CD.
    //
    //         Also, wow this is an excessive annotator; I vaguely remember doing this
    //         on an gratuitously long train ride.
    //
    // EcoQuest 1 displays messages in a unique way and the code is different between
    // CD and floppy versions. Floppy uses an early version of message resources that
    // only supports nouns and verbs, and so that's all the scripts deal with.
    // CD upgraded to the later version with the standard noun/verb/cond/seq tuple.
    // Neither uses the usual messager class, messages are displayed by calling
    // "init" on a descendant of EcoTalker.
    //
    // The seq param is loaded. If it's high then it gets transformed and alters
    // the noun and/or cond, which in turn is affected by the value of a global
    // variable. That global variable is usually set at the start of room init
    // and i can usually parse that out and do the right thing.
    //
    // EcoTalker (class)
    //   Adam (instance)
    //   Delphineus (instance)
    //   An instance per character...
    //   EcoNarrator (class)
    //     FishNarrator (instance)
    //
    // (Narcissus ... init: noun verb cond seq [ ___ ___ [ modNum ] ]
    //    Narcissus is an EcoTalker.
    //
    // TODO for extra credit:
    // - resolve lookStr property when passed as seq (or any param)
    // - handle Random (start, end) passed as part of tuple and
    //   evaluate all possibilities??
    static class Eco1CDMessageAnnotator
    {
        public static void Run(Game game, bool isFloppy, MessageFinder messageFinder)
        {
            var ecoTalkers = game.GetObjectsBySuper(isFloppy ? "Talker" : "EcoTalker");
            var rooms = game.GetRooms();
            var roomCallers = game.GetUniqueRoomCallers(rooms.Keys);

            foreach (var script in game.Scripts)
            {
                int global251 = GetGlobal251(game, script, rooms);

                foreach (var node in script.Root)
                {
                    if (node.Text != "init:") continue;

                    Object ecoTalker = GetEcoTalker(game, node.Parent.At(0), ecoTalkers);
                    if (ecoTalker == null)
                    {
                        continue;
                    }

                    // read first four integer params: noun, verb, cond, seq.
                    // may eventually support some of these being properties
                    // of the object whose method contains this node.
                    // for now, integer literals only.
                    Node nounNode = node.Next(1);
                    if (!(nounNode is Integer)) continue;
                    Node verbNode = node.Next(2);
                    if (!(verbNode is Integer)) continue;
                    Node condNode = node.Next(3);
                    if (!(condNode is Integer)) continue;
                    Node seqNode = node.Next(4);
                    if (!(seqNode is Integer)) continue;

                    // count parameters. could be 4 to 7.
                    int parameterCount = 4;
                    while (true)
                    {
                        var nextParameter = node.Next(parameterCount + 1);
                        if (nextParameter is Nil) break;
                        if (nextParameter.Children.Any()) break;
                        if (nextParameter.IsSelector()) break;
                        parameterCount += 1;
                    }

                    // try to find modNum. if it's not specified then the
                    // current room is used, so if this script is a room,
                    // then use that. there are two classes to deal with:
                    // EcoNarrator init: n v c s [caller [modNum]]
                    // EcoTalker   init: n v c s [disposeWhenDone [caller] [[modNum]]]
                    // if there are 7 params then the last is modNum. if not integer then bail.
                    // if there are 6 params and 6 is integer then it is modNum

                    int modNum = -1;
                    if (parameterCount == 7)
                    {
                        var modNumNode = node.Next(7);
                        if (!(modNumNode is Integer))
                        {
                            // can't figure out modNum
                            continue;
                        }
                        modNum = modNumNode.Number;
                    }
                    else if (parameterCount == 6)
                    {
                        var modNumNode = node.Next(6);
                        if (modNumNode is Integer)
                        {
                            modNum = modNumNode.Number;
                        }
                    }

                    // if the modNum wasn't specified then use the room.
                    // if this isn't a room then can't annotate.
                    if (modNum == -1)
                    {
                        if (rooms.ContainsKey(script))
                        {
                            modNum = script.Number;
                        }
                        else if (!roomCallers.TryGetValue(script.Number, out modNum))
                        {
                            continue;
                        }
                    }

                    int noun = nounNode.Number;
                    int verb = verbNode.Number;
                    int cond = condNode.Number;
                    int seq = seqNode.Number;

                    // EcoNarrator:init alters noun and seq when seq >= 100
                    if (seq >= 100)
                    {
                        noun += 1;
                        seq -= 100;
                    }

                    // EcoTalker:say alters cond and seq when seq > 36.
                    // cond becomes either 1 or 2 depending on global 251,
                    // whose value is usually set in room initialization.
                    // if i can parse that out then i use it, otherwise
                    // try both cond values and if there's only one message
                    // then use that, otherwise give up because it's ambiguous.
                    bool ambiguous = false;
                    if (seq > 72)
                    {
                        // cond would be 1 or 2, whichever is opposite of global251
                        if (global251 == 1)
                        {
                            cond = 2;
                        }
                        else if (global251 == 2)
                        {
                            cond = 1;
                        }
                        else
                        {
                            ambiguous = true;
                        }
                        seq -= 72;
                    }
                    else if (seq > 36)
                    {
                        // cond would be 1 or 2, whichever is global251
                        if (global251 == 1 || global251 == 2)
                        {
                            cond = global251;
                        }
                        else
                        {
                            ambiguous = true;
                        }
                        seq -= 36;
                    }

                    Message message;
                    if (!ambiguous)
                    {
                        message = messageFinder.GetFirstMessage(modNum, modNum, noun, verb, cond, seq);
                    }
                    else
                    {
                        var messages = new List<Message>();
                        messages.Add(messageFinder.GetFirstMessage(modNum, modNum, noun, verb, 1, seq, false));
                        messages.Add(messageFinder.GetFirstMessage(modNum, modNum, noun, verb, 2, seq, false));
                        messages.RemoveAll(m => m == null);
                        if (messages.Count == 0)
                        {
                            message = null;
                        }
                        else if (messages.Count == 1)
                        {
                            message = messages[0];
                        }
                        else
                        {
                            // can't figure it out, don't know global251 in this context
                            continue;
                        }
                    }

                    if (message != null)
                    {
                        node.Annotate(message.Text.QuoteMessageText());
                    }
                }
            }
        }

        static Object GetEcoTalker(Game game, Node head, HashSet<Object> ecoTalkers)
        {
            Object ecoTalker = null;
            if (head is Atom)
            {
                ecoTalker = ecoTalkers.FirstOrDefault(e => e.Name == head.Text);
            }
            else if (head is List)
            {
                if (head.Children.Count == 3 &&
                    head.At(0).Text == "ScriptID" &&
                    head.At(1) is Integer &&
                    head.At(2) is Integer)
                {
                    int scriptNumber = head.At(1).Number;
                    int exportNumber = head.At(2).Number;
                    string export = game.GetExport(scriptNumber, exportNumber);
                    var exportScript = game.GetScript(scriptNumber);
                    if (exportScript != null)
                    {
                        var obj = exportScript.Objects.FirstOrDefault(o => o.Name == export);
                        if (obj != null && ecoTalkers.Contains(obj))
                        {
                            ecoTalker = obj;
                        }
                    }
                }
            }
            return ecoTalker;
        }

        static int GetGlobal251(Game game, Script script, IReadOnlyDictionary<Script, string> rooms)
        {
            string roomName;
            if (!rooms.TryGetValue(script, out roomName)) return -1;

            string global251 = game.GetGlobal(251).Name;

            Object room = script.Objects.First(o => o.Name == roomName);
            Function init = room.Methods.FirstOrDefault(m => m.Name == "init");
            if (init == null) return -1;
            foreach (var node in init.Node)
            {
                if (node.At(0).Text == "=" &&
                    node.At(1).Text == global251 &&
                    node.At(2) is Integer)
                {
                    return node.At(2).Number;
                }
            }
            return -1;
        }
    }
}
