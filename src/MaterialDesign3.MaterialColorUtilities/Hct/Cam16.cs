using static System.Math;
using static MaterialColorUtilities.MathUtils;

namespace MaterialColorUtilities;

/// <summary>
/// CAM16, a color appearance model with CAM16-UCS coordinates for measuring distances.
/// </summary>
public sealed class Cam16
{
    /// <summary>
    /// Transforms XYZ color space coordinates to 'cone'/'RGB' responses in CAM16.
    /// </summary>
    internal static double[][] XYZ_TO_CAM16RGB =
    [
        [0.401288, 0.650173, -0.051461],
        [-0.250268, 1.204414, 0.045854],
        [-0.002079, 0.048952, 0.953127]
    ];

    /// <summary>
    /// Transforms 'cone'/'RGB' responses in CAM16 to XYZ color space coordinates.
    /// </summary>
    internal static double[][] CAM16RGB_TO_XYZ =
    [
        [1.8620678, -1.0112547, 0.14918678],
        [0.38752654, 0.62144744, -0.00897398],
        [-0.01584150, -0.03412294, 1.0499644]
    ];

    // CAM16 color dimensions
    public double Hue { get; private set; }
    public double Chroma { get; private set; }
    public double J { get; private set; }
    public double Q { get; private set; }
    public double M { get; private set; }
    public double S { get; private set; }

    // Coordinates in UCS space.
    public double Jstar { get; private set; }
    public double Astar { get; private set; }
    public double Bstar { get; private set; }

    // Avoid allocations during conversion by pre-allocating an array.
    private readonly double[] _tempArray = [0.0, 0.0, 0.0];

    /// <summary>
    /// CAM16-UCS distance between this color and another.
    /// </summary>
    public double Distance(Cam16 other)
    {
        double dJ = Jstar - other.Jstar;
        double dA = Astar - other.Astar;
        double dB = Bstar - other.Bstar;
        double dEPrime = Sqrt(dJ * dJ + dA * dA + dB * dB);
        double dE = 1.41 * Pow(dEPrime, 0.63);
        return dE;
    }

    /// <summary>
    /// Hue in CAM16.
    /// </summary>
    public double GetHue() => Hue;

    /// <summary>
    /// Chroma in CAM16.
    /// </summary>
    public double GetChroma() => Chroma;

    /// <summary>
    /// Lightness in CAM16.
    /// </summary>
    public double GetJ() => J;

    /// <summary>
    /// Brightness in CAM16. Prefer lightness.
    /// </summary>
    public double GetQ() => Q;

    /// <summary>
    /// Colorfulness in CAM16. Prefer chroma.
    /// </summary>
    public double GetM() => M;

    /// <summary>
    /// Saturation in CAM16. Prefer chroma.
    /// </summary>
    public double GetS() => S;

    /// <summary>
    /// Lightness coordinate in CAM16-UCS.
    /// </summary>
    public double GetJstar() => Jstar;

    /// <summary>
    /// a* coordinate in CAM16-UCS.
    /// </summary>
    public double GetAstar() => Astar;

    /// <summary>
    /// b* coordinate in CAM16-UCS.
    /// </summary>
    public double GetBstar() => Bstar;

    /// <summary>
    /// Constructor that sets all CAM16 and CAM16-UCS dimensions.
    /// </summary>
    private Cam16(
        double hue,
        double chroma,
        double j,
        double q,
        double m,
        double s,
        double jstar,
        double astar,
        double bstar)
    {
        Hue = hue;
        Chroma = chroma;
        J = j;
        Q = q;
        M = m;
        S = s;
        Jstar = jstar;
        Astar = astar;
        Bstar = bstar;
    }

    /// <summary>
    /// Create a CAM16 color from an ARGB color in default viewing conditions.
    /// </summary>
    public static Cam16 FromInt(int argb) => FromIntInViewingConditions(argb, ViewingConditions.DEFAULT);

    /// <summary>
    /// Create a CAM16 color from an ARGB color in defined viewing conditions.
    /// </summary>
    internal static Cam16 FromIntInViewingConditions(int argb, ViewingConditions viewingConditions)
    {
        // Transform ARGB int to XYZ
        int red = (argb & 0x00ff0000) >> 16;
        int green = (argb & 0x0000ff00) >> 8;
        int blue = (argb & 0x000000ff);
        double redL = ColorUtils.Linearized(red);
        double greenL = ColorUtils.Linearized(green);
        double blueL = ColorUtils.Linearized(blue);
        double x = 0.41233895 * redL + 0.35762064 * greenL + 0.18051042 * blueL;
        double y = 0.2126 * redL + 0.7152 * greenL + 0.0722 * blueL;
        double z = 0.01932141 * redL + 0.11916382 * greenL + 0.95034478 * blueL;

        return FromXyzInViewingConditions(x, y, z, viewingConditions);
    }

