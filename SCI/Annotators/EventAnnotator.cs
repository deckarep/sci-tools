using System.Collections.Generic;
using System.Linq;
using SCI.Language;
using SCI.Annotators.Visitors;

namespace SCI.Annotators
{
    // EventAnnotator converts numbers into Event symbols when it can, and
    // annotates them when it can't. If there is an accurate symbol definition
    // in the SCI fan template then the number can be converted, but if it's
    // a mask with multiple flags, or the template symbol is wrong or doesn't
    // apply to the game version, or if it's a known value that the template
    // doesn't have a symbol for, then it's annotated and reformatted as hex.
    //
    // Events have three properties with enum values: type, message, modifiers.
    //
    // Two pass algorithm on every function:
    // 1. Detect all symbols that are assigned Event objects and the properties.
    // 2. Annotate all integers that come into contact with these symbols,
    //    or methods that return events like User:curEvent or Event:new.
    //
    // When I can, I just replace integers with enums. But sometimes the
    // integers are masks, so I detect that and annotate with "flag1 | flag2 ...".
    //
    // Other considerations:
    // - Event types changes between SCI versions
    // - SCI Fan template doesn't take SCI versions into account
    // - SCI Fan template doesn't have symbols for every event type
    // - SCI Fan template has some mistakes
    // - KQ5 uses event messages as verbs, so have to not touch those

    static class EventAnnotator
    {
        enum EventProp
        {
            None,
            Type,
            Message,
            Modifier
        }

        public static void Run(Game game, bool sci32)
        {

            var state = new State();
            var db = new DeepBeige();
            db.OnNode = (Node node) =>
            {
                FindSymbols(node, state);
            };

            // kq5 used Event:message for verbs, requires not enum'ing
            // message integers in non-system scripts.
            bool kq5 = (game.GetExport(0, 0) == "KQ5");

            foreach (var function in game.GetFunctions())
            {
                // lsl1,etc pragmaFail method in script 0
                // compares Event:message against game verbs, which this
                // annotator isn't smart enough to know about. skip 'em.
                // otherwise they will become joystick constants.
                if (function.Name == "pragmaFail") continue;

                state.Reset();

                // add known event parameters as symbols
                if ((function.Name == "handleEvent" ||
                     function.Name == "dispatchEvent") &&
                    function.Parameters.Any())
                {
                    state.EventSymbols.Add(function.Parameters[0].Name);
                }

                // find symbols for events, event types, etc
                db.Visit(function);

                // annotate!
                Annotate(function, state, kq5, sci32);
            }
        }

        static void FindSymbols(Node node, State state)
        {
            // only interested in non-complex assignments.
            // if the destination is an event or an event property
            // then add the source to the set of known symbols.
            if (node.Children.Count == 3 &&
                node.At(0).Text == "=" &&
                node.At(1) is Atom)
            {
                var dest = node.At(1);
                var source = GetAssignmentSource(node);
                if (state.IsEvent(source))
                {
                    state.EventSymbols.Add(dest.Text);
                }
                else if (state.IsType(source))
                {
                    state.TypeSymbols.Add(dest.Text);
                }
                else if (state.IsMessage(source))
                {
                    state.MessageSymbols.Add(dest.Text);
                }
                else if (state.IsModifier(source))
                {
                    state.ModifierSymbols.Add(dest.Text);
                }
            }
        }

        // recurse to handle (= temp0 (= temp1 source))
        static Node GetAssignmentSource(Node assignment)
        {
            var source = assignment.At(2);
            if (source.At(0).Text == "=")
            {
                return GetAssignmentSource(source);
            }
            return source;
        }

