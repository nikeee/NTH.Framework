using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace NTH.Framework.Windows.Interaction
{
    /// <summary>Represents the window that is used internally to get the messages.</summary>
    internal class HotkeyWindowHost : NativeWindow, IDisposable
    {
        public HotkeyWindowHost()
        {
            CreateHandle(new CreateParams()); // create the handle for the window.
        }

        /// <summary>Overridden to get the notifications.</summary>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            const int WM_HOTKEY = 0x0312;

            // check if we got a hotkey pressed.
            if (m.Msg == WM_HOTKEY)
            {
                // get the keys.
                var key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                var modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

                // invoke the event to notify the parent.
                var ev = KeyPressed;
                if (ev != null)
                {
                    ev(this, new KeyPressedEventArgs(modifier, key));
                }
            }
        }

        public void RegisterHotkey(ModifierKeys modifiers, Keys key, int id)
        {
            if (!NativeMethods.RegisterHotKey(Handle, id, modifiers, key))
                throw new Win32Exception();
        }

        public void UnregisterHotkey(int id)
        {
            if (!NativeMethods.UnregisterHotKey(Handle, id))
                throw new Win32Exception();
        }

        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        #region IDisposable Members

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                DestroyHandle();
                _disposed = true;
            }
        }

        ~HotkeyWindowHost()
        {
            Dispose(false);
        }

        #endregion

    }
}
