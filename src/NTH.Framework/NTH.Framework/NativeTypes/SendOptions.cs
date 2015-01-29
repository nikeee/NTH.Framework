using System;

namespace NTH.Framework.NativeTypes
{
    [Flags]
    internal enum SendOptions : int
    {
        LogonUI = 0x00000001,
        Dialog = 0x00000008
    }
}
