using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class GreenSwatch : ISwatch
    {
        public static CodeHue Green50 { get; } = new CodeHue("Green", "50", "#E8F5E9");
        public static CodeHue Green100 { get; } = new CodeHue("Green", "100", "#C8E6C9");
        public static CodeHue Green200 { get; } = new CodeHue("Green", "200", "#A5D6A7");
        public static CodeHue Green300 { get; } = new CodeHue("Green", "300", "#81C784");
        public static CodeHue Green400 { get; } = new CodeHue("Green", "400", "#66BB6A");
        public static CodeHue Green500 { get; } = new CodeHue("Green", "500", "#4CAF50");
        public static CodeHue Green600 { get; } = new CodeHue("Green", "600", "#43A047");
        public static CodeHue Green700 { get; } = new CodeHue("Green", "700", "#388E3C");
        public static CodeHue Green800 { get; } = new CodeHue("Green", "800", "#2E7D32");
        public static CodeHue Green900 { get; } = new CodeHue("Green", "900", "#1B5E20");
        public static CodeHue GreenA100 { get; } = new CodeHue("Green", "A100", "#B9F6CA");
        public static CodeHue GreenA200 { get; } = new CodeHue("Green", "A200", "#69F0AE");
        public static CodeHue GreenA400 { get; } = new CodeHue("Green", "A400", "#00E676");
        public static CodeHue GreenA700 { get; } = new CodeHue("Green", "A700", "#00C853");

        public string Name { get; } = "Green";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            Green50,
            Green100,
            Green200,
            Green300,
            Green400,
            Green500,
            Green600,
            Green700,
            Green800,
            Green900,
            GreenA100,
            GreenA200,
            GreenA400,
            GreenA700
        };
    }
}