        static void Annotate(Function function, State state, bool kq5, bool sci32)
        {
            // symbols have already been identified so we can just
            // iterate through nodes normally; deep beige already
            // took care of visiting them in execution order.
            foreach (var node in function.Node)
            {
                // (event type:/message:/modifier: integer)
                if (node.Text == "type:" &&
                    node.Next() is Integer &&
                    state.IsEvent(node.Parent.At(0)))
                {
                    AnnotateType(node.Next() as Integer, sci32);
                    continue;
                }
                if (node.Text == "message:" &&
                    node.Next() is Integer &&
                    state.IsEvent(node.Parent.At(0)))
                {
                    AnnotateMessage(node.Next(), kq5, function.Script);
                    continue;
                }
                if (node.Text == "modifiers:" &&
                     node.Next() is Integer &&
                    state.IsEvent(node.Parent.At(0)))
                {
                    AnnotateModifier(node.Next());
                    continue;
                }

                // (Event new: type)
                if (node.Text == "new:" &&
                    node.Parent.At(0).Text == "Event"
                    && node.Next() is Integer)
                {
                    AnnotateType(node.Next() as Integer, sci32);
                    continue;
                }

                // (GetEvent type)
                if (node.Text == "GetEvent" &&
                    node.Next() is Integer)
                {
                    AnnotateType(node.Next() as Integer, sci32);
                    continue;
                }

                // comparison / math / assignment
                switch (node.At(0).Text)
                {
                    case "=":
                    case "==":
                    case "!=":
                    case ">":
                    case ">=":
                    case "u>":
                    case "u>=":
                    case "<":
                    case "<=":
                    case "u<":
                    case "u<=":
                    case "&":
                    case "&=":
                    case "|":
                    case "|=":
                    case "+":
                    case "+=":
                        // scan for an operand that is an event prop
                        foreach (var expression in node.Children.Skip(1))
                        {
                            var eventProp = state.GetEventProp(expression);
                            if (eventProp != EventProp.None)
                            {
                                // annotate all integer operands accordingly
                                foreach (var number in node.Children.Where(n => n is Integer))
                                {
                                    Annotate(eventProp, number, kq5, sci32, function.Script);
                                }
                                break;
                            }
                        }
                        break;
                }

                // switch
                if (node.At(0).Text == "switch")
                {
                    var eventProp = state.GetEventProp(node.At(1));
                    if (eventProp != EventProp.None)
                    {
                        foreach (var case_ in node.Children.Skip(2))
                        {
                            if (case_.At(0) is Integer)
                            {
                                Annotate(eventProp, case_.At(0), kq5, sci32, function.Script);
                            }
                        }
                    }
                }

                // OneOf
                if (node.At(0).Text == "OneOf")
                {
                    var eventProp = state.GetEventProp(node.At(1));
                    if (eventProp != EventProp.None)
                    {
                        foreach (var parameter in node.Children.Skip(2))
                        {
                            if (parameter is Integer)
                            {
                                Annotate(eventProp, parameter, kq5, sci32, function.Script);
                            }
                        }
                    }
                }
            }
        }

        static void Annotate(EventProp eventProp, Node node, bool kq5, bool sci32, Script script)
        {
            if (eventProp == EventProp.Type)
            {
                if (node is Integer) // always
                {
                    AnnotateType(node as Integer, sci32);
                }
            }
            else if (eventProp == EventProp.Message)
            {
                AnnotateMessage(node, kq5, script);
            }
            else if (eventProp == EventProp.Modifier)
            {
                AnnotateModifier(node);
            }
        }

        static void AnnotateType(Integer node, bool sci32)
        {
            EventType eventType;
            var eventTypes = sci32 ? EventTypes32 : EventTypes16;
            node.MakeUnsigned(); // required for KernelCallAnnotator.MakeFlagString
            if (eventTypes.TryGetValue(node.Number, out eventType))
            {
                if (eventType.Annotate)
                {
                    node.Annotate(eventType.Name);
                    node.SetHexFormat();
                }
                else
                {
                    node.SetDefineText(eventType.Name);
                }
            }
            else
            {
                // forgive me
                var map = eventTypes.ToDictionary(kv => kv.Key, kv => kv.Value.Name);
                string flagString = KernelCallAnnotator.MakeFlagString(node.Number, map);
                if (!string.IsNullOrWhiteSpace(flagString))
                {
                    node.Annotate(flagString);
                }
                node.SetHexFormat();
            }
        }

