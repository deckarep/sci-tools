using System;
using System.Collections.Generic;
using SCI.Annotators.Original;

namespace SCI.Annotators
{
    class Qfg1EgaAnnotator : GameAnnotator
    {
        public override void Run()
        {
            RunEarly();
            GlobalRenamer.Run(Game, globals);
            ExportRenamer.Run(Game, exports);
            InventoryAnnotator.Run(Game, items);
            Sci0InventoryAnnotator.Run(Game, items);
            RunLate();

            QfgSkillAnnotator.Run(Game, 139, skills, new[] {
                Game.GetExport(0, 30), // TrySkill
                Game.GetExport(0, 31), // SkillUsed
            });
            Qfg12InventoryAnnotator.Run(Game, items); // after globals are renamed
            Qfg12GaitAnnotator.Run(Game); // after globals are renamed
            Qfg1EgaDeathAnnotator.Run(Game, TextMessageFinder);
            GlobalEnumAnnotator.Run(Game, "gHeroType", characterClasses);
        }

        protected override Dictionary<int, Original.Script[]> GameHeaders { get { return Qfg1Symbols.Headers; } }

        static IReadOnlyCollection<PrintTextDef> printTextFunctions = new[]
        {
            new PrintTextDef("HighPrint"),
            new PrintTextDef("LowPrint"),
            new PrintTextDef("CenterPrint"),
            new PrintTextDef("TimePrint", 1),
            new PrintTextDef(803, 0, 1), // (say object message), always one param before message in this game
        };
        protected override IReadOnlyCollection<PrintTextDef> PrintTextFunctions { get { return printTextFunctions; } }

        static Dictionary<int, string> globals = new Dictionary<int, string>
        {
            { 100, "gEgoGait" },

            { 130, "gClock" },
            { 131, "gNight" },
            { 132, "gDay" },
            { 133, "gTimeOfDay" }, // "timeODay" in original; it's an enum [ could annotate this ]

            { 136, "gHeroType" },
            { 137, "gHowFast" },
            { 139, "gEgoStats" },

            { 219, "gFreeMeals" },
            { 222, "gOldStats" },

            { 441, "gInvNum" },
            { 491, "gInvDropped" },
            { 541, "gInvWeight" },
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

        static IReadOnlyDictionary<Tuple<int, int>, string> exports = new Dictionary<Tuple<int, int>, string>()
        {
        };

        static string[] items =
        {
            "", // the indexes are all 1-based
            "silver",
            "gold",
            "food",
            "mandrake",
            "key",
            "blade",
            "dagger",
            "leather",
            "shield",
            "note",
            "apple",
            "carrot",
            "magic gem",
            "vase",
            "candelabra",
            "music box",
            "candlestick",
            "pearl",
            "healer's ring",
            "seed",
            "boulder",
            "flower",
            "lockpick",
            "thief kit",
            "thief certificate",
            "empty bottle",
            "green fur",
            "faerie dust",
            "water",
            "magic mushroom",
            "cheetaur claw",
            "troll beard",
            "chainmail",
            "healing",
            "mana potion",
            "vigor potion",
            "hero potion",
            "disenchant potion",
            "grease",
            "mirror",
            "acorn"
        };
    }
}
