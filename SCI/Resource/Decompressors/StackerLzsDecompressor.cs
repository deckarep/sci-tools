using System;

// Stacker LZS is used in PC versions of SCI2 and SCI3 for all resource types.
//
// This implementation is based off of ANSI Standard X3.241-1994:
// https://web.archive.org/web/20220301223828/http://masters.donntu.org/2003/fvti/boykov/library/lzs.pdf
//
// The bitstream contains three types of tokens that can appear in any order:
// - Raw byte token: 0 bit then 8 bits
// - String token: 1 bit then variable-bit offset (1-2047) and length (2+)
// - End marker: 9 bit pattern 1 1000 0000
//
// Each string token references a string of bytes to copy from the previous
// 2048 bytes of output. This is a sliding window compression scheme.

namespace SCI.Resource.Decompressors
{
    public static class StackerLzsDecompressor
    {
        public static byte[] Decompress(Span span, int uncompressedSize)
        {
            byte[] output = new byte[uncompressedSize];
            int outputPos = 0;

            var bitStream = new BitStream(span);
            while (outputPos < uncompressedSize && !bitStream.EOF)
            {
                if (bitStream.GetMSB(1) == 0)
                {
                    // raw byte token
                    output[outputPos++] = (byte)bitStream.GetMSB(8);
                }
                else
                {
                    // string token or end marker
                    int stringOffset;
                    if (bitStream.GetMSB(1) == 1)
                    {
                        // 7-bit string token offset: 1-127
                        stringOffset = (byte)bitStream.GetMSB(7);

                        // check if this is the end marker: 1 1000 0000.
                        // after this it's just zero padding bits.
                        if (stringOffset == 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        // 11-bit string token offset: 128-2047
                        stringOffset = (UInt16)bitStream.GetMSB(11);
                    }

                    int stringLength;
                    switch (bitStream.GetMSB(2))
                    {
                        case 0: stringLength = 2; break;
                        case 1: stringLength = 3; break;
                        case 2: stringLength = 4; break;
                        default:
                            switch (bitStream.GetMSB(2))
                            {
                                case 0: stringLength = 5; break;
                                case 1: stringLength = 6; break;
                                case 2: stringLength = 7; break;
                                default:
                                    {
                                        // a series of nibbles, all of which are 1111
                                        // until the last one, plus 8. does not count
                                        // the 1111 nibble we've already consumed.
                                        stringLength = 8;
                                        byte nibble;
                                        do
                                        {
                                            nibble = (byte)bitStream.GetMSB(4);
                                            stringLength += nibble;
                                        } while (nibble == 0x0f);
                                    }
                                    break;
                            }
                            break;
                    }

                    // write the string
                    int stringStartOffset = outputPos - stringOffset;
                    int stringEndOffset = stringStartOffset + stringLength;
                    for (int i = stringStartOffset; i < stringEndOffset; i++)
                    {
                        output[outputPos++] = output[i];
                    }
                }
            }
            return output;
        }
    }
}
