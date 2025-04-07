using System.Collections.Generic;
using System.Linq;
using SCI.Language;
using SCI.Annotators.Visitors;

namespace SCI.Annotators
{
    // SayAnnotator is the crown jewel of the annotators. Replaced MessageAnnotator.
    //
    // For each function:
    //   evaluate each code node recursively, looking for messager say/sayRange calls.
    //   while descending, identify statements that identify symbols as integers,
    //   and maintain stacks of these so that they are known at any given node.
    //
    //   when a say is found, resolve any symbol parameters using state.
    //   resolution will include symbols that are at top of stacks and
    //   object properties if the function is a method.
    //
    // Annotating each "say" requires figuring out all five message tuple components:
    //
    //    modNum    noun   verb   cond    seq
    //
    // Obstacles:
    //   - Many overloads to say, some of which are ambiguous to static analysis.
    //     They painted themselves into a corner. Messager:say has to dynamically
    //     query parameter 5 to see if it's a non-zero integer, otherwise it's assumed
    //     to be null or an object, and that determines which set of overloads are used.
    //   - Many symbols are passed to say: verb parameter from doVerb, noun and modNum
    //     properties, local variables. Properties are usually constant at runtime but
    //     sometimes they are updated.
    //   - All parameters except noun are optional.
    //   - modNum defaults to the current room number and many non-room scripts don't
    //     pass modNum because they only run within one room, or each room has its own
    //     message, or the code is in a base class.
    //   - A lot of say: calls are buggy or unused and there is no message.
    //
    // Solutions:
    //   - Resolve symbols to integers using comparison scope of code and object properties.
    //   - Evaluate if/switch/cond statements while descending each function's syntax
    //     tree and identify all symbols whose literal values are known within a scope.
    //   - Use Sierra's Messager:say parameter determining algorithm up to a point, and
    //     then apply aggressive heuristics to determine which overload we're dealing with.
    //   - Identify room scripts and use their script number if modNum isn't passed within.
    //   - Analyze script dependencies to figure out if a script is only used by one room,
    //     even if by a chain of scripts, so that the room number can be used when modNum
    //     isn't passed by the script.
    //   - If modNum can't be unambiguously determined but there is only one tuple in the
    //     game with the noun/verb/cond/seq combo, use that. If there are multiple but
    //     they have the same text then use the first.
    //   - If modNum can't be unambiguously determined and there are multiple messages
    //     that match the rest of the tuple and one of them matches the script number,
    //     use that message. "Throw A Hail Msg"
    //   - Annotate all "says" that have been unambiguously resolved (by static standards)
    //     but don't have messages with "MISSING MESSAGE". In the past this generated
    //     many false positives, now it's very good, and everything is a bug or unreachable
    //     code or an edge case where the correct values are set at runtime.
    //
    // Conversations:
    //
    // The Conversation class appears around KQ6 and LB2, I have original source for it,
    // and it's much easier to annotate but it does rely on flow analysis to get the
    // state of variables and current room number so I'm adding it here. This is a good
    // example of how it would be nice to factor out all the code that analyzes what
    // variables are what values into a separate module that Say and Conversation could use.
    static class SayAnnotator
    {
        class State
        {
            public DeepBeige DeepBeige;

            public Game Game;
            public string Messager;
            public MessageFinder MessageFinder;
            public List<Script> RoomScripts;
            public IReadOnlyDictionary<int, int> DetectedCallerScripts;
            public HashSet<Object> Conversations;
            public HashSet<Object> Prints;
            public IReadOnlyDictionary<string, int> SayProcs;

            public Script Script { get { return DeepBeige.Function.Script; } }
            public Object Object { get { return DeepBeige.Function.Object; } }
        }

