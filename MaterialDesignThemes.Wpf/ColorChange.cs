using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public class ColorChange
    {
        public string Name { get; }
        public Color Color { get; }

        public ColorChange(ColorName name, Color color) : this(name.ToString(), color) { }

        public ColorChange(string name, Color color)
        {
            Name = name;
            Color = color;
        }
    }
}
