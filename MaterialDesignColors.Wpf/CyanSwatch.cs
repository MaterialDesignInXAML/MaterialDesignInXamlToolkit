using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class CyanSwatch : ISwatch
    {
        public static CodeHue Cyan50 { get; } = new CodeHue("Cyan", "50", "#E0F7FA");
        public static CodeHue Cyan100 { get; } = new CodeHue("Cyan", "100", "#B2EBF2");
        public static CodeHue Cyan200 { get; } = new CodeHue("Cyan", "200", "#80DEEA");
        public static CodeHue Cyan300 { get; } = new CodeHue("Cyan", "300", "#4DD0E1");
        public static CodeHue Cyan400 { get; } = new CodeHue("Cyan", "400", "#26C6DA");
        public static CodeHue Cyan500 { get; } = new CodeHue("Cyan", "500", "#00BCD4");
        public static CodeHue Cyan600 { get; } = new CodeHue("Cyan", "600", "#00ACC1");
        public static CodeHue Cyan700 { get; } = new CodeHue("Cyan", "700", "#0097A7");
        public static CodeHue Cyan800 { get; } = new CodeHue("Cyan", "800", "#00838F");
        public static CodeHue Cyan900 { get; } = new CodeHue("Cyan", "900", "#006064");
        public static CodeHue CyanA100 { get; } = new CodeHue("Cyan", "A100", "#84FFFF");
        public static CodeHue CyanA200 { get; } = new CodeHue("Cyan", "A200", "#18FFFF");
        public static CodeHue CyanA400 { get; } = new CodeHue("Cyan", "A400", "#00E5FF");
        public static CodeHue CyanA700 { get; } = new CodeHue("Cyan", "A700", "#00B8D4");

        public string Name { get; } = "Cyan";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            Cyan50,
            Cyan100,
            Cyan200,
            Cyan300,
            Cyan400,
            Cyan500,
            Cyan600,
            Cyan700,
            Cyan800,
            Cyan900,
            CyanA100,
            CyanA200,
            CyanA400,
            CyanA700
        };
    }
}
