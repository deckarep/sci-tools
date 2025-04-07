using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class PepperAnnotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            VerbAnnotator.Run(Game, verbs);
            InventoryAnnotator.Run(Game, items);
            RunLate();
        }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
            { 193, "gAct" },
            { 215, "gDictionaryWord" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 2), "HaveMem" },
            { Tuple.Create(0, 4), "SetFlag" },
            { Tuple.Create(0, 5), "IsFlag" },
            { Tuple.Create(0, 6), "ClearFlag" },
            { Tuple.Create(0, 7), "Face" },
        };

        static Dictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 3, "Walk" },
            { 6, "Look" },
            { 7, "Do" },
            { 30, "Paw" },
            { 83, "Help" },
            { 84, "Trivia" },
            { 85, "Talk" },
            { 86, "Nose" },
            { 89, "Teeth" },
            { 128, "Exit" },

            // dynamic stuff, some of which is buggy
            { 49,  "Glass_Jar [empty]" },
            { 53,  "Glass_Jar [tin]" },
            { 144, "Glass_Jar [tin+water]" },
            { 145, "Glass_Jar [tin+water+stopper]" },
            { 54,  "Glass_Jar [tin+water+stopper+rod]" },
            { 55,  "Glass_Jar [Leyden]" },
            { 125, "Glass_Jar [BUGGY]" }, // unused, phew

            { 68, "Carpet_Bag [closed]" },
            { 69, "Carpet_Bag [opened]" },
            { 109, "Notepad [unsketched]" }, // yes, there is also a Notepad verb
            { 110, "Notepad [sketched]" },
            // sigh and there's some for the three woodcuttings
            // and of course a lot for jar

            { 9, "Hard_Gum" },
            { 10, "Soft_Gum" },
            { 17, "Dog_Harness" },
            { 20, "Canvas" },
            { 21, "Hammer" },
            { 23, "Herb_Cluster" },
            { 24, "Herb_Packet" },
            { 25, "Bone or invDoggieBone" },
            { 26, "Boy_s_Clothes" },
            { 27, "Baby_s_Clothes" },
            { 32, "Ima_s_Room_Key" },
            { 34, "Fleas" },
            { 36, "Package" },
            { 39, "Doctrine" },
            { 40, "Schematic" },
            { 41, "Tomato" },
            { 42, "invPicnicCloth" },
            { 43, "invBensClothes" },
            { 44, "Kite" },
            { 45, "Ordinary_String" },
            { 46, "Kite_String" },
            { 47, "Metal_Rod" },
            { 48, "Recipe_Card" },
            { 50, "Tin" },
            { 51, "Stopper" },
            { 52, "Glass_Jar" },
            { 56, "A_Brass_Key" },
            { 60, "Back_Scratcher" },
            { 61, "Paddles" },
            { 62, "Press_Lever" },
            { 63, "StepStool" },
            { 64, "King_s_Letter" },
            { 65, "Puzzle_Box" },
            { 67, "Love_Letters" },
            { 70, "Special_Edition" },
            { 71, "Magnet" },
            { 72, "invBifocals" },
            { 73, "Stick" },
            { 74, "Chocolate" },
            { 75, "Bonbons" },
            { 76, "Notepad" },
            { 78, "Pencil" },
            { 79, "Bag_Of_Money" },
            { 80, "Glass" },
            { 92, "Dipper" },
            { 95, "Wood_Cutting_a or Wood_Cutting_b or Wood_Cutting_c" },
            { 96, "Pebbles" },
            { 103, "Drain_Pipe" },
            { 108, "Tub_Fan" },
            { 111, "A_Shilling" },
            { 114, "outfitI" },
            { 115, "invIron" },
            { 123, "Bag_of_Marbles" },
            { 124, "Bens_Key" },
            { 152, "invPotHolder" },
            { 154, "Nails" },
            { 156, "Tools" },
        };

        static string[] items =
        {
            "Dog_Harness",
            "Herb_Cluster",
            "Herb_Packet",
            "Baby_Swaddling",
            "Pebbles",
            "Bag_of_Marbles",
            "A_Shilling",
            "Ima_s_Room_Key",
            "Fleas",
            "Package",
            "Doctrine",
            "Schematic",
            "Tomato",
            "Kite",
            "Ordinary_String",
            "Kite_String",
            "Metal_Rod",
            "Recipe_Card",
            "Glass_Jar",
            "Tin",
            "Stopper",
            "A_Brass_Key",
            "Wood_Cutting_a",
            "Back_Scratcher",
            "Tub_Fan",
            "Paddles",
            "Press_Lever",
            "StepStool",
            "King_s_Letter",
            "Puzzle_Box",
            "Love_Letters",
            "Carpet_Bag",
            "Special_Edition",
            "Magnet",
            "Stick",
            "Chocolate",
            "Bonbons",
            "Notepad",
            "Pencil",
            "Bag_Of_Money",
            "Bens_Key",
            "Glass",
            "outfitI",
            "Wood_Cutting_b",
            "Wood_Cutting_c",
        };
    }
}
