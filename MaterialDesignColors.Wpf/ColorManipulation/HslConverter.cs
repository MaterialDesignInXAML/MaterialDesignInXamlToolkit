using System;
using System.Windows.Media;

namespace MaterialDesignColors.ColorManipulation
{
    internal static class HslConverter
    {
        public static Color ToColor(this Hsl hsl)
        {
            double hsv_rbg(double v1, double v2, double vH)
            {
                if (vH < 0) vH += 1;
                if (vH > 1) vH -= 1;
                if ((6 * vH) < 1) return (v1 + (v2 - v1) * 6 * vH);
                if ((2 * vH) < 1) return (v2);
                if ((3 * vH) < 2) return (v1 + (v2 - v1) * ((2.0 / 3) - vH) * 6);
                return (v1);
            }

            var h = hsl.H * (1.0 / 360);
            var s = hsl.S * (1.0 / 100);
            var l = hsl.L * (1.0 / 100);

            double r, g, b;
            if (s == 0)
            {
                r = l * 255;
                g = l * 255;
                b = l * 255;
}
            else
            {
                double var_2;
                if (l < 0.5) var_2 = l * (1 + s);
                else var_2 = (l + s) - (s * l);

                var var_1 = 2 * l - var_2;

                r = 255 * hsv_rbg(var_1, var_2, h + (1.0 / 3));
                g = 255 * hsv_rbg(var_1, var_2, h);
                b = 255 * hsv_rbg(var_1, var_2, h - (1.0 / 3));
            }

            return Color.FromRgb((byte)Math.Round(r), (byte)Math.Round(g), (byte)Math.Round(b));
        }
    }
}
