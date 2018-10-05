using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class LightBlueSwatch : ISwatch
    {
        public static CodeHue LightBlue50 { get; } = new CodeHue("Light Blue", "50", "#E1F5FE");
        public static CodeHue LightBlue100 { get; } = new CodeHue("Light Blue", "100", "#B3E5FC");
        public static CodeHue LightBlue200 { get; } = new CodeHue("Light Blue", "200", "#81D4FA");
        public static CodeHue LightBlue300 { get; } = new CodeHue("Light Blue", "300", "#4FC3F7");
        public static CodeHue LightBlue400 { get; } = new CodeHue("Light Blue", "400", "#29B6F6");
        public static CodeHue LightBlue500 { get; } = new CodeHue("Light Blue", "500", "#03A9F4");
        public static CodeHue LightBlue600 { get; } = new CodeHue("Light Blue", "600", "#039BE5");
        public static CodeHue LightBlue700 { get; } = new CodeHue("Light Blue", "700", "#0288D1");
        public static CodeHue LightBlue800 { get; } = new CodeHue("Light Blue", "800", "#0277BD");
        public static CodeHue LightBlue900 { get; } = new CodeHue("Light Blue", "900", "#01579B");
        public static CodeHue LightBlueA100 { get; } = new CodeHue("Light Blue", "A100", "#80D8FF");
        public static CodeHue LightBlueA200 { get; } = new CodeHue("Light Blue", "A200", "#40C4FF");
        public static CodeHue LightBlueA400 { get; } = new CodeHue("Light Blue", "A400", "#00B0FF");
        public static CodeHue LightBlueA700 { get; } = new CodeHue("Light Blue", "A700", "#0091EA");

        public string Name { get; } = "Light Blue";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            LightBlue50,
            LightBlue100,
            LightBlue200,
            LightBlue300,
            LightBlue400,
            LightBlue500,
            LightBlue600,
            LightBlue700,
            LightBlue800,
            LightBlue900,
            LightBlueA100,
            LightBlueA200,
            LightBlueA400,
            LightBlueA700
        };
    }
}
