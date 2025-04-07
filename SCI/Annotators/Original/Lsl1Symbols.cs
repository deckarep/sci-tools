using System.Collections.Generic;
using FunctionType = SCI.Language.FunctionType;

namespace SCI.Annotators.Original
{
    static class Lsl1Symbols
    {
        public static Dictionary<int, Script[]> Headers = new Dictionary<int, Script[]> {
            { 0, new Script[] {
                new Script {
                    Number = 0,
                    Exports = new Dictionary<int, string> {
                        { 0, "LSL1" },
                        { 1, "NormalEgo" },
                        { 2, "HandsOff" },
                        { 3, "HandsOn" },
                        { 4, "HaveMem" },
                        { 5, "SteppedOn" },
                        { 6, "IsFlag" },
                        { 7, "SetFlag" },
                        { 8, "ClearFlag" },
                        { 9, "ToggleFlag" },
                        { 10, "GameOver" },
                        { 11, "Points" },
                        { 12, "Face" },
                        { 14, "SteppedFullyOn" },
                        { 15, "Babble" },
                        { 17, "InitEgoHead" },
                        { 18, "LarryHours" },
                        { 19, "LarryMinutes" },
                        { 20, "LarrySeconds" },
                        { 21, "Death" },
                        { 22, "ObjInRoom" },
                        { 23, "spraySound" },
                        { 24, "deathIcon" },
                        { 25, "icon0" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "NormalEgo",
                            Parameters = new string[] {
                                "theLoop",
                                "theView",
                                "swView",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "stopView"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "HandsOff",
                            Temps = new Variable[] {
                                new Variable(0, 1, "saveIcon"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "HandsOn",
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
                            Name = "SteppedOn",
                            Parameters = new string[] {
                                "who",
                                "color",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SteppedFullyOn",
                            Parameters = new string[] {
                                "who",
                                "color",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "IsFlag",
                            Parameters = new string[] {
                                "flagEnum",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SetFlag",
                            Parameters = new string[] {
                                "flagEnum",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "oldState"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ClearFlag",
                            Parameters = new string[] {
                                "flagEnum",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "oldState"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ToggleFlag",
                            Parameters = new string[] {
                                "flagEnum",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Death",
                            Parameters = new string[] {
                                "view",
                                "loop",
                                "cycler",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "GameOver",
                            Temps = new Variable[] {
                                new Variable(0, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Points",
                            Parameters = new string[] {
                                "flag",
                                "val",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Face",
                            Parameters = new string[] {
                                "who",
                                "theObjOrX",
                                "theY",
                                "whoCares",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theHeading"),
                                new Variable(1, 1, "lookX"),
                                new Variable(2, 1, "lookY"),
                                new Variable(3, 1, "whoToCue"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Babble",
                            Parameters = new string[] {
                                "theView",
                                "msgS",
                                "msgO",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 500, "buffer"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "InitEgoHead",
                            Parameters = new string[] {
                                "headView",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "hView"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "LarryHours",
                            Temps = new Variable[] {
                                new Variable(0, 1, "hours"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "LarryMinutes",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "LarrySeconds",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ObjInRoom",
                            Parameters = new string[] {
                                "object",
                                "theRoom",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theOwner"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ego",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "egoHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "stopGroop",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "babbleIcon",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL1",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "foo"),
                                new Variable(1, 1, "num"),
                                new Variable(2, 5, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL1",
                            Name = "pragmaFail",
                            Temps = new Variable[] {
                                new Variable(0, 1, "theVerb"),
                                new Variable(1, 1, "theItem"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL1",
                            Name = "quitGame",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL1",
                            Name = "startRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL1",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "saveIcon"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL1",
                            Name = "restart",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL1",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "speedORama",
                            Name = "doit",
                            Parameters = new string[] {
                                "num",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "deathIcon",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon0",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon3",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon4",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon5",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon5",
                            Name = "select",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon6",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon7",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon8",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon8",
                            Name = "select",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon9",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ll1DoVerbCode",
                            Name = "doit",
                            Parameters = new string[] {
                                "theVerb",
                                "theObj",
                                "invNo",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "objDesc"),
                                new Variable(1, 1, "itemDesc"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ll1FtrInit",
                            Name = "doit",
                            Parameters = new string[] {
                                "theObj",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "gcWin",
                            Name = "open",
                            Temps = new Variable[] {
                                new Variable(0, 1, "savePort"),
                                new Variable(1, 1, "theBevelWid"),
                                new Variable(2, 1, "t"),
                                new Variable(3, 1, "l"),
                                new Variable(4, 1, "r"),
                                new Variable(5, 1, "b"),
                                new Variable(6, 1, "theColor"),
                                new Variable(7, 1, "theMaps"),
                                new Variable(8, 1, "topColor"),
                                new Variable(9, 1, "bottomColor"),
                                new Variable(10, 1, "rightColor"),
                                new Variable(11, 1, "leftColor"),
                                new Variable(12, 1, "thePri"),
                                new Variable(13, 1, "i"),
                                new Variable(14, 15, "str"),
                                new Variable(29, 4, "rectPt"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Person",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Person",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
                new Script {
                    Number = 0,
                    Exports = new Dictionary<int, string> {
                        { 0, "LSL1" },
                        { 1, "NormalEgo" },
                        { 2, "HandsOff" },
                        { 3, "HandsOn" },
                        { 4, "HaveMem" },
                        { 5, "SteppedOn" },
                        { 6, "IsFlag" },
                        { 7, "SetFlag" },
                        { 8, "ClearFlag" },
                        { 9, "ToggleFlag" },
                        { 10, "GameOver" },
                        { 11, "Points" },
                        { 12, "Face" },
                        { 14, "SteppedFullyOn" },
                        { 15, "Babble" },
                        { 17, "InitEgoHead" },
                        { 18, "LarryHours" },
                        { 19, "LarryMinutes" },
                        { 20, "LarrySeconds" },
                        { 21, "Death" },
                        { 22, "ObjInRoom" },
                        { 23, "spraySound" },
                        { 24, "deathIcon" },
                        { 25, "icon0" },
                        { 26, "proc0_26" },
                        { 27, "proc0_27" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "NormalEgo",
                            Parameters = new string[] {
                                "theLoop",
                                "theView",
                                "swView",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "stopView"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "HandsOff",
                            Temps = new Variable[] {
                                new Variable(0, 1, "saveIcon"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "HandsOn",
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
                            Name = "SteppedOn",
                            Parameters = new string[] {
                                "who",
                                "color",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SteppedFullyOn",
                            Parameters = new string[] {
                                "who",
                                "color",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "IsFlag",
                            Parameters = new string[] {
                                "flagEnum",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SetFlag",
                            Parameters = new string[] {
                                "flagEnum",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "oldState"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ClearFlag",
                            Parameters = new string[] {
                                "flagEnum",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "oldState"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ToggleFlag",
                            Parameters = new string[] {
                                "flagEnum",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Death",
                            Parameters = new string[] {
                                "view",
                                "loop",
                                "cycler",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "GameOver",
                            Temps = new Variable[] {
                                new Variable(0, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Points",
                            Parameters = new string[] {
                                "flag",
                                "val",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Face",
                            Parameters = new string[] {
                                "who",
                                "theObjOrX",
                                "theY",
                                "whoCares",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theHeading"),
                                new Variable(1, 1, "lookX"),
                                new Variable(2, 1, "lookY"),
                                new Variable(3, 1, "whoToCue"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Babble",
                            Parameters = new string[] {
                                "theView",
                                "msgS",
                                "msgO",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 500, "buffer"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "InitEgoHead",
                            Parameters = new string[] {
                                "headView",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "hView"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "LarryHours",
                            Temps = new Variable[] {
                                new Variable(0, 1, "hours"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "LarryMinutes",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "LarrySeconds",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ObjInRoom",
                            Parameters = new string[] {
                                "object",
                                "theRoom",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theOwner"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "proc0_26",
                            Parameters = new string[] {
                                "param1",
                                "param2",
                                "param3",
                                "param4",
                                "param5",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "proc0_27",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ego",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "egoHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "stopGroop",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "babbleIcon",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL1",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "foo"),
                                new Variable(1, 1, "num"),
                                new Variable(2, 5, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL1",
                            Name = "pragmaFail",
                            Temps = new Variable[] {
                                new Variable(0, 1, "theVerb"),
                                new Variable(1, 1, "theItem"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL1",
                            Name = "quitGame",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL1",
                            Name = "startRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL1",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "saveIcon"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL1",
                            Name = "restart",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL1",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "speedORama",
                            Name = "doit",
                            Parameters = new string[] {
                                "num",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "deathIcon",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon0",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon3",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon4",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon5",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon5",
                            Name = "select",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon6",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon7",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon8",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon8",
                            Name = "select",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon9",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ll1DoVerbCode",
                            Name = "doit",
                            Parameters = new string[] {
                                "theVerb",
                                "theObj",
                                "invNo",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "objDesc"),
                                new Variable(1, 1, "itemDesc"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ll1FtrInit",
                            Name = "doit",
                            Parameters = new string[] {
                                "theObj",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "gcWin",
                            Name = "open",
                            Temps = new Variable[] {
                                new Variable(0, 1, "savePort"),
                                new Variable(1, 1, "theBevelWid"),
                                new Variable(2, 1, "t"),
                                new Variable(3, 1, "l"),
                                new Variable(4, 1, "r"),
                                new Variable(5, 1, "b"),
                                new Variable(6, 1, "theColor"),
                                new Variable(7, 1, "theMaps"),
                                new Variable(8, 1, "topColor"),
                                new Variable(9, 1, "bottomColor"),
                                new Variable(10, 1, "rightColor"),
                                new Variable(11, 1, "leftColor"),
                                new Variable(12, 1, "thePri"),
                                new Variable(13, 1, "i"),
                                new Variable(14, 15, "str"),
                                new Variable(29, 4, "rectPt"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Person",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Person",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 100, new Script[] {
                new Script {
                    Number = 100,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm100" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "clapTimer") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm100",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm100",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "n"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm100",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sWalkWest",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fromBarScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnterDoor",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sChaseEgo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDieOfTheClap",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theWindow",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "taxiSign",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pole",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "upperWindows",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "building",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCop",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "doormat",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cans",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 110, new Script[] {
                new Script {
                    Number = 110,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm110" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "tipCount") },
                        { 1, new Variable(1, 1, "ordered") },
                        { 2, new Variable(2, 1, "tookADrink") },
                        { 3, new Variable(3, 1, "beenToldPrice") },
                        { 4, new Variable(4, 1, "jokeCycles") },
                        { 5, new Variable(5, 1, "jokeNumber") },
                        { 6, new Variable(6, 1, "selection") },
                        { 7, new Variable(7, 1, "pitch") },
                        { 8, new Variable(8, 1, "theta") },
                        { 9, new Variable(9, 1, "saidHello") },
                        { 10, new Variable(10, 1, "clickX") },
                        { 11, new Variable(11, 1, "clickY") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PickAJoke",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm110",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm110",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm110",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm110",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromPimp",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetUp",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sWalkDrunk",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sWalkDrunk",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sKenTalksGirl",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTellJoke",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSulk",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSitDown",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToStoreroom",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromStoreroom",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDoor",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGoPimp",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLeftyServes",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBabeScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGuyScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFatsoScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDudeScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sThrowLarry",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPlaysong",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "egoActions",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "jukebox",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "blondHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "blondHand",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "blondGuy",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "babeTop",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "babe",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "kenHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ken",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dude",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skinnyMan",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fatso",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lefty",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lefty",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fan",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fan",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "mooseEyes",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "peephole",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "peephole",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "stool",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "moose",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
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
                        { 0, new Variable(0, 1, "drunkMessage") },
                        { 1, new Variable(1, 1, "seenTPmsg") },
                        { 2, new Variable(2, 1, "nutsMessage") },
                        { 3, new Variable(3, 1, "whichMessage") },
                        { 4, new Variable(4, 33, "RATPTS") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm120",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm120",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm120",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRatScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRatScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromToilet",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToToilet",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDrunkTalks",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDrunkDrinks",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDrunkDrinks",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetRose",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aRose",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drunkHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drunkArm",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drunkLeg",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDrunk",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDrunk",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "choice"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "barrels",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "table",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "transom",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theFan",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theLight",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
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
                        { 0, new Variable(0, 1, "graffitiCount") },
                        { 1, new Variable(1, 2, "unused") },
                        { 3, new Variable(3, 1, "lookedSink") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm130",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "foo"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm130",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm130",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm130",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "egoActions",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTakeCrap",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTakeCrap",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTakePiss",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFlood",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetRing",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSparkle",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCloseDoor",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDoDrips",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theSparkle",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "faucet",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "toilet",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sink",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "graffiti",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theHandle",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                        { 0, new Variable(0, 1, "climbing") },
                        { 1, new Variable(1, 1, "gaveWarning") },
                        { 2, new Variable(2, 1, "madeComment") },
                        { 3, new Variable(3, 17, "unused") },
                        { 20, new Variable(20, 1, "moveTimer") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm140",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm140",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm140",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm140",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPimp",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToHooker",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromHooker",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDoChannel",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTail",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToBar",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pimp",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pimp",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "moose",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "mooseTail",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tvLights",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theDoorFeature",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tv",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boxes",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "barrel1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "barrel2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "thePeephole",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "oldSpeed") },
                        { 1, new Variable(1, 1, "popping") },
                        { 2, new Variable(2, 25, "screwPts") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "RoomFeats",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CloseupFeats",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm150",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm150",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm150",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "egoActions",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetUndressed",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetDressed",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromStairs",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDownstairs",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCloseup",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBlink",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sScrew",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sExitWindow",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromWindow",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetCandy",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sOpenWindow",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSmoke",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hookerHead",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hookerHead",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hookerHead",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hookerHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theHooker",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aWindow",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eyesProp",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "herMouth",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "table",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "underwear",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bed",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "clothesline",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "windowsill",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "herBreast",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "herFace",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "herCrack",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bedpost",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "humpCycler",
                            Name = "doit",
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "lookedInTrash") },
                        { 1, new Variable(1, 1, "clickX") },
                        { 2, new Variable(2, 1, "clickY") },
                        { 3, new Variable(3, 13, "FALLPATH") },
                        { 16, new Variable(16, 37, "DIE_PATH") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "FallAndDie",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm160",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm160",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm160",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "egoActions",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFall",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToHooker",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sUntieRailing",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sUnTie",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sClimbOut",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sComeTo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sJumpIn",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetHammer",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTie",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTieToRail",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sJumpRail",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sClimbBack",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSmashWindow",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFallDie",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetPills",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sWiggle",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eastWindow",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eastWindow",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hotelSign",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hotelSign",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bottle",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hookerWindow",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "binF",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "balconyF",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hookerWindowF",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hotelSignF",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eastWindowF",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fenceF",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lidF",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 170, new Script[] {
                new Script {
                    Number = 170,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm170" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm170",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "foo"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm170",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm170",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sChased",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sChased",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMugged",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aThug",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 190, new Script[] {
                new Script {
                    Number = 190,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm190" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm190",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRecycle",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRecycle",
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "destination") },
                        { 1, new Variable(1, 1, "lookedCabbie") },
                        { 2, new Variable(2, 1, "paidCabbie") },
                        { 3, new Variable(3, 1, "destCount") },
                        { 4, new Variable(4, 1, "cabFare") },
                        { 5, new Variable(5, 1, "badgerTimer") },
                        { 6, new Variable(6, 1, "letsGo") },
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
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm200",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRoomScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRoomScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sWineScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "carSpeed"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "driver",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "driver",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "tmpDest"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "larry",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "meter1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "meter2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "meter3",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "meter3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "meter4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "license",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "trunk",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
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
                        { 0, new Variable(0, 1, "dealerTotal") },
                        { 1, new Variable(1, 1, "egoTotal") },
                        { 2, new Variable(2, 1, "splitTotal") },
                        { 3, new Variable(3, 1, "cardNum") },
                        { 4, new Variable(4, 6, "dealerCards") },
                        { 10, new Variable(10, 7, "egoCards") },
                        { 17, new Variable(17, 6, "splitCards") },
                        { 23, new Variable(23, 1, "dealersAces") },
                        { 24, new Variable(24, 1, "egosAces") },
                        { 25, new Variable(25, 1, "splitsAces") },
                        { 26, new Variable(26, 1, "egoBusted") },
                        { 27, new Variable(27, 1, "egoCardInc") },
                        { 28, new Variable(28, 1, "splitCardInc") },
                        { 29, new Variable(29, 1, "handOver") },
                        { 30, new Variable(30, 1, "dealerCardInc") },
                        { 31, new Variable(31, 1, "dealersCard") },
                        { 32, new Variable(32, 1, "bet") },
                        { 33, new Variable(33, 1, "status1") },
                        { 34, new Variable(34, 1, "status2") },
                        { 35, new Variable(35, 80, "str") },
                        { 115, new Variable(115, 80, "str1") },
                        { 195, new Variable(195, 1, "dealersFirstCard") },
                        { 196, new Variable(196, 1, "dealerShowCard") },
                        { 197, new Variable(197, 1, "egoDoublingDown") },
                        { 198, new Variable(198, 1, "splitDoublingDown") },
                        { 199, new Variable(199, 1, "egosFirstCard") },
                        { 200, new Variable(200, 1, "egosSecondCard") },
                        { 201, new Variable(201, 1, "splitting") },
                        { 202, new Variable(202, 1, "egoHandDone") },
                        { 203, new Variable(203, 1, "splitBusted") },
                        { 204, new Variable(204, 1, "machineDollars") },
                        { 205, new Variable(205, 1, "dontBlink") },
                        { 206, new Variable(206, 1, "brokeHouse") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "BackToRoom",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "GetCard",
                            Temps = new Variable[] {
                                new Variable(0, 1, "num"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ShowDealerCard",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "GetValue",
                            Parameters = new string[] {
                                "whoseHand",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ShowCard",
                            Parameters = new string[] {
                                "whoseHand",
                                "whoseCard",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Commit",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "AskInsurance",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ChangeDollars",
                            Parameters = new string[] {
                                "amount",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "BlinkIt",
                            Parameters = new string[] {
                                "button",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "BetSize",
                            Temps = new Variable[] {
                                new Variable(0, 1, "theSize"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "HandOver",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm250",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm250",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm250",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theDeal",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dealEm",
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
                            Object = "hitMe",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "egoGetsHit",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "splitGetsHit",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "stand",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "egoStands",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "splitStands",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "egoWins",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dealerWins",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "increase",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "increase",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "increase",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "evt",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "event"),
                                new Variable(1, 1, "ticks"),
                                new Variable(2, 1, "doDelay"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "decrease",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "decrease",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "decrease",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "evt",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "event"),
                                new Variable(1, 1, "ticks"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "surrender",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theDouble",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theSplit",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "surrenderScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "odds",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cashout",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
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
                        { 0, new Variable(0, 1, "bet") },
                        { 1, new Variable(1, 1, "status1") },
                        { 2, new Variable(2, 1, "status2") },
                        { 3, new Variable(3, 80, "str") },
                        { 83, new Variable(83, 1, "machineDollars") },
                        { 84, new Variable(84, 7, "display1") },
                        { 91, new Variable(91, 1, "blewIt") },
                        { 92, new Variable(92, 1, "overLimit") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "BackToRoom",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ChangeDollars",
                            Parameters = new string[] {
                                "amount",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "BlinkIt",
                            Parameters = new string[] {
                                "obj",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "BetSize",
                            Temps = new Variable[] {
                                new Variable(0, 1, "theSize"),
                            }
                        },
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
                            Object = "rm260",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm260",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sWin",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "playButton",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "spinEm",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "increaseButton",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "increaseButton",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "increaseButton",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "evt",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "event"),
                                new Variable(1, 1, "ticks"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "decreaseButton",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "decreaseButton",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "decreaseButton",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "evt",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "event"),
                                new Variable(1, 1, "ticks"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "oddsButton",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "oddsButton",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cashOutButton",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cashOutButton",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "wheelLeft",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "wheelCenter",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "circle",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "stripe0",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "stripe1",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "stripe2",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "stripe3",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "stripe4",
                            Name = "cue",
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
                        { 0, new Variable(0, 8, "aAppleManPolyPts") },
                        { 8, new Variable(8, 1, "lookedOnce") },
                        { 9, new Variable(9, 1, "appleManTimer") },
                        { 10, new Variable(10, 1, "paidWith") },
                        { 11, new Variable(11, 1, "doorState") },
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
                            Object = "sToCasino",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromCasino",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sAutoDoorOpen",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sAutoDoorClose",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sAppleMan",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sAppleMan",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBuyApple",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLookInBarrel",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aAppleMan",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDoorLeft",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aDoorRight",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "appleHead",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "appleHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tit",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hip",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "knee",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fountain",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fPlant",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fSign",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fLights",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "statue",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                        { 0, new Variable(0, 1, "conversationTimer") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm310",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm310",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm310",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm310",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBlink",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGamble",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "oldlady",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "man1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "man1eyes",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "shortman",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "babe",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "jane",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rodney",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "blackjack",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "slots",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "BJ4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "BJ2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "slots2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                        { 0, new Variable(0, 1, "trickTimer") },
                        { 1, new Variable(1, 1, "mcOnStage") },
                        { 2, new Variable(2, 1, "poopied") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintJoke",
                            Temps = new Variable[] {
                                new Variable(0, 1, "joke"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PrintLine",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm320",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm320",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm320",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm320",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                            Object = "egoActions",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sComedian",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMagicTrick",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSit",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sStand",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "comedian",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "head",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "head",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "head",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drummerHead",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drummerHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drummer",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drummer",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dancer",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theStage",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "light1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "table1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "light2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "table2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "light3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "table3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "light4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "table4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "light5",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "table5",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "light6",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "table6",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
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
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ElevatorNumbers",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm330",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm330",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm330",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGoLounge",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromLounge",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromElevator",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToElevator",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sElevatorScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sElevatorScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "j"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetPass",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sNoRide",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "disposeNumbers",
                            Name = "doit",
                            Parameters = new string[] {
                                "obj",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "elevator",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "elevator",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "discoPass",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ashTray",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "thePhone",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "plants",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "elevatorShaft",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theWindow",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
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
                        { 0, new Variable(0, 1, "floorWant") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "FadeIt",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ElevatorNumbers",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm340",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm340",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm340",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                            Object = "sUpElevator",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGoUp",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDownElevator",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGoDown",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sArriveUp",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sArriveDown",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sElevatorScript",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "j"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFrom390",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sNoRide",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDoorMessage",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSuiteDoor",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "disposeNumbers",
                            Name = "doit",
                            Parameters = new string[] {
                                "obj",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "elevator",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "elevator",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theHeart",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "suiteDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door5",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door6",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "plants",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "elevatorShaft",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                        { 0, new Variable(0, 3, "unused") },
                        { 3, new Variable(3, 28, "closePts") },
                        { 31, new Variable(31, 34, "openPts") },
                        { 65, new Variable(65, 28, "middlePts") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ElevatorNumbers",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm350",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm350",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm350",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm350",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromElevator",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToElevator",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sElevatorScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sElevatorScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "j"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFaithLeaves",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToPenthouse",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromPenthouse",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPushButton",
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
                            Object = "sNoRide",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "disposeNumbers",
                            Name = "doit",
                            Parameters = new string[] {
                                "obj",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "faith",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "elevator",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "elevator",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "elevator",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "numberPad",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "doors",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "doors",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "deskF",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "plants",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sculpture",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "employeeExit",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
                new Script {
                    Number = 350,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm350" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 3, "unused") },
                        { 3, new Variable(3, 28, "closePts") },
                        { 31, new Variable(31, 38, "openPts") },
                        { 69, new Variable(69, 28, "middlePts") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ElevatorNumbers",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm350",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm350",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm350",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm350",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromElevator",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToElevator",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sElevatorScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sElevatorScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "j"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFaithLeaves",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToPenthouse",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromPenthouse",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPushButton",
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
                            Object = "sNoRide",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "disposeNumbers",
                            Name = "doit",
                            Parameters = new string[] {
                                "obj",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "faith",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "elevator",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "elevator",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "elevator",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "numberPad",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "doors",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "doors",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "deskF",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "plants",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sculpture",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "employeeExit",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "lookCount") },
                        { 1, new Variable(1, 1, "talkCount") },
                        { 2, new Variable(2, 1, "breathLineTimer") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm355",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm355",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm355",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFaithLeaves",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFrown",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSmile",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFlap",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBlink",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "herEyes",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "herMouth",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "face",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "neck",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "necklace",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hair",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theBreasts",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "faithF",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm360",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm360",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToTub",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromTub",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToBedroom",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFlyingDoll",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromBedroom",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromElevator",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToElevator",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEveIsReady",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "elevator",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "elevator",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "elevatorF",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "painting1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "painting2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sofa",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "planter",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fTable",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fShelf",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fSkylight",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fDoorway",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fDoorwayWest",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "InitRoomFeas",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm370",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm370",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEveDoesLarry",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromLivingroom",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToLivingroom",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sClosetDoor",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLeakyDoll",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sInflate",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sKenSpeaks",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "kenHead",
                            Name = "doit:",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "flatDoll",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "painting",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "painting2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lightSwitch",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bed",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pillows",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "flowerBox",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tree",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 371, new Script[] {
                new Script {
                    Number = 371,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm371" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "triedOnce") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "InitDollFeas",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm371",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm371",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "herMouth",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fRightNipple",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fLeftNipple",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fRightTit",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fLeftTit",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "doll",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "fireworkCounter") },
                        { 1, new Variable(1, 1, "colorCycle") },
                        { 2, new Variable(2, 4, "sizeElem") },
                        { 6, new Variable(6, 1, "fireworkNum") },
                        { 7, new Variable(7, 1, "sparkX") },
                        { 8, new Variable(8, 1, "sparkY") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "FindSize",
                            Parameters = new string[] {
                                "theText",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Credits",
                            Parameters = new string[] {
                                "theText",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "margin"),
                                new Variable(1, 1, "c"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "KillFireWorks",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm375",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "foo"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEcstasy",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEcstasy",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCredits",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sparkJump",
                            Name = "doit",
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
                        { 0, new Variable(0, 2, "unused") },
                        { 2, new Variable(2, 1, "cycleInterval") },
                        { 3, new Variable(3, 1, "larrySplash") },
                        { 4, new Variable(4, 1, "palDelay") },
                        { 5, new Variable(5, 41, "INTO_TUB_PATH") },
                        { 46, new Variable(46, 29, "EXIT_TUB_PATH") },
                        { 75, new Variable(75, 33, "EXIT_TUB_AND_DRESS_PATH") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "JetsOn",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "JetsOff",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm380",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm380",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm380",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "egoActions",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sUndress",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sUndress",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetDressed",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEveLeaves",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToLivingroom",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromLivingroom",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFlyingDoll",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aEve",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aJet",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bJet",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cJet",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dJet",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eJet",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rimJet1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rimJet2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rimJet3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "towel",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "larryClothes",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fSpaButton",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fHotels",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fHotTub",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fLivingRoom",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 385, new Script[] {
                new Script {
                    Number = 385,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm385" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "talked") },
                        { 1, new Variable(1, 1, "looked") },
                        { 2, new Variable(2, 1, "lookDirection") },
                        { 3, new Variable(3, 1, "randRegister") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "JetsOn",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "JetsOff",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm385",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm385",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm385",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sWanderEyes",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEveAngry",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sWink&Pucker",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEveHappy",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEveEatsApple",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sButton",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "herEyes",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eyeLeft",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eyeRight",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "herMouth",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aSpaButton",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "chestBubbles",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "leftRim",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "smallBubble",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bubble1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bubble2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bubble3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fTowel",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fEveHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fEveArms",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fBoobs",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                        { 0, new Variable(0, 1, "wearingRubber") },
                        { 1, new Variable(1, 1, "pouredWine") },
                        { 2, new Variable(2, 1, "doneForeplay") },
                        { 3, new Variable(3, 1, "knifeTimer") },
                        { 4, new Variable(4, 1, "radioTimer") },
                        { 5, new Variable(5, 1, "fawnDollars") },
                        { 6, new Variable(6, 33, "DIVEPTS") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm390",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "foo"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm390",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm390",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm390",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "egoActions",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnter",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetHim",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetHim",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCutLoose",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCutLoose",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sExit",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetRibbon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDoRadio",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFawn",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPourWine",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "foo"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDoCommercial",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDoCommercial",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ribbon",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "wine",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fawn",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fawn",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theRadio",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theBed",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theWindow",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "iceBucket",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "vase",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theChair",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sculpture",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "flower",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
                new Script {
                    Number = 390,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm390" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "pouredWine") },
                        { 1, new Variable(1, 1, "doneForeplay") },
                        { 2, new Variable(2, 1, "knifeTimer") },
                        { 3, new Variable(3, 1, "radioTimer") },
                        { 4, new Variable(4, 1, "fawnDollars") },
                        { 5, new Variable(5, 33, "DIVEPTS") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm390",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "foo"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm390",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm390",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm390",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "egoActions",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnter",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetHim",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetHim",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCutLoose",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCutLoose",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sExit",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetRibbon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDoRadio",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFawn",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPourWine",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "foo"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDoCommercial",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDoCommercial",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ribbon",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "wine",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fawn",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fawn",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theRadio",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theBed",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theWindow",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "iceBucket",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "vase",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theChair",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sculpture",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "flower",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
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
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnterDoor",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromDoor",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sWhereIsShe",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLookFlasher",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "doors",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "doors",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bigFountain",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "smallFountain",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "quikiSign",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "billionSign",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "reflection",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "flasher",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "flasher",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
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
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm410",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm410",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm410",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetMarried",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "preacherHead",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "preacherHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aPreacher",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fawn",
                            Name = "doVerb:",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rightCandle",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "leftCandle",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "window1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "window2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "window3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fSign",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pew1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pew2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bouquet",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "candelabra",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "candelabra2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
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
                        { 1, "aBum" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aBumThreat") },
                        { 1, new Variable(1, 1, "bumLoop") },
                        { 2, new Variable(2, 1, "bumBegged") },
                        { 3, new Variable(3, 1, "bumVerb") },
                        { 4, new Variable(4, 1, "bumObj") },
                        { 5, new Variable(5, 2, "unused") },
                        { 7, new Variable(7, 1, "ringInc") },
                        { 8, new Variable(8, 8, "aBumPolyPts") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "MakePoly",
                            Temps = new Variable[] {
                                new Variable(0, 1, "rL"),
                                new Variable(1, 1, "rT"),
                                new Variable(2, 1, "rR"),
                                new Variable(3, 1, "rB"),
                            }
                        },
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
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromStore",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToStore",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBumBegs",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBumBegs",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetKnife",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBumInteraction",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBumLeaves",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBumLeaves",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sHot&Bothered",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromTelephone",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToTelephone",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fEntryLights",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "darkAlley",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "street",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fWindow",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fSign",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fTelephone",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fArtGallery",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBumHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBumHead",
                            Name = "doit:",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aBum",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 505, new Script[] {
                new Script {
                    Number = 505,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm505" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 10, "ajax") },
                        { 10, new Variable(10, 10, "sexline") },
                        { 20, new Variable(20, 10, "sierra") },
                        { 30, new Variable(30, 10, "sierra2") },
                        { 40, new Variable(40, 1, "index") },
                        { 41, new Variable(41, 1, "dialTimer") },
                        { 42, new Variable(42, 11, "phoneEntry") },
                        { 53, new Variable(53, 1, "touchTone") },
                        { 54, new Variable(54, 1, "tmpFont") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "DialNumber",
                            Parameters = new string[] {
                                "number",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ResetPhone",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CheckAjax",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CheckSexline",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CheckSierra",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CheckSierra2",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm505",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm505",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm505",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBackToRoom",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCallSexline",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 200, "tmpStr"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSurveyResponse",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCallAjax",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCallSierra",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sfxPhoneRinging",
                            Name = "check",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sButton",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "one",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "two",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "three",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "four",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "five",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "six",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "seven",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eight",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "nine",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "zero",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "star",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pound",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
                new Script {
                    Number = 505,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm505" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 10, "ajax") },
                        { 10, new Variable(10, 10, "sexline") },
                        { 20, new Variable(20, 10, "sierra") },
                        { 30, new Variable(30, 1, "index") },
                        { 31, new Variable(31, 1, "dialTimer") },
                        { 32, new Variable(32, 11, "phoneEntry") },
                        { 43, new Variable(43, 1, "touchTone") },
                        { 44, new Variable(44, 1, "tmpFont") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "DialNumber",
                            Parameters = new string[] {
                                "number",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ResetPhone",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CheckAjax",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CheckSexline",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CheckSierra",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm505",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm505",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm505",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBackToRoom",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCallSexline",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 200, "tmpStr"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSurveyResponse",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCallAjax",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCallSierra",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sfxPhoneRinging",
                            Name = "check",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sButton",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "one",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "two",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "three",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "four",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "five",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "six",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "seven",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eight",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "nine",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "zero",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "star",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pound",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                        { 0, new Variable(0, 1, "lubberMaterial") },
                        { 1, new Variable(1, 1, "lubberTexture") },
                        { 2, new Variable(2, 1, "lubberColor") },
                        { 3, new Variable(3, 1, "lubberLubrication") },
                        { 4, new Variable(4, 1, "lubberPattern") },
                        { 5, new Variable(5, 1, "lubberFlavor") },
                        { 6, new Variable(6, 1, "lubberWeight") },
                        { 7, new Variable(7, 1, "lubberThickness") },
                        { 8, new Variable(8, 1, "lubberCoating") },
                        { 9, new Variable(9, 1, "lubberSize") },
                        { 10, new Variable(10, 1, "sprayPrice") },
                        { 11, new Variable(11, 1, "lubberPrice") },
                        { 12, new Variable(12, 1, "winePrice") },
                        { 13, new Variable(13, 1, "magazinePrice") },
                        { 14, new Variable(14, 1, "moneyOwed") },
                        { 15, new Variable(15, 1, "askedForBucks") },
                        { 16, new Variable(16, 1, "readyForMoney") },
                        { 17, new Variable(17, 1, "notPaid") },
                        { 18, new Variable(18, 1, "clerkTalk") },
                        { 19, new Variable(19, 1, "scopeRoom") },
                        { 20, new Variable(20, 1, "headCount") },
                        { 21, new Variable(21, 1, "readSign") },
                        { 22, new Variable(22, 1, "lookedAtMagRack") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "OweMoney",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm510",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm510",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm510",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sShootLarry",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBuyLubber",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "c"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBuyLubber",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGrabWine",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGrabBreathSpray",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pClerk",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pClerk",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cam1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cam2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cam3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cam4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cam5",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cam6",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cam7",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cam8",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cam9",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cam10",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cam11",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cam12",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fShelves",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fShelves1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fMagazineStand",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fWineShelves",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fWineShelves1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fWineShelves2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fFreezer",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fBreathSpray",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "microwave",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "checkoutCounter",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lubberSign",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "bouncerTalks") },
                        { 1, new Variable(1, 1, "seenBouncer") },
                        { 2, new Variable(2, 1, "gaveRing") },
                        { 3, new Variable(3, 33, "MBRPOINTS") },
                        { 36, new Variable(36, 29, "MBLPOINTS") },
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
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm600",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromDisco",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToDisco",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBouncer",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCard",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDiamond",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bouncerHead",
                            Name = "init:",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bouncer",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "firePlug",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "artGallery",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "darkAlley",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 4, "unused") },
                        { 4, new Variable(4, 1, "cueNum") },
                        { 5, new Variable(5, 1, "sharkTimer") },
                        { 6, new Variable(6, 1, "fish1Timer") },
                        { 7, new Variable(7, 1, "fish2Timer") },
                        { 8, new Variable(8, 1, "fish3Timer") },
                        { 9, new Variable(9, 1, "clickX") },
                        { 10, new Variable(10, 1, "clickY") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ReviveActors",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "DumpActors",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm610",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "foo"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm610",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm610",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSitDown",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sStandUp",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFawnIsHistory",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDance",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDance",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSharkChase",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFish1",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFish2",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFish3",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aTuna",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aShark",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aFawn",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aFish1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aFish2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aFish3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "alEyes",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rightGuyEyes",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rogerHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "chair",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boat",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lowe",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skirvin",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aquarium",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "louZerr",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "man2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "man3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "man4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rogerMan",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "coral1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "coral2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "coral3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "coral5",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 615, new Script[] {
                new Script {
                    Number = 615,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm615" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "looked") },
                        { 1, new Variable(1, 1, "talked") },
                        { 2, new Variable(2, 1, "moneyTimer") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm615",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm615",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSmile",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFrown",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sWink",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "herMouth",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rightEye",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "leftEye",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "angryEyes",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fawnBody",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fawnHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fawnNeck",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 700, new Script[] {
                new Script {
                    Number = 700,
                    Exports = new Dictionary<int, string> {
                        { 0, "sidewalk" },
                        { 1, "dog" },
                        { 2, "taxi" },
                        { 3, "sTaxiScript" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "flatY") },
                        { 1, new Variable(1, 8, "taxiPts") },
                        { 9, new Variable(9, 8, "dogPts") },
                        { 17, new Variable(17, 1, "dogTimer") },
                        { 18, new Variable(18, 1, "taxiY") },
                        { 19, new Variable(19, 1, "carView") },
                        { 20, new Variable(20, 1, "beenPissedOn") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PolyTaxi",
                            Temps = new Variable[] {
                                new Variable(0, 1, "rL"),
                                new Variable(1, 1, "rT"),
                                new Variable(2, 1, "rR"),
                                new Variable(3, 1, "rB"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PolyDog",
                            Temps = new Variable[] {
                                new Variable(0, 1, "rL"),
                                new Variable(1, 1, "rT"),
                                new Variable(2, 1, "rR"),
                                new Variable(3, 1, "rB"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sidewalk",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sidewalk",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sidewalk",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "virginScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDropoff",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDropoff",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFindLarry",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFindLarry",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRunOff",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTaxiScript",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTaxiScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTaxiWait",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTaxiWait",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFlattenLarry",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "dist"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFlattenLarry",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnterTaxi",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "maxVal"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnterTaxi",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sShakeLeg",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sShakeLeg",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aCar",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "taxi",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "taxi",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dog",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "taxiSignProp",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 710, new Script[] {
                new Script {
                    Number = 710,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm710" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm710",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm710",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm710",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRoom",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 720, new Script[] {
                new Script {
                    Number = 720,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm720" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "atY") },
                        { 1, new Variable(1, 1, "correct") },
                        { 2, new Variable(2, 1, "question") },
                        { 3, new Variable(3, 1, "hisAnswer") },
                        { 4, new Variable(4, 1, "theAnswer") },
                        { 5, new Variable(5, 1, "thisColor") },
                        { 6, new Variable(6, 1, "thisFile") },
                        { 7, new Variable(7, 1, "theKey") },
                        { 8, new Variable(8, 99, "filesSeen") },
                        { 107, new Variable(107, 300, "string") },
                        { 407, new Variable(407, 1, "missedOne") },
                        { 408, new Variable(408, 1, "dQuestion") },
                        { 409, new Variable(409, 4, "dLetter") },
                        { 413, new Variable(413, 4, "dAnswer") },
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
                            Name = "HighLightCopy",
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
                            Name = "SetFlgTrivia",
                            Parameters = new string[] {
                                "flag",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "TstFlgTrivia",
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
                            Type = FunctionType.Method,
                            Object = "rm720",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "tryCount"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm720",
                            Name = "dispose",
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
                                new Variable(2, 200, "tmpString"),
                            }
                        },
                    }
                },
                new Script {
                    Number = 720,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm720" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "atY") },
                        { 1, new Variable(1, 1, "correct") },
                        { 2, new Variable(2, 1, "question") },
                        { 3, new Variable(3, 1, "hisAnswer") },
                        { 4, new Variable(4, 1, "theAnswer") },
                        { 5, new Variable(5, 1, "thisColor") },
                        { 6, new Variable(6, 1, "thisFile") },
                        { 7, new Variable(7, 1, "theKey") },
                        { 8, new Variable(8, 99, "filesSeen") },
                        { 107, new Variable(107, 300, "string") },
                        { 407, new Variable(407, 1, "missedOne") },
                        { 408, new Variable(408, 1, "dQuestion") },
                        { 409, new Variable(409, 4, "dLetter") },
                        { 413, new Variable(413, 4, "dAnswer") },
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
                            Name = "SetFlgTrivia",
                            Parameters = new string[] {
                                "flag",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "TstFlgTrivia",
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
                            Type = FunctionType.Method,
                            Object = "rm720",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "tryCount"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm720",
                            Name = "dispose",
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
                                new Variable(2, 200, "tmpString"),
                            }
                        },
                    }
                },
            } },
            { 800, new Script[] {
                new Script {
                    Number = 800,
                    Exports = new Dictionary<int, string> {
                        { 0, "debugHandler" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "yesI") },
                        { 1, new Variable(1, 25, "theText") },
                        { 26, new Variable(26, 1, "oldCursor") },
                        { 27, new Variable(27, 1, "wR") },
                        { 28, new Variable(28, 1, "wG") },
                        { 29, new Variable(29, 1, "wB") },
                        { 30, new Variable(30, 1, "tR") },
                        { 31, new Variable(31, 1, "tG") },
                        { 32, new Variable(32, 1, "tB") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "GetNum",
                            Parameters = new string[] {
                                "string",
                                "default",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 40, "theLine"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CheckScroll",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "debugHandler",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "debugHandler",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "debugHandler",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 75, "str"),
                                new Variable(75, 75, "str1"),
                                new Variable(150, 10, "str2"),
                                new Variable(160, 1, "wind"),
                                new Variable(161, 1, "evt"),
                                new Variable(162, 1, "num"),
                                new Variable(163, 1, "obj"),
                                new Variable(164, 1, "underbits"),
                                new Variable(165, 1, "palNum"),
                                new Variable(166, 1, "t"),
                                new Variable(167, 1, "l"),
                                new Variable(168, 1, "r"),
                                new Variable(169, 1, "g"),
                                new Variable(170, 1, "b"),
                                new Variable(171, 1, "marginHigh"),
                                new Variable(172, 1, "marginWide"),
                                new Variable(173, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dInvD",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "lastX"),
                                new Variable(1, 1, "lastY"),
                                new Variable(2, 1, "widest"),
                                new Variable(3, 1, "num"),
                                new Variable(4, 1, "el"),
                                new Variable(5, 1, "node"),
                                new Variable(6, 1, "obj"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dInvD",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "el"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dInvD",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "msg"),
                                new Variable(1, 1, "typ"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "showFeatureCode",
                            Name = "doit",
                            Parameters = new string[] {
                                "obj",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "t"),
                                new Variable(1, 1, "l"),
                                new Variable(2, 1, "r"),
                                new Variable(3, 1, "b"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "NameFeatureCode",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "NameFeatureCode",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "NameFeatureCode",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "NameFeatureCode",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "obj"),
                                new Variable(1, 40, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "justifyText",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "justifyText",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "justifyText",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "inc"),
                                new Variable(1, 1, "theX"),
                                new Variable(2, 1, "theY"),
                                new Variable(3, 1, "eType"),
                                new Variable(4, 1, "eMsg"),
                                new Variable(5, 1, "eMods"),
                                new Variable(6, 25, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "justifyText",
                            Name = "showCoord",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 20, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "justifyText",
                            Name = "doit",
                            Parameters = new string[] {
                                "clear",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theX"),
                                new Variable(1, 1, "theY"),
                            }
                        },
                    }
                },
            } },
            { 801, new Script[] {
                new Script {
                    Number = 801,
                    Exports = new Dictionary<int, string> {
                        { 0, "disposeCode" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "disposeCode",
                            Name = "doit",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                    }
                },
            } },
            { 803, new Script[] {
                new Script {
                    Number = 803,
                    Exports = new Dictionary<int, string> {
                        { 0, "speedTest" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "doneTime") },
                        { 1, new Variable(1, 1, "machineSpeed") },
                        { 2, new Variable(2, 1, "cfgHandle") },
                        { 3, new Variable(3, 1, "fastThreshold") },
                        { 4, new Variable(4, 1, "mediumThreshold") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "speedTest",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "speedTest",
                            Name = "doit",
                        },
                    }
                },
            } },
            { 811, new Script[] {
                new Script {
                    Number = 811,
                    Exports = new Dictionary<int, string> {
                        { 0, "aboutCode" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "hours") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aboutCode",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 200, "string"),
                            }
                        },
                    }
                },
            } },
            { 812, new Script[] {
                new Script {
                    Number = 812,
                    Exports = new Dictionary<int, string> {
                        { 0, "egoSprays" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "sprayCount") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "egoSprays",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "egoSprays",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 813, new Script[] {
                new Script {
                    Number = 813,
                    Exports = new Dictionary<int, string> {
                        { 0, "eRS" },
                        { 1, "lRS" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Object = "eRS",
                            Name = "SeeIfOffX",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Object = "eRS",
                            Name = "SeeIfOffY",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LLRoom",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "wide"),
                                new Variable(1, 1, "high"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LLRoom",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "nRoom"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lRS",
                            Name = "changeState",
                            Parameters = new string[] {
                                "ns",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "high"),
                                new Variable(1, 1, "wide"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eRS",
                            Name = "changeState",
                            Parameters = new string[] {
                                "ns",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "high"),
                                new Variable(1, 1, "wide"),
                            }
                        },
                    }
                },
            } },
            { 814, new Script[] {
                new Script {
                    Number = 814,
                    Exports = new Dictionary<int, string> {
                        { 0, "invCode" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "invCode",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "invWin",
                            Name = "open",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "invLook",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "invHand",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "invSelect",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "invHelp",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ok",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LLinvItem",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "wallet",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "choice"),
                                new Variable(1, 100, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "watch",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 100, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "remoteControl",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "discoPass",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "magazine",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 815, new Script[] {
                new Script {
                    Number = 815,
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 8, "headCel") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LLEgo",
                            Name = "headView",
                            Parameters = new string[] {
                                "theView",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LLEgo",
                            Name = "hide",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LLEgo",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LLEgo",
                            Name = "stopUpd",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LLEgo",
                            Name = "egoSpeed",
                            Parameters = new string[] {
                                "num",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LLEgo",
                            Name = "userSpeed",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Head",
                            Name = "init",
                            Parameters = new string[] {
                                "owner",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Head",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Head",
                            Name = "showSelf",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Head",
                            Name = "lookAround",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                    }
                },
            } },
            { 850, new Script[] {
                new Script {
                    Number = 850,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm850" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm850",
                            Name = "init",
                        },
                    }
                },
            } },
            { 851, new Script[] {
                new Script {
                    Number = 851,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm851" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm851",
                            Name = "init",
                        },
                    }
                },
            } },
            { 852, new Script[] {
                new Script {
                    Number = 852,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm852" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm852",
                            Name = "init",
                        },
                    }
                },
            } },
            { 853, new Script[] {
                new Script {
                    Number = 853,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm853" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm853",
                            Name = "init",
                        },
                    }
                },
            } },
            { 854, new Script[] {
                new Script {
                    Number = 854,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm854" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm854",
                            Name = "init",
                        },
                    }
                },
            } },
            { 855, new Script[] {
                new Script {
                    Number = 855,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm855" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm855",
                            Name = "init",
                        },
                    }
                },
            } },
            { 856, new Script[] {
                new Script {
                    Number = 856,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm856" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm856",
                            Name = "init",
                        },
                    }
                },
            } },
            { 857, new Script[] {
                new Script {
                    Number = 857,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm857" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm857",
                            Name = "init",
                        },
                    }
                },
            } },
            { 858, new Script[] {
                new Script {
                    Number = 858,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm858" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm858",
                            Name = "init",
                        },
                    }
                },
            } },
            { 859, new Script[] {
                new Script {
                    Number = 859,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm859" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm859",
                            Name = "init",
                        },
                    }
                },
            } },
            { 860, new Script[] {
                new Script {
                    Number = 860,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm860" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm860",
                            Name = "init",
                        },
                    }
                },
            } },
            { 861, new Script[] {
                new Script {
                    Number = 861,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm861" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm861",
                            Name = "init",
                        },
                    }
                },
            } },
            { 862, new Script[] {
                new Script {
                    Number = 862,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm862" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm862",
                            Name = "init",
                        },
                    }
                },
            } },
            { 863, new Script[] {
                new Script {
                    Number = 863,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm863" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm863",
                            Name = "init",
                        },
                    }
                },
            } },
            { 864, new Script[] {
                new Script {
                    Number = 864,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm864" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm864",
                            Name = "init",
                        },
                    }
                },
            } },
            { 865, new Script[] {
                new Script {
                    Number = 865,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm865" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm865",
                            Name = "init",
                        },
                    }
                },
            } },
            { 866, new Script[] {
                new Script {
                    Number = 866,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm866" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm866",
                            Name = "init",
                        },
                    }
                },
            } },
            { 867, new Script[] {
                new Script {
                    Number = 867,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm867" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm867",
                            Name = "init",
                        },
                    }
                },
            } },
        };
    }
}
