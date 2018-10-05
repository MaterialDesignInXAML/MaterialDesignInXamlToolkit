using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class RedSwatch : ISwatch
    {
        public static CodeHue Red50 { get; } = new CodeHue("Red", "50", "#FFEBEE");
        public static CodeHue Red100 { get; } = new CodeHue("Red", "100", "#FFCDD2");
        public static CodeHue Red200 { get; } = new CodeHue("Red", "200", "#EF9A9A");
        public static CodeHue Red300 { get; } = new CodeHue("Red", "300", "#E57373");
        public static CodeHue Red400 { get; } = new CodeHue("Red", "400", "#EF5350");
        public static CodeHue Red500 { get; } = new CodeHue("Red", "500", "#F44336");
        public static CodeHue Red600 { get; } = new CodeHue("Red", "600", "#E53935");
        public static CodeHue Red700 { get; } = new CodeHue("Red", "700", "#D32F2F");
        public static CodeHue Red800 { get; } = new CodeHue("Red", "800", "#C62828");
        public static CodeHue Red900 { get; } = new CodeHue("Red", "900", "#B71C1C");
        public static CodeHue RedA100 { get; } = new CodeHue("Red", "A100", "#FF8A80");
        public static CodeHue RedA200 { get; } = new CodeHue("Red", "A200", "#FF5252");
        public static CodeHue RedA400 { get; } = new CodeHue("Red", "A400", "#FF1744");
        public static CodeHue RedA700 { get; } = new CodeHue("Red", "A700", "#D50000");

        public string Name { get; } = "Red";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            Red50,
            Red100,
            Red200,
            Red300,
            Red400,
            Red500,
            Red600,
            Red700,
            Red800,
            Red900,
            RedA100,
            RedA200,
            RedA400,
            RedA700
        };
    }
}
