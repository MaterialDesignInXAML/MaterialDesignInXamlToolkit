using System;
using System.Windows.Media;

namespace MaterialDesignColors.ColorManipulation
{
    internal static class LabConverter
    {
        public static Lab ToLab(this Color c)
        {
            var xyz = c.ToXyz();
            return xyz.ToLab();
        }

        public static Lab ToLab(this Xyz xyz)
        {
            double xyz_lab(double v)
            {
                if (v > LabConstants.e)
                    return Math.Pow(v, 1 / 3.0);
                else
                    return (v * LabConstants.k + 16) / 116;
            }

            var fx = xyz_lab(xyz.X / LabConstants.WhitePointX);
            var fy = xyz_lab(xyz.Y / LabConstants.WhitePointY);
            var fz = xyz_lab(xyz.Z / LabConstants.WhitePointZ);

            var l = 116 * fy - 16;
            var a = 500 * (fx - fy);
            var b = 200 * (fy - fz);
            return new Lab(l, a, b);
        }

        public static Color ToColor(this Lab lab)
        {
            var xyz = lab.ToXyz();

            return xyz.ToColor();
        }
    }
}
