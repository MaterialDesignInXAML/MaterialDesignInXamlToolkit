using System;
using System.Windows.Media;

namespace MaterialDesignColors.ColorManipulation
{
    public static class ColorAssist
    {
        public static Color ContrastingForegroundColor(this Color color)
        {
            double rgb_srgb(double d)
            {
                d = d / 255.0;
                return (d > 0.03928)
                    ? d = Math.Pow((d + 0.055) / 1.055, 2.4)
                    : d = d / 12.92;
            }
            var r = rgb_srgb(color.R);
            var g = rgb_srgb(color.G);
            var b = rgb_srgb(color.B);

            var luminance = 0.2126 * r + 0.7152 * g + 0.0722 * b;
            return luminance > 0.179 ? Colors.Black : Colors.White;
        }

        public static Color ShiftLightness(this Color color, int amount = 1)
        {
            var lab = color.ToLab();
            var shifted = new Lab(lab.L - LabConstants.Kn * amount, lab.A, lab.B);
            return shifted.ToColor();
        }

        public static Color Darken(this Color color, int amount = 1)
        {
            return color.ShiftLightness(amount);
        }

        public static Color Lighten(this Color color, int amount = 1)
        {
            return color.ShiftLightness(-amount);
        }

        /// <summary>
        /// Calculates the CIE76 distance between two colors.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static double Difference(this Color color, Color other)
        {
            Lab lab1 = color.ToLab();
            Lab lab2 = color.ToLab();

            return Math.Sqrt(Math.Pow(lab2.L - lab1.L, 2) +
                             Math.Pow(lab2.A - lab1.A, 2) +
                             Math.Pow(lab2.B - lab1.B, 2));
        }
    }
}
