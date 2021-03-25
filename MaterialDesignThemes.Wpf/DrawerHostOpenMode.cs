using System;

namespace MaterialDesignThemes.Wpf
{
    public enum DrawerHostOpenMode
    {
        Default = 0,
        Modal = 0,
        [Obsolete("Use DrawerHostOpenMode.Modal instead; will be removed in future release")]
        Model = 0,
        Standard = 1
    }
}