using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class LightBlueSwatch : ISwatch
    {
        public static class Primary
        {
            public static Color LightBlue50 { get; } = (Color)ColorConverter.ConvertFromString("#E1F5FE");
            public static Color LightBlue100 { get; } = (Color)ColorConverter.ConvertFromString("#B3E5FC");
            public static Color LightBlue200 { get; } = (Color)ColorConverter.ConvertFromString("#81D4FA");
            public static Color LightBlue300 { get; } = (Color)ColorConverter.ConvertFromString("#4FC3F7");
            public static Color LightBlue400 { get; } = (Color)ColorConverter.ConvertFromString("#29B6F6");
            public static Color LightBlue500 { get; } = (Color)ColorConverter.ConvertFromString("#03A9F4");
            public static Color LightBlue600 { get; } = (Color)ColorConverter.ConvertFromString("#039BE5");
            public static Color LightBlue700 { get; } = (Color)ColorConverter.ConvertFromString("#0288D1");
            public static Color LightBlue800 { get; } = (Color)ColorConverter.ConvertFromString("#0277BD");
            public static Color LightBlue900 { get; } = (Color)ColorConverter.ConvertFromString("#01579B");

        }

        public static class Accent
        {
            public static Color LightBlue100 { get; } = (Color)ColorConverter.ConvertFromString("#80D8FF");
            public static Color LightBlue200 { get; } = (Color)ColorConverter.ConvertFromString("#40C4FF");
            public static Color LightBlue400 { get; } = (Color)ColorConverter.ConvertFromString("#00B0FF");
            public static Color LightBlue700 { get; } = (Color)ColorConverter.ConvertFromString("#0091EA");

        }


        public string Name { get; } = "Light Blue";

        public IEnumerable<Color> Hues
        {
            get
            {
                yield return Primary.LightBlue50;
                yield return Primary.LightBlue100;
                yield return Primary.LightBlue200;
                yield return Primary.LightBlue300;
                yield return Primary.LightBlue400;
                yield return Primary.LightBlue500;
                yield return Primary.LightBlue600;
                yield return Primary.LightBlue700;
                yield return Primary.LightBlue800;
                yield return Primary.LightBlue900;

                yield return Accent.LightBlue100;
                yield return Accent.LightBlue200;
                yield return Accent.LightBlue400;
                yield return Accent.LightBlue700;
            }
        }
    }
}
