using System;
using System.Windows.Media;

namespace MaterialDesignColors
{
    public class CodeHue
    {
        public string Name { get; }
        public string Interval { get; }
        public Color Color { get; }

        public CodeHue(string name, string interval, string color)
        {
            Name = name;
            Interval = interval;
            Color = (Color) ColorConverter.ConvertFromString(color);
        }

        public Color ShiftLightnesss(int amount = 1)
        {

            var lab = Rgb2Lab(Color);
            var darker = new Lab(lab.L - LAB_CONSTANTS.Kn * amount, lab.A, lab.B);
            return Color;
        }

        public Color Darken(int amount)
        {
            return ShiftLightnesss(amount);
        }

        public Color Lighten(int amount)
        {
            return ShiftLightnesss(-amount);
        }

        public Lab Rgb2Lab(Color c)
        {
            var xyz = Rgb2Xyz(c);
            return new Lab(116 * xyz.Y - 16, 500 * (xyz.X - xyz.Y), 200 * (xyz.Y - xyz.Z));
        }

        public Xyz Rgb2Xyz(Color c)
        {
            var r = Rgb_Xyz(c.R);
            var g = Rgb_Xyz(c.G);
            var b = Rgb_Xyz(c.B);
            var x = Xyz_Lab((0.4124564 * r + 0.3575761 * g + 0.1804375 * b) / LAB_CONSTANTS.Xn);
            var y = Xyz_Lab((0.2126729 * r + 0.7151522 * g + 0.0721750 * b) / LAB_CONSTANTS.Yn);
            var z = Xyz_Lab((0.0193339 * r + 0.1191920 * g + 0.9503041 * b) / LAB_CONSTANTS.Zn);

            return new Xyz(x, y, z);
        }

        private double Rgb_Xyz(double r)
        {
            if ((r /= 255) <= 0.04045)
            {
                return r / 12.92;
            }
            else
            {
                return Math.Pow((r + 0.055) / 1.055, 2.4);
            }
        }

        private double Xyz_Lab(double t)
        {
            if (t > LAB_CONSTANTS.t3)
            {
                return Math.Pow(t, 1 / 3);
            }
            else
            {
                return t / LAB_CONSTANTS.t2 + LAB_CONSTANTS.t0;
            }
        }

        public class Xyz
        {
            public double X { get; }
            public double Y { get; }
            public double Z { get; }

            public Xyz(double x, double y, double z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }

        public class Lab
        {
            public double L { get; }
            public double A { get; }
            public double B { get; }

            public Lab(double l, double a, double b)
            {
                L = l;
                B = a;
                B = b;
            }
        }


        private class LAB_CONSTANTS
        {
            public const double Xn = 0.95047;
            public const double Yn = 1;
            public const double Zn = 1.08883;
            public const double Kn = 18;
            public const double t0 = 0.137931034;
            public const double t1 = 0.206896552;
            public const double t2 = 0.12841855;
            public const double t3 = 0.008856452;
        }

    }
}
