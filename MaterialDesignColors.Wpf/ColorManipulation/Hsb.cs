namespace MaterialDesignColors.ColorManipulation
{
    public struct Hsb
    {
        public double H { get; }
        public double S { get; }
        public double B { get; }

        public Hsb(double h, double s, double b)
        {
            H = h;
            S = s;
            B = b;
        }
    }
}
