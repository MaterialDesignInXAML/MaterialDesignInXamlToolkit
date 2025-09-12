namespace MaterialColorUtilities;

/// <summary>
/// Color science utilities and conversions not covered by HCT or CAM16.
/// </summary>
public static class ColorUtils
{
    internal static readonly double[][] SRGB_TO_XYZ =
    [
        [0.41233895, 0.35762064, 0.18051042],
        [0.2126, 0.7152, 0.0722],
        [0.01932141, 0.11916382, 0.95034478]
    ];

    internal static readonly double[][] XYZ_TO_SRGB =
    [
        [3.2413774792388685, -1.5376652402851851, -0.49885366846268053],
        [-0.9691452513005321, 1.8758853451067872, 0.04156585616912061],
        [0.05562093689691305, -0.20395524564742123, 1.0571799111220335]
    ];

    internal static readonly double[] WHITE_POINT_D65 = [95.047, 100.0, 108.883];

    /// <summary>
    /// Converts a color from RGB components to ARGB format.
    /// </summary>
    public static int ArgbFromRgb(int red, int green, int blue) => (255 << 24) | ((red & 255) << 16) | ((green & 255) << 8) | (blue & 255);

    /// <summary>
    /// Converts a color from linear RGB components to ARGB format.
    /// </summary>
    public static int ArgbFromLinrgb(double[] linrgb)
    {
        var r = Delinearized(linrgb[0]);
        var g = Delinearized(linrgb[1]);
        var b = Delinearized(linrgb[2]);
        return ArgbFromRgb(r, g, b);
    }

    /// <summary>
    /// Returns the alpha component of a color in ARGB format.
    /// </summary>
    public static int AlphaFromArgb(int argb) => (argb >> 24) & 255;

    /// <summary>
    /// Returns the red component of a color in ARGB format.
    /// </summary>
    public static int RedFromArgb(int argb) => (argb >> 16) & 255;

    /// <summary>
    /// Returns the green component of a color in ARGB format.
    /// </summary>
    public static int GreenFromArgb(int argb) => (argb >> 8) & 255;

    /// <summary>
    /// Returns the blue component of a color in ARGB format.
    /// </summary>
    public static int BlueFromArgb(int argb) => argb & 255;

    /// <summary>
    /// Returns whether a color in ARGB format is opaque.
    /// </summary>
    public static bool IsOpaque(int argb) => AlphaFromArgb(argb) >= 255;

    /// <summary>
    /// Converts a color from XYZ to ARGB.
    /// </summary>
    public static int ArgbFromXyz(double x, double y, double z)
    {
        var m = XYZ_TO_SRGB;
        var linearR = m[0][0] * x + m[0][1] * y + m[0][2] * z;
        var linearG = m[1][0] * x + m[1][1] * y + m[1][2] * z;
        var linearB = m[2][0] * x + m[2][1] * y + m[2][2] * z;
        var r = Delinearized(linearR);
        var g = Delinearized(linearG);
        var b = Delinearized(linearB);
        return ArgbFromRgb(r, g, b);
    }

    /// <summary>
    /// Converts a color from ARGB to XYZ.
    /// </summary>
    public static double[] XyzFromArgb(int argb)
    {
        var r = Linearized(RedFromArgb(argb));
        var g = Linearized(GreenFromArgb(argb));
        var b = Linearized(BlueFromArgb(argb));
        return MathUtils.MatrixMultiply([r, g, b], SRGB_TO_XYZ);
    }

    /// <summary>
    /// Converts a color represented in Lab color space into an ARGB integer.
    /// </summary>
    public static int ArgbFromLab(double l, double a, double b)
    {
        var fy = (l + 16.0) / 116.0;
        var fx = a / 500.0 + fy;
        var fz = fy - b / 200.0;
        var xNormalized = LabInvf(fx);
        var yNormalized = LabInvf(fy);
        var zNormalized = LabInvf(fz);
        var x = xNormalized * WHITE_POINT_D65[0];
        var y = yNormalized * WHITE_POINT_D65[1];
        var z = zNormalized * WHITE_POINT_D65[2];
        return ArgbFromXyz(x, y, z);
    }

