using System;
using System.Diagnostics;

namespace NTH.Framework.Windows.Interaction
{
    /// <summary>Event Args for the event that is fired after the hotkey has been pressed.</summary>
    public class HotkeyPressedEventArgs : EventArgs
    {
        private readonly Hotkey _hotkey;
        public Hotkey Hotkey { get { return _hotkey; } }

        private readonly KeyboardHook _hook;
        public KeyboardHook Hook { get { return _hook; } }

        internal HotkeyPressedEventArgs(KeyboardHook hook, Hotkey hotkey)
        {
            Debug.Assert(hook != null);
            Debug.Assert(hotkey != null);
            _hook = hook;
            _hotkey = hotkey;
        }
    }
}
