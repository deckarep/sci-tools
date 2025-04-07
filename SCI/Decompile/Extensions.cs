using System;
using System.Collections.Generic;
using SCI.Resource;

namespace SCI.Decompile
{
    static class Extensions
    {
        public static int IndexOf<T>(this IReadOnlyList<T> list, T item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(item)) return i;
            }
            return -1;
        }

        // 16-bit values have ambiguous signs in some places.
        // Prior to SCI32, treating them all as signed is a good guess.
        // Scripts weren't prone to using large values like >= 32767.
        // But in SCI32, they moved the system resources to very high numbers.
        // For example, all the system scripts are in the range 64000-64999.
        // So now, large unsigned resource numbers get used frequently.
        // My dumb heuristic is to treat 64000-64999 as unsigned in SCI32.
        public static int GetSignedOrUnsigned(this UInt16 raw, ByteCodeVersion byteCodeVersion)
        {
            if (byteCodeVersion == ByteCodeVersion.SCI0_11 ||
                raw >= 65000 ||
                raw == 64537 || // -999,  never used as a resource
                raw == 64536)   // -1000, never used as a resource
            {
                // signed
                return (Int16)raw;
            }
            else
            {
                // unsigned
                return raw;
            }
        }
    }
}
