using System;

namespace MaterialDesignColors.ColorManipulation
{
    internal struct Lab
    {
        public double L { get; }
        public double A { get; }
        public double B { get; }

        public Lab(double l, double a, double b)
        {
            L = l;
            A = a;
            B = b;
        }
    }

    internal class LabConstants
    {
        public const double Kn = 18;

        public const double WhitePointX = 0.95047;
        public const double WhitePointY = 1;
        public const double WhitePointZ = 1.08883;

        public static readonly double eCubedRoot = Math.Pow(e, 1.0 / 3);
        public const double k = 24389 / 27.0;
        public const double e = 216 / 24389.0;
    }
}
