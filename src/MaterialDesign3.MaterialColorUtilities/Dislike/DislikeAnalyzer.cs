using static System.Math;

namespace MaterialColorUtilities;

/// <summary>
/// Check and/or fix universally disliked colors.
///
/// Color science studies of color preference indicate universal distaste for dark yellow-greens,
/// and also show this is correlated to distaste for biological waste and rotting food.
///
/// See Palmer and Schloss, 2010 or Schloss and Palmer's Chapter 21 in Handbook of Color
/// Psychology (2015).
/// </summary>
public static class DislikeAnalyzer
{
    /// <summary>
    /// Returns true if color is disliked.
    ///
    /// Disliked is defined as a dark yellow-green that is not neutral.
    /// </summary>
    public static bool IsDisliked(Hct hct)
    {
        var roundedHue = Round(hct.Hue);
        var roundedChroma = Round(hct.Chroma);
        var roundedTone = Round(hct.Tone);

        var huePasses = roundedHue is >= 90.0 and <= 111.0;
        var chromaPasses = roundedChroma > 16.0;
        var tonePasses = roundedTone < 65.0;

        return huePasses && chromaPasses && tonePasses;
    }

    /// <summary>
    /// If color is disliked, lighten it to make it likable.
    /// </summary>
    public static Hct FixIfDisliked(Hct hct)
    {
        return IsDisliked(hct)
            ? Hct.From(hct.Hue, hct.Chroma, 70.0)
            : hct;
    }
}