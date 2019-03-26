using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class RedSwatch : ISwatch
    {
        //TODO: Convert to static instances (Color.FromArgb) rather than parsing strings
        public static class Primary
        {
            public static Color Red50 { get; } = (Color)ColorConverter.ConvertFromString("#FFEBEE");
            public static Color Red100 { get; } = (Color)ColorConverter.ConvertFromString("#FFCDD2");
            public static Color Red200 { get; } = (Color)ColorConverter.ConvertFromString("#EF9A9A");
            public static Color Red300 { get; } = (Color)ColorConverter.ConvertFromString("#E57373");
            public static Color Red400 { get; } = (Color)ColorConverter.ConvertFromString("#EF5350");
            public static Color Red500 { get; } = (Color)ColorConverter.ConvertFromString("#F44336");
            public static Color Red600 { get; } = (Color)ColorConverter.ConvertFromString("#E53935");
            public static Color Red700 { get; } = (Color)ColorConverter.ConvertFromString("#D32F2F");
            public static Color Red800 { get; } = (Color)ColorConverter.ConvertFromString("#C62828");
            public static Color Red900 { get; } = (Color)ColorConverter.ConvertFromString("#B71C1C");

            public static Color Red50Foreground { get; } = (Color)ColorConverter.ConvertFromString("#DD000000");
            public static Color Red100Foreground { get; } = (Color)ColorConverter.ConvertFromString("#DD000000");
            public static Color Red200Foreground { get; } = (Color)ColorConverter.ConvertFromString("#DD000000");
            public static Color Red300Foreground { get; } = (Color)ColorConverter.ConvertFromString("#DD000000");
            public static Color Red400Foreground { get; } = (Color)ColorConverter.ConvertFromString("#DDFFFFFF");
            public static Color Red500Foreground { get; } = (Color)ColorConverter.ConvertFromString("#DDFFFFFF");
            public static Color Red600Foreground { get; } = (Color)ColorConverter.ConvertFromString("#DDFFFFFF");
            public static Color Red700Foreground { get; } = (Color)ColorConverter.ConvertFromString("#DDFFFFFF");
            public static Color Red800Foreground { get; } = (Color)ColorConverter.ConvertFromString("#DDFFFFFF");
            public static Color Red900Foreground { get; } = (Color)ColorConverter.ConvertFromString("#DDFFFFFF");
        }

        public static class Accent
        {
            public static Color Red100 { get; } = (Color)ColorConverter.ConvertFromString("#FF8A80");
            public static Color Red200 { get; } = (Color)ColorConverter.ConvertFromString("#FF5252");
            public static Color Red400 { get; } = (Color)ColorConverter.ConvertFromString("#FF1744");
            public static Color Red700 { get; } = (Color)ColorConverter.ConvertFromString("#D50000");


            public static Color Red100Foreground { get; } = (Color)ColorConverter.ConvertFromString("#DD000000");
            public static Color Red200Foreground { get; } = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
            public static Color Red400Foreground { get; } = (Color)ColorConverter.ConvertFromString("#DDFFFFFF");
            public static Color Red700Foreground { get; } = (Color)ColorConverter.ConvertFromString("#DDFFFFFF");
        }

        public string Name { get; } = "Red";

        public IEnumerable<Color> Hues
        {
            get
            {
                yield return Primary.Red50;
                yield return Primary.Red100;
                yield return Primary.Red200;
                yield return Primary.Red300;
                yield return Primary.Red400;
                yield return Primary.Red500;
                yield return Primary.Red600;
                yield return Primary.Red700;
                yield return Primary.Red800;
                yield return Primary.Red900;

                yield return Primary.Red50Foreground;
                yield return Primary.Red100Foreground;
                yield return Primary.Red200Foreground;
                yield return Primary.Red300Foreground;
                yield return Primary.Red400Foreground;
                yield return Primary.Red500Foreground;
                yield return Primary.Red600Foreground;
                yield return Primary.Red700Foreground;
                yield return Primary.Red800Foreground;
                yield return Primary.Red900Foreground;

                yield return Accent.Red100;
                yield return Accent.Red200;
                yield return Accent.Red400;
                yield return Accent.Red700;

                yield return Accent.Red100Foreground;
                yield return Accent.Red200Foreground;
                yield return Accent.Red400Foreground;
                yield return Accent.Red700Foreground;
            }
        }
    }
}
