using System;
using System.Linq;
using System.Windows.Media;

namespace MaterialDesignColors.ColorManipulation
{
    public static class HsbConverter
    {
        public static Color ToColor(this Hsb hsv)
        {
            var h = hsv.H;
            var s = hsv.S;
            var b = hsv.B;

            b *= 255;

            if (s == 0) return Color.FromRgb((byte)b, (byte)b, (byte)b);

            if (h == 360) h = 0;
            while (h > 360) h -= 360;
            while (h < 0) h += 360;

            h /= 60;

            var i = (int)Math.Floor(h);
            var f = h - i;
            var p = b * (1 - s);
            var q = b * (1 - s * f);
            var t = b * (1 - s * (1 - f));

            if (i == 0) return Color.FromRgb((byte)b, (byte)t, (byte)p);
            if (i == 1) return Color.FromRgb((byte)q, (byte)b, (byte)p);
            if (i == 2) return Color.FromRgb((byte)p, (byte)b, (byte)t);
            if (i == 3) return Color.FromRgb((byte)p, (byte)q, (byte)b);
            if (i == 4) return Color.FromRgb((byte)t, (byte)p, (byte)b);
            if (i == 5) return Color.FromRgb((byte)b, (byte)p, (byte)q);

            throw new Exception("Invalid HSB values");
        }

        public static Hsb ToHsb(this Color color)
        {
            double r = color.R;
            double g = color.G;
            double b = color.B;

            r = r / 255;
            g = g / 255;
            b = b / 255;

            var rgb = new[] { r, g, b };
            var max = rgb.Max();
            var min = rgb.Min();
            double h, s, v;
            h = s = v = max;

            var d = max - min;
            s = max == 0 ? 0 : d / max;

            if (max == min)
            {
                h = 0; // achromatic
            }
            else
            {
                if (max == r)
                {
                    h = (g - b) / d + (g < b ? 6 : 0);
                }
                else if (max == g)
                {
                    h = (b - r) / d + 2;
                }
                else if (max == b)
                {
                    h = (r - g) / d + 4;
                }

                h *= 60;
            }

            return new Hsb(h, s, v);
        }
    }
}
