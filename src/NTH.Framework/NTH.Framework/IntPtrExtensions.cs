using System;
using System.Runtime.InteropServices;

namespace NTH.Framework
{
    internal static class IntPtrExtensions
    {
        public static void FreeNativeArray<T>(this IntPtr ptr, int itemCount)
            where T : struct
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Invalid pointer to free");

            if (itemCount <= 0)
                return;

            int size = Marshal.SizeOf(typeof(T));
            try
            {
                for (int i = 0; i < itemCount; ++i)
                {
                    MarshalEx.DestroyStructure<T>(ptr + (size * i));
                }
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }
}
