using System;
using System.Windows.Media;

namespace MaterialDesignColors.ColorManipulation
{
    public static class HsvConverter
    {
        public static Color ToColor(this Hsv hsv)
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
    }
}
