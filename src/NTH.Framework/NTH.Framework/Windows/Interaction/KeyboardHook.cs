using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace NTH.Framework.Windows.Interaction
{
    public sealed class KeyboardHook : IDisposable
    {
        private readonly HotkeyWindowHost _window;
        private readonly Dictionary<int, Hotkey> _registeredKeys;

        public KeyboardHook()
        {
            _registeredKeys = new Dictionary<int, Hotkey>();
            _window = new HotkeyWindowHost();
            _window.KeyPressed += KeyPressed; // register the event of the inner native window.
        }

        /// <summary>Registers a hotkey in the system.</summary>
        public void RegisterHotkey(Hotkey hotkey)
        {
            if (hotkey == null)
                throw new ArgumentNullException("hotkey");

            int id = hotkey.GetHashCode();
            if (_registeredKeys.ContainsKey(id))
                throw new HotkeyRegistrationException(hotkey, new InvalidOperationException("Hotkey already registered."));

            try
            {
                _window.RegisterHotkey(hotkey.Modifiers, hotkey.Key, id);
            }
            catch (Win32Exception ex)
            {
                throw new HotkeyRegistrationException(hotkey, ex);
            }
            _registeredKeys.Add(id, hotkey);
        }

        /// <summary>Unregisters a hotkey in the system.</summary>
        public void UnregisterHotkey(Hotkey hotkey)
        {
            if (hotkey == null)
                throw new ArgumentNullException("hotkey");

            int id = hotkey.GetHashCode();

            Hotkey hk;
            if (!_registeredKeys.TryGetValue(id, out hk))
                throw new HotkeyRegistrationException(hotkey, new InvalidOperationException("Hotkey not registered."));

            try
            {
                _window.UnregisterHotkey(id);
            }
            catch (Win32Exception ex)
            {
                throw new HotkeyRegistrationException(hotkey, ex);
            }
            _registeredKeys.Remove(id);
        }

        public void UnregisterAllHotkeys()
        {
            // unregister all the registered hotkeys.
            foreach (var kv in _registeredKeys)
                NativeMethods.UnregisterHotKey(_window.Handle, kv.Key);
        }

        private void KeyPressed(object sender, KeyPressedEventArgs args)
        {
            Debug.Assert(args != null);

            var key = args.GetIdentifier();

            Hotkey hk;
            if (_registeredKeys.TryGetValue(key, out hk) && hk != null)
            {
                hk.InvokePressed(this);
            }
        }

        #region IDisposable Members

        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                UnregisterAllHotkeys();
                // dispose the inner native window.
                _window.Dispose();

                _disposed = true;
            }
        }

        ~KeyboardHook()
        {
            Dispose(false);
        }

        #endregion
    }
}
