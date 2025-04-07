using System;

// BitStream reads bits from an array in MSB-first order or LSB-first.
//
// This is based on Variant 1 from Fabian Giesen's great bit reading articles,
// even though it's the example of what to improve on. That's okay! I don't need
// anything more complicated than this. It's comprehensible and fast enough.
// At least, it's much faster than the embarrassing one I wrote to get started.
//
// https://fgiesen.wordpress.com/2018/02/19/reading-bits-in-far-too-many-ways-part-1/
// https://fgiesen.wordpress.com/2018/02/20/reading-bits-in-far-too-many-ways-part-2/
// https://fgiesen.wordpress.com/2018/09/27/reading-bits-in-far-too-many-ways-part-3/

namespace SCI.Resource.Decompressors
{
    public class BitStream
    {
        byte[] array;
        UInt32 bytePosition;
        UInt32 byteEndPosition;
        UInt64 bitBuffer;
        int bitCount;

        public BitStream(byte[] array)
        {
            this.array = array;
            byteEndPosition = (UInt32)array.Length;
        }

        public BitStream(Span span, int bytePosition = 0)
        {
            array = span.Array;
            this.bytePosition = (UInt32)(span.Start + bytePosition);
            byteEndPosition = (UInt32)(span.Start + span.Length);
        }

        public bool EOF
        {
            get
            {
                return !(bytePosition < byteEndPosition || (bytePosition == byteEndPosition && bitCount != 0));
            }
        }

        public UInt32 GetMSB(int count)
        {
            // refill bit buffer
            while (bitCount < count)
            {
                bitBuffer |= ((UInt64)array[bytePosition++]) << (56 - bitCount);
                bitCount += 8;
            }

            // peek bits
            UInt32 result = (UInt32)(bitBuffer >> (64 - count));

            // consume bits
            bitBuffer <<= count;
            bitCount -= count;

            return result;
        }

        public UInt32 GetLSB(int count)
        {
            // refill bit buffer
            while (bitCount < count)
            {
                bitBuffer |= ((UInt64)array[bytePosition++]) << bitCount;
                bitCount += 8;
            }

            // peek bits
            UInt32 result = (UInt32)(bitBuffer & PeekBitMasks[count]);

            // consume bits
            bitBuffer >>= count;
            bitCount -= count;

            return result;
        }

        // Lookup table for: (1 << x) - 1
        static UInt32[] PeekBitMasks =
        {
            0x00000000,
            0x00000001,
            0x00000003,
            0x00000007,
            0x0000000F,
            0x0000001F,
            0x0000003F,
            0x0000007F,
            0x000000FF,
            0x000001FF,
            0x000003FF,
            0x000007FF,
            0x00000FFF,
            0x00001FFF,
            0x00003FFF,
            0x00007FFF,
            0x0000FFFF,
            0x0001FFFF,
            0x0003FFFF,
            0x0007FFFF,
            0x000FFFFF,
            0x001FFFFF,
            0x003FFFFF,
            0x007FFFFF,
            0x00FFFFFF,
            0x01FFFFFF,
            0x03FFFFFF,
            0x07FFFFFF,
            0x0FFFFFFF,
            0x1FFFFFFF,
            0x3FFFFFFF,
            0x7FFFFFFF,
            0xFFFFFFFF,
        };
    }
}