        // setup state and recursively Visit() every node in every procedure and method
        public static void Run(Game game, MessageFinder messageFinder, IReadOnlyDictionary<string, int> sayProcs = null)
        {
            var state = new State();
            state.Game = game;
            state.Messager = game.GetGlobal(91).Name;
            state.MessageFinder = messageFinder;
            state.RoomScripts = game.GetRooms().Keys.ToList();
            state.DetectedCallerScripts = game.GetUniqueRoomCallers(state.RoomScripts);
            state.Conversations = game.GetObjectsBySuper("Conversation");
            state.Prints = game.GetObjectsBySuper("Print"); // not the Print function, the Print class
            state.SayProcs = sayProcs;

            state.DeepBeige = new DeepBeige();
            state.DeepBeige.OnNode = (Node node) =>
            {
                Visit(node, state);
            };
            state.DeepBeige.Visit(game);
        }

        static void Visit(Node node, State state)
        {
            // (gMessager ... say: ....
            // gk1 also uses sayRange: with same params
            if ((node.Text == "say:" || node.Text == "sayRange:") &&
                node.Parent.At(0).Text == state.Messager)
            {
                AnnotateSay(node, state);
                return;
            }

            // (Print ... addText: or addButton: ...
            if ((node.Text == "addText:" ||
                 node.Text == "addButton:" ||
                 node.Text == "addColorButton:" ||
                 node.Text == "addButtonBM:" ||
                 node.Text == "say:") &&
                IsObjectInCollection(node.Parent.At(0), state.Prints, state))
            {
                AnnotatePrintClass(node, state);
            }

            // (aConversation ... add: modNum noun verb cond seq ...
            if (node.Text == "add:" &&
                IsObjectInCollection(node.Parent.At(0), state.Conversations, state))
            {
                AnnotateConversationAdd(node, state);
                return;
            }

            // (aConversation ... load: @aLocal ...
            if (node.Text == "load:" &&
                IsObjectInCollection(node.Parent.At(0), state.Conversations, state))
            {
                AnnotateConversationLoad(node, state);
                return;
            }

            // (SayProc [other-params] say-style parameters)
            // kind of a hack because only brain2 has one of these,
            // with one extra parameter before the say-styles ones.
            if (state.SayProcs != null &&
                state.SayProcs.ContainsKey(node.At(0).Text))
            {
                int extraParamCount = state.SayProcs[node.At(0).Text];
                if (node.Children.Count >= 1 + extraParamCount + 1)
                {
                    AnnotateSay(node.At(extraParamCount), state);
                }
            }
        }

