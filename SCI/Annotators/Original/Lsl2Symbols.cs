using System.Collections.Generic;
using FunctionType = SCI.Language.FunctionType;

namespace SCI.Annotators.Original
{
    static class Lsl2Symbols
    {
        public static Dictionary<int, Script[]> Headers = new Dictionary<int, Script[]> {
            { 0, new Script[] {
                new Script {
                    Number = 0,
                    Exports = new Dictionary<int, string> {
                        { 0, "LSL2" },
                        { 1, "LookAt" },
                        { 2, "NormalEgo" },
                        { 3, "NearControl" },
                        { 4, "AddActorToPic" },
                        { 5, "HandsOff" },
                        { 6, "HandsOn" },
                        { 7, "Notify" },
                        { 8, "HaveMem" },
                        { 9, "RedrawCast" },
                        { 10, "SoundLoops" },
                        { 11, "cls" },
                        { 12, "Ok" },
                        { 13, "ItIs" },
                        { 14, "YouAre" },
                        { 15, "NotNow" },
                        { 16, "NotClose" },
                        { 17, "AlreadyTook" },
                        { 18, "SeeNothing" },
                        { 19, "CantDo" },
                        { 20, "DontHave" },
                        { 21, "SetRgTimer" },
                        { 22, "ShowVersion" },
                    },
                    Functions = new Function[] {
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
                            Name = "NearControl",
                            Parameters = new string[] {
                                "actor",
                                "distance",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "NormalEgo",
                            Parameters = new string[] {
                                "theLoop",
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
                            Name = "HandsOff",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "HandsOn",
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
                            Name = "RedrawCast",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SoundLoops",
                            Parameters = new string[] {
                                "who",
                                "howMany",
                            },
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
                            Name = "SeeNothing",
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
                            Name = "SetRgTimer",
                            Parameters = new string[] {
                                "name",
                                "minutes",
                                "seconds",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ShowVersion",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "testRoom"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "replay",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "changeScore",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "startRoom",
                            Parameters = new string[] {
                                "n",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "newRegion"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "systime"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "wordFail",
                            Parameters = new string[] {
                                "word",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "syntaxFail",
                            Parameters = new string[] {
                                "input",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "pragmaFail",
                            Parameters = new string[] {
                                "input",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "theObj"),
                                new Variable(2, 1, "xyWindow"),
                                new Variable(3, 1, "evt"),
                                new Variable(4, 1, "fd"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Iitem",
                            Name = "showSelf",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dyingScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
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
                    }
                },
                new Script {
                    Number = 0,
                    Exports = new Dictionary<int, string> {
                        { 0, "LSL2" },
                        { 1, "LookAt" },
                        { 2, "NormalEgo" },
                        { 3, "NearControl" },
                        { 4, "AddActorToPic" },
                        { 5, "HandsOff" },
                        { 6, "HandsOn" },
                        { 7, "Notify" },
                        { 8, "HaveMem" },
                        { 9, "RedrawCast" },
                        { 10, "SoundLoops" },
                        { 11, "cls" },
                        { 12, "Ok" },
                        { 13, "ItIs" },
                        { 14, "YouAre" },
                        { 15, "NotNow" },
                        { 16, "NotClose" },
                        { 17, "AlreadyTook" },
                        { 18, "SeeNothing" },
                        { 19, "CantDo" },
                        { 20, "DontHave" },
                        { 21, "SetRgTimer" },
                    },
                    Functions = new Function[] {
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
                            Name = "NearControl",
                            Parameters = new string[] {
                                "actor",
                                "distance",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "NormalEgo",
                            Parameters = new string[] {
                                "theLoop",
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
                            Name = "HandsOff",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "HandsOn",
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
                            Name = "RedrawCast",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SoundLoops",
                            Parameters = new string[] {
                                "who",
                                "howMany",
                            },
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
                            Name = "SeeNothing",
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
                            Name = "SetRgTimer",
                            Parameters = new string[] {
                                "name",
                                "minutes",
                                "seconds",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "testRoom"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "replay",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "changeScore",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "startRoom",
                            Parameters = new string[] {
                                "n",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "newRegion"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "systime"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "wordFail",
                            Parameters = new string[] {
                                "word",
                                "input",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "syntaxFail",
                            Parameters = new string[] {
                                "input",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "pragmaFail",
                            Parameters = new string[] {
                                "input",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL2",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "theObj"),
                                new Variable(2, 1, "xyWindow"),
                                new Variable(3, 1, "evt"),
                                new Variable(4, 1, "fd"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Iitem",
                            Name = "showSelf",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dyingScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
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
                    }
                },
            } },
            { 2, new Script[] {
                new Script {
                    Number = 2,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm2" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm2",
                            Name = "init",
                        },
                    }
                },
            } },
            { 3, new Script[] {
                new Script {
                    Number = 3,
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
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
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
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
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
            { 4, new Script[] {
                new Script {
                    Number = 4,
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "BassSetter",
                            Name = "doit",
                            Parameters = new string[] {
                                "whom",
                            },
                        },
                    }
                },
            } },
            { 5, new Script[] {
                new Script {
                    Number = 5,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm5" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm5",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "theObj"),
                                new Variable(2, 1, "xyWindow"),
                                new Variable(3, 1, "evt"),
                                new Variable(4, 1, "fd"),
                                new Variable(5, 50, "string2"),
                            }
                        },
                    }
                },
            } },
            { 6, new Script[] {
                new Script {
                    Number = 6,
                    Exports = new Dictionary<int, string> {
                        { 0, "airplaneScript" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Airplane",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "airplaneScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 7, new Script[] {
                new Script {
                    Number = 7,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm7" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm7",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm7",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 8, new Script[] {
                new Script {
                    Number = 8,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm8" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMessage") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm8",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm8",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm8",
                            Name = "notify",
                            Parameters = new string[] {
                                "loopCnt",
                            },
                        },
                    }
                },
            } },
            { 9, new Script[] {
                new Script {
                    Number = 9,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm9" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm9",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm9Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm9Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 10, new Script[] {
                new Script {
                    Number = 10,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm10" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "girl") },
                        { 1, new Variable(1, 1, "body") },
                        { 2, new Variable(2, 1, "face") },
                        { 3, new Variable(3, 1, "hair") },
                        { 4, new Variable(4, 1, "ear") },
                        { 5, new Variable(5, 1, "page") },
                        { 6, new Variable(6, 1, "phone") },
                        { 7, new Variable(7, 1, "room") },
                        { 8, new Variable(8, 1, "testing") },
                        { 9, new Variable(9, 1, "testRoom") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm10",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm10",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                    }
                },
                new Script {
                    Number = 10,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm10" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "girl") },
                        { 1, new Variable(1, 1, "body") },
                        { 2, new Variable(2, 1, "face") },
                        { 3, new Variable(3, 1, "hair") },
                        { 4, new Variable(4, 1, "ear") },
                        { 5, new Variable(5, 1, "page") },
                        { 6, new Variable(6, 1, "phone") },
                        { 7, new Variable(7, 1, "room") },
                        { 8, new Variable(8, 1, "testing") },
                        { 9, new Variable(9, 1, "testRoom") },
                        { 10, new Variable(10, 1, "aBody") },
                        { 11, new Variable(11, 1, "aFace") },
                        { 12, new Variable(12, 1, "aHair") },
                        { 13, new Variable(13, 1, "aEar") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm10",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm10",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                    }
                },
            } },
            { 11, new Script[] {
                new Script {
                    Number = 11,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm11" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "manOnScreen") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm11",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm11Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm11Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm11Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 11,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm11" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aDoor") },
                        { 1, new Variable(1, 1, "aShowBizType") },
                        { 2, new Variable(2, 1, "manOnScreen") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm11",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm11Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm11Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm11Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 12, new Script[] {
                new Script {
                    Number = 12,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm12" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aStreet") },
                        { 1, new Variable(1, 1, "aFreeway1") },
                        { 2, new Variable(2, 1, "aFreeway2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm12",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm12Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm12Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 13, new Script[] {
                new Script {
                    Number = 13,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm13" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 2, "unused") },
                        { 2, new Variable(2, 1, "tookApiss") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm13",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm13Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm13Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm13Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pissScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 13,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm13" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 2, "unused") },
                        { 2, new Variable(2, 1, "tookApiss") },
                        { 3, new Variable(3, 1, "aHead") },
                        { 4, new Variable(4, 1, "aPuddle") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm13",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm13Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm13Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm13Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pissScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 13,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm13" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "beenInPissArea") },
                        { 2, new Variable(2, 1, "tookApiss") },
                        { 3, new Variable(3, 1, "aHead") },
                        { 4, new Variable(4, 1, "aPuddle") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm13",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm13Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm13Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm13Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pissScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 14, new Script[] {
                new Script {
                    Number = 14,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm14" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aSign") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm14",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm14Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm14Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 15, new Script[] {
                new Script {
                    Number = 15,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm15" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "storeOpen") },
                        { 1, new Variable(1, 1, "thisIsIt") },
                        { 2, new Variable(2, 1, "lclWindow") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm15",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm15Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm15Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm15Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "trafficSignalScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 15,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm15" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "storeOpen") },
                        { 1, new Variable(1, 1, "thisIsIt") },
                        { 2, new Variable(2, 1, "lclWindow") },
                        { 3, new Variable(3, 1, "aCar") },
                        { 4, new Variable(4, 1, "aSignal") },
                        { 5, new Variable(5, 1, "aHench") },
                        { 6, new Variable(6, 1, "aDoorWest") },
                        { 7, new Variable(7, 1, "aDoorEast") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm15",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm15Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm15Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm15Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "trafficSignalScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 16, new Script[] {
                new Script {
                    Number = 16,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm16" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aDoor") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm16",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm16Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm16Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 17, new Script[] {
                new Script {
                    Number = 17,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm17" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm17",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm17Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm17Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 18, new Script[] {
                new Script {
                    Number = 18,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm18" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aDoor") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm18",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm18Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm18Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 19, new Script[] {
                new Script {
                    Number = 19,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm19" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 2, "unused") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm19",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm19Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm19Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "monoScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "coasterScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 19,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm19" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aChairlift") },
                        { 2, new Variable(2, 1, "aMonorail") },
                        { 3, new Variable(3, 1, "aMatterhorn") },
                        { 4, new Variable(4, 1, "aRollerCoaster") },
                        { 5, new Variable(5, 1, "aFlags") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm19",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm19Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm19Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "monoScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "coasterScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aPlane") },
                        { 1, new Variable(1, 1, "aJogger") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm20",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm20Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm20Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm20Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 21, new Script[] {
                new Script {
                    Number = 21,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm21" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aSign") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm21",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm21Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm21Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "henchHere") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm22",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm22Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm22Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm22Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boatScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 22,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm22" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "henchHere") },
                        { 2, new Variable(2, 1, "aSpudsSign1") },
                        { 3, new Variable(3, 1, "aSpudsSign2") },
                        { 4, new Variable(4, 1, "aSpudsSign3") },
                        { 5, new Variable(5, 1, "aBoat") },
                        { 6, new Variable(6, 1, "aHench") },
                        { 7, new Variable(7, 1, "aStars") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm22",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm22Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm22Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm22Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boatScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 23, new Script[] {
                new Script {
                    Number = 23,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm23" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "askedAlready") },
                        { 2, new Variable(2, 1, "trashHere") },
                        { 3, new Variable(3, 1, "garageClosed") },
                        { 4, new Variable(4, 1, "local4") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm23",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm23Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm23Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
                new Script {
                    Number = 23,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm23" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "askedAlready") },
                        { 2, new Variable(2, 1, "trashHere") },
                        { 3, new Variable(3, 1, "garageClosed") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm23",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm23Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm23Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 24, new Script[] {
                new Script {
                    Number = 24,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm24" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aPeople") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm24",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm24Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm24Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "detailScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 25, new Script[] {
                new Script {
                    Number = 25,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm25" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aDoor") },
                        { 1, new Variable(1, 1, "aPole") },
                        { 2, new Variable(2, 1, "aWave") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm25",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm25Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm25Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm25Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 26, new Script[] {
                new Script {
                    Number = 26,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm26" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "shipHere") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm26",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm26Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm26Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm26Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "birdScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 26,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm26" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aPurser") },
                        { 2, new Variable(2, 1, "aGate") },
                        { 3, new Variable(3, 1, "shipHere") },
                        { 4, new Variable(4, 1, "aWave") },
                        { 5, new Variable(5, 1, "aBird") },
                        { 6, new Variable(6, 1, "aPoop") },
                        { 7, new Variable(7, 1, "aPoop2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm26",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm26Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm26Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm26Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "birdScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 27, new Script[] {
                new Script {
                    Number = 27,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm27" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "confettiFalling") },
                        { 1, new Variable(1, 1, "numConfetti") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm27",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm27Script",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm27Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "confettiScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "tmpX"),
                                new Variable(1, 1, "tmpLoop"),
                                new Variable(2, 1, "tmpCel"),
                            }
                        },
                    }
                },
            } },
            { 28, new Script[] {
                new Script {
                    Number = 28,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm28" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm28",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm28Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm28Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sparkleScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "sparkleX"),
                                new Variable(1, 1, "sparkleY"),
                            }
                        },
                    }
                },
                new Script {
                    Number = 28,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm28" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aLA") },
                        { 2, new Variable(2, 1, "aBird") },
                        { 3, new Variable(3, 1, "aSparkle") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm28",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm28Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm28Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sparkleScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "sparkleX"),
                                new Variable(1, 1, "sparkleY"),
                            }
                        },
                    }
                },
            } },
            { 31, new Script[] {
                new Script {
                    Number = 31,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm31" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "numClouds") },
                        { 1, new Variable(1, 1, "seenMessage") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm31",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "cloud"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm31Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm31Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cloudScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "tmpY"),
                                new Variable(1, 1, "tmpCel"),
                            }
                        },
                    }
                },
                new Script {
                    Number = 31,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm31" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aBar") },
                        { 1, new Variable(1, 1, "aFlag") },
                        { 2, new Variable(2, 1, "aWakeFront") },
                        { 3, new Variable(3, 1, "aWakeRear") },
                        { 4, new Variable(4, 1, "aHorizonBow") },
                        { 5, new Variable(5, 1, "aHorizonStern") },
                        { 6, new Variable(6, 1, "numClouds") },
                        { 7, new Variable(7, 1, "seenMessage") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm31",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "cloud"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm31Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm31Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cloudScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "tmpY"),
                                new Variable(1, 1, "tmpCel"),
                            }
                        },
                    }
                },
            } },
            { 32, new Script[] {
                new Script {
                    Number = 32,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm32" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "oldEgoX") },
                        { 1, new Variable(1, 1, "oldEgoY") },
                        { 2, new Variable(2, 1, "causeOfDeath") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm32",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm32Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm32Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm32Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 32,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm32" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "oldEgoX") },
                        { 1, new Variable(1, 1, "oldEgoY") },
                        { 2, new Variable(2, 1, "causeOfDeath") },
                        { 3, new Variable(3, 1, "aPorthole") },
                        { 4, new Variable(4, 1, "aDoor") },
                        { 5, new Variable(5, 1, "aMama") },
                        { 6, new Variable(6, 1, "aFruit") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm32",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm32Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm32Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm32Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 33, new Script[] {
                new Script {
                    Number = 33,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm33" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "mamaInRoom") },
                        { 1, new Variable(1, 1, "aMama") },
                        { 2, new Variable(2, 1, "drawerState") },
                        { 3, new Variable(3, 1, "closetState") },
                        { 4, new Variable(4, 1, "debugMsg") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm33",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm33Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm33Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm33Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "whipScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigMama",
                            Name = "cue",
                        },
                    }
                },
                new Script {
                    Number = 33,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm33" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "mamaInRoom") },
                        { 1, new Variable(1, 1, "aPorthole") },
                        { 2, new Variable(2, 1, "aMama") },
                        { 3, new Variable(3, 1, "aCloset") },
                        { 4, new Variable(4, 1, "aDrawer") },
                        { 5, new Variable(5, 1, "drawerState") },
                        { 6, new Variable(6, 1, "closetState") },
                        { 7, new Variable(7, 1, "debugMsg") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm33",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm33Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm33Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm33Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "whipScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigMama",
                            Name = "cue",
                        },
                    }
                },
            } },
            { 34, new Script[] {
                new Script {
                    Number = 34,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm34" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "seenHenchette") },
                        { 2, new Variable(2, 1, "goto95") },
                        { 3, new Variable(3, 1, "localBS") },
                        { 4, new Variable(4, 1, "swamInSuit") },
                        { 5, new Variable(5, 1, "henchInvited") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm34",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm34Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm34Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm34Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "henchScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "BSetter",
                            Name = "doit",
                            Parameters = new string[] {
                                "whom",
                            },
                        },
                    }
                },
                new Script {
                    Number = 34,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm34" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "seenHenchette") },
                        { 2, new Variable(2, 1, "aHead2") },
                        { 3, new Variable(3, 1, "aHead6") },
                        { 4, new Variable(4, 1, "aHead7") },
                        { 5, new Variable(5, 1, "aWake") },
                        { 6, new Variable(6, 1, "aDrain") },
                        { 7, new Variable(7, 1, "aMan") },
                        { 8, new Variable(8, 1, "goto95") },
                        { 9, new Variable(9, 1, "localBS") },
                        { 10, new Variable(10, 1, "swamInSuit") },
                        { 11, new Variable(11, 1, "aHench") },
                        { 12, new Variable(12, 1, "henchInvited") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm34",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm34Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm34Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm34Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "henchScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "BSetter",
                            Name = "doit",
                            Parameters = new string[] {
                                "whom",
                            },
                        },
                    }
                },
            } },
            { 35, new Script[] {
                new Script {
                    Number = 35,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm35" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "dipHere") },
                        { 2, new Variable(2, 1, "goto95") },
                        { 3, new Variable(3, 1, "henchInvited") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm35",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm35Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm35Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm35Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "shipScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "henchScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 35,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm35" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aSpinachDip") },
                        { 2, new Variable(2, 1, "dipHere") },
                        { 3, new Variable(3, 1, "goto95") },
                        { 4, new Variable(4, 1, "aBartender") },
                        { 5, new Variable(5, 1, "aTV") },
                        { 6, new Variable(6, 1, "aGirl1drinking") },
                        { 7, new Variable(7, 1, "aTitFeeler") },
                        { 8, new Variable(8, 1, "aGirl2drinking") },
                        { 9, new Variable(9, 1, "aGirl3drinking") },
                        { 10, new Variable(10, 1, "aManDrinking") },
                        { 11, new Variable(11, 1, "aShip") },
                        { 12, new Variable(12, 1, "aHench") },
                        { 13, new Variable(13, 1, "henchInvited") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm35",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm35Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm35Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm35Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "shipScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "henchScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 36, new Script[] {
                new Script {
                    Number = 36,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm36" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm36",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm36Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm36Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm36Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "captainScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "tmpSpeed"),
                                new Variable(1, 1, "tmpDelay"),
                            }
                        },
                    }
                },
                new Script {
                    Number = 36,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm36" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aDials1") },
                        { 2, new Variable(2, 1, "aDials2") },
                        { 3, new Variable(3, 1, "aDials3") },
                        { 4, new Variable(4, 1, "aDials4") },
                        { 5, new Variable(5, 1, "aDials5") },
                        { 6, new Variable(6, 1, "aDials6") },
                        { 7, new Variable(7, 1, "aLever") },
                        { 8, new Variable(8, 1, "aCaptain") },
                        { 9, new Variable(9, 1, "aWheel") },
                        { 10, new Variable(10, 1, "aHench") },
                        { 11, new Variable(11, 1, "aDart") },
                        { 12, new Variable(12, 1, "aHorizonEast") },
                        { 13, new Variable(13, 1, "aHorizonWest") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm36",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm36Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm36Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm36Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "captainScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "tmpSpeed"),
                                new Variable(1, 1, "tmpDelay"),
                            }
                        },
                    }
                },
            } },
            { 37, new Script[] {
                new Script {
                    Number = 37,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm37" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "talkedToBarber") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm37",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm37Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm37Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm37Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 37,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm37" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aBarber") },
                        { 2, new Variable(2, 1, "aChair") },
                        { 3, new Variable(3, 1, "talkedToBarber") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm37",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm37Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm37Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm37Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 38, new Script[] {
                new Script {
                    Number = 38,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm38" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMessage") },
                        { 1, new Variable(1, 1, "numClouds") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm38",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "cloud"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm38Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm38Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm38Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cloudScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "tmpY"),
                                new Variable(1, 1, "tmpCel"),
                            }
                        },
                    }
                },
            } },
            { 40, new Script[] {
                new Script {
                    Number = 40,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm40" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "earlyOut") },
                        { 2, new Variable(2, 1, "lclEgoView") },
                        { 3, new Variable(3, 1, "flowerHere") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm40",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm40Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm40Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm40Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 40,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm40" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "lclEgoView") },
                        { 2, new Variable(2, 1, "flowerHere") },
                        { 3, new Variable(3, 1, "aHench") },
                        { 4, new Variable(4, 1, "aFlower") },
                        { 5, new Variable(5, 1, "aPottedPlant") },
                        { 6, new Variable(6, 1, "aBird1") },
                        { 7, new Variable(7, 1, "aBird2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm40",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm40Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm40Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm40Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 40,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm40" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "earlyOut") },
                        { 2, new Variable(2, 1, "lclEgoView") },
                        { 3, new Variable(3, 1, "flowerHere") },
                        { 4, new Variable(4, 1, "aHench") },
                        { 5, new Variable(5, 1, "aFlower") },
                        { 6, new Variable(6, 1, "aPottedPlant") },
                        { 7, new Variable(7, 1, "aBird1") },
                        { 8, new Variable(8, 1, "aBird2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm40",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm40Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm40Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm40Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
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
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "bikiniHere") },
                        { 2, new Variable(2, 1, "henchInvited") },
                        { 3, new Variable(3, 1, "goto95") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm41",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm41Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm41Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "henchScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 41,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm41" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "bikiniHere") },
                        { 2, new Variable(2, 1, "henchInvited") },
                        { 3, new Variable(3, 1, "goto95") },
                        { 4, new Variable(4, 1, "aBikini") },
                        { 5, new Variable(5, 1, "aHench") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm41",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm41Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm41Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "henchScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 42, new Script[] {
                new Script {
                    Number = 42,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm42" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aSport") },
                        { 1, new Variable(1, 1, "aCreep") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm42",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm42Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm42Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm42Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sportsScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "sportsX"),
                                new Variable(1, 1, "sportsY"),
                            }
                        },
                    }
                },
            } },
            { 43, new Script[] {
                new Script {
                    Number = 43,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm43" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "knifeHere") },
                        { 1, new Variable(1, 1, "tookKnifeThisTime") },
                        { 2, new Variable(2, 1, "preventInput") },
                        { 3, new Variable(3, 1, "MDatPodium") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm43",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm43Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm43Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm43Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "SITscript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "TSscript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MDscript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "to1Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "to2Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "to3Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "to4Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "toLarryScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "groupScript",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "groupScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "enterScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "enterX"),
                                new Variable(1, 1, "enterY"),
                                new Variable(2, 100, "str"),
                            }
                        },
                    }
                },
            } },
            { 44, new Script[] {
                new Script {
                    Number = 44,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm44" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "maidInRoom") },
                        { 2, new Variable(2, 2, "unused") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm44",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm44Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm44Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm44Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "brotherScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boatScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigFace",
                            Name = "cue",
                        },
                    }
                },
                new Script {
                    Number = 44,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm44" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aSoap") },
                        { 2, new Variable(2, 1, "aBoat") },
                        { 3, new Variable(3, 1, "aMaid") },
                        { 4, new Variable(4, 1, "aBrother") },
                        { 5, new Variable(5, 1, "maidInRoom") },
                        { 6, new Variable(6, 2, "unused") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm44",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm44Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm44Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm44Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "brotherScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boatScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigFace",
                            Name = "cue",
                        },
                    }
                },
                new Script {
                    Number = 44,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm44" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aSoap") },
                        { 2, new Variable(2, 1, "aBoat") },
                        { 3, new Variable(3, 1, "aMaid") },
                        { 4, new Variable(4, 1, "aBrother") },
                        { 5, new Variable(5, 1, "maidInRoom") },
                        { 6, new Variable(6, 1, "oldEgoX") },
                        { 7, new Variable(7, 1, "oldEgoY") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm44",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm44Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm44Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm44Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "brotherScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boatScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigFace",
                            Name = "cue",
                        },
                    }
                },
            } },
            { 45, new Script[] {
                new Script {
                    Number = 45,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm45" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "talkedToBarber") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm45",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm45Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm45Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm45Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 45,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm45" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aBarber") },
                        { 2, new Variable(2, 1, "aChair") },
                        { 3, new Variable(3, 1, "talkedToBarber") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm45",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm45Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm45Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm45Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 47, new Script[] {
                new Script {
                    Number = 47,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm47" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "henchStatus") },
                        { 1, new Variable(1, 1, "unused") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm47",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm47Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm47Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hench1Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hench2Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "henchScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "tmpX"),
                                new Variable(1, 1, "tmpY"),
                            }
                        },
                    }
                },
                new Script {
                    Number = 47,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm47" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aPlane") },
                        { 1, new Variable(1, 1, "aWave") },
                        { 2, new Variable(2, 1, "henchStatus") },
                        { 3, new Variable(3, 1, "aHench1") },
                        { 4, new Variable(4, 1, "aHench2") },
                        { 5, new Variable(5, 1, "aWaveEast") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm47",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm47Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm47Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hench1Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hench2Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "henchScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "tmpX"),
                                new Variable(1, 1, "tmpY"),
                            }
                        },
                    }
                },
            } },
            { 48, new Script[] {
                new Script {
                    Number = 48,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm48" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "loopCount") },
                        { 1, new Variable(1, 1, "safeX") },
                        { 2, new Variable(2, 1, "safeY") },
                        { 3, new Variable(3, 1, "oldScore") },
                        { 4, new Variable(4, 1, "localBS") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "TripDone",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm48",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm48Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm48Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm48Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 50, new Script[] {
                new Script {
                    Number = 50,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm50" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "currentHench") },
                        { 1, new Variable(1, 1, "seenMessage") },
                        { 2, new Variable(2, 1, "henchStatus") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm50",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm50Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm50Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "obj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm50Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "copScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "tmpX"),
                                new Variable(1, 1, "tmpY"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "krishnaScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "tmpX"),
                                new Variable(1, 1, "tmpY"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigFace",
                            Name = "cue",
                        },
                    }
                },
                new Script {
                    Number = 50,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm50" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "currentHench") },
                        { 1, new Variable(1, 1, "seenMessage") },
                        { 2, new Variable(2, 1, "henchStatus") },
                        { 3, new Variable(3, 1, "aDoor") },
                        { 4, new Variable(4, 1, "aRadar") },
                        { 5, new Variable(5, 1, "aHench1") },
                        { 6, new Variable(6, 1, "aHench2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm50",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm50Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm50Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "obj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm50Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "copScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "tmpX"),
                                new Variable(1, 1, "tmpY"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "krishnaScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "tmpX"),
                                new Variable(1, 1, "tmpY"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigFace",
                            Name = "cue",
                        },
                    }
                },
            } },
            { 51, new Script[] {
                new Script {
                    Number = 51,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm51" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "goto95") },
                        { 2, new Variable(2, 1, "henchInvited") },
                        { 3, new Variable(3, 1, "LarrySaidYes") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm51",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm51Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm51Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "henchScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 51,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm51" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "goto95") },
                        { 2, new Variable(2, 1, "henchInvited") },
                        { 3, new Variable(3, 1, "aBarberPole") },
                        { 4, new Variable(4, 1, "aPlane") },
                        { 5, new Variable(5, 1, "aHench") },
                        { 6, new Variable(6, 1, "LarrySaidYes") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm51",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm51Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm51Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "henchScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 52, new Script[] {
                new Script {
                    Number = 52,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm52" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aGreenAgent") },
                        { 2, new Variable(2, 1, "activeLine") },
                        { 3, new Variable(3, 1, "agentHere") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm52",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm52Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm52Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm52Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ticketScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "done"),
                            }
                        },
                    }
                },
                new Script {
                    Number = 52,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm52" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aBlueAgent") },
                        { 2, new Variable(2, 1, "aGreenAgent") },
                        { 3, new Variable(3, 1, "aCyanAgent") },
                        { 4, new Variable(4, 1, "aBlueLine") },
                        { 5, new Variable(5, 1, "aGreenLine") },
                        { 6, new Variable(6, 1, "aCyanLine") },
                        { 7, new Variable(7, 1, "aCustomer") },
                        { 8, new Variable(8, 1, "activeLine") },
                        { 9, new Variable(9, 1, "agentHere") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm52",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm52Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm52Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm52Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ticketScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "done"),
                            }
                        },
                    }
                },
            } },
            { 53, new Script[] {
                new Script {
                    Number = 53,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm53" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm53",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm53Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm53Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "obj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm53Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "theObj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "travelerScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 53,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm53" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aPlane") },
                        { 2, new Variable(2, 1, "aDoor") },
                        { 3, new Variable(3, 1, "aConveyor1") },
                        { 4, new Variable(4, 1, "aConveyor2") },
                        { 5, new Variable(5, 1, "aConveyor3") },
                        { 6, new Variable(6, 1, "aConveyor4") },
                        { 7, new Variable(7, 1, "aAgentFar") },
                        { 8, new Variable(8, 1, "aAgentNear") },
                        { 9, new Variable(9, 1, "aTraveler") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm53",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm53Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm53Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "obj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm53Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "theObj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "travelerScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 54, new Script[] {
                new Script {
                    Number = 54,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm54" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "bagNumber") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm54",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm54Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm54Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm54Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "travelerScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bagScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 54,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm54" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "bagNumber") },
                        { 1, new Variable(1, 1, "aPlane") },
                        { 2, new Variable(2, 1, "aBag") },
                        { 3, new Variable(3, 1, "aDoor") },
                        { 4, new Variable(4, 1, "aTraveler") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm54",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm54Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm54Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm54Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "travelerScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bagScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 55, new Script[] {
                new Script {
                    Number = 55,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm55" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "waitressX") },
                        { 1, new Variable(1, 1, "unused") },
                        { 2, new Variable(2, 1, "orderedSpecial") },
                        { 3, new Variable(3, 1, "seenMessage") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm55",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm55Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm55Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm55Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "waitressScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sidewalkNorthScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sidewalkSouthScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 55,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm55" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "waitressX") },
                        { 1, new Variable(1, 1, "aWaitress") },
                        { 2, new Variable(2, 1, "aSidewalk") },
                        { 3, new Variable(3, 1, "aPlane") },
                        { 4, new Variable(4, 1, "aDoor") },
                        { 5, new Variable(5, 1, "aParachute") },
                        { 6, new Variable(6, 1, "aPlate") },
                        { 7, new Variable(7, 1, "aSidewalkNorth") },
                        { 8, new Variable(8, 1, "aSidewalkSouth") },
                        { 9, new Variable(9, 1, "orderedSpecial") },
                        { 10, new Variable(10, 1, "seenMessage") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm55",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm55Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm55Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm55Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "waitressScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sidewalkNorthScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sidewalkSouthScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 56, new Script[] {
                new Script {
                    Number = 56,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm56" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "lineNum") },
                        { 1, new Variable(1, 1, "lineMax") },
                        { 2, new Variable(2, 1, "lineMaxY") },
                        { 3, new Variable(3, 1, "eastLineMaxX") },
                        { 4, new Variable(4, 1, "westLineMaxX") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm56",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm56Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm56Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eastLineScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "westLineScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 56,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm56" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aMind") },
                        { 1, new Variable(1, 1, "lineNum") },
                        { 2, new Variable(2, 1, "lineMax") },
                        { 3, new Variable(3, 1, "lineMaxY") },
                        { 4, new Variable(4, 1, "eastLineMaxX") },
                        { 5, new Variable(5, 1, "westLineMaxX") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm56",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm56Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm56Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eastLineScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "westLineScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 57, new Script[] {
                new Script {
                    Number = 57,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm57" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMessage") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm57",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm57Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm57Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm57Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sidewalkNorthScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sidewalkSouthScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tumbleScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "tumbleX"),
                                new Variable(1, 1, "tumbleY"),
                            }
                        },
                    }
                },
                new Script {
                    Number = 57,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm57" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMessage") },
                        { 1, new Variable(1, 1, "aSidewalkNorth") },
                        { 2, new Variable(2, 1, "aSidewalkSouth") },
                        { 3, new Variable(3, 1, "aPlane") },
                        { 4, new Variable(4, 1, "aKid1") },
                        { 5, new Variable(5, 1, "aKid2") },
                        { 6, new Variable(6, 1, "aKid3") },
                        { 7, new Variable(7, 1, "aDoor") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm57",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm57Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm57Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm57Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sidewalkNorthScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sidewalkSouthScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tumbleScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "tumbleX"),
                                new Variable(1, 1, "tumbleY"),
                            }
                        },
                    }
                },
            } },
            { 58, new Script[] {
                new Script {
                    Number = 58,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm58" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "seenMessage") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm58",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm58Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm58Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
                new Script {
                    Number = 58,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm58" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "seenMessage") },
                        { 2, new Variable(2, 1, "aPlane") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm58",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm58Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm58Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 61, new Script[] {
                new Script {
                    Number = 61,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm61" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aStewardess") },
                        { 1, new Variable(1, 1, "aCockDoor") },
                        { 2, new Variable(2, 1, "aKnitting") },
                        { 3, new Variable(3, 1, "aNewspaper") },
                        { 4, new Variable(4, 1, "aHand") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm61",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm61Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm61Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm61Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 62, new Script[] {
                new Script {
                    Number = 62,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm62" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "cartsInAisle") },
                        { 1, new Variable(1, 1, "sawCartMessage") },
                        { 2, new Variable(2, 1, "firstTime") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm62",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm62Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm62Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm62Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sittingScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cartLeftScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cartRightScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boreScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 62,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm62" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "cartsInAisle") },
                        { 1, new Variable(1, 1, "sawCartMessage") },
                        { 2, new Variable(2, 1, "firstTime") },
                        { 3, new Variable(3, 1, "aStewardess") },
                        { 4, new Variable(4, 1, "aBore") },
                        { 5, new Variable(5, 1, "aCartLeft") },
                        { 6, new Variable(6, 1, "aCartRight") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm62",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm62Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm62Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm62Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sittingScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cartLeftScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cartRightScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boreScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 63, new Script[] {
                new Script {
                    Number = 63,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm63" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aSmoker1") },
                        { 1, new Variable(1, 1, "aSmoker2") },
                        { 2, new Variable(2, 1, "aSmoker3") },
                        { 3, new Variable(3, 1, "aJohnLight") },
                        { 4, new Variable(4, 1, "aJohnDoor") },
                        { 5, new Variable(5, 1, "aJohnUser1") },
                        { 6, new Variable(6, 1, "aJohnUser2") },
                        { 7, new Variable(7, 1, "aEmergencyExit") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm63",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm63Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm63Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm63Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "smokerScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "johnScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "johnScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 64, new Script[] {
                new Script {
                    Number = 64,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm64" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "numClouds") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm64",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "cloud"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm64Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm64Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm64Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 64,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm64" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "numClouds") },
                        { 1, new Variable(1, 1, "aPlane") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm64",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "cloud"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm64Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm64Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm64Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 65, new Script[] {
                new Script {
                    Number = 65,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm65" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "numClouds") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm65",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "cloud"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm65Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm65Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm65Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aBigEgo") },
                        { 1, new Variable(1, 1, "aBigEgoBottom") },
                        { 2, new Variable(2, 1, "aSparkle") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm70",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm70Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm70Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm70Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
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
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "stickHere") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm71",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm71Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm71Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm71Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 71,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm71" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "stickHere") },
                        { 1, new Variable(1, 1, "aBigEgo") },
                        { 2, new Variable(2, 1, "aBigEgoFace") },
                        { 3, new Variable(3, 1, "aStick") },
                        { 4, new Variable(4, 1, "aSwarm") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm71",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm71Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm71Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm71Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 72, new Script[] {
                new Script {
                    Number = 72,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm72" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "usedStick") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm72",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm72Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm72Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm72Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 72,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm72" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "usedStick") },
                        { 1, new Variable(1, 1, "aSnake") },
                        { 2, new Variable(2, 1, "aBurp") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm72",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm72Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm72Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm72Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 73, new Script[] {
                new Script {
                    Number = 73,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm73" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm73",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm73Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm73Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm73Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 73,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm73" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aMonkey") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm73",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm73Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm73Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm73Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 74, new Script[] {
                new Script {
                    Number = 74,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm74" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "vineHere") },
                        { 1, new Variable(1, 1, "seenCreekMessage") },
                        { 2, new Variable(2, 1, "seenMessage") },
                        { 3, new Variable(3, 1, "grabbedVine") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm74",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm74Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm74Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm74Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 74,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm74" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "vineHere") },
                        { 1, new Variable(1, 1, "seenCreekMessage") },
                        { 2, new Variable(2, 1, "seenMessage") },
                        { 3, new Variable(3, 1, "grabbedVine") },
                        { 4, new Variable(4, 1, "aBigEgo") },
                        { 5, new Variable(5, 1, "aBigEgoFace") },
                        { 6, new Variable(6, 1, "aRapids") },
                        { 7, new Variable(7, 1, "aVine1") },
                        { 8, new Variable(8, 1, "aVine2") },
                        { 9, new Variable(9, 1, "aVine3") },
                        { 10, new Variable(10, 1, "aTHEvine") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm74",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm74Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm74Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm74Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 75, new Script[] {
                new Script {
                    Number = 75,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm75" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMessage") },
                        { 1, new Variable(1, 1, "aKalalau") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm75",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm75Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm75Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm75Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 75,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm75" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMessage") },
                        { 1, new Variable(1, 1, "aKalalau") },
                        { 2, new Variable(2, 1, "aCupidWest") },
                        { 3, new Variable(3, 1, "aCupidEast") },
                        { 4, new Variable(4, 1, "aCopter") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm75",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm75Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm75Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm75Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 76, new Script[] {
                new Script {
                    Number = 76,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm76" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aPedaler") },
                        { 1, new Variable(1, 1, "aBearer") },
                        { 2, new Variable(2, 1, "aBigEgo") },
                        { 3, new Variable(3, 1, "aBigScreen") },
                        { 4, new Variable(4, 1, "aDrummer") },
                        { 5, new Variable(5, 1, "aKalalau") },
                        { 6, new Variable(6, 1, "aChief") },
                        { 7, new Variable(7, 1, "aMouth") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm76",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm76Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm76Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm76Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 77, new Script[] {
                new Script {
                    Number = 77,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm77" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm77",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm77Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm77Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm77Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 77,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm77" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aCampfire") },
                        { 2, new Variable(2, 1, "aArm") },
                        { 3, new Variable(3, 1, "aKalalau") },
                        { 4, new Variable(4, 1, "aBearer") },
                        { 5, new Variable(5, 1, "aWeaver") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm77",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm77Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm77Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm77Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 78, new Script[] {
                new Script {
                    Number = 78,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm78" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "pauseCycles") },
                        { 1, new Variable(1, 1, "aCupidWest") },
                        { 2, new Variable(2, 1, "aCupidEast") },
                        { 3, new Variable(3, 1, "aHeart") },
                        { 4, new Variable(4, 1, "aFlash") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm78",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm78Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm78Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm78Script",
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
                            Object = "woodScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "minicamScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 78,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm78" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aWoodchopper") },
                        { 1, new Variable(1, 1, "pauseCycles") },
                        { 2, new Variable(2, 1, "aHulaHooper") },
                        { 3, new Variable(3, 1, "aDancer") },
                        { 4, new Variable(4, 1, "aPhotographer") },
                        { 5, new Variable(5, 1, "aDrummer") },
                        { 6, new Variable(6, 1, "aPedaler") },
                        { 7, new Variable(7, 1, "aDoctor") },
                        { 8, new Variable(8, 1, "aChief") },
                        { 9, new Variable(9, 1, "aMouth") },
                        { 10, new Variable(10, 1, "aKalalau") },
                        { 11, new Variable(11, 1, "aCameraman") },
                        { 12, new Variable(12, 1, "aCupidWest") },
                        { 13, new Variable(13, 1, "aCupidEast") },
                        { 14, new Variable(14, 1, "aHeart") },
                        { 15, new Variable(15, 1, "aFlash") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm78",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm78Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm78Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm78Script",
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
                            Object = "woodScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "minicamScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 79, new Script[] {
                new Script {
                    Number = 79,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm79" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 2, "unused") },
                        { 2, new Variable(2, 1, "restoreX") },
                        { 3, new Variable(3, 1, "restoreY") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm79",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm79Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm79Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm79Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 79,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm79" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aWater1") },
                        { 2, new Variable(2, 1, "aWater2") },
                        { 3, new Variable(3, 1, "aSteam") },
                        { 4, new Variable(4, 1, "aPlane") },
                        { 5, new Variable(5, 1, "aLimb") },
                        { 6, new Variable(6, 1, "restoreX") },
                        { 7, new Variable(7, 1, "restoreY") },
                        { 8, new Variable(8, 1, "aChief") },
                        { 9, new Variable(9, 1, "aMouth") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm79",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm79Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm79Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm79Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMessage") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm80",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm80Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm80Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
                new Script {
                    Number = 80,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm80" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aWaterfall") },
                        { 1, new Variable(1, 1, "seenMessage") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm80",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm80Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm80Script",
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "readMessage") },
                        { 1, new Variable(1, 1, "spreading") },
                        { 2, new Variable(2, 1, "hasPissed") },
                        { 3, new Variable(3, 1, "localBS") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm81",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm81Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm81Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm81Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 81,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm81" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "readMessage") },
                        { 1, new Variable(1, 1, "spreading") },
                        { 2, new Variable(2, 1, "hasPissed") },
                        { 3, new Variable(3, 1, "aCoil") },
                        { 4, new Variable(4, 1, "aYellowName") },
                        { 5, new Variable(5, 1, "localBS") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm81",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm81Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm81Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm81Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 82, new Script[] {
                new Script {
                    Number = 82,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm82" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "bottleInHand") },
                        { 1, new Variable(1, 1, "bagInBottle") },
                        { 2, new Variable(2, 1, "bagBurning") },
                        { 3, new Variable(3, 1, "causedEruption") },
                        { 4, new Variable(4, 1, "safeX") },
                        { 5, new Variable(5, 1, "safeY") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm82",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm82Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm82Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm82Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bottleScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 82,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm82" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "bottleInHand") },
                        { 1, new Variable(1, 1, "bagInBottle") },
                        { 2, new Variable(2, 1, "bagBurning") },
                        { 3, new Variable(3, 1, "causedEruption") },
                        { 4, new Variable(4, 1, "safeX") },
                        { 5, new Variable(5, 1, "safeY") },
                        { 6, new Variable(6, 1, "aDoor") },
                        { 7, new Variable(7, 1, "aSteam1") },
                        { 8, new Variable(8, 1, "aSteam2") },
                        { 9, new Variable(9, 1, "aSteam3") },
                        { 10, new Variable(10, 1, "aBottle") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm82",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm82Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm82Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm82Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bottleScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 83, new Script[] {
                new Script {
                    Number = 83,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm83" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm83",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm83Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 84, new Script[] {
                new Script {
                    Number = 84,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm84" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm84",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm84Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm84Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 85, new Script[] {
                new Script {
                    Number = 85,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm85" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aDoor") },
                        { 2, new Variable(2, 2, "unused") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm85",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm85Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm85Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 85,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm85" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aDoor") },
                        { 2, new Variable(2, 1, "a2") },
                        { 3, new Variable(3, 1, "a3") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm85",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm85Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm85Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 86, new Script[] {
                new Script {
                    Number = 86,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm86" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm86",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm86Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm86Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 86,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm86" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aVolcano") },
                        { 2, new Variable(2, 1, "aSmoke") },
                        { 3, new Variable(3, 1, "aLava") },
                        { 4, new Variable(4, 1, "aReflection") },
                        { 5, new Variable(5, 1, "aBigEgo") },
                        { 6, new Variable(6, 1, "aBigEgoEyes") },
                        { 7, new Variable(7, 1, "aBigEgoMouth") },
                        { 8, new Variable(8, 1, "aKalalau") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm86",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm86Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm86Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aBall") },
                        { 1, new Variable(1, 1, "aName") },
                        { 2, new Variable(2, 1, "sparkleCount") },
                        { 3, new Variable(3, 1, "heardSong") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm90",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm90Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm90Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm90Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "bx"),
                                new Variable(1, 1, "by"),
                                new Variable(2, 1, "ex"),
                                new Variable(3, 1, "ey"),
                            }
                        },
                    }
                },
                new Script {
                    Number = 90,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm90" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aGirl") },
                        { 1, new Variable(1, 1, "aBall") },
                        { 2, new Variable(2, 1, "aName") },
                        { 3, new Variable(3, 1, "sparkleCount") },
                        { 4, new Variable(4, 1, "heardSong") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm90",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm90Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm90Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm90Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "bx"),
                                new Variable(1, 1, "by"),
                                new Variable(2, 1, "ex"),
                                new Variable(3, 1, "ey"),
                            }
                        },
                    }
                },
            } },
            { 91, new Script[] {
                new Script {
                    Number = 91,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm91" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm91",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm91Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm91Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm91Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 91,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm91" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aMouth") },
                        { 2, new Variable(2, 1, "aCar") },
                        { 3, new Variable(3, 1, "aDog") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm91",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm91Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm91Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm91Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 92, new Script[] {
                new Script {
                    Number = 92,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm92" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "endOfGame") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm92",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm92Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm92Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm92Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "islandScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fogScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "fogX"),
                                new Variable(1, 1, "fogY"),
                            }
                        },
                    }
                },
                new Script {
                    Number = 92,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm92" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "endOfGame") },
                        { 1, new Variable(1, 1, "aWaterfall") },
                        { 2, new Variable(2, 1, "aFog") },
                        { 3, new Variable(3, 1, "aDoor") },
                        { 4, new Variable(4, 1, "aCopter") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm92",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm92Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm92Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm92Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "islandScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fogScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "fogX"),
                                new Variable(1, 1, "fogY"),
                            }
                        },
                    }
                },
            } },
            { 93, new Script[] {
                new Script {
                    Number = 93,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm93" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm93",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm93Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm93Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm93Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 93,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm93" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aGenerator") },
                        { 2, new Variable(2, 1, "aRadio") },
                        { 3, new Variable(3, 1, "aRightPanel1") },
                        { 4, new Variable(4, 1, "aRightPanel2") },
                        { 5, new Variable(5, 1, "aLeftPanel1") },
                        { 6, new Variable(6, 1, "aLeftPanel2") },
                        { 7, new Variable(7, 1, "aDoor") },
                        { 8, new Variable(8, 1, "aFanEast") },
                        { 9, new Variable(9, 1, "aFanWest") },
                        { 10, new Variable(10, 1, "aGrapeEast") },
                        { 11, new Variable(11, 1, "aGrapeWest") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm93",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm93Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm93Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm93Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 95, new Script[] {
                new Script {
                    Number = 95,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm95" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm95",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm95Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm95Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "henchScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 95,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm95" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aHench") },
                        { 2, new Variable(2, 1, "aChainEast") },
                        { 3, new Variable(3, 1, "aChainWest") },
                        { 4, new Variable(4, 1, "aDoor") },
                        { 5, new Variable(5, 1, "aLaser") },
                        { 6, new Variable(6, 1, "aBeam") },
                        { 7, new Variable(7, 1, "aAcid") },
                        { 8, new Variable(8, 1, "aBed") },
                        { 9, new Variable(9, 1, "aBigEgo") },
                        { 10, new Variable(10, 1, "aBigEgoFace") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm95",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm95Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm95Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "henchScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 96, new Script[] {
                new Script {
                    Number = 96,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm96" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm96",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm96Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm96Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 99, new Script[] {
                new Script {
                    Number = 99,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm99" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "doneTime") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm99",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm99",
                            Name = "doit",
                        },
                    }
                },
            } },
            { 101, new Script[] {
                new Script {
                    Number = 101,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm101" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "hisNum1") },
                        { 1, new Variable(1, 1, "hisNum2") },
                        { 2, new Variable(2, 1, "hisNum3") },
                        { 3, new Variable(3, 1, "hisNum4") },
                        { 4, new Variable(4, 1, "hisNum5") },
                        { 5, new Variable(5, 1, "hisNum6") },
                        { 6, new Variable(6, 1, "myNum1") },
                        { 7, new Variable(7, 1, "myNum2") },
                        { 8, new Variable(8, 1, "myNum3") },
                        { 9, new Variable(9, 1, "myNum4") },
                        { 10, new Variable(10, 1, "myNum5") },
                        { 11, new Variable(11, 1, "myNum6") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm101",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm101Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm101Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm101Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "receptionistScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "guyScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dollScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigClerk",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigMouth",
                            Name = "cue",
                        },
                    }
                },
                new Script {
                    Number = 101,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm101" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aReceptionist") },
                        { 1, new Variable(1, 1, "aGuy") },
                        { 2, new Variable(2, 1, "aDoll") },
                        { 3, new Variable(3, 1, "aDoorWest") },
                        { 4, new Variable(4, 1, "aDoorEast") },
                        { 5, new Variable(5, 1, "aDoorNorth") },
                        { 6, new Variable(6, 1, "hisNum1") },
                        { 7, new Variable(7, 1, "hisNum2") },
                        { 8, new Variable(8, 1, "hisNum3") },
                        { 9, new Variable(9, 1, "hisNum4") },
                        { 10, new Variable(10, 1, "hisNum5") },
                        { 11, new Variable(11, 1, "hisNum6") },
                        { 12, new Variable(12, 1, "myNum1") },
                        { 13, new Variable(13, 1, "myNum2") },
                        { 14, new Variable(14, 1, "myNum3") },
                        { 15, new Variable(15, 1, "myNum4") },
                        { 16, new Variable(16, 1, "myNum5") },
                        { 17, new Variable(17, 1, "myNum6") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm101",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm101Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm101Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm101Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "receptionistScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "guyScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dollScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigClerk",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigMouth",
                            Name = "cue",
                        },
                    }
                },
            } },
            { 102, new Script[] {
                new Script {
                    Number = 102,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm102" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "doorIsOpen") },
                        { 2, new Variable(2, 1, "aProducer") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm102",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm102Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm102Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm102Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sittingScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 102,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm102" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "doorIsOpen") },
                        { 2, new Variable(2, 1, "aProducer") },
                        { 3, new Variable(3, 1, "aDoorWest") },
                        { 4, new Variable(4, 1, "aDoorEast") },
                        { 5, new Variable(5, 1, "aTVwest") },
                        { 6, new Variable(6, 1, "aTVeast") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm102",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm102Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm102Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm102Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sittingScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 103, new Script[] {
                new Script {
                    Number = 103,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm103" },
                        { 1, "StartTalk" },
                        { 2, "StopTalk" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 30, "userLine") },
                        { 30, new Variable(30, 1, "oldPrompt") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "StartTalk",
                            Parameters = new string[] {
                                "who",
                                "tvLoop",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "StopTalk",
                            Parameters = new string[] {
                                "who",
                                "cel",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm103",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm103Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm103Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm103Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "minicamScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 103,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm103" },
                        { 1, "StartTalk" },
                        { 2, "StopTalk" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 40, "userLine") },
                        { 40, new Variable(40, 1, "aSignWest") },
                        { 41, new Variable(41, 1, "aSignEast") },
                        { 42, new Variable(42, 1, "aCameraMonitor") },
                        { 43, new Variable(43, 1, "aTV") },
                        { 44, new Variable(44, 1, "aMC") },
                        { 45, new Variable(45, 1, "aCameraman") },
                        { 46, new Variable(46, 1, "aGirl") },
                        { 47, new Variable(47, 1, "aBoy1") },
                        { 48, new Variable(48, 1, "aBoy3") },
                        { 49, new Variable(49, 1, "aLarry") },
                        { 50, new Variable(50, 1, "aStoolLarry") },
                        { 51, new Variable(51, 1, "aApplause") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "StartTalk",
                            Parameters = new string[] {
                                "who",
                                "tvLoop",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "StopTalk",
                            Parameters = new string[] {
                                "who",
                                "cel",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm103",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm103Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm103Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm103Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "minicamScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 103,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm103" },
                        { 1, "StartTalk" },
                        { 2, "StopTalk" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 30, "userLine") },
                        { 30, new Variable(30, 1, "oldPrompt") },
                        { 31, new Variable(31, 1, "aSignWest") },
                        { 32, new Variable(32, 1, "aSignEast") },
                        { 33, new Variable(33, 1, "aCameraMonitor") },
                        { 34, new Variable(34, 1, "aTV") },
                        { 35, new Variable(35, 1, "aMC") },
                        { 36, new Variable(36, 1, "aCameraman") },
                        { 37, new Variable(37, 1, "aGirl") },
                        { 38, new Variable(38, 1, "aBoy1") },
                        { 39, new Variable(39, 1, "aBoy3") },
                        { 40, new Variable(40, 1, "aLarry") },
                        { 41, new Variable(41, 1, "aStoolLarry") },
                        { 42, new Variable(42, 1, "aApplause") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "StartTalk",
                            Parameters = new string[] {
                                "who",
                                "tvLoop",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "StopTalk",
                            Parameters = new string[] {
                                "who",
                                "cel",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm103",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm103Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm103Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm103Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "minicamScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 104, new Script[] {
                new Script {
                    Number = 104,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm104" },
                        { 1, "StartMC" },
                        { 2, "StopMC" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aSign") },
                        { 1, new Variable(1, 1, "aCameraMonitor") },
                        { 2, new Variable(2, 1, "aTV") },
                        { 3, new Variable(3, 1, "aMC") },
                        { 4, new Variable(4, 1, "aWheel") },
                        { 5, new Variable(5, 1, "aCameraman") },
                        { 6, new Variable(6, 1, "aLana") },
                        { 7, new Variable(7, 1, "aApplause") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "StartMC",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "StopMC",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm104",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm104Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm104Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm104Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 114, new Script[] {
                new Script {
                    Number = 114,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm114" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "owesForSoda") },
                        { 2, new Variable(2, 1, "girlTalk") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm114",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm114Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm114Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm114Script",
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
                            Object = "girlScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigClerk",
                            Name = "cue",
                        },
                    }
                },
                new Script {
                    Number = 114,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm114" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "owesForSoda") },
                        { 2, new Variable(2, 1, "girlTalk") },
                        { 3, new Variable(3, 1, "aGulpCup") },
                        { 4, new Variable(4, 1, "aSpigots") },
                        { 5, new Variable(5, 1, "aClerk") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm114",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm114Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm114Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm114Script",
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
                            Object = "girlScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigClerk",
                            Name = "cue",
                        },
                    }
                },
            } },
            { 115, new Script[] {
                new Script {
                    Number = 115,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm115" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm115",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm115Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm115Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm115Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bellScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigClerk",
                            Name = "cue",
                        },
                    }
                },
                new Script {
                    Number = 115,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm115" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aClerk") },
                        { 2, new Variable(2, 1, "aBell") },
                        { 3, new Variable(3, 1, "aOnklunk") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm115",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm115Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm115Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm115Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bellScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigClerk",
                            Name = "cue",
                        },
                    }
                },
            } },
            { 116, new Script[] {
                new Script {
                    Number = 116,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm116" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "swimsuitHere") },
                        { 1, new Variable(1, 1, "seenMessage") },
                        { 2, new Variable(2, 1, "owesForSwimsuit") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm116",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm116Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm116Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm116Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "work"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigClerk",
                            Name = "cue",
                        },
                    }
                },
                new Script {
                    Number = 116,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm116" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "swimsuitHere") },
                        { 1, new Variable(1, 1, "seenMessage") },
                        { 2, new Variable(2, 1, "owesForSwimsuit") },
                        { 3, new Variable(3, 1, "aClerk") },
                        { 4, new Variable(4, 1, "aBigHand") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm116",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm116Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm116Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm116Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "work"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigClerk",
                            Name = "cue",
                        },
                    }
                },
            } },
            { 118, new Script[] {
                new Script {
                    Number = 118,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm118" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "seenMessage") },
                        { 2, new Variable(2, 1, "owesForSunscreen") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm118",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm118Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm118Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm118Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigClerk",
                            Name = "cue",
                        },
                    }
                },
                new Script {
                    Number = 118,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm118" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "seenMessage") },
                        { 2, new Variable(2, 1, "owesForSunscreen") },
                        { 3, new Variable(3, 1, "aClerk") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm118",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm118Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm118Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm118Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigClerk",
                            Name = "cue",
                        },
                    }
                },
            } },
            { 125, new Script[] {
                new Script {
                    Number = 125,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm125" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "cycSpeed") },
                        { 1, new Variable(1, 1, "thoughtView") },
                        { 2, new Variable(2, 1, "talkedToBarber") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm125",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm125Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm125Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm125Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 125,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm125" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aBarber") },
                        { 1, new Variable(1, 1, "aChair") },
                        { 2, new Variable(2, 1, "aThoughtBalloon") },
                        { 3, new Variable(3, 1, "aThought") },
                        { 4, new Variable(4, 1, "aHeads") },
                        { 5, new Variable(5, 1, "aHandle") },
                        { 6, new Variable(6, 1, "cycSpeed") },
                        { 7, new Variable(7, 1, "thoughtView") },
                        { 8, new Variable(8, 1, "talkedToBarber") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm125",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm125Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm125Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm125Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 131, new Script[] {
                new Script {
                    Number = 131,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm131" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aBoat0") },
                        { 1, new Variable(1, 1, "aBoat1") },
                        { 2, new Variable(2, 1, "aBoat2") },
                        { 3, new Variable(3, 1, "aBoat3") },
                        { 4, new Variable(4, 1, "aBoat4") },
                        { 5, new Variable(5, 1, "aBoat5") },
                        { 6, new Variable(6, 1, "aEgoBoat") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm131",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm131Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm131Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm131Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 134, new Script[] {
                new Script {
                    Number = 134,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm134" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "egoPissing") },
                        { 1, new Variable(1, 1, "passInRoom") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm134",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm134Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm134Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm134Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fartScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 134,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm134" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aSkimmer") },
                        { 1, new Variable(1, 1, "aBra") },
                        { 2, new Variable(2, 1, "aMan") },
                        { 3, new Variable(3, 1, "aFart") },
                        { 4, new Variable(4, 1, "aPiss") },
                        { 5, new Variable(5, 1, "egoPissing") },
                        { 6, new Variable(6, 1, "passInRoom") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm134",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm134Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm134Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm134Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fartScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 138, new Script[] {
                new Script {
                    Number = 138,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm138" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "day") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm138",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm138Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm138Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "calendarScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 138,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm138" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "day") },
                        { 1, new Variable(1, 1, "aBigEgo") },
                        { 2, new Variable(2, 1, "aShip") },
                        { 3, new Variable(3, 1, "aCalendar") },
                        { 4, new Variable(4, 1, "aPage") },
                        { 5, new Variable(5, 1, "aDate") },
                        { 6, new Variable(6, 1, "aFlame") },
                        { 7, new Variable(7, 1, "aWave") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm138",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm138Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm138Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "calendarScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 143, new Script[] {
                new Script {
                    Number = 143,
                    Exports = new Dictionary<int, string> {
                        { 0, "rgRestaurant" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "rgRestaurant",
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "talkedToBarber") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm151",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm151Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm151Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm151Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigFace",
                            Name = "cue",
                        },
                    }
                },
                new Script {
                    Number = 151,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm151" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aBarber") },
                        { 1, new Variable(1, 1, "aChair") },
                        { 2, new Variable(2, 1, "talkedToBarber") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm151",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm151Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm151Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm151Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBigFace",
                            Name = "cue",
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
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm152Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm152Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 178, new Script[] {
                new Script {
                    Number = 178,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm178" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm178",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm178Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm178Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 178,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm178" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "work") },
                        { 1, new Variable(1, 1, "aBarber") },
                        { 2, new Variable(2, 1, "aChair") },
                        { 3, new Variable(3, 1, "aThoughtBalloon") },
                        { 4, new Variable(4, 1, "aThought") },
                        { 5, new Variable(5, 1, "aHandle") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm178",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm178Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm178Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 181, new Script[] {
                new Script {
                    Number = 181,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm181" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMessage") },
                        { 1, new Variable(1, 1, "safeX") },
                        { 2, new Variable(2, 1, "safeY") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm181",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm181Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm181Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm181Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 181,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm181" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aWaterfall") },
                        { 1, new Variable(1, 1, "seenMessage") },
                        { 2, new Variable(2, 1, "safeX") },
                        { 3, new Variable(3, 1, "safeY") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm181",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm181Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm181Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm181Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
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
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm200",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm200",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm200",
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
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm300",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm300",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm300",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 301, new Script[] {
                new Script {
                    Number = 301,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm301" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMessage") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm301",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm301",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm301",
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
                            Object = "rm400",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 401, new Script[] {
                new Script {
                    Number = 401,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm401" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMessage") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm401",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm401",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm401",
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
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm500",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm500",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm500",
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
                            Object = "rm600",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm600",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 700, new Script[] {
                new Script {
                    Number = 700,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm700" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm700",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm700",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                    }
                },
            } },
            { 901, new Script[] {
                new Script {
                    Number = 901,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm901" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aBox") },
                        { 1, new Variable(1, 1, "lclWindow") },
                        { 2, new Variable(2, 1, "boxView") },
                        { 3, new Variable(3, 1, "boxLoop") },
                        { 4, new Variable(4, 1, "boxCel") },
                        { 5, new Variable(5, 1, "boxX") },
                        { 6, new Variable(6, 1, "boxY") },
                        { 7, new Variable(7, 1, "oldBoxX") },
                        { 8, new Variable(8, 1, "oldBoxY") },
                        { 9, new Variable(9, 1, "clickX") },
                        { 10, new Variable(10, 1, "clickY") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm901",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm901Script",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm901Script",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm901Script",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 997, new Script[] {
                new Script {
                    Number = 997,
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "oldPause") },
                    },
                    Functions = new Function[] {
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
                                new Variable(2, 100, "string2"),
                            }
                        },
                    }
                },
            } },
        };
    }
}
