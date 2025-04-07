using System;

// DCL IMPLODE is used in PC versions of SCI1.1 for all resource types.
// Sierra also used it for compressing save game files.
//
// This compression is by PKWARE in their Data Compression Library but it is
// different than PKWARE's "implode" compression method #6 in zip files.
//
// See Ben Rudiak-Gould's reverse engineered description from 2001:
// https://groups.google.com/g/comp.compression/c/M5P064or93o/m/W1ca1-ad6kgJ
//
// This is a sliding window compression scheme with loads of Huffman encoding.

namespace SCI.Resource.Decompressors
{
    public static class DclImplodeDecompressor
    {
        public static byte[] Decompress(Span span, int uncompressedSize)
        {
            byte[] output = new byte[uncompressedSize];
            int outputPos = 0;

            Mode mode;
            switch (span[0])
            {
                case 0: mode = Mode.Binary; break;
                case 1: mode = Mode.Ascii; break;
                default: throw new Exception(string.Format("DCL: Illegal first byte: {0:X2}", span[0]));
            }

            byte lowerOffsetBitSize = span[1];
            if (!(4 <= lowerOffsetBitSize && lowerOffsetBitSize <= 6)) {
                throw new Exception(string.Format("DCL: Illegal second byte: {0:X2}", span[1]));
            }

            var bitStream = new BitStream(span, 2);
            while (outputPos < uncompressedSize && !bitStream.EOF)
            {
                if (bitStream.GetLSB(1) == 0)
                {
                    // Literal byte
                    if (mode == Mode.Binary)
                    {
                        output[outputPos++] = (byte)bitStream.GetLSB(8);
                    }
                    else
                    {
                        output[outputPos++] = (byte)DecodeHuffmanValue(bitStream, AsciiHuffmanTree);
                    }
                }
                else
                {
                    // String from dictionary or terminator. Three parts:
                    // 1. Length of string (Huffman encoded + lower bits)
                    // 2. Upper six bits of offset (Huffman encoded)
                    // 3. Lower bits of offset
                    //
                    // Length 519 is the terminator (000000011111111).
                    // The number of lower bits is in the header, unless Length
                    // is two, in which case the number of lower bits is two.
                    UInt16 stringLength = DecodeHuffmanValue(bitStream, LengthHuffmanTree);
                    if (stringLength <= 7)
                    {
                        // 0-7: the length (2-9)
                        stringLength += 2;
                    }
                    else
                    {
                        // 8-15: the group
                        // [8]   10-11 00100x
                        // [9]   12-15 00011xx
                        // [10]  16-23 00010xxx
                        // ...
                        // [15] 264-519 0000000xxxxxxxx (519 is terminator)
                        int lowerBitCount = stringLength - 7;
                        stringLength = (UInt16)(8 + (1 << lowerBitCount));
                        stringLength += (UInt16)bitStream.GetLSB(lowerBitCount);
                    }
                    if (stringLength == 519)
                    {
                        break; // Terminator
                    }

                    int offset = DecodeHuffmanValue(bitStream, OffsetHuffmanTree);
                    if (stringLength == 2)
                    {
                        offset <<= 2;
                        offset |= (int)bitStream.GetLSB(2);
                    }
                    else
                    {
                        offset <<= lowerOffsetBitSize;
                        offset += (int)bitStream.GetLSB(lowerOffsetBitSize);
                    }

                    // write the string
                    int stringStartOffset = outputPos - offset - 1;
                    int stringEndOffset = stringStartOffset + stringLength;
                    for (int i = stringStartOffset; i < stringEndOffset; i++)
                    {
                        output[outputPos++] = output[i];
                    }
                }
            }

            return output;
        }

        static UInt16 DecodeHuffmanValue(BitStream bitStream, UInt32[] tree)
        {
            // Each element in the array is a node. The msb indicates a leaf.
            // Leaf:   16 bits: 10000000 00000000
            //         16 bits: the encoded value
            // Branch:  8 bits: 00000000 
            //         12 bits: left node index  (bit == 0)
            //         12 bits: right node index (bit == 1)
            UInt32 index = 0;
            do
            {
                if (bitStream.GetLSB(1) == 0)
                {
                    index = tree[index] >> 12;
                }
                else
                {
                    index = tree[index] & 0x00000fff;
                }
            } while ((tree[index] & 0x80000000) == 0);

            return (UInt16)(tree[index] & 0x0000ffff);
        }

        enum Mode { Binary, Ascii }

