using System;
using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // get list of Talkers and their charNum property
    // foreach Talker init: ...
    //   noun: Talker:charNum
    //   verb: first parameter
    // modNum: fourth param OR current room

    // Talker
    //   Narrator [ there's only one, not exported ]

    static class Eco1FloppyMessageAnnotator
    {
        public static void Run(Game game, MessageFinder messageFinder)
        {
            var rooms = game.GetRooms();
            var roomCallers = game.GetUniqueRoomCallers(rooms.Keys);

            // find all public talkers
            var publicTalkers = new Dictionary<Tuple<int, int>, Language.Object>();
            foreach (var script in game.Scripts)
            {
                foreach (var export in script.Exports)
                {
                    var talker = script.Instances.FirstOrDefault(i => i.Name == export.Value &&
                                                                      i.Super == "Talker");
                    if (talker != null)
                    {
                        publicTalkers.Add(Tuple.Create(script.Number, export.Key), talker);
                    }
                }
            }

            // narrator gets referenced by class
            var narratorScript = game.GetScript(928);
            if (narratorScript == null) return; // demo
            var narrator = narratorScript.Classes.FirstOrDefault(c => c.Name == "Narrator");
            if (narrator == null) return;

            // foreach script, get their local talkers,
            // then try to find each talker init.
            var localTalkers = new Dictionary<string, Language.Object>();
            foreach (var script in game.Scripts)
            {
                localTalkers.Clear();
                localTalkers.Add(narrator.Name, narrator);
                foreach (var instance in script.Instances)
                {
                    if (instance.Super == "Talker" || instance.Super == "Narrator")
                    {
                        localTalkers.Add(instance.Name, instance);
                    }
                }

                // check each init:
                foreach (var init in script.Root.Where(n => n.Text == "init:"))
                {
                    var first = init.Parent.At(0);
                    Language.Object talker;
                    if (!localTalkers.TryGetValue(first.Text, out talker))
                    {
                        if (first.At(0).Text == "ScriptID" &&
                            first.At(1) is Integer &&
                            first.At(2) is Integer)
                        {
                            publicTalkers.TryGetValue(Tuple.Create(first.At(1).Number, first.At(2).Number), out talker);
                        }
                    }
                    if (talker == null) continue;

                    int noun = talker.GetIntegerProperty("charNum");
                    bool isNarrator = talker.Name == "Narrator" ||
                                      talker.Super == "Narrator";

                    var verbNode = init.Next(1);
                    if (!(verbNode is Integer)) continue;
                    int verb = verbNode.Number;

                    int modNum;
                    Node modNumNode;
                    if (isNarrator)
                    {
                        modNumNode = init.Next(3);
                        if (init.Next(2).IsSelector())
                        {
                            modNumNode = Node.Nil;
                        }
                    }
                    else
                    {
                        modNumNode = init.Next(4);
                        if (init.Next(2).IsSelector() || init.Next(3).IsSelector())
                        {
                            modNumNode = Node.Nil;
                        }
                    }
                    if (modNumNode is Integer)
                    {
                        modNum = modNumNode.Number;
                    }
                    else if (modNumNode is Nil)
                    {
                        // if the modNum wasn't specified then use the room.
                        // if this isn't a room then can't annotate.
                        if (rooms.ContainsKey(script))
                        {
                            modNum = script.Number;
                        }
                        else if (!roomCallers.TryGetValue(script.Number, out modNum))
                        {
                            continue;
                        }
                    }
                    else continue;

                    var message = messageFinder.GetFirstMessage(modNum, modNum, noun, verb, 0, 1, false);
                    if (message != null)
                    {
                        init.Annotate(message.Text.QuoteMessageText());
                    }
                    else
                    {
                        // huh yeah the room callers thing isn't always working, whatever.
                        // i copied it from CD, it produces warnings too.
                    }
                }
            }
        }
    }
}
