using System;
using System.Runtime.InteropServices;

namespace NTH.Framework.NativeTypes
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct MapiRecipientDescription
    {
        public int reserved;
        public RecipientKind recipClass;
        public string name;
        public string address;
        public int eIDSize;
        public IntPtr entryID;
    }
}
