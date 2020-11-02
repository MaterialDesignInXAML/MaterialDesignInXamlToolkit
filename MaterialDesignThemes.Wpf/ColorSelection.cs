using System;

namespace MaterialDesignThemes.Wpf
{
    [Flags]
    public enum ColorSelection
    {
        None = 0,
        Primary = 1,
        Secondary = 2,
        All = Primary | Secondary
    }
}