        static UInt32[] AsciiHuffmanTree = {
            0x00001002, 0x00003004, 0x00005006, 0x00007008,
            0x0000900a, 0x0000b00c, 0x0000d00e, 0x0000f010,
            0x00011012, 0x00013014, 0x00015016, 0x00017018,
            0x0001901a, 0x0001b01c, 0x0001d01e, 0x0001f020,
            0x00021022, 0x00023024, 0x00025026, 0x00027028,
            0x0002902a, 0x0002b02c, 0x0002d02e, 0x0002f030,
            0x00031032, 0x00033034, 0x00035036, 0x00037038,
            0x0003903a, 0x0003b03c, 0x80000020, 0x0003d03e,
            0x0003f040, 0x00041042, 0x00043044, 0x00045046,
            0x00047048, 0x0004904a, 0x0004b04c, 0x0004d04e,
            0x0004f050, 0x00051052, 0x00053054, 0x00055056,
            0x00057058, 0x0005905a, 0x0005b05c, 0x0005d05e,
            0x0005f060, 0x00061062, 0x80000075, 0x80000074,
            0x80000073, 0x80000072, 0x8000006f, 0x8000006e,
            0x8000006c, 0x80000069, 0x80000065, 0x80000061,
            0x80000045, 0x00063064, 0x00065066, 0x00067068,
            0x0006906a, 0x0006b06c, 0x0006d06e, 0x0006f070,
            0x00071072, 0x00073074, 0x00075076, 0x00077078,
            0x0007907a, 0x0007b07c, 0x0007d07e, 0x0007f080,
            0x00081082, 0x00083084, 0x00085086, 0x80000070,
            0x8000006d, 0x80000068, 0x80000067, 0x80000066,
            0x80000064, 0x80000063, 0x80000062, 0x80000054,
            0x80000053, 0x80000052, 0x8000004f, 0x8000004e,
            0x8000004c, 0x80000049, 0x80000044, 0x80000043,
            0x80000041, 0x80000031, 0x8000002d, 0x00087088,
            0x0008908a, 0x0008b08c, 0x0008d08e, 0x0008f090,
            0x00091092, 0x00093094, 0x00095096, 0x00097098,
            0x0009909a, 0x0009b09c, 0x0009d09e, 0x0009f0a0,
            0x000a10a2, 0x000a30a4, 0x80000077, 0x8000006b,
            0x80000055, 0x80000050, 0x8000004d, 0x80000046,
            0x80000042, 0x8000003d, 0x80000038, 0x80000037,
            0x80000035, 0x80000034, 0x80000033, 0x80000032,
            0x80000030, 0x8000002e, 0x8000002c, 0x80000029,
            0x80000028, 0x8000000d, 0x8000000a, 0x000a50a6,
            0x000a70a8, 0x000a90aa, 0x000ab0ac, 0x000ad0ae,
            0x000af0b0, 0x000b10b2, 0x000b30b4, 0x000b50b6,
            0x000b70b8, 0x000b90ba, 0x000bb0bc, 0x000bd0be,
            0x000bf0c0, 0x80000079, 0x80000078, 0x80000076,
            0x8000005f, 0x8000005b, 0x80000057, 0x80000048,
            0x80000047, 0x8000003a, 0x80000039, 0x80000036,
            0x8000002f, 0x8000002a, 0x80000027, 0x80000022,
            0x80000009, 0x000c10c2, 0x000c30c4, 0x000c50c6,
            0x000c70c8, 0x000c90ca, 0x000cb0cc, 0x000cd0ce,
            0x000cf0d0, 0x000d10d2, 0x000d30d4, 0x000d50d6,
            0x000d70d8, 0x000d90da, 0x000db0dc, 0x000dd0de,
            0x000df0e0, 0x000e10e2, 0x000e30e4, 0x000e50e6,
            0x000e70e8, 0x000e90ea, 0x8000005d, 0x80000059,
            0x80000058, 0x80000056, 0x8000004b, 0x8000003e,
            0x8000002b, 0x000eb0ec, 0x000ed0ee, 0x000ef0f0,
            0x000f10f2, 0x000f30f4, 0x000f50f6, 0x000f70f8,
            0x000f90fa, 0x000fb0fc, 0x000fd0fe, 0x000ff100,
            0x00101102, 0x00103104, 0x00105106, 0x00107108,
            0x0010910a, 0x0010b10c, 0x0010d10e, 0x0010f110,
            0x00111112, 0x00113114, 0x00115116, 0x00117118,
            0x0011911a, 0x0011b11c, 0x0011d11e, 0x0011f120,
            0x00121122, 0x00123124, 0x00125126, 0x00127128,
            0x0012912a, 0x0012b12c, 0x0012d12e, 0x0012f130,
            0x00131132, 0x00133134, 0x8000007a, 0x80000071,
            0x80000026, 0x80000024, 0x80000021, 0x00135136,
            0x00137138, 0x0013913a, 0x0013b13c, 0x0013d13e,
            0x0013f140, 0x00141142, 0x00143144, 0x00145146,
            0x00147148, 0x0014914a, 0x0014b14c, 0x0014d14e,
            0x0014f150, 0x00151152, 0x00153154, 0x00155156,
            0x00157158, 0x0015915a, 0x0015b15c, 0x0015d15e,
            0x0015f160, 0x00161162, 0x00163164, 0x00165166,
            0x00167168, 0x0016916a, 0x0016b16c, 0x0016d16e,
            0x0016f170, 0x00171172, 0x00173174, 0x00175176,
            0x00177178, 0x0017917a, 0x0017b17c, 0x0017d17e,
            0x0017f180, 0x00181182, 0x00183184, 0x00185186,
            0x00187188, 0x0018918a, 0x0018b18c, 0x0018d18e,
            0x0018f190, 0x00191192, 0x00193194, 0x00195196,
            0x00197198, 0x0019919a, 0x0019b19c, 0x0019d19e,
            0x0019f1a0, 0x001a11a2, 0x001a31a4, 0x001a51a6,
            0x001a71a8, 0x001a91aa, 0x001ab1ac, 0x001ad1ae,
            0x001af1b0, 0x001b11b2, 0x001b31b4, 0x8000007c,
            0x8000007b, 0x8000006a, 0x8000005c, 0x8000005a,
            0x80000051, 0x8000004a, 0x8000003f, 0x8000003c,
            0x80000000, 0x001b51b6, 0x001b71b8, 0x001b91ba,
            0x001bb1bc, 0x001bd1be, 0x001bf1c0, 0x001c11c2,
            0x001c31c4, 0x001c51c6, 0x001c71c8, 0x001c91ca,
            0x001cb1cc, 0x001cd1ce, 0x001cf1d0, 0x001d11d2,
            0x001d31d4, 0x001d51d6, 0x001d71d8, 0x001d91da,
            0x001db1dc, 0x001dd1de, 0x001df1e0, 0x001e11e2,
            0x001e31e4, 0x001e51e6, 0x001e71e8, 0x001e91ea,
            0x001eb1ec, 0x001ed1ee, 0x001ef1f0, 0x001f11f2,
            0x001f31f4, 0x001f51f6, 0x001f71f8, 0x001f91fa,
            0x001fb1fc, 0x001fd1fe, 0x800000f4, 0x800000f3,
            0x800000f2, 0x800000ee, 0x800000e9, 0x800000e5,
            0x800000e1, 0x800000df, 0x800000de, 0x800000dd,
            0x800000dc, 0x800000db, 0x800000da, 0x800000d9,
            0x800000d8, 0x800000d7, 0x800000d6, 0x800000d5,
            0x800000d4, 0x800000d3, 0x800000d2, 0x800000d1,
            0x800000d0, 0x800000cf, 0x800000ce, 0x800000cd,
            0x800000cc, 0x800000cb, 0x800000ca, 0x800000c9,
            0x800000c8, 0x800000c7, 0x800000c6, 0x800000c5,
            0x800000c4, 0x800000c3, 0x800000c2, 0x800000c1,
            0x800000c0, 0x800000bf, 0x800000be, 0x800000bd,
            0x800000bc, 0x800000bb, 0x800000ba, 0x800000b9,
            0x800000b8, 0x800000b7, 0x800000b6, 0x800000b5,
            0x800000b4, 0x800000b3, 0x800000b2, 0x800000b1,
            0x800000b0, 0x8000007f, 0x8000007e, 0x8000007d,
            0x80000060, 0x8000005e, 0x80000040, 0x8000003b,
            0x80000025, 0x80000023, 0x8000001f, 0x8000001e,
            0x8000001d, 0x8000001c, 0x8000001b, 0x80000019,
            0x80000018, 0x80000017, 0x80000016, 0x80000015,
            0x80000014, 0x80000013, 0x80000012, 0x80000011,
            0x80000010, 0x8000000f, 0x8000000e, 0x8000000c,
            0x8000000b, 0x80000008, 0x80000007, 0x80000006,
            0x80000005, 0x80000004, 0x80000003, 0x80000002,
            0x80000001, 0x800000ff, 0x800000fe, 0x800000fd,
            0x800000fc, 0x800000fb, 0x800000fa, 0x800000f9,
            0x800000f8, 0x800000f7, 0x800000f6, 0x800000f5,
            0x800000f1, 0x800000f0, 0x800000ef, 0x800000ed,
            0x800000ec, 0x800000eb, 0x800000ea, 0x800000e8,
            0x800000e7, 0x800000e6, 0x800000e4, 0x800000e3,
            0x800000e2, 0x800000e0, 0x800000af, 0x800000ae,
            0x800000ad, 0x800000ac, 0x800000ab, 0x800000aa,
            0x800000a9, 0x800000a8, 0x800000a7, 0x800000a6,
            0x800000a5, 0x800000a4, 0x800000a3, 0x800000a2,
            0x800000a1, 0x800000a0, 0x8000009f, 0x8000009e,
            0x8000009d, 0x8000009c, 0x8000009b, 0x8000009a,
            0x80000099, 0x80000098, 0x80000097, 0x80000096,
            0x80000095, 0x80000094, 0x80000093, 0x80000092,
            0x80000091, 0x80000090, 0x8000008f, 0x8000008e,
            0x8000008d, 0x8000008c, 0x8000008b, 0x8000008a,
            0x80000089, 0x80000088, 0x80000087, 0x80000086,
            0x80000085, 0x80000084, 0x80000083, 0x80000082,
            0x80000081, 0x80000080, 0x8000001a
        };

