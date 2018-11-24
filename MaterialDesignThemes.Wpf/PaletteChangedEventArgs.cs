using System;

namespace MaterialDesignThemes.Wpf
{
    public class PaletteChangedEventArgs : EventArgs
    {
        public PaletteChangedEventArgs(Palette palette)
        {
            Palette = palette;
        }

        public Palette Palette { get; }
    }
}