using static System.Math;
#if NET462
using static System.MathCompat;
#endif

namespace MaterialColorUtilities;

/// <summary>
/// Viewing conditions used by CAM16/HCT; caches intermediate values for performance.
/// </summary>
public sealed class ViewingConditions
{
    /// <summary>
    /// sRGB-like viewing conditions.
    /// </summary>
    public static readonly ViewingConditions DEFAULT = DefaultWithBackgroundLstar(50.0);

    // Properties corresponding to Java getters
    public double Aw { get; }
    public double N { get; }
    public double Nbb { get; }
    internal double Ncb { get; }
    internal double C { get; }
    internal double Nc { get; }
    public double[] RgbD { get; }
    internal double Fl { get; }
    public double FlRoot { get; }
    internal double Z { get; }

    /// <summary>
    /// Create ViewingConditions from a simple, physically relevant set of parameters.
    /// </summary>
    public static ViewingConditions Make(
        double[] whitePoint,
        double adaptingLuminance,
        double backgroundLstar,
        double surround,
        bool discountingIlluminant)
    {
        // A background of pure black is non-physical and leads to infinities that represent the idea
        // that any color viewed in pure black can't be seen.
        backgroundLstar = Max(0.1, backgroundLstar);

        // Transform white point XYZ to 'cone'/'rgb' responses
        var matrix = Cam16.XYZ_TO_CAM16RGB;
        var xyz = whitePoint;
        double rW = (xyz[0] * matrix[0][0]) + (xyz[1] * matrix[0][1]) + (xyz[2] * matrix[0][2]);
        double gW = (xyz[0] * matrix[1][0]) + (xyz[1] * matrix[1][1]) + (xyz[2] * matrix[1][2]);
        double bW = (xyz[0] * matrix[2][0]) + (xyz[1] * matrix[2][1]) + (xyz[2] * matrix[2][2]);

        double f = 0.8 + (surround / 10.0);
        double c = (f >= 0.9)
            ? MathUtils.Lerp(0.59, 0.69, ((f - 0.9) * 10.0))
            : MathUtils.Lerp(0.525, 0.59, ((f - 0.8) * 10.0));

        double d = discountingIlluminant
            ? 1.0
            : f * (1.0 - ((1.0 / 3.6) * Exp((-adaptingLuminance - 42.0) / 92.0)));
        d = MathUtils.ClampDouble(0.0, 1.0, d);
        double nc = f;
        double[] rgbD =
        [
            d * (100.0 / rW) + 1.0 - d,
            d * (100.0 / gW) + 1.0 - d,
            d * (100.0 / bW) + 1.0 - d
        ];

        double k = 1.0 / (5.0 * adaptingLuminance + 1.0);
        double k4 = k * k * k * k;
        double k4F = 1.0 - k4;
        double fl = (k4 * adaptingLuminance) + (0.1 * k4F * k4F * Cbrt(5.0 * adaptingLuminance));

        double n = (ColorUtils.YFromLstar(backgroundLstar) / whitePoint[1]);
        double z = 1.48 + Sqrt(n);
        double nbb = 0.725 / Pow(n, 0.2);
        double ncb = nbb;
        double[] rgbAFactors =
        [
            Pow(fl * rgbD[0] * rW / 100.0, 0.42),
            Pow(fl * rgbD[1] * gW / 100.0, 0.42),
            Pow(fl * rgbD[2] * bW / 100.0, 0.42)
        ];

        double[] rgbA =
        [
            (400.0 * rgbAFactors[0]) / (rgbAFactors[0] + 27.13),
            (400.0 * rgbAFactors[1]) / (rgbAFactors[1] + 27.13),
            (400.0 * rgbAFactors[2]) / (rgbAFactors[2] + 27.13)
        ];

        double aw = ((2.0 * rgbA[0]) + rgbA[1] + (0.05 * rgbA[2])) * nbb;
        return new ViewingConditions(n, aw, nbb, ncb, c, nc, rgbD, fl, Pow(fl, 0.25), z);
    }

    /// <summary>
    /// Create sRGB-like viewing conditions with a custom background L*.
    /// </summary>
    public static ViewingConditions DefaultWithBackgroundLstar(double lstar)
    {
        return Make(
            ColorUtils.WhitePointD65(),
            (200.0 / PI * ColorUtils.YFromLstar(50.0) / 100.0),
            lstar,
            2.0,
            false);
    }

    /// <summary>
    /// Parameterized constructor for caching viewing condition intermediates.
    /// </summary>
    private ViewingConditions(
        double n,
        double aw,
        double nbb,
        double ncb,
        double c,
        double nc,
        double[] rgbD,
        double fl,
        double flRoot,
        double z)
    {
        N = n;
        Aw = aw;
        Nbb = nbb;
        Ncb = ncb;
        C = c;
        Nc = nc;
        RgbD = rgbD;
        Fl = fl;
        FlRoot = flRoot;
        Z = z;
    }
}
