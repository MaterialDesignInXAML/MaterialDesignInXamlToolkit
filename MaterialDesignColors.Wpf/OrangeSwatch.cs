using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class OrangeSwatch : ISwatch
    {
        public static CodeHue Orange50 { get; } = new CodeHue("Orange", "50", "#FFF3E0");
        public static CodeHue Orange100 { get; } = new CodeHue("Orange", "100", "#FFE0B2");
        public static CodeHue Orange200 { get; } = new CodeHue("Orange", "200", "#FFCC80");
        public static CodeHue Orange300 { get; } = new CodeHue("Orange", "300", "#FFB74D");
        public static CodeHue Orange400 { get; } = new CodeHue("Orange", "400", "#FFA726");
        public static CodeHue Orange500 { get; } = new CodeHue("Orange", "500", "#FF9800");
        public static CodeHue Orange600 { get; } = new CodeHue("Orange", "600", "#FB8C00");
        public static CodeHue Orange700 { get; } = new CodeHue("Orange", "700", "#F57C00");
        public static CodeHue Orange800 { get; } = new CodeHue("Orange", "800", "#EF6C00");
        public static CodeHue Orange900 { get; } = new CodeHue("Orange", "900", "#E65100");
        public static CodeHue OrangeA100 { get; } = new CodeHue("Orange", "A100", "#FFD180");
        public static CodeHue OrangeA200 { get; } = new CodeHue("Orange", "A200", "#FFAB40");
        public static CodeHue OrangeA400 { get; } = new CodeHue("Orange", "A400", "#FF9100");
        public static CodeHue OrangeA700 { get; } = new CodeHue("Orange", "A700", "#FF6D00");

        public string Name { get; } = "Orange";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            Orange50,
            Orange100,
            Orange200,
            Orange300,
            Orange400,
            Orange500,
            Orange600,
            Orange700,
            Orange800,
            Orange900,
            OrangeA100,
            OrangeA200,
            OrangeA400,
            OrangeA700
        };
    }
}
