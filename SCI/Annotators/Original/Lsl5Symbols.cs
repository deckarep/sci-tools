using System.Collections.Generic;
using FunctionType = SCI.Language.FunctionType;

namespace SCI.Annotators.Original
{
    static class Lsl5Symbols
    {
        public static Dictionary<int, Script[]> Headers = new Dictionary<int, Script[]> {
            { 0, new Script[] {
                new Script {
                    Number = 0,
                    Exports = new Dictionary<int, string> {
                        { 0, "LSL5" },
                        { 1, "EgoIs" },
                        { 2, "HandsOff" },
                        { 3, "HandsOn" },
                        { 4, "HaveMem" },
                        { 5, "StepOn" },
                        { 6, "IsFlag" },
                        { 7, "SetFlag" },
                        { 8, "ClearFlag" },
                        { 9, "RecordTape" },
                        { 10, "Points" },
                        { 11, "Face" },
                        { 12, "NoResponse" },
                        { 13, "SetFFRoom" },
                        { 14, "TPrint" },
                        { 15, "WalkTo" },
                        { 16, "RestoreIB" },
                        { 17, "Delay" },
                        { 18, "Say" },
                        { 20, "gcWin" },
                        { 21, "ll5Win" },
                        { 22, "SetupExit" },
                        { 23, "SaveCurIcon" },
                        { 24, "LoadCurIcon" },
                        { 25, "CenterDisplay" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "EgoIs",
                            Parameters = new string[] {
                                "who",
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
                            Name = "HaveMem",
                            Parameters = new string[] {
                                "howMuch",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "StepOn",
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
                            Name = "RecordTape",
                            Parameters = new string[] {
                                "who",
                                "theTape",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "whichTape"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Points",
                            Parameters = new string[] {
                                "val",
                                "flag",
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
                            Name = "SetFFRoom",
                            Parameters = new string[] {
                                "theRoom",
                                "cueObj",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "TPrint",
                            Parameters = new string[] {
                                "arg1",
                                "arg2",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 300, "str"),
                                new Variable(300, 1, "theTime"),
                                new Variable(301, 1, "saveCursor"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "WalkTo",
                            Parameters = new string[] {
                                "theObj",
                                "theVerb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "evt"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "RestoreIB",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Delay",
                            Parameters = new string[] {
                                "howLong",
                                "delayType",
                                "whoCares",
                                "theCode",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Say",
                            Parameters = new string[] {
                                "who",
                                "arg1",
                                "arg2",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "disposeOrNot"),
                                new Variable(1, 1, "who2Cue"),
                                new Variable(2, 1, "i"),
                                new Variable(3, 200, "buffer"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "NoResponse",
                            Parameters = new string[] {
                                "obj",
                                "verb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 40, "str"),
                                new Variable(40, 10, "fileName"),
                                new Variable(50, 10, "vstr"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SetupExit",
                            Parameters = new string[] {
                                "turnOn",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SaveCurIcon",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "LoadCurIcon",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CenterDisplay",
                            Parameters = new string[] {
                                "lines",
                                "theColor",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "oldPort"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "WrapMusic",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "WrapMusic",
                            Name = "cue",
                            Temps = new Variable[] {
                                new Variable(0, 1, "num"),
                                new Variable(1, 1, "oldVol"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ll5Timer",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "camcorderTimer",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "camcorderTimer",
                            Name = "cue",
                            Temps = new Variable[] {
                                new Variable(0, 1, "cam"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Actions",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "quitIcon",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "num"),
                                new Variable(1, 5, "str"),
                                new Variable(6, 1, "theEgo"),
                                new Variable(7, 1, "cfgHandle"),
                                new Variable(8, 1, "panicWin"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "pragmaFail",
                            Temps = new Variable[] {
                                new Variable(0, 1, "theVerb"),
                                new Variable(1, 1, "theItem"),
                                new Variable(2, 30, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "restart",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "quitGame",
                            Temps = new Variable[] {
                                new Variable(0, 10, "str1"),
                                new Variable(10, 8, "str2"),
                                new Variable(18, 1, "theFile"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "newRoom",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "setSpeed",
                            Parameters = new string[] {
                                "what",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "startRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon0",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon0",
                            Name = "select",
                            Temps = new Variable[] {
                                new Variable(0, 1, "evt"),
                            }
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
                            Object = "icon6",
                            Name = "select",
                            Parameters = new string[] {
                                "relVer",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "event"),
                                new Variable(1, 1, "whichCel"),
                                new Variable(2, 1, "cii"),
                                new Variable(3, 1, "theX"),
                                new Variable(4, 1, "theY"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon7",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon7",
                            Name = "select",
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
                            Object = "checkIcon",
                            Name = "doit",
                            Parameters = new string[] {
                                "theIcon",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ll5DoVerbCode",
                            Name = "doit",
                            Parameters = new string[] {
                                "theVerb",
                                "theObj",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "objDesc"),
                                new Variable(1, 1, "theItem"),
                                new Variable(2, 100, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ll5FtrInit",
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
                            Object = "detailSlider",
                            Name = "doit",
                            Parameters = new string[] {
                                "num",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "speedSlider",
                            Name = "show",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "speedSlider",
                            Name = "move",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "speedSlider",
                            Name = "mask",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "textSlider",
                            Name = "doit",
                            Parameters = new string[] {
                                "num",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "iconRestore",
                            Name = "select",
                        },
                    }
                },
                new Script {
                    Number = 0,
                    Exports = new Dictionary<int, string> {
                        { 0, "LSL5" },
                        { 1, "EgoIs" },
                        { 2, "HandsOff" },
                        { 3, "HandsOn" },
                        { 4, "HaveMem" },
                        { 5, "StepOn" },
                        { 6, "IsFlag" },
                        { 7, "SetFlag" },
                        { 8, "ClearFlag" },
                        { 9, "RecordTape" },
                        { 10, "Points" },
                        { 11, "Face" },
                        { 13, "SetFFRoom" },
                        { 14, "TPrint" },
                        { 15, "WalkTo" },
                        { 16, "RestoreIB" },
                        { 17, "Delay" },
                        { 18, "Say" },
                        { 20, "gcWin" },
                        { 21, "ll5Win" },
                        { 22, "SetupExit" },
                        { 23, "SaveCurIcon" },
                        { 24, "LoadCurIcon" },
                        { 25, "CenterDisplay" },
                        { 26, "proc0_26" },
                        { 27, "proc0_27" },
                        { 28, "proc0_28" },
                        { 29, "proc0_29" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "proc0_28",
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
                            Name = "proc0_29",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "proc0_26",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "proc0_27",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "EgoIs",
                            Parameters = new string[] {
                                "who",
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
                            Name = "HaveMem",
                            Parameters = new string[] {
                                "howMuch",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "StepOn",
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
                            Name = "RecordTape",
                            Parameters = new string[] {
                                "who",
                                "theTape",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "whichTape"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Points",
                            Parameters = new string[] {
                                "val",
                                "flag",
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
                            Name = "SetFFRoom",
                            Parameters = new string[] {
                                "theRoom",
                                "cueObj",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "TPrint",
                            Parameters = new string[] {
                                "arg1",
                                "arg2",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 300, "str"),
                                new Variable(300, 1, "theTime"),
                                new Variable(301, 1, "saveCursor"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "WalkTo",
                            Parameters = new string[] {
                                "theObj",
                                "theVerb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "evt"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "RestoreIB",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Delay",
                            Parameters = new string[] {
                                "howLong",
                                "delayType",
                                "whoCares",
                                "theCode",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Say",
                            Parameters = new string[] {
                                "who",
                                "arg1",
                                "arg2",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "disposeOrNot"),
                                new Variable(1, 1, "who2Cue"),
                                new Variable(2, 1, "i"),
                                new Variable(3, 200, "buffer"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SetupExit",
                            Parameters = new string[] {
                                "turnOn",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SaveCurIcon",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "LoadCurIcon",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CenterDisplay",
                            Parameters = new string[] {
                                "lines",
                                "theColor",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "oldPort"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "WrapMusic",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "WrapMusic",
                            Name = "cue",
                            Temps = new Variable[] {
                                new Variable(0, 1, "num"),
                                new Variable(1, 1, "oldVol"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ll5Timer",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "camcorderTimer",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "camcorderTimer",
                            Name = "cue",
                            Temps = new Variable[] {
                                new Variable(0, 1, "cam"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Actions",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "quitIcon",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "num"),
                                new Variable(1, 5, "str"),
                                new Variable(6, 1, "theEgo"),
                                new Variable(7, 1, "cfgHandle"),
                                new Variable(8, 1, "panicWin"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "pragmaFail",
                            Temps = new Variable[] {
                                new Variable(0, 1, "theVerb"),
                                new Variable(1, 1, "theItem"),
                                new Variable(2, 30, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "restart",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "quitGame",
                            Temps = new Variable[] {
                                new Variable(0, 10, "str1"),
                                new Variable(10, 8, "str2"),
                                new Variable(18, 1, "theFile"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "newRoom",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "setSpeed",
                            Parameters = new string[] {
                                "what",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "startRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LSL5",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon0",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon0",
                            Name = "select",
                            Temps = new Variable[] {
                                new Variable(0, 1, "evt"),
                            }
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
                            Object = "icon6",
                            Name = "select",
                            Parameters = new string[] {
                                "relVer",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "event"),
                                new Variable(1, 1, "whichCel"),
                                new Variable(2, 1, "cii"),
                                new Variable(3, 1, "theX"),
                                new Variable(4, 1, "theY"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon7",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "icon7",
                            Name = "select",
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
                            Object = "checkIcon",
                            Name = "doit",
                            Parameters = new string[] {
                                "theIcon",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ll5DoVerbCode",
                            Name = "doit",
                            Parameters = new string[] {
                                "theVerb",
                                "theObj",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "objDesc"),
                                new Variable(1, 1, "theItem"),
                                new Variable(2, 100, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ll5FtrInit",
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
                            Object = "detailSlider",
                            Name = "doit",
                            Parameters = new string[] {
                                "num",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "speedSlider",
                            Name = "show",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "speedSlider",
                            Name = "move",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "speedSlider",
                            Name = "mask",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "textSlider",
                            Name = "doit",
                            Parameters = new string[] {
                                "num",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "iconRestore",
                            Name = "select",
                        },
                    }
                },
            } },
            { 10, new Script[] {
                new Script {
                    Number = 10,
                    Exports = new Dictionary<int, string> {
                        { 0, "debugHandler" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "yesI") },
                        { 1, new Variable(1, 26, "theText") },
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
                                new Variable(169, 1, "b"),
                                new Variable(170, 1, "marginHigh"),
                                new Variable(171, 1, "marginWide"),
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
                    }
                },
            } },
            { 11, new Script[] {
                new Script {
                    Number = 11,
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
            { 12, new Script[] {
                new Script {
                    Number = 12,
                    Exports = new Dictionary<int, string> {
                        { 0, "ColorInit" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ColorInit",
                        },
                    }
                },
            } },
            { 15, new Script[] {
                new Script {
                    Number = 15,
                    Exports = new Dictionary<int, string> {
                        { 0, "aboutCode" },
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
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cycleIcon",
                            Name = "init",
                        },
                    }
                },
            } },
            { 18, new Script[] {
                new Script {
                    Number = 18,
                    Exports = new Dictionary<int, string> {
                        { 0, "eRS" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 7, "chargerCoords") },
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
                                new Variable(2, 1, "i"),
                                new Variable(3, 1, "theLoop"),
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
                            Object = "LLRoom",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 200, "str"),
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
            { 20, new Script[] {
                new Script {
                    Number = 20,
                    Exports = new Dictionary<int, string> {
                        { 0, "TTDialer" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 100, "str") },
                        { 100, new Variable(100, 1, "buttons") },
                        { 101, new Variable(101, 14, "buttonX") },
                        { 115, new Variable(115, 14, "buttonY") },
                        { 129, new Variable(129, 6, "prefix") },
                        { 135, new Variable(135, 10, "ATMLoops") },
                        { 145, new Variable(145, 1, "thePrefix") },
                        { 146, new Variable(146, 1, "theLine") },
                        { 147, new Variable(147, 1, "function") },
                        { 148, new Variable(148, 1, "who2Cue") },
                        { 149, new Variable(149, 1, "userInput") },
                        { 150, new Variable(150, 1, "vol1") },
                        { 151, new Variable(151, 1, "vol2") },
                        { 152, new Variable(152, 1, "oldSettings") },
                        { 153, new Variable(153, 1, "walkType") },
                        { 154, new Variable(154, 1, "notifyVal") },
                        { 155, new Variable(155, 1, "cheatVal") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "TTDialer",
                            Name = "init",
                            Parameters = new string[] {
                                "which",
                                "who",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "TTDialer",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "TTDialer",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "TTDialer",
                            Name = "cue",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMessage",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sInformation",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 222, "string"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PushButton",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "idx"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PushButton",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PushButton",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 5, "tempStr"),
                                new Variable(5, 5, "str1"),
                                new Variable(10, 1, "eType"),
                                new Variable(11, 1, "eMsg"),
                                new Variable(12, 1, "theTone"),
                            }
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
                            Object = "btn1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "btn2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "btn3",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "btn7",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "btn8",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "btn9",
                            Name = "init",
                        },
                    }
                },
            } },
            { 21, new Script[] {
                new Script {
                    Number = 21,
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Door",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Door",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
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
                    }
                },
            } },
            { 22, new Script[] {
                new Script {
                    Number = 22,
                    Exports = new Dictionary<int, string> {
                        { 0, "sPlugCharger" },
                        { 1, "sUnplugCharger" },
                        { 2, "sGetShocked" },
                        { 3, "charger" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 7, "chargerCoords") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPlugCharger",
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
                            Object = "sPlugCamcorder",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sUnplugCharger",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "n"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetShocked",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "charger",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "charger",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 50, "str"),
                            }
                        },
                    }
                },
            } },
            { 23, new Script[] {
                new Script {
                    Number = 23,
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Larry",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Larry",
                            Name = "normalize",
                            Parameters = new string[] {
                                "theView",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Larry",
                            Name = "setUpInv",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Larry",
                            Name = "showInv",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Larry",
                            Name = "get",
                            Parameters = new string[] {
                                "what",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Larry",
                            Name = "has",
                            Parameters = new string[] {
                                "what",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theItem"),
                            }
                        },
                    }
                },
            } },
            { 24, new Script[] {
                new Script {
                    Number = 24,
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Patti",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Patti",
                            Name = "normalize",
                            Parameters = new string[] {
                                "theView",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Patti",
                            Name = "setUpInv",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Patti",
                            Name = "showInv",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Patti",
                            Name = "get",
                            Parameters = new string[] {
                                "what",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Patti",
                            Name = "has",
                            Parameters = new string[] {
                                "what",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theItem"),
                            }
                        },
                    }
                },
            } },
            { 40, new Script[] {
                new Script {
                    Number = 40,
                    Exports = new Dictionary<int, string> {
                        { 0, "HollywoodRgn" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "HollywoodRgn",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "HollywoodRgn",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRemember",
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
                        { 0, "rm050" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm050",
                            Name = "init",
                        },
                    }
                },
            } },
            { 51, new Script[] {
                new Script {
                    Number = 51,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm051" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm051",
                            Name = "init",
                        },
                    }
                },
            } },
            { 52, new Script[] {
                new Script {
                    Number = 52,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm052" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm052",
                            Name = "init",
                        },
                    }
                },
            } },
            { 53, new Script[] {
                new Script {
                    Number = 53,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm053" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm053",
                            Name = "init",
                        },
                    }
                },
            } },
            { 54, new Script[] {
                new Script {
                    Number = 54,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm054" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm054",
                            Name = "init",
                        },
                    }
                },
            } },
            { 55, new Script[] {
                new Script {
                    Number = 55,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm055" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm055",
                            Name = "init",
                        },
                    }
                },
            } },
            { 56, new Script[] {
                new Script {
                    Number = 56,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm056" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm056",
                            Name = "init",
                        },
                    }
                },
            } },
            { 57, new Script[] {
                new Script {
                    Number = 57,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm057" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm057",
                            Name = "init",
                        },
                    }
                },
            } },
            { 58, new Script[] {
                new Script {
                    Number = 58,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm058" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm058",
                            Name = "init",
                        },
                    }
                },
            } },
            { 59, new Script[] {
                new Script {
                    Number = 59,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm059" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm059",
                            Name = "init",
                        },
                    }
                },
            } },
            { 60, new Script[] {
                new Script {
                    Number = 60,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm060" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm060",
                            Name = "init",
                        },
                    }
                },
            } },
            { 61, new Script[] {
                new Script {
                    Number = 61,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm061" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm061",
                            Name = "init",
                        },
                    }
                },
            } },
            { 62, new Script[] {
                new Script {
                    Number = 62,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm062" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm062",
                            Name = "init",
                        },
                    }
                },
            } },
            { 63, new Script[] {
                new Script {
                    Number = 63,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm063" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm063",
                            Name = "init",
                        },
                    }
                },
            } },
            { 64, new Script[] {
                new Script {
                    Number = 64,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm064" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm064",
                            Name = "init",
                        },
                    }
                },
            } },
            { 65, new Script[] {
                new Script {
                    Number = 65,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm065" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm065",
                            Name = "init",
                        },
                    }
                },
            } },
            { 66, new Script[] {
                new Script {
                    Number = 66,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm066" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm066",
                            Name = "init",
                        },
                    }
                },
            } },
            { 67, new Script[] {
                new Script {
                    Number = 67,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm067" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm067",
                            Name = "init",
                        },
                    }
                },
            } },
            { 68, new Script[] {
                new Script {
                    Number = 68,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm068" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm068",
                            Name = "init",
                        },
                    }
                },
            } },
            { 69, new Script[] {
                new Script {
                    Number = 69,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm069" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm069",
                            Name = "init",
                        },
                    }
                },
            } },
            { 70, new Script[] {
                new Script {
                    Number = 70,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm070" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm070",
                            Name = "init",
                        },
                    }
                },
            } },
            { 71, new Script[] {
                new Script {
                    Number = 71,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm071" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm071",
                            Name = "init",
                        },
                    }
                },
            } },
            { 72, new Script[] {
                new Script {
                    Number = 72,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm072" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm072",
                            Name = "init",
                        },
                    }
                },
            } },
            { 80, new Script[] {
                new Script {
                    Number = 80,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm080" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm080",
                            Name = "init",
                        },
                    }
                },
            } },
            { 81, new Script[] {
                new Script {
                    Number = 81,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm081" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm081",
                            Name = "init",
                        },
                    }
                },
            } },
            { 82, new Script[] {
                new Script {
                    Number = 82,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm082" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm082",
                            Name = "init",
                        },
                    }
                },
            } },
            { 83, new Script[] {
                new Script {
                    Number = 83,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm083" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm083",
                            Name = "init",
                        },
                    }
                },
            } },
            { 84, new Script[] {
                new Script {
                    Number = 84,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm084" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm084",
                            Name = "init",
                        },
                    }
                },
            } },
            { 85, new Script[] {
                new Script {
                    Number = 85,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm085" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm085",
                            Name = "init",
                        },
                    }
                },
            } },
            { 86, new Script[] {
                new Script {
                    Number = 86,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm086" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm086",
                            Name = "init",
                        },
                    }
                },
            } },
            { 87, new Script[] {
                new Script {
                    Number = 87,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm087" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm087",
                            Name = "init",
                        },
                    }
                },
            } },
            { 88, new Script[] {
                new Script {
                    Number = 88,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm088" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm088",
                            Name = "init",
                        },
                    }
                },
            } },
            { 89, new Script[] {
                new Script {
                    Number = 89,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm089" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm089",
                            Name = "init",
                        },
                    }
                },
            } },
            { 90, new Script[] {
                new Script {
                    Number = 90,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm090" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm090",
                            Name = "init",
                        },
                    }
                },
            } },
            { 91, new Script[] {
                new Script {
                    Number = 91,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm091" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm091",
                            Name = "init",
                        },
                    }
                },
            } },
            { 92, new Script[] {
                new Script {
                    Number = 92,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm092" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm092",
                            Name = "init",
                        },
                    }
                },
            } },
            { 93, new Script[] {
                new Script {
                    Number = 93,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm093" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm093",
                            Name = "init",
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
                        { 0, new Variable(0, 1, "letter") },
                        { 1, new Variable(1, 1, "letterX") },
                        { 2, new Variable(2, 50, "string") },
                        { 52, new Variable(52, 12, "underBs") },
                        { 64, new Variable(64, 1, "cycleThemColors") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "TypeIt",
                            Temps = new Variable[] {
                                new Variable(0, 2, "ltr"),
                                new Variable(2, 1, "char"),
                                new Variable(3, 1, "theUnders"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm100",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm100",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm100",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLarryCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 100,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm100" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "letter") },
                        { 1, new Variable(1, 1, "letterX") },
                        { 2, new Variable(2, 100, "string") },
                        { 102, new Variable(102, 12, "underBs") },
                        { 114, new Variable(114, 1, "cycleThemColors") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "TypeIt",
                            Temps = new Variable[] {
                                new Variable(0, 2, "ltr"),
                                new Variable(2, 1, "char"),
                                new Variable(3, 1, "theUnders"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm100",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm100",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm100",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLarryCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
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
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ShadowPrint",
                            Parameters = new string[] {
                                "theText",
                                "atX",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm110",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm110",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm110",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 100, "str"),
                            }
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
                        { 0, new Variable(0, 1, "seenMsg") },
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
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Vinnie",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Bruno",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
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
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "textBox") },
                        { 2, new Variable(2, 1, "cycleTime") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm130",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm130",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Mr. Bigg",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Mr. Bigg",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Mr. Bigg",
                            Name = "show",
                            Temps = new Variable[] {
                                new Variable(0, 1, "pnv"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Mr. Bigg",
                            Name = "cycle",
                            Parameters = new string[] {
                                "obj",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theCel"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "evType"),
                                new Variable(1, 1, "dir"),
                            }
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
                        { 0, new Variable(0, 1, "seenMsg") },
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
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Biffie",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Scooter",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Silas Scruemall",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLarryCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
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
                        { 0, new Variable(0, 1, "seenMsg") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm150",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm150",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "You",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Silas Scruemall",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Silas Scruemall",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 155, new Script[] {
                new Script {
                    Number = 155,
                    Exports = new Dictionary<int, string> {
                        { 0, "passwordTest" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 10, "passStr") },
                        { 10, new Variable(10, 10, "realStr") },
                        { 20, new Variable(20, 1, "theCount") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "AskPassword",
                            Temps = new Variable[] {
                                new Variable(0, 1, "rc"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ConvertCase",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "char"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Hash",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "char"),
                                new Variable(2, 1, "seed"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "LoadPassword",
                            Temps = new Variable[] {
                                new Variable(0, 10, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SavePassword",
                            Temps = new Variable[] {
                                new Variable(0, 10, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "passwordTest",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "rc"),
                            }
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
                        { 0, new Variable(0, 1, "oldDoVerbCode") },
                        { 1, new Variable(1, 1, "usedEyeball") },
                        { 2, new Variable(2, 1, "usedHand") },
                        { 3, new Variable(3, 1, "lookedAtPot") },
                        { 4, new Variable(4, 1, "belched") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm160",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm160",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm160",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm160",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMoveOffControl",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCoffee",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromWest",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBelch",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDeliver",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theX"),
                                new Variable(1, 1, "theY"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fileDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fileDoor",
                            Name = "open",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fileDoor",
                            Name = "close",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "presDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "presDoor",
                            Name = "open",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "coffee",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "waterCooler",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "waterCooler",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "helpTimer",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "coffeeTimer",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "coffeeMaker",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "leftDoor",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bigSign",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "portrait",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poster",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pictures",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "onePicture",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "certificate",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "plant",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "smallPlant",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "largePlant",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bronzeAward",
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
                            Object = "desk",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cabinetDoors",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "outlet",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "doVerb160Code",
                            Name = "doit",
                            Parameters = new string[] {
                                "theVerb",
                                "theObj",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "objDesc"),
                                new Variable(1, 1, "theItem"),
                                new Variable(2, 100, "str"),
                            }
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "page") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm170",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sOpenDrawer",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTapes",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDegaussTape",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "camcorderTapes",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door",
                            Name = "open",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door",
                            Name = "close",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "degausser",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drawer1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drawer2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drawer3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cabinet",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sterileBarrel",
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
                            Object = "videoMonitor1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "videoMonitor2",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drain",
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
                            Object = "videotapes1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "videotapes2",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "videotapes3",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "videotapes4",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "videotapes5",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "videotapes6",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "videotapes7",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lightWest",
                            Name = "onMe",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eightTrack",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eightTrack",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eightTrack",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "num"),
                            }
                        },
                    }
                },
                new Script {
                    Number = 170,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm170" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "tapesDegaussed") },
                        { 1, new Variable(1, 1, "page") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm170",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sOpenDrawer",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTapes",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDegaussTape",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "camcorderTapes",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door",
                            Name = "open",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door",
                            Name = "close",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "degausser",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drawer1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drawer2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drawer3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cabinet",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sterileBarrel",
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
                            Object = "videoMonitor1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "videoMonitor2",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drain",
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
                            Object = "videotapes1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "videotapes2",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "videotapes3",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "videotapes4",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "videotapes5",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "videotapes6",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "videotapes7",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lightWest",
                            Name = "onMe",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eightTrack",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eightTrack",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "eightTrack",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "num"),
                            }
                        },
                    }
                },
            } },
            { 175, new Script[] {
                new Script {
                    Number = 175,
                    Exports = new Dictionary<int, string> {
                        { 0, "PasswordTest" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "localproc_0",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PasswordTest",
                            Temps = new Variable[] {
                                new Variable(0, 1, "rc"),
                                new Variable(1, 1, "result"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "AskPassword",
                            Temps = new Variable[] {
                                new Variable(0, 1, "rc"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "ConvertCase",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "char"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Hash",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "char"),
                                new Variable(2, 1, "seed"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "LoadPassword",
                            Temps = new Variable[] {
                                new Variable(0, 10, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SavePassword",
                            Temps = new Variable[] {
                                new Variable(0, 10, "str"),
                            }
                        },
                    }
                },
            } },
            { 180, new Script[] {
                new Script {
                    Number = 180,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm180" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "drawerOpen") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm180",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "imprinter",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "imprinter",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drawer",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetCard",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDrawer",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bookcase",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "nudePainting",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "desk",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lamp",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "magnifier",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "loserDrawer1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "loserDrawer2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "loserDrawer3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                            Object = "chair",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fTwins",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 100, "str"),
                            }
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
                            Object = "rm190",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm190",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnterLimo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "limo",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
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
                            Temps = new Variable[] {
                                new Variable(0, 30, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "waves",
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
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tree",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "windows1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "windows2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "debugFeature",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "debugFeature",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "debugFeature",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
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
                        { 0, new Variable(0, 1, "faxHere") },
                        { 1, new Variable(1, 1, "limoCueCount") },
                        { 2, new Variable(2, 1, "missedDayTrotter") },
                        { 3, new Variable(3, 1, "lastSec") },
                        { 4, new Variable(4, 1, "tvOn") },
                        { 5, new Variable(5, 1, "windowOpen") },
                        { 6, new Variable(6, 1, "tDrive") },
                        { 7, new Variable(7, 1, "onPhone") },
                        { 8, new Variable(8, 1, "driving") },
                        { 9, new Variable(9, 1, "dreaming") },
                        { 10, new Variable(10, 1, "phonedDesmond") },
                        { 11, new Variable(11, 1, "whichFax") },
                        { 12, new Variable(12, 1, "lookedAtDT") },
                        { 13, new Variable(13, 1, "faxPaperCue") },
                        { 14, new Variable(14, 1, "driverCue") },
                        { 15, new Variable(15, 1, "armCue") },
                        { 16, new Variable(16, 1, "okToExit") },
                        { 17, new Variable(17, 17, "dreamPts") },
                        { 34, new Variable(34, 1, "dreamDone") },
                        { 35, new Variable(35, 3, "unused") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm200",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "num"),
                            }
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
                            Object = "rm200",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm200",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm200",
                            Name = "notify",
                            Parameters = new string[] {
                                "what",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm200",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFishScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fish",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "glass",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dayTrotter",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "legs",
                            Name = "onMe",
                            Parameters = new string[] {
                                "evt",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theTv",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tv",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tv",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "phone",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "arm",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bottle",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "vcr",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tapes",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fishTank",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bottles&Glasses",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "stereo",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rightDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRoll",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLegs",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "windowL",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "faxMachine",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "driversWindow",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetBottle",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDrive",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 10, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDrive",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
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
                                new Variable(0, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDream",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPhone",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFax",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "faxPaper",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "faxPaper",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
                new Script {
                    Number = 200,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm200" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "limoCueCount") },
                        { 1, new Variable(1, 1, "missedDayTrotter") },
                        { 2, new Variable(2, 1, "lastSec") },
                        { 3, new Variable(3, 1, "tvOn") },
                        { 4, new Variable(4, 1, "windowOpen") },
                        { 5, new Variable(5, 1, "tDrive") },
                        { 6, new Variable(6, 1, "onPhone") },
                        { 7, new Variable(7, 1, "driving") },
                        { 8, new Variable(8, 1, "dreaming") },
                        { 9, new Variable(9, 1, "phonedDesmond") },
                        { 10, new Variable(10, 1, "whichFax") },
                        { 11, new Variable(11, 1, "lookedAtDT") },
                        { 12, new Variable(12, 1, "faxPaperCue") },
                        { 13, new Variable(13, 1, "driverCue") },
                        { 14, new Variable(14, 1, "armCue") },
                        { 15, new Variable(15, 1, "okToExit") },
                        { 16, new Variable(16, 17, "dreamPts") },
                        { 33, new Variable(33, 1, "dreamDone") },
                        { 34, new Variable(34, 3, "unused") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm200",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "num"),
                            }
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
                            Object = "rm200",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm200",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm200",
                            Name = "notify",
                            Parameters = new string[] {
                                "what",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm200",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFishScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fish",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "glass",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dayTrotter",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "legs",
                            Name = "onMe",
                            Parameters = new string[] {
                                "evt",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theTv",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tv",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tv",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "phone",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "arm",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bottle",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "vcr",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tapes",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fishTank",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bottles&Glasses",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "stereo",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rightDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRoll",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLegs",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "windowL",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "faxMachine",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "driversWindow",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetBottle",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDrive",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 10, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDrive",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
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
                                new Variable(0, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDream",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPhone",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFax",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "faxPaper",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "faxPaper",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
                new Script {
                    Number = 200,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm200" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "faxHere") },
                        { 1, new Variable(1, 1, "limoCueCount") },
                        { 2, new Variable(2, 1, "missedDayTrotter") },
                        { 3, new Variable(3, 1, "lastSec") },
                        { 4, new Variable(4, 1, "tvOn") },
                        { 5, new Variable(5, 1, "windowOpen") },
                        { 6, new Variable(6, 1, "tDrive") },
                        { 7, new Variable(7, 1, "onPhone") },
                        { 8, new Variable(8, 1, "driving") },
                        { 9, new Variable(9, 1, "dreaming") },
                        { 10, new Variable(10, 1, "phonedDesmond") },
                        { 11, new Variable(11, 1, "whichFax") },
                        { 12, new Variable(12, 1, "lookedAtDT") },
                        { 13, new Variable(13, 1, "faxPaperCue") },
                        { 14, new Variable(14, 1, "driverCue") },
                        { 15, new Variable(15, 1, "armCue") },
                        { 16, new Variable(16, 1, "okToExit") },
                        { 17, new Variable(17, 1, "dreamDone") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm200",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "num"),
                            }
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
                            Object = "rm200",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm200",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm200",
                            Name = "notify",
                            Parameters = new string[] {
                                "what",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm200",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFishScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fish",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "glass",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dayTrotter",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "legs",
                            Name = "onMe",
                            Parameters = new string[] {
                                "evt",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theTv",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tv",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tv",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "phone",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "arm",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bottle",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "vcr",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tapes",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fishTank",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bottles&Glasses",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "stereo",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rightDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRoll",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLegs",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "windowL",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "faxMachine",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "driversWindow",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetBottle",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDrive",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 10, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDrive",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
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
                                new Variable(0, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDream",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPhone",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFax",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "faxPaper",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "faxPaper",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 205, new Script[] {
                new Script {
                    Number = 205,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm205" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "textUp") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm205",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "num"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMobWantsCane",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sScruemallGetsCall",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCaneGetsBigGrant",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCaneWorksCongress",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Vinnie",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Bruno",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Silas Scruemall",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "President of C.A.N.E.",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Luigi",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "A C.A.N.E.-er",
                            Name = "init",
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
                        { 0, new Variable(0, 1, "cardHere") },
                        { 1, new Variable(1, 1, "cueCounter") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm250",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm250",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm250",
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
                            Object = "rm250",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm250",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ATM",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "limo",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "limo",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "limo",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tower",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bush",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "luggage",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "luggage2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "car",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "car2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "driveway",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "otherDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theSign",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 40, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "envelope",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "envelope",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "plane",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "trashCan",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cloud",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPlane",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLimo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                    }
                },
            } },
            { 258, new Script[] {
                new Script {
                    Number = 258,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm258" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "cueCounter") },
                        { 1, new Variable(1, 1, "wrongNumber") },
                        { 2, new Variable(2, 1, "weCheated") },
                        { 3, new Variable(3, 1, "warnCounter") },
                        { 4, new Variable(4, 4, "cpCode") },
                        { 8, new Variable(8, 4, "cpTime") },
                        { 12, new Variable(12, 4, "dest") },
                        { 16, new Variable(16, 1, "theBar") },
                        { 17, new Variable(17, 10, "departTime") },
                        { 27, new Variable(27, 8, "cityToState") },
                        { 35, new Variable(35, 16, "NYcopyProtCode") },
                        { 51, new Variable(51, 16, "ACcopyProtCode") },
                        { 67, new Variable(67, 16, "McopyProtCode") },
                        { 83, new Variable(83, 16, "LAcopyProtCode") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "FormatTime",
                            Parameters = new string[] {
                                "which",
                                "theCity",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "theTime"),
                                new Variable(2, 10, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm258",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm258",
                            Name = "notify",
                            Parameters = new string[] {
                                "theNumber",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "cPCode"),
                                new Variable(1, 1, "theHour"),
                                new Variable(2, 1, "theMins"),
                                new Variable(3, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm258",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm258",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm258",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm258",
                            Name = "newRoom",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boardPass",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boardPass",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boardPass",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "card",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "card",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "card",
                            Name = "cue",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "slot",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bar1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bar2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bar3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 80, "str"),
                            }
                        },
                    }
                },
                new Script {
                    Number = 258,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm258" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "cueCounter") },
                        { 1, new Variable(1, 1, "wrongNumber") },
                        { 2, new Variable(2, 1, "weCheated") },
                        { 3, new Variable(3, 1, "warnCounter") },
                        { 4, new Variable(4, 4, "cpCode") },
                        { 8, new Variable(8, 4, "cpTime") },
                        { 12, new Variable(12, 4, "dest") },
                        { 16, new Variable(16, 1, "theBar") },
                        { 17, new Variable(17, 20, "departTime") },
                        { 37, new Variable(37, 8, "cityToState") },
                        { 45, new Variable(45, 16, "NYcopyProtCode") },
                        { 61, new Variable(61, 16, "ACcopyProtCode") },
                        { 77, new Variable(77, 16, "McopyProtCode") },
                        { 93, new Variable(93, 16, "LAcopyProtCode") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "FormatTime",
                            Parameters = new string[] {
                                "which",
                                "theCity",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "theTime"),
                                new Variable(2, 10, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm258",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm258",
                            Name = "notify",
                            Parameters = new string[] {
                                "theNumber",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "cPCode"),
                                new Variable(1, 1, "theHour"),
                                new Variable(2, 1, "theMins"),
                                new Variable(3, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm258",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm258",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm258",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm258",
                            Name = "newRoom",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boardPass",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boardPass",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boardPass",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "card",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "card",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "card",
                            Name = "cue",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "slot",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bar1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bar2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bar3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 80, "str"),
                            }
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
                        { 0, new Variable(0, 1, "chargeInOutlet") },
                        { 1, new Variable(1, 1, "whichSlot") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm260",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm260",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 100, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm260",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm260",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm260",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cigaretteMachine",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "chairs",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "post",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "plant",
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
                            Object = "sGetChange",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "outlet",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "plantSouth",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ashtray",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 30, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "handle1",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "handle2",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "handle3",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "handle4",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "slots",
                            Name = "onMe",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "slot1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "slot2",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "slot3",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "slot4",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSlots",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                    }
                },
            } },
            { 270, new Script[] {
                new Script {
                    Number = 270,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm270" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aCueCounter") },
                        { 1, new Variable(1, 1, "scaredRed") },
                        { 2, new Variable(2, 1, "scaredBlonde") },
                        { 3, new Variable(3, 1, "newRedX") },
                        { 4, new Variable(4, 1, "newBlondeX") },
                        { 5, new Variable(5, 1, "cueCounter") },
                        { 6, new Variable(6, 1, "lookCounter") },
                        { 7, new Variable(7, 500, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm270",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm270",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 150, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm270",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm270",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm270",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cannister",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "plant",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "chairs",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ashtray",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 30, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "vipSign",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theCounter",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "blonde",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "redHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sStealChange",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBlonde",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "oldX"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRedHead",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "oldX"),
                            }
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
                            Object = "ad1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "camera",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "camera",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "theLoop"),
                                new Variable(1, 1, "theCel"),
                                new Variable(2, 1, "oldCel"),
                                new Variable(3, 1, "oldLoop"),
                                new Variable(4, 20, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "camera",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sShowCard",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                    }
                },
                new Script {
                    Number = 270,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm270" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "aCueCounter") },
                        { 1, new Variable(1, 1, "scaredRed") },
                        { 2, new Variable(2, 1, "scaredBlonde") },
                        { 3, new Variable(3, 1, "newRedX") },
                        { 4, new Variable(4, 1, "newBlondeX") },
                        { 5, new Variable(5, 1, "cueCounter") },
                        { 6, new Variable(6, 1, "lookCounter") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm270",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm270",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 150, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm270",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm270",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm270",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cannister",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "plant",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "chairs",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ashtray",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 30, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "vipSign",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theCounter",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "blonde",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "redHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sStealChange",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBlonde",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "oldX"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRedHead",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "oldX"),
                            }
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
                            Object = "ad1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "camera",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "camera",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "theLoop"),
                                new Variable(1, 1, "theCel"),
                                new Variable(2, 1, "oldCel"),
                                new Variable(3, 1, "oldLoop"),
                                new Variable(4, 20, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "camera",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sShowCard",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                    }
                },
            } },
            { 280, new Script[] {
                new Script {
                    Number = 280,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm280" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "scaredBlonde") },
                        { 1, new Variable(1, 1, "scaredRed") },
                        { 2, new Variable(2, 1, "numberDialed") },
                        { 3, new Variable(3, 1, "validNumber") },
                        { 4, new Variable(4, 1, "onPhone") },
                        { 5, new Variable(5, 1, "fighting") },
                        { 6, new Variable(6, 500, "string") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm280",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm280",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm280",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 100, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm280",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm280",
                            Name = "notify",
                            Parameters = new string[] {
                                "what",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm280",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lostDesk",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "largePlant",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "smallPlant",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "post",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ashtray",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 30, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theCounter",
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
                            Object = "blonde",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "redHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRedHead",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "newX"),
                                new Variable(1, 1, "oldX"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBlonde",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "newX"),
                                new Variable(1, 1, "oldX"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "thePhone",
                            Name = "init",
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
                            Object = "phoneHandle",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "phoneHandle",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "p1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "p1",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "p2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "p2",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "p3",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "p3",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "p4",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "p4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ph1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ph1",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ph2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ph2",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ph3",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ph3",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ph4",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ph4",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "phoneBook1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "phoneBook2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFight",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "newX"),
                                new Variable(1, 1, "oldX"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPhone",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                                new Variable(2, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "phones",
                            Name = "onMe",
                        },
                    }
                },
                new Script {
                    Number = 280,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm280" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "scaredBlonde") },
                        { 1, new Variable(1, 1, "scaredRed") },
                        { 2, new Variable(2, 1, "numberDialed") },
                        { 3, new Variable(3, 1, "validNumber") },
                        { 4, new Variable(4, 1, "onPhone") },
                        { 5, new Variable(5, 1, "fighting") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm280",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm280",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm280",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 100, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm280",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm280",
                            Name = "notify",
                            Parameters = new string[] {
                                "what",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm280",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lostDesk",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "largePlant",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "smallPlant",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "post",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ashtray",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 30, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theCounter",
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
                            Object = "blonde",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "redHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRedHead",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "newX"),
                                new Variable(1, 1, "oldX"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBlonde",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "newX"),
                                new Variable(1, 1, "oldX"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "thePhone",
                            Name = "init",
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
                            Object = "phoneHandle",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "phoneHandle",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "p1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "p1",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "p2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "p2",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "p3",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "p3",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "p4",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "p4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ph1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ph1",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ph2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ph2",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ph3",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ph3",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ph4",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ph4",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ad4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "phoneBook1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "phoneBook2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFight",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "newX"),
                                new Variable(1, 1, "oldX"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPhone",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                                new Variable(2, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "phones",
                            Name = "onMe",
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
                        { 0, new Variable(0, 50, "str") },
                        { 50, new Variable(50, 50, "locStr") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm290",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm290",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm290",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 150, "tmpStr"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm290",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm290",
                            Name = "doit",
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
                            Object = "door",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boardingSign",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boardingSign",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sExit270",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnter270",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ABM",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pot",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cabinet",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "light",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "leftPainting",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rightPainting",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDoAnnouncement",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sChair",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sReturning",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sJetWay",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                    }
                },
                new Script {
                    Number = 290,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm290" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "local0") },
                        { 1, new Variable(1, 50, "str") },
                        { 51, new Variable(51, 50, "locStr") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm290",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm290",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm290",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 150, "tmpStr"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm290",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm290",
                            Name = "doit",
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
                            Object = "door",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boardingSign",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boardingSign",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sExit270",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnter270",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ABM",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pot",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cabinet",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "light",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "leftPainting",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rightPainting",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDoAnnouncement",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sChair",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sReturning",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sJetWay",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                    }
                },
            } },
            { 295, new Script[] {
                new Script {
                    Number = 295,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm295" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "cameraFlasherCueCounter") },
                        { 1, new Variable(1, 1, "kidCueCounter") },
                        { 2, new Variable(2, 1, "videoCueCounter") },
                        { 3, new Variable(3, 1, "cameraManCueCounter") },
                        { 4, new Variable(4, 1, "fatReporterCueCounter") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Flash",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm295",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sJetWay",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sJetWay",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTalk",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "videoCameraMan",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cameraFlasher",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cameraMan",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fatReporter",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "kid",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Mr. President",
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
                        { 0, new Variable(0, 1, "birdX") },
                        { 1, new Variable(1, 1, "birdY") },
                        { 2, new Variable(2, 1, "birdFlying") },
                        { 3, new Variable(3, 1, "cueCounter") },
                        { 4, new Variable(4, 25, "crash1Pts") },
                        { 29, new Variable(29, 9, "crash1aPts") },
                        { 38, new Variable(38, 17, "crash2Pts") },
                        { 55, new Variable(55, 21, "crash3Pts") },
                        { 76, new Variable(76, 9, "crash3aPts") },
                        { 85, new Variable(85, 29, "crash4Pts") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SetUpRoom",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm310",
                            Name = "init",
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
                            Object = "rm310",
                            Name = "cue:",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLand",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "aPlane2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCrash",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTakeOff",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFlyGumbo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFly",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
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
                        { 0, new Variable(0, 1, "cueCounter") },
                        { 1, new Variable(1, 1, "dreaming") },
                        { 2, new Variable(2, 1, "palNumber") },
                        { 3, new Variable(3, 1, "choice") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "RetControl",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm320",
                            Name = "init",
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
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFlyingCoach",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
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
                            Object = "tray",
                            Name = "onMe",
                            Parameters = new string[] {
                                "theObj",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tray",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "body",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cloud",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cloud",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "conf",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSqeezeNuts",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
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
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MyForward",
                            Name = "cycleDone",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDreamAthens",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDreamAthens",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sWakeUpAthens",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sWakeUpVenice",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDreamTaj",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sWakeUpTaj",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDreamVenice",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theMagazine",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDreamCasa",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Leisure Suit Bogie",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Ingrid Patti",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPlayGuitar",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
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
                            Object = "floor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm380",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "You",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Desmond",
                            Name = "init",
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
                        { 0, new Variable(0, 1, "cueCounter") },
                        { 1, new Variable(1, 1, "step") },
                        { 2, new Variable(2, 1, "myTicks") },
                        { 3, new Variable(3, 1, "nonPerfect") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm385",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm385",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm385",
                            Name = "newRoom",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cloud",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDyingInFirstClass",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
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
                        { 0, new Variable(0, 1, "cloudX") },
                        { 1, new Variable(1, 1, "cloud2X") },
                        { 2, new Variable(2, 1, "clickCounter") },
                        { 3, new Variable(3, 1, "outOfTime") },
                        { 4, new Variable(4, 1, "larryStop") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm390",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm390",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
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
                            Object = "sPlane",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "panel1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "panel2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "panel3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "panel4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "panel5",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "panel6",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "panel7",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "panel8",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "wheel",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "wheel2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cloud",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cloud2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "frontCloud",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "frontCloud2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFrontCloud",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFrontCloud2",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCloud",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCloud2",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "console",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "throttle",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "windows",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPlaneCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sAutoPilot",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "plane",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "larryHead",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "larry",
                            Name = "cue",
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
                            Object = "bigLarry",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "thePlane",
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "didPlay") },
                        { 2, new Variable(2, 1, "clapCnt") },
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
                            Object = "rm400",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "patrons",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToManager",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromManager",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 405, new Script[] {
                new Script {
                    Number = 405,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm405" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm405",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm405",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Andy",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
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
                            Object = "rm410",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "reflection",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDesmondCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon2",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
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
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "You",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Inspector Desmond",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
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
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm420",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRoom",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Inspector Desmond",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "You",
                            Name = "init",
                        },
                    }
                },
            } },
            { 425, new Script[] {
                new Script {
                    Number = 425,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm425" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm425",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLimo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromLimo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "limo",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
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
                        { 0, new Variable(0, 8, "braPts") },
                        { 8, new Variable(8, 8, "braPts2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm430",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm430",
                            Name = "dispose",
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
                            Object = "sWork",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
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
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sVibrator",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBra",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "twit",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "twit",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "desmondDoor",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "desmondDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "northDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "vibMan",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "braMan",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "techMan",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "techMan",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "techMan",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dartboard",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "monitors",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "plug",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "outlet",
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
                            Object = "joystick",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "circuitboard",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "northBank",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "southBank",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Commander Twit",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Inspector Desmond",
                            Name = "init",
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
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm435",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm435",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetBra",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hooterShooter",
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
                            Object = "dartboard",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "northBank",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "southBank",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "monitors",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "plug",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "outlet",
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
                            Object = "joystick",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "circuitboard",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
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
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm440",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm440",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRoom",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToDoc",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToDoc",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFart",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "twit",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "twit",
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
                            Object = "computer",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fartman",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMix",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Commander Twit",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Dr. Phil Hopian",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "machine",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "contraption",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "southBank",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "books",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "desk",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 445, new Script[] {
                new Script {
                    Number = 445,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm445" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm445",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetData",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theDataMan",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dataPak",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dataPak2",
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
                            Object = "machine",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "computer",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "contraption",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "southBank",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "books",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "desk",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "duckF",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                        { 0, new Variable(0, 1, "seenMsg") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm450",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRoom",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Dr. Phil Hopian",
                            Name = "init",
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
                        { 0, new Variable(0, 1, "cycleColors") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm460",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon2",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDesmondSits",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTalkDesmond",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTalkDesmond",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRogerSleeps",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sHumphreyDrinks",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSleeperSleeps",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pattiActions",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "roger",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "humphrey",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sleeper",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "quayle",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bigg",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "heart",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Mr. Bigg",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "You",
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
                        { 0, new Variable(0, 1, "soundIdx") },
                        { 1, new Variable(1, 25, "soundNums") },
                        { 26, new Variable(26, 4, "sizeElem") },
                        { 30, new Variable(30, 1, "clr") },
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
                                "numLines",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "margin"),
                                new Variable(1, 1, "c"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm480",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm480",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "cycleTime") },
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
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPiss",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sExitLimo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnterLimo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLimoLeaves",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLimoLeaves",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "limo",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "doorR",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lavaLamp1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lavaLamp2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "plants1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "plants2",
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
                        { 0, new Variable(0, 1, "talkCounter") },
                        { 1, new Variable(1, 3, "unused") },
                        { 4, new Variable(4, 1, "okToMakeTape") },
                        { 5, new Variable(5, 1, "tapeHere") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm510",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm510",
                            Name = "notify",
                            Parameters = new string[] {
                                "what",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm510",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm510",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sExitNorth",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnterSouth",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBribe",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "bribe"),
                                new Variable(1, 30, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMouth",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMusicBox",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPhone",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sExitSouth",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMD",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDoubleTalk",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetTape",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "phone",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "woman",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "musicBox",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "musicBox",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tape",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tape",
                            Name = "dispose:",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tape",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "maitreD",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "maitreD",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "LDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door",
                            Name = "init",
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
                            Object = "the maitre d'",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tapeReader",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "diskDrive",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pet",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "appleII",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cashRegister",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "monitor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "atari400",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tapeDrive",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pianoRoll",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "punchCardMachine",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cactus1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cactus2",
                            Name = "doVerb",
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
                        { 0, new Variable(0, 1, "dispSave1") },
                        { 1, new Variable(1, 1, "dispSave2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Restore",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm520",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm520",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm520",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLobby",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCafe",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTapeout",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
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
                        { 0, new Variable(0, 1, "clickX") },
                        { 1, new Variable(1, 1, "clickY") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm525",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm525",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm525",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm525",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToHollerith",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnter",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMichelle",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFatLadyEating",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSitDown",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
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
                            Object = "sExitSouth",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
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
                            Object = "michelle",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fatLady",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "baldMan",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "SprattFamilyTable",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "larryTable",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tables",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cactus",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tapeReader",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "daDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                        { 0, new Variable(0, 1, "michelleCount") },
                        { 1, new Variable(1, 1, "talkedToTable1") },
                        { 2, new Variable(2, 1, "talkedToTable2") },
                        { 3, new Variable(3, 1, "talkedToTable3") },
                        { 4, new Variable(4, 1, "cueCounter") },
                        { 5, new Variable(5, 1, "cueCounter0") },
                        { 6, new Variable(6, 1, "cueCounter2") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm530",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTalk",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnter",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCloseCurtain",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCloseCurtain",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDoMichelle",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMichelleEating",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sOpenCurtain",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "michelle",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drape1",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drape1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drape2",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drape2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drape3",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drape3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drape4",
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
                            Object = "cactus",
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
                            Object = "table2",
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
                            Object = "theTable",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cart",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "light",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Michelle Milken",
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
                        { 0, new Variable(0, 1, "michelleCount") },
                        { 1, new Variable(1, 1, "talkedToTable1") },
                        { 2, new Variable(2, 1, "talkedToTable2") },
                        { 3, new Variable(3, 1, "talkedToTable3") },
                        { 4, new Variable(4, 1, "cueCounter") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm530",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTalk",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnter",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCloseCurtain",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCloseCurtain",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDoMichelle",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMichelleEating",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sOpenCurtain",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "michelle",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drape1",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drape1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drape2",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drape2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drape3",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drape3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "drape4",
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
                            Object = "cactus",
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
                            Object = "table2",
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
                            Object = "theTable",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cart",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "light",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Michelle Milken",
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
                        { 0, new Variable(0, 1, "cueCounter") },
                        { 1, new Variable(1, 1, "oldTime") },
                        { 2, new Variable(2, 1, "secsInRoom") },
                        { 3, new Variable(3, 2, "unused") },
                        { 5, new Variable(5, 1, "giveItems") },
                        { 6, new Variable(6, 1, "didSomething") },
                        { 7, new Variable(7, 1, "loserCounter") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "BranchIt",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "Reset",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm535",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm535",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLoser",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGoDown",
                            Name = "doit",
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
                            Object = "sBlink",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSuckFinger",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCherry",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sStartGoDown",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTrotter",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCreditCards",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMoney",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sConversation",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "michelle",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "mEyes",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "mMouth",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cherry",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tits",
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
                            Object = "Michelle Milken",
                            Name = "init",
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
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCouple",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sExitLimo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnterLimo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "revolvingDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "revolvingDoor",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "revolvingDoor",
                            Name = "open",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "revolvingDoor",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "limo",
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
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm610",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sExitLimo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "revolvingDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "revolvingDoor",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "revolvingDoor",
                            Name = "open",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "revolvingDoor",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "limo",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "guardStatus") },
                        { 1, new Variable(1, 1, "datamanSolution") },
                        { 2, new Variable(2, 1, "faxSolution") },
                        { 3, new Variable(3, 1, "wrongDataman") },
                        { 4, new Variable(4, 1, "wrongFax") },
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
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm620",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm620",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "roomNumber",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sExitElevator",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGuardWakes",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGuardSleeps",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGuardApproves",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sUsesBoard",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sReturnsFromBoard",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBackToSleep",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSleeping",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "guard",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "guard",
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
                            Object = "elevatorLeft",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "board",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "businessOne",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "businessTwo",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "businessThree",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "businessFour",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "businessFive",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "businessSix",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "businessSeven",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "businessEight",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fStairs",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fAshtray",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "breasts",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "The Guard",
                            Name = "init",
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
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "askedInToStudio") },
                        { 2, new Variable(2, 1, "moveNeedle") },
                        { 3, new Variable(3, 1, "music2Playing") },
                        { 4, new Variable(4, 1, "getRecordFirstTime") },
                        { 5, new Variable(5, 1, "turntableCycler") },
                        { 6, new Variable(6, 1, "turntableSpeed") },
                        { 7, new Variable(7, 1, "speakerCycler") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "TalkFromBooth",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm640",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm640",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm640",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm640",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "roomNumber",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sExitElevator",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetRecord",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMonitorRecording",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSpeed78Reverse",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSpeed33Reverse",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSpeed78Forward",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSpeed33Forward",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sUsesStereo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sReturnsFromStereo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sStopButtonPressed",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sForwardButtonPressed",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sReverseButtonPressed",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sButton33Pressed",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sButton78Pressed",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnterElevator",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPlayMusic",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "stereo",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "plaque",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "record",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "record2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                            Object = "reverseBiaz",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "turntable",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "turnTableWRecord",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "needle",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "speakerOne",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "speakerOne",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "speakerTwo",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "MyRandCycle",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "reverseButton",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "stopButton",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "forwardButton",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "button33",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "button78",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Reverse Biaz",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fGoldRecords",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 660, new Script[] {
                new Script {
                    Number = 660,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm660" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "takes") },
                        { 1, new Variable(1, 1, "realTakes") },
                        { 2, new Variable(2, 1, "didSession") },
                        { 3, new Variable(3, 1, "didFF") },
                        { 4, new Variable(4, 1, "talkCounter") },
                        { 5, new Variable(5, 1, "champagneTalkCounter") },
                        { 6, new Variable(6, 1, "didZipper") },
                        { 7, new Variable(7, 1, "mouseDownKey") },
                        { 8, new Variable(8, 1, "keyDownKey") },
                        { 9, new Variable(9, 1, "noteCount") },
                        { 10, new Variable(10, 18, "whiteKeyNums") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "FindKeyboardKey",
                            Parameters = new string[] {
                                "event",
                            },
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "FindKey",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "keyNum"),
                                new Variable(1, 1, "theKey"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm660",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm660",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm660",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEntersRoom",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sWalkIn",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sReverseReminds",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sUsesSynth",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sReturnFromSynth",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sSession",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 20, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMonitorRecording",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMonitorRecording",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sAutoKeys",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theKeyNum"),
                                new Variable(1, 1, "theCue"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEndSession",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnterBooth",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sStopMusic",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDrunkReverse",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTalkScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDoSex",
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
                            Object = "reverseBiaz",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "reverseBiaz",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "reverseBiaz",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "panelExtender",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "reelOne",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "reelTwo",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "reelThree",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "reelFour",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "synthKey",
                            Name = "depress",
                            Parameters = new string[] {
                                "playNote",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "synthKey",
                            Name = "release",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "synthKey",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 1, "evt"),
                                new Variable(1, 1, "newKey"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "synthKey",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "synthKey",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "keyboard",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "keyNum"),
                                new Variable(1, 1, "octave"),
                                new Variable(2, 1, "theKey"),
                                new Variable(3, 1, "keyIsWhite"),
                                new Variable(4, 1, "i"),
                                new Variable(5, 1, "theX"),
                                new Variable(6, 1, "theY"),
                                new Variable(7, 1, "theView"),
                                new Variable(8, 1, "theLoop"),
                                new Variable(9, 1, "theFreq"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "keyboard",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "keyboard",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "evt"),
                                new Variable(1, 1, "theKey"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "synth",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "music",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "synthPanel",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "controlPanel",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Reverse Biaz",
                            Name = "init",
                        },
                    }
                },
            } },
            { 690, new Script[] {
                new Script {
                    Number = 690,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm690" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "soundPlayed") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm690",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFBI",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sweep",
                            Name = "doit",
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "cycleTime") },
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
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm700",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRoll",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromSouth",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sOcean",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromLimo",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromLimo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLimo",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLimo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 110, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGirlTalk",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "guess"),
                                new Variable(1, 1, "num"),
                                new Variable(2, 30, "str"),
                                new Variable(32, 1, "saveX"),
                                new Variable(33, 1, "saveY"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bird",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bird",
                            Name = "cue",
                            Temps = new Variable[] {
                                new Variable(0, 1, "theX"),
                                new Variable(1, 1, "theY"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "doorman",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "doorman",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "changeGirl",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "changeGirl",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "guess"),
                                new Variable(1, 1, "num"),
                                new Variable(2, 30, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "trampSign",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "slotSign",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "leftLight",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rightLight",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "leftNip",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "leftNip",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rightNip",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rightNip",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "centerNip",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "centerNip",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "leftRoller",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "leftRoller",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "middleRoller",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rightRoller",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "leftNeon",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rightNeon",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "centerNeon",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dollars",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "burgerStand",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "burgerStand",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "breasts1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "breasts2",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "breasts3",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ass1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ass2",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ass3",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "girl1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "girl2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "girl3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Cheri",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Doorman",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rightLightF",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dollarsF",
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
                            Name = "doit",
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
                            Object = "sClosed",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sToNorth",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromNorth",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "person0",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "person0",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "person0",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "person1",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "person1",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "person2",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "person2",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "person3",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "person3",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "person4",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "person4",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "person5",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "person5",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "girlPic1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "girlPic2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rouletteArea",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "blackjackArea",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "chandelier",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "chandelier2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "statue1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "statue2",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "statue3",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "statue4",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "statue5",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poker1",
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
                            Object = "poker3",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poker5",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poker6",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poker7",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poker8",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poker9",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poker10",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poker11",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poker12",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poker15",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poker16",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poker17",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poker18",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poker19",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poker20",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poker21",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "poker22",
                            Name = "doVerb",
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
                        { 0, new Variable(0, 1, "oldSpeed") },
                        { 1, new Variable(1, 1, "handsPlayed") },
                        { 2, new Variable(2, 5, "drop") },
                        { 7, new Variable(7, 15, "nTally") },
                        { 22, new Variable(22, 4, "sTally") },
                        { 26, new Variable(26, 10, "payoff") },
                        { 36, new Variable(36, 1, "theHand") },
                        { 37, new Variable(37, 1, "winnings") },
                        { 38, new Variable(38, 1, "theBet") },
                        { 39, new Variable(39, 1, "credits") },
                        { 40, new Variable(40, 1, "paid") },
                        { 41, new Variable(41, 1, "nextCard") },
                        { 42, new Variable(42, 1, "holdOK") },
                        { 43, new Variable(43, 1, "dealOK") },
                        { 44, new Variable(44, 1, "wUnders") },
                        { 45, new Variable(45, 1, "bUnders") },
                        { 46, new Variable(46, 1, "cUnders") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "NewDeck",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "theVal"),
                                new Variable(2, 1, "theSuit"),
                                new Variable(3, 4, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "DealCard",
                            Parameters = new string[] {
                                "which",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "oldCard"),
                                new Variable(1, 1, "newCard"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "TallyHand",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "loCard"),
                                new Variable(2, 1, "hiCard"),
                                new Variable(3, 1, "xCard"),
                                new Variable(4, 1, "consecutive"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "SortTallies",
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "j"),
                                new Variable(2, 1, "foo"),
                                new Variable(3, 100, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "PayBet",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "DisplayStats",
                            Temps = new Variable[] {
                                new Variable(0, 50, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "CountScore",
                            Parameters = new string[] {
                                "whichSound",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm720",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm720",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 200, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm720",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm720",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sortCode",
                            Name = "doit",
                            Parameters = new string[] {
                                "theObj",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theCel"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "clearCode",
                            Name = "doit",
                            Parameters = new string[] {
                                "theObj",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "uniqueCode",
                            Name = "doit",
                            Parameters = new string[] {
                                "theObj",
                                "v",
                                "s",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "incBet",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "incBet",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "incBet",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "evt"),
                                new Variable(1, 1, "ticks"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "incBet",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 10, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "decBet",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "decBet",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "decBet",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "evt"),
                                new Variable(1, 1, "ticks"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "decBet",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 10, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cashout",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "deal",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hold1",
                            Name = "onMe",
                            Temps = new Variable[] {
                                new Variable(0, 1, "theCard"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hold1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hold2",
                            Name = "onMe",
                            Temps = new Variable[] {
                                new Variable(0, 1, "theCard"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hold2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hold3",
                            Name = "onMe",
                            Temps = new Variable[] {
                                new Variable(0, 1, "theCard"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hold3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hold4",
                            Name = "onMe",
                            Temps = new Variable[] {
                                new Variable(0, 1, "theCard"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hold4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hold5",
                            Name = "onMe",
                            Temps = new Variable[] {
                                new Variable(0, 1, "theCard"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "hold5",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPlayPoker",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 50, "str"),
                            }
                        },
                    }
                },
            } },
            { 730, new Script[] {
                new Script {
                    Number = 730,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm730" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "justWatching") },
                        { 2, new Variable(2, 1, "mainEvent") },
                        { 3, new Variable(3, 1, "payoff") },
                        { 4, new Variable(4, 1, "paid") },
                        { 5, new Variable(5, 1, "contestantState") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "InitFeatures",
                        },
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "DisposeFeatures",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm730",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm730",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm730",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm730",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLeave",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromSouth",
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
                            Object = "sStandUp",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sWrestle",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sContest",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLarryIntoRing",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTaunt",
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
                            Object = "contestant",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lana",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bouncer",
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
                            Object = "bouncer",
                            Name = "cue",
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
                            Object = "The Bouncer",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Lana",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "stage",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "runway",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 740, new Script[] {
                new Script {
                    Number = 740,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm740" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "doitCounter") },
                        { 1, new Variable(1, 1, "partCounter") },
                        { 2, new Variable(2, 1, "lSeconds") },
                        { 3, new Variable(3, 1, "thisLSeconds") },
                        { 4, new Variable(4, 1, "lastLSeconds") },
                        { 5, new Variable(5, 1, "missTime") },
                        { 6, new Variable(6, 1, "grabs") },
                        { 7, new Variable(7, 1, "wrestleSeconds") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm740",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm740",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm740",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm740",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRoom",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRoar",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFadeout",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bodyPart",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bodyPart",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "findWhere",
                            Name = "doit",
                            Parameters = new string[] {
                                "who",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theX"),
                                new Variable(1, 1, "theY"),
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
                            Object = "larry",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "larry",
                            Name = "cue",
                            Parameters = new string[] {
                                "cueType",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "theAng"),
                                new Variable(1, 1, "theX"),
                                new Variable(2, 1, "theY"),
                            }
                        },
                    }
                },
            } },
            { 750, new Script[] {
                new Script {
                    Number = 750,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm750" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 29, "shirtPts") },
                        { 29, new Variable(29, 33, "pantsPts") },
                        { 62, new Variable(62, 37, "jacketPts") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm750",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "guy1",
                            Name = "cue",
                        },
                    }
                },
            } },
            { 760, new Script[] {
                new Script {
                    Number = 760,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm760" },
                        { 1, "bench" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "seenWarning") },
                        { 2, new Variable(2, 1, "movingOffControl") },
                        { 3, new Variable(3, 1, "egosPrevDir") },
                        { 4, new Variable(4, 1, "caughtLana") },
                        { 5, new Variable(5, 1, "smallView") },
                        { 6, new Variable(6, 1, "largeView") },
                        { 7, new Variable(7, 1, "talkCount") },
                        { 8, new Variable(8, 1, "eX") },
                        { 9, new Variable(9, 1, "eY") },
                        { 10, new Variable(10, 1, "obj2Cue") },
                        { 11, new Variable(11, 1, "verb2Use") },
                        { 12, new Variable(12, 4, "shrinkY") },
                        { 16, new Variable(16, 4, "growY") },
                        { 20, new Variable(20, 1, "gonnaFall") },
                        { 21, new Variable(21, 7, "building") },
                        { 28, new Variable(28, 17, "buildWidth") },
                        { 45, new Variable(45, 1, "learnCounter") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm760",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "theView"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm760",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm760",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm760",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm760",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "eType"),
                                new Variable(1, 1, "eMsg"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm760",
                            Name = "drawPic",
                            Parameters = new string[] {
                                "thePic",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "val"),
                                new Variable(2, 1, "theLoop"),
                                new Variable(3, 1, "theCel"),
                                new Variable(4, 1, "nextX"),
                                new Variable(5, 1, "theX"),
                                new Variable(6, 1, "appX"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Skater",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Skater",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Skater",
                            Name = "turn",
                            Parameters = new string[] {
                                "theDir",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Skater",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lana",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "roomLo"),
                                new Variable(1, 1, "roomHi"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lana",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lana",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lana",
                            Name = "turn",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lana",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lana",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater1",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater1",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater2",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater2",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater3",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater3",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater4",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater4",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater5",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater5",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater6",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater6",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater7",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater7",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater8",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater8",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater9",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater9",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "zCode",
                            Name = "doit",
                            Parameters = new string[] {
                                "theObj",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "showSkaterCode",
                            Name = "doit",
                            Parameters = new string[] {
                                "obj",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theBuilding",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "evt"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rightEye",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "leftEye",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "nose",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "nose",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lanaMouth",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Lana Luscious",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Lana Luscious",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromWest",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFall",
                            Name = "doit",
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
                            Object = "sScroll",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMeetLana",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLanaSits",
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
                            Object = "sDoSkates",
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
                            Object = "sMoveLarry",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "idx"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLanaTalks",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lanaFtr",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "buildings",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bench",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "who2Cue"),
                            }
                        },
                    }
                },
                new Script {
                    Number = 760,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm760" },
                        { 1, "bench" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "seenWarning") },
                        { 2, new Variable(2, 1, "movingOffControl") },
                        { 3, new Variable(3, 1, "egosPrevDir") },
                        { 4, new Variable(4, 1, "caughtLana") },
                        { 5, new Variable(5, 1, "smallView") },
                        { 6, new Variable(6, 1, "largeView") },
                        { 7, new Variable(7, 1, "talkCount") },
                        { 8, new Variable(8, 1, "eX") },
                        { 9, new Variable(9, 1, "eY") },
                        { 10, new Variable(10, 1, "obj2Cue") },
                        { 11, new Variable(11, 1, "verb2Use") },
                        { 12, new Variable(12, 4, "shrinkY") },
                        { 16, new Variable(16, 11, "growY") },
                        { 27, new Variable(27, 17, "buildWidth") },
                        { 44, new Variable(44, 1, "learnCounter") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm760",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "theView"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm760",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm760",
                            Name = "doit",
                            Temps = new Variable[] {
                                new Variable(0, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm760",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm760",
                            Name = "handleEvent",
                            Parameters = new string[] {
                                "event",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "eType"),
                                new Variable(1, 1, "eMsg"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm760",
                            Name = "drawPic",
                            Parameters = new string[] {
                                "thePic",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "val"),
                                new Variable(2, 1, "theLoop"),
                                new Variable(3, 1, "theCel"),
                                new Variable(4, 1, "nextX"),
                                new Variable(5, 1, "theX"),
                                new Variable(6, 1, "appX"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Skater",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Skater",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Skater",
                            Name = "turn",
                            Parameters = new string[] {
                                "theDir",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Skater",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lana",
                            Name = "init",
                            Temps = new Variable[] {
                                new Variable(0, 1, "roomLo"),
                                new Variable(1, 1, "roomHi"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lana",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lana",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lana",
                            Name = "turn",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lana",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lana",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater1",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater1",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater2",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater2",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater3",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater3",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater4",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater4",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater5",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater5",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater6",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater6",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater7",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater7",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater8",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater8",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater9",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skater9",
                            Name = "checkDetail",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "zCode",
                            Name = "doit",
                            Parameters = new string[] {
                                "theObj",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "showSkaterCode",
                            Name = "doit",
                            Parameters = new string[] {
                                "obj",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theBuilding",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "evt"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rightEye",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "leftEye",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "nose",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "nose",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lanaMouth",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Lana Luscious",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Lana Luscious",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromWest",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFall",
                            Name = "doit",
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
                            Object = "sScroll",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sMeetLana",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLanaSits",
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
                            Object = "sDoSkates",
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
                            Object = "sMoveLarry",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "idx"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLanaTalks",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lanaFtr",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "buildings",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "bench",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "who2Cue"),
                            }
                        },
                    }
                },
            } },
            { 780, new Script[] {
                new Script {
                    Number = 780,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm780" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm780",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm780",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sScroll",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "wave1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "wave2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "wave3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "post1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "post2",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "post3",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "post4",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "swimActions",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 790, new Script[] {
                new Script {
                    Number = 790,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm790" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "talked") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm790",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm790",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm790",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sIvanaEnters",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sFromSouth",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTalk",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "num"),
                                new Variable(1, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCamcorder",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sRollerblades",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "num"),
                                new Variable(1, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Ivana",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "num"),
                                new Variable(1, 80, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "skates",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "curtain",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "curtain",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "box",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Ivana",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sitActions",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 800, new Script[] {
                new Script {
                    Number = 800,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm800" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm800",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm800",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "limo",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "neonSign",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cone1",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "cone2",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sIntoBuilding",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "exitDreamScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sExitLimo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnterLimo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                    }
                },
            } },
            { 820, new Script[] {
                new Script {
                    Number = 820,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm820" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm820",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm820",
                            Name = "notify",
                            Parameters = new string[] {
                                "codeNumber",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm820",
                            Name = "dispose",
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
                            Object = "keypad",
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
                    }
                },
            } },
            { 840, new Script[] {
                new Script {
                    Number = 840,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm840" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "haveSeenBothThings") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm840",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm840",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm840",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "desk",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "plant",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "copier",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "johnDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "johnDoor",
                            Name = "close",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lobbyDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lobbyDoor",
                            Name = "close",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "telephone",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "computer",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "opener",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "peekScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "getKeyScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPawThruPlant",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "replaceKeyScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "openDeskScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 100, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sOpenDesk2ndTime",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 100, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lookDeskScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 100, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLookDesk2ndTime",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 100, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "pickLockScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "unlockDeskScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lockDeskScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "getPapersScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lookPapersScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "putPapersScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "runCopierScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ActionsKRAP",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ActionsKRAP",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm850",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm850",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm850",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "shower",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "showerSide",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "officeDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "officeDoor",
                            Name = "close",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "officeDoor",
                            Name = "open",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                            Object = "myWindow",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "curtain1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "curtain2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sink1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sink2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "TakeAShowerScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "invItem"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ActionsKRAP",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ActionsKRAP",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm860",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPeeping",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 870, new Script[] {
                new Script {
                    Number = 870,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm870" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm870",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm870",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm870",
                            Name = "notify",
                            Parameters = new string[] {
                                "codeNumber",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "keypad",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "studioARoom",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "elevatorKeypad",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "clothes",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dude1",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dude2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "dude3",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
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
                            Object = "eDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "elevatorSide",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "goingDownScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "getClothesScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ActionsKRAP",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ActionsKRAP",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 880, new Script[] {
                new Script {
                    Number = 880,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm880" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "enteredCode") },
                        { 1, new Variable(1, 1, "correctDoor") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm880",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm880",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm880",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm880",
                            Name = "notify",
                            Parameters = new string[] {
                                "codeNumber",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "doorA",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "doorC",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "keypadA",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "keypadB",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "keypadC",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "PChammer",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "mikeStand",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ActionsKRAP",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ActionsKRAP",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 890, new Script[] {
                new Script {
                    Number = 890,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm890" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenMsg") },
                        { 1, new Variable(1, 1, "recording") },
                        { 2, new Variable(2, 1, "recorded") },
                        { 3, new Variable(3, 1, "jammed") },
                        { 4, new Variable(4, 1, "usedMixer") },
                        { 5, new Variable(5, 1, "egoMove") },
                        { 6, new Variable(6, 1, "mixerLookCounter") },
                        { 7, new Variable(7, 1, "mixerDoCounter") },
                        { 8, new Variable(8, 1, "volumeUp") },
                        { 9, new Variable(9, 1, "rewoundTape") },
                        { 10, new Variable(10, 1, "displayVar") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm890",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm890",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm890",
                            Name = "dispose",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fElectronics4",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "fElectronics5",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tapeShelf",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "mixer",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "microphone",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "recorder",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tape",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "tapeHole",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lights",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "useMikeScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "i"),
                                new Variable(1, 1, "invItem"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "GetTape",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "turnRecorderOn",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "turnRecorderOff",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rewindTape",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "getRecordedTape",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "useMixer",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sHearingScrew",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "mountTapeScript",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "doorA",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ActionsKRAP",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "ActionsKRAP",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                    }
                },
            } },
            { 900, new Script[] {
                new Script {
                    Number = 900,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm900" },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm900",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm900",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm900",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm900",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "gymWindow",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "gymDoor",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "gymSign",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "stairs",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "limo",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sExitLimo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLimoLeaves",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sLimoLeaves",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "theDoor",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnterLimo",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                    }
                },
            } },
            { 905, new Script[] {
                new Script {
                    Number = 905,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm905" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenDoilyMsg") },
                        { 1, new Variable(1, 1, "talkedToGirl") },
                        { 2, new Variable(2, 1, "talkCounter") },
                        { 3, new Variable(3, 1, "usePhone") },
                        { 4, new Variable(4, 1, "cueCounter") },
                        { 5, new Variable(5, 1, "whoToTalkTo") },
                        { 6, new Variable(6, 1, "onPhone") },
                        { 7, new Variable(7, 1, "gotEvent") },
                        { 8, new Variable(8, 1, "returningFrom910") },
                        { 9, new Variable(9, 1, "windowTalkCount") },
                        { 10, new Variable(10, 1, "satDown") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "NormalEgo",
                            Parameters = new string[] {
                                "theLoop",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm905",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm905",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm905",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm905",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm905",
                            Name = "notify",
                            Parameters = new string[] {
                                "what",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "magazines",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "palm",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "palm2",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lChair",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rChair",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rChair2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lTable",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rTable",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "glass",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "phone",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Alberta",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnterFromOutside",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sExitToOutside",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sHangUp",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sChair",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sChair",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPhone",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "nurseWindow",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door",
                            Name = "close",
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
                            Object = "sWindow",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                                new Variable(2, 60, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "laceDoily",
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
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetDoily",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
                new Script {
                    Number = 905,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm905" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "seenDoilyMsg") },
                        { 1, new Variable(1, 1, "talkedToGirl") },
                        { 2, new Variable(2, 1, "talkCounter") },
                        { 3, new Variable(3, 1, "usePhone") },
                        { 4, new Variable(4, 1, "cueCounter") },
                        { 5, new Variable(5, 1, "whoToTalkTo") },
                        { 6, new Variable(6, 1, "onPhone") },
                        { 7, new Variable(7, 1, "gotEvent") },
                        { 8, new Variable(8, 1, "returningFrom910") },
                        { 9, new Variable(9, 1, "windowTalkCount") },
                        { 10, new Variable(10, 1, "satDown") },
                        { 11, new Variable(11, 1, "local11") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Procedure,
                            Name = "NormalEgo",
                            Parameters = new string[] {
                                "theLoop",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm905",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm905",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm905",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm905",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm905",
                            Name = "notify",
                            Parameters = new string[] {
                                "what",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "magazines",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "palm",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "palm2",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lChair",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rChair",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rChair2",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "lTable",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rTable",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "glass",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "phone",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Alberta",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sEnterFromOutside",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sExitToOutside",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sHangUp",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sChair",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sChair",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sPhone",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "nurseWindow",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "door",
                            Name = "close",
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
                            Object = "sWindow",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                                new Variable(2, 60, "str"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "laceDoily",
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
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGetDoily",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 910, new Script[] {
                new Script {
                    Number = 910,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm910" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "blouseOpen") },
                        { 1, new Variable(1, 1, "closeUpSecs") },
                        { 2, new Variable(2, 1, "secsDrilled") },
                        { 3, new Variable(3, 1, "oldTime") },
                        { 4, new Variable(4, 1, "allDone") },
                        { 5, new Variable(5, 1, "scored") },
                        { 6, new Variable(6, 1, "saidJoke") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm910",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm910",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm910",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Chi Chi Lambada",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCloseUp",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBringInChiChi",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sWorkOnTeeth",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sDance",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "chiChi",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "chiChi",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sScored",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                            Temps = new Variable[] {
                                new Variable(0, 1, "var1"),
                                new Variable(1, 1, "var2"),
                            }
                        },
                    }
                },
            } },
            { 915, new Script[] {
                new Script {
                    Number = 915,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm915" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 1, "blouseOpen") },
                        { 1, new Variable(1, 1, "oldTime") },
                        { 2, new Variable(2, 1, "closeUpSecs") },
                        { 3, new Variable(3, 1, "talkCounter") },
                        { 4, new Variable(4, 1, "buttonCounter") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm915",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm915",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm915",
                            Name = "doit",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sTalk",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "chichis",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "herButton",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boobs",
                            Name = "cue",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "boobs",
                            Name = "doVerb",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "Chi Chi Lambada",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "herEye",
                            Name = "doVerb",
                            Parameters = new string[] {
                                "theVerb",
                                "invItem",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sBackToWork",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sHideBoobs",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sStopThat",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sGiveGreen",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
            { 920, new Script[] {
                new Script {
                    Number = 920,
                    Exports = new Dictionary<int, string> {
                        { 0, "rm920" },
                    },
                    Locals = new Dictionary<int, Variable> {
                        { 0, new Variable(0, 25, "runJumpPts") },
                    },
                    Functions = new Function[] {
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm920",
                            Name = "init",
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "rm920",
                            Name = "newRoom",
                            Parameters = new string[] {
                                "n",
                            },
                        },
                        new Function {
                            Type = FunctionType.Method,
                            Object = "sCartoon",
                            Name = "changeState",
                            Parameters = new string[] {
                                "newState",
                            },
                        },
                    }
                },
            } },
        };
    }
}
