using System;

namespace MaterialDesignThemes.Wpf
{
    public class ThemeSetEventArgs : EventArgs
    {
        public ThemeSetEventArgs(BaseTheme theme)
        {
            Theme = theme;
        }

        public BaseTheme Theme { get; }
    }
}