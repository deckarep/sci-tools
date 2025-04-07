using System;

// LZW compression is the primary compression used in SCI0 and SCI1.
//
// The bitstream is a series of variable bit codes, from 9 bits to 12:
// 
// 0-255  Literal byte to write to output (a one-byte string)
// 256    Resets the decoder
// 257    Terminates decompression
// 258+   Index into the LZW table of the string to write
//
// When processing a code (except Reset or Terminate), the decompressor:
// 1. Writes a string to the output.
// 2. Adds a new string to the end of the LZW table comprised of the string
//    that was just written, plus the subsequent byte that will appear in
//    the output, even though we don't know yet know what that byte will be.
//    * If the LZW table is full, no new strings are added until it is reset.
//
// The LZW table contains strings defined by offsets and lengths in the output
// array. We don't know the last byte of a string when we add it to the table,
// but we know where the string starts, and we know its length (one plus the
// length of the string we just wrote), so by the time we have to copy that
// unknown byte to the end of the output, it will exist in its source location.
//
// Codes in the bitstream are initially 9 bits. As the LZW table size exceeds
// each bit limit, the code size grows by one bit until reaching 12. The reset
// code resets the code size back to 9.
//
// There are two LZW formats. The first is used in early-to-mid SCI0 and AGIv3.
// The second is used in late SCI0 and SCI1. There are only two differences:
//
// 1. The bit order switched from LSB-first to MSB-first.
// 2. Code size increases one code earlier in the second LZW than in the first.
//    This is a bug in the second LZW, because the code size increases one code
//    earlier than necessary. It's only a small waste of bits, but it's such a
//    common bug in LZW implementations that it has a name: "Early Change".
//    PDF files have an "early change" header flag for writers to set if they
//    have this bug so that PDF readers can decode LZW both ways.

namespace SCI.Resource.Decompressors
{
    public static class LzwDecompressor
    {
        public static byte[] Decompress(CompressionFormat format, Span span, int uncompressedSize)
        {
            byte[] output = new byte[uncompressedSize];
            int outputPos = 0;

            int codeBitLength = 9;
            int tableSize = 258;
            int codeLimit = (format == CompressionFormat.SCI0) ? 512 : 511;

            UInt16[] stringOffsets = new UInt16[4096]; // 0-257: unused
            UInt16[] stringLengths = new UInt16[4096]; // 0-257: unused

            var bitStream = new BitStream(span);
            while (outputPos < uncompressedSize && !bitStream.EOF)
            {
                // Read the next code, 9-12 bits long
                UInt16 code = (format == CompressionFormat.SCI0) ?
                              (UInt16)bitStream.GetLSB(codeBitLength) :
                              (UInt16)bitStream.GetMSB(codeBitLength);
                if (code >= tableSize)
                {
                    throw new Exception(string.Format("LZW code {0} exceeds table size {1}", code.Print(), tableSize.Print()));
                }

                // Terminate on code 257
                if (code == 257)
                {
                    break;
                }

                // Reset the table on code 256
                if (code == 256)
                {
                    codeBitLength = 9;
                    tableSize = 258;
                    codeLimit = (format == CompressionFormat.SCI0) ? 512 : 511;
                    continue;
                }

                UInt16 newStringOffset = (UInt16)outputPos;
                if (code <= 255)
                {
                    // Code is a literal byte
                    output[outputPos++] = (byte)code;
                }
                else
                {
                    // Code is a table index
                    for (UInt16 i = 0; i < stringLengths[code]; i++)
                    {
                        output[outputPos++] = output[stringOffsets[code] + i];
                    }
                }

                // Stop adding to the table once it is full
                if (tableSize >= 4096)
                {
                    continue;
                }

                // Increase code size once a bit limit has been reached
                if (tableSize == codeLimit && codeBitLength < 12)
                {
                    codeBitLength++;
                    codeLimit = 1 << codeBitLength;
                    if (format == CompressionFormat.SCI1)
                    {
                        codeLimit--;
                    }
                }

                // Append code to table
                stringOffsets[tableSize] = newStringOffset;
                stringLengths[tableSize] = (UInt16)(outputPos - newStringOffset + 1);
                tableSize++;
            }

            return output;
        }
    }
}
