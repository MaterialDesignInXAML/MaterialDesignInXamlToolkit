using static System.Math;

namespace MaterialColorUtilities;

/// <summary>
/// An intermediate between a key color and a full color scheme: generates 5 tonal palettes varying chroma.
/// </summary>
[Obsolete("Use DynamicScheme for color scheme generation. Use CorePalettes for core palettes container class.")]
public sealed class CorePalette
{
    public TonalPalette a1 { get; }
    public TonalPalette a2 { get; }
    public TonalPalette a3 { get; }
    public TonalPalette n1 { get; }
    public TonalPalette n2 { get; }
    public TonalPalette error { get; }

    /// <summary>
    /// Create key palettes from a color.
    /// </summary>
    [Obsolete("Use DynamicScheme for color scheme generation. Use CorePalettes for core palettes container class.")]
    public static CorePalette Of(int argb)
    {
        return new CorePalette(argb, false);
    }

    /// <summary>
    /// Create content key palettes from a color.
    /// </summary>
    [Obsolete("Use DynamicScheme for color scheme generation. Use CorePalettes for core palettes container class.")]
    public static CorePalette ContentOf(int argb)
    {
        return new CorePalette(argb, true);
    }

    private CorePalette(int argb, bool isContent)
    {
        var hct = Hct.FromInt(argb);
        var hue = hct.Hue;
        var chroma = hct.Chroma;
        if (isContent)
        {
            a1 = TonalPalette.FromHueAndChroma(hue, chroma);
            a2 = TonalPalette.FromHueAndChroma(hue, chroma / 3.0);
            a3 = TonalPalette.FromHueAndChroma(hue + 60.0, chroma / 2.0);
            n1 = TonalPalette.FromHueAndChroma(hue, Min(chroma / 12.0, 4.0));
            n2 = TonalPalette.FromHueAndChroma(hue, Min(chroma / 6.0, 8.0));
        }
        else
        {
            a1 = TonalPalette.FromHueAndChroma(hue, Max(48.0, chroma));
            a2 = TonalPalette.FromHueAndChroma(hue, 16.0);
            a3 = TonalPalette.FromHueAndChroma(hue + 60.0, 24.0);
            n1 = TonalPalette.FromHueAndChroma(hue, 4.0);
            n2 = TonalPalette.FromHueAndChroma(hue, 8.0);
        }
        error = TonalPalette.FromHueAndChroma(25.0, 84.0);
    }
}