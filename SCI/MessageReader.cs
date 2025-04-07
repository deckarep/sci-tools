using System;
using System.Collections.Generic;
using System.Text;
using SCI.Resource;

// MessageReader parses all of a game's messages into a dictionary with the
// resource number as the key and a list of its messages as the value.
//
// It autodetects message version and endianness *but* it does need to be told
// if it's SCI32 because SCI32 Mac messages have a slightly different format.
// It would be nice to autodetect that too.

namespace SCI
{
    public class MessageReader
    {
        // sci32 parameter only matters if messages are mac
        public static Dictionary<int, List<Message>> ReadMessages(string directory, bool sci32 = false)
        {
            return ReadMessages(new ResourceManager(directory), sci32);
        }

        // sci32 parameter only matters if messages are mac
        public static Dictionary<int, List<Message>> ReadMessages(ResourceManager resourceManager, bool sci32 = false)
        {
            var messages = new Dictionary<int, List<Message>>();
            var resources = resourceManager.GetResources(ResourceType.Message);
            foreach (var resource in resources)
            {
                int modNum = resource.Number;
                var messageData = resourceManager.GetResource(resource);

                // detect version and endianess of the message resource
                int version;
                bool littleEndian;
                if (!DetectVersion(messageData, out version, out littleEndian))
                {
                    throw new Exception("unable to detect message version: " + resource);
                }
                messageData.Endian = littleEndian ? Endian.Little : Endian.Big;

                if (version == 4 || version == 5)
                {
                    messages[modNum] = ReadMessagesV4(modNum, messageData, sci32);
                }
                else if (version == 3)
                {
                    messages[modNum] = ReadMessagesV3(modNum, messageData);
                }
                else if (version == 2)
                {
                    messages[modNum] = ReadMessagesV2(modNum, messageData);
                }
                else
                {
                    throw new Exception("unknown message version: " + version + " in resource: " + resource);
                }
            }
            return messages;
        }

        static bool DetectVersion(Span data, out int version, out bool littleEndian)
        {
            // first field is the version number multiplied by 1000 in platform endian format.
            // valid versions are 2, 3, 4, and 5.
            UInt32 versionLE = data.GetUInt32LE(0);
            UInt32 versionBE = data.GetUInt32BE(0);
            foreach (var isLittleEndian in new [] { true, false} )
            {
                UInt32 rawVersion = isLittleEndian ? versionLE : versionBE;
                if (2000 <= rawVersion && rawVersion <= 5999)
                {
                    littleEndian = isLittleEndian;
                    version = (int)(rawVersion / 1000);
                    return true;
                }
            }

            version = 0;
            littleEndian = true;
            return false;
        }

        public static List<Message> ReadMessagesV4(int modNum, Span data, bool sci32)
        {
            // resource format:
            // 10 byte header. last 2 are message count.
            // 11 byte message records. contain offsets to strings
            // ...
            // message strings, null terminated.

            bool isSci32Mac = (data.Endian == Endian.Big) && sci32;

            int messageCount = data.GetUInt16(8);
            var messages = new List<Message>(messageCount);
            var text = new StringBuilder();
            for (int i = 0; i < messageCount; i++)
            {
                int pos = 10 + (i * (isSci32Mac ? 12: 11)); // skip first 10 bytes

                var message = new Message();
                message.ModNum = modNum;
                message.Noun = data[pos + 0];
                message.Verb = data[pos + 1];
                message.Cond = data[pos + 2];
                message.Seq  = data[pos + 3];
                message.Talker = data[pos + 4];

                // sci32 mac adds a byte between talker and string
                if (isSci32Mac)
                {
                    pos += 1;
                }

                UInt16 stringOffset = data.GetUInt16(pos + 5);
                text.Length = 0;
                for (UInt16 c = stringOffset; data[c] != 0; c++)
                {
                    text.Append((char)data[c]);
                }
                message.Text = text.ToString();

                message.RefNoun = data[pos + 7];
                message.RefVerb = data[pos + 8];
                message.RefCond = data[pos + 9];

                messages.Add(message);
            }
            return messages;
        }

        public static List<Message> ReadMessagesV3(int modNum, Span data)
        {
            // resource format:
            // 8 byte header. last 2 are message count.
            // 10 byte message records. contain offsets to strings
            // ...
            // message strings, null terminated.

            int messageCount = data.GetUInt16(6);
            var messages = new List<Message>(messageCount);
            var text = new StringBuilder();
            for (int i = 0; i < messageCount; i++)
            {
                int pos = 8 + (i * 10);

                var message = new Message();
                message.ModNum = modNum;
                message.Noun = data[pos + 0];
                message.Verb = data[pos + 1];
                message.Cond = data[pos + 2];
                message.Seq = data[pos + 3];
                message.Talker = data[pos + 4];

                UInt16 stringOffset = data.GetUInt16(pos + 5);
                text.Length = 0;
                for (UInt16 c = stringOffset; data[c] != 0; c++)
                {
                    text.Append((char)data[c]);
                }
                message.Text = text.ToString();

                messages.Add(message);
            }
            return messages;
        }

        // eco1 floppy 1.0
        static List<Message> ReadMessagesV2(int modNum, Span data)
        {
            // six byte header. last 2 are message count
            int messageCount = data.GetUInt16(4);
            var messages = new List<Message>(messageCount);
            var text = new StringBuilder();
            for (int i = 0; i < messageCount; i++)
            {
                int pos = 6 + (i * 4); // skip first 6 bytes

                var message = new Message();
                message.ModNum = modNum;
                message.Noun = data[pos + 0];
                message.Verb = data[pos + 1];
                message.Seq = 1; // there is no seq, but MessageFinder expects 1 as default

                UInt16 stringOffset = data.GetUInt16(pos + 2);
                text.Length = 0;
                for (UInt16 c = stringOffset; data[c] != 0; c++)
                {
                    text.Append((char)data[c]);
                }
                message.Text = text.ToString();

                messages.Add(message);
            }
            return messages;
        }
    }
}
