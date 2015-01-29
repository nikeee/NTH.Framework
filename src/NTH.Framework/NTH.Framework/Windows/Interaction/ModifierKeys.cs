using System;

namespace NTH.Framework.Windows.Interaction
{
    /// <summary>The enumeration of possible modifiers.</summary>
    [Flags]
    public enum ModifierKeys : uint
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Win = 8
    }
}
