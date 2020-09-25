using System;
using System.Windows.Markup.Localizer;
using System.Windows.Media;

namespace MaterialDesignColors.ColorManipulation
{
    public static class ColorAssist
    {
        /// <summary>
        /// The relative brightness of any point in a colorspace, normalized to 0 for darkest black and 1 for lightest white
        /// For the sRGB colorspace, the relative luminance of a color is defined as L = 0.2126 * R + 0.7152 * G + 0.0722 * B where R, G and B are defined as:
        /// if RsRGB <= 0.03928 then R = RsRGB / 12.92 else R = ((RsRGB+0.055)/1.055) ^ 2.4
        /// if GsRGB <= 0.03928 then G = GsRGB / 12.92 else G = ((GsRGB+0.055)/1.055) ^ 2.4
        /// if BsRGB <= 0.03928 then B = BsRGB / 12.92 else B = ((BsRGB+0.055)/1.055) ^ 2.4
        /// and RsRGB, GsRGB, and BsRGB are defined as:
        /// RsRGB = R8bit/255
        /// GsRGB = G8bit/255
        /// BsRGB = B8bit/255
        /// Based on https://www.w3.org/TR/WCAG21/#dfn-relative-luminance
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static float RelativeLuninance(this Color color)
        {
            return
                0.2126f * Calc(color.R / 255f) +
                0.7152f * Calc(color.G / 255f) +
                0.0722f * Calc(color.B / 255f);

            static float Calc(float colorValue)
                => colorValue <= 0.03928f ? colorValue / 12.92f : (float)Math.Pow((colorValue + 0.055f) / 1.055f, 2.4);
        }

        /// <summary>
        /// The contrast ratio is calculated as (L1 + 0.05) / (L2 + 0.05), where
        /// L1 is the: relative luminance of the lighter of the colors, and
        /// L2 is the relative luminance of the darker of the colors.
        /// Based on https://www.w3.org/TR/2008/REC-WCAG20-20081211/#contrast%20ratio
        /// </summary>
        /// <param name="color"></param>
        /// <param name="color2"></param>
        /// <returns></returns>
        public static float ContrastRatio(this Color color, Color color2)
        {
            float l1 = color.RelativeLuninance();
            float l2 = color2.RelativeLuninance();
            if (l2 > l1)
            {
                float temp = l1;
                l1 = l2;
                l2 = temp;
            }
            return (l1 + 0.05f) / (l2 + 0.05f);
        }

        public static Color EnsureContrastRatio(this Color foreground, Color background, float targetRatio, float tollerance = 0.1f)
            => EnsureContrastRatio(foreground, background, targetRatio, out _, tollerance);

        public static Color EnsureContrastRatio(this Color foreground, Color background, float targetRatio, out double offset, float tollerance = 0.1f)
        {
            offset = 0.0f;
            float ratio = foreground.ContrastRatio(background);
            if (ratio > targetRatio) return foreground;

            Color finalColor = foreground;
            double? adjust = null;

            while (Math.Abs(ratio - targetRatio) > tollerance &&
                   finalColor != Colors.White &&
                   finalColor != Colors.Black)
            {
                if (ratio - targetRatio < 0.0)
                {
                    if (adjust > 0)
                    {
                        adjust /= -2;
                    }
                    else if (adjust == null)
                    {
                        adjust = -1.0f;
                    }
                }
                else
                {
                    if (adjust < 0)
                    {
                        adjust /= -2;
                    }
                    else if (adjust == null)
                    {
                        adjust = 1.0f;
                    }
                }

                offset += adjust.Value;

                finalColor = foreground.ShiftLightness(offset);

                ratio = finalColor.ContrastRatio(background);
            }
            return finalColor;
        }

        public static Color ContrastingForegroundColor(this Color color)
        {
            double rgb_srgb(double d)
            {
                d = d / 255.0;
                return (d > 0.03928)
                    ? Math.Pow((d + 0.055) / 1.055, 2.4)
                    : d / 12.92;
            }
            var r = rgb_srgb(color.R);
            var g = rgb_srgb(color.G);
            var b = rgb_srgb(color.B);

            var luminance = 0.2126 * r + 0.7152 * g + 0.0722 * b;
            return luminance > 0.179 ? Colors.Black : Colors.White;
        }

        public static Color ShiftLightness(this Color color, double amount = 1.0f)
        {
            var lab = color.ToLab();
            var shifted = new Lab(lab.L - LabConstants.Kn * amount, lab.A, lab.B);
            return shifted.ToColor();
        }

        public static Color ShiftLightness(this Color color, int amount = 1)
        {
            var lab = color.ToLab();
            var shifted = new Lab(lab.L - LabConstants.Kn * amount, lab.A, lab.B);
            return shifted.ToColor();
        }

        public static Color Darken(this Color color, int amount = 1) => color.ShiftLightness(amount);

        public static Color Lighten(this Color color, int amount = 1) => color.ShiftLightness(-amount);

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
