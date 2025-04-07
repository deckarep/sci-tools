using System;

namespace SCI.Resource
{
    public static class ResourceExtensions
    {
        public static string Print(this int i)
        {
            return string.Format("{0} (0x{0:X})", i);
        }

        public static string Print(this UInt16 i)
        {
            return string.Format("{0} (0x{0:X4})", i);
        }

        public static string Print(this UInt32 i)
        {
            return string.Format("{0} (0x{0:X8})", i);
        }
    }
}
