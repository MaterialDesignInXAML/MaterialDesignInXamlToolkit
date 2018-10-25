using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class BlueGreySwatch : ISwatch
    {
        public static CodeHue BlueGrey50 { get; } = new CodeHue("Blue Grey", "50", "#ECEFF1");
        public static CodeHue BlueGrey100 { get; } = new CodeHue("Blue Grey", "100", "#CFD8DC");
        public static CodeHue BlueGrey200 { get; } = new CodeHue("Blue Grey", "200", "#B0BEC5");
        public static CodeHue BlueGrey300 { get; } = new CodeHue("Blue Grey", "300", "#90A4AE");
        public static CodeHue BlueGrey400 { get; } = new CodeHue("Blue Grey", "400", "#78909C");
        public static CodeHue BlueGrey500 { get; } = new CodeHue("Blue Grey", "500", "#607D8B");
        public static CodeHue BlueGrey600 { get; } = new CodeHue("Blue Grey", "600", "#546E7A");
        public static CodeHue BlueGrey700 { get; } = new CodeHue("Blue Grey", "700", "#455A64");
        public static CodeHue BlueGrey800 { get; } = new CodeHue("Blue Grey", "800", "#37474F");
        public static CodeHue BlueGrey900 { get; } = new CodeHue("Blue Grey", "900", "#263238");

        public string Name { get; } = "Blue Grey";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            BlueGrey50,
            BlueGrey100,
            BlueGrey200,
            BlueGrey300,
            BlueGrey400,
            BlueGrey500,
            BlueGrey600,
            BlueGrey700,
            BlueGrey800,
            BlueGrey900
        };
    }
}
