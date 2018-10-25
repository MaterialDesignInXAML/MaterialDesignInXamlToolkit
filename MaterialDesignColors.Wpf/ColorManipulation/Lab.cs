namespace MaterialDesignColors.ColorManipulation
{
    public class Lab
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

    public class LabConstants
    {
        public const double Kn = 18;

        public const double WhitePointX = 0.95047;
        public const double WhitePointY = 1;
        public const double WhitePointZ = 1.08883;

        public const double t1 = 0.206896552;
        public const double k = 24389/27.0;
        public const double e = 216/24389.0;
    }
}
