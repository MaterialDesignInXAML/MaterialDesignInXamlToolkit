using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaterialDesignColors
{
    public class Hsv
    {
        public double H { get; }
        public double S { get; }
        public double V { get; }

        public Hsv(double h, double s, double v)
        {
            H = h;
            S = s;
            V = v;
        }
    }
}
