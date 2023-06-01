namespace MaterialDesignColors.ColorManipulation;

internal record struct Lab(double L, double A, double B);

internal class LabConstants
{
    public const double Kn = 18;

    public const double WhitePointX = 0.95047;
    public const double WhitePointY = 1;
    public const double WhitePointZ = 1.08883;

    public static readonly double eCubedRoot = Math.Pow(e, 1.0 / 3);
    public const double k = 24389 / 27.0;
    public const double e = 216 / 24389.0;
}