        static void AnnotateMessage(Node node, bool kq5, Script script)
        {
            // KQ5 workaround: don't annotate Event:message values 0-8
            // in non-system scripts; this was the first point and click
            // game and they used Event:message for verbs. They are not
            // joystick events. Kq5VerbAnnotator handles them.
            if (kq5 && (script.Number != 255 || script.Number < 900) && node.Number <= 8) return;

            string name;
            if (EventMessages.TryGetValue(node.Number, out name))
            {
                (node as Integer).SetDefineText(name);
            }
            else
            {
                if (AnnotateEventMessages.TryGetValue(node.Number, out name))
                {
                    node.Annotate(name);
                }
                (node as Integer).SetHexFormat();
            }
        }

        static void AnnotateModifier(Node node)
        {
            string name;
            if (EventModifiers.TryGetValue(node.Number, out name))
            {
                (node as Integer).SetDefineText(name);
            }
            else
            {
                string flagString = KernelCallAnnotator.MakeFlagString(node.Number, EventModifiers);
                if (!string.IsNullOrWhiteSpace(flagString))
                {
                    node.Annotate(flagString);
                }
                (node as Integer).SetHexFormat();
            }
        }

        class State
        {
            public HashSet<string> EventSymbols;
            public HashSet<string> TypeSymbols;
            public HashSet<string> MessageSymbols;
            public HashSet<string> ModifierSymbols;

            public State()
            {
                EventSymbols = new HashSet<string>();
                TypeSymbols = new HashSet<string>();
                MessageSymbols = new HashSet<string>();
                ModifierSymbols = new HashSet<string>();
            }

            public bool IsEvent(Node node)
            {
                // known
                if (EventSymbols.Contains(node.Text) ||
                    IsAssignment(node, EventSymbols))
                {
                    return true;
                }

                if (node is List)
                {
                    // (User ... curEvent:)
                    if (node.LastOrDefault()?.Text == "curEvent:")
                    {
                        return true;
                    }

                    // (Event new:) or (event new:) [ not sure if that second one ever happens ]
                    if ((node.At(0).Text == "Event" || EventSymbols.Contains(node.At(0).Text)) &&
                        node.LastOrDefault()?.Text == "new:")
                    {
                        return true;
                    }
                }
                return false;
            }

            // this is for detecting a node that evaluates to a type,
            // but what about a node that sets a type? maybe that's different
            public bool IsType(Node node)
            {
                // known
                if (TypeSymbols.Contains(node.Text) ||
                    IsAssignment(node, TypeSymbols))
                {
                    return true;
                }

                if (node is List)
                {
                    // (event ... type:)
                    if (IsEvent(node.At(0)) &&
                        node.LastOrDefault()?.Text == "type:")
                    {
                        return true;
                    }
                }
                return false;
            }

            public bool IsMessage(Node node)
            {
                // known
                if (MessageSymbols.Contains(node.Text) ||
                    IsAssignment(node, MessageSymbols))
                {
                    return true;
                }

                if (node is List)
                {
                    // (event ... type:)
                    if (IsEvent(node.At(0)) &&
                        node.LastOrDefault()?.Text == "message:")
                    {
                        return true;
                    }
                }
                return false;
            }

            public bool IsModifier(Node node)
            {
                // known
                if (ModifierSymbols.Contains(node.Text) ||
                    IsAssignment(node, ModifierSymbols))

                {
                    return true;
                }

                if (node is List)
                {
                    // (event ... type:)
                    if (IsEvent(node.At(0)) &&
                        node.LastOrDefault()?.Text == "modifiers:")
                    {
                        return true;
                    }
                }
                return false;
            }

            // returns true if node is (= [symbol-in-set] ...),
            // including recursive (= temp0 (= [symbol-in-set]) ...)
            static bool IsAssignment(Node node, HashSet<string> set)
            {
                if (node.At(0).Text == "=")
                {
                    if (set.Contains(node.At(1).Text)) return true;
                    return IsAssignment(node.At(2), set);
                }
                return false;
            }

