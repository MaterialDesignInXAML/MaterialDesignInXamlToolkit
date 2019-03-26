using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class BrownSwatch : ISwatch
    {
        public static class Primary
        {
            public static Color Brown50 { get; } = (Color)ColorConverter.ConvertFromString("#EFEBE9");
            public static Color Brown100 { get; } = (Color)ColorConverter.ConvertFromString("#D7CCC8");
            public static Color Brown200 { get; } = (Color)ColorConverter.ConvertFromString("#BCAAA4");
            public static Color Brown300 { get; } = (Color)ColorConverter.ConvertFromString("#A1887F");
            public static Color Brown400 { get; } = (Color)ColorConverter.ConvertFromString("#8D6E63");
            public static Color Brown500 { get; } = (Color)ColorConverter.ConvertFromString("#795548");
            public static Color Brown600 { get; } = (Color)ColorConverter.ConvertFromString("#6D4C41");
            public static Color Brown700 { get; } = (Color)ColorConverter.ConvertFromString("#5D4037");
            public static Color Brown800 { get; } = (Color)ColorConverter.ConvertFromString("#4E342E");
            public static Color Brown900 { get; } = (Color)ColorConverter.ConvertFromString("#3E2723");
        }

        public string Name { get; } = "Brown";

        public IEnumerable<Color> Hues
        {
            get
            {
                yield return Primary.Brown50;
                yield return Primary.Brown100;
                yield return Primary.Brown200;
                yield return Primary.Brown300;
                yield return Primary.Brown400;
                yield return Primary.Brown500;
                yield return Primary.Brown600;
                yield return Primary.Brown700;
                yield return Primary.Brown800;
                yield return Primary.Brown900;
            }
        }
    }
}
