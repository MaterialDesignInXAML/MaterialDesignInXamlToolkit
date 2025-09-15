using static System.Math;

namespace MaterialColorUtilities;

/// <summary>
/// Color science for contrast utilities.
///
/// Utility methods for calculating contrast given two colors, or calculating a color given one
/// color and a contrast ratio.
///
/// Contrast ratio is calculated using XYZ's Y. When linearized to match human perception, Y
/// becomes HCT's tone and L*a*b*'s' L*.
/// </summary>
public static class Contrast
{
    // The minimum contrast ratio of two colors.
    // Contrast ratio equation = lighter + 5 / darker + 5, if lighter == darker, ratio == 1.
    public const double RatioMin = 1.0;

    // The maximum contrast ratio of two colors.
    // Contrast ratio equation = lighter + 5 / darker + 5. Lighter and darker scale from 0 to 100.
    // If lighter == 100, darker = 0, ratio == 21.
    public const double RatioMax = 21.0;

    public const double Ratio30 = 3.0;
    public const double Ratio45 = 4.5;
    public const double Ratio70 = 7.0;

    // Given a color and a contrast ratio to reach, the luminance of a color that reaches that ratio
    // with the color can be calculated. However, that luminance may not contrast as desired, i.e. the
    // contrast ratio of the input color and the returned luminance may not reach the contrast ratio
    // asked for.
    //
    // When the desired contrast ratio and the result contrast ratio differ by more than this amount,
    // an error value should be returned, or the method should be documented as 'unsafe', meaning,
    // it will return a valid luminance but that luminance may not meet the requested contrast ratio.
    //
    // 0.04 selected because it ensures the resulting ratio rounds to the same tenth.
    private const double ContrastRatioEpsilon = 0.04;

    // Color spaces that measure luminance, such as Y in XYZ, L* in L*a*b*, or T in HCT, are known as
    // perceptually accurate color spaces.
    //
    // To be displayed, they must gamut map to a "display space", one that has a defined limit on the
    // number of colors. Display spaces include sRGB, more commonly understood  as RGB/HSL/HSV/HSB.
    // Gamut mapping is undefined and not defined by the color space. Any gamut mapping algorithm must
    // choose how to sacrifice accuracy in hue, saturation, and/or lightness.
    //
    // A principled solution is to maintain lightness, thus maintaining contrast/a11y, maintain hue,
    // thus maintaining aesthetic intent, and reduce chroma until the color is in gamut.
    //
    // HCT chooses this solution, but, that doesn't mean it will _exactly_ matched desired lightness,
    // if only because RGB is quantized: RGB is expressed as a set of integers: there may be an RGB
    // color with, for example, 47.892 lightness, but not 47.891.
    //
    // To allow for this inherent incompatibility between perceptually accurate color spaces and
    // display color spaces, methods that take a contrast ratio and luminance, and return a luminance
    // that reaches that contrast ratio for the input luminance, purposefully darken/lighten their
    // result such that the desired contrast ratio will be reached even if inaccuracy is introduced.
    //
    // 0.4 is generous, ex. HCT requires much less delta. It was chosen because it provides a rough
    // guarantee that as long as a perceptual color space gamut maps lightness such that the resulting
    // lightness rounds to the same as the requested, the desired contrast ratio will be reached.
    private const double LuminanceGamutMapTolerance = 0.4;

    /// <summary>
    /// Contrast ratio is a measure of legibility, its used to compare the lightness of two colors.
    /// This method is used commonly in industry due to its use by WCAG.
    ///
    /// To compare lightness, the colors are expressed in the XYZ color space, where Y is lightness,
    /// also known as relative luminance.
    ///
    /// The equation is ratio = lighter Y + 5 / darker Y + 5.
    /// </summary>
    public static double RatioOfYs(double y1, double y2)
    {
        var lighter = y1 > y2 ? y1 : y2;
        var darker = (lighter == y2) ? y1 : y2;
        return (lighter + 5.0) / (darker + 5.0);
    }

