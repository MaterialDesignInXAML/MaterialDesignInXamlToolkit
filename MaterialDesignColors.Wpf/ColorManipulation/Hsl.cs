namespace MaterialDesignColors.ColorManipulation
{
    internal struct Hsl
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
