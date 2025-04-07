using SCI.Language;

namespace SCI.Annotators
{
    // proc26_0 [ cond ] [ modNum ]
    // noun: 0
    // verb: 63
    // cond: param1 else 1
    // modNum: param2 else current room. but if argc == 0 then 26

    static class Qfg3DeathAnnotator
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
                        int cond;
                        Node condNode = node.At(1);
                        if (condNode is Integer)
                        {
                            cond = condNode.Number;
                        }
                        else if (condNode is Nil)
                        {
                            cond = 1;
                        }
                        else
                        {
                            continue;
                        }

                        int modNum;
                        Node modNumNode = node.At(2);
                        if (condNode is Nil)
                        {
                            // no parameters passed
                            modNum = 26;
                        }
                        else if (modNumNode is Integer && modNumNode.Number != 0)
                        {
                            // integer passed
                            modNum = modNumNode.Number;
                        }
                        else if (modNumNode is Nil || (modNumNode is Integer && modNumNode.Number == 0))
                        {
                            // no second parameter OR second parameter is zero
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
                            continue;
                        }

                        int noun = 0;
                        int verb = 63;
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
