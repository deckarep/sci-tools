using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Phant1Annotator : GameAnnotator
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

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
            { 100, "gDebugging" },
            { 106, "gChapter" },
            { 178, "gVideoSpeed" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "SetFlag" },
            { Tuple.Create(0, 2), "ClearFlag" },
            { Tuple.Create(0, 3), "IsFlag" },

            { Tuple.Create(1111, 6), "KillRobot" },
            { Tuple.Create(1111, 7), "DoRobot" },
        };

        static IReadOnlyDictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 3, "Move" },
            { 4, "Do" },
            { 21, "Exit" },

            { 6, "invMoney" },
            { 7, "invNail" },
            { 8, "invLibKey" },
            { 9, "invNewspaper" },
            { 10, "invPoker" },
            { 11, "invHammer" },
            { 12, "invStairKey" },
            { 13, "invVampBook" },
            { 14, "invMatch" },
            { 15, "invTarot" },
            { 16, "invBrooch" },
            { 17, "invPhoto" },
            { 18, "invLensPiece" },
            { 19, "invDrainCln" },
            { 20, "invCrucifix" },
            { 22, "invBeads" },
            { 23, "invSpellBook" },
            { 25, "invXmasOrn" },
            { 26, "invStone" },
            { 27, "invCutter" },
            { 28, "invDogBone" },
            { 34, "invFigurine" },
            { 36, "invPitchfork" },
        };

        static string[] items =
        {
            "invLibKey",
            "invMoney",
            "invNail",
            "invNewspaper",
            "invPoker",
            "invHammer",
            "invStairKey",
            "invVampBook",
            "invMatch",
            "invTarot",
            "invBrooch",
            "invPhoto",
            "invLensPiece",
            "invDrainCln",
            "invCrucifix",
            "invBeads",
            "invSpellBook",
            "invXmasOrn",
            "invStone",
            "invCutter",
            "invDogBone",
            "invFigurine",
        };
    }
}