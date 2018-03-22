using System;
using MaterialDesignColors;

namespace MaterialDesignThemes.Wpf
{
    public class PrimarySwatchChangedEventArgs : EventArgs
    {
        public PrimarySwatchChangedEventArgs(Swatch swatch, Hue light, Hue mid, Hue dark)
        {
            Swatch = swatch;
            Light = light;
            Mid = mid;
            Dark = dark;
        }

        public Swatch Swatch { get; }
        public Hue Light { get; }
        public Hue Mid { get; }
        public Hue Dark { get; }
    }
}