            public EventProp GetEventProp(Node node)
            {
                if (IsType(node)) return EventProp.Type;
                if (IsMessage(node)) return EventProp.Message;
                if (IsModifier(node)) return EventProp.Modifier;
                return EventProp.None;
            }

            public void Reset()
            {
                EventSymbols.Clear();
                TypeSymbols.Clear();
                MessageSymbols.Clear();
                ModifierSymbols.Clear();
            }
        }

        class EventType
        {
            public readonly string Name;
            public readonly bool Annotate;
            public EventType(string name, bool annotate = false) { Name = name; Annotate = annotate; }
        }

        static Dictionary<int, EventType> EventTypes16 = new Dictionary<int, EventType>
        {
            { 0x0000, new EventType("evNULL") },
            { 0x0001, new EventType("evMOUSEBUTTON") },
            { 0x0002, new EventType("evMOUSERELEASE") },
            { 0x0003, new EventType("evMOUSE") },
            { 0x0004, new EventType("evKEYBOARD") },
            { 0x0005, new EventType("evMOUSEKEYBOARD") },
            { 0x0008, new EventType("evKEYUP") },
            { 0x0010, new EventType("evMENUSTART") },
            { 0x0020, new EventType("evMENUHIT") },
            { 0x0040, new EventType("direction", true) }, // evJOYSTICK, wrong
            { 0x0080, new EventType("evSAID") }, // speechEvent in SCI1.1, don't care
            { 0x0100, new EventType("evJOYDOWN") },
            { 0x0200, new EventType("evJOYUP") },
            { 0x1000, new EventType("evMOVE") },
            { 0x2000, new EventType("evHELP") },
            { 0x4000, new EventType("evVERB") }, // userEvent
            { 0x7FFF, new EventType("evALL_EVENTS") },
            { 0x8000, new EventType("evPEEK") },
        };

        static Dictionary<int, EventType> EventTypes32 = new Dictionary<int, EventType>
        {
            { 0x0000, new EventType("evNULL") },
            { 0x0001, new EventType("evMOUSEBUTTON") },
            { 0x0002, new EventType("evMOUSERELEASE") },
            { 0x0003, new EventType("evMOUSE") },
            { 0x0004, new EventType("evKEYBOARD") },
            { 0x0005, new EventType("evMOUSEKEYBOARD") },
            { 0x0008, new EventType("evKEYUP") },
            { 0x0010, new EventType("direction", true) }, // $40 in SCI16
            { 0x0020, new EventType("joyUp", true) },
            { 0x0040, new EventType("joyDown", true) },
            { 0x0060, new EventType("joyEvent", true) },
            { 0x0100, new EventType("speechEvent", true) },
            { 0x0200, new EventType("extMouse", true) },
            { 0x1000, new EventType("evMOVE") },
            { 0x2000, new EventType("evHELP") },
            { 0x4000, new EventType("evVERB") }, // userEvent
            { 0x7FFF, new EventType("evALL_EVENTS") },
            { 0x8000, new EventType("evPEEK") },
        };

        static Dictionary<int, string> EventModifiers = new Dictionary<int, string>
        {
            { 1, "emRIGHT_SHIFT" },
            { 2, "emLEFT_SHIFT" },
            { 3, "emSHIFT" },
            { 4, "emCTRL" },
            { 8, "emALT" },
        };