    internal static Cam16 FromXyzInViewingConditions(
        double x, double y, double z, ViewingConditions viewingConditions)
    {
        // Transform XYZ to 'cone'/'rgb' responses
        double[][] matrix = XYZ_TO_CAM16RGB;
        double rT = (x * matrix[0][0]) + (y * matrix[0][1]) + (z * matrix[0][2]);
        double gT = (x * matrix[1][0]) + (y * matrix[1][1]) + (z * matrix[1][2]);
        double bT = (x * matrix[2][0]) + (y * matrix[2][1]) + (z * matrix[2][2]);

        // Discount illuminant
        double rD = viewingConditions.RgbD[0] * rT;
        double gD = viewingConditions.RgbD[1] * gT;
        double bD = viewingConditions.RgbD[2] * bT;

        // Chromatic adaptation
        double rAF = Pow(viewingConditions.Fl * Abs(rD) / 100.0, 0.42);
        double gAF = Pow(viewingConditions.Fl * Abs(gD) / 100.0, 0.42);
        double bAF = Pow(viewingConditions.Fl * Abs(bD) / 100.0, 0.42);
        double rA = Sign(rD) * 400.0 * rAF / (rAF + 27.13);
        double gA = Sign(gD) * 400.0 * gAF / (gAF + 27.13);
        double bA = Sign(bD) * 400.0 * bAF / (bAF + 27.13);

        // redness-greenness
        double a = (11.0 * rA + -12.0 * gA + bA) / 11.0;
        // yellowness-blueness
        double bb = (rA + gA - 2.0 * bA) / 9.0;

        // auxiliary components
        double u = (20.0 * rA + 20.0 * gA + 21.0 * bA) / 20.0;
        double p2 = (40.0 * rA + 20.0 * gA + bA) / 20.0;

        // hue
        double atan2 = Atan2(bb, a);
        double atanDegrees = atan2 * (180.0 / PI);
        double hue =
            atanDegrees < 0
                ? atanDegrees + 360.0
                : atanDegrees >= 360 ? atanDegrees - 360.0 : atanDegrees;
        double hueRadians = hue * (PI / 180.0);

        // achromatic response to color
        double ac = p2 * viewingConditions.Nbb;

        // CAM16 lightness and brightness
        double j =
            100.0
                * Pow(
                    ac / viewingConditions.Aw,
                    viewingConditions.C * viewingConditions.Z);
        double q =
            4.0
                / viewingConditions.C
                * Sqrt(j / 100.0)
                * (viewingConditions.Aw + 4.0)
                * viewingConditions.FlRoot;

        // CAM16 chroma, colorfulness, and saturation.
        double huePrime = (hue < 20.14) ? hue + 360 : hue;
        double eHue = 0.25 * (Cos(huePrime * (PI / 180.0) + 2.0) + 3.8);
        double p1 = 50000.0 / 13.0 * eHue * viewingConditions.Nc * viewingConditions.Ncb;
        double t = p1 * Hypot(a, bb) / (u + 0.305);
        double alpha =
            Pow(1.64 - Pow(0.29, viewingConditions.N), 0.73) * Pow(t, 0.9);
        // CAM16 chroma, colorfulness, saturation
        double c = alpha * Sqrt(j / 100.0);
        double m = c * viewingConditions.FlRoot;
        double s =
            50.0 * Sqrt((alpha * viewingConditions.C) / (viewingConditions.Aw + 4.0));

        // CAM16-UCS components
        double jstar = (1.0 + 100.0 * 0.007) * j / (1.0 + 0.007 * j);
        double mstar = 1.0 / 0.0228 * Log1p(0.0228 * m);
        double astar = mstar * Cos(hueRadians);
        double bstar = mstar * Sin(hueRadians);

        return new Cam16(hue, c, j, q, m, s, jstar, astar, bstar);
    }

    /// <summary>
    /// Create CAM16 from J, C, and H.
    /// </summary>
    internal static Cam16 FromJch(double j, double c, double h) => FromJchInViewingConditions(j, c, h, ViewingConditions.DEFAULT);

    /// <summary>
    /// Create CAM16 from J, C, H and viewing conditions.
    /// </summary>
    private static Cam16 FromJchInViewingConditions(double j, double c, double h, ViewingConditions viewingConditions)
    {
        double q =
            4.0
                / viewingConditions.C
                * Sqrt(j / 100.0)
                * (viewingConditions.Aw + 4.0)
                * viewingConditions.FlRoot;
        double m = c * viewingConditions.FlRoot;
        double alpha = c / Sqrt(j / 100.0);
        double s =
            50.0 * Sqrt((alpha * viewingConditions.C) / (viewingConditions.Aw + 4.0));

        double hueRadians = h * (PI / 180.0);
        double jstar = (1.0 + 100.0 * 0.007) * j / (1.0 + 0.007 * j);
        double mstar = 1.0 / 0.0228 * Log1p(0.0228 * m);
        double astar = mstar * Cos(hueRadians);
        double bstar = mstar * Sin(hueRadians);
        return new Cam16(h, c, j, q, m, s, jstar, astar, bstar);
    }

