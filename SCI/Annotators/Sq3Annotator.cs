using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    // if i need to detect astrochicken demo, it has script 890 and no room 1

    class Sq3Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            InventoryAnnotator.Run(Game, items);
            Sci0InventoryAnnotator.Run(Game, items);
            RunLate();

            Sq3DeathAnnotator.Run(Game);
        }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
            { 155, "gHandsOff" }
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "NormalEgo" },
            { Tuple.Create(0, 2), "HandsOff" },
            { Tuple.Create(0, 3), "HandsOn" },
            { Tuple.Create(0, 4), "HaveMem" },
            { Tuple.Create(0, 5), "NotClose" },
            { Tuple.Create(0, 6), "AlreadyTook" },
            { Tuple.Create(0, 7), "SeeNothing" },
            { Tuple.Create(0, 8), "CantDo" },
            { Tuple.Create(0, 9), "DontHave" },
            { Tuple.Create(0, 10), "RedrawCast" },

            { Tuple.Create(0, 12), "cls" },
            { Tuple.Create(0, 13), "IsItemAt" },
            { Tuple.Create(0, 14), "PutItem" },
            { Tuple.Create(0, 15), "NearControl" },

            { Tuple.Create(0, 17), "EgoDead" },
        };

        static string[] items =
        {
            "Glowing_Gem",
            "Wire",
            "Ladder",
            "Reactor",
            "Orat_on_a_Stick",
            "ThermoWeave_Underwear",
            "Astro_Chicken_Flight_Hat",
            "Monolith_Decoder_Ring",
            "Buckazoids",
            "Metal_Pole",
            "Thermal_Detonator",
            "Keycard",
            "Coveralls",
            "Vaporizer",
            "Elmo_s_picture",
            "a_copy_of_Elmo_s_picture",
            "Invisibility_Belt",
            "Bag_of_Fast_Food",
        };
    }
}
