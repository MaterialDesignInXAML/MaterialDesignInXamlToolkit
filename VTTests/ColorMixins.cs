using System;
using System.Windows.Media;
using Newtonsoft.Json.Serialization;

namespace VTTests
{
    public static class ColorMixins
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
        /// Based on https://www.w3.org/TR/2008/REC-WCAG20-20081211/#relativeluminancedef
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static float RelativeLuninance(this Color color)
        {
            return
                0.2126f * Calc(color.ScR) +
                0.7152f * Calc(color.ScG) +
                0.0722f * Calc(color.ScB);

            static float Calc(float colorValue)
                => colorValue <= 0.03928f ? colorValue / 12.92f : Square((colorValue + 0.055f) / 1.055f);

            static float Square(float value)
                => value * value;
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

        public static Color FlattenOnto(this Color foreground, Color background)
        {
            if (background.A == 0) return foreground;

            float alpha = foreground.ScA;
            float alphaReverse = 1 - alpha;

            float newAlpha = foreground.ScA + background.ScA * alphaReverse;
            return Color.FromArgb((byte)(newAlpha * byte.MaxValue),
                (byte)(alpha * foreground.R + alphaReverse * background.R),
                (byte)(alpha * foreground.G + alphaReverse * background.G),
                (byte)(alpha * foreground.B + alphaReverse * background.B)
            );
        }

        public static Color WithOpacity(this Color color, double opacity) 
            => Color.FromArgb((byte)(color.A * opacity), color.R, color.G, color.B);
    }
}