    /// <summary>
    /// Create CAM16 from CAM16-UCS coordinates in default viewing conditions.
    /// </summary>
    public static Cam16 FromUcs(double jstar, double astar, double bstar) => FromUcsInViewingConditions(jstar, astar, bstar, ViewingConditions.DEFAULT);

    /// <summary>
    /// Create CAM16 from CAM16-UCS coordinates in defined viewing conditions.
    /// </summary>
    public static Cam16 FromUcsInViewingConditions(double jstar, double astar, double bstar, ViewingConditions viewingConditions)
    {
        double m = Hypot(astar, bstar);
        double m2 = Expm1(m * 0.0228) / 0.0228;
        double c = m2 / viewingConditions.FlRoot;
        double h = Atan2(bstar, astar) * (180.0 / PI);
        if (h < 0.0)
        {
            h += 360.0;
        }
        double j = jstar / (1.0 - (jstar - 100.0) * 0.007);
        return FromJchInViewingConditions(j, c, h, viewingConditions);
    }

    /// <summary>
    /// ARGB representation of this color in default viewing conditions.
    /// </summary>
    public int ToInt() => Viewed(ViewingConditions.DEFAULT);

    /// <summary>
    /// ARGB representation of this color in specified viewing conditions.
    /// </summary>
    internal int Viewed(ViewingConditions viewingConditions)
    {
        double[] xyz = XyzInViewingConditions(viewingConditions, _tempArray);
        return ColorUtils.ArgbFromXyz(xyz[0], xyz[1], xyz[2]);
    }

    public double[] XyzInViewingConditions(ViewingConditions viewingConditions, double[]? returnArray = null)
    {
        double alpha =
            (Chroma == 0.0 || J == 0.0) ? 0.0 : Chroma / Sqrt(J / 100.0);

        double t =
            Pow(
                alpha / Pow(1.64 - Pow(0.29, viewingConditions.N), 0.73), 1.0 / 0.9);
        double hRad = Hue * (PI / 180.0);

        double eHue = 0.25 * (Cos(hRad + 2.0) + 3.8);
        double ac =
            viewingConditions.Aw
                * Pow(J / 100.0, 1.0 / viewingConditions.C / viewingConditions.Z);
        double p1 = eHue * (50000.0 / 13.0) * viewingConditions.Nc * viewingConditions.Ncb;
        double p2 = (ac / viewingConditions.Nbb);

        double hSin = Sin(hRad);
        double hCos = Cos(hRad);

        double gamma = 23.0 * (p2 + 0.305) * t / (23.0 * p1 + 11.0 * t * hCos + 108.0 * t * hSin);
        double a = gamma * hCos;
        double b = gamma * hSin;
        double rA = (460.0 * p2 + 451.0 * a + 288.0 * b) / 1403.0;
        double gA = (460.0 * p2 - 891.0 * a - 261.0 * b) / 1403.0;
        double bA = (460.0 * p2 - 220.0 * a - 6300.0 * b) / 1403.0;

        double rCBase = Max(0, (27.13 * Abs(rA)) / (400.0 - Abs(rA)));
        double rC =
            Sign(rA) * (100.0 / viewingConditions.Fl) * Pow(rCBase, 1.0 / 0.42);
        double gCBase = Max(0, (27.13 * Abs(gA)) / (400.0 - Abs(gA)));
        double gC =
            Sign(gA) * (100.0 / viewingConditions.Fl) * Pow(gCBase, 1.0 / 0.42);
        double bCBase = Max(0, (27.13 * Abs(bA)) / (400.0 - Abs(bA)));
        double bC =
            Sign(bA) * (100.0 / viewingConditions.Fl) * Pow(bCBase, 1.0 / 0.42);
        double rF = rC / viewingConditions.RgbD[0];
        double gF = gC / viewingConditions.RgbD[1];
        double bF = bC / viewingConditions.RgbD[2];

        double[][] matrix = CAM16RGB_TO_XYZ;
        double x = (rF * matrix[0][0]) + (gF * matrix[0][1]) + (bF * matrix[0][2]);
        double y = (rF * matrix[1][0]) + (gF * matrix[1][1]) + (bF * matrix[1][2]);
        double z = (rF * matrix[2][0]) + (gF * matrix[2][1]) + (bF * matrix[2][2]);

        if (returnArray is { Length: 3 })
        {
            returnArray[0] = x;
            returnArray[1] = y;
            returnArray[2] = z;
            return returnArray;
        }

        return [x, y, z];
    }
}
