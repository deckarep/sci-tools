using System.Collections.Generic;
using System.Linq;

namespace SCI
{
    public class TextMessageFinder
    {
        string game;
        Dictionary<int, List<TextMessage>> textMessages;

        public TextMessageFinder(string game, Dictionary<int, List<TextMessage>> textMessages)
        {
            this.game = game;
            this.textMessages = textMessages;
        }

        public TextMessage GetMessage(int modNum, int number)
        {
            List<TextMessage> messages;
            if (!textMessages.TryGetValue(modNum, out messages))
            {
                Log.Warn(game, "No text resource: " + modNum);
                return null;
            }

            var message = messages.FirstOrDefault(m => m.ModNum == modNum && m.Number == number);
            if (message == null)
            {
                Log.Warn(game, "text message not found: " + modNum + " " + number);
            }
            return message;
        }
    }
}
