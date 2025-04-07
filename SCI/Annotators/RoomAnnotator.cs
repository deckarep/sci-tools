using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SCI.Language;

namespace SCI.Annotators
{
    static class RoomAnnotator
    {
        public static void Run(Game game)
        {
            var rooms = GetRooms(game);
            RemoveBoringRooms(rooms); // experimental

            HashSet<string> roomSymbols = new HashSet<string>
            {
                game.GetGlobal(11).Name, // current room number
                game.GetGlobal(12).Name, // previous room number
                game.GetGlobal(13).Name, // new room number
                "newRoomNumber",         // newRoom's first parameter
                "roomNum",               // startRoom's first parameter
            };

            foreach (var script in game.Scripts)
            {
                // annotate all integer comparisons/assignments to room symbols
                ConstantFinder.Run(
                    script.Root,
                    n => roomSymbols.Contains(n.Text),
                    n =>
                    {
                        string roomName;
                        if (rooms.TryGetValue(n.Number, out roomName))
                        {
                            n.Annotate(roomName);
                        }
                    });

                // annotate calls to newRoom: integer
                var newRooms = from n in script.Root
                               where n.Text == "newRoom:" &&
                                     n.Next() is Integer
                               select n.Next();
                foreach (var newRoom in newRooms)
                {
                    string roomName;
                    if (rooms.TryGetValue(newRoom.Number, out roomName))
                    {
                        newRoom.Annotate(roomName);
                    }
                }
            }
        }

        static Dictionary<int, string> GetRooms(Game game)
        {
            var rooms = new Dictionary<int, string>();
            var roomScriptNames = game.GetRooms();
            foreach (var roomScriptName in roomScriptNames)
            {
                rooms.Add(roomScriptName.Key.Number, roomScriptName.Value);
            }
            return rooms;
        }

        static Regex BoringRoomRegex = new Regex(@"^r(oo)?m\d+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        static void RemoveBoringRooms(Dictionary<int, string> rooms)
        {
            // Default room names are "rm100" and "Room100" and it's line noise
            // to annotate room number 100 with them. A few are overwriting
            // my existing dagger comments so rather than adjust my code
            // i'd rather just skip this. interesting room names only please.

            foreach (int roomNumber in rooms.Keys.ToList())
            {
                string name = rooms[roomNumber];
                if (BoringRoomRegex.IsMatch(name))
                {
                    rooms.Remove(roomNumber);
                }
            }
        }
    }
}