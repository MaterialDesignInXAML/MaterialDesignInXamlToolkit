using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class BlueSwatch : ISwatch
    {
        public static class Primary
        {
            public static Color Blue50 { get; } = (Color)ColorConverter.ConvertFromString("#E3F2FD");
            public static Color Blue100 { get; } = (Color)ColorConverter.ConvertFromString("#BBDEFB");
            public static Color Blue200 { get; } = (Color)ColorConverter.ConvertFromString("#90CAF9");
            public static Color Blue300 { get; } = (Color)ColorConverter.ConvertFromString("#64B5F6");
            public static Color Blue400 { get; } = (Color)ColorConverter.ConvertFromString("#42A5F5");
            public static Color Blue500 { get; } = (Color)ColorConverter.ConvertFromString("#2196F3");
            public static Color Blue600 { get; } = (Color)ColorConverter.ConvertFromString("#1E88E5");
            public static Color Blue700 { get; } = (Color)ColorConverter.ConvertFromString("#1976D2");
            public static Color Blue800 { get; } = (Color)ColorConverter.ConvertFromString("#1565C0");
            public static Color Blue900 { get; } = (Color)ColorConverter.ConvertFromString("#0D47A1");
        }

        public static class Accent
        {
            public static Color Blue100 { get; } = (Color)ColorConverter.ConvertFromString("#82B1FF");
            public static Color Blue200 { get; } = (Color)ColorConverter.ConvertFromString("#448AFF");
            public static Color Blue400 { get; } = (Color)ColorConverter.ConvertFromString("#2979FF");
            public static Color Blue700 { get; } = (Color)ColorConverter.ConvertFromString("#2962FF");
        }


        public string Name { get; } = "Blue";

        public IEnumerable<Color> Hues
        {
            get
            {
                yield return Primary.Blue50;
                yield return Primary.Blue100;
                yield return Primary.Blue200;
                yield return Primary.Blue300;
                yield return Primary.Blue400;
                yield return Primary.Blue500;
                yield return Primary.Blue600;
                yield return Primary.Blue700;
                yield return Primary.Blue800;
                yield return Primary.Blue900;

                yield return Accent.Blue100;
                yield return Accent.Blue200;
                yield return Accent.Blue400;
                yield return Accent.Blue700;
            }
        }
    }
}
