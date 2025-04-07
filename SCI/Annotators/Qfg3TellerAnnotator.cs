using System;
using System.Linq;
using SCI.Language;

// QFG3 tellers (class Teller) are initialized with a client and
// pointers to local arrays filled with their options.
//
// I care about annotating teller prompts in two methods:
//
// - showDialog, which contains the very interesting logic about
//   which options are available.
// - doChild, which does custom things based on the prompt.
//
// For most messages, Teller handles mapping the prompt to the
// response. I don't care about the response, there's no lines
// to annotate that with. (Unlike QFG4)
//
// To get the prompt message for a teller...
// Noun: Teller:client's noun property.
// Verb: 2
// Cond: The number from showDialog or doChild
// Seq: 1, except for welcomeTell in room 300, as it overrides getSeqNumber
// ModNum: Current room number
//
// Almost every teller is in a room script. The few that aren't are
// only used in one room so I've just hard-coded those.
namespace SCI.Annotators
{
    static class Qfg3TellerAnnotator
    {
        public static void Run(Game game, MessageFinder messageFinder)
        {
            var tellers = game.GetObjectsBySuper("Teller");
            tellers.RemoveWhere(t => t.Name == "Teller");
            foreach (var teller in tellers)
            {
                // almost every teller is in their room script except these guys
                int roomNumber = teller.Script.Number;
                if (roomNumber == 402) roomNumber = 400;
                if (roomNumber == 701) roomNumber = 700;
                if (roomNumber == 702) roomNumber = 700;
                var room = game.GetScript(roomNumber);

                // TODO: gather all nouns and try them all until one works during annotation
                int clientNoun = GetTellerClientNoun(teller, room, game);
                if (clientNoun == -1)
                {
                    continue;
                }

                foreach (var method in teller.Methods)
                {
                    if (method.Name == "showDialog" || method.Name == "cue")
                    {
                        foreach (var node in method.Node)
                        {
                            if (node.Text == "showDialog:")
                            {
                                for (var cond = node.Next(); !(cond is Nil); cond = cond.Next(2))
                                {
                                    AnnotateCond(cond, roomNumber, clientNoun, messageFinder);
                                }
                            }
                        }
                    }
                    else if (method.Name == "doChild")
                    {
                        // if there's a parameter to doChild, use that, otherwise "query" property
                        string query = method.Parameters.Any() ? method.Parameters[0].Name : "query";
                        ConstantFinder.Run(
                            method.Node,
                            n => n.Text == query,
                            n => AnnotateCond(n, roomNumber, clientNoun, messageFinder));
                    }
                }
            }
        }

        static int GetTellerClientNoun(Language.Object teller, Script room, Game game)
        {
            // a few exceptions that are weird and not worth coding for
            if (teller.Name == "leaderTell" && room.Number == 620) return 1;
            if (teller.Name == "shamanTell" && room.Number == 620) return 3;
            if (teller.Name == "leopManTell" && room.Number == 620) return 4;
            if (teller.Name == "egoTell" && room.Number == 700) return 2;
            if (teller.Name == "johariTell" && room.Number == 700) return 3;

            // try to find the client by looking for the teller init in its
            // own script, and if that doesn't work, then in its room script.
            var client = GetTellerClient(teller, teller.Script, game);
            if (client == null && teller.Script != room)
            {
                client = GetTellerClient(teller, room, game);
            }
            if (client == null)
            {
                return -1;
            }

            // default noun is zero
            int noun = client.GetIntegerProperty("noun");
            return noun >= 0 ? noun : 0;
        }

        static Language.Object GetTellerClient(Language.Object teller, Script script, Game game)
        {
            int exportNumber = -1;
            foreach (var export in teller.Script.Exports)
            {
                if (export.Value == teller.Name)
                {
                    exportNumber = export.Key;
                    break;
                }
            }

            // look for (teller ... init: client)
            // "teller" can be local or (ScriptID __ __)
            Node clientNode = Node.Nil;
            foreach (var node in script.Root)
            {
                if (node.Text == "init:")
                {
                    if (node.Parent.At(0).Text == teller.Name)
                    {
                        clientNode = node.Next(1);
                        break;
                    }

                    var scriptId = node.Parent.At(0);
                    if (scriptId.At(0).Text == "ScriptID" &&
                        (scriptId.At(1) is Integer && scriptId.At(1).Number == teller.Script.Number) &&
                        (scriptId.At(2) is Integer && scriptId.At(2).Number == exportNumber))
                    {
                        clientNode = node.Next(1);
                        break;
                    }
                }
            }

            if (clientNode.Text == game.GetGlobal(0).Name)
            {
                return game.GetExportedObject(28, 0); // hero
            }
            else if (clientNode is Atom)
            {
                return script.Instances.FirstOrDefault(i => i.Name == clientNode.Text);
            }
            else if (clientNode.At(0).Text == "ScriptID" &&
                     clientNode.At(1) is Integer &&
                     clientNode.At(2) is Integer)
            {
                return game.GetExportedObject(clientNode.At(1).Number, clientNode.At(2).Number);
            }
            return null;
        }

        static void AnnotateCond(Node cond, int roomNumber, int noun, MessageFinder messageFinder)
        {
            if (!(cond is Integer)) return;
            var message = messageFinder.GetFirstMessage(roomNumber, roomNumber, noun, 2, Math.Abs(cond.Number), 1);
            if (message != null)
            {
                cond.Annotate(message.Text.QuoteMessageText());
            }
        }
    }
}
