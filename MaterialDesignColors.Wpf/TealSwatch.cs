using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class TealSwatch : ISwatch
    {
        public static CodeHue Teal50 { get; } = new CodeHue("Teal", "50", "#E0F2F1");
        public static CodeHue Teal100 { get; } = new CodeHue("Teal", "100", "#B2DFDB");
        public static CodeHue Teal200 { get; } = new CodeHue("Teal", "200", "#80CBC4");
        public static CodeHue Teal300 { get; } = new CodeHue("Teal", "300", "#4DB6AC");
        public static CodeHue Teal400 { get; } = new CodeHue("Teal", "400", "#26A69A");
        public static CodeHue Teal500 { get; } = new CodeHue("Teal", "500", "#009688");
        public static CodeHue Teal600 { get; } = new CodeHue("Teal", "600", "#00897B");
        public static CodeHue Teal700 { get; } = new CodeHue("Teal", "700", "#00796B");
        public static CodeHue Teal800 { get; } = new CodeHue("Teal", "800", "#00695C");
        public static CodeHue Teal900 { get; } = new CodeHue("Teal", "900", "#004D40");
        public static CodeHue TealA100 { get; } = new CodeHue("Teal", "A100", "#A7FFEB");
        public static CodeHue TealA200 { get; } = new CodeHue("Teal", "A200", "#64FFDA");
        public static CodeHue TealA400 { get; } = new CodeHue("Teal", "A400", "#1DE9B6");
        public static CodeHue TealA700 { get; } = new CodeHue("Teal", "A700", "#00BFA5");

        public string Name { get; } = "Teal";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            Teal50,
            Teal100,
            Teal200,
            Teal300,
            Teal400,
            Teal500,
            Teal600,
            Teal700,
            Teal800,
            Teal900,
            TealA100,
            TealA200,
            TealA400,
            TealA700
        };
    }
}
