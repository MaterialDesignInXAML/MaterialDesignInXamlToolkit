using System;

namespace MaterialDesignThemes.Wpf
{
    public class PaletteChangedEventArgs : EventArgs
    {
        public PaletteChangedEventArgs(ColorPalette palette)
        {
            Palette = palette;
        }

        public ColorPalette Palette { get; }
    }
}