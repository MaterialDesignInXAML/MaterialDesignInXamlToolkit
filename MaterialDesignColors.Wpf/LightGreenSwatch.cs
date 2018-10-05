using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class LightGreenSwatch : ISwatch
    {
        public static CodeHue LightGreen50 { get; } = new CodeHue("Light Green", "50", "#F1F8E9");
        public static CodeHue LightGreen100 { get; } = new CodeHue("Light Green", "100", "#DCEDC8");
        public static CodeHue LightGreen200 { get; } = new CodeHue("Light Green", "200", "#C5E1A5");
        public static CodeHue LightGreen300 { get; } = new CodeHue("Light Green", "300", "#AED581");
        public static CodeHue LightGreen400 { get; } = new CodeHue("Light Green", "400", "#9CCC65");
        public static CodeHue LightGreen500 { get; } = new CodeHue("Light Green", "500", "#8BC34A");
        public static CodeHue LightGreen600 { get; } = new CodeHue("Light Green", "600", "#7CB342");
        public static CodeHue LightGreen700 { get; } = new CodeHue("Light Green", "700", "#689F38");
        public static CodeHue LightGreen800 { get; } = new CodeHue("Light Green", "800", "#558B2F");
        public static CodeHue LightGreen900 { get; } = new CodeHue("Light Green", "900", "#33691E");
        public static CodeHue LightGreenA100 { get; } = new CodeHue("Light Green", "A100", "#CCFF90");
        public static CodeHue LightGreenA200 { get; } = new CodeHue("Light Green", "A200", "#B2FF59");
        public static CodeHue LightGreenA400 { get; } = new CodeHue("Light Green", "A400", "#76FF03");
        public static CodeHue LightGreenA700 { get; } = new CodeHue("Light Green", "A700", "#64DD17");

        public string Name { get; } = "Light Green";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            LightGreen50,
            LightGreen100,
            LightGreen200,
            LightGreen300,
            LightGreen400,
            LightGreen500,
            LightGreen600,
            LightGreen700,
            LightGreen800,
            LightGreen900,
            LightGreenA100,
            LightGreenA200,
            LightGreenA400,
            LightGreenA700
        };
    }
}
