using System;

// Macintosh resource compression used in SCI1.1 and later. SCI resources are
// stored in the Macintosh resource fork of files named Data1, Data2, etc.
//
// This is a sliding window compression scheme; the tokens contain strings of
// literal bytes and lengths/offsets to strings of recently output bytes.
//
// The last four bytes of the Mac resource are the uncompressed size; zero
// indicates no compression. Some games don't have this footer, so there are
// three possibilities to detect and handle:
// - Uncompressed with no footer
// - Uncompressed with footer (00 00 00 00)
// - Compressed with footer

namespace SCI.Resource.Decompressors
{
    public static class MacDecompressor
    {
        public static Span Decompress(Span resource)
        {
            // read footer and strip it
            UInt32 uncompressedSize = resource.GetUInt32BE(resource.Length - 4);
            resource = resource.Slice(0, resource.Length - 4);

            // if footer says it's uncompressed then we're done
            if (uncompressedSize == 0)
            {
                return resource;
            }

            // decompress
            byte[] output = Decompress(resource, (int)uncompressedSize);

            // assume anything mac-compressed is big endian
            return new Span(output, Endian.Big);
        }

        public static byte[] Decompress(Span span, int uncompressedSize)
        {
            byte[] output = new byte[uncompressedSize];
            int outputPos = 0;

            var stream = new SpanStream(span);
            while (!stream.EOF && outputPos < uncompressedSize)
            {
                byte b0 = stream.ReadByte();
                if (b0 == 0xff) 
                {
                    break; // Terminator
                }

                // First two bits determine token type:
                // 11 Literal string of bytes
                // 10 Literal string of bytes followed by a long copy
                // 0* Literal string of bytes followed by a short copy
                byte b1, b2;
                int literalLength, copyOffset, copyLength;
                switch (b0 >> 6)
                {
                    case 3:
                        // Literal string
                        // 2 bits: 11
                        // 2 bits: non-zero for a two bit length, zero for four bits
                        // 4 bits: literal length
                        if ((b0 & 0x30) != 0)
                        {
                            // length is lowest two bits
                            literalLength = b0 & 3;
                        }
                        else
                        {
                            // length is lowest 4 bits * 4 + 4
                            literalLength = (b0 & 0xf) * 4 + 4;
                        }

                        for (int i = 0; i < literalLength; i++)
                        {
                            output[outputPos++] = stream.ReadByte();
                        }
                        break;

                    case 2:
                        // Literal string + copy string (long)
                        // 2 bits: 10
                        // 6 bits: lower bits of copy offset
                        // 3 bits: middle bits of copy offset
                        // 5 bits: copy length += 3
                        // 6 bits: upper bits of copy offset
                        // 2 bits: literal length
                        b1 = stream.ReadByte();
                        b2 = stream.ReadByte();
                        copyOffset = ((b0 & 0x3f) | ((b1 & 0xe0) << 1) | ((b2 & 0xfc) << 7)) + 1;
                        copyLength = (b1 & 0x1f) + 3;
                        literalLength = b2 & 3;

                        for (int i = 0; i < literalLength; i++)
                        {
                            output[outputPos++] = stream.ReadByte();
                        }
                        for (int i = 0; i < copyLength; i++)
                        {
                            byte b = output[outputPos - copyOffset];
                            output[outputPos++] = b;
                        }
                        break;

                    default:
                        // Literal string + copy string (short)
                        // 1 bit: 0
                        // 7 bits: lower bits of copy offset
                        // 3 bits: upper bits of copy offset
                        // 2 bits: literal length
                        // 3 bits: copy length += 3
                        b1 = stream.ReadByte();
                        copyOffset = (b0 + ((b1 & 0xe0) << 2)) + 1;
                        literalLength = (b1 >> 3) & 0x3;
                        copyLength = (b1 & 0x7) + 3;

                        for (int i = 0; i < literalLength; i++)
                        {
                            output[outputPos++] = stream.ReadByte();
                        }
                        for (int i = 0; i < copyLength; i++)
                        {
                            byte b = output[outputPos - copyOffset];
                            output[outputPos++] = b;
                        }
                        break;
                }
            }
            return output;
        }

        // Detect a compression footer by examining a script or heap resource.
        //
        // We're trying to figure out if this game includes a compression footer
        // at the end of resources. If there is no footer, then the game never
        // uses compression. If there is a footer, then any resource with a
        // non-zero footer value is compressed.
        //
        // This function returns true if it successfully determines whether or
        // not there is a footer, in which case hasFooter can be used. If the
        // results are ambiguous, try another script or heap until successful.
        //
        // Script11 heap/hunk resources both have the same property that makes
        // them useful for this detection: they begin with an offset to the
        // relocation table at the end, and the relocation table begins with
        // the size of the table. If these values land at the end of the
        // resource then there is no footer. If they land four bytes before
        // the end followed by zeros, then there is an uncompressed footer.
        // Otherwise, if the last four bytes are larger than the resource,
        // then assume the resource is compressed.
        public static bool DetectCompressionFooter(Span script, out bool hasFooter)
        {
            UInt32 uncompressedSize = script.GetUInt32BE(script.Length - 4);
            int calculatedSize;
            if (CalculateScriptSize(script, out calculatedSize))
            {
                if (calculatedSize == script.Length)
                {
                    // no compression footer
                    hasFooter = false;
                    return true;
                }
                else if (calculatedSize == script.Length - 4 && uncompressedSize == 0)
                {
                    // compression footer, although this one is uncompressed
                    hasFooter = true;
                    return true;
                }
            }

            // at this point, if the footer is large enough then assume compression
            if (uncompressedSize > script.Length - 4)
            {
                hasFooter = true;
                return true;
            }

            // i don't know!
            hasFooter = false;
            return false;
        }

        static bool CalculateScriptSize(Span script, out int scriptSize)
        {
            UInt16 relocationPosition = script.GetUInt16BE(0);
            if (!(relocationPosition + 2 < script.Length))
            {
                scriptSize = 0;
                return false;
            }
            UInt16 relocationCount = script.GetUInt16BE(relocationPosition);
            scriptSize = relocationPosition + 2 + (relocationCount * 2);
            return (scriptSize <= script.Length);
        }
    }
}
