using System;
using System.Windows.Media;

namespace MaterialDesignColors.ColorManipulation
{
    internal static class XyzConverter
    {
        public static Color ToColor(this Xyz xyz)
        {
            double xyz_rgb(double d)
            {
                if (d > 0.0031308)
                    return 255.0 * (1.055 * Math.Pow(d, 1.0 / 2.4) - 0.055);
                else
                    return 255.0 * (12.92 * d);
            }

            byte clip(double d)
            {
                if (d < 0) return 0;
                if (d > 255) return 255;
                return (byte)Math.Round(d);
            }
            var r = xyz_rgb(3.2404542 * xyz.X - 1.5371385 * xyz.Y - 0.4985314 * xyz.Z);
            var g = xyz_rgb(-0.9692660 * xyz.X + 1.8760108 * xyz.Y + 0.0415560 * xyz.Z);
            var b = xyz_rgb(0.0556434 * xyz.X - 0.2040259 * xyz.Y + 1.0572252 * xyz.Z);

            return Color.FromRgb(clip(r), clip(g), clip(b));
        }
        public static Xyz ToXyz(this Color c)
        {
            double rgb_xyz(double v)
            {
                v /= 255;
                if (v > 0.04045)
                    return Math.Pow((v + 0.055) / 1.055, 2.4);
                else
                    return v / 12.92;
            }

            var r = rgb_xyz(c.R);
            var g = rgb_xyz(c.G);
            var b = rgb_xyz(c.B);

            var x = 0.4124564 * r + 0.3575761 * g + 0.1804375 * b;
            var y = 0.2126729 * r + 0.7151522 * g + 0.0721750 * b;
            var z = 0.0193339 * r + 0.1191920 * g + 0.9503041 * b;
            return new Xyz(x, y, z);
        }

        public static Xyz ToXyz(this Lab lab)
        {
            double lab_xyz(double d)
            {
                if (d > LabConstants.eCubedRoot)
                    return d * d * d;
                else
                    return (116 * d - 16) / LabConstants.k;
            }

            var y = (lab.L + 16.0) / 116.0;
            var x = double.IsNaN(lab.A) ? y : y + lab.A / 500.0;
            var z = double.IsNaN(lab.B) ? y : y - lab.B / 200.0;

            y = LabConstants.WhitePointY * lab_xyz(y);
            x = LabConstants.WhitePointX * lab_xyz(x);
            z = LabConstants.WhitePointZ * lab_xyz(z);

            return new Xyz(x, y, z);
        }

    }
}
