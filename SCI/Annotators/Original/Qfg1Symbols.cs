using System.Collections.Generic;
using FunctionType = SCI.Language.FunctionType;

namespace SCI.Annotators.Original
{
    static class Qfg1Symbols
    {
        public static Dictionary<int, Script[]> Headers = new Dictionary<int, Script[]> {
            { 0, new Script[] {
                new Script {
                    Number = 0,
                    Exports = new Dictionary<int, string> {
                        { 0, "HQ" },
                        { 1, "EgoDead" },
                        { 2, "RedrawCast" },
                        { 3, "HaveMem" },
                        { 4, "clr" },
                        { 5, "HandsOff" },
                        { 6, "HandsOn" },
                        { 7, "MouseClaimed" },
                        { 8, "SoundFX" },
                        { 9, "SetFlag" },
                        { 10, "ClearFlag" },
                        { 11, "IsFlag" },
                        { 12, "NextDay" },
                        { 13, "proc0_13" },
                        { 14, "NormalEgo" },
                        { 15, "NotClose" },
                        { 16, "AlreadyDone" },
                        { 17, "DontHave" },
                        { 18, "CantDo" },
                        { 19, "HighPrint" },
                        { 20, "LowPrint" },
                        { 21, "TimePrint" },
                        { 22, "FindTime" },
                        { 23, "CanPickLocks" },
                        { 24, "LookAt" },
                        { 25, "Purchase" },
                        { 26, "FixTime" },
                        { 27, "PromptQuit" },
                        { 28, "UseMana" },
                        { 29, "UseStamina" },
                        { 30, "TrySkill" },
                        { 31, "SkillUsed" },
                        { 32, "SolvePuzzle" },
                        { 35, "Random100" },
                        { 36, "TakeDamage" },
                        { 37, "MaxMana" },
                        { 38, "MaxStamina" },
                        { 39, "MaxHealth" },
                        { 40, "MaxLoad" },
                        { 41, "CastSpell" },
                        { 42, "EgoGait" },
                        { 43, "EatMeal" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "EgoDead",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "RedrawCast",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "HaveMem",
                            Parameters = new string[] {
                                "howMuch",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "clr",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "HandsOff",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "HandsOn",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "MouseClaimed",
                            Parameters = new string[] {
                                "obj",
                                "event",
                                "shifts",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SoundFX",
                            Parameters = new string[] {
                                "soundNum",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SetFlag",
                            Parameters = new string[] {
                                "flag",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ClearFlag",
                            Parameters = new string[] {
                                "flag",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "IsFlag",
                            Parameters = new string[] {
                                "flag",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "NextDay",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "proc0_13",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "NormalEgo",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "NotClose",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "AlreadyDone",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CantDo",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "DontHave",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "HighPrint",
                            Temps = new Variable[] {
                                new Variable(0, 4, "temp0"),
                                new Variable(4, 400, "temp4"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "TimePrint",
                            Parameters = new string[] {
                                "numSeconds",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 4, "temp0"),
                                new Variable(4, 400, "temp4"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "LowPrint",
                            Temps = new Variable[] {
                                new Variable(0, 4, "temp0"),
                                new Variable(4, 400, "temp4"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "FindTime",
                            Temps = new Variable[] {
                                new Variable(0, 1, "whatDay"),
                                new Variable(1, 30, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CanPickLocks",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "LookAt",
                            Parameters = new string[] {
                                "actor1",
                                "actor2",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Purchase",
                            Parameters = new string[] {
                                "itemPrice",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "oldSilver"),
                                new Variable(1, 1, "oldGold"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "FixTime",
                            Parameters = new string[] {
                                "newClock",
                                "newMinutes",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "oldTime"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PromptQuit",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "UseMana",
                            Parameters = new string[] {
                                "pointsUsed",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "UseStamina",
                            Parameters = new string[] {
                                "pointsUsed",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "foo"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "TrySkill",
                            Parameters = new string[] {
                                "skillNum",
                                "difficulty",
                                "bonus",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "skVal"),
                                new Variable(1, 1, "skDiv"),
                                new Variable(2, 1, "skRef"),
                                new Variable(3, 1, "success"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SolvePuzzle",
                            Parameters = new string[] {
                                "pFlag",
                                "pValue",
                                "charType",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SkillUsed",
                            Parameters = new string[] {
                                "skillNum",
                                "learnValue",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "learnSign"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Random100",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "TakeDamage",
                            Parameters = new string[] {
                                "damage",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "temp0"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "MaxMana",
                            Temps = new Variable[] {
                                new Variable(0, 1, "temp0"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "MaxStamina",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "MaxHealth",
                            Temps = new Variable[] {
                                new Variable(0, 1, "temp0"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "MaxLoad",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CastSpell",
                            Parameters = new string[] {
                                "spellNum",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "temp0"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "EgoGait",
                            Parameters = new string[] {
                                "newGait",
                                "gaitMsg",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theView"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "EatMeal",
                        },
                    }
                },
            } },
            { 1, new Script[] {
                new Script {
                    Number = 1,
                    Exports = new Dictionary<int, string> {
                        { 0, "InitGame" },
                        { 1, "InitGlobals" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "InitGame",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "InitGlobals",
                        },
                    }
                },
            } },
            { 5, new Script[] {
                new Script {
                    Number = 5,
                    Exports = new Dictionary<int, string> {
                        { 0, "DrinkPotion" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "DrinkPotion",
                            Parameters = new string[] {
                                "event",
                                "index",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "wPotion"),
                            }
                        },
                    }
                },
            } },
            { 6, new Script[] {
                new Script {
                    Number = 6,
                    Exports = new Dictionary<int, string> {
                        { 0, "Eat" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Eat",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "haveEaten"),
                            }
                        },
                    }
                },
            } },
            { 7, new Script[] {
                new Script {
                    Number = 7,
                    Exports = new Dictionary<int, string> {
                        { 0, "EgoSleeps" },
                        { 1, "CanSleep" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "EgoSleeps",
                            Parameters = new string[] {
                                "theHour",
                                "theMin",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "sleptHours"),
                                new Variable(1, 1, "oldTime"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CanSleep",
                        },
                    }
                },
            } },
            { 8, new Script[] {
                new Script {
                    Number = 8,
                    Exports = new Dictionary<int, string> {
                        { 0, "EgoRests" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "EgoRests",
                            Parameters = new string[] {
                                "restTime",
                                "mess",
                            },
                        },
                    }
                },
            } },
            { 100, new Script[] {
                new Script {
                    Number = 100,
                    Exports = new Dictionary<int, string> {
                        { 0, "CastDart" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CastDart",
                            Parameters = new string[] {
                                "param1",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "temp0"),
                                new Variable(1, 1, "temp1"),
                            }
                        },
                    }
                },
            } },
            { 101, new Script[] {
                new Script {
                    Number = 101,
                    Exports = new Dictionary<int, string> {
                        { 0, "ThrowKnife" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "knTarg") },
                        { 1, new Variable(1, 1, "thisX") },
                        { 2, new Variable(2, 1, "thisY") },
                        { 3, new Variable(3, 1, "knTargX") },
                        { 4, new Variable(4, 1, "knTargY") },
                        { 5, new Variable(5, 1, "gotHit") },
                        { 6, new Variable(6, 1, "savSignal") },
                        { 7, new Variable(7, 1, "savPriority") },
                        { 8, new Variable(8, 1, "savIllegalBits") },
                        { 9, new Variable(9, 1, "projSound") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ThrowKnife",
                            Parameters = new string[] {
                                "atWhat",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "pushX"),
                                new Variable(1, 1, "pushY"),
                                new Variable(2, 1, "projectile"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "knifeScript",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "knifeScript",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "hitFlag"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "knifeScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 101,
                    Exports = new Dictionary<int, string> {
                        { 0, "ThrowKnife" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "knTarg") },
                        { 1, new Variable(1, 1, "thisX") },
                        { 2, new Variable(2, 1, "thisY") },
                        { 3, new Variable(3, 1, "knTargX") },
                        { 4, new Variable(4, 1, "knTargY") },
                        { 5, new Variable(5, 1, "savSignal") },
                        { 6, new Variable(6, 1, "savPriority") },
                        { 7, new Variable(7, 1, "savIllegalBits") },
                        { 8, new Variable(8, 1, "projectile") },
                        { 9, new Variable(9, 1, "projSound") },
                        { 10, new Variable(10, 1, "gotHit") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ThrowKnife",
                            Parameters = new string[] {
                                "atWhat",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "pushX"),
                                new Variable(1, 1, "pushY"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "knifeScript",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "knifeScript",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "hitFlag"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "knifeScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 102, new Script[] {
                new Script {
                    Number = 102,
                    Exports = new Dictionary<int, string> {
                        { 0, "ThrowRock" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ThrowRock",
                            Parameters = new string[] {
                                "atWhat",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "pushX"),
                            }
                        },
                    }
                },
            } },
            { 104, new Script[] {
                new Script {
                    Number = 104,
                    Exports = new Dictionary<int, string> {
                        { 0, "CastCalm" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CastCalm",
                            Parameters = new string[] {
                                "param1",
                                "param2",
                            },
                        },
                    }
                },
            } },
            { 105, new Script[] {
                new Script {
                    Number = 105,
                    Exports = new Dictionary<int, string> {
                        { 0, "CastOpen" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CastOpen",
                            Parameters = new string[] {
                                "param1",
                                "param2",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "temp0"),
                                new Variable(1, 1, "temp1"),
                            }
                        },
                    }
                },
            } },
            { 106, new Script[] {
                new Script {
                    Number = 106,
                    Exports = new Dictionary<int, string> {
                        { 0, "CastDazzle" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CastDazzle",
                            Parameters = new string[] {
                                "param1",
                                "param2",
                            },
                        },
                    }
                },
            } },
            { 238, new Script[] {
                new Script {
                    Number = 238,
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "setDeltaX",
                            Parameters = new string[] {
                                "theDir",
                                "DX",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "setDeltaY",
                            Parameters = new string[] {
                                "theDir",
                                "DY",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeBug",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeBug",
                            Name = "highPri",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeBug",
                            Name = "lowPri",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeBug",
                            Name = "changeForm",
                            Temps = new Variable[] {
                                new Variable(0, 1, "newLoop"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeBug",
                            Name = "fixState",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeBug",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "whichControl"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeBug",
                            Name = "canBeHere",
                            Temps = new Variable[] {
                                new Variable(0, 1, "canWe"),
                                new Variable(1, 1, "ctrls"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeBug",
                            Name = "die",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeBug",
                            Name = "nearRock",
                            Parameters = new string[] {
                                "dist",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeBug",
                            Name = "nearBridge",
                            Parameters = new string[] {
                                "dist",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeBug",
                            Name = "nearLadder",
                            Parameters = new string[] {
                                "dist",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeMove",
                            Name = "init",
                            Parameters = new string[] {
                                "actor",
                                "xTo",
                                "yTo",
                                "toCall",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "cy"),
                                new Variable(1, 1, "theAngle"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeMove",
                            Name = "calcDir",
                            Parameters = new string[] {
                                "theHeading",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeMove",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "clForm"),
                                new Variable(1, 1, "thisControl"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeMove",
                            Name = "doMove",
                            Temps = new Variable[] {
                                new Variable(0, 1, "oldX"),
                                new Variable(1, 1, "oldY"),
                                new Variable(2, 1, "DX"),
                                new Variable(3, 1, "DY"),
                                new Variable(4, 1, "newDir"),
                                new Variable(5, 1, "newAngle"),
                                new Variable(6, 1, "mSpeed"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeMove",
                            Name = "tryStep",
                            Parameters = new string[] {
                                "oldX",
                                "oldY",
                                "theDir",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "DX"),
                                new Variable(1, 1, "DY"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeMove",
                            Name = "chooseRoute",
                            Parameters = new string[] {
                                "wasBlocked",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theAngle"),
                                new Variable(1, 1, "newDir"),
                                new Variable(2, 1, "incr"),
                                new Variable(3, 1, "aDir"),
                                new Variable(4, 1, "sm"),
                                new Variable(5, 1, "best"),
                                new Variable(6, 1, "cur"),
                                new Variable(7, 1, "bestDir"),
                                new Variable(8, 1, "other"),
                                new Variable(9, 1, "forms"),
                                new Variable(10, 1, "dist"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeMove",
                            Name = "checkRoute",
                            Parameters = new string[] {
                                "theDir",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "oldX"),
                                new Variable(1, 1, "oldY"),
                                new Variable(2, 1, "curX"),
                                new Variable(3, 1, "curY"),
                                new Variable(4, 1, "sm"),
                                new Variable(5, 1, "index"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MazeMove",
                            Name = "setHeading",
                            Parameters = new string[] {
                                "newHeading",
                            },
                        },
                    }
                },
            } },
            { 803, new Script[] {
                new Script {
                    Number = 803,
                    Exports = new Dictionary<int, string> {
                        { 0, "Say" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Say",
                            Parameters = new string[] {
                                "whom",
                            },
                        },
                    }
                },
            } },
            { 813, new Script[] {
                new Script {
                    Number = 813,
                    Exports = new Dictionary<int, string> {
                        { 0, "AdvanceTime" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "AdvanceTime",
                            Parameters = new string[] {
                                "addHours",
                                "addMinutes",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "newTime"),
                            }
                        },
                    }
                },
            } },
            { 995, new Script[] {
                new Script {
                    Number = 995,
                    Exports = new Dictionary<int, string> {
                        { 0, "SaidInv" },
                        { 1, "WtCarried" },
                        { 2, "SaidSpell" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SaidInv",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "index"),
                                new Variable(1, 1, "obj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "WtCarried",
                            Temps = new Variable[] {
                                new Variable(0, 1, "tot"),
                                new Variable(1, 1, "index"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SaidSpell",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "index"),
                                new Variable(1, 1, "obj"),
                            }
                        },
                    }
                },
            } },
        };
    }
}
