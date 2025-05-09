using System.Collections.Generic;

namespace SCI.Decompile.Mac
{
    static class Hoyle4MacSymbols
    {
        public static IReadOnlyDictionary<Id, string> Classes = new Dictionary<Id, string>
        {
            { new Id(0, 0), "HoyleRoom" },
            { new Id(0, 1), "ColorBorderWindow" },
            { new Id(0, 2), "HoyleGameControls" },
            { new Id(0, 3), "PlaySong" },
            { new Id(3, 0), "Choice" },
            { new Id(3, 1), "NamePlate" },
            { new Id(7, 0), "KeyMouse" },
            { new Id(9, 0), "SpeakWindow" },
            { new Id(10, 0), "InvisibleWindow" },
            { new Id(10, 1), "HoyleWindow" },
            { new Id(15, 0), "Piles" },
            { new Id(15, 1), "Card" },
            { new Id(15, 2), "Deck" },
            { new Id(15, 3), "Arrow" },
            { new Id(15, 4), "Dealer" },
            { new Id(15, 5), "Hand" },
            { new Id(15, 6), "DiscardPile" },
            { new Id(15, 7), "Stock" },
            { new Id(16, 0), "TrackingView" },
            { new Id(16, 1), "DragableActor" },
            { new Id(17, 0), "Character" },
            { new Id(17, 1), "Characters" },
            { new Id(18, 0), "Tray" },
            { new Id(100, 0), "Crazy8sHand" },
            { new Id(170, 0), "Crazy8sStrategy" },
            { new Id(200, 0), "Walk" },
            { new Id(200, 1), "OldMaidHand" },
            { new Id(200, 2), "MaidCard" },
            { new Id(255, 1), "DText" },
            { new Id(255, 2), "Dialog" },
            { new Id(270, 0), "OldMaidStrategy" },
            { new Id(300, 0), "HeartsCard" },
            { new Id(300, 1), "HeartsHand" },
            { new Id(300, 2), "HeartsTrick" },
            { new Id(370, 0), "HeartsStrategy" },
            { new Id(400, 0), "GinCard" },
            { new Id(400, 2), "GinHand" },
            { new Id(500, 0), "Peg" },
            { new Id(500, 1), "CribbageHand" },
            { new Id(500, 2), "CountCombos" },
            { new Id(500, 3), "CribbageTrick" },
            { new Id(504, 0), "MugginsInputItem" },
            { new Id(570, 0), "CribbageStrategy" },
            { new Id(600, 0), "Foundation" },
            { new Id(600, 1), "KlonCard" },
            { new Id(600, 2), "KlonHand" },
            { new Id(600, 3), "EmptyCol" },
            { new Id(600, 4), "KlonDealer" },
            { new Id(600, 5), "Undoer" },
            { new Id(700, 0), "BridgeHands" },
            { new Id(700, 1), "BridgeCard" },
            { new Id(700, 2), "BridgeHand" },
            { new Id(701, 0), "BridgeInputItem" },
            { new Id(702, 0), "Trick" },
            { new Id(704, 0), "BScore" },
            { new Id(710, 0), "BridgeStrategyPlay" },
            { new Id(725, 0), "SortArray" },
            { new Id(730, 0), "BridgeDefense" },
            { new Id(732, 0), "Declarer_Lead_NT" },
            { new Id(733, 0), "Declarer_Second_NT" },
            { new Id(734, 0), "Declarer_Third_NT" },
            { new Id(735, 0), "Declarer_Fourth_NT" },
            { new Id(745, 0), "Bid" },
            { new Id(745, 1), "BidBox" },
            { new Id(746, 0), "Discard" },
            { new Id(747, 0), "LeadReturn_Trump" },
            { new Id(748, 0), "LeadSeat_Trump" },
            { new Id(749, 0), "SecondSeat_Trump" },
            { new Id(750, 0), "ThirdSeat_Trump" },
            { new Id(751, 0), "FourthSeat_Trump" },
            { new Id(752, 0), "LeadReturn_NoTrump" },
            { new Id(753, 0), "LeadSeat_NoTrump" },
            { new Id(754, 0), "SecondSeat_NoTrump" },
            { new Id(755, 0), "ThirdSeat_NoTrump" },
            { new Id(756, 0), "FourthSeat_NoTrump" },
            { new Id(760, 0), "DefFinesse" },
            { new Id(770, 0), "BridgeStrategy" },
            { new Id(800, 0), "EuchreTrick" },
            { new Id(800, 1), "EuchreHand" },
            { new Id(802, 0), "EScore" },
            { new Id(870, 0), "EuchreStrategy" },
            { new Id(910, 0), "IconBarList" },
            { new Id(910, 1), "IconTrackingView" },
            { new Id(910, 2), "DemoChoice" },
            { new Id(910, 3), "GuyTrackingView" },
            { new Id(910, 4), "ChooseTrackingView" },
            { new Id(910, 5), "DeckTrackingView" },
            { new Id(910, 6), "DemoNamePlate" },
            { new Id(921, 0), "Print" },
            { new Id(922, 0), "DIcon" },
            { new Id(922, 1), "DButton" },
            { new Id(922, 2), "DEdit" },
            { new Id(924, 0), "Messager" },
            { new Id(925, 0), "MessageObj" },
            { new Id(925, 1), "Conversation" },
            { new Id(928, 0), "Narrator" },
            { new Id(929, 0), "Sync" },
            { new Id(929, 1), "ScriptSync" },
            { new Id(929, 2), "MouthSync" },
            { new Id(934, 0), "Slider" },
            { new Id(935, 0), "Scaler" },
            { new Id(936, 0), "BorderWindow" },
            { new Id(937, 1), "IconBar" },
            { new Id(940, 0), "TrickBox" },
            { new Id(941, 0), "RandCycle" },
            { new Id(950, 0), "Feature" },
            { new Id(956, 0), "ForwardCounter" },
            { new Id(963, 0), "RelDPath" },
            { new Id(964, 0), "DPath" },
            { new Id(967, 0), "DCIcon" },
            { new Id(973, 0), "Timer" },
            { new Id(978, 0), "GameControls" },
            { new Id(978, 1), "ControlIcon" },
            { new Id(981, 0), "SysWindow" },
            { new Id(981, 1), "Window" },
            { new Id(983, 0), "Path" },
            { new Id(983, 1), "RelPath" },
            { new Id(989, 0), "Sound" },
            { new Id(991, 0), "Jump" },
            { new Id(991, 1), "JumpTo" },
            { new Id(992, 0), "Cycle" },
            { new Id(992, 4), "Motion" },
            { new Id(992, 5), "MoveTo" },
            { new Id(994, 0), "Sounds" },
            { new Id(994, 1), "Game" },
            { new Id(996, 0), "User" },
            { new Id(996, 1), "Ego" },
            { new Id(996, 2), "OnMeAndLowY" },
            { new Id(998, 0), "View" },
            { new Id(998, 1), "Prop" },
            { new Id(998, 2), "Actor" },
            { new Id(999, 1), "Code" },
            { new Id(999, 3), "List" },
            { new Id(999, 4), "Set" },
            { new Id(999, 5), "EventHandler" },
            { new Id(999, 6), "Script" },
            { new Id(999, 7), "Event" },
            { new Id(999, 8), "Cursor" },
        };

