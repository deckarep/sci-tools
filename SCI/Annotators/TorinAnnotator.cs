using System;
using System.Collections.Generic;
using System.Linq;
using SCI.Annotators.Original;
using SCI.Language;

namespace SCI.Annotators
{
    class TorinAnnotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            VerbAnnotator.Run(Game, verbs);
            TorinInventoryAnnotator();
            RunLate();
        }

        protected override Dictionary<int, Original.Script[]> GameHeaders { get { return TorinSymbols.Headers; } }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
            { 201, "goConsolePattern" },
            { 202, "gnChapter" },
            { 203, "goMusic1" },
            { 204, "goSound1" },
            { 205, "gnEgoScrollBorderX" },
            { 206, "gnEgoScrollBorderY" },
            { 207, "gTileDirections" },
            { 216, "gTilePositions" },
            { 224, "gPlaceholder" },
            { 225, "gnSnailTalk" },
            { 226, "gnHermanTalker" },
            { 227, "gMusicVol" },
            { 228, "gSFXVol" },
            { 229, "gAudioVol" },
            { 230, "gZaxTalk" },
            { 231, "gnPlantTalk" },
            { 232, "gnTreeTalk" },
            { 233, "gnRabbitTalker" },
            { 234, "goCrystX" },
            { 235, "goCrystY" },
            { 236, "goCrystZ" },
            { 237, "gnCentipedeTalker" },
            { 238, "gnCursorSaveX" },
            { 239, "gnCursorSaveY" },
            { 240, "gtTorin" },
            { 241, "gtArchivist" },
            { 242, "gbTeleport" },
            { 243, "gbDebugTeleport" },
            { 244, "gnGameSpeed" },
            { 245, "gtSmetana" },
            { 246, "gbMovedCursor" },
            { 247, "gtLeenah" },
            { 248, "gbCatapultEastSide" },
            { 249, "gnBallsInCatapult" },
            { 250, "gnBallsInSeeSawLeft" },
            { 251, "gnBallsInSeeSawRight" },
            { 252, "gPrismArray1" },
            { 253, "gPrismArray2" },
            { 254, "gPrismArray3" },
            { 255, "gPrismArray4" },
            { 256, "gPrismArray5" },
            { 257, "gPrismArray6" },
            { 258, "gPrismArray7" },
            { 259, "gnLastHelpLevel" },
            { 260, "gnLastHelpCase" },
            { 261, "gnLastHelpSeq" },
            { 262, "gnVulturesTalk" },
            { 263, "gnVederTalk" },
            { 264, "gnSkunkTalk" },
            { 265, "gnInvHandler" },
            { 266, "gtViscera" },
            { 267, "gtTripe" },
            { 268, "gnDialogFont" },
            { 269, "gnDialogLeading" },
            { 270, "gnButtonFont" },
            { 271, "gnButtonLeading" },
            { 272, "gnButtonUpColor" },
            { 273, "gnButtonDownColor" },
            { 274, "gnTextColor" },
            { 275, "gvDialogTile" },
            { 276, "gvButtonUpTile" },
            { 277, "gvButtonDownTile" },
            { 288, "goDismissString" },
            { 289, "gnMrsBitterTalk" },
            { 290, "gtSam" },
            { 291, "gtMax" },
            { 292, "gtVeder" },
            { 293, "gnKingTalk" },
            { 294, "gnVideoSpeed" },
            { 295, "gnCPUSpeed" },
            { 296, "gbTipOfTheDay" },
            { 297, "gtKurtzwell" },
            { 298, "gtSoldier" },
            { 299, "gtPecand" },
            { 300, "gtLycentia" },
            { 301, "gtDreep" },
            { 302, "gnSpeedPosX" },
            { 303, "gnSpeedPosY" },
            { 304, "gnVolumePosX" },
            { 305, "gnVolumePosY" },
            { 306, "gnHintTimerPosX" },
            { 307, "gnHintTimerPosY" },
            { 308, "gnHintTime" },
            { 309, "gnHintElapsed" },
            { 310, "gnHintTickCounter" },
            { 311, "gtCop" },
            { 312, "gtArcher" },
            { 313, "gtCarpenter" },
            { 314, "gnNextTip" },
            { 315, "gbInterfaceInitted" },
            { 316, "gtMrsBitter" },
            { 317, "gtBobbyBitter" },
            { 318, "gtKing" },
            { 319, "gtQueen" },
            { 320, "gtQueenToKing" },
            { 321, "gtKingDi" },
            { 322, "gtPhace" },
            { 323, "gtZippy" },
            { 324, "gtTree" },
            { 325, "gnLanguage" },
            { 326, "gbUnflattenBoogle" },
            { 327, "gnBoogleInBagMsg" },
            { 328, "gbScrollInited" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
        };

        static IReadOnlyDictionary<int, string> verbs = new Dictionary<int, string>
        {
            {  1, "Do" },
            {  5, "Pot" },
            {  6, "Help" },
            { 19, "Take" },

            { 2, "ioAx" },
            { 3, "ioPouch" },
            { 4, "ioRope" },
            { 8, "ioChuckBerries" },
            { 9, "ioSquareRoot" },
            { 10, "ioSnails" },
            { 11, "ioMoatScum" },
            { 12, "ioShard" },
            { 13, "ioEressdy" },
            { 15, "ioBoogleBox" },
            { 18, "ioInchworm" },
            { 20, "ioSlugs" },
            { 21, "ioPeat" },
            { 22, "ioConsoleShard" },
            { 23, "ioLeaf" },
            { 24, "ioGuillotineTile" },
            { 25, "ioStepTile" },
            { 26, "ioTrivetTile" },
            { 27, "ioSmallDoorTile" },
            { 28, "ioTableTopTile" },
            { 29, "ioWarningTile" },
            { 30, "ioTubTile" },
            { 31, "ioSeatTile" },
            { 32, "ioFloorTile" },
            { 33, "ioClothespin" },
            { 34, "ioBallInvite" },
            { 35, "ioBeestLeg" },
            { 36, "ioDragonPoo" },
            { 37, "ioHaremPillow" },
            { 38, "ioRedCarpet" },
            { 39, "ioStinkyCarpet" },
            { 40, "ioFan" },
            { 41, "ioLocket" },
            { 43, "ioBoogleWorm" },
            { 44, "ioBoogleShovel" },
            { 45, "ioBoogleLantern" },
            { 46, "ioBoogleYoYo" },
            { 48, "ioCleanTile" },
            { 49, "ioOpenAmmonia" },
            { 50, "ioCannonball1-6" },
            { 51, "ioKnife" },
            { 52, "ioDawburr" },
            { 53, "ioSappyDawburr" },
            { 54, "ioSilkWorms" },
            { 55, "ioPlaybill" },
            { 56, "ioSilkHanky" },
            { 57, "ioTopHat" },
            { 58, "ioRabbit" },
            { 59, "ioCane" },
            { 60, "ioWand" },
            { 61, "ioMagicTrick" },
            { 62, "ioMagicBook" },
            { 63, "ioBagpipes" },
            { 64, "ioCrystcorder" },
            { 65, "ioAudcryst" },
            { 66, "ioShatteredShard" },
            { 67, "ioSaw" },
            { 68, "ioBow" },
            { 69, "ioRosinedBow" },
            { 70, "ioRosin" },
            { 71, "ioBoogleRedCross" },
            { 73, "ioOpenLocket" },
            { 74, "ioWrench" },
            { 76, "ioAmmonia" },
        };

        static string[] torinItems =
        {
            "ioAx",
            "ioRope",
            "ioPouch",
            "ioInchworm",
            "ioChuckBerries",
            "ioSquareRoot",
            "ioSnails",
            "ioMoatScum",
            "ioShard",
            "ioEressdy",
            "ioSlugs",
            "ioPeat",
            "ioConsoleShard",
            "ioLeaf",
            "ioGuillotineTile",
            "ioStepTile",
            "ioTrivetTile",
            "ioSmallDoorTile",
            "ioTableTopTile",
            "ioWarningTile",
            "ioTubTile",
            "ioSeatTile",
            "ioFloorTile",
            "ioClothespin",
            "ioBallInvite",
            "ioBeestLeg",
            "ioDragonPoo",
            "ioHaremPillow",
            "ioRedCarpet",
            "ioStinkyCarpet",
            "ioFan",
            "ioLocket",
            "ioOpenLocket",
            "ioCleanTile",
            "ioKnife",
            "ioAmmonia",
            "ioOpenAmmonia",
            "ioWrench",
            "ioCannonball1",
            "ioCannonball2",
            "ioCannonball3",
            "ioCannonball4",
            "ioCannonball5",
            "ioCannonball6",
            "ioSilkWorms",
            "ioPlaybill",
            "ioBagpipes",
            "ioTopHat",
            "ioRabbit",
            "ioCane",
            "ioWand",
            "ioMagicBook",
            "ioCrystcorder",
            "ioAudcryst",
            "ioShatteredShard",
            "ioSaw",
            "ioBow",
            "ioRosin",
            "ioSilkHanky",
            "ioMagicTrick",
            "ioRosinedBow",
            "ioDawburr",
            "ioSappyDawburr",
        };

        static string[] boogleItems =
        {
            "ioBoogleBox",
            "ioBoogleWorm",
            "ioBoogleYoYo",
            "ioBoogleShovel",
            "ioBoogleLantern",
            "ioBoogleRedCross",
        };

        void TorinInventoryAnnotator()
        {
            // torin inventory item:  (ScriptID 64001 0) get: torin-item
            // boogle inventory item: (ScriptID 64001 1) get: boogle-item

            foreach (var node in Game.GetFunctions().SelectMany(n => n.Node))
            {
                if (node.At(0).Text == "ScriptID" &&
                    node.At(1) is Integer &&
                    node.At(2) is Integer &&
                    node.At(1).Number == 64001)
                {
                    string[] items = (node.At(2).Number == 0) ? torinItems : boogleItems;
                    if (node.Next(1).Text == "get:")
                    {
                        Node itemNode = node.Next(2);
                        if (itemNode is Integer &&
                            0 <= itemNode.Number &&
                            itemNode.Number < items.Length)
                        {
                            itemNode.Annotate(items[itemNode.Number]);
                        }
                    }
                }
            }
        }
    }
}
