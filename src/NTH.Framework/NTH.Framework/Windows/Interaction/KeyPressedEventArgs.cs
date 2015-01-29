using System;
using System.Windows.Forms;

namespace NTH.Framework.Windows.Interaction
{
    /// <summary>Event Args for the event that is fired after the hotkey has been pressed.</summary>
    internal class KeyPressedEventArgs : EventArgs
    {
        private readonly ModifierKeys _modifier;
        private readonly Keys _key;
        public ModifierKeys Modifier { get { return _modifier; } }
        public Keys Key { get { return _key; } }

        internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
        {
            _modifier = modifier;
            _key = key;
        }

        public int GetIdentifier()
        {
            return (int)_key << 16 | (int)_modifier;
        }
    }
}
