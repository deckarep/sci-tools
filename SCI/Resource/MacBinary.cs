using System;
using System.Linq;

// MacBinary parser. Supports the MacBinary Standard from 1987,
// also known as MacBinary II. This matches ScummVM's support.
//
// MacBinary files start with a 128 byte header and then an
// optional data fork and then an optional resource fork.
// I'm only interested in the resource fork.

namespace SCI.Resource
{
    public static class MacBinary
    {
        const int HeaderSize = 128;
        const int MaxFileNameLength = 63;

        // header offsets
        const int MbVersion = 0; // always zero
        const int MbNameLength = 1;
        const int MbType = 65;
        const int MbCreator = 69;
        const int MbFlagsHigh = 73;
        const int MbZero2 = 74; // always zero
        const int MbPosY = 75;
        const int MbPosX = 77;
        const int MbFolderId = 79;
        const int MbZero3 = 82; // always zero
        const int MbDataForkLength = 83;
        const int MbRsrcForkLength = 87;
        const int MbFlagsLow = 101;
        const int MbCrc = 124;

        public static Span Read(Span span)
        {
            if (!IsValid(span))
            {
                return null;
            }

            // the validation did all the real work; just read
            // the lengths of both forks and calculate the start
            // of the resource fork.
            UInt32 dataLength = span.GetUInt32BE(MbDataForkLength);
            UInt32 rsrcLength = span.GetUInt32BE(MbRsrcForkLength);

            UInt32 dataLengthPadded = ((dataLength + 127) >> 7) << 7;
            UInt32 rsrcStart = HeaderSize + dataLengthPadded;

            return span.Slice((int)rsrcStart, (int)(rsrcLength));
        }

        public static bool IsValid(Span span)
        {
            if (span.Length < HeaderSize) return false;

            // reject the file if the entire header is zeros.
            // crc won't catch that because crc(0) == 0.
            // scummvm only checks a few key fields.
            if (span.Slice(0, HeaderSize).All(b => b == 0))
            {
                return false;
            }

            // spec says to first test that bytes 0 and 74 are zero.
            // in MB2, byte 82 must be zero too, and that's the only
            // version i'm supporting.
            if (span[MbVersion] != 0 ||
                span[MbZero2] != 0 ||
                span[MbZero3] != 0)
            {
                return false;
            }

            // scummvm validates name length
            if (span[MbNameLength] > MaxFileNameLength)
            {
                return false;
            }

            // validate lengths.
            // data and resource forks are padded with nulls at the
            // end to be a multiple of 128 bytes. that padding is not
            // included in the length. scummvm calculates this and
            // validates that header + lengths + padding exactly
            // matches the buffer size. but there's also a comment
            // that isobuster doesn't include padding, which would be
            // a problem, but then confusingly that code is commented
            // out. so does it or not? why aren't we just checking
            // both lengths?
            //
            // i don't know! i'm doing what scummvm does.
            UInt32 dataLength = span.GetUInt32BE(MbDataForkLength);
            UInt32 rsrcLength = span.GetUInt32BE(MbRsrcForkLength);

            UInt32 dataLengthPadded = ((dataLength + 127) >> 7) << 7;
            if (HeaderSize + dataLengthPadded + rsrcLength > span.Length)
            {
                return false;
            }

            return true;
        }
    }
}
