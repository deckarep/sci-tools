using System.Collections.Generic;
using FunctionType = SCI.Language.FunctionType;

namespace SCI.Annotators.Original
{
    static class Lsl3Symbols
    {
        public static Dictionary<int, Script[]> Headers = new Dictionary<int, Script[]> {
            { 0, new Script[] {
                new Script {
                    Number = 0,
                    Exports = new Dictionary<int, string> {
                        { 0, "LSL3" },
                        { 1, "NormalEgo" },
                        { 2, "NormalActor" },
                        { 3, "HandsOff" },
                        { 4, "HandsOn" },
                        { 5, "cls" },
                        { 6, "Ok" },
                        { 7, "ItIs" },
                        { 8, "YouAre" },
                        { 9, "NotNow" },
                        { 10, "NotClose" },
                        { 11, "AlreadyTook" },
                        { 12, "DontHave" },
                        { 13, "Notify" },
                        { 14, "HaveMem" },
                        { 15, "AddActorToPic" },
                        { 16, "SetRgTimer" },
                        { 17, "LogIt" },
                        { 18, "LameResponse" },
                        { 19, "SetFlag" },
                        { 20, "ClearFlag" },
                        { 21, "ToggleFlag" },
                        { 22, "TestFlag" },
                        { 23, "InRoom" },
                        { 24, "PutInRoom" },
                        { 25, "PrintDelay" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "NormalEgo",
                            Parameters = new string[] {
                                "theLoop",
                                "theView",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "NormalActor",
                            Parameters = new string[] {
                                "who",
                                "theLoop",
                                "theView",
                            },
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
                            Name = "cls",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Ok",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ItIs",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "YouAre",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "NotNow",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "NotClose",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "AlreadyTook",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "DontHave",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Notify",
                            Parameters = new string[] {
                                "whom",
                            },
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
                            Name = "AddActorToPic",
                            Parameters = new string[] {
                                "whom",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SetRgTimer",
                            Parameters = new string[] {
                                "name",
                                "minutes",
                                "seconds",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "LogIt",
                            Temps = new Variable[] {
                                new Variable(0, 10, "string"),
                                new Variable(10, 60, "string1"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "LameResponse",
                            Temps = new Variable[] {
                                new Variable(0, 50, "string"),
                            }
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
                            Name = "ToggleFlag",
                            Parameters = new string[] {
                                "flag",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "TestFlag",
                            Parameters = new string[] {
                                "flag",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "InRoom",
                            Parameters = new string[] {
                                "iEnum",
                                "room",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PutInRoom",
                            Parameters = new string[] {
                                "iEnum",
                                "room",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintDelay",
                            Parameters = new string[] {
                                "string",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL3",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "testRoom"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL3",
                            Name = "replay",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL3",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                                "style",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL3",
                            Name = "startRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL3",
                            Name = "changeScore",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL3",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 50, "string"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL3",
                            Name = "wordFail",
                            Parameters = new string[] {
                                "word",
                                "input",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 50, "string"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL3",
                            Name = "syntaxFail",
                            Parameters = new string[] {
                                "input",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 40, "string"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL3",
                            Name = "pragmaFail",
                            Parameters = new string[] {
                                "input",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 40, "string"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL3",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "theLine"),
                                new Variable(2, 1, "theObj"),
                                new Variable(3, 1, "xyWindow"),
                                new Variable(4, 1, "evt"),
                                new Variable(5, 1, "fd"),
                                new Variable(6, 50, "string"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Iitem",
                            Name = "showSelf",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Credit Card",
                            Name = "saidMe",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Granadilla Wood_",
                            Name = "saidMe",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Native Grass",
                            Name = "saidMe",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "A Twenty Dollar Bill",
                            Name = "saidMe",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "some Orchids",
                            Name = "saidMe",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Bottle of Wine_",
                            Name = "saidMe",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Marijuana",
                            Name = "saidMe",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "statusCode",
                            Name = "doit",
                            Parameters = new string[] {
                                "str",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theWindow",
                            Name = "open",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "NormalBase",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "w"),
                            }
                        },
                    }
                },
            } },
            { 20, new Script[] {
                new Script {
                    Number = 20,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm20" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm20",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "theObj"),
                                new Variable(2, 1, "xyWindow"),
                                new Variable(3, 1, "evt"),
                                new Variable(4, 30, "string"),
                                new Variable(34, 30, "string1"),
                                new Variable(64, 30, "string2"),
                            }
                        },
                    }
                },
            } },
            { 21, new Script[] {
                new Script {
                    Number = 21,
                    Exports = new Dictionary<int, string> {
                        { 1, "ShowState" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ShowState",
                            Parameters = new string[] {
                                "whatScript",
                                "newState",
                                "where",
                                "color",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 33, "str"),
                            }
                        },
                    }
                },
            } },
            { 22, new Script[] {
                new Script {
                    Number = 22,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm22" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm22",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "theObj"),
                                new Variable(2, 1, "xyWindow"),
                                new Variable(3, 1, "evt"),
                                new Variable(4, 30, "string"),
                                new Variable(34, 30, "string1"),
                                new Variable(64, 30, "string2"),
                            }
                        },
                    }
                },
            } },
            { 30, new Script[] {
                new Script {
                    Number = 30,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm30" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm30",
                            Name = "init",
                        },
                    }
                },
            } },
            { 40, new Script[] {
                new Script {
                    Number = 40,
                    Exports = new Dictionary<int, string> {
                        { 0, "DyingScript" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "DyingScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 100, "string"),
                                new Variable(100, 33, "string2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "deadIcon",
                            Name = "init",
                        },
                    }
                },
            } },
            { 41, new Script[] {
                new Script {
                    Number = 41,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm41" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "fallPri") },
                        { 1, new Variable(1, 1, "destY") },
                        { 2, new Variable(2, 1, "restoreX") },
                        { 3, new Variable(3, 1, "restoreY") },
                        { 4, new Variable(4, 44, "string") },
                        { 48, new Variable(48, 22, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm41",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm41",
                            Name = "notify",
                            Parameters = new string[] {
                                "fallingPriority",
                                "destinationY",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "FallScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "FallScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theJump",
                            Name = "init",
                        },
                    }
                },
                new Script {
                    Number = 41,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm41" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "fallPri") },
                        { 1, new Variable(1, 1, "destY") },
                        { 2, new Variable(2, 1, "restoreX") },
                        { 3, new Variable(3, 1, "restoreY") },
                        { 4, new Variable(4, 132, "string") },
                        { 136, new Variable(136, 66, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm41",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm41",
                            Name = "notify",
                            Parameters = new string[] {
                                "fallingPriority",
                                "destinationY",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "FallScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "FallScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theJump",
                            Name = "init",
                        },
                    }
                },
            } },
            { 42, new Script[] {
                new Script {
                    Number = 42,
                    Exports = new Dictionary<int, string> {
                        { 0, "LeiingScript" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "oldIllegalBits") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LeiingScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 43, new Script[] {
                new Script {
                    Number = 43,
                    Exports = new Dictionary<int, string> {
                        { 0, "CarvingScript" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "oldIllegalBits") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "CarvingScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 44, new Script[] {
                new Script {
                    Number = 44,
                    Exports = new Dictionary<int, string> {
                        { 0, "WeavingScript" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "oldIllegalBits") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "WeavingScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 45, new Script[] {
                new Script {
                    Number = 45,
                    Exports = new Dictionary<int, string> {
                        { 0, "LockerScript" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 20, "name1") },
                        { 20, new Variable(20, 20, "name2") },
                        { 40, new Variable(40, 20, "name3") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LockerScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 46, new Script[] {
                new Script {
                    Number = 46,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm046" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm046",
                            Name = "init",
                        },
                    }
                },
            } },
            { 50, new Script[] {
                new Script {
                    Number = 50,
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Door",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Door",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Door",
                            Name = "open",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Door",
                            Name = "close",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Door",
                            Name = "cue",
                        },
                    }
                },
            } },
            { 51, new Script[] {
                new Script {
                    Number = 51,
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "AutoDoor",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "AutoDoor",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "AutoDoor",
                            Name = "open",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "AutoDoor",
                            Name = "close",
                        },
                    }
                },
            } },
            { 70, new Script[] {
                new Script {
                    Number = 70,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm70" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm70",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 71, new Script[] {
                new Script {
                    Number = 71,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm71" },
                        { 1, "React" },
                        { 2, "PrintL" },
                        { 3, "PrintP" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "takeCycles") },
                        { 1, new Variable(1, 30, "titleStr") },
                        { 31, new Variable(31, 5, "nameStr") },
                        { 36, new Variable(36, 300, "responseBuffer") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "React",
                            Parameters = new string[] {
                                "how",
                                "howLong",
                                "txt",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintL",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintP",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm71",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm71",
                            Name = "notify",
                            Parameters = new string[] {
                                "whom",
                                "x1",
                                "y1",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm71",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RegionScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RegionScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theObj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RegionScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "EyeScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "EyelidScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "NoseScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MouthScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aEyeWest",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aEyeEast",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aEyelidWest",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aEyelidEast",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aNose",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aMouth",
                            Name = "init",
                        },
                    }
                },
                new Script {
                    Number = 71,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm71" },
                        { 1, "React" },
                        { 2, "PrintL" },
                        { 3, "PrintP" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "takeCycles") },
                        { 1, new Variable(1, 60, "titleStr") },
                        { 61, new Variable(61, 30, "nameStr") },
                        { 91, new Variable(91, 600, "responseBuffer") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "React",
                            Parameters = new string[] {
                                "how",
                                "howLong",
                                "txt",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintL",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintP",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm71",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm71",
                            Name = "notify",
                            Parameters = new string[] {
                                "whom",
                                "x1",
                                "y1",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm71",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RegionScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RegionScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theObj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RegionScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "EyeScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "EyelidScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "NoseScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MouthScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aEyeWest",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aEyeEast",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aEyelidWest",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aEyelidEast",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aNose",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aMouth",
                            Name = "init",
                        },
                    }
                },
            } },
            { 80, new Script[] {
                new Script {
                    Number = 80,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm80" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm80",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 81, new Script[] {
                new Script {
                    Number = 81,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm81" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm81",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 90, new Script[] {
                new Script {
                    Number = 90,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm90" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm90",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm90",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 120, new Script[] {
                new Script {
                    Number = 120,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm120" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "heardSong") },
                        { 1, new Variable(1, 20, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ShadowPrint",
                            Parameters = new string[] {
                                "x",
                                "y",
                                "c",
                                "f",
                                "ptr",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm120",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 120,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm120" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "heardSong") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ShadowPrint",
                            Parameters = new string[] {
                                "x",
                                "y",
                                "c",
                                "f",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm120",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 130, new Script[] {
                new Script {
                    Number = 130,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm130" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "pixelsPerFloor") },
                        { 1, new Variable(1, 1, "numberOfFloors") },
                        { 2, new Variable(2, 1, "houseCount") },
                        { 3, new Variable(3, 42, "houseXY") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "BuildHouse",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "AddBuildingToPic",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ChangeFloors",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm130",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aWorkers",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBuilding",
                            Name = "init",
                        },
                    }
                },
            } },
            { 140, new Script[] {
                new Script {
                    Number = 140,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm140" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "tryCount") },
                        { 1, new Variable(1, 1, "atY") },
                        { 2, new Variable(2, 1, "correct") },
                        { 3, new Variable(3, 1, "suitCel") },
                        { 4, new Variable(4, 1, "question") },
                        { 5, new Variable(5, 1, "hisAnswer") },
                        { 6, new Variable(6, 1, "theAnswer") },
                        { 7, new Variable(7, 1, "thisFile") },
                        { 8, new Variable(8, 1, "theKey") },
                        { 9, new Variable(9, 99, "filesSeen") },
                        { 108, new Variable(108, 300, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "HighLight",
                            Parameters = new string[] {
                                "i",
                                "c",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 200, "tmpString"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "MouseInRect",
                            Parameters = new string[] {
                                "event",
                                "left",
                                "top",
                                "right",
                                "bottom",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "evX"),
                                new Variable(1, 1, "evY"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SetFlg140",
                            Parameters = new string[] {
                                "flag",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "TstFlg140",
                            Parameters = new string[] {
                                "flag",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "LoadSetup",
                            Temps = new Variable[] {
                                new Variable(0, 1, "questionFile"),
                                new Variable(1, 1, "soundState"),
                                new Variable(2, 30, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SaveSetup",
                            Temps = new Variable[] {
                                new Variable(0, 40, "str"),
                                new Variable(40, 1, "questionFile"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CLS",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm140",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "evMod"),
                                new Variable(1, 33, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "age"),
                                new Variable(1, 1, "i"),
                                new Variable(2, 1, "filthFile"),
                                new Variable(3, 200, "tmpString"),
                            }
                        },
                    }
                },
                new Script {
                    Number = 140,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm140" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "tryCount") },
                        { 1, new Variable(1, 1, "atY") },
                        { 2, new Variable(2, 1, "correct") },
                        { 3, new Variable(3, 1, "suitCel") },
                        { 4, new Variable(4, 1, "question") },
                        { 5, new Variable(5, 1, "hisAnswer") },
                        { 6, new Variable(6, 1, "theAnswer") },
                        { 7, new Variable(7, 1, "thisFile") },
                        { 8, new Variable(8, 1, "theKey") },
                        { 9, new Variable(9, 99, "filesSeen") },
                        { 108, new Variable(108, 300, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "HighLight",
                            Parameters = new string[] {
                                "i",
                                "c",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 600, "tmpString"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "MouseInRect",
                            Parameters = new string[] {
                                "event",
                                "left",
                                "top",
                                "right",
                                "bottom",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "evX"),
                                new Variable(1, 1, "evY"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SetFlg140",
                            Parameters = new string[] {
                                "flag",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "TstFlg140",
                            Parameters = new string[] {
                                "flag",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "LoadSetup",
                            Temps = new Variable[] {
                                new Variable(0, 1, "soundState"),
                                new Variable(1, 30, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SaveSetup",
                            Temps = new Variable[] {
                                new Variable(0, 40, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CLS",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm140",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "evMod"),
                                new Variable(1, 33, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "age"),
                                new Variable(1, 1, "i"),
                                new Variable(2, 1, "filthFile"),
                                new Variable(3, 600, "tmpString"),
                            }
                        },
                    }
                },
            } },
            { 141, new Script[] {
                new Script {
                    Number = 141,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm141" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm141",
                            Name = "init",
                        },
                    }
                },
            } },
            { 142, new Script[] {
                new Script {
                    Number = 142,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm142" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm142",
                            Name = "init",
                        },
                    }
                },
            } },
            { 143, new Script[] {
                new Script {
                    Number = 143,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm143" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm143",
                            Name = "init",
                        },
                    }
                },
            } },
            { 144, new Script[] {
                new Script {
                    Number = 144,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm144" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm144",
                            Name = "init",
                        },
                    }
                },
            } },
            { 145, new Script[] {
                new Script {
                    Number = 145,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm145" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm145",
                            Name = "init",
                        },
                    }
                },
            } },
            { 146, new Script[] {
                new Script {
                    Number = 146,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm146" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm146",
                            Name = "init",
                        },
                    }
                },
            } },
            { 147, new Script[] {
                new Script {
                    Number = 147,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm147" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm147",
                            Name = "init",
                        },
                    }
                },
            } },
            { 148, new Script[] {
                new Script {
                    Number = 148,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm148" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm148",
                            Name = "init",
                        },
                    }
                },
            } },
            { 149, new Script[] {
                new Script {
                    Number = 149,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm149" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm149",
                            Name = "init",
                        },
                    }
                },
            } },
            { 150, new Script[] {
                new Script {
                    Number = 150,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm150" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm150",
                            Name = "init",
                        },
                    }
                },
            } },
            { 151, new Script[] {
                new Script {
                    Number = 151,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm151" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm151",
                            Name = "init",
                        },
                    }
                },
            } },
            { 152, new Script[] {
                new Script {
                    Number = 152,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm152" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm152",
                            Name = "init",
                        },
                    }
                },
            } },
            { 153, new Script[] {
                new Script {
                    Number = 153,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm153" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm153",
                            Name = "init",
                        },
                    }
                },
            } },
            { 154, new Script[] {
                new Script {
                    Number = 154,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm154" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm154",
                            Name = "init",
                        },
                    }
                },
            } },
            { 155, new Script[] {
                new Script {
                    Number = 155,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm155" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm155",
                            Name = "init",
                        },
                    }
                },
            } },
            { 156, new Script[] {
                new Script {
                    Number = 156,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm156" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm156",
                            Name = "init",
                        },
                    }
                },
            } },
            { 157, new Script[] {
                new Script {
                    Number = 157,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm157" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm157",
                            Name = "init",
                        },
                    }
                },
            } },
            { 158, new Script[] {
                new Script {
                    Number = 158,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm158" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm158",
                            Name = "init",
                        },
                    }
                },
            } },
            { 159, new Script[] {
                new Script {
                    Number = 159,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm159" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm159",
                            Name = "init",
                        },
                    }
                },
            } },
            { 160, new Script[] {
                new Script {
                    Number = 160,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm160" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm160",
                            Name = "init",
                        },
                    }
                },
            } },
            { 161, new Script[] {
                new Script {
                    Number = 161,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm161" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm161",
                            Name = "init",
                        },
                    }
                },
            } },
            { 162, new Script[] {
                new Script {
                    Number = 162,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm162" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm162",
                            Name = "init",
                        },
                    }
                },
            } },
            { 163, new Script[] {
                new Script {
                    Number = 163,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm163" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm163",
                            Name = "init",
                        },
                    }
                },
            } },
            { 164, new Script[] {
                new Script {
                    Number = 164,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm164" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm164",
                            Name = "init",
                        },
                    }
                },
            } },
            { 165, new Script[] {
                new Script {
                    Number = 165,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm165" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm165",
                            Name = "init",
                        },
                    }
                },
            } },
            { 166, new Script[] {
                new Script {
                    Number = 166,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm166" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm166",
                            Name = "init",
                        },
                    }
                },
            } },
            { 200, new Script[] {
                new Script {
                    Number = 200,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm200" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 222, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintPlot",
                            Temps = new Variable[] {
                                new Variable(0, 1, "t"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm200",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCredit1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCredit2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "CreditsScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 203, new Script[] {
                new Script {
                    Number = 203,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm203" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 30, "string") },
                        { 30, new Variable(30, 1, "letteringColor") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ShadowPrint",
                            Parameters = new string[] {
                                "x",
                                "y",
                                "c",
                                "f",
                                "ptr",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintPlaque",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm203",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
                new Script {
                    Number = 203,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm203" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 30, "string") },
                        { 30, new Variable(30, 1, "letteringColor") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ShadowPrint",
                            Parameters = new string[] {
                                "x",
                                "y",
                                "c",
                                "f",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintPlaque",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm203",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 206, new Script[] {
                new Script {
                    Number = 206,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm206" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "stripperState") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm206",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aShade1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aShade2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aShade3",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aGirl",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aGull",
                            Name = "init",
                        },
                    }
                },
            } },
            { 210, new Script[] {
                new Script {
                    Number = 210,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm210" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 200, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintPlot",
                            Temps = new Variable[] {
                                new Variable(0, 1, "t"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm210",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCredit1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCredit2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "CreditsScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 213, new Script[] {
                new Script {
                    Number = 213,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm213" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "havePaper") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm213",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm213",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aTv",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRiver",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRiver2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aNewspaper",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aNewspaper",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCredit1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCredit2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "CreditsScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 216, new Script[] {
                new Script {
                    Number = 216,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm216" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "girlTalkCount") },
                        { 1, new Variable(1, 1, "boxOpen") },
                        { 2, new Variable(2, 222, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintKalalau",
                            Temps = new Variable[] {
                                new Variable(0, 1, "t"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Print216",
                            Temps = new Variable[] {
                                new Variable(0, 1, "t"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm216",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aKandBB",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "KandBBScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "KandBBScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCredit1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCredit2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "CreditsScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 220, new Script[] {
                new Script {
                    Number = 220,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm220" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 222, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintPlot",
                            Temps = new Variable[] {
                                new Variable(0, 1, "t"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm220",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCredit1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCredit2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "CreditsScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBooth",
                            Name = "init",
                        },
                    }
                },
            } },
            { 230, new Script[] {
                new Script {
                    Number = 230,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm230" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "beenWarned") },
                        { 2, new Variable(2, 200, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintMD",
                            Parameters = new string[] {
                                "v",
                                "l",
                                "c",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm230",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDoor",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDoor",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aSign",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDoorman",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "DoormanScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theObj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "DoormanScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theLine"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "BJicon",
                            Name = "init",
                        },
                    }
                },
            } },
            { 235, new Script[] {
                new Script {
                    Number = 235,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm235" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm235",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 240, new Script[] {
                new Script {
                    Number = 240,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm240" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm240",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 245, new Script[] {
                new Script {
                    Number = 245,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm245" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm245",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm245",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 250, new Script[] {
                new Script {
                    Number = 250,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm250" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "onSteps") },
                        { 1, new Variable(1, 1, "seenMsg") },
                        { 2, new Variable(2, 1, "seenCasinoMsg") },
                        { 3, new Variable(3, 1, "seenNativesMsg") },
                        { 4, new Variable(4, 1, "sharpenCount") },
                        { 5, new Variable(5, 200, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintPlot",
                            Temps = new Variable[] {
                                new Variable(0, 1, "t"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm250",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aFountain",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCredit1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCredit2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "CreditsScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 253, new Script[] {
                new Script {
                    Number = 253,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm253" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "sinkX") },
                        { 1, new Variable(1, 1, "sinkY") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm253",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aSoap",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "BillScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "BillScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aJodi",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "JodiScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "JodiScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBill",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCredit1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCredit2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "CreditsScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 260, new Script[] {
                new Script {
                    Number = 260,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm260" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "lieDownAfterTakingTowel") },
                        { 1, new Variable(1, 1, "seenMsg") },
                        { 2, new Variable(2, 1, "tawniBusy") },
                        { 3, new Variable(3, 2, "unused") },
                        { 5, new Variable(5, 40, "string") },
                        { 45, new Variable(45, 22, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm260",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "rollX"),
                                new Variable(1, 1, "jumpX"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aTawni",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aTawni",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theObj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "TawniScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aVendor",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "VendorScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "VendorScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aTowel",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLizard",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LizardScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LizardScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LizardScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "humpCycler",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "salesViewer",
                            Name = "doit",
                        },
                    }
                },
                new Script {
                    Number = 260,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm260" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "tawniBusy") },
                        { 2, new Variable(2, 2, "unused") },
                        { 4, new Variable(4, 40, "string") },
                        { 44, new Variable(44, 22, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm260",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "rollX"),
                                new Variable(1, 1, "jumpX"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aTawni",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aTawni",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theObj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "TawniScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aVendor",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "VendorScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "VendorScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aTowel",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLizard",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LizardScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LizardScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LizardScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "humpCycler",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "salesViewer",
                            Name = "doit",
                        },
                    }
                },
                new Script {
                    Number = 260,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm260" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "lieDownAfterTakingTowel") },
                        { 1, new Variable(1, 1, "seenMsg") },
                        { 2, new Variable(2, 1, "tawniBusy") },
                        { 3, new Variable(3, 2, "unused") },
                        { 5, new Variable(5, 120, "string") },
                        { 125, new Variable(125, 66, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm260",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "rollX"),
                                new Variable(1, 1, "jumpX"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aTawni",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aTawni",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theObj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "TawniScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aVendor",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "VendorScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "VendorScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aTowel",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLizard",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LizardScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LizardScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LizardScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "humpCycler",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "salesViewer",
                            Name = "doit",
                        },
                    }
                },
            } },
            { 265, new Script[] {
                new Script {
                    Number = 265,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm265" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm265",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theObj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 266, new Script[] {
                new Script {
                    Number = 266,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm266" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm266",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 290, new Script[] {
                new Script {
                    Number = 290,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm290" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "doneTime") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm290",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "filthFile"),
                                new Variable(1, 9, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm290",
                            Name = "doit",
                        },
                    }
                },
            } },
            { 299, new Script[] {
                new Script {
                    Number = 299,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm299" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm299",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm299",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 300, new Script[] {
                new Script {
                    Number = 300,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm300" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 301, "unused") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm300",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aSpout",
                            Name = "init",
                        },
                    }
                },
                new Script {
                    Number = 300,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm300" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "unused") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm300",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aSpout",
                            Name = "init",
                        },
                    }
                },
            } },
            { 305, new Script[] {
                new Script {
                    Number = 305,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm305" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "theCounter") },
                        { 2, new Variable(2, 305, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm305",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDoor",
                            Name = "init",
                        },
                    }
                },
            } },
            { 310, new Script[] {
                new Script {
                    Number = 310,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm310" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "theCounter") },
                        { 2, new Variable(2, 310, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm310",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDoor",
                            Name = "init",
                        },
                    }
                },
            } },
            { 320, new Script[] {
                new Script {
                    Number = 320,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm320" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 300, "string") },
                        { 300, new Variable(300, 1, "LarrySpoke") },
                        { 301, new Variable(301, 1, "SecretaryState") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm320",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm320",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "SecretaryScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "SecretaryScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aFax",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRoger",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDoor",
                            Name = "init",
                        },
                    }
                },
            } },
            { 323, new Script[] {
                new Script {
                    Number = 323,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm323" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 300, "string") },
                        { 300, new Variable(300, 1, "seenMsg") },
                        { 301, new Variable(301, 1, "SuziState") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm323",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "SuziScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ChairScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aChair",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aSuzi",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aPhone",
                            Name = "init",
                        },
                    }
                },
            } },
            { 324, new Script[] {
                new Script {
                    Number = 324,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm324" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "talkCount") },
                        { 1, new Variable(1, 1, "humpCount") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm324",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aChair",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aSuzi",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aPhone",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aTrapdoor",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDoorSouth",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDoorNorth",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "humpCycler",
                            Name = "doit",
                        },
                    }
                },
            } },
            { 325, new Script[] {
                new Script {
                    Number = 325,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm325" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm325",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 330, new Script[] {
                new Script {
                    Number = 330,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm330" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "clothesOnTable") },
                        { 1, new Variable(1, 1, "seenDaleDance") },
                        { 2, new Variable(2, 1, "DaleState") },
                        { 3, new Variable(3, 1, "currentDrinker") },
                        { 4, new Variable(4, 1, "humpCount") },
                        { 5, new Variable(5, 1, "sitWithPatti") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm330",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDale",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "DaleScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDrinker1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDrinker2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDrinker3",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDrinker4",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drinkerScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCurtain",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aPanties",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aClothes",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "squareBase",
                            Name = "doit",
                            Parameters = new string[] {
                                "whom",
                            },
                        },
                    }
                },
            } },
            { 335, new Script[] {
                new Script {
                    Number = 335,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm335" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "sawWaterClue") },
                        { 1, new Variable(1, 1, "sawBraClue") },
                        { 2, new Variable(2, 1, "sawPantyhoseClue") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm335",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 340, new Script[] {
                new Script {
                    Number = 340,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm340" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 45, "toldJoke") },
                        { 45, new Variable(45, 1, "testCount") },
                        { 46, new Variable(46, 1, "currentDrinker") },
                        { 47, new Variable(47, 1, "whichLoop") },
                        { 48, new Variable(48, 1, "comicOnStage") },
                        { 49, new Variable(49, 30, "string") },
                        { 79, new Variable(79, 10, "ethnicGroup1") },
                        { 89, new Variable(89, 10, "ethnicGroup2") },
                        { 99, new Variable(99, 10, "ethnicGroup3") },
                        { 109, new Variable(109, 1, "group1") },
                        { 110, new Variable(110, 1, "group2") },
                        { 111, new Variable(111, 1, "group3") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm340",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm340",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ComicScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ComicScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 200, "jokeString"),
                                new Variable(200, 4, "jokeArgs"),
                                new Variable(204, 1, "count"),
                                new Variable(205, 1, "line"),
                                new Variable(206, 1, "i"),
                                new Variable(207, 1, "j"),
                                new Variable(208, 1, "k"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drinkerScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDrummer",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBottle",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBillTop",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBill",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aAlTop",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aAl",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLadyUL_Top",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLadyLR_Top",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aManUL_Top",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "talkCycler",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aComic",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aSign",
                            Name = "init",
                        },
                    }
                },
            } },
            { 341, new Script[] {
                new Script {
                    Number = 341,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm341" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm341",
                            Name = "init",
                        },
                    }
                },
            } },
            { 350, new Script[] {
                new Script {
                    Number = 350,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm350" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "DaveTalkCounter") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm350",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aPins",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDave",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "DaveScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "DaveScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "DaveLoop"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDoor",
                            Name = "init",
                        },
                    }
                },
            } },
            { 355, new Script[] {
                new Script {
                    Number = 355,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm355" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm355",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aKen",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tiradeCycler",
                            Name = "doit",
                        },
                    }
                },
            } },
            { 360, new Script[] {
                new Script {
                    Number = 360,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm360" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "messageCount") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm360",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRobin",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ManScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ManScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBambi",
                            Name = "init",
                        },
                    }
                },
            } },
            { 365, new Script[] {
                new Script {
                    Number = 365,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm365" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintBambi",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintLarry",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm365",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBambi",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDoor",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLid",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "humpCycler",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "s"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "egoHumpCycler",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "cs"),
                            }
                        },
                    }
                },
            } },
            { 370, new Script[] {
                new Script {
                    Number = 370,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm370" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "hisCombination1") },
                        { 1, new Variable(1, 1, "hisCombination2") },
                        { 2, new Variable(2, 1, "hisCombination3") },
                        { 3, new Variable(3, 1, "seenMsg") },
                        { 4, new Variable(4, 1, "howFar") },
                        { 5, new Variable(5, 200, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm370",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm370",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aMan1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Man1Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aMan2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Man2Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aMan3",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Man3Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 375, new Script[] {
                new Script {
                    Number = 375,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm375" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm375",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm375",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aWater1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aWater2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aWater3",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aWater4",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDrain",
                            Name = "init",
                        },
                    }
                },
            } },
            { 380, new Script[] {
                new Script {
                    Number = 380,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm380" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "messageNum") },
                        { 1, new Variable(1, 1, "loopNum") },
                        { 2, new Variable(2, 1, "touchedBottom") },
                        { 3, new Variable(3, 1, "waitingToHulk") },
                        { 4, new Variable(4, 1, "previousX") },
                        { 5, new Variable(5, 1, "previousY") },
                        { 6, new Variable(6, 1, "previousPri") },
                        { 7, new Variable(7, 1, "BenchPressMax") },
                        { 8, new Variable(8, 1, "LegCurlsMax") },
                        { 9, new Variable(9, 1, "PullupsMax") },
                        { 10, new Variable(10, 1, "BarPullMax") },
                        { 11, new Variable(11, 1, "curMachine") },
                        { 12, new Variable(12, 1, "workOutState") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "StartExercising",
                            Parameters = new string[] {
                                "onWhat",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "NormalJock",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "UpdateCounters",
                            Temps = new Variable[] {
                                new Variable(0, 11, "string"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm380",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "s"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRoundBar",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBarPullBarView",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLegCurlBar",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDumbbell",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aExtraBar",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aActor1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCenterWeight",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigEgo",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "egoViewer",
                            Name = "doit",
                        },
                    }
                },
            } },
            { 390, new Script[] {
                new Script {
                    Number = 390,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm390" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 200, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintBambi",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintLarry",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm390",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBambi",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "BambiScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "BambiScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aSpeakerLeft",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aSpeakerRight",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLens",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aMike",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aMonitorLeft",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aMonitorLeft",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aMonitorRight",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aMonitorRight",
                            Name = "doit",
                        },
                    }
                },
            } },
            { 395, new Script[] {
                new Script {
                    Number = 395,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm395" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "talkCount") },
                        { 1, new Variable(1, 55, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm395",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm395",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theObj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 399, new Script[] {
                new Script {
                    Number = 399,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm399" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm399",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm399",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 400, new Script[] {
                new Script {
                    Number = 400,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm400" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm400",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 410, new Script[] {
                new Script {
                    Number = 410,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm410" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm410",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 415, new Script[] {
                new Script {
                    Number = 415,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm415" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm415",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm415",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aAlterEgo",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aAlterEgo",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aWalker",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "WalkerScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 416, new Script[] {
                new Script {
                    Number = 416,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm416" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm416",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm416",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aAlterEgo",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aAlterEgo",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aWalker",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "WalkerScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 417, new Script[] {
                new Script {
                    Number = 417,
                    Exports = new Dictionary<int, string> {
                        { 0, "regCasino" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "regCasino",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 420, new Script[] {
                new Script {
                    Number = 420,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm420" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 166, "string") },
                        { 166, new Variable(166, 22, "string2") },
                        { 188, new Variable(188, 1, "pageNum") },
                        { 189, new Variable(189, 1, "passNum") },
                        { 190, new Variable(190, 27, "passNumbers") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintMD",
                            Parameters = new string[] {
                                "v",
                                "l",
                                "c",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm420",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm420",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aMaitreD",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MaitreDScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theObj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MaitreDScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 5, "theLine"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRope",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aManager",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDoor",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCherri",
                            Name = "init",
                        },
                    }
                },
            } },
            { 421, new Script[] {
                new Script {
                    Number = 421,
                    Exports = new Dictionary<int, string> {
                        { 0, "ManagerScript" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "punchCounter") },
                        { 1, new Variable(1, 40, "string") },
                        { 41, new Variable(41, 22, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ManagerScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 421,
                    Exports = new Dictionary<int, string> {
                        { 0, "ManagerScript" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "punchCounter") },
                        { 1, new Variable(1, 120, "string") },
                        { 121, new Variable(121, 66, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ManagerScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 422, new Script[] {
                new Script {
                    Number = 422,
                    Exports = new Dictionary<int, string> {
                        { 0, "CherriScript" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "CherriScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "CherriScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "CherriScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 430, new Script[] {
                new Script {
                    Number = 430,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm430" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aHoist") },
                        { 1, new Variable(1, 1, "aTrapdoor") },
                        { 2, new Variable(2, 1, "aCherri") },
                        { 3, new Variable(3, 6, "aMoney") },
                        { 9, new Variable(9, 6, "aDancer") },
                        { 15, new Variable(15, 14, "dancerPosn") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm430",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCurtain",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MoneyScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 431, new Script[] {
                new Script {
                    Number = 431,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm431" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 6, "aMoney") },
                        { 6, new Variable(6, 6, "aVeggie") },
                        { 12, new Variable(12, 9, "spotlightX") },
                        { 21, new Variable(21, 40, "string") },
                        { 61, new Variable(61, 22, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm431",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCurtain",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "VeggieScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MoneyScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 435, new Script[] {
                new Script {
                    Number = 435,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm435" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "readyForDeed") },
                        { 1, new Variable(1, 1, "seenMsg") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm435",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theObj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 440, new Script[] {
                new Script {
                    Number = 440,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm440" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "humpSeconds") },
                        { 1, new Variable(1, 1, "seenMsg") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintCherri",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintLarry",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm440",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "shadowViewer",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "humpCycler",
                            Name = "doit",
                        },
                    }
                },
            } },
            { 450, new Script[] {
                new Script {
                    Number = 450,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm450" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "elvisWinked") },
                        { 1, new Variable(1, 1, "pattiAtPiano") },
                        { 2, new Variable(2, 1, "request") },
                        { 3, new Variable(3, 1, "seenMsg") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm450",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aElvis",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ElvisScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ElvisScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRoger",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RogerScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RogerScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aTips",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aMarker",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aPatti",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PattiScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PattiScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "howMany"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pianoCycler",
                            Name = "doit",
                        },
                    }
                },
            } },
            { 455, new Script[] {
                new Script {
                    Number = 455,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm455" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm455",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theObj"),
                                new Variable(1, 200, "string"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 460, new Script[] {
                new Script {
                    Number = 460,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm460" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "msgVar") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm460",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ManScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ManScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aMan",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LightScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "destCel"),
                            }
                        },
                    }
                },
            } },
            { 470, new Script[] {
                new Script {
                    Number = 470,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm470" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "destination") },
                        { 1, new Variable(1, 1, "lightCel") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm470",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 50, "string"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "destY"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBeamFront",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBeamRear",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDoor",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aFloor",
                            Name = "init",
                        },
                    }
                },
            } },
            { 480, new Script[] {
                new Script {
                    Number = 480,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm480" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "drinkCounter") },
                        { 1, new Variable(1, 200, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintPatti",
                            Temps = new Variable[] {
                                new Variable(0, 1, "t"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintLarry",
                            Temps = new Variable[] {
                                new Variable(0, 1, "t"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm480",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "destX"),
                                new Variable(1, 1, "destY"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aPatti",
                            Name = "init",
                        },
                    }
                },
            } },
            { 481, new Script[] {
                new Script {
                    Number = 481,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm481" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 200, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Print481",
                            Temps = new Variable[] {
                                new Variable(0, 1, "t"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm481",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 482, new Script[] {
                new Script {
                    Number = 482,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm482" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 222, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm482",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "t"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PrintLarry",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PrintPatti",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aPatti",
                            Name = "init",
                        },
                    }
                },
            } },
            { 483, new Script[] {
                new Script {
                    Number = 483,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm483" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 200, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Print483",
                            Temps = new Variable[] {
                                new Variable(0, 1, "t"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm483",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 484, new Script[] {
                new Script {
                    Number = 484,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm484" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 200, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm484",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm484",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "SheetBase",
                            Name = "doit",
                        },
                    }
                },
                new Script {
                    Number = 484,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm484" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm484",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm484",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "SheetBase",
                            Name = "doit",
                        },
                    }
                },
            } },
            { 499, new Script[] {
                new Script {
                    Number = 499,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm499" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm499",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm499",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm499",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 500, new Script[] {
                new Script {
                    Number = 500,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm500" },
                        { 1, "RedrawMaze" },
                        { 2, "BitSet" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 66, "string") },
                        { 66, new Variable(66, 22, "string2") },
                        { 88, new Variable(88, 5, "blockNorth") },
                        { 93, new Variable(93, 5, "blockSouth") },
                        { 98, new Variable(98, 5, "blockWest") },
                        { 103, new Variable(103, 5, "blockEast") },
                        { 108, new Variable(108, 1, "pic") },
                        { 109, new Variable(109, 1, "lclRoom") },
                        { 110, new Variable(110, 1, "roomCount") },
                        { 111, new Variable(111, 1, "lastEdge") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "RedrawMaze",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "BitSet",
                            Parameters = new string[] {
                                "index",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm500",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "SteadyBase",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "SlowWalk",
                            Name = "doit",
                        },
                    }
                },
            } },
            { 510, new Script[] {
                new Script {
                    Number = 510,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm510" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "drownCount") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm510",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock3",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock4",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock5",
                            Name = "init",
                        },
                    }
                },
            } },
            { 520, new Script[] {
                new Script {
                    Number = 520,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm520" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "drownCount") },
                        { 1, new Variable(1, 50, "string") },
                        { 51, new Variable(51, 22, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm520",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock3",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock4",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock5",
                            Name = "init",
                        },
                    }
                },
                new Script {
                    Number = 520,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm520" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "drownCount") },
                        { 1, new Variable(1, 1, "local1") },
                        { 2, new Variable(2, 50, "string") },
                        { 52, new Variable(52, 22, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm520",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock3",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock4",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock5",
                            Name = "init",
                        },
                    }
                },
                new Script {
                    Number = 520,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm520" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "drownCount") },
                        { 1, new Variable(1, 150, "string") },
                        { 151, new Variable(151, 66, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm520",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock3",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock4",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRock5",
                            Name = "init",
                        },
                    }
                },
            } },
            { 523, new Script[] {
                new Script {
                    Number = 523,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm523" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm523",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 525, new Script[] {
                new Script {
                    Number = 525,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm525" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 40, "string") },
                        { 40, new Variable(40, 22, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm525",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theJump",
                            Name = "init",
                        },
                    }
                },
            } },
            { 530, new Script[] {
                new Script {
                    Number = 530,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm530" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "attempts") },
                        { 1, new Variable(1, 1, "onOtherSide") },
                        { 2, new Variable(2, 1, "moveCounter") },
                        { 3, new Variable(3, 1, "potState") },
                        { 4, new Variable(4, 44, "string") },
                        { 48, new Variable(48, 22, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm530",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRope",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RopeScript",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theJump",
                            Name = "init",
                        },
                    }
                },
                new Script {
                    Number = 530,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm530" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "attempts") },
                        { 1, new Variable(1, 1, "onOtherSide") },
                        { 2, new Variable(2, 1, "moveCounter") },
                        { 3, new Variable(3, 1, "potState") },
                        { 4, new Variable(4, 120, "string") },
                        { 124, new Variable(124, 66, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm530",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRope",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RopeScript",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theJump",
                            Name = "init",
                        },
                    }
                },
            } },
            { 535, new Script[] {
                new Script {
                    Number = 535,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm535" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 77, "string") },
                        { 77, new Variable(77, 22, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm535",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBird1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Bird1Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBird2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Bird2Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBird3",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Bird3Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Bird3Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theJump",
                            Name = "init",
                        },
                    }
                },
            } },
            { 540, new Script[] {
                new Script {
                    Number = 540,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm540" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 33, "string") },
                        { 34, new Variable(34, 22, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm540",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm540",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aPig",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PigScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PigScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PigScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
                new Script {
                    Number = 540,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm540" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "local0") },
                        { 1, new Variable(1, 1, "local1") },
                        { 2, new Variable(2, 1, "seenMsg") },
                        { 3, new Variable(3, 33, "string") },
                        { 36, new Variable(36, 22, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm540",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm540",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aPig",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PigScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PigScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PigScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
                new Script {
                    Number = 540,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm540" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 100, "string") },
                        { 101, new Variable(101, 66, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm540",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm540",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aPig",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PigScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PigScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PigScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 550, new Script[] {
                new Script {
                    Number = 550,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm550" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "drowningLoop") },
                        { 1, new Variable(1, 55, "string") },
                        { 56, new Variable(56, 22, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm550",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PattiViewer",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drowningViewer",
                            Name = "doit",
                        },
                    }
                },
                new Script {
                    Number = 550,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm550" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "drowningLoop") },
                        { 1, new Variable(1, 150, "string") },
                        { 151, new Variable(151, 66, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm550",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PattiViewer",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drowningViewer",
                            Name = "doit",
                        },
                    }
                },
            } },
            { 560, new Script[] {
                new Script {
                    Number = 560,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm560" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "startingScore") },
                        { 2, new Variable(2, 1, "OB_counter") },
                        { 3, new Variable(3, 1, "destX") },
                        { 4, new Variable(4, 1, "theCounter") },
                        { 5, new Variable(5, 1, "killX") },
                        { 6, new Variable(6, 2, "unused") },
                        { 8, new Variable(8, 1, "lastLoop") },
                        { 9, new Variable(9, 40, "string") },
                        { 49, new Variable(49, 22, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm560",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "DotScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDot",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLeftBank",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRightBank",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aObstacle",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLog",
                            Name = "init",
                        },
                    }
                },
                new Script {
                    Number = 560,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm560" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "startingScore") },
                        { 2, new Variable(2, 1, "OB_counter") },
                        { 3, new Variable(3, 1, "destX") },
                        { 4, new Variable(4, 1, "theCounter") },
                        { 5, new Variable(5, 1, "killX") },
                        { 6, new Variable(6, 2, "unused") },
                        { 8, new Variable(8, 1, "lastLoop") },
                        { 9, new Variable(9, 120, "string") },
                        { 129, new Variable(129, 66, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm560",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "DotScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDot",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLeftBank",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRightBank",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aObstacle",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLog",
                            Name = "init",
                        },
                    }
                },
            } },
            { 580, new Script[] {
                new Script {
                    Number = 580,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm580" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm580",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 100, "string"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aWoman2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Woman2Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "HeadTurner",
                            Name = "doit",
                        },
                    }
                },
            } },
            { 585, new Script[] {
                new Script {
                    Number = 585,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm585" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm585",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBowl",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDrummer1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDrummer2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aOldMan",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aWhipper",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aSlave",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCornMan",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ManScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aWoman",
                            Name = "init",
                        },
                    }
                },
            } },
            { 590, new Script[] {
                new Script {
                    Number = 590,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm590" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 88, "string") },
                        { 88, new Variable(88, 22, "string2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm590",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCageFront",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCageBack",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBubblesFront",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBubblesRear",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLarry",
                            Name = "init",
                        },
                    }
                },
            } },
            { 599, new Script[] {
                new Script {
                    Number = 599,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm599" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm599",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm599",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 600, new Script[] {
                new Script {
                    Number = 600,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm600" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm600",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 610, new Script[] {
                new Script {
                    Number = 610,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm610" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm610",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aHole",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCamera",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCar1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aMan1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCar2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aMan2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Man2Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aTowers",
                            Name = "init",
                        },
                    }
                },
            } },
            { 620, new Script[] {
                new Script {
                    Number = 620,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm620" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm620",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm620",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm620",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 630, new Script[] {
                new Script {
                    Number = 630,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm630" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm630",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm630",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aGeneratorBottom",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aGeneratorTop",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aSwitch",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLarry",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "SQ3Base",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "w"),
                            }
                        },
                    }
                },
            } },
            { 640, new Script[] {
                new Script {
                    Number = 640,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm640" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "theCounter") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm640",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRoberta",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRosella",
                            Name = "init",
                        },
                    }
                },
            } },
            { 650, new Script[] {
                new Script {
                    Number = 650,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm650" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "theCounter") },
                        { 1, new Variable(1, 1, "seenMsg") },
                        { 2, new Variable(2, 222, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Print650",
                            Parameters = new string[] {
                                "w",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "t"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm650",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "RoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRightHand",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRightHand",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLeftHand",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLeftHand",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aMonitor",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aMonitor",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLeg",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aLeg",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDoor",
                            Name = "init",
                        },
                    }
                },
            } },
            { 699, new Script[] {
                new Script {
                    Number = 699,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm699" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm699",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 997, new Script[] {
                new Script {
                    Number = 997,
                    Exports = new Dictionary<int, string> {
                        { 1, "TglSound" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "fermata") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "TglSound",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "TheMenuBar",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "TheMenuBar",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "msg"),
                                new Variable(1, 1, "i"),
                                new Variable(2, 1, "t"),
                                new Variable(3, 1, "b"),
                                new Variable(4, 200, "string"),
                                new Variable(204, 20, "string2"),
                            }
                        },
                    }
                },
            } },
        };
    }
}
