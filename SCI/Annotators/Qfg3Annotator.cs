using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Qfg3Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            VerbAnnotator.Run(Game, verbs);
            InventoryAnnotator.Run(Game, items);
            RunLate();

            GlobalEnumAnnotator.Run(Game, 125, characterClasses);
            GlobalEnumAnnotator.Run(Game, 362, characterClasses);

            QfgSkillAnnotator.Run(Game, 251, skills);
            Qfg34GaitAnnotator.Run(Game);
            Qfg3TellerAnnotator.Run(Game, MessageFinder);
            Qfg3DeathAnnotator.Run(Game, MessageFinder);
        }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
            { 100, "gEgoGait" },

            { 120, "gClock" },
            { 121, "gNight" },
            { 122, "gDay" },
            { 123, "gTimeOfDay" }, // it's an enum [ 0 - 7 ]
            { 124, "gOldSysTime" },
            { 125, "gHeroType" },
            { 136, "gFreeMeals" },
            { 251, "gEgoStats" },
            { 291, "gSkillTicks" },

            { 362, "gBaseHeroType" }, // 0, 1, 2. paladins are 0 (fighter)
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 1), "HaveMem" },
            { Tuple.Create(0, 2), "HandsOff" },
            { Tuple.Create(0, 3), "HandsOn" },

            { Tuple.Create(0, 4), "SetFlag" },
            { Tuple.Create(0, 5), "ClearFlag" },
            { Tuple.Create(0, 6), "IsFlag" },

            { Tuple.Create(0, 8), "Random300" },

            { Tuple.Create(0, 11), "NextDay" },
            { Tuple.Create(0, 12), "Face" },

            { Tuple.Create(26, 0), "EgoDead" },
        };

        static IReadOnlyDictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 1, "Look" },
            { 3, "Walk" },
            { 4, "Do" },
            { 2, "Talk" },

            { 10, "Money" }, // don't know why this isn't 59 theRoyals, but clicking on hero is 10

            { 11, "theSword" },
            { 12, "theFineDagger" },
            { 13, "theFineSpear" },
            { 14, "theChainmail" },
            { 15, "theShield" },
            { 16, "theGrapnel" },
            { 17, "theToolkit" },
            { 18, "theGuildCard" },
            { 19, "theTinderbox" },
            { 20, "theDaggers" },
            { 21, "theCurePills" },
            { 22, "theHealPills" },
            { 23, "theManaPills" },
            { 24, "theRations" },
            { 25, "theWaterskin" },
            { 26, "theDispell" },
            { 27, "theFish" },
            { 28, "theMeat" },
            { 29, "theFruit" },
            { 30, "theBeads" },
            { 31, "theSkins" },
            { 32, "theHorn" },
            { 33, "theRocks" },
            { 34, "theVine" },
            { 35, "theOil" },
            { 36, "theRope" },
            { 37, "theGagGift" },
            { 39, "thePin" },
            { 40, "theHoney" },
            { 41, "theFeather" },
            { 42, "theAmulet" },
            { 43, "theLeopard" },
            { 44, "theBird" },
            { 45, "theOpal" },
            { 46, "theVineFruit" },
            { 47, "theGem" },
            { 48, "thePeaceWater" },
            { 49, "theHeartGift" },
            { 50, "theOrchid" },
            { 51, "theRobe" },
            { 52, "theBridge" },
            { 53, "theEye" },
            { 54, "theNote" },
            { 55, "theWood" },
            { 56, "theMagicSpear" },
            { 57, "theMagicDrum" },
            { 59, "theRoyals" },

            { 65, "Rest" },
            { 73, "invDrop" },
            { 74, "Sleep" },

            // spells
            { 66, "healingSpell" },
            { -75, "openSpell" },
            { 75, "openSpell" },
            { -76, "detectMagicSpell" },
            { 76, "detectMagicSpell" },
            { -77, "triggerSpell" },
            { 77, "triggerSpell" },
            { 78, "dazzleSpell" },
            { -80, "calmSpell" },
            { 80, "calmSpell" },
            { 81, "flameDartSpell" },
            { -82, "fetchSpell" },
            { 82, "fetchSpell" },
            { 83, "forceBoltSpell" },
            { 84, "levitateSpell" },
            { 85, "reversalSpell" },
            { 86, "jugglingLightsSpell" },
            { 87, "summonStaffSpell" },
            { 88, "lightningBallSpell" },
        };

        static string[] items =
        {
            "theRoyals",
            "theSword",
            "theFineDagger",
            "theFineSpear",
            "theChainmail",
            "theShield",
            "theGrapnel",
            "theToolkit",
            "theGuildCard",
            "theTinderbox",
            "theDaggers",
            "theCurePills",
            "theHealPills",
            "theManaPills",
            "theRations",
            "theWaterskin",
            "theDispell",
            "theFish",
            "theMeat",
            "theFruit",
            "theBeads",
            "theSkins",
            "theHorn",
            "theRocks",
            "theVine",
            "theOil",
            "theRope",
            "theGagGift",
            "thePin",
            "theHoney",
            "theFeather",
            "theAmulet",
            "theLeopard",
            "theBird",
            "theOpal",
            "theVineFruit",
            "theGem",
            "thePeaceWater",
            "theHeartGift",
            "theOrchid",
            "theRobe",
            "theBridge",
            "theEye",
            "theNote",
            "theWood",
            "theMagicSpear",
            "theMagicDrum",
            "invPageDown",
            "invPageUp",
            "invLook",
            "invSelect",
            "invDrop",
            "ok",
            "invHelp",
            "dummyIcon",
        };

        static Dictionary<int, string> characterClasses = new Dictionary<int, string>
        {
            { 0, "Fighter" },
            { 1, "Magic User" },
            { 2, "Thief" },
            { 3, "Paladin" }
        };

        static string[] skills =
        {
            "strength",
            "intelligence",
            "agility",
            "Vitality",
            "luck",
            "weapon-use",
            "parry",
            "dodge",
            "sneak",
            "pick locks",
            "throw",
            "climb",
            "magic use",
            "communication",
            "honor",
            "experience",
            "health",
            "stamina",
            "mana",
            "openSpell",
            "detectSpell",
            "triggerSpell",
            "dazzleSpell",
            "zapSpell",
            "calmSpell",
            "flameDartSpell",
            "fetchSpell",
            "forceBoltSpell",
            "levitateSpell",
            "reversalSpell",
            "jugglingLightsSpell",
            "summonStaffSpell",
            "lightningBallSpell",
            "healingSpell"
        };
    }
}
