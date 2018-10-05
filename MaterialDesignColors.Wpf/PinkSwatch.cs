using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class PinkSwatch : ISwatch
    {
        public static CodeHue Pink50 { get; } = new CodeHue("Pink", "50", "#FCE4EC");
        public static CodeHue Pink100 { get; } = new CodeHue("Pink", "100", "#F8BBD0");
        public static CodeHue Pink200 { get; } = new CodeHue("Pink", "200", "#F48FB1");
        public static CodeHue Pink300 { get; } = new CodeHue("Pink", "300", "#F06292");
        public static CodeHue Pink400 { get; } = new CodeHue("Pink", "400", "#EC407A");
        public static CodeHue Pink500 { get; } = new CodeHue("Pink", "500", "#E91E63");
        public static CodeHue Pink600 { get; } = new CodeHue("Pink", "600", "#D81B60");
        public static CodeHue Pink700 { get; } = new CodeHue("Pink", "700", "#C2185B");
        public static CodeHue Pink800 { get; } = new CodeHue("Pink", "800", "#AD1457");
        public static CodeHue Pink900 { get; } = new CodeHue("Pink", "900", "#880E4F");
        public static CodeHue PinkA100 { get; } = new CodeHue("Pink", "A100", "#FF80AB");
        public static CodeHue PinkA200 { get; } = new CodeHue("Pink", "A200", "#FF4081");
        public static CodeHue PinkA400 { get; } = new CodeHue("Pink", "A400", "#F50057");
        public static CodeHue PinkA700 { get; } = new CodeHue("Pink", "A700", "#C51162");

        public string Name { get; } = "Pink";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            Pink50,
            Pink100,
            Pink200,
            Pink300,
            Pink400,
            Pink500,
            Pink600,
            Pink700,
            Pink800,
            Pink900,
            PinkA100,
            PinkA200,
            PinkA400,
            PinkA700
        };
    }
}
