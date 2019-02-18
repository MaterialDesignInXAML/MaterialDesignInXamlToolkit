using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf
{
    public class ColorPalette
    {
        public string Name { get; }
        public Color Light { get; }
        public Color Mid { get; }
        public Color Dark { get; }
        public Color LightForeground { get; }
        public Color MidForeground { get; }
        public Color DarkForeground { get; }

        public ColorPalette(PaletteName name, Color color) 
            : this(name.ToString(), color) { }

        public ColorPalette(string name, Color color) 
            : this(name, color.Lighten(), color, color.Darken()) { }

        public ColorPalette(PaletteName name, Color light, Color mid, Color dark) 
            : this(name.ToString(), light, mid, dark) { }

        public ColorPalette(string name, Color light, Color mid, Color dark)
            : this(name, light, mid, dark,
                  ColorHelper.ContrastingForeGroundColor(light),
                  ColorHelper.ContrastingForeGroundColor(mid),
                  ColorHelper.ContrastingForeGroundColor(dark)) { }

        public ColorPalette(PaletteName name, Color light, Color mid, Color dark, Color lightForeground, Color midForeground, Color darkForeground)
            : this(name.ToString(), light, mid, dark, lightForeground, midForeground, darkForeground) { }

        public ColorPalette(string name, Color light, Color mid, Color dark, Color lightForeground, Color midForeground, Color darkForeground)
        {
            Name = name;

            Light = light;
            Mid = mid;
            Dark = dark;

            LightForeground = lightForeground;
            MidForeground = midForeground;
            DarkForeground = darkForeground;
        }
    }
}
