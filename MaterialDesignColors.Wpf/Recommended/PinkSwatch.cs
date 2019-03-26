using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class PinkSwatch : ISwatch
    {
        public static class Primary
        {
            public static Color Pink50 { get; } = (Color)ColorConverter.ConvertFromString("#FCE4EC");
            public static Color Pink100 { get; } = (Color)ColorConverter.ConvertFromString("#F8BBD0");
            public static Color Pink200 { get; } = (Color)ColorConverter.ConvertFromString("#F48FB1");
            public static Color Pink300 { get; } = (Color)ColorConverter.ConvertFromString("#F06292");
            public static Color Pink400 { get; } = (Color)ColorConverter.ConvertFromString("#EC407A");
            public static Color Pink500 { get; } = (Color)ColorConverter.ConvertFromString("#E91E63");
            public static Color Pink600 { get; } = (Color)ColorConverter.ConvertFromString("#D81B60");
            public static Color Pink700 { get; } = (Color)ColorConverter.ConvertFromString("#C2185B");
            public static Color Pink800 { get; } = (Color)ColorConverter.ConvertFromString("#AD1457");
            public static Color Pink900 { get; } = (Color)ColorConverter.ConvertFromString("#880E4F");
        }

        public static class Accent
        {
            public static Color Pink100 { get; } = (Color)ColorConverter.ConvertFromString("#FF80AB");
            public static Color Pink200 { get; } = (Color)ColorConverter.ConvertFromString("#FF4081");
            public static Color Pink400 { get; } = (Color)ColorConverter.ConvertFromString("#F50057");
            public static Color Pink700 { get; } = (Color)ColorConverter.ConvertFromString("#C51162");
        }

        public string Name { get; } = "Pink";

        public IEnumerable<Color> Hues
        {
            get
            {
                yield return Primary.Pink50;
                yield return Primary.Pink100;
                yield return Primary.Pink200;
                yield return Primary.Pink300;
                yield return Primary.Pink400;
                yield return Primary.Pink500;
                yield return Primary.Pink600;
                yield return Primary.Pink700;
                yield return Primary.Pink800;
                yield return Primary.Pink900;

                yield return Accent.Pink100;
                yield return Accent.Pink200;
                yield return Accent.Pink400;
                yield return Accent.Pink700;
            }
        }
    }
}
