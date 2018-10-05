using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class BlueSwatch : ISwatch
    {
        public static CodeHue Blue50 { get; } = new CodeHue("Blue", "50", "#E3F2FD");
        public static CodeHue Blue100 { get; } = new CodeHue("Blue", "100", "#BBDEFB");
        public static CodeHue Blue200 { get; } = new CodeHue("Blue", "200", "#90CAF9");
        public static CodeHue Blue300 { get; } = new CodeHue("Blue", "300", "#64B5F6");
        public static CodeHue Blue400 { get; } = new CodeHue("Blue", "400", "#42A5F5");
        public static CodeHue Blue500 { get; } = new CodeHue("Blue", "500", "#2196F3");
        public static CodeHue Blue600 { get; } = new CodeHue("Blue", "600", "#1E88E5");
        public static CodeHue Blue700 { get; } = new CodeHue("Blue", "700", "#1976D2");
        public static CodeHue Blue800 { get; } = new CodeHue("Blue", "800", "#1565C0");
        public static CodeHue Blue900 { get; } = new CodeHue("Blue", "900", "#0D47A1");
        public static CodeHue BlueA100 { get; } = new CodeHue("Blue", "A100", "#82B1FF");
        public static CodeHue BlueA200 { get; } = new CodeHue("Blue", "A200", "#448AFF");
        public static CodeHue BlueA400 { get; } = new CodeHue("Blue", "A400", "#2979FF");
        public static CodeHue BlueA700 { get; } = new CodeHue("Blue", "A700", "#2962FF");

        public string Name { get; } = "Blue";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            Blue50,
            Blue100,
            Blue200,
            Blue300,
            Blue400,
            Blue500,
            Blue600,
            Blue700,
            Blue800,
            Blue900,
            BlueA100,
            BlueA200,
            BlueA400,
            BlueA700
        };
    }
}