        // node is "say" selector and parent is confirmed as messager
        static void AnnotateSay(Node node, State state)
        {
            node.Annotate(""); // flush all existing ones in source for testing

            // count parameters. (inefficient because of next)
            int parameterCount = 0;
            Node paramNode = node.Next();
            while (!(paramNode is Nil || paramNode.IsSelector()))
            {
                parameterCount += 1;
                paramNode = paramNode.Next();
            }
            if (parameterCount == 0) return;

            /* from sierra source:
              ; Set up the parameters for saying a message & get a talker to
              ;  say them.  Parameters take the following forms:
              ;
              ;     a) noun [verb [case [sequence [caller [module]]]]]
              ;     b) noun [verb [case [fromSequence toSequence [caller [module]]]]]
              ;     c) NEXT [caller]
              ;
              ; If two sequence numbers are specified (form b), they comprise a range.
              ;  In this case, lastSequence will be set to the upper (last) sequence
              ;  number.  If there is no range, this property will be NULL.
           */

            // now to replicate the Messager logic. here's the problem, when they added
            // the second overload that can accept from/to sequences they created ambiguity
            // between parameter 5 and 6. they resolve that by testing if it's an object.
            // i can't do that with static analysis! but i can test if something is a string
            // or not, because at this point everything needs to be an integer constant except
            // caller. for what it's worth, caller is usually "self".

            // noun is mandatory
            var nounNode = node.Next(1);
            int noun = ResolveNode(nounNode, state);
            if (noun == -1)
            {
                //node.Annotate("NO NOUN");
                return;
            }

            // verb is optional
            int verb = 0;
            if (parameterCount > 1)
            {
                var verbNode = node.Next(2);
                verb = ResolveNode(verbNode, state);
                if (verb == -1)
                {
                    //node.Annotate("NO VERB");
                    return;
                }
            }

            // cond is optional
            int cond = 0;
            if (parameterCount > 2)
            {
                var condNode = node.Next(3);
                cond = ResolveNode(condNode, state);
                if (cond == -1)
                {
                    //node.Annotate("NO COND");
                    return;
                }
            }

            // now it starts getting tricky but at least we know this next one is a sequence.
            // it could be a single, a start of a range, or a 0 indicating "play 'em all"
            int seq = 1;
            if (parameterCount > 3)
            {
                var seqNode = node.Next(4);
                seq = ResolveNode(seqNode, state);
                if (seq == -1)
                {
                    //node.Annotate("NO SEQ");
                    return;
                }

                // Sierra:
                // If we get a sequence number other than 0, then we want to say
                // either that one message only or a range whose upper limit will be
                // the next parameter.A sequence number of 0 indicates that we
                // want to say all sequences, so set it to 1.

                // nothing for me to do but set the sequence to 1 because i always
                // want the first message and there are no 0 sequences.
                if (seq == 0)
                {
                    seq = 1;
                }
            }

            // Sierra:
            // If the next arg is not 0 and not an object, then it is the upper end
            // of a sequence range.  In this case, be sure to set oneOnly FALSE.
            int paramIndex = 4; // zero-based
            if (parameterCount > paramIndex)
            {
                // this is the hardest part, identifying if this is an overload with
                // lastSeq or one without. i don't use lastSeq for anything, so
                // any heuristic that breaks the tie is a winner. options:
                // 5 params: n v c s [ $caller ]
                // 6 params: n v c s [ $caller ] modNum
                //  -------------------------------------
                // 5 params: n v c s lastSeq
                // 6 params: n v c s lastSeq $caller
                // 7 params: n v c s lastSeq $caller modNum
                bool isLastSeq = false;

                // try resolving this node to an in integer first.
                // if it can be resolved to a non-zero integer then
                // as sierra's comments say, it's a lastSeq.
                var maybeLastSeqNode = node.Next(5);
                int maybeLastSeq = ResolveNode(maybeLastSeqNode, state);
                var param6Node = node.Next(6);

                // unambiguous: 7 parameters means this is lastSeq
                if (parameterCount >= 7)
                {
                    isLastSeq = true;
                }
                // also unambiguous: is this param a non-zero int?
                else if (maybeLastSeq > 0)
                {
                    isLastSeq = true;
                }
                // gross fallback heuristic: "self" is the most common caller param.
                else if (param6Node.Text == "self")
                {
                    isLastSeq = true;
                }
                else if (maybeLastSeqNode.Text == "self")
                {
                    isLastSeq = false;
                }
                // if parameter 6 can be resolved to a non-zero int
                // then it is modNum, not caller,  and there is no lastSeq
                else if (parameterCount >= 6 &&
                         ResolveNode(param6Node, state) > 0)
                {
                    isLastSeq = false;
                }

                if (isLastSeq)
                {
                    paramIndex += 1; // skip lastSeq
                }
            }

            // Sierra:
            // See if we have a caller in parameter 4 (or 5 if sequence range)
            //
            // i'm not doing anything here because i don't care about the value
            // of the caller, i'm just skipping ahead, having already determined
            // if this is an "lastSeq" overload when inspecting the previous param
            paramIndex += 1; // skip caller

            // Sierra:
            // If there's a 5th (or 6th if sequence range) parameter, it'll be a
            // module number.  If not, make the module the curRoomNum
            int modNum;
            //bool modNumWasExplicit = false;
            if (parameterCount > paramIndex)
            {
                var modNumNode = node.Next(paramIndex + 1); // zero-based => one-based
                modNum = ResolveNode(modNumNode, state);
                if (modNum != -1)
                {
                    //modNumWasExplicit = true;
                }
            }
            else
            {
                modNum = GetCurrentRoomNumber(state);
            }

            Annotate(node, state, modNum, noun, verb, cond, seq);
        }

