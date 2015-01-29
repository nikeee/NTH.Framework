using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NTH.Framework
{
    internal static class ListExtensions
    {
        public static IntPtr ToNativeArray<T>(this IList<T> source)
            where T : struct
        {
            if (source.Count == 0)
                return IntPtr.Zero;

            int size = Marshal.SizeOf(typeof(T));
            var arrayPointer = Marshal.AllocHGlobal(source.Count * size);

            for (int i = 0; i < source.Count; ++i)
            {
                MarshalEx.StructureToPtr(source[i], arrayPointer + (size * i), false);
            }

            return arrayPointer;
        }
    }
}
