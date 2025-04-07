using System;
using System.Collections.Generic;

namespace SCI.Annotators
{
    class Qfg4Annotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            VerbAnnotator.Run(Game, verbs);
            InventoryAnnotator.Run(Game, items);
            RunLate();

            QfgSkillAnnotator.Run(Game, 247, skills);

            GlobalEnumAnnotator.Run(Game, 125, characterClasses);
            GlobalEnumAnnotator.Run(Game, 365, monsterNumbers);

            Qfg34GaitAnnotator.Run(Game);
            Qfg4TellerAnnotator.Run(Game, MessageFinder);
            Qfg4DeathAnnotator.Run(Game, MessageFinder);
        }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
            { 1, "gGlory" }, // decompiler names this differently on different versions

            { 100, "gEgoGait" },

            { 120, "gClock" },
            { 121, "gNight" },
            { 122, "gDay" },
            { 123, "gTime" },
            { 125, "gHeroType" },
            { 126, "gTempEgoSpeed" },
            { 136, "gFreeMeals" },
            { 157, "gHeroName" },
            { 201, "gDebugging" },

            { 205, "gInitialStats" }, // used to initialize char-sheet from importer
            { 247, "gEgoStats" },

            { 416, "gPrevKatrinaDayNumber" },
            { 426, "gPrevDomoTalkDayNumber" },

            // combat
            { 155, "gCombatResult" },
            { 365, "gCombatMonsterNum" },
            { 185, "gCombatMonsterActor" },
            { 195, "gCombatEgoActor" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 2), "SetFlag" },
            { Tuple.Create(0, 3), "ClearFlag" },
            { Tuple.Create(0, 4), "IsFlag" },

            { Tuple.Create(0, 6), "Random300" },

            { Tuple.Create(0, 9), "NextDay" },
            { Tuple.Create(0, 10), "Face" },

            { Tuple.Create(26, 0), "EgoDead" },
        };

        static Dictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 1, "Look" },
            { 2, "Talk" },
            { 3, "Walk" },
            { 4, "Do" },

            { 10, "Jump" },

            { 13, "theBag" },
            { 14, "theBonsai" },
            { 15, "thePurse" },
            { 16, "theManas" },
            { 17, "theCures" },
            { 18, "theHeals" },
            { 19, "theRations" },
            { 21, "theRocks" },
            { 22, "theCandle" },
            { 23, "theCandy" },
            { 24, "theFlask" },
            { 25, "theWater" },
            { 26, "theGruegoo" },
            { 27, "theBonemeal" },
            { 28, "theLockpick" },
            { 29, "theKeyRing" },
            { 32, "theOil" },
            { 33, "theGrapnel" },
            { 34, "theArmor" },
            { 35, "theShield" },
            { 36, "theSword" },
            { 37, "theThrowdagger" },
            { 39, "theFlint" },
            { 40, "theGarlic" },
            { 41, "theGuildcard" },
            { 42, "theToolkit" },
            { 43, "theDarksign" },
            { 44, "theHat" },
            { 45, "theCorn" },
            { 46, "thePiepan" },
            { 47, "theBones" },
            { 48, "theBerries" },
            { 49, "theHumorbar" },
            { 51, "theRehydrator" },
            { 52, "theDoll" },
            { 53, "theBlackbird" },
            { 54, "theCloth" },
            { 55, "theChicken" },
            { 56, "theAmulet" },
            { 57, "theHair" },
            { 58, "theBroom" },
            { 59, "theFlowers" },
            { 60, "theWillowisp" },
            { 63, "theLocket" },
            { 66, "theStatue" },
            { 67, "theBoneRit" },
            { 69, "theBloodRit" },
            { 70, "theBreathRit" },
            { 72, "theSenseRit" },
            { 74, "theHeartRit" },
            { 76, "theTorch" },
            { 77, "theJewelry" },
            { 78, "theKnob" },
            { 157, "theStaff" },
            { 170, "theStake" },
            { 171, "theHammer" },

            // spells. these values have to be manually
            // extracted from spell code in script 21.
            // they might pass this value through doVerb,
            // event message, or room notify.
            { 11, "glideSpell" },
            { 79,  "frostSpell" },
            { 80,  "openSpell" },
            { 81,  "detectMagicSpell" },
            { 82,  "triggerSpell" },
            { 83,  "dazzleSpell" },
            { 84,  "zapSpell" },
            { 85,  "calmSpell" },
            { 86,  "flameDartSpell" },
            { 87,  "fetchSpell" },
            { 88,  "forceBoltSpell" },
            { 89,  "levitateSpell" },
            { 90,  "reversalSpell" },
            { 91,  "jugglingLightsSpell" },
            { 92,  "summonStaffSpell" },
            { 93,  "lightningBallSpell" },
            { 94,  "ritualSpell" },
            { 95,  "invisibleSpell" },
            { 96,  "auraSpell" },
            { 97,  "protectionSpell" },
            { 98,  "resistanceSpell" },
            { 100, "glideSpell" },
            { 102, "healingSpell" },

             // negative versions are the second half of the spell, like
            // when fetch returns stuff.
            { -11, "glideSpell (part 2)" },
            { -79,  "frostSpell (part 2)" },
            { -80,  "openSpell (part 2)" },
            { -81,  "detectMagicSpell (part 2)" },
            { -82,  "triggerSpell (part 2)" },
            { -83,  "dazzleSpell (part 2)" },
            { -84,  "zapSpell (part 2)" },
            { -85,  "calmSpell (part 2)" },
            { -86,  "flameDartSpell (part 2)" },
            { -87,  "fetchSpell (part 2)" },
            { -88,  "forceBoltSpell (part 2)" },
            { -89,  "levitateSpell (part 2)" },
            { -90,  "reversalSpell (part 2)" },
            { -91,  "jugglingLightsSpell (part 2)" },
            { -92,  "summonStaffSpell (part 2)" },
            { -93,  "lightningBallSpell (part 2)" },
            { -94,  "ritualSpell (part 2)" },
            { -95,  "invisibleSpell (part 2)" },
            { -96,  "auraSpell (part 2)" },
            { -97,  "protectionSpell (part 2)" },
            { -98,  "resistanceSpell (part 2)" },
            { -100, "glideSpell (part 2)" },
            { -102, "healingSpell (part 2)" },

            { 103, "Sleep 1 hour or less" },
            { 104, "Sleep all night" },
        };

        static string[] items =
        {
            "thePurse",
            "theManas",
            "theCures",
            "theHeals",
            "theRations",
            "theThrowdagger",
            "theRocks",
            "theCandle",
            "theCandy",
            "theFlask",
            "theWater",
            "theGruegoo",
            "theBonemeal",
            "theLockpick",
            "theKeyRing",
            "theOil",
            "theGrapnel",
            "theArmor",
            "theShield",
            "theSword",
            "theDagger",
            "theFlint",
            "theGarlic",
            "theGuildcard",
            "theToolkit",
            "theDarksign",
            "theHat",
            "theCorn",
            "thePiepan",
            "theBones",
            "theBerries",
            "theHumorbar",
            "theRehydrator",
            "theDoll",
            "theBlackbird",
            "theCloth",
            "theChicken",
            "theAmulet",
            "theHair",
            "theBroom",
            "theFlowers",
            "theWillowisp",
            "theLocket",
            "theStatue",
            "theTorch",
            "theJewelry",
            "theKnob",
            "theStaff",
            "theBonsai",
            "theHammer",
            "theStake",
            "theBag",
            "theBoneRit",
            "theBloodRit",
            "theBreathRit",
            "theSenseRit",
            "theHeartRit",
            "invDummy1",
            "lab-key", //"invLook",
            "invSelect",
            "crypt-key", //"invHelp",
            "guild-key", //"ok",
            "relief-key", // "invDummy2",
            "large-key", // "invSlider",
            "invUpArrow",
            "invDownArrow",
        };

        static Dictionary<int, string> characterClasses = new Dictionary<int, string>
        {
            { 0, "Fighter" },
            { 1, "Magic User" },
            { 2, "Thief" },
            { 3, "Paladin" }
        };

        static Dictionary<int, string> monsterNumbers = new Dictionary<int, string>
        {
            { 810, "rabbit" },
            { 825, "badder" },
            { 830, "revenant" },
            { 835, "wyvern" },
            { 840, "chernovy" },
            { 850, "wraith" },
            { 855, "horror" },
            { 870, "nectar" },
        };

        static string[] skills =
        {
            "strength",
            "intelligence",
            "agility",
            "vitality",
            "luck",
            "weapon-use",
            "parry",
            "dodge",
            "stealth",
            "pick locks",
            "throwing",
            "climbing",
            "magic",
            "communication",
            "honor",
            "acrobatics",
            "experience", // if you warp with debugger then script 4 initializes to
					              // maxing health/stam/mana to maxes allowed by player
            "health",
            "stamina",
            "mana",
            // spells
            "openSpell",
            "detectSpell",
            "triggerSpell",
            "dazzleSpell",
            "zapSpell",
            "calmSpell",
            "flameDartSpell",
            "fetchSpell",
            "forceSpell",
            "levitateSpell",
            "reversalSpell",
            "jugglingLightsSpell",
            "summonStaffSpell", // confirmed with debugger
            "lightningSpell",
            "frostSpell",
            "ritualOfReleaseSpell",
            "invisibilitySpell",
            "auraSpell",
            "protectionSpell",
            "resistanceSpell",
            "glideSpell",
            "healSpell",
        };
    }
}
