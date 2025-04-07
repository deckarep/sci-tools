using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // annotates room transition functions and room properties.
    // allow boring room names on this one.
    //
    // room transitions via:
    //   (proc11_7 ___ ___ [roomNumber]
    //
    // room properties:
    // (instance neuSaangerPic2 of ExitRoom
    //    (properties
    //       picture 9741
    //       east 9742 ; neuSaangerPic3
    //       south 970 ; rm970
    //       west 9740 ; neuSaangerPic1
    //)
    static class Gk2RoomAnnotator
    {
        public static void Run(Game game)
        {
            // build dictionary of script numbers to room names
            var roomNames = new Dictionary<int, string>();
            foreach (var room in game.GetRooms())
            {
                roomNames.Add(room.Key.Number, room.Value);
            }

            // annotate calls to proc11_7 that include a room number
            var procName = game.GetExport(11, 7);
            foreach (var node in game.Scripts.SelectMany(s => s.Root))
            {
                if (node.At(0).Text == procName &&
                    node.At(3) is Integer &&
                    node.At(3).Number != -1) // -1 is passed when not changing rooms
                {
                    Annotate(node.At(3), roomNames);
                }
            }

            // annotate properties
            string[] propertyNames = { "north", "south", "east", "west", "nextRoomNum" };
            var properties = from s in game.Scripts
                             from o in s.Objects
                             from p in o.Properties
                             select p;
            foreach (var property in properties)
            {
                if (propertyNames.Contains(property.Name) &&
                    property.ValueNode is Integer &&
                    property.ValueNode.Number > 0) // ignore 0
                {
                    Annotate(property.ValueNode, roomNames);
                }
            }
        }

        static void Annotate(Node node, IReadOnlyDictionary<int, string> roomNames)
        {
            string roomName;
            if (!roomNames.TryGetValue(node.Number, out roomName))
            {
                roomName = "MISSING ROOM";
            }
            node.Annotate(roomName);
        }
    }
}
