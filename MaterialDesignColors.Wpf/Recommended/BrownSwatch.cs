using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class BrownSwatch : ISwatch
    {
        public static CodeHue Brown50 { get; } = new CodeHue("Brown", "50", "#EFEBE9");
        public static CodeHue Brown100 { get; } = new CodeHue("Brown", "100", "#D7CCC8");
        public static CodeHue Brown200 { get; } = new CodeHue("Brown", "200", "#BCAAA4");
        public static CodeHue Brown300 { get; } = new CodeHue("Brown", "300", "#A1887F");
        public static CodeHue Brown400 { get; } = new CodeHue("Brown", "400", "#8D6E63");
        public static CodeHue Brown500 { get; } = new CodeHue("Brown", "500", "#795548");
        public static CodeHue Brown600 { get; } = new CodeHue("Brown", "600", "#6D4C41");
        public static CodeHue Brown700 { get; } = new CodeHue("Brown", "700", "#5D4037");
        public static CodeHue Brown800 { get; } = new CodeHue("Brown", "800", "#4E342E");
        public static CodeHue Brown900 { get; } = new CodeHue("Brown", "900", "#3E2723");

        public string Name { get; } = "Brown";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            Brown50,
            Brown100,
            Brown200,
            Brown300,
            Brown400,
            Brown500,
            Brown600,
            Brown700,
            Brown800,
            Brown900
        };
    }
}
