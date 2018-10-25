using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class YellowSwatch : ISwatch
    {
        public static CodeHue Yellow50 { get; } = new CodeHue("Yellow", "50", "#FFFDE7");
        public static CodeHue Yellow100 { get; } = new CodeHue("Yellow", "100", "#FFF9C4");
        public static CodeHue Yellow200 { get; } = new CodeHue("Yellow", "200", "#FFF59D");
        public static CodeHue Yellow300 { get; } = new CodeHue("Yellow", "300", "#FFF176");
        public static CodeHue Yellow400 { get; } = new CodeHue("Yellow", "400", "#FFEE58");
        public static CodeHue Yellow500 { get; } = new CodeHue("Yellow", "500", "#FFEB3B");
        public static CodeHue Yellow600 { get; } = new CodeHue("Yellow", "600", "#FDD835");
        public static CodeHue Yellow700 { get; } = new CodeHue("Yellow", "700", "#FBC02D");
        public static CodeHue Yellow800 { get; } = new CodeHue("Yellow", "800", "#F9A825");
        public static CodeHue Yellow900 { get; } = new CodeHue("Yellow", "900", "#F57F17");
        public static CodeHue YellowA100 { get; } = new CodeHue("Yellow", "A100", "#FFFF8D");
        public static CodeHue YellowA200 { get; } = new CodeHue("Yellow", "A200", "#FFFF00");
        public static CodeHue YellowA400 { get; } = new CodeHue("Yellow", "A400", "#FFEA00");
        public static CodeHue YellowA700 { get; } = new CodeHue("Yellow", "A700", "#FFD600");

        public string Name { get; } = "Yellow";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            Yellow50,
            Yellow100,
            Yellow200,
            Yellow300,
            Yellow400,
            Yellow500,
            Yellow600,
            Yellow700,
            Yellow800,
            Yellow900,
            YellowA100,
            YellowA200,
            YellowA400,
            YellowA700
        };
    }
}
