using MaterialDesignColors;

namespace MaterialDesignThemes.Wpf;

/// <summary>
/// Provides full information about a palette.
/// </summary>
public class Palette
{
    public Palette(Swatch primarySwatch, Swatch secondarySwatch, int primaryLightHueIndex, int primaryMidHueIndex, int primaryDarkHueIndex, int secondaryHueIndex)
    {
        PrimarySwatch = primarySwatch;
        SecondarySwatch = secondarySwatch;
        PrimaryLightHueIndex = primaryLightHueIndex;
        PrimaryMidHueIndex = primaryMidHueIndex;
        PrimaryDarkHueIndex = primaryDarkHueIndex;
        SecondaryHueIndex = secondaryHueIndex;
    }

    public Swatch PrimarySwatch { get; }

    public Swatch SecondarySwatch { get; }

    public int PrimaryLightHueIndex { get; }

    public int PrimaryMidHueIndex { get; }

    public int PrimaryDarkHueIndex { get; }

    public int SecondaryHueIndex { get; }
}
