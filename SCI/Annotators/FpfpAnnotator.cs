using System;
using System.Collections.Generic;
using SCI.Annotators.Original;

namespace SCI.Annotators
{
    class FpFpAnnotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);

            if (Game.GetExport(0, 12) == "fpApproachCode")
            {
                // floppy demo
                VerbAnnotator.Run(Game, floppyDemoVerbs);
                InventoryAnnotator.Run(Game, floppyDemoItems);
            }
            else if (Game.GetExport(0, 8) == "fpWin")
            {
                // cd demo
                VerbAnnotator.Run(Game, cdDemoVerbs);
                InventoryAnnotator.Run(Game, cdDemoItems);
            }
            else
            {
                // full (floppy or cd)
                VerbAnnotator.Run(Game, verbs);
                InventoryAnnotator.Run(Game, items);
            }

            RunLate();
        }

        protected override Dictionary<int, Original.Script[]> GameHeaders { get { return FpFpSymbols.Headers; } }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
            { 119, "gCurPuzzle" },
            { 120, "gAct" },
        };

        // the other exported procs that differ between versions, but i haven't
        // tried naming them yet so i haven't had to differentiate
        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            // same in all versions
            { Tuple.Create(0, 2), "IsFlag" },
            { Tuple.Create(0, 3), "SetFlag" },
            { Tuple.Create(0, 4), "ClearFlag" },
            { Tuple.Create(0, 5), "Face" },

            // only in full versions
            { Tuple.Create(0, 11), "SetScore" },
        };

        static IReadOnlyDictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 1, "Look" },
            { 4, "Do" },
            { 2, "Talk" },

            { 8,"icon10" }, // used for several scene-specific things

            { 5, "Elixir" },
            { 9, "Door_Key" },
            { 10, "Penelope_s_Rx" },
            { 11, "Med_1" },
            { 12, "Helen_s_Rx" },
            { 13, "Med_2" },
            { 14, "Madame_s_Rx" },
            { 15, "Shot_Glass" },
            { 16, "Under_Glass" },
            { 17, "Correct_Rx" },
            { 18, "Med_3" },
            { 20, "Prep_G" },
            { 23, "Tower_H20" },
            { 24, "Tin_Can" },
            { 25, "Ice_Pick" },
            { 26, "Charcoal" },
            { 27, "Leather_Strap" },
            { 28, "Gas_Mask" },
            { 29, "Deflatulizer" },
            { 30, "Snails" },
            { 31, "Money" },
            { 32, "Beer" },
            { 33, "Open_Beer" },
            { 34, "Empty_Bottles" },
            { 35, "Church_Key" },
            { 36, "Ladder" },
            { 37, "Rope" },
            { 38, "Lasso" },
            { 39, "Pure_Solution" },
            { 40, "Nitrous_Oxide" },
            { 41, "Baking_Soda" },
            { 42, "Post_Cards" },
            { 43, "Candle_Wax" },
            { 44, "Knife" },
            { 45, "Desk_Key" },
            { 46, "Deposit_Key" },
            { 47, "Pistols" },
            { 48, "Pie" },
            { 49, "Coffee" },
            { 50, "Cleaning_Kit" },
            { 51, "Bullets" },
            { 52, "Clay" },
            { 53, "Medallion" },
            { 55, "Silver_Ear" },
            { 56, "Clothes" },
            { 57, "Claim_Check" },
            { 58, "Boots" },
            { 59, "Neckerchief" },
            { 60, "Sharp_Ear" },
            { 61, "Sword" },
            { 62, "Letter" },
            { 63, "Shovel" },
            { 64, "Filled_Sack" },
            { 65, "Paper_Sack" },
            { 66, "Horse_Plop" },
            { 67, "Silver_Filled_Mold" },
            { 68, "Wax_Filled_Mold" },
            { 69, "Empty_Mold" },
            { 70, "Wax_Ear" },
            { 71, "Incorrect_Med" },
            { 83, "Incorrect_Med2" },
        };

        static string[] items =
        {
            "Baking_Soda",
            "Beer",
            "Boots",
            "Bullets",
            "Candle_Wax",
            "Charcoal",
            "Church_Key",
            "Claim_Check",
            "Clay",
            "Cleaning_Kit",
            "Clothes",
            "Coffee",
            "Correct_Rx",
            "Deflatulizer",
            "Deposit_Key",
            "Desk_Key",
            "Door_Key",
            "Elixir",
            "Empty_Bottles",
            "Empty_Mold",
            "Filled_Sack",
            "Gas_Mask",
            "Helen_s_Rx",
            "Horse_Plop",
            "Ice_Pick",
            "Incorrect_Med",
            "Incorrect_Med2",
            "Knife",
            "Ladder",
            "Lasso",
            "Leather_Strap",
            "Letter",
            "Madame_s_Rx",
            "Medallion",
            "Med_1",
            "Med_2",
            "Med_3",
            "Money",
            "Neckerchief",
            "Nitrous_Oxide",
            "Open_Beer",
            "Paper_Sack",
            "Penelope_s_Rx",
            "Pie",
            "Pistols",
            "Post_Cards",
            "Prep_G",
            "Pure_Solution",
            "Rope",
            "Sharp_Ear",
            "Shot_Glass",
            "Shovel",
            "Silver_Ear",
            "Silver_Filled_Mold",
            "Snails",
            "Sword",
            "Tin_Can",
            "Tower_H20",
            "Under_Glass",
            "Wax_Ear",
            "Wax_Filled_Mold",
            "invLook",
            "invHand",
            "invSelect",
            "invHelp",
            "invMore",
            "ok",
        };

        static string[] floppyDemoItems =
        {
            "Empty_Vial",
            "Full_Vial",
            "Beer_Bottle",
            "Empty_Bottle",
            "Full_Bottle",
            "Charcoal",
            "Saltpeter",
            "Full_Saltpeter",
            "Tin_Can",
            "Empty_Cup",
            "Full_Cup",
            "Mole",
            "Prescription",
            "Fuse",
            "Matches",
            "Unlit_Bomb",
            "Bomb",
        };

        static IReadOnlyDictionary<int, string> floppyDemoVerbs = new Dictionary<int, string>
        {
            { 1, "Look" },
            { 4, "Do" },
            { 2, "Talk" },

            { 9, "Mole" },
            { 10, "Full_Bottle" },
            { 11, "Beer_Bottle" },
            { 12, "Full_Cup" },
            { 13, "Empty_Vial" },
            { 14, "Full_Vial" },
            { 15, "Empty_Bottle" },
            { 16, "Empty_Cup" },
            { 17, "Prescription" },
            { 18, "Fuse" },
            { 19, "Matches" },
            { 20, "Saltpeter" },
            { 21, "Bomb" },
            { 22, "Full_Saltpeter" },
            { 23, "Unlit_Bomb" },
            { 24, "Tin_Can" },
            { 26, "Charcoal" },
        };

        static IReadOnlyDictionary<int, string> cdDemoVerbs = new Dictionary<int, string>
        {
            { 1, "Look" },
            { 4, "Do" },
            { 2, "Talk" },

            { 31, "Money" },
            { 32, "Beer" },
            { 33, "Open_Beer" },
            { 36, "Ladder" },
        };

        static string[] cdDemoItems =
        {
            "Beer",
            "Ladder",
            "Money",
            "Open_Beer",
        };
    }
}
