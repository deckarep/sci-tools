using System;
using System.Collections.Generic;
using System.Linq;
using SCI.Language;

namespace SCI.Annotators
{
    // this is a great annotator that i didn't document at all in the first version
    // so i'm trying to make up for it as i port it to the real parser.
    //
    // GK2 question/answer uses "topics". A room where questions appear at the bottom
    // is a room of class TopicRoom. TopicRoom scripts have instances of the Topic
    // class, each one is an option that can appear when questioning.
    //
    // A topic's text can be found by the TopicRoom:modnum and Topic:noun, the rest
    // of the tuple is always (v: 3 c: 0 s: 1). Topic:flagNum is the flag that's
    // set when asking, and I believe prevents the question from appearing again.
    // If a topic only appears once a condition is met, Topic:readyFlagNum is set
    // to the flag that must be set for it to appear. This is an optional property.
    //
    // This annotator...
    // 1. Annotates all Topic declarations with their text.
    // 2. Annotates Topic:readyFlagNum with the text of the Topic whose flagNum matches
    // 3. Annotates IsFlag calls with the text of the topic whose flagNum is being tested
    // 4. Annotates SetFlag/ClearFlag calls with the text of the topics
    // *. Annotations include the source script along with the topic text if the
    //    topic is from a different script.
    //
    // Example:
    // (instance tMissingPersons1 of Topic; "Missing Persons"
    //    (properties
    //       sceneNum 249
    //	     flagNum 245
    //	     readyFlagNum 218 ; "The Black Wolf" in rm3210
    //       noun 19

    static class Gk2TopicAnnotator
    {
        public static void Run(Game game, MessageFinder messageFinder)
        {
            // build a list of all topics in the game
            var topics = GetTopics(game, messageFinder);
            if (topics.Count == 0) return; // portuguese, no selectors for topics

            // annotate Topic instances
            foreach (var script in game.Scripts)
            {
                foreach (var topicInstance in script.Instances.Where(i => i.Super == "Topic"))
                {
                    // annotate topic declaration
                    var topic = topics.First(t => t.ScriptNumber == script.Number &&
                                                  t.Name == topicInstance.Name);
                    topicInstance.Node.Annotate(topic.Text.QuoteMessageText());

                    // annotate readyFlagNum
                    var readyFlagNumProperty = topicInstance.Properties.FirstOrDefault(p => p.Name == "readyFlagNum");
                    if (readyFlagNumProperty != null)
                    {
                        var otherTopics = topics.Where(t => t.FlagNumber == readyFlagNumProperty.ValueNode.Number);
                        foreach (var otherTopic in otherTopics)
                        {
                            readyFlagNumProperty.ValueNode.Annotate(FormatTopic(otherTopic, script.Number));
                        }
                    }
                }
            }

            // annotate flag functions
            string isFlag = game.GetExport(11, 0);
            string setFlag = game.GetExport(11, 1);
            string clearFlag = game.GetExport(11, 2);
            foreach (var script in game.Scripts)
            {
                foreach (var node in script.Root)
                {
                    // IsFlag
                    if (node.At(0).Text == isFlag &&
                        node.At(1) is Integer)
                    {
                        foreach (var topic in topics.Where(t => t.FlagNumber == node.At(1).Number))
                        {
                            node.At(1).Annotate(FormatTopic(topic, script.Number));
                        }
                    }

                    // SetFlag / ClearFlag
                    else if ((node.At(0).Text == setFlag ||
                              node.At(0).Text == clearFlag) &&
                             node.At(1) is Integer)
                    {
                        string action = (node.At(0).Text == setFlag) ? "enable " : "disable ";
                        foreach (var topic in topics.Where(t => t.ReadyFlagNumber == node.At(1).Number))
                        {
                            node.At(1).Annotate(action + FormatTopic(topic, script.Number));
                        }
                    }
                }
            }
        }

        static List<Topic> GetTopics(Game game, MessageFinder messageFinder)
        {
            var topics = new List<Topic>();
            foreach (var script in game.Scripts)
            {
                var topicRoom = script.Instances.FirstOrDefault(i => i.Super == "TopicRoom");
                if (topicRoom == null) continue;

                // all TopicRooms have a modNum property
                int modNum = topicRoom.Properties.First(p => p.Name == "modNum").ValueNode.Number;

                // add each Topic in the script to the list
                foreach (var topicInstance in script.Instances.Where(i => i.Super == "Topic"))
                {
                    var topic = new Topic();
                    topic.ScriptNumber = script.Number;
                    topic.TopicRoomName = topicRoom.Name;
                    topic.Name = topicInstance.Name;

                    // Topic:noun is always set
                    int noun = topicInstance.Properties.First(p => p.Name == "noun").ValueNode.Number;
                    var message = messageFinder.GetFirstMessage(script.Number, modNum, noun, 3, 0, 1, false);
                    if (message != null)
                    {
                        topic.Text = message.Text;
                    }
                    else
                    {
                        Log.Debug(game, script + ": no topic text for: " + topicInstance);
                        continue;
                    }

                    // Topic:flagNum is always set,
                    // but portuguese does BAD_SELECTOR, so give up
                    var flagNumProperty = topicInstance.Properties.FirstOrDefault(p => p.Name == "flagNum");
                    if (flagNumProperty == null) return new List<Topic>();
                    topic.FlagNumber = flagNumProperty.ValueNode.Number;


                    // Topic:readyFlagNum is optional
                    var readyFlagNumProperty = topicInstance.Properties.FirstOrDefault(p => p.Name == "readyFlagNum");
                    if (readyFlagNumProperty != null && readyFlagNumProperty.ValueNode is Integer)
                    {
                        topic.ReadyFlagNumber = readyFlagNumProperty.ValueNode.Number;
                    }

                    topics.Add(topic);
                }
            }
            return topics;
        }

        static string FormatTopic(Topic topic, int currentScriptNumber)
        {
            string text = topic.Text.QuoteMessageText();
            if (topic.ScriptNumber != currentScriptNumber)
            {
                text += " in " + topic.TopicRoomName;
            }
            return text;
        }

        class Topic
        {
            public int ScriptNumber;
            public string TopicRoomName;
            public string Name;
            public string Text;
            public int FlagNumber;
            public int ReadyFlagNumber;

            public override string ToString()
            {
                return Name + (string.IsNullOrWhiteSpace(Text) ? "" : (" - " + Text));
            }
        }
    }
}
