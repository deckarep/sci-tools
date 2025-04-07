using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Pq1Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            VerbAnnotator.Run(Game, verbs, ArrayToDictionary(0, inventoryVerbs));
            InventoryAnnotator.Run(Game, inventoryVerbs);
            RunLate();
        }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "NormalEgo" },
            { Tuple.Create(0, 2), "HandsOff" },
            { Tuple.Create(0, 3), "HandsOn" },
            { Tuple.Create(0, 4), "HaveMem" },
            { Tuple.Create(0, 5), "EgoDead" },
            { Tuple.Create(0, 6), "SetScore" },

            { Tuple.Create(0, 9), "SetFlag" },
            { Tuple.Create(0, 10), "IsFlag" },
            { Tuple.Create(0, 11), "ClearFlag" },

            { Tuple.Create(0, 12), "Face" },
        };

        static IReadOnlyDictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 3, "Walk" },
            { 1, "Look" },
            { 4, "Do" },
            { 2, "Talk" },

            { 9, "Service_Revolver" },
            { 10, "Pen" },
            { 11, "Extender" },
            { 12, "Patrol_Car_Keys" },
            { 13, "Bullets" },
            { 14, "Ticket_Book" },
            { 16, "Night_Stick" },
            { 17, "Undercover_money" },
            { 18, "Hoffman_s_License" },
            { 19, "Ticket" },
            { 21, "Handcuffs" },
            { 22, "Camaro_Keys" },
            { 23, "Undercover_Car_Keys" },
            { 26, "No_Bail_Warrant" },
            { 27, "Hoffman_File" },
            { 28, "Disguise" },
            { 29, "Hair_Dye" },
            { 49, "Deluxe_Transmitter_Pen" },
            { 51, "Wanted_Poster" },
            { 54, "Hotel_Room_Key" },
        };

        static string[] inventoryVerbs =
        {
            "Service_Revolver",
            "Pen",
            "Extender",
            "Patrol_Car_Keys",
            "Bullets",
            "Ticket_Book",
            "Night_Stick",
            "Undercover_money",
            "Ticket",
            "Handcuffs",
            "Camaro_Keys",
            "Undercover_Car_Keys",
            "No_Bail_Warrant",
            "Hoffman_File",
            "Disguise",
            "Hair_Dye",
            "Wanted_Poster",
            "Hoffman_s_License",
            "Hotel_Room_Key",
            "Deluxe_Transmitter_Pen",
        };
    }
}
