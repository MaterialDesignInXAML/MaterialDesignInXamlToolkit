using System.Windows.Media;

namespace MaterialDesignColors.ColorManipulation;

internal static class LabExtensions
{
    public static Lab ToLab(this Color c)
        => c.ToXyz().ToLab();

    public static Lab ToLab(this Xyz xyz)
    {
        double xyz_lab(double v)
        {
            if (v > LabConstants.e)
                return Math.Pow(v, 1 / 3.0);
            else
                return (v * LabConstants.k + 16) / 116;
        }

        double fx = xyz_lab(xyz.X / LabConstants.WhitePointX);
        double fy = xyz_lab(xyz.Y / LabConstants.WhitePointY);
        double fz = xyz_lab(xyz.Z / LabConstants.WhitePointZ);

        double l = 116 * fy - 16;
        double a = 500 * (fx - fy);
        double b = 200 * (fy - fz);
        return new Lab(l, a, b);
    }

    public static Color ToColor(this Lab lab)
        => lab.ToXyz().ToColor();
}
