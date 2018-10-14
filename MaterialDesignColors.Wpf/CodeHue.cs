using System;
using System.Windows.Media;

namespace MaterialDesignColors
{
    public class CodeHue
    {
        public string Name { get; }
        public string Interval { get; }
        public Color Color { get; }
        public string FullName => Name + Interval;

        public CodeHue(string name, string interval, string color)
        {
            Name = name;
            Interval = interval;
            Color = (Color) ColorConverter.ConvertFromString(color);
        }
    }
}
