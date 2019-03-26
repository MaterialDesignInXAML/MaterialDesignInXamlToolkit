using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class CyanSwatch : ISwatch
    {
        public static class Primary
        {
            public static Color Cyan50 { get; } = (Color)ColorConverter.ConvertFromString("#E0F7FA");
            public static Color Cyan100 { get; } = (Color)ColorConverter.ConvertFromString("#B2EBF2");
            public static Color Cyan200 { get; } = (Color)ColorConverter.ConvertFromString("#80DEEA");
            public static Color Cyan300 { get; } = (Color)ColorConverter.ConvertFromString("#4DD0E1");
            public static Color Cyan400 { get; } = (Color)ColorConverter.ConvertFromString("#26C6DA");
            public static Color Cyan500 { get; } = (Color)ColorConverter.ConvertFromString("#00BCD4");
            public static Color Cyan600 { get; } = (Color)ColorConverter.ConvertFromString("#00ACC1");
            public static Color Cyan700 { get; } = (Color)ColorConverter.ConvertFromString("#0097A7");
            public static Color Cyan800 { get; } = (Color)ColorConverter.ConvertFromString("#00838F");
            public static Color Cyan900 { get; } = (Color)ColorConverter.ConvertFromString("#006064");

        }

        public static class Accent
        {
            public static Color Cyan100 { get; } = (Color)ColorConverter.ConvertFromString("#84FFFF");
            public static Color Cyan200 { get; } = (Color)ColorConverter.ConvertFromString("#18FFFF");
            public static Color Cyan400 { get; } = (Color)ColorConverter.ConvertFromString("#00E5FF");
            public static Color Cyan700 { get; } = (Color)ColorConverter.ConvertFromString("#00B8D4");

        }

        public string Name { get; } = "Cyan";

        public IEnumerable<Color> Hues
        {
            get
            {
                yield return Primary.Cyan50;
                yield return Primary.Cyan100;
                yield return Primary.Cyan200;
                yield return Primary.Cyan300;
                yield return Primary.Cyan400;
                yield return Primary.Cyan500;
                yield return Primary.Cyan600;
                yield return Primary.Cyan700;
                yield return Primary.Cyan800;
                yield return Primary.Cyan900;

                yield return Accent.Cyan100;
                yield return Accent.Cyan200;
                yield return Accent.Cyan400;
                yield return Accent.Cyan700;
            }
        }
    }
}
