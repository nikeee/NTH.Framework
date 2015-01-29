using System.Windows.Forms;
using NTH.Framework.NativeTypes;
using System;
using System.Runtime.InteropServices;
using NTH.Framework.Windows.Interaction;

namespace NTH.Framework
{
    internal static class NativeMethods
    {
        private const string Mapi32 = "mapi32.dll";
        private const string User32 = "user32.dll";

        [DllImport(Mapi32)]
        internal static extern SendMailReturnValue MAPISendMail(IntPtr sess, IntPtr hwnd, MapiMessage message, SendOptions flags, int rsv);

        [DllImport(User32)]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, ModifierKeys fsModifiers, Keys vk);
        [DllImport(User32)]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}