        static UInt32[] LengthHuffmanTree = {
            0x00001002, 0x00003004, 0x00005006, 0x00007008,
            0x0000900a, 0x0000b00c, 0x80000001, 0x0000d00e,
            0x0000f010, 0x00011012, 0x80000003, 0x80000002,
            0x80000000, 0x00013014, 0x00015016, 0x00017018,
            0x80000006, 0x80000005, 0x80000004, 0x0001901a,
            0x0001b01c, 0x8000000a, 0x80000009, 0x80000008,
            0x80000007, 0x0001d01e, 0x8000000d, 0x8000000c,
            0x8000000b, 0x8000000f, 0x8000000e
        };

        static UInt32[] OffsetHuffmanTree = {
            0x00001002, 0x00003004, 0x00005006, 0x00007008,
            0x0000900a, 0x0000b00c, 0x80000000, 0x0000d00e,
            0x0000f010, 0x00011012, 0x00013014, 0x00015016,
            0x00017018, 0x0001901a, 0x0001b01c, 0x0001d01e,
            0x0001f020, 0x00021022, 0x00023024, 0x00025026,
            0x00027028, 0x0002902a, 0x0002b02c, 0x80000002,
            0x80000001, 0x0002d02e, 0x0002f030, 0x00031032,
            0x00033034, 0x00035036, 0x00037038, 0x0003903a,
            0x0003b03c, 0x0003d03e, 0x0003f040, 0x00041042,
            0x00043044, 0x00045046, 0x00047048, 0x0004904a,
            0x0004b04c, 0x80000006, 0x80000005, 0x80000004,
            0x80000003, 0x0004d04e, 0x0004f050, 0x00051052,
            0x00053054, 0x00055056, 0x00057058, 0x0005905a,
            0x0005b05c, 0x0005d05e, 0x0005f060, 0x00061062,
            0x00063064, 0x00065066, 0x00067068, 0x0006906a,
            0x0006b06c, 0x0006d06e, 0x80000015, 0x80000014,
            0x80000013, 0x80000012, 0x80000011, 0x80000010,
            0x8000000f, 0x8000000e, 0x8000000d, 0x8000000c,
            0x8000000b, 0x8000000a, 0x80000009, 0x80000008,
            0x80000007, 0x0006f070, 0x00071072, 0x00073074,
            0x00075076, 0x00077078, 0x0007907a, 0x0007b07c,
            0x0007d07e, 0x8000002f, 0x8000002e, 0x8000002d,
            0x8000002c, 0x8000002b, 0x8000002a, 0x80000029,
            0x80000028, 0x80000027, 0x80000026, 0x80000025,
            0x80000024, 0x80000023, 0x80000022, 0x80000021,
            0x80000020, 0x8000001f, 0x8000001e, 0x8000001d,
            0x8000001c, 0x8000001b, 0x8000001a, 0x80000019,
            0x80000018, 0x80000017, 0x80000016, 0x8000003f,
            0x8000003e, 0x8000003d, 0x8000003c, 0x8000003b,
            0x8000003a, 0x80000039, 0x80000038, 0x80000037,
            0x80000036, 0x80000035, 0x80000034, 0x80000033,
            0x80000032, 0x80000031, 0x80000030
        };
    }
}
