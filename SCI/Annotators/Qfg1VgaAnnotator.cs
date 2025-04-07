using System;
using System.Collections.Generic;
using System.Linq;

namespace SCI.Annotators
{
    class Qfg1VgaAnnotator : GameAnnotator
    {
        public override void Run()
        {
            // DOS version doesn't have script 341
            bool isDos = Game.Scripts.All(s => s.Number != 341);

            RunEarly();
            GlobalRenamer.Run(Game, globals);
            GlobalRenamer.Run(Game, isDos ? dosGlobals : macGlobals);
            ExportRenamer.Run(Game, exports);
            VerbAnnotator.Run(Game, verbs);
            InventoryAnnotator.Run(Game, items);
            RunLate();

            QfgSkillAnnotator.Run(Game, 125, skills, new[] {
                Game.GetExport(814, 15), // TrySkill
                Game.GetExport(814, 16), // SkillUsed
            });
            Qfg12GaitAnnotator.Run(Game); // after globals are renamed
            Qfg1VgaDeathAnnotator.Run(Game, MessageFinder);
            GlobalEnumAnnotator.Run(Game, "gHeroType", characterClasses);
        }

        static IReadOnlyDictionary<int, string> globals = new Dictionary<int, string>
        {
            { 116, "gClock" },
            { 117, "gNight" },
            { 118, "gDay" },
            { 119, "gTimeOfDay" },

            { 122, "gHeroType" },
            { 125, "gEgoStats" },

            { 199, "gFreeMeals" },
        };

        static IReadOnlyDictionary<int, string> dosGlobals = new Dictionary<int, string>
        {
            { 100, "gEgoGait" },
        };

        static IReadOnlyDictionary<int, string> macGlobals = new Dictionary<int, string>
        {
            { 289, "gEgoGait" },
        };

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
            { Tuple.Create(0, 2), "HandsOff" },
            { Tuple.Create(0, 3), "HandsOn" },

            { Tuple.Create(0, 5), "SetFlag" },
            { Tuple.Create(0, 6), "ClearFlag" },
            { Tuple.Create(0, 7), "IsFlag" },

            { Tuple.Create(1, 0), "InitGame" },
            { Tuple.Create(1, 1), "InitGlobals" },

            { Tuple.Create(7, 0), "EgoSleeps" },
            { Tuple.Create(7, 1), "CanSleep" },
            { Tuple.Create(7, 2), "PromptSleep" },
            { Tuple.Create(8, 0), "EgoRests" },

            { Tuple.Create(100, 0), "CastDart" },
            { Tuple.Create(101, 0), "ThrowKnife" },
            { Tuple.Create(102, 0), "ThrowRock" },
            { Tuple.Create(104, 0), "CastCalm" },
            { Tuple.Create(105, 0), "CastOpen" },
            { Tuple.Create(106, 0), "CastDazzle" },

            { Tuple.Create(813, 0), "AdvanceTime" },

            { Tuple.Create(814, 0), "EgoDead" },
            { Tuple.Create(814, 1), "PrintTimeAndDay" },
            { Tuple.Create(814, 2), "EgoGait" },
            { Tuple.Create(814, 3), "NormalEgo" },
            { Tuple.Create(814, 5), "AlreadyDone" },
            { Tuple.Create(814, 6), "CantDo" },
            { Tuple.Create(814, 7), "DontHave" },
            { Tuple.Create(814, 8), "RedrawCast" },
            { Tuple.Create(814, 9), "HaveMem" },
            { Tuple.Create(814, 10), "Face" },
            { Tuple.Create(814, 11), "HaveMoney" },
            { Tuple.Create(814, 12), "FixTime" },
            { Tuple.Create(814, 13), "NextDay" },
            { Tuple.Create(814, 14), "Random100" },
            { Tuple.Create(814, 15), "TrySkill" },
            { Tuple.Create(814, 16), "SkillUsed" },
            { Tuple.Create(814, 17), "UseStamina" },
            { Tuple.Create(814, 18), "UseMana" },
            { Tuple.Create(814, 19), "TakeDamage" },
            { Tuple.Create(814, 20), "MaxStamina" },
            { Tuple.Create(814, 21), "MaxHealth" },
            { Tuple.Create(814, 22), "MaxMana" },
            { Tuple.Create(814, 23), "MaxLoad" },
            { Tuple.Create(814, 24), "CastSpell" },
            { Tuple.Create(814, 25), "SoundFX" },
            { Tuple.Create(814, 26), "SolvePuzzle" },
            { Tuple.Create(814, 27), "EatMeal" },
            { Tuple.Create(814, 28), "WtCarried" },
            { Tuple.Create(814, 29), "CanPickLocks" },
            // icon stuff
            { Tuple.Create(814, 33), "CastArea" },
        };