        // (Print ... addText: noun verb cond [ seq = 1 ] [ theX theY modNum = currentRoom ]
        // (Print ... addButton: same but with an id parameter first
        //
        // I'm also handling what looks to be only a KQ6CD/Spanish thing here, suck it.
        // KQ6Print derived from Print and has a say: method similar to the Print methods.
        static void AnnotatePrintClass(Node node, State state)
        {
            // figure out some stuff based on which method this is
            int baseParameter = 0;
            switch (node.Text)
            {
                case "addButton:":
                case "addColorButton:":
                case "say:":
                    baseParameter = 1;
                    break;
                case "addButtonBM:":
                    baseParameter = 4;
                    break;
            }

            // count parameters. (inefficient because of next)
            int parameterCount = 0;
            Node paramNode = node.Next();
            while (!(paramNode is Nil || paramNode.IsSelector()))
            {
                parameterCount += 1;
                paramNode = paramNode.Next();
            }
            if (parameterCount < (baseParameter + 1)) return;

            // noun
            var nounNode = node.Next(baseParameter + 1);
            int noun = ResolveNode(nounNode, state);
            if (noun == -1) return;

            // verb
            var verbNode = node.Next(baseParameter + 2);
            int verb = ResolveNode(verbNode, state);
            if (verb == -1) return;

            // cond
            var condNode = node.Next(baseParameter + 3);
            int cond = ResolveNode(condNode, state);
            if (cond == -1) return;

            // seq, default 1
            int seq = 1;
            if (parameterCount > (baseParameter + 3))
            {
                var seqNode = node.Next(baseParameter + 4);
                seq = ResolveNode(seqNode, state);
                if (seq == -1) return;
                if (seq == 0) seq = 1;
            }

            int modNum;
            //bool modNumWasExplicit = false;
            if (parameterCount > (baseParameter + 6))
            {
                var modNumNode = node.Next(baseParameter + 7);
                modNum = ResolveNode(modNumNode, state);
                if (modNum != -1)
                {
                    //modNumWasExplicit = true;
                }
            }
            else
            {
                modNum = GetCurrentRoomNumber(state);
            }

            Annotate(node, state, modNum, noun, verb, cond, seq);
        }

        // everything is known except maybe not modNum, in which case a brute force will be tried
        static void Annotate(Node node, State state, int modNum, int noun, int verb, int cond, int seq)
        {
            if (modNum == -1)
            {
                // no modNum parameter or an unresolvable one so lets see if there is only one modNum
                // that fulfills this tuple. if so, then use that message!
                var bruteForce = state.MessageFinder.BruteForce(noun, cond, verb, seq);
                if (bruteForce != null)
                {
                    node.Annotate(/*"BRUTEFORCE: " +*/ bruteForce.Text.QuoteMessageText());
                    return;
                }

                // multiple modNums. if the current script # works then lets try that,
                // but it's not a MISSING MESSAGE if it doesn't exist.
                var hailMessage = state.MessageFinder.GetFirstMessage(state.Script.Number, state.Script.Number, noun, verb, cond, seq, false);
                if (hailMessage != null)
                {
                    node.Annotate(/*"HAILMSG: " +*/ hailMessage.Text.QuoteMessageText());
                    return;
                }

                //node.Annotate("NO MODNUM AND CANT FIGURE IT OUT");
                return;
            }

            //
            // Tuple resolved, time to annotate
            //

            var message = state.MessageFinder.GetFirstMessage(state.Script.Number, modNum, noun, verb, cond, seq);
            if (message != null)
            {
                node.Annotate(message.Text.QuoteMessageText());
            }
            else
            {
                node.Annotate("MISSING MESSAGE");
            }
        }