    /// <summary>
    /// Contrast ratio of two tones. T in HCT, L* in L*a*b*. Also known as luminance or perceptual
    /// luminance.
    ///
    /// Contrast ratio is defined using Y in XYZ, relative luminance. However, relative luminance is
    /// linear to number of photons, not to perception of lightness. Perceptual luminance, L* in
    /// L*a*b*, T in HCT, is. Designers prefer color spaces with perceptual luminance since they're
    /// accurate to the eye.
    ///
    /// Y and L* are pure functions of each other, so it possible to use perceptually accurate color
    /// spaces, and measure contrast, and measure contrast in a much more understandable way: instead
    /// of a ratio, a linear difference. This allows a designer to determine what they need to adjust a
    /// color's lightness to in order to reach their desired contrast, instead of guessing &amp; checking
    /// with hex codes.
    /// </summary>
    public static double RatioOfTones(double toneA, double toneB)
    {
        return RatioOfYs(
            ColorUtils.YFromLstar(MathUtils.Clamp(0.0, 100.0, toneA)),
            ColorUtils.YFromLstar(MathUtils.Clamp(0.0, 100.0, toneB)));
    }

    /// <summary>
    /// Returns T in HCT, L* in L*a*b* &gt;= tone parameter that ensures ratio with input T/L*.
    /// Returns -1 if ratio cannot be achieved.
    /// </summary>
    /// <param name="tone">Tone return value must contrast with.</param>
    /// <param name="ratio">Desired contrast ratio of return value and tone parameter.</param>
    public static double Lighter(double tone, double ratio)
    {
        if (tone is < 0.0 or > 100.0)
        {
            return -1.0;
        }
        var darkY = ColorUtils.YFromLstar(tone);
        var lightY = ratio * (darkY + 5.0) - 5.0;
        if (lightY is < 0.0 or > 100.0)
        {
            return -1.0;
        }
        var realContrast = RatioOfYs(lightY, darkY);
        var delta = Abs(realContrast - ratio);
        if (realContrast < ratio && delta > ContrastRatioEpsilon)
        {
            return -1.0;
        }

        var returnValue = ColorUtils.LstarFromY(lightY) + LuminanceGamutMapTolerance;
        if (returnValue is < 0.0 or > 100.0)
        {
            return -1.0;
        }
        return returnValue;
    }

    /// <summary>
    /// Tone &gt;= tone parameter that ensures ratio. 100 if ratio cannot be achieved.
    ///
    /// This method is unsafe because the returned value is guaranteed to be in bounds, but, the in
    /// bounds return value may not reach the desired ratio.
    /// </summary>
    /// <param name="tone">Tone return value must contrast with.</param>
    /// <param name="ratio">Desired contrast ratio of return value and tone parameter.</param>
    public static double LighterUnsafe(double tone, double ratio)
    {
        var lighterSafe = Lighter(tone, ratio);
        return lighterSafe < 0.0 ? 100.0 : lighterSafe;
    }

    /// <summary>
    /// Returns T in HCT, L* in L*a*b* &lt;= tone parameter that ensures ratio with input T/L*.
    /// Returns -1 if ratio cannot be achieved.
    /// </summary>
    /// <param name="tone">Tone return value must contrast with.</param>
    /// <param name="ratio">Desired contrast ratio of return value and tone parameter.</param>
    public static double Darker(double tone, double ratio)
    {
        if (tone is < 0.0 or > 100.0)
        {
            return -1.0;
        }
        var lightY = ColorUtils.YFromLstar(tone);
        var darkY = ((lightY + 5.0) / ratio) - 5.0;
        if (darkY is < 0.0 or > 100.0)
        {
            return -1.0;
        }
        var realContrast = RatioOfYs(lightY, darkY);
        var delta = Abs(realContrast - ratio);
        if (realContrast < ratio && delta > ContrastRatioEpsilon)
        {
            return -1.0;
        }
        var returnValue = ColorUtils.LstarFromY(darkY) - LuminanceGamutMapTolerance;
        if (returnValue is < 0.0 or > 100.0)
        {
            return -1.0;
        }
        return returnValue;
    }

    /// <summary>
    /// Tone &lt;= tone parameter that ensures ratio. 0 if ratio cannot be achieved.
    ///
    /// This method is unsafe because the returned value is guaranteed to be in bounds, but, the in
    /// bounds return value may not reach the desired ratio.
    /// </summary>
    /// <param name="tone">Tone return value must contrast with.</param>
    /// <param name="ratio">Desired contrast ratio of return value and tone parameter.</param>
    public static double DarkerUnsafe(double tone, double ratio)
    {
        var darkerSafe = Darker(tone, ratio);
        return Max(0.0, darkerSafe);
    }
}
