using System;
using System.Runtime.InteropServices;

namespace NTH.Framework.NativeTypes
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal class MapiMessage
    {
        public int reserved;
        public string subject;
        public string noteText;
        public string messageType;
        public string dateReceived;
        public string conversationID;
        public int flags;
        public IntPtr originator;
        public int recipCount;
        public IntPtr recips;
        public int fileCount;
        public IntPtr files;
    }
}
