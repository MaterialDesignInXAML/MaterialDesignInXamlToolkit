using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class TealSwatch : ISwatch
    {
        public static class Primary
        {
            public static Color Teal50 { get; } = (Color)ColorConverter.ConvertFromString("#E0F2F1");
            public static Color Teal100 { get; } = (Color)ColorConverter.ConvertFromString("#B2DFDB");
            public static Color Teal200 { get; } = (Color)ColorConverter.ConvertFromString("#80CBC4");
            public static Color Teal300 { get; } = (Color)ColorConverter.ConvertFromString("#4DB6AC");
            public static Color Teal400 { get; } = (Color)ColorConverter.ConvertFromString("#26A69A");
            public static Color Teal500 { get; } = (Color)ColorConverter.ConvertFromString("#009688");
            public static Color Teal600 { get; } = (Color)ColorConverter.ConvertFromString("#00897B");
            public static Color Teal700 { get; } = (Color)ColorConverter.ConvertFromString("#00796B");
            public static Color Teal800 { get; } = (Color)ColorConverter.ConvertFromString("#00695C");
            public static Color Teal900 { get; } = (Color)ColorConverter.ConvertFromString("#004D40");
        }

        public static class Accent
        {
            public static Color Teal100 { get; } = (Color)ColorConverter.ConvertFromString("#A7FFEB");
            public static Color Teal200 { get; } = (Color)ColorConverter.ConvertFromString("#64FFDA");
            public static Color Teal400 { get; } = (Color)ColorConverter.ConvertFromString("#1DE9B6");
            public static Color Teal700 { get; } = (Color)ColorConverter.ConvertFromString("#00BFA5");
        }


        public string Name { get; } = "Teal";

        public IEnumerable<Color> Hues
        {
            get
            {
                yield return Primary.Teal50;
                yield return Primary.Teal100;
                yield return Primary.Teal200;
                yield return Primary.Teal300;
                yield return Primary.Teal400;
                yield return Primary.Teal500;
                yield return Primary.Teal600;
                yield return Primary.Teal700;
                yield return Primary.Teal800;
                yield return Primary.Teal900;

                yield return Accent.Teal100;
                yield return Accent.Teal200;
                yield return Accent.Teal400;
                yield return Accent.Teal700;

            }
        }
    }
}