        static int GetCurrentRoomNumber(State state)
        {
            int currentRoomNumber;
            if (state.RoomScripts.Contains(state.Script))
            {
                // current script isn't called by anybody but it is a room
                return state.Script.Number;

            }
            else if (state.DetectedCallerScripts.TryGetValue(state.Script.Number, out currentRoomNumber) &&
                     state.RoomScripts.Any(s => s.Number == currentRoomNumber))
            {
                // i've detected that this script has a unique caller which is also a room, so use that
                return currentRoomNumber;
            }

            // couldn't figure it out
            return -1;
        }

        // node is "add" selector and parent is confirmed as conversation
        static void AnnotateConversationAdd(Node node, State state)
        {
            // verify that all five parameters are here (there could be more)
            int index = node.Parent.Children.IndexOf(node);
            for (int i = 0; i < 5; ++i)
            {
                var paramNode = node.Parent.At(index + 1 + i);

                // if seq is missing that's okay, it will default to 1
                if (i == 4 && paramNode is Nil) break;

                if (paramNode.IsSelector() ||
                    !(paramNode is Integer || paramNode is Atom))
                {
                    return;
                }
            }

            // modNum
            var modNumNode = node.Parent.At(index + 1);
            int modNum = ResolveNode(modNumNode, state);

            // noun
            var nounNode = node.Parent.At(index + 2);
            int noun = ResolveNode(nounNode, state);
            if (noun == -1)
            {
                //node.Annotate("NO NOUN");
                return;
            }

            // verb
            var verbNode = node.Parent.At(index + 3);
            int verb = ResolveNode(verbNode, state);
            if (verb == -1)
            {
                //node.Annotate("NO VERB");
                return;
            }

            // cond
            var condNode = node.Parent.At(index + 4);
            int cond = ResolveNode(condNode, state);
            if (cond == -1)
            {
                //node.Annotate("NO COND");
                return;
            }

            // seq [ optional ]
            var seqNode = node.Parent.At(index + 5);
            int seq = (seqNode is Nil) ? 1 : ResolveNode(seqNode, state);
            if (seq == -1)
            {
                //node.Annotate("NO SEQ");
                return;
            }

            AnnotateConversationAddOrLoad(node, state, modNum, noun, verb, cond, seq);
        }

        // node is "load" selector and parent is confirmed as conversation
        static void AnnotateConversationLoad(Node node, State state)
        {
            // "load:" first (only?) parameter is address of a local which is an array
            // that contains the series of tuples for the conversation. i only care
            // about the first message. each entry starts with modNum noun verb cond seq
            var localAddressNode = node.Next();
            if (!(localAddressNode is AddressOf)) return; // shouldn't happen
            string localName = localAddressNode.At(0).Text;
            var local = state.Script.Locals.Values.FirstOrDefault(l => l.Name == localName);
            if (local == null) return; // shouldn't happen

            if (local.Count < 5)
            {
                node.Annotate("ARRAY IS TOO SMALL: " + local.Count);
                return;
            }

            int modNum = (int)local.Values[0];
            int noun = (int)local.Values[1];
            int verb = (int)local.Values[2];
            int cond = (int)local.Values[3];
            int seq = (int)local.Values[4];

            AnnotateConversationAddOrLoad(node, state, modNum, noun, verb, cond, seq);
        }

