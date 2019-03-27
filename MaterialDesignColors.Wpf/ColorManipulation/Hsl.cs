using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaterialDesignColors.ColorManipulation
{
    public class Hsl
    {
        public double H { get; }
        public double S { get; }
        public double L { get; }

        public Hsl(double h, double s, double l)
        {
            H = h;
            S = s;
            L = l;
        }
    }
}
