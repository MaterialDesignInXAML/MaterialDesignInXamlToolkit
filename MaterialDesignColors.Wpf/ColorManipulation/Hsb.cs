namespace MaterialDesignColors.ColorManipulation
{
    public struct Hsb
    {
        public double Hue { get; }
        public double Saturation { get; }
        public double Brightness { get; }

        public Hsb(double hue, double saturation, double brightness)
        {
            Hue = hue;
            Saturation = saturation;
            Brightness = brightness;
        }
    }
}
