using System;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public interface IPaletteHelper
    {
        event EventHandler<ThemeSetEventArgs> ThemeChanged;
        event EventHandler<PaletteChangedEventArgs> PaletteChanged;
        void SetTheme(IBaseTheme theme);
        void SetPrimaryPalette(Color color);
        void SetSecondaryPalette(Color color);
        void SetPalette(string name, Color color);
        void SetPrimaryForeground(Color color);
        void SetSecondaryForeground(Color color);
        void SetForegroundBrushes(string scheme, Color color);
        void SetPalette(ColorPalette palette);
    }
}
