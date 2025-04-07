using System;
using System.Collections.Generic;
using System.Text;

namespace SCI.Resource
{
    public static class SaidParser
    {
        static string[] SaidOperators =
        {
            ",", // 0xf0
            "&", // 0xf1
            "/", // 0xf2
            "(", // 0xf3
            ")", // 0xf4
            "[", // 0xf5
            "]", // 0xf6
            "#", // 0xf7
            "<", // 0xf8
            ">", // 0xf9
        };

        public static string Parse(SpanStream stream, Vocab vocab)
        {
            var text = new StringBuilder();
            byte b;
            while ((b = stream.ReadByte()) != 0xff)
            {
                if (b >= 0xf0)
                {
                    // byte operator
                    text.Append(SaidOperators[b - 0xf0]);
                }
                else
                {
                    // word group
                    UInt16 group = (UInt16)(b << 8 | stream.ReadByte());
                    string word = vocab.GetPreferredWord(group);
                    text.Append(word);
                }
            }
            return text.ToString();
        }
    }
}
