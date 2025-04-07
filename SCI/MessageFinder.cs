using System.Collections.Generic;
using System.Linq;

namespace SCI
{
    public class MessageFinder
    {
        string game;
        Dictionary<int, List<Message>> scriptMessages;
        public IReadOnlyDictionary<int, List<Message>> Messages { get { return scriptMessages; } }

        public MessageFinder(string game, Dictionary<int, List<Message>> scriptMessages)
        {
            this.game = game;
            this.scriptMessages = scriptMessages;
        }

        // currentScript is (was?) only used for logging, long ago the old annotators used it for more
        public Message GetFirstMessage(int currentScript, int script, int noun, int verb, int cond, int seq = 1, bool logFailures = true)
        {
            var message = GetFirstMessageNonRecursive(currentScript, script, noun, verb, cond, seq, logFailures);
            if (message != null)
            {
                message = GetRefMessage(message);
            }
            return message;
        }

        public Message GetFirstMessageNonRecursive(int currentScript, int script, int noun, int verb, int cond, int seq = 1, bool logFailures = true)
        {
            List<Message> messages;
            if (!scriptMessages.TryGetValue(script, out messages))
            {
                if (logFailures)
                {
                    Log.Debug(game, currentScript + ": No messages for script " + script);
                }
                return null;
            }

            // look up the message normally. if it doesn't exist and we were trying cond 1 then try cond 0.
            var message = messages.FirstOrDefault(m => m.ModNum == script && m.Noun == noun && m.Verb == verb && m.Cond == cond && m.Seq == seq);
            if (message == null && cond == 1)
            {
                message = messages.FirstOrDefault(m => m.ModNum == script && m.Noun == noun && m.Verb == verb && m.Cond == 0 && m.Seq == seq);
            }
            if (message == null)
            {
                if (logFailures)
                {
                    Log.Debug(game, currentScript + ": message not found: " + script + " " + noun + " " + verb + " " + cond + " " + seq);
                }
            }
            return message;
        }

        Message GetRefMessage(Message message)
        {
            List<Message> messages;
            if (!scriptMessages.TryGetValue(message.ModNum, out messages)) return null;

            // recursively ref!
            while (message.IsRef)
            {
                var refMessage = messages.FirstOrDefault(m => m.Noun == message.RefNoun && m.Verb == message.RefVerb && m.Cond == message.RefCond && m.Seq == 1);
                if (refMessage == null)
                {
                    // this should never happen; it would mean a broken ref
                    Log.Warn(game, "ref message not found: " + message.ModNum + " " + message.RefNoun + " " + message.RefVerb + " " + message.RefCond + " 1");
                    return null;
                }
                message = refMessage;
            }

            return message;
        }

        public Message BruteForce(int noun, int cond, int verb, int seq)
        {
            Message foundMessage = null;
            foreach (var scriptNumber in scriptMessages.Keys)
            {
                var message = GetFirstMessage(scriptNumber, scriptNumber, noun, verb, cond, seq, false);
                if (message != null)
                {
                    if (foundMessage == null)
                    {
                        foundMessage = message;
                    }
                    else
                    {
                        // 2nd message found, it's ambiguous.
                        // but maybe they have the same text, in which
                        // case treat them as the same.
                        if (foundMessage.Text != message.Text)
                        {
                            return null;
                        }
                    }
                }
            }
            return foundMessage;
        }

        public List<Message> GetMessagesByModNum(int modNum)
        {
            List<Message> messages;
            if (scriptMessages.TryGetValue(modNum, out messages))
            {
                return messages;
            }
            return null;
        }
    }
}
