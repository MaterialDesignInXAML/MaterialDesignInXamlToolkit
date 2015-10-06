using System;
using System.Windows.Media;

namespace MaterialDesignColors
{
    public class Hue
    {
        public Hue(string name, Color color, Color foreground)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            Name = name;
            Color = color;
            Foreground = foreground;
        }

        public string Name { get; }

        public Color Color { get; }

        public Color Foreground { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}