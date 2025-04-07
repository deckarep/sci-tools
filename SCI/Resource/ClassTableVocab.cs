using System;
using System.Collections.Generic;

// Class Table: VOCAB.996
//
// The class table is an array of two word entries:
//
//   word    zero, filled in by SSCI at runtime
//   word    script number the class is in. FF FF for unused.
//
// A class number, or "species", is an index into this table.
//
// The class table is necessary for parsing scripts, because some games
// contain multiple scripts with a class with the same number (species).
// The class table reveals which of these scripts is actually used.
// Example: KQ6 has a RegionPath class in scripts 918 and 984.

namespace SCI.Resource
{
    public static class ClassTableVocab
    {
        public static UInt16[] Read(Span vocab)
        {
            vocab.Endian = DetectEndian(vocab);

            var classCount = vocab.Length / 4;
            var classes = new UInt16[classCount];
            for (int i = 0; i < classCount; ++i)
            {
                classes[i] = vocab.GetUInt16(i * 4 + 2);
            }
            return classes;
        }

        static Endian DetectEndian(Span vocab)
        {
            // only run detection if endianness is unknown;
            // the vocab endianness should always match the container.
            if (vocab.Endian != Endian.Unknown)
            {
                return vocab.Endian;
            }

            // there should always be script 999 or 64999 or 60000
            var classCount = vocab.Length / 4;
            var scripts = new HashSet<UInt16>();
            for (int i = 0; i < classCount; ++i)
            {
                scripts.Add(vocab.GetUInt16(i * 4 + 2));
            }

            // test for sci32 first.
            // Obj script is 64999 (fde7), in realm 60000 (ea60) [ LE only ]
            if (scripts.Contains(0xfde7) || scripts.Contains(0xea60))
            {
                return Endian.Little;
            }
            else if (scripts.Contains(0xe7fd))
            {
                return Endian.Big;
            }

            // now sci16. Obj script is 999 (03e7)
            if (scripts.Contains(0x03e7))
            {
                return Endian.Little;
            }
            else if (scripts.Contains(0xe703))
            {
                return Endian.Big;
            }
            return Endian.Unknown;
        }
    }
}
