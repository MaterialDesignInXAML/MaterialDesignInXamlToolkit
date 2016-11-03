using MaterialDesignColors;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Provides full information about a palette.
    /// </summary>
    public class Palette
    {
        public Palette(Swatch primarySwatch, Swatch accentSwatch, int primaryLightHueIndex, int primaryMidHueIndex, int primaryDarkHueIndex, int accentHueIndex)
        {
            PrimarySwatch = primarySwatch;
            AccentSwatch = accentSwatch;
            PrimaryLightHueIndex = primaryLightHueIndex;
            PrimaryMidHueIndex = primaryMidHueIndex;
            PrimaryDarkHueIndex = primaryDarkHueIndex;
            AccentHueIndex = accentHueIndex;
        }

        public Swatch PrimarySwatch { get; }

        public Swatch AccentSwatch { get; }

        public int PrimaryLightHueIndex { get; }

        public int PrimaryMidHueIndex { get; }

        public int PrimaryDarkHueIndex { get; }

        public int AccentHueIndex { get; }
    }
}