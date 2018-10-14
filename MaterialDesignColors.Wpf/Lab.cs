using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaterialDesignColors
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

        public const double Xn = 0.95047;
        public const double Yn = 1;
        public const double Zn = 1.08883;

        public const double t0 = 0.137931034;
        public const double t1 = 0.206896552;
        public const double t2 = 0.12841855;
        public const double t3 = 0.008856452;
    }
}