    /// <summary>
    /// Converts a color from ARGB representation to L*a*b* representation.
    /// </summary>
    public static double[] LabFromArgb(int argb)
    {
        var linearR = Linearized(RedFromArgb(argb));
        var linearG = Linearized(GreenFromArgb(argb));
        var linearB = Linearized(BlueFromArgb(argb));
        var m = SRGB_TO_XYZ;
        var x = m[0][0] * linearR + m[0][1] * linearG + m[0][2] * linearB;
        var y = m[1][0] * linearR + m[1][1] * linearG + m[1][2] * linearB;
        var z = m[2][0] * linearR + m[2][1] * linearG + m[2][2] * linearB;
        var xNorm = x / WHITE_POINT_D65[0];
        var yNorm = y / WHITE_POINT_D65[1];
        var zNorm = z / WHITE_POINT_D65[2];
        var fx = LabF(xNorm);
        var fy = LabF(yNorm);
        var fz = LabF(zNorm);
        var l = 116.0 * fy - 16.0;
        var a = 500.0 * (fx - fy);
        var b = 200.0 * (fy - fz);
        return [l, a, b];
    }

    /// <summary>
    /// Converts an L* value to an ARGB representation.
    /// </summary>
    public static int ArgbFromLstar(double lstar)
    {
        var y = YFromLstar(lstar);
        var component = Delinearized(y);
        return ArgbFromRgb(component, component, component);
    }

    /// <summary>
    /// Computes the L* value of a color in ARGB representation.
    /// </summary>
    public static double LstarFromArgb(int argb)
    {
        var y = XyzFromArgb(argb)[1];
        return 116.0 * LabF(y / 100.0) - 16.0;
    }

    /// <summary>
    /// Converts an L* value to a Y value.
    /// </summary>
    public static double YFromLstar(double lstar) => 100.0 * LabInvf((lstar + 16.0) / 116.0);

    /// <summary>
    /// Converts a Y value to an L* value.
    /// </summary>
    public static double LstarFromY(double y) => LabF(y / 100.0) * 116.0 - 16.0;

    /// <summary>
    /// Linearizes an RGB component.
    /// </summary>
    public static double Linearized(int rgbComponent)
    {
        var normalized = rgbComponent / 255.0;
        if (normalized <= 0.040449936)
        {
            return normalized / 12.92 * 100.0;
        }

        return Math.Pow((normalized + 0.055) / 1.055, 2.4) * 100.0;
    }

    /// <summary>
    /// Delinearizes an RGB component.
    /// </summary>
    public static int Delinearized(double rgbComponent)
    {
        var normalized = rgbComponent / 100.0;
        double delinearized;
        if (normalized <= 0.0031308)
        {
            delinearized = normalized * 12.92;
        }
        else
        {
            delinearized = 1.055 * Math.Pow(normalized, 1.0 / 2.4) - 0.055;
        }
        return MathUtils.ClampInt(0, 255, (int)Math.Round(delinearized * 255.0));
    }

    /// <summary>
    /// Returns the standard white point; white on a sunny day.
    /// </summary>
    public static double[] WhitePointD65() => WHITE_POINT_D65;

    internal static double LabF(double t)
    {
        var e = 216.0 / 24389.0;
        var kappa = 24389.0 / 27.0;
        if (t > e)
        {
            return Math.Pow(t, 1.0 / 3.0);
        }

        return (kappa * t + 16.0) / 116.0;
    }

    internal static double LabInvf(double ft)
    {
        var e = 216.0 / 24389.0;
        var kappa = 24389.0 / 27.0;
        var ft3 = ft * ft * ft;
        if (ft3 > e)
        {
            return ft3;
        }

        return (116.0 * ft - 16.0) / kappa;
    }
}
