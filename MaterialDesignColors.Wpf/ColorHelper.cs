using System;
using System.Windows.Media;

namespace MaterialDesignColors
{
    public static class ColorHelper
    {
        public static Lab Rgb2Lab(Color c)
        {
            var xyz = Rgb2Xyz(c);
            return new Lab(116.0 * xyz.Y - 16.0, 500.0 * (xyz.X - xyz.Y), 200.0 * (xyz.Y - xyz.Z));
        }

        public static Xyz Rgb2Xyz(Color c)
        {
            double rgb_xyz(double d)
            {
                if ((d /= 255.0) <= 0.04045)
                    return d / 12.92;
                else
                    return Math.Pow((d + 0.055) / 1.055, 2.4);
            }

            double xyz_lab(double d)
            {
                if (d > LabConstants.t3)
                    return Math.Pow(d, 1.0 / 3.0);
                else
                    return d / LabConstants.t2 + LabConstants.t0;
            }

            var r = rgb_xyz(c.R);
            var g = rgb_xyz(c.G);
            var b = rgb_xyz(c.B);
            var x = xyz_lab((0.4124564 * r + 0.3575761 * g + 0.1804375 * b) / LabConstants.Xn);
            var y = xyz_lab((0.2126729 * r + 0.7151522 * g + 0.0721750 * b) / LabConstants.Yn);
            var z = xyz_lab((0.0193339 * r + 0.1191920 * g + 0.9503041 * b) / LabConstants.Zn);

            return new Xyz(x, y, z);
        }

        public static Xyz Lab2Xyz(Lab lab)
        {
            double lab_xyz(double d)
            {
                if (d > LabConstants.t1)
                    return d * d * d;
                else
                    return LabConstants.t2 * (d - LabConstants.t0);
            }

            var y = (lab.L + 16.0) / 116.0;
            var x = double.IsNaN(lab.A) ? y : y + lab.A / 500.0;
            var z = double.IsNaN(lab.B) ? y : y - lab.B / 200.0;

            y = LabConstants.Yn * lab_xyz(y);
            x = LabConstants.Xn * lab_xyz(x);
            z = LabConstants.Zn * lab_xyz(z);

            return new Xyz(x, y, z);
        }

        public static Color Lab2Rgb(Lab lab)
        {
            double xyz_rgb(double d)
            {
                if (d <= 0.00304)
                    return 255.0 * (12.92 * d);
                else
                    return 255.0 * (1.055 * Math.Pow(d, 1.0 / 2.4) - 0.055);
            }

            byte clip(double d)
            {
                if (d < 0) return 0;
                if (d > 255) return 255;
                return (byte)Math.Round(d);
            }
            var xyz = Lab2Xyz(lab);

            var r = xyz_rgb(3.2404542 * xyz.X - 1.5371385 * xyz.Y - 0.4985314 * xyz.Z);
            var g = xyz_rgb(-0.9692660 * xyz.X + 1.8760108 * xyz.Y + 0.0415560 * xyz.Z);
            var b = xyz_rgb(0.0556434 * xyz.X - 0.2040259 * xyz.Y + 1.0572252 * xyz.Z);

            return Color.FromRgb(clip(r), clip(g), clip(b));
        }

        public static Color ContrastingForeGroundColor(Color color)
        {
            double rgb_srgb(double d)
            {
                d = d / 255.0;
                return (d <= 0.03928)
                    ? d = d / 12.92
                    : d = Math.Pow((d + 0.055) / 1.055, 2.4);
            }
            var r = rgb_srgb(color.R);
            var g = rgb_srgb(color.G);
            var b = rgb_srgb(color.B);

            var luminance = 0.2126 * r + 0.7152 * g + 0.0722 * b;
            return luminance > 0.179 ? Colors.Black : Colors.White;
        }

        public static Color Hsv2Rgb(Hsv hsv)
        {
            var h = hsv.H;
            var s = hsv.S;
            var v = hsv.V;

            v *= 255;

            if (s == 0) return Color.FromRgb((byte)v, (byte)v, (byte)v);

            if (h == 360) h = 0;
            while (h > 360) h -= 360;
            while (h < 0) h += 360;

            h /= 60;

            var i = (int)Math.Floor(h);
            var f = h - i;
            var p = v * (1 - s);
            var q = v * (1 - s * f);
            var t = v * (1 - s * (1 - f));

            if (i == 0) return Color.FromRgb((byte)v, (byte)t, (byte)p);
            if (i == 1) return Color.FromRgb((byte)q, (byte)v, (byte)p);
            if (i == 2) return Color.FromRgb((byte)p, (byte)v, (byte)t);
            if (i == 3) return Color.FromRgb((byte)p, (byte)q, (byte)v);
            if (i == 4) return Color.FromRgb((byte)t, (byte)p, (byte)v);
            if (i == 5) return Color.FromRgb((byte)v, (byte)p, (byte)q);

            throw new Exception("Invalid HSV values");
        }

        public static Color ShiftLightnesss(this Color color, int amount = 1)
        {
            var lab = Rgb2Lab(color);
            var shifted = new Lab(lab.L - LabConstants.Kn * amount, lab.A, lab.B);
            return Lab2Rgb(shifted);
        }

        public static Color Darken(this Color color, int amount = 1)
        {
            return color.ShiftLightnesss(amount);
        }

        public static Color Lighten(this Color color, int amount = 1)
        {
            return color.ShiftLightnesss(-amount);
        }
    }
}
