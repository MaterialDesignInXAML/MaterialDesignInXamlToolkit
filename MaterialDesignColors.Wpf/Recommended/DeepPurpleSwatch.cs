using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class DeepPurpleSwatch : ISwatch
    {
        public static class Primary
        {
            public static Color DeepPurple50 { get; } = (Color)ColorConverter.ConvertFromString("#EDE7F6");
            public static Color DeepPurple100 { get; } = (Color)ColorConverter.ConvertFromString("#D1C4E9");
            public static Color DeepPurple200 { get; } = (Color)ColorConverter.ConvertFromString("#B39DDB");
            public static Color DeepPurple300 { get; } = (Color)ColorConverter.ConvertFromString("#9575CD");
            public static Color DeepPurple400 { get; } = (Color)ColorConverter.ConvertFromString("#7E57C2");
            public static Color DeepPurple500 { get; } = (Color)ColorConverter.ConvertFromString("#673AB7");
            public static Color DeepPurple600 { get; } = (Color)ColorConverter.ConvertFromString("#5E35B1");
            public static Color DeepPurple700 { get; } = (Color)ColorConverter.ConvertFromString("#512DA8");
            public static Color DeepPurple800 { get; } = (Color)ColorConverter.ConvertFromString("#4527A0");
            public static Color DeepPurple900 { get; } = (Color)ColorConverter.ConvertFromString("#311B92");

        }

        public static class Accent
        {
            public static Color DeepPurple100 { get; } = (Color)ColorConverter.ConvertFromString("#B388FF");
            public static Color DeepPurple200 { get; } = (Color)ColorConverter.ConvertFromString("#7C4DFF");
            public static Color DeepPurple400 { get; } = (Color)ColorConverter.ConvertFromString("#651FFF");
            public static Color DeepPurple700 { get; } = (Color)ColorConverter.ConvertFromString("#6200EA");

        }


        public string Name { get; } = "Deep Purple";

        public IEnumerable<Color> Hues
        {
            get
            {
                yield return Primary.DeepPurple50;
                yield return Primary.DeepPurple100;
                yield return Primary.DeepPurple200;
                yield return Primary.DeepPurple300;
                yield return Primary.DeepPurple400;
                yield return Primary.DeepPurple500;
                yield return Primary.DeepPurple600;
                yield return Primary.DeepPurple700;
                yield return Primary.DeepPurple800;
                yield return Primary.DeepPurple900;

                yield return Accent.DeepPurple100;
                yield return Accent.DeepPurple200;
                yield return Accent.DeepPurple400;
                yield return Accent.DeepPurple700;
            }
        }
    }
}
