using System;

// Huffman compression is used in SCI0 through SCI1 for picture resources.
//
// Header
//   u8  node count
//   u8  most common byte in output (used as terminator)
// Tree nodes
//   u8  value (only used in leaf nodes)
//   u4  left node offset
//   u4  right node offset 
// Bitstream
//
// Decoded bytes come from nodes or the bitstream. Common bytes have nodes,
// uncommon bytes are embedded in the bitstream as literals. The bitstream
// terminates in the most common byte appearing as a literal instead of a node.
// The most common byte is specified in the header. Node offsets are indexes
// relative to the current node's index. A leaf node has both offsets as zero.

namespace SCI.Resource.Decompressors
{
    public static class HuffmanDecompressor
    {
        public static byte[] Decompress(Span span, int uncompressedSize)
        {
            byte[] output = new byte[uncompressedSize];
            int outputPos = 0;

            // header
            byte nodeCount = span.GetByte(0);
            byte terminatorByte = span.GetByte(1);
            Span nodes = span.Slice(2, nodeCount * 2);

            // bitstream
            var bitStream = new BitStream(span, 2 + (nodes.Length));

            UInt16 terminatorWord = (UInt16)(0x0100 | terminatorByte);
            while (outputPos < uncompressedSize && !bitStream.EOF)
            {
                UInt16 decodedValue = Decode(nodes, bitStream);
                if (decodedValue == terminatorWord)
                {
                    break;
                }
                output[outputPos++] = (byte)decodedValue;
            }
            return output;
        }

        static UInt16 Decode(Span nodes, BitStream bitStream)
        {
            int nodeIndex = 0;
            while (true)
            {
                int nodePosition = nodeIndex * 2;
                if (nodes[nodePosition + 1] == 0)
                {
                    // leaf node reached; use its value
                    return nodes[nodePosition];
                }

                UInt32 bit = bitStream.GetMSB(1);
                if (bit == 0)
                {
                    // traverse the left node
                    int left = nodes[nodePosition + 1] >> 4;
                    nodeIndex += left;
                }
                else
                {
                    // traverse the right node if there is one,
                    // otherwise this is a literal byte in the bitstream.
                    int right = nodes[nodePosition + 1] & 0x0f;
                    if (right != 0)
                    {
                        nodeIndex += right;
                    }
                    else
                    {
                        // this is a literal byte in the bitstream.
                        // return it as a word with the upper byte set
                        // to indicate that this was a literal, that is
                        // used to determine if this is the terminator.
                        UInt16 literal = (UInt16)bitStream.GetMSB(8);
                        literal |= 0x0100;
                        return literal;
                    }
                }
            }
        }
    }
}
