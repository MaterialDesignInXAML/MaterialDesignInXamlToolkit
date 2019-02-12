using System;

namespace MaterialDesignThemes.Wpf
{
    public class ThemeSetEventArgs : EventArgs
    {
        public ThemeSetEventArgs(IBaseTheme theme)
        {
            Theme = theme;
        }

        public IBaseTheme Theme { get; }
    }
}