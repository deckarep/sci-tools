using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Kq7Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            VerbAnnotator.Run(Game, verbs);
            InventoryAnnotator.Run(Game, items);
            RunLate();

            GlobalEnumAnnotator.Run(Game, 104, new Dictionary<int, string> {
                {-3, "Roz" },
                {-4, "Val" }
            });
        }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
            { 104, "gValOrRoz" }, // -3 is roz, -4 is val
            { 111, "gDebugging" },
            { 122, "gChapter" },

            { 307, "gInventoryCount" },

            { 326, "gGem1Position" },
            { 327, "gGem2Position" },
            { 328, "gGem3Position" },

            { 337, "gRozInventoryIndexes" },
            { 353, "gValInventoryIndexes" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(11, 0), "IsFlag" },
            { Tuple.Create(11, 1), "SetFlag" },
            { Tuple.Create(11, 2), "ClearFlag" },

            // wish i could annotate death message but the param is an int that's used
            // in a huge switch with each case doing different messages, sometimes
            // based on if you're roz or val, and there's no correlation between the
            // message tuple ints and the int parameter. i'd consider making the dang
            // mapping dict myself but the proc doesn't decompile so that's one more obstacle.
            // the death messages are in message 30 fwiw. (i hope that's all of them)
            { Tuple.Create(11, 5), "EgoDead" },

            //{ Tuple.Create(98, 3), "SoundStuff" },
            //{ Tuple.Create(98, 6), "CutSceneComplete" },
            //{ Tuple.Create(98, 9), "DoesCarlosDebugFileExist" }, // does carlos.kq7 exist?
        };

        static Dictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 8, "Do" },
            { 10, "Exit" },

            { 5, "Golden_Comb" },
            { 6, "Ripped_Petticoat" },
            { 11, "Stick" },
            { 12, "Clay_Pot" },
            { 13, "Flag" },
            { 15, "Gourd_Seed" },
            { 16, "Turquoise_Bead" },
            { 17, "Basket" },
            { 18, "Hunting_Horn" },
            { 19, "Glasses" },
            { 20, "Turquoise_Piece_a" },
            { 21, "Jackalope_Fur" },
            { 22, "Turquoise_Piece_b" },
            { 23, "Puzzle" },
            { 24, "Corn_Kernel" },
            { 25, "Rope" },
            { 26, "Bug_Reducing_Powder" },
            { 27, "Salt_Water" },
            { 28, "Fresh_Water" },
            { 30, "Ear_of_Corn" },
            { 32, "Toy_Rat" },
            { 33, "Bowl_a" },
            { 34, "Silver_Spoon" },
            { 35, "Baked_Beetles" },
            { 36, "Dragon_Scale" },
            { 37, "Silver_Pellet" },
            { 38, "Shield" },
            { 39, "Dragon_Toad" },
            { 40, "Enchanted_Rope" },
            { 41, "Wet_Sulfur" },
            { 43, "Lantern" },
            { 44, "Lantern_with_Spark" },
            { 45, "Big_Gem" },
            { 46, "Hammer_and_Chisel" },
            { 50, "Prickly_Pear" },
            { 51, "Salt_Crystals" },
            { 52, "Crook" },
            { 53, "Turquoise_Shape" },
            { 54, "Nectar_in_Pot" },
            { 55, "Feather" },
            { 56, "China_Bird" },
            { 57, "Mask" },
            { 58, "Book" },
            { 59, "Wooden_Nickel" },
            { 60, "Rubber_Chicken" },
            { 61, "Magic_Statue" },
            { 65, "Grave_Digger_s_Horn" },
            { 66, "Back_Bone" },
            { 67, "Weird_Pet" },
            { 68, "Defoliant" },
            { 69, "Magic_Wand" },
            { 70, "Veil" },
            { 71, "Moon" },
            { 72, "Were-beast_Salve" },
            { 73, "Pomegranate" },
            { 74, "Scarab" },
            { 75, "Shovel" },
            { 76, "Ambrosia" },
            { 77, "Extra_Life" },
            { 78, "Grave_Digger_s_Rat" },
            { 79, "Foot-In-A-Bag" },
            { 80, "Fragrant_Flower" },
            { 81, "Dream_Catcher" },
            { 82, "Magic_Bridle" },
            { 83, "Tapestry_of_Dreams" },
            { 84, "Woolen_Stocking" },
            { 85, "Device" },
            { 86, "Sling" },
            { 87, "Crystal_Shaft" },
            { 88, "Golden_Grape" },
            { 89, "Horseman_s_Medal" },
            { 90, "Femur" },
            { 92, "Firecracker" },
            { 93, "Horseman_s_Head" },
            { 94, "Horseman_s_Fife" },
            { 95, "Bowl_b" },
            { 97, "Shield_Spike" },
            { 100, "Shrieking_Horn" },
            { 101, "Ooga_Booga_Flower" },

            // useObj objects scattered in rooms
            { 47, "redGem" },
            { 48, "greenGem" },
            { 49, "yellowGem" },
            { 99, "tongs" },
            { 96, "tongs with mold" },
            { 62, "chair" },
            { 63, "stand" },
            { 64, "stool" },
        };

        static string[] items =
        {
            "Golden_Comb",
            "Ripped_Petticoat",
            "Clay_Pot",
            "Stick",
            "Flag",
            "Turquoise_Piece_a",
            "Gourd_Seed",
            "Basket",
            "Hunting_Horn",
            "Glasses",
            "Jackalope_Fur",
            "Turquoise_Bead",
            "Turquoise_Piece_b",
            "Puzzle",
            "Corn_Kernel",
            "Salt_Water",
            "Fresh_Water",
            "Rope",
            "Bug_Reducing_Powder",
            "Ear_of_Corn",
            "Prickly_Pear",
            "Salt_Crystals",
            "Turquoise_Shape",
            "Toy_Rat",
            "Bowl_a",
            "Silver_Spoon",
            "Baked_Beetles",
            "Dragon_Scale",
            "Silver_Pellet",
            "Shield",
            "Shield_Spike",
            "Dragon_Toad",
            "Enchanted_Rope",
            "Bowl_b",
            "Wet_Sulfur",
            "Lantern",
            "Lantern_with_Spark",
            "Big_Gem",
            "Hammer_and_Chisel",
            "Crook",
            "Nectar_in_Pot",
            "Feather",
            "China_Bird",
            "Mask",
            "Wooden_Nickel",
            "Book",
            "Rubber_Chicken",
            "Magic_Statue",
            "Moon",
            "Grave_Digger_s_Horn",
            "Back_Bone",
            "Weird_Pet",
            "Defoliant",
            "Magic_Wand",
            "Veil",
            "Scarab",
            "Shovel",
            "Grave_Digger_s_Rat",
            "Extra_Life",
            "Foot-In-A-Bag",
            "Fragrant_Flower",
            "Woolen_Stocking",
            "Device",
            "Sling",
            "Golden_Grape",
            "Were-beast_Salve",
            "Pomegranate",
            "Ambrosia",
            "Dream_Catcher",
            "Magic_Bridle",
            "Tapestry_of_Dreams",
            "Crystal_Shaft",
            "Femur",
            "Horseman_s_Medal",
            "Firecracker",
            "Horseman_s_Head",
            "Horseman_s_Fife",
            "Shrieking_Horn",
            "Ooga_Booga_Flower",
        };
    }
}