        static IReadOnlyDictionary<int, string> verbs = new Dictionary<int, string>
        {
            { 1, "Look" },
            { 3, "Walk" },
            { 4, "Do" },
            { 2, "Talk" },

            { 10, "silver" },
            { 11, "rations" },
            { 12, "sword" },
            { 13, "chainMail" },
            { 14, "leather" },
            { 15, "shield" },
            { 16, "dagger" },
            { 17, "lockPick" },
            { 18, "thiefKit" },
            { 19, "thiefLicense" },
            { 20, "rock" },
            { 21, "flask" },
            { 22, "healingPotion" },
            { 23, "manaPotion" },
            { 24, "staminaPotion" },
            { 25, "disenchant" },
            { 26, "brassKey" },
            { 27, "magicGem" },
            { 28, "ring" },
            { 29, "ghostOil" },
            { 30, "magicMirror" },
            { 31, "mandrake" },
            { 32, "fruit" },
            { 33, "vegetables" },
            { 34, "acorn" },
            { 35, "seed" },
            { 36, "flowers" },
            { 37, "greenFur" },
            { 38, "fairyDust" },
            { 39, "flyingWater" },
            { 40, "mushroom" },
            { 41, "vase" },
            { 42, "candelabra" },
            { 43, "musicBox" },
            { 44, "candleSticks" },
            { 45, "pearls" },
            { 46, "cheetaurClaw" },
            { 47, "trollBeard" },
            { 49, "gold" },
            { 53, "paper" },

            { 50, "detectMagicSpell" },
            { 51, "openSpell" },
            { 77, "triggerSpell or Pickup Inventory" }, // uh-oh they re-used
            { 78, "dazzleSpell" },
            { 79, "zapSpell" },
            { 80, "calmSpell" },
            { 81, "flameDartSpell" },
            { 82, "fetchSpell" },

            { 52, "Sleep" },
            { 73, "Drop Inventory" },

            // verbs they removed, handlers/messages remain
            { 48, "Smell [ REMOVED ]" },
            { 57, "Talk [ REMOVED ]" },
        };

        static string[] items =
        {
            "silver",
            "rations",
            "sword",
            "chainMail",
            "leather",
            "shield",
            "dagger",
            "lockPick",
            "thiefKit",
            "thiefLicense",
            "rock",
            "flask",
            "healingPotion",
            "manaPotion",
            "staminaPotion",
            "disenchant",
            "brassKey",
            "magicGem",
            "ring",
            "ghostOil",
            "magicMirror",
            "mandrake",
            "fruit",
            "vegetables",
            "acorn",
            "seed",
            "flowers",
            "greenFur",
            "fairyDust",
            "flyingWater",
            "mushroom",
            "vase",
            "candelabra",
            "musicBox",
            "candleSticks",
            "pearls",
            "cheetaurClaw",
            "trollBeard",
            "gold",
            "paper",
            "invPageDown",
            "invPageUp",
            "invLook",
            "invSelect",
            "invDrop",
            "invPickup",
            "invWeight",
            "ok",
            "invHelp",
        };

        static Dictionary<int, string> characterClasses = new Dictionary<int, string>
        {
            { 0, "Fighter" },
            { 1, "Magic User" },
            { 2, "Thief" },
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
        };
    }
}
