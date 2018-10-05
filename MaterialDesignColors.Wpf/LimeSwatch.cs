using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class LimeSwatch : ISwatch
    {
        public static CodeHue Lime50 { get; } = new CodeHue("Lime", "50", "#F9FBE7");
        public static CodeHue Lime100 { get; } = new CodeHue("Lime", "100", "#F0F4C3");
        public static CodeHue Lime200 { get; } = new CodeHue("Lime", "200", "#E6EE9C");
        public static CodeHue Lime300 { get; } = new CodeHue("Lime", "300", "#DCE775");
        public static CodeHue Lime400 { get; } = new CodeHue("Lime", "400", "#D4E157");
        public static CodeHue Lime500 { get; } = new CodeHue("Lime", "500", "#CDDC39");
        public static CodeHue Lime600 { get; } = new CodeHue("Lime", "600", "#C0CA33");
        public static CodeHue Lime700 { get; } = new CodeHue("Lime", "700", "#AFB42B");
        public static CodeHue Lime800 { get; } = new CodeHue("Lime", "800", "#9E9D24");
        public static CodeHue Lime900 { get; } = new CodeHue("Lime", "900", "#827717");
        public static CodeHue LimeA100 { get; } = new CodeHue("Lime", "A100", "#F4FF81");
        public static CodeHue LimeA200 { get; } = new CodeHue("Lime", "A200", "#EEFF41");
        public static CodeHue LimeA400 { get; } = new CodeHue("Lime", "A400", "#C6FF00");
        public static CodeHue LimeA700 { get; } = new CodeHue("Lime", "A700", "#AEEA00");

        public string Name { get; } = "Lime";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            Lime50,
            Lime100,
            Lime200,
            Lime300,
            Lime400,
            Lime500,
            Lime600,
            Lime700,
            Lime800,
            Lime900,
            LimeA100,
            LimeA200,
            LimeA400,
            LimeA700
        };
    }
}
