using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class IndigoSwatch : ISwatch
    {
        public static class Primary
        {
            public static Color Indigo50 { get; } = (Color)ColorConverter.ConvertFromString("#E8EAF6");
            public static Color Indigo100 { get; } = (Color)ColorConverter.ConvertFromString("#C5CAE9");
            public static Color Indigo200 { get; } = (Color)ColorConverter.ConvertFromString("#9FA8DA");
            public static Color Indigo300 { get; } = (Color)ColorConverter.ConvertFromString("#7986CB");
            public static Color Indigo400 { get; } = (Color)ColorConverter.ConvertFromString("#5C6BC0");
            public static Color Indigo500 { get; } = (Color)ColorConverter.ConvertFromString("#3F51B5");
            public static Color Indigo600 { get; } = (Color)ColorConverter.ConvertFromString("#3949AB");
            public static Color Indigo700 { get; } = (Color)ColorConverter.ConvertFromString("#303F9F");
            public static Color Indigo800 { get; } = (Color)ColorConverter.ConvertFromString("#283593");
            public static Color Indigo900 { get; } = (Color)ColorConverter.ConvertFromString("#1A237E");

        }

        public static class Accent
        {
            public static Color Indigo100 { get; } = (Color)ColorConverter.ConvertFromString("#8C9EFF");
            public static Color Indigo200 { get; } = (Color)ColorConverter.ConvertFromString("#536DFE");
            public static Color Indigo400 { get; } = (Color)ColorConverter.ConvertFromString("#3D5AFE");
            public static Color Indigo700 { get; } = (Color)ColorConverter.ConvertFromString("#304FFE");
        }


        public string Name { get; } = "Indigo";

        public IEnumerable<Color> Hues
        {
            get
            {
                yield return Primary.Indigo50;
                yield return Primary.Indigo100;
                yield return Primary.Indigo200;
                yield return Primary.Indigo300;
                yield return Primary.Indigo400;
                yield return Primary.Indigo500;
                yield return Primary.Indigo600;
                yield return Primary.Indigo700;
                yield return Primary.Indigo800;
                yield return Primary.Indigo900;

                yield return Accent.Indigo100;
                yield return Accent.Indigo200;
                yield return Accent.Indigo400;
                yield return Accent.Indigo700;
            }
        }
    }
}
