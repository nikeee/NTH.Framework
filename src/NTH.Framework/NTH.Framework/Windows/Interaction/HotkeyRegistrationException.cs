using System;

namespace NTH.Framework.Windows.Interaction
{
    public class HotkeyRegistrationException : Exception
    {
        public HotkeyRegistrationException(Hotkey hotkey, Exception innerException)
            : base("Failed to register hotkey " + hotkey + ".", innerException)
        { }
    }
}
