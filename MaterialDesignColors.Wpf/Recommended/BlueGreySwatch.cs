using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class BlueGreySwatch : ISwatch
    {
        public static class Primary
        {
            public static Color BlueGrey50 { get; } = (Color)ColorConverter.ConvertFromString("#ECEFF1");
            public static Color BlueGrey100 { get; } = (Color)ColorConverter.ConvertFromString("#CFD8DC");
            public static Color BlueGrey200 { get; } = (Color)ColorConverter.ConvertFromString("#B0BEC5");
            public static Color BlueGrey300 { get; } = (Color)ColorConverter.ConvertFromString("#90A4AE");
            public static Color BlueGrey400 { get; } = (Color)ColorConverter.ConvertFromString("#78909C");
            public static Color BlueGrey500 { get; } = (Color)ColorConverter.ConvertFromString("#607D8B");
            public static Color BlueGrey600 { get; } = (Color)ColorConverter.ConvertFromString("#546E7A");
            public static Color BlueGrey700 { get; } = (Color)ColorConverter.ConvertFromString("#455A64");
            public static Color BlueGrey800 { get; } = (Color)ColorConverter.ConvertFromString("#37474F");
            public static Color BlueGrey900 { get; } = (Color)ColorConverter.ConvertFromString("#263238");
        }


        public string Name { get; } = "Blue Grey";

        public IEnumerable<Color> Hues
        {
            get
            {
                yield return Primary.BlueGrey50;
                yield return Primary.BlueGrey100;
                yield return Primary.BlueGrey200;
                yield return Primary.BlueGrey300;
                yield return Primary.BlueGrey400;
                yield return Primary.BlueGrey500;
                yield return Primary.BlueGrey600;
                yield return Primary.BlueGrey700;
                yield return Primary.BlueGrey800;
                yield return Primary.BlueGrey900;
            }
        }
    }
}
