using System;

namespace SCI.Resource.Decompressors
{
    public enum Compression
    {
        None,
        Huffman,
        Lzw0,
        Lzw1,
        Lzw1View,
        Lzw1Pic,
        DclImplode,
        StackerLzs
    }

    public enum CompressionFormat
    {
        SCI0,
        SCI1
    }

    public static class Decompressor
    {
        public static byte[] Decompress(Compression compression, Span source, int uncompressedSize)
        {
            switch (compression)
            {
                case Compression.Huffman:
                    return HuffmanDecompressor.Decompress(source, uncompressedSize);
                case Compression.Lzw0:
                    return LzwDecompressor.Decompress(CompressionFormat.SCI0, source, uncompressedSize);
                case Compression.Lzw1:
                    return LzwDecompressor.Decompress(CompressionFormat.SCI1, source, uncompressedSize);
                case Compression.DclImplode:
                    return DclImplodeDecompressor.Decompress(source, uncompressedSize);
                case Compression.StackerLzs:
                   return StackerLzsDecompressor.Decompress(source, uncompressedSize);

                // I haven't implemented Lzw1View or Lzw1Pic because I haven't written
                // anything that's dealt with parsing SCI1 views or pics yet.
                // Sierra heavily transformed the two formats before applying LZW
                // for better compression, so those rather intense parsings and
                // transformations need to be applied after LZW decompression.
                default:
                    throw new NotImplementedException("Compression not implemented: " + compression);
            }
        }
    }
}
