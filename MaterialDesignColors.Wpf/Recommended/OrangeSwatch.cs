using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class OrangeSwatch : ISwatch
    {
        public static class Primary
        {
            public static Color Orange50 { get; } = (Color)ColorConverter.ConvertFromString("#FFF3E0");
            public static Color Orange100 { get; } = (Color)ColorConverter.ConvertFromString("#FFE0B2");
            public static Color Orange200 { get; } = (Color)ColorConverter.ConvertFromString("#FFCC80");
            public static Color Orange300 { get; } = (Color)ColorConverter.ConvertFromString("#FFB74D");
            public static Color Orange400 { get; } = (Color)ColorConverter.ConvertFromString("#FFA726");
            public static Color Orange500 { get; } = (Color)ColorConverter.ConvertFromString("#FF9800");
            public static Color Orange600 { get; } = (Color)ColorConverter.ConvertFromString("#FB8C00");
            public static Color Orange700 { get; } = (Color)ColorConverter.ConvertFromString("#F57C00");
            public static Color Orange800 { get; } = (Color)ColorConverter.ConvertFromString("#EF6C00");
            public static Color Orange900 { get; } = (Color)ColorConverter.ConvertFromString("#E65100");

        }

        public static class Accent
        {
            public static Color Orange100 { get; } = (Color)ColorConverter.ConvertFromString("#FFD180");
            public static Color Orange200 { get; } = (Color)ColorConverter.ConvertFromString("#FFAB40");
            public static Color Orange400 { get; } = (Color)ColorConverter.ConvertFromString("#FF9100");
            public static Color Orange700 { get; } = (Color)ColorConverter.ConvertFromString("#FF6D00");

        }

        public string Name { get; } = "Orange";

        public IEnumerable<Color> Hues
        {
            get
            {
                yield return Primary.Orange50;
                yield return Primary.Orange100;
                yield return Primary.Orange200;
                yield return Primary.Orange300;
                yield return Primary.Orange400;
                yield return Primary.Orange500;
                yield return Primary.Orange600;
                yield return Primary.Orange700;
                yield return Primary.Orange800;
                yield return Primary.Orange900;

                yield return Accent.Orange100;
                yield return Accent.Orange200;
                yield return Accent.Orange400;
                yield return Accent.Orange700;

            }
        }
    }
}