        static Dictionary<int, string> EventMessages = new Dictionary<int, string>
        {
            //{ 0x08, "KEY_BACK" },
            { 0x09, "KEY_TAB" },
            { 0xF00, "KEY_SHIFTTAB" },

            { 0x0C, "KEY_CLEAR" },
            { 0x0D, "KEY_RETURN" },

            { 0x10, "KEY_SHIFT" },
            { 0x11, "KEY_CONTROL" },
            { 0x12, "KEY_MENU" },
            { 0x13, "KEY_PAUSE" },
            { 0x14, "KEY_CAPITAL" },

            { 0x1B, "KEY_ESCAPE" },
            //{ 0x1B, "KEY_ESC" },

            { 0x20, "KEY_SPACE" },
            //{ 0x21, "KEY_PRIOR" }, // really !
            //{ 0x22, "KEY_NEXT" },  // really "
            { 0x4F00, "KEY_END" },
            { 0x4700, "KEY_HOME" },
            { 0x4B00, "KEY_LEFT" },
            { 0x4800, "KEY_UP" },
            { 0x4D00, "KEY_RIGHT" },
            { 0x5000, "KEY_DOWN" },

            // Fans got these backwards, so I don't convert them
            // to wrong enum, instead I annotate with correct text.
            // correct definitions: PAGEDOWN $5100, PAGEUP $4900
            //{ 0x4900, "KEY_PAGEDOWN" },
            //{ 0x5100, "KEY_PAGEUP" },

            { 0x25, "KEY_PERCENT" },
            // { 0x29, "KEY_SELECT" }, really (
            //{ 0x2A, "KEY_PRINT" },
            //{ 0x2B, "KEY_EXECUTE" },
            { 0x2C, "KEY_SNAPSHOT" },
            { 0x5200, "KEY_INSERT" },
            { 0x5300, "KEY_DELETE" },
            //{ 0x2F, "KEY_HELP" },

            { 0x30, "KEY_0" },
            { 0x31, "KEY_1" },
            { 0x32, "KEY_2" },
            { 0x33, "KEY_3" },
            { 0x34, "KEY_4" },
            { 0x35, "KEY_5" },
            { 0x36, "KEY_6" },
            { 0x37, "KEY_7" },
            { 0x38, "KEY_8" },
            { 0x39, "KEY_9" },
            { 0x3F, "KEY_QUESTION" },
            { 0x41, "KEY_A" },
            { 0x42, "KEY_B" },
            { 0x43, "KEY_C" },
            { 0x44, "KEY_D" },
            { 0x45, "KEY_E" },
            { 0x46, "KEY_F" },
            { 0x47, "KEY_G" },
            { 0x48, "KEY_H" },
            { 0x49, "KEY_I" },
            { 0x4A, "KEY_J" },
            { 0x4B, "KEY_K" },
            { 0x4C, "KEY_L" },
            { 0x4D, "KEY_M" },
            { 0x4E, "KEY_N" },
            { 0x4F, "KEY_O" },
            { 0x50, "KEY_P" },
            { 0x51, "KEY_Q" },
            { 0x52, "KEY_R" },
            { 0x53, "KEY_S" },
            { 0x54, "KEY_T" },
            { 0x55, "KEY_U" },
            { 0x56, "KEY_V" },
            { 0x57, "KEY_W" },
            { 0x58, "KEY_X" },
            { 0x59, "KEY_Y" },
            { 0x5A, "KEY_Z" },

            { 0x61, "KEY_a" },
            { 0x62, "KEY_b" },
            { 0x63, "KEY_c" },
            { 0x64, "KEY_d" },
            { 0x65, "KEY_e" },
            { 0x66, "KEY_f" },
            { 0x67, "KEY_g" },
            { 0x68, "KEY_h" },
            { 0x69, "KEY_i" },
            { 0x6A, "KEY_j" },
            { 0x6B, "KEY_k" },
            { 0x6C, "KEY_l" },
            { 0x6D, "KEY_m" },
            { 0x6E, "KEY_n" },
            { 0x6F, "KEY_o" },
            { 0x70, "KEY_p" },
            { 0x71, "KEY_q" },
            { 0x72, "KEY_r" },
            { 0x73, "KEY_s" },
            { 0x74, "KEY_t" },
            { 0x75, "KEY_u" },
            { 0x76, "KEY_v" },
            { 0x77, "KEY_w" },
            { 0x78, "KEY_x" },
            { 0x79, "KEY_y" },
            { 0x7A, "KEY_z" },

            { 0x1000, "KEY_ALT_q" },
            { 0x1100, "KEY_ALT_w" },
            { 0x1200, "KEY_ALT_e" },
            { 0x1300, "KEY_ALT_r" },
            { 0x1400, "KEY_ALT_t" },
            { 0x1500, "KEY_ALT_y" },
            { 0x1600, "KEY_ALT_u" },
            { 0x1700, "KEY_ALT_i" },
            { 0x1800, "KEY_ALT_o" },
            { 0x1900, "KEY_ALT_p" },

            { 0x1E00, "KEY_ALT_a" },
            { 0x1F00, "KEY_ALT_s" },
            { 0x2000, "KEY_ALT_d" },
            { 0x2100, "KEY_ALT_f" },
            { 0x2200, "KEY_ALT_g" },
            { 0x2300, "KEY_ALT_h" },
            { 0x2400, "KEY_ALT_j" },
            { 0x2500, "KEY_ALT_k" },
            { 0x2600, "KEY_ALT_l" },

            { 0x2C00, "KEY_ALT_z" },
            { 0x2D00, "KEY_ALT_x" },
            { 0x2E00, "KEY_ALT_c" },
            { 0x2F00, "KEY_ALT_v" },
            { 0x3000, "KEY_ALT_b" },
            { 0x3100, "KEY_ALT_n" },
            { 0x3200, "KEY_ALT_m" },

            //{ 0x5200, "KEY_NUMPAD0" },
            //{ 0x4F00, "KEY_NUMPAD1" },
            //{ 0x5000, "KEY_NUMPAD2" },
            //{ 0x5100, "KEY_NUMPAD3" },
            //{ 0x4B00, "KEY_NUMPAD4" },
            { 0x4C00, "KEY_NUMPAD5" },
            //{ 0x4D00, "KEY_NUMPAD6" },
            //{ 0x4700, "KEY_NUMPAD7" },
            //{ 0x4800, "KEY_NUMPAD8" },
            //{ 0x4900, "KEY_NUMPAD9" },

            { 0x2A, "KEY_MULTIPLY" },
            { 0x2B, "KEY_ADD" },
            //{ 0x2F, "KEY_SEPARATOR" },
            { 0x2D, "KEY_SUBTRACT" },
            { 0x2E, "KEY_DECIMAL" },
            { 0x2F, "KEY_DIVIDE" },
            { 0x3B00, "KEY_F1" },
            { 0x3C00, "KEY_F2" },
            { 0x3D00, "KEY_F3" },
            { 0x3E00, "KEY_F4" },
            { 0x3F00, "KEY_F5" },
            { 0x4000, "KEY_F6" },
            { 0x4100, "KEY_F7" },
            { 0x4200, "KEY_F8" },
            { 0x4300, "KEY_F9" },
            { 0x4400, "KEY_F10" },

            { 0, "JOY_NULL" },
            { 1, "JOY_UP" },
            { 2, "JOY_UPRIGHT" },
            { 3, "JOY_RIGHT" },
            { 4, "JOY_DOWNRIGHT" },
            { 5, "JOY_DOWN" },
            { 6, "JOY_DOWNLEFT" },
            { 7, "JOY_LEFT" },
            { 8, "JOY_UPLEFT" },
        };

        static Dictionary<int,string> AnnotateEventMessages = new Dictionary<int, string>
        {
            // fan template defined these backwards, so i annotate instead
            { 0x4900, "PAGEUP" },
            { 0x5100, "PAGEDOWN" },

            { 0x21, "!" },
            { 0x22, "\"" },
            { 0x23, "#" },
            { 0x24, "$" },
            { 0x26, "&" },
            { 0x27, "'" },
            { 0x28, "(" },
            { 0x29, ")" },

            { 0x2c, "," },

            { 0x3a, ":" },
            { 0x3b, ";" },
            { 0x3c, "<" },
            { 0x3d, "=" },
            { 0x3e, ">" },
            { 0x40, "@" },

            { 0x5b, "[" },
            { 0x5c, "\\" },
            { 0x5d, "]" },
            { 0x5e, "^" },
            { 0x5f, "_" },
            { 0x60, "`" },

            { 0x7b, "{" },
            { 0x7c, "|" },
            { 0x7d, "}" },
            { 0x7e, "~" },
        };
    }
}