        // node is "add" or "load"
        // modNum may be -1 in which case need to figure out current room from script
        // seq may be 0 instead of 1
        static void AnnotateConversationAddOrLoad(Node node, State state, int modNum, int noun, int verb, int cond, int seq)
        {
            if (seq == 0)
            {
                seq = 1;
            }

            if (modNum == -1) // "-1" is probably what the script put, means "use current room"
            {
                if (state.RoomScripts.Contains(state.Script))
                {
                    // no modNum parameter but this script is a room, use that
                    modNum = state.Script.Number;

                }
                else if (state.DetectedCallerScripts.TryGetValue(state.Script.Number, out modNum) &&
                         state.RoomScripts.Any(s => s.Number == modNum))
                {
                    // no modNum parameter but i've detected that this script has a unique caller
                    // which is also a room, so use that
                }
                else
                {
                    modNum = -1; // because TryGetValue() might succeed by state.RoomScripts.Any() might not
                }

                if (modNum == -1)
                {
                    // no modNum parameter or an unresolvable one so lets see if there is only one modNum
                    // that fulfills this tuple. if so, then use that message.
                    var bruteForce = state.MessageFinder.BruteForce(noun, cond, verb, seq);
                    if (bruteForce != null)
                    {
                        node.Annotate(/*"BRUTEFORCE: " +*/ bruteForce.Text.QuoteMessageText());
                        return;
                    }

                    // multiple modNums. if the current script # works then lets try that,
                    // but it's not a MISSING MESSAGE if it doesn't exist.
                    var hailMessage = state.MessageFinder.GetFirstMessage(state.Script.Number, state.Script.Number, noun, verb, cond, seq, false);
                    if (hailMessage != null)
                    {
                        node.Annotate(/*"HAILMSG: " +*/ hailMessage.Text.QuoteMessageText());
                        return;
                    }

                    //node.Annotate("NO MODNUM AND CANT FIGURE IT OUT");
                    return;
                }
            }

            //
            // Tuple resolved, time to annotate
            //

            var message = state.MessageFinder.GetFirstMessage(state.Script.Number, modNum, noun, verb, cond, seq);
            if (message != null)
            {
                node.Annotate(message.Text.QuoteMessageText());
            }
            else
            {
                node.Annotate("MISSING MESSAGE");
            }
        }



        static int ResolveNode(Node node, State state)
        {
            // easy!
            if (node is Integer)
            {
                return node.Number;
            }

            // atoms only
            if (!(node is Atom)) return -1;
            string symbol = node.Text;

            // if the symbol can be resolved using the code state
            // then use that. for example, if an if statement
            // required "theVerb" to equal 4 then it's 4.
            int value = state.DeepBeige.Resolve(symbol);
            if (value != -1)
            {
                return value;
            }

            // if the symbol is an integer property of the'
            // current object then use that. this is not
            // guaranteed to be correct since it could
            // have been changed at runtime. still, this
            // is usually right.
            if (state.Object != null)
            {
                var property = state.Object.Properties.FirstOrDefault(p => p.Name == symbol);
                if (property != null && property.ValueNode is Integer)
                {
                    // special case: if this is a noun on a class then that's not a real
                    // value, it will get overridden, so return -1 (not found) so that
                    // this doesn't get annotated as MISSING MESSAGE.
                    if (/*property.Name == "noun" && */
                        property.ValueNode.Number == 0 &&
                        state.Object.Type == ObjectType.Class)
                    {
                        return -1;
                    }

                    return property.ValueNode.Number;
                }
            }

            return -1;
        }

        static bool IsObjectInCollection(Node node, ICollection<Object> objects, State state)
        {
            // if it's a class and name matches then Yes
            if (objects.Any(o => o.Type == ObjectType.Class &&
                                 o.Name == node.Text))
            {
                return true;
            }

            // if it's an instance in this script and name matches then Yes
            if (objects.Any(o => o.Type == ObjectType.Instance &&
                                 o.Script == state.Script &&
                                 o.Name == node.Text))
            {
                return true;
            }

            if (node.Children.Count == 3 &&
                node.At(0).Text == "ScriptID" &&
                node.At(1) is Integer &&
                node.At(2) is Integer)
            {
                int scriptNumber = node.At(1).Number;
                string export = state.Game.GetExport(scriptNumber, node.At(2).Number);
                if (export != null)
                {
                    return state.Game.GetScript(scriptNumber).Objects.Any(o => o.Name == export &&
                                                                               objects.Contains(o));
                }
            }

            return false;
        }
    }
}
