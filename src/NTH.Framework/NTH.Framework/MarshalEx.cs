using System;
using System.Runtime.InteropServices;

namespace NTH.Framework
{
    internal static class MarshalEx
    {
        public static void StructureToPtr<T>(T structure, IntPtr ptr, bool fDeleteOld)
            where T : struct
        {
            Marshal.StructureToPtr(structure, ptr, fDeleteOld);
        }

        public static void DestroyStructure<T>(IntPtr ptr)
            where T : struct
        {
            Marshal.DestroyStructure(ptr, typeof(T));
        }
    }
}
