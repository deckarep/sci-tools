using SCI.Language;

namespace SCI.Annotators
{
    // proc0_26 [ cond ] [ modNum ]
    // noun: 0
    // verb: 100
    // cond: param1 or 1
    // modNum: param2 or current room or 26

    static class Qfg4DeathAnnotator
    {
        public static void Run(Game game, MessageFinder messageFinder)
        {
            string deathFunction = game.GetExport(26, 0);

            // get rooms and unique callers so we can figure out current room
            var rooms = game.GetRooms();
            var roomCallers = game.GetUniqueRoomCallers(rooms.Keys);

            foreach (var script in game.Scripts)
            {
                foreach (var node in script.Root)
                {
                    if (node.At(0).Text == deathFunction)
                    {
                        int paramCount = node.Children.Count - 1;

                        int cond;
                        int modNum;
                        if (paramCount > 0)
                        {
                            Node condNode = node.At(1);
                            if (condNode is Integer)
                            {
                                cond = condNode.Number;
                            }
                            else
                            {
                                // non-integer parameter, skip
                                continue;
                            }

                            Node modNumNode = node.At(2);
                            if (modNumNode is Integer)
                            {
                                modNum = modNumNode.Number;
                            }
                            else if (modNumNode is Nil)
                            {
                                // no second parameter passed, just first
                                if (rooms.ContainsKey(script))
                                {
                                    modNum = script.Number;
                                }
                                else if (!roomCallers.TryGetValue(script.Number, out modNum))
                                {
                                    // can't figure out current room, skip
                                    continue;
                                }
                            }
                            else
                            {
                                // non-integer parameter, skip
                                continue;
                            }
                        }
                        else
                        {
                            // no parameters passed
                            cond = 1;
                            modNum = 26;
                        }

                        int noun = 0;
                        int verb = 100;
                        var message = messageFinder.GetFirstMessage(modNum, modNum, noun, verb, cond);
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
