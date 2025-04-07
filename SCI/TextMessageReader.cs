using System.Collections.Generic;
using System.Text;
using SCI.Resource;

// TextMessageReader parses all of a game's messages from Text resources
// into a dictionary with the resource number as the key and a list of
// messages as the value.
//
// This resource format is simple: just a series of null terminated strings.

namespace SCI
{
    public class TextMessageReader
    {
        public static Dictionary<int, List<TextMessage>> ReadMessages(string messageDirectory)
        {
            return ReadMessages(new ResourceManager(messageDirectory));
        }

        public static Dictionary<int, List<TextMessage>> ReadMessages(ResourceManager resourceManager)
        {
            var messages = new Dictionary<int, List<TextMessage>>();
            foreach (var textResource in resourceManager.GetResources(ResourceType.Text))
            {
                var span = resourceManager.GetResource(textResource);
                messages[textResource.Number] = ReadMessages(resourceManager.Name, textResource.Number, span);
            }
            return messages;
        }

        public static List<TextMessage> ReadMessages(string game, int modNum, Span messageData)
        {
            // file format:
            // null terminated strings
            var messages = new List<TextMessage>();
            var currentString = new StringBuilder();
            foreach (var b in messageData)
            {
                if (b == 0)
                {
                    var message = new TextMessage(modNum, messages.Count, currentString.ToString());
                    messages.Add(message);
                    currentString.Length = 0;
                }
                else
                {
                    currentString.Append((char)b);
                }
            }
            if (currentString.Length > 0)
            {
                // seen in fan game SCI Tetris
                Log.Warn(game, "Text " + modNum + ": unterminated string: " + currentString);
            }
            return messages;
        }
    }
}
