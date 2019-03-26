using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class PurpleSwatch : ISwatch
    {
        public static class Primary
        {
            public static Color Purple50 { get; } = (Color)ColorConverter.ConvertFromString("#F3E5F5");
            public static Color Purple100 { get; } = (Color)ColorConverter.ConvertFromString("#E1BEE7");
            public static Color Purple200 { get; } = (Color)ColorConverter.ConvertFromString("#CE93D8");
            public static Color Purple300 { get; } = (Color)ColorConverter.ConvertFromString("#BA68C8");
            public static Color Purple400 { get; } = (Color)ColorConverter.ConvertFromString("#AB47BC");
            public static Color Purple500 { get; } = (Color)ColorConverter.ConvertFromString("#9C27B0");
            public static Color Purple600 { get; } = (Color)ColorConverter.ConvertFromString("#8E24AA");
            public static Color Purple700 { get; } = (Color)ColorConverter.ConvertFromString("#7B1FA2");
            public static Color Purple800 { get; } = (Color)ColorConverter.ConvertFromString("#6A1B9A");
            public static Color Purple900 { get; } = (Color)ColorConverter.ConvertFromString("#4A148C");

        }

        public static class Accent
        {
            public static Color Purple100 { get; } = (Color)ColorConverter.ConvertFromString("#EA80FC");
            public static Color Purple200 { get; } = (Color)ColorConverter.ConvertFromString("#E040FB");
            public static Color Purple400 { get; } = (Color)ColorConverter.ConvertFromString("#D500F9");
            public static Color Purple700 { get; } = (Color)ColorConverter.ConvertFromString("#AA00FF");

        }


        public string Name { get; } = "Purple";

        public IEnumerable<Color> Hues
        {
            get
            {
                yield return Primary.Purple50;
                yield return Primary.Purple100;
                yield return Primary.Purple200;
                yield return Primary.Purple300;
                yield return Primary.Purple400;
                yield return Primary.Purple500;
                yield return Primary.Purple600;
                yield return Primary.Purple700;
                yield return Primary.Purple800;
                yield return Primary.Purple900;

                yield return Accent.Purple100;
                yield return Accent.Purple200;
                yield return Accent.Purple400;
                yield return Accent.Purple700;
            }
        }
    }
}
