using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class AmberSwatch : ISwatch
    {
        public static CodeHue Amber50 { get; } = new CodeHue("Amber", "50", "#FFF8E1");
        public static CodeHue Amber100 { get; } = new CodeHue("Amber", "100", "#FFECB3");
        public static CodeHue Amber200 { get; } = new CodeHue("Amber", "200", "#FFE082");
        public static CodeHue Amber300 { get; } = new CodeHue("Amber", "300", "#FFD54F");
        public static CodeHue Amber400 { get; } = new CodeHue("Amber", "400", "#FFCA28");
        public static CodeHue Amber500 { get; } = new CodeHue("Amber", "500", "#FFC107");
        public static CodeHue Amber600 { get; } = new CodeHue("Amber", "600", "#FFB300");
        public static CodeHue Amber700 { get; } = new CodeHue("Amber", "700", "#FFA000");
        public static CodeHue Amber800 { get; } = new CodeHue("Amber", "800", "#FF8F00");
        public static CodeHue Amber900 { get; } = new CodeHue("Amber", "900", "#FF6F00");
        public static CodeHue AmberA100 { get; } = new CodeHue("Amber", "A100", "#FFE57F");
        public static CodeHue AmberA200 { get; } = new CodeHue("Amber", "A200", "#FFD740");
        public static CodeHue AmberA400 { get; } = new CodeHue("Amber", "A400", "#FFC400");
        public static CodeHue AmberA700 { get; } = new CodeHue("Amber", "A700", "#FFAB00");

        public string Name { get; } = "Amber";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            Amber50,
            Amber100,
            Amber200,
            Amber300,
            Amber400,
            Amber500,
            Amber600,
            Amber700,
            Amber800,
            Amber900,
            AmberA100,
            AmberA200,
            AmberA400,
            AmberA700
        };
    }
}
