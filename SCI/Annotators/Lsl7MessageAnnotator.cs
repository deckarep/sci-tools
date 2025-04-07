using SCI.Language;

namespace SCI.Annotators
{
    // Annotator for the "MakeMessageText" proc. Name revealed by debug strings.
    //
    // proc64896_5 [ noun verb cond seq mod [i-dont-care] ]

    static class Lsl7MessageAnnotator
    {
        public static void Run(Game game, MessageFinder messageFinder)
        {
            string messageFunction = game.GetExport(64896, 5);

            // get rooms and unique callers so we can figure out current room
            var rooms = game.GetRooms();
            var roomCallers = game.GetUniqueRoomCallers(rooms.Keys);

            foreach (var script in game.Scripts)
            {
                foreach (var node in script.Root)
                {
                    if (node.At(0).Text == messageFunction)
                    {
                        int noun = 0;
                        Node nounNode = node.At(1);
                        if (nounNode is Integer)
                        {
                            noun = nounNode.Number;
                        }
                        else if (!(nounNode is Nil))
                        {
                            continue;
                        }

                        int verb = 0;
                        Node verbNode = node.At(2);
                        if (verbNode is Integer)
                        {
                            verb = verbNode.Number;
                        }
                        else if (!(verbNode is Nil))
                        {
                            continue;
                        }

                        int cond = 0;
                        Node condNode = node.At(3);
                        if (condNode is Integer)
                        {
                            cond = condNode.Number;
                        }
                        else if (!(condNode is Nil))
                        {
                            continue;
                        }

                        int seq = 1;
                        Node seqNode = node.At(4);
                        if (seqNode is Integer)
                        {
                            seq = seqNode.Number;
                        }
                        else if (!(seqNode is Nil))
                        {
                            continue;
                        }

                        int modNum;
                        Node modNumNode = node.At(5);
                        if (modNumNode is Integer)
                        {
                            modNum = modNumNode.Number;
                        }
                        else if (modNumNode is Nil)
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
                        else
                        {
                            // can't do anything with a variable
                            continue;
                        }

                        var message = messageFinder.GetFirstMessage(modNum, modNum, noun, verb, cond, seq);
                        if (message != null)
                        {
                            node.At(0).Annotate(message.Text.QuoteMessageText());
                        }
                    }
                }
            }
        }
    }
}
