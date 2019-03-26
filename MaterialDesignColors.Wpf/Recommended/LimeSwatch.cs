using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class LimeSwatch : ISwatch
    {
        public static class Primary
        {
            public static Color Lime50 { get; } = (Color)ColorConverter.ConvertFromString("#F9FBE7");
            public static Color Lime100 { get; } = (Color)ColorConverter.ConvertFromString("#F0F4C3");
            public static Color Lime200 { get; } = (Color)ColorConverter.ConvertFromString("#E6EE9C");
            public static Color Lime300 { get; } = (Color)ColorConverter.ConvertFromString("#DCE775");
            public static Color Lime400 { get; } = (Color)ColorConverter.ConvertFromString("#D4E157");
            public static Color Lime500 { get; } = (Color)ColorConverter.ConvertFromString("#CDDC39");
            public static Color Lime600 { get; } = (Color)ColorConverter.ConvertFromString("#C0CA33");
            public static Color Lime700 { get; } = (Color)ColorConverter.ConvertFromString("#AFB42B");
            public static Color Lime800 { get; } = (Color)ColorConverter.ConvertFromString("#9E9D24");
            public static Color Lime900 { get; } = (Color)ColorConverter.ConvertFromString("#827717");

        }

        public static class Accent
        {
            public static Color Lime100 { get; } = (Color)ColorConverter.ConvertFromString("#F4FF81");
            public static Color Lime200 { get; } = (Color)ColorConverter.ConvertFromString("#EEFF41");
            public static Color Lime400 { get; } = (Color)ColorConverter.ConvertFromString("#C6FF00");
            public static Color Lime700 { get; } = (Color)ColorConverter.ConvertFromString("#AEEA00");

        }

        public string Name { get; } = "Lime";

        public IEnumerable<Color> Hues
        {
            get
            {
                yield return Primary.Lime50;
                yield return Primary.Lime100;
                yield return Primary.Lime200;
                yield return Primary.Lime300;
                yield return Primary.Lime400;
                yield return Primary.Lime500;
                yield return Primary.Lime600;
                yield return Primary.Lime700;
                yield return Primary.Lime800;
                yield return Primary.Lime900;

                yield return Accent.Lime100;
                yield return Accent.Lime200;
                yield return Accent.Lime400;
                yield return Accent.Lime700;

            }
        }
    }
}
