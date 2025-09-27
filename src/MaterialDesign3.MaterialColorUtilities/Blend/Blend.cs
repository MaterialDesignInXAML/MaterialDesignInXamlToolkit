using static System.Math;
using static MaterialColorUtilities.MathUtils;

namespace MaterialColorUtilities;

/// <summary>
/// Functions for blending in HCT and CAM16.
/// </summary>
public static class Blend
{
    /// <summary>
    /// Blend the design color's HCT hue towards the key color's HCT hue, in a way that
    /// leaves the original color recognizable and recognizably shifted towards the key color.
    /// </summary>
    /// <param name="designColor">ARGB representation of an arbitrary color.</param>
    /// <param name="sourceColor">ARGB representation of the main theme color.</param>
    /// <returns>
    /// The design color with a hue shifted towards the system's color, a slightly
    /// warmer/cooler variant of the design color's hue.
    /// </returns>
    public static int Harmonize(int designColor, int sourceColor)
    {
        var fromHct = Hct.FromInt(designColor);
        var toHct = Hct.FromInt(sourceColor);
        double differenceDegrees = DifferenceDegrees(fromHct.Hue, toHct.Hue);
        double rotationDegrees = Min(differenceDegrees * 0.5, 15.0);
        double outputHue = SanitizeDegreesDouble(
            fromHct.Hue + rotationDegrees * RotationDirection(fromHct.Hue, toHct.Hue));
        return Hct.From(outputHue, fromHct.Chroma, fromHct.Tone).Argb;
    }

    /// <summary>
    /// Blends hue from one color into another. The chroma and tone of the original color are maintained.
    /// </summary>
    /// <param name="from">ARGB representation of color.</param>
    /// <param name="to">ARGB representation of color.</param>
    /// <param name="amount">How much blending to perform; 0.0 &gt;= and &lt;= 1.0.</param>
    /// <returns>
    /// <paramref name="from"/> with a hue blended towards <paramref name="to"/>.
    /// Chroma and tone are constant.
    /// </returns>
    public static int HctHue(int from, int to, double amount)
    {
        int ucs = Cam16Ucs(from, to, amount);
        var ucsCam = Cam16.FromInt(ucs);
        var fromCam = Cam16.FromInt(from);
        var blended = Hct.From(ucsCam.GetHue(), fromCam.GetChroma(), ColorUtils.LstarFromArgb(from));
        return blended.Argb;
    }

    /// <summary>
    /// Blend in CAM16-UCS space.
    /// </summary>
    /// <param name="from">ARGB representation of color.</param>
    /// <param name="to">ARGB representation of color.</param>
    /// <param name="amount">How much blending to perform; 0.0 &gt;= and &lt;= 1.0.</param>
    /// <returns>
    /// <paramref name="from"/>, blended towards <paramref name="to"/>. Hue, chroma, and tone will change.
    /// </returns>
    public static int Cam16Ucs(int from, int to, double amount)
    {
        var fromCam = Cam16.FromInt(from);
        var toCam = Cam16.FromInt(to);
        double fromJ = fromCam.GetJstar();
        double fromA = fromCam.GetAstar();
        double fromB = fromCam.GetBstar();
        double toJ = toCam.GetJstar();
        double toA = toCam.GetAstar();
        double toB = toCam.GetBstar();
        double jstar = fromJ + (toJ - fromJ) * amount;
        double astar = fromA + (toA - fromA) * amount;
        double bstar = fromB + (toB - fromB) * amount;
        return Cam16.FromUcs(jstar, astar, bstar).ToInt();
    }
}
