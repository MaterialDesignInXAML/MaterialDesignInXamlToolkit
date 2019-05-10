using System;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public class ThemeChangedEventArgs : EventArgs
    {
        public ThemeChangedEventArgs(ResourceDictionary resourceDictionary, ITheme oldTheme, ITheme newTheme)
        {
            ResourceDictionary = resourceDictionary;
            OldTheme = oldTheme;
            NewTheme = newTheme;
        }

        public ResourceDictionary ResourceDictionary { get; }
        public ITheme NewTheme { get; }
        public ITheme OldTheme { get; }
    }
}