using System;
using System.Windows.Media;

namespace MaterialDesignColors.ColorManipulation
{
    public static class ColorHelper
    {
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

        public static Color ShiftLightnesss(this Color color, int amount = 1)
        {
            var lab = color.ToLab();
            var shifted = new Lab(lab.L - LabConstants.Kn * amount, lab.A, lab.B);
            return shifted.ToColor();
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
