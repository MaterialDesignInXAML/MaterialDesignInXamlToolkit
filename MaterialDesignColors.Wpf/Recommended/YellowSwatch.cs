using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class YellowSwatch : ISwatch
    {
        public static class Primary
        {
            public static Color Yellow50 { get; } = (Color)ColorConverter.ConvertFromString("#FFFDE7");
            public static Color Yellow100 { get; } = (Color)ColorConverter.ConvertFromString("#FFF9C4");
            public static Color Yellow200 { get; } = (Color)ColorConverter.ConvertFromString("#FFF59D");
            public static Color Yellow300 { get; } = (Color)ColorConverter.ConvertFromString("#FFF176");
            public static Color Yellow400 { get; } = (Color)ColorConverter.ConvertFromString("#FFEE58");
            public static Color Yellow500 { get; } = (Color)ColorConverter.ConvertFromString("#FFEB3B");
            public static Color Yellow600 { get; } = (Color)ColorConverter.ConvertFromString("#FDD835");
            public static Color Yellow700 { get; } = (Color)ColorConverter.ConvertFromString("#FBC02D");
            public static Color Yellow800 { get; } = (Color)ColorConverter.ConvertFromString("#F9A825");
            public static Color Yellow900 { get; } = (Color)ColorConverter.ConvertFromString("#F57F17");

        }

        public static class Accent
        {
            public static Color Yellow100 { get; } = (Color)ColorConverter.ConvertFromString("#FFFF8D");
            public static Color Yellow200 { get; } = (Color)ColorConverter.ConvertFromString("#FFFF00");
            public static Color Yellow400 { get; } = (Color)ColorConverter.ConvertFromString("#FFEA00");
            public static Color Yellow700 { get; } = (Color)ColorConverter.ConvertFromString("#FFD600");

        }

        public string Name { get; } = "Yellow";

        public IEnumerable<Color> Hues
        {
            get
            {
                yield return Primary.Yellow50;
                yield return Primary.Yellow100;
                yield return Primary.Yellow200;
                yield return Primary.Yellow300;
                yield return Primary.Yellow400;
                yield return Primary.Yellow500;
                yield return Primary.Yellow600;
                yield return Primary.Yellow700;
                yield return Primary.Yellow800;
                yield return Primary.Yellow900;

                yield return Accent.Yellow100;
                yield return Accent.Yellow200;
                yield return Accent.Yellow400;
                yield return Accent.Yellow700;

            }
        }
    }
}
