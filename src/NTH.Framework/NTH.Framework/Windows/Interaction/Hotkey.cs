using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NTH.Framework.Windows.Interaction
{
    public class Hotkey
    {
        private readonly ModifierKeys _modifiers;
        public ModifierKeys Modifiers { get { return _modifiers; } }

        private readonly Keys _key;
        public Keys Key { get { return _key; } }

        public Hotkey(ModifierKeys modifiers, Keys key)
        {
            _modifiers = modifiers;
            _key = key;
        }

        public override int GetHashCode()
        {
            return (int)Key << 16 | (int)Modifiers;
        }

        internal static Hotkey FromHashCode(int hashCode)
        {
            throw new NotImplementedException();
        }

        internal void InvokePressed(KeyboardHook hook)
        {
            var kp = KeyPressed;
            if (kp != null)
                kp(this, new HotkeyPressedEventArgs(hook, this));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if ((_modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
                sb.Append("Alt").Append('+');
            if ((_modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                sb.Append("Ctrl").Append('+');
            if ((_modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                sb.Append("Shift").Append('+');
            if ((_modifiers & ModifierKeys.Win) == ModifierKeys.Win)
                sb.Append("Win").Append('+');

            sb.Append(_key == Keys.Space ? "Space" : _key.ToString());
            return sb.ToString();
        }

        /// <summary>The hotkey has been pressed.</summary>
        public event EventHandler<HotkeyPressedEventArgs> KeyPressed;
    }
}