        public static IReadOnlyDictionary<Id, string> Instances = new Dictionary<Id, string>
        {
            { new Id(0, 0), "char1" },
            { new Id(0, 1), "char2" },
            { new Id(0, 2), "char3" },
            { new Id(0, 3), "face1" },
            { new Id(0, 4), "face2" },
            { new Id(0, 5), "face3" },
            { new Id(0, 6), "charScript1" },
            { new Id(0, 7), "charScript2" },
            { new Id(0, 8), "charScript3" },
            { new Id(0, 9), "hoyle4" },
            { new Id(0, 10), "hoyle4MouseDown" },
            { new Id(0, 11), "hoyle4KeyDown" },
            { new Id(0, 12), "aTalker" },
            { new Id(0, 13), "hoyle4Direction" },
            { new Id(0, 14), "theKeyMouseList" },
            { new Id(0, 15), "aniList" },
            { new Id(0, 16), "mouseCursor" },
            { new Id(0, 17), "hoyle4GameControls" },
            { new Id(0, 18), "gcWindow" },
            { new Id(0, 22), "theCard1" },
            { new Id(0, 23), "delayCast" },
            { new Id(2, 0), "intro" },
            { new Id(2, 1), "palScript" },
            { new Id(2, 2), "introScript" },
            { new Id(2, 3), "tradeMark" },
            { new Id(2, 4), "hoyleWord" },
            { new Id(2, 5), "cardGameWord" },
            { new Id(2, 6), "panel" },
            { new Id(2, 7), "playTheGame" },
            { new Id(2, 8), "introduction" },
            { new Id(2, 9), "aboutTheGame" },
            { new Id(2, 10), "tutorial" },
            { new Id(2, 11), "information" },
            { new Id(2, 12), "quitTheGame" },
            { new Id(3, 0), "selectPlayers" },
            { new Id(3, 1), "things" },
            { new Id(3, 2), "okButn" },
            { new Id(3, 3), "showGuysButn" },
            { new Id(3, 4), "frameGuy1" },
            { new Id(3, 5), "frameGuy2" },
            { new Id(3, 6), "frameGuy3" },
            { new Id(3, 7), "frameGuy4" },
            { new Id(3, 8), "frameGuy5" },
            { new Id(3, 9), "frameGuy6" },
            { new Id(3, 10), "frameGuy7" },
            { new Id(3, 11), "frameGuy8" },
            { new Id(3, 12), "frameGuy9" },
            { new Id(3, 13), "frameChoice1" },
            { new Id(3, 14), "frameChoice2" },
            { new Id(3, 15), "frameChoice3" },
            { new Id(3, 16), "name1" },
            { new Id(3, 17), "name2" },
            { new Id(3, 18), "name3" },
            { new Id(3, 19), "name4" },
            { new Id(3, 20), "name5" },
            { new Id(3, 21), "name6" },
            { new Id(3, 22), "name7" },
            { new Id(3, 23), "name8" },
            { new Id(3, 24), "name9" },
            { new Id(4, 0), "aboutCode" },
            { new Id(12, 0), "gameControlIcon" },
            { new Id(12, 1), "optionsIcon" },
            { new Id(12, 2), "scoreIcon" },
            { new Id(12, 3), "rulesIcon" },
            { new Id(12, 4), "deckIcon" },
            { new Id(12, 5), "helpIcon" },
            { new Id(13, 0), "theDeckCode" },
            { new Id(13, 1), "deckControls" },
            { new Id(13, 2), "deckWindow" },
            { new Id(13, 3), "iconOK" },
            { new Id(13, 4), "iconDeckLeft" },
            { new Id(13, 5), "iconDeckRight" },
            { new Id(13, 6), "iconBoardLeft" },
            { new Id(13, 7), "iconBoardRight" },
            { new Id(14, 0), "setGC" },
            { new Id(14, 1), "volumeSlider" },
            { new Id(14, 2), "detailSlider" },
            { new Id(14, 3), "paceSlider" },
            { new Id(14, 4), "iconRestart" },
            { new Id(14, 5), "iconQuit" },
            { new Id(14, 6), "iconReplay" },
            { new Id(14, 7), "iconTime" },
            { new Id(14, 8), "iconMusic" },
            { new Id(14, 9), "iconHelp" },
            { new Id(14, 10), "iconOK" },
            { new Id(14, 11), "detailCode" },
            { new Id(14, 12), "paceCode" },
            { new Id(15, 0), "workngDeck" },
            { new Id(15, 1), "dealScript" },
            { new Id(15, 2), "sortedList" },
            { new Id(15, 3), "ascendingCode" },
            { new Id(15, 4), "descendingCode" },
            { new Id(15, 5), "ascendingBySuitCode" },
            { new Id(15, 6), "descendingBySuitCode" },
            { new Id(15, 7), "littleCard" },
            { new Id(15, 8), "discardList" },
            { new Id(15, 9), "discardScript" },
            { new Id(15, 10), "stockBack" },
            { new Id(15, 11), "stockScript" },
            { new Id(17, 0), "characterPrint" },
            { new Id(18, 0), "aTray" },
            { new Id(18, 1), "theTitle" },
            { new Id(18, 2), "trayOKButton" },
            { new Id(18, 3), "trayParts" },
            { new Id(19, 0), "credits" },
            { new Id(19, 1), "creditsScript" },
            { new Id(20, 0), "rulesCrazy8s" },
            { new Id(21, 0), "gameIntros" },
            { new Id(21, 1), "roomScript" },
            { new Id(99, 0), "debugRm" },
            { new Id(100, 0), "crazy8s" },
            { new Id(100, 1), "handleEventList" },
            { new Id(100, 2), "roomScript" },
            { new Id(100, 4), "hand1" },
            { new Id(100, 5), "hand2" },
            { new Id(100, 6), "hand3" },
            { new Id(100, 7), "hand4" },
            { new Id(100, 8), "crazy8sSortCode" },
            { new Id(100, 9), "theStock" },
            { new Id(100, 10), "discardPile" },
            { new Id(100, 11), "transition" },
            { new Id(100, 12), "theHands" },
            { new Id(100, 13), "directionArrow" },
            { new Id(120, 0), "rulesCrazy8s" },
            { new Id(150, 0), "chooseSuit" },
            { new Id(150, 1), "chooseWindow" },
            { new Id(150, 2), "chooseSuitControls" },
            { new Id(150, 3), "iconSpades" },
            { new Id(150, 4), "iconClubs" },
            { new Id(150, 5), "iconDiamonds" },
            { new Id(150, 6), "iconHearts" },
            { new Id(180, 0), "scoreCrazy8s" },
            { new Id(180, 1), "crazy8sScoreWindow" },
            { new Id(180, 2), "crazy8sScore" },
            { new Id(180, 3), "iconOK" },
            { new Id(190, 0), "optionCrazy8s" },
            { new Id(190, 1), "crazy8sWindow" },
            { new Id(190, 2), "crazy8sOptions" },
            { new Id(190, 3), "iconSort" },
            { new Id(190, 4), "iconEights" },
            { new Id(190, 5), "iconVariant" },
            { new Id(190, 6), "iconDrawLimit" },
            { new Id(190, 7), "iconHelp" },
            { new Id(190, 8), "iconOK" },
            { new Id(200, 0), "oldMaid" },
            { new Id(200, 1), "removePairs" },
            { new Id(200, 2), "compGetCard" },
            { new Id(200, 3), "roomScript" },
            { new Id(200, 4), "flasher" },
            { new Id(200, 5), "transScript" },
            { new Id(200, 6), "maid" },
            { new Id(200, 7), "humanLossScript" },
            { new Id(200, 8), "card" },
            { new Id(200, 10), "getCardScript" },
            { new Id(200, 11), "hand1" },
            { new Id(200, 12), "hand2" },
            { new Id(200, 13), "hand3" },
            { new Id(200, 14), "hand4" },
            { new Id(200, 15), "theHands" },
            { new Id(200, 16), "theDiscard" },
            { new Id(200, 17), "handleEventList" },
            { new Id(200, 18), "theCard2" },
            { new Id(200, 19), "theCard3" },
            { new Id(200, 20), "pairCode" },
            { new Id(200, 21), "pairButton" },
            { new Id(200, 22), "pairBack" },
            { new Id(200, 23), "joker" },
            { new Id(220, 0), "rulesOldMaid" },
            { new Id(280, 0), "scoreOldMaid" },
            { new Id(280, 1), "oldMaidScoreWindow" },
            { new Id(280, 2), "drawMaids" },
            { new Id(280, 3), "oldMaidScore" },
            { new Id(280, 4), "iconOK" },
            { new Id(290, 0), "optionOldMaid" },
            { new Id(290, 1), "oldMaidWindow" },
            { new Id(290, 2), "oldMaidOptions" },
            { new Id(290, 3), "iconRemovePairs" },
            { new Id(290, 4), "iconDeck" },
            { new Id(290, 5), "iconHelp" },
            { new Id(290, 6), "iconOK" },
            { new Id(300, 0), "heartsGame" },
            { new Id(300, 1), "hand1" },
            { new Id(300, 2), "hand2" },
            { new Id(300, 3), "hand3" },
            { new Id(300, 4), "hand4" },
            { new Id(300, 5), "passList1" },
            { new Id(300, 6), "passList2" },
            { new Id(300, 7), "passList3" },
            { new Id(300, 8), "passList4" },
            { new Id(300, 9), "theHands" },
            { new Id(300, 10), "handleEventList" },
            { new Id(300, 12), "roomScript" },
            { new Id(300, 13), "theCard4" },
            { new Id(320, 0), "rulesHearts" },
            { new Id(380, 0), "scoreHearts" },
            { new Id(380, 1), "heartsScoreWindow" },
            { new Id(380, 2), "heartsScore" },
            { new Id(380, 3), "iconOK" },
            { new Id(390, 0), "optionHearts" },
            { new Id(390, 1), "heartsWindow" },
            { new Id(390, 2), "heartsOptions" },
            { new Id(390, 3), "iconSort" },
            { new Id(390, 4), "iconSuitOrder" },
            { new Id(390, 5), "iconLead" },
            { new Id(390, 6), "iconPassing" },
            { new Id(390, 7), "iconQueenHeart" },
            { new Id(390, 8), "iconBreak1st" },
            { new Id(390, 9), "iconHelp" },
            { new Id(390, 10), "iconOK" },
            { new Id(400, 0), "ginRummy" },
            { new Id(400, 1), "discardPile" },
            { new Id(400, 2), "theStock" },
            { new Id(400, 3), "humGrp1List" },
            { new Id(400, 4), "humGrp2List" },
            { new Id(400, 5), "humGrp3List" },
            { new Id(400, 6), "humDeadList" },
            { new Id(400, 7), "comGrp1List" },
            { new Id(400, 8), "comGrp2List" },
            { new Id(400, 9), "comGrp3List" },
            { new Id(400, 10), "comDeadList" },
            { new Id(400, 11), "deadWood1" },
            { new Id(400, 12), "deadWood2" },
            { new Id(400, 13), "layOffHand" },
            { new Id(400, 14), "disposeList" },
            { new Id(400, 15), "tempHand" },
            { new Id(400, 16), "hand1" },
            { new Id(400, 17), "hand2" },
            { new Id(400, 18), "theHands" },
            { new Id(400, 19), "handleEventList" },
            { new Id(400, 21), "roomScript" },
            { new Id(400, 22), "passButton" },
            { new Id(400, 23), "knockButton" },
            { new Id(400, 24), "compCode" },
            { new Id(400, 25), "knockCheck" },
            { new Id(400, 26), "clearTable" },
            { new Id(400, 27), "showCards" },
            { new Id(400, 28), "tempDead" },
            { new Id(400, 29), "layEmOut" },
            { new Id(400, 30), "findStartX" },
            { new Id(400, 31), "variantCode" },
            { new Id(400, 32), "okieList" },
            { new Id(400, 33), "okieBox" },
            { new Id(400, 34), "okieNum" },
            { new Id(400, 35), "okieDouble" },
            { new Id(400, 36), "knownCards" },
            { new Id(402, 0), "numInWorkingDeck" },
            { new Id(402, 1), "isCardInWorkingDeck" },
            { new Id(403, 0), "numInWorkingDeck" },
            { new Id(403, 1), "isCardInWorkingDeck" },
            { new Id(404, 0), "numRankInDead" },
            { new Id(420, 0), "rulesGinRummy" },
            { new Id(480, 0), "scoreGinRummy" },
            { new Id(480, 1), "ginRummyScoreWindow" },
            { new Id(480, 2), "ginRummyScore" },
            { new Id(480, 3), "iconOK" },
            { new Id(480, 4), "scoreCode" },
            { new Id(480, 5), "drawScore" },
            { new Id(490, 0), "optionGinRummy" },
            { new Id(490, 1), "ginRummyWindow" },
            { new Id(490, 2), "ginRummyOptions" },
            { new Id(490, 3), "iconSort" },
            { new Id(490, 4), "iconVariant" },
            { new Id(490, 5), "iconHelp" },
            { new Id(490, 6), "iconOK" },
            { new Id(500, 0), "cribbage" },
            { new Id(500, 1), "peg1A" },
            { new Id(500, 2), "peg2A" },
            { new Id(500, 3), "peg3A" },
            { new Id(500, 4), "peg1B" },
            { new Id(500, 5), "peg2B" },
            { new Id(500, 6), "peg3B" },
            { new Id(500, 7), "hand1" },
            { new Id(500, 8), "hand2" },
            { new Id(500, 9), "theHands" },
            { new Id(500, 10), "handleEventList" },
            { new Id(500, 11), "theCrib" },
            { new Id(500, 13), "discardToCribScript" },
            { new Id(500, 14), "goButton" },
            { new Id(500, 15), "handScorePanel" },
            { new Id(500, 16), "scoreOKButton" },
            { new Id(500, 17), "maskOutOfPlayCards" },
            { new Id(500, 18), "maskCard" },
            { new Id(500, 19), "mask0" },
            { new Id(500, 20), "mask1" },
            { new Id(500, 21), "mask2" },
            { new Id(500, 22), "mask3" },
            { new Id(500, 23), "mask4" },
            { new Id(500, 24), "mask5" },
            { new Id(500, 25), "mask6" },
            { new Id(500, 26), "mask7" },
            { new Id(500, 27), "countBox" },
            { new Id(500, 28), "countNumber" },
            { new Id(500, 29), "roomScript" },
            { new Id(504, 0), "muggins1Input" },
            { new Id(504, 1), "muggins1InputWindow" },
            { new Id(504, 2), "muggins1InputControls" },
            { new Id(504, 3), "zero" },
            { new Id(504, 4), "one" },
            { new Id(504, 5), "two" },
            { new Id(504, 6), "three" },
            { new Id(504, 7), "four" },
            { new Id(504, 8), "five" },
            { new Id(504, 9), "six" },
            { new Id(504, 10), "seven" },
            { new Id(504, 11), "eight" },
            { new Id(504, 12), "nine" },
            { new Id(504, 13), "ten" },
            { new Id(504, 14), "eleven" },
            { new Id(504, 15), "twelve" },
            { new Id(504, 16), "thirteen" },
            { new Id(504, 17), "fourteen" },
            { new Id(504, 18), "fifteen" },
            { new Id(504, 19), "sixteen" },
            { new Id(504, 20), "seventeen" },
            { new Id(504, 21), "eighteen" },
            { new Id(504, 22), "nineteen" },
            { new Id(504, 23), "twenty" },
            { new Id(504, 24), "twentyOne" },
            { new Id(504, 25), "twentyTwo" },
            { new Id(504, 26), "twentyThree" },
            { new Id(504, 27), "twentyFour" },
            { new Id(504, 28), "twentyFive" },
            { new Id(504, 29), "twentySix" },
            { new Id(504, 30), "twentySeven" },
            { new Id(504, 31), "twentyEight" },
            { new Id(504, 32), "twentyNine" },
            { new Id(520, 0), "rulesCribbage" },
            { new Id(580, 0), "scoreCribbage" },
            { new Id(580, 1), "cribbageScoreWindow" },
            { new Id(580, 2), "cribbageScore" },
            { new Id(580, 3), "iconOK" },
            { new Id(590, 0), "optionCribbage" },
            { new Id(590, 1), "cribbageWindow" },
            { new Id(590, 2), "cribbageOptions" },
            { new Id(590, 3), "iconSort" },
            { new Id(590, 4), "iconMuggins" },
            { new Id(590, 5), "iconHelp" },
            { new Id(590, 6), "iconOK" },
            { new Id(600, 0), "klondike" },
            { new Id(600, 2), "roomScript" },
            { new Id(600, 3), "spadeActor" },
            { new Id(600, 4), "clubActor" },
            { new Id(600, 5), "heartActor" },
            { new Id(600, 6), "diamondActor" },
            { new Id(600, 7), "cleanUp" },
            { new Id(600, 8), "discardHand" },
            { new Id(600, 9), "endCode" },
            { new Id(600, 10), "canPlay" },
            { new Id(600, 11), "checkHands" },
            { new Id(600, 12), "theStock" },
            { new Id(600, 13), "resignButton" },
            { new Id(600, 14), "stockScript" },
            { new Id(600, 15), "timesThru" },
            { new Id(600, 16), "getThree" },
            { new Id(600, 17), "getOne" },
            { new Id(600, 18), "resetStock" },
            { new Id(600, 19), "undoReset" },
            { new Id(600, 20), "tempDiscard" },
            { new Id(600, 21), "optionCode" },
            { new Id(600, 22), "showThree" },
            { new Id(600, 23), "spadeFound" },
            { new Id(600, 24), "clubFound" },
            { new Id(600, 25), "diamondFound" },
            { new Id(600, 26), "heartFound" },
            { new Id(600, 27), "spadeView" },
            { new Id(600, 28), "clubView" },
            { new Id(600, 29), "diamondView" },
            { new Id(600, 30), "heartView" },
            { new Id(600, 31), "tempList" },
            { new Id(600, 32), "emptyCol1" },
            { new Id(600, 33), "hand1" },
            { new Id(600, 34), "emptyCol2" },
            { new Id(600, 35), "hand2" },
            { new Id(600, 36), "emptyCol3" },
            { new Id(600, 37), "hand3" },
            { new Id(600, 38), "emptyCol4" },
            { new Id(600, 39), "hand4" },
            { new Id(600, 40), "emptyCol5" },
            { new Id(600, 41), "hand5" },
            { new Id(600, 42), "emptyCol6" },
            { new Id(600, 43), "hand6" },
            { new Id(600, 44), "emptyCol7" },
            { new Id(600, 45), "hand7" },
            { new Id(600, 46), "theHands" },
            { new Id(600, 47), "handleEventList" },
            { new Id(600, 48), "dealScript" },
            { new Id(600, 49), "undoList" },
            { new Id(600, 50), "enterAdd" },
            { new Id(600, 51), "handleCode" },
            { new Id(601, 0), "spadeScript" },
            { new Id(601, 1), "clubScript" },
            { new Id(601, 2), "heartScript" },
            { new Id(601, 3), "diamondScript" },
            { new Id(620, 0), "rulesKlondike" },
            { new Id(680, 0), "scoreKlondike" },
            { new Id(680, 1), "drawSigns" },
            { new Id(680, 2), "klondikeScoreWindow" },
            { new Id(680, 3), "klondikeScore" },
            { new Id(680, 4), "iconOK" },
            { new Id(680, 5), "scoreHand" },
            { new Id(690, 0), "optionKlondike" },
            { new Id(690, 1), "klondikeWindow" },
            { new Id(690, 2), "klondikeOptions" },
            { new Id(690, 3), "iconFlip" },
            { new Id(690, 4), "iconTopShown" },
            { new Id(690, 5), "iconScoring" },
            { new Id(690, 6), "iconTimesThru" },
            { new Id(690, 7), "iconAbandon" },
            { new Id(690, 8), "iconUndo" },
            { new Id(690, 9), "iconSameDeck" },
            { new Id(690, 10), "iconHelp" },
            { new Id(690, 11), "iconOK" },
            { new Id(700, 0), "bridge" },
            { new Id(700, 1), "hand1" },
            { new Id(700, 2), "hand2" },
            { new Id(700, 3), "hand3" },
            { new Id(700, 4), "hand4" },
            { new Id(700, 5), "handleEventList" },
            { new Id(700, 7), "roomScript" },
            { new Id(700, 8), "dummySymbol" },
            { new Id(700, 9), "addToTricksWon" },
            { new Id(700, 10), "bridgeTrick" },
            { new Id(700, 11), "playedList1" },
            { new Id(700, 12), "playedList2" },
            { new Id(700, 13), "playedList3" },
            { new Id(700, 14), "playedList4" },
            { new Id(700, 15), "fixFile" },
            { new Id(700, 16), "putFile" },
            { new Id(701, 0), "bridgeInput" },
            { new Id(701, 1), "bridgeInputWindow" },
            { new Id(701, 2), "bridgeInputControls" },
            { new Id(701, 3), "oneClub" },
            { new Id(701, 4), "twoClub" },
            { new Id(701, 5), "threeClub" },
            { new Id(701, 6), "fourClub" },
            { new Id(701, 7), "fiveClub" },
            { new Id(701, 8), "sixClub" },
            { new Id(701, 9), "sevenClub" },
            { new Id(701, 10), "oneDiamond" },
            { new Id(701, 11), "twoDiamond" },
            { new Id(701, 12), "threeDiamond" },
            { new Id(701, 13), "fourDiamond" },
            { new Id(701, 14), "fiveDiamond" },
            { new Id(701, 15), "sixDiamond" },
            { new Id(701, 16), "sevenDiamond" },
            { new Id(701, 17), "oneHeart" },
            { new Id(701, 18), "twoHeart" },
            { new Id(701, 19), "threeHeart" },
            { new Id(701, 20), "fourHeart" },
            { new Id(701, 21), "fiveHeart" },
            { new Id(701, 22), "sixHeart" },
            { new Id(701, 23), "sevenHeart" },
            { new Id(701, 24), "oneSpade" },
            { new Id(701, 25), "twoSpade" },
            { new Id(701, 26), "threeSpade" },
            { new Id(701, 27), "fourSpade" },
            { new Id(701, 28), "fiveSpade" },
            { new Id(701, 29), "sixSpade" },
            { new Id(701, 30), "sevenSpade" },
            { new Id(701, 31), "oneNT" },
            { new Id(701, 32), "twoNT" },
            { new Id(701, 33), "threeNT" },
            { new Id(701, 34), "fourNT" },
            { new Id(701, 35), "fiveNT" },
            { new Id(701, 36), "sixNT" },
            { new Id(701, 37), "sevenNT" },
            { new Id(701, 38), "passButton" },
            { new Id(701, 39), "double" },
            { new Id(701, 40), "scrollUp" },
            { new Id(701, 41), "scrollDown" },
            { new Id(702, 0), "trickScript" },
            { new Id(702, 1), "theCard2" },
            { new Id(702, 2), "theCard3" },
            { new Id(703, 0), "bridgeReview" },
            { new Id(703, 1), "bridgeReviewWindow" },
            { new Id(703, 2), "bridgeReviewControls" },
            { new Id(703, 3), "iconOK" },
            { new Id(703, 4), "scrollUp" },
            { new Id(703, 5), "scrollDown" },
            { new Id(710, 0), "workingList" },
            { new Id(710, 1), "sortedList" },
            { new Id(710, 2), "descendingCode" },
            { new Id(712, 0), "n1_tree" },
            { new Id(713, 0), "n2_tree" },
            { new Id(714, 0), "n3_tree" },
            { new Id(715, 0), "c2_tree" },
            { new Id(716, 0), "other1_tree" },
            { new Id(717, 0), "weak2_tree" },
            { new Id(718, 0), "compete_tree" },
            { new Id(719, 0), "preempt_tree" },
            { new Id(720, 0), "rulesBridge" },
            { new Id(720, 1), "readWhcihWindow" },
            { new Id(720, 2), "readWhichControls" },
            { new Id(720, 3), "iconConventions" },
            { new Id(720, 4), "iconRules" },
            { new Id(721, 0), "stayman" },
            { new Id(722, 0), "b1" },
            { new Id(730, 0), "workingList" },
            { new Id(730, 1), "sortedList" },
            { new Id(730, 2), "descendingCode" },
            { new Id(731, 0), "compwe_tree" },
            { new Id(761, 0), "b1" },
            { new Id(762, 0), "scorePanel" },
            { new Id(762, 1), "scoreOKButton" },
            { new Id(780, 0), "scoreBridge" },
            { new Id(780, 1), "bridgeScoreWindow" },
            { new Id(780, 2), "bridgeScore" },
            { new Id(780, 3), "iconOK" },
            { new Id(790, 0), "optionBridge" },
            { new Id(790, 1), "bridgeWindow" },
            { new Id(790, 2), "bridgeOptions" },
            { new Id(790, 3), "iconSort" },
            { new Id(790, 4), "iconDummy" },
            { new Id(790, 5), "iconReview" },
            { new Id(790, 6), "iconRedeal" },
            { new Id(790, 7), "iconRebid" },
            { new Id(790, 8), "iconReplay" },
            { new Id(790, 9), "iconRandom" },
            { new Id(790, 10), "iconSave" },
            { new Id(790, 11), "iconHelp" },
            { new Id(790, 12), "iconOK" },
            { new Id(800, 0), "hand1" },
            { new Id(800, 1), "hand2" },
            { new Id(800, 2), "hand3" },
            { new Id(800, 3), "hand4" },
            { new Id(800, 4), "euchre" },
            { new Id(800, 5), "theHands" },
            { new Id(800, 6), "handleEventList" },
            { new Id(800, 8), "roomScript" },
            { new Id(800, 9), "addToTricksWon" },
            { new Id(800, 10), "extraCards" },
            { new Id(800, 11), "theStock" },
            { new Id(800, 12), "discardPile" },
            { new Id(801, 0), "bidEuchre" },
            { new Id(801, 1), "euchreWindow" },
            { new Id(801, 2), "euchreBid" },
            { new Id(801, 3), "iconPlayAlone" },
            { new Id(801, 4), "iconPass" },
            { new Id(801, 5), "iconOrderUp" },
            { new Id(801, 6), "iconSpades" },
            { new Id(801, 7), "iconClubs" },
            { new Id(801, 8), "iconDiamonds" },
            { new Id(801, 9), "iconHearts" },
            { new Id(820, 0), "rulesSpades" },
            { new Id(880, 0), "scoreEuchre" },
            { new Id(880, 1), "euchreScoreWindow" },
            { new Id(880, 2), "euchreScore" },
            { new Id(880, 3), "iconOK" },
            { new Id(890, 0), "optionEuchre" },
            { new Id(890, 1), "euchreWindow" },
            { new Id(890, 2), "euchreOptions" },
            { new Id(890, 3), "iconSort" },
            { new Id(890, 4), "iconPoints" },
            { new Id(890, 5), "iconStickDealer" },
            { new Id(890, 6), "iconHelp" },
            { new Id(890, 7), "iconOK" },
            { new Id(900, 0), "sierra" },
            { new Id(900, 1), "introScript" },
            { new Id(900, 2), "sparkle" },
            { new Id(900, 3), "fred" },
            { new Id(910, 0), "demoCursor" },
            { new Id(910, 1), "assocList" },
            { new Id(910, 2), "helpDemo" },
            { new Id(910, 3), "demoScript" },
            { new Id(910, 4), "controlPanelDemo" },
            { new Id(910, 5), "deckDemo" },
            { new Id(910, 6), "selectCharectureDemo" },
            { new Id(910, 7), "handDemo" },
            { new Id(910, 8), "quitButton" },
            { new Id(910, 9), "chooseCrazy8s" },
            { new Id(910, 10), "chooseOldMaid" },
            { new Id(910, 11), "chooseGinRummy" },
            { new Id(910, 12), "chooseCribbage" },
            { new Id(910, 13), "chooseHearts" },
            { new Id(910, 14), "chooseBridge" },
            { new Id(910, 15), "chooseKlondike" },
            { new Id(910, 16), "chooseEuchre" },
            { new Id(910, 17), "chooseTitle" },
            { new Id(910, 18), "frameChoice1" },
            { new Id(910, 19), "frameChoice2" },
            { new Id(910, 20), "frameChoice3" },
            { new Id(910, 21), "name1" },
            { new Id(910, 22), "name2" },
            { new Id(910, 23), "name3" },
            { new Id(910, 24), "name4" },
            { new Id(910, 25), "name5" },
            { new Id(910, 26), "name6" },
            { new Id(910, 27), "name7" },
            { new Id(910, 28), "name8" },
            { new Id(910, 29), "name9" },
            { new Id(910, 30), "frameGuy1" },
            { new Id(910, 31), "frameGuy2" },
            { new Id(910, 32), "frameGuy3" },
            { new Id(910, 33), "frameGuy4" },
            { new Id(910, 34), "frameGuy5" },
            { new Id(910, 35), "frameGuy6" },
            { new Id(910, 36), "frameGuy7" },
            { new Id(910, 37), "frameGuy8" },
            { new Id(910, 38), "frameGuy9" },
            { new Id(910, 39), "okButn" },
            { new Id(910, 40), "showGuysButn" },
            { new Id(910, 41), "things" },
            { new Id(910, 42), "deckOK" },
            { new Id(910, 43), "iconDeckLeft" },
            { new Id(910, 44), "iconDeckRight" },
            { new Id(910, 45), "iconBoardLeft" },
            { new Id(910, 46), "iconBoardRight" },
            { new Id(910, 47), "tempView" },
            { new Id(910, 48), "discardPile" },
            { new Id(910, 49), "theStock" },
            { new Id(911, 0), "giveTimeCode" },
            { new Id(911, 1), "giveTimeWindow" },
            { new Id(911, 2), "giveTime" },
            { new Id(911, 3), "iconOK" },
            { new Id(912, 0), "useStndrd" },
            { new Id(912, 1), "useStandardWindow" },
            { new Id(912, 2), "useStandardControls" },
            { new Id(912, 3), "iconUseStandard" },
            { new Id(912, 4), "iconHelp" },
            { new Id(912, 5), "iconOK" },
            { new Id(914, 0), "doMusicControls" },
            { new Id(914, 1), "musicWindow" },
            { new Id(914, 2), "musicControls" },
            { new Id(914, 3), "iconMusic" },
            { new Id(914, 5), "iconOK" },
            { new Id(925, 0), "cleanCode" },
            { new Id(930, 0), "yesNo" },
            { new Id(930, 1), "yesNoWindow" },
            { new Id(930, 2), "yesNoControls" },
            { new Id(930, 3), "iconYes" },
            { new Id(930, 4), "iconNo" },
            { new Id(952, 0), "sysLogger" },
            { new Id(975, 0), "chooseGame" },
            { new Id(975, 1), "chooseScript" },
            { new Id(975, 2), "chooseCrazy8s" },
            { new Id(975, 3), "chooseOldMaid" },
            { new Id(975, 4), "chooseGinRummy" },
            { new Id(975, 5), "chooseCribbage" },
            { new Id(975, 6), "chooseHearts" },
            { new Id(975, 7), "chooseBridge" },
            { new Id(975, 8), "chooseKlondike" },
            { new Id(975, 9), "chooseEuchre" },
            { new Id(975, 10), "chooseTitle" },
            { new Id(994, 2), "mayPause" },
            { new Id(996, 0), "uEvt" },
        };
    }
}
