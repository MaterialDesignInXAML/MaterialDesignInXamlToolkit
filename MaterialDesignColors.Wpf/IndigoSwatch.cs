using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class IndigoSwatch : ISwatch
    {
        public static CodeHue Indigo50 { get; } = new CodeHue("Indigo", "50", "#E8EAF6");
        public static CodeHue Indigo100 { get; } = new CodeHue("Indigo", "100", "#C5CAE9");
        public static CodeHue Indigo200 { get; } = new CodeHue("Indigo", "200", "#9FA8DA");
        public static CodeHue Indigo300 { get; } = new CodeHue("Indigo", "300", "#7986CB");
        public static CodeHue Indigo400 { get; } = new CodeHue("Indigo", "400", "#5C6BC0");
        public static CodeHue Indigo500 { get; } = new CodeHue("Indigo", "500", "#3F51B5");
        public static CodeHue Indigo600 { get; } = new CodeHue("Indigo", "600", "#3949AB");
        public static CodeHue Indigo700 { get; } = new CodeHue("Indigo", "700", "#303F9F");
        public static CodeHue Indigo800 { get; } = new CodeHue("Indigo", "800", "#283593");
        public static CodeHue Indigo900 { get; } = new CodeHue("Indigo", "900", "#1A237E");
        public static CodeHue IndigoA100 { get; } = new CodeHue("Indigo", "A100", "#8C9EFF");
        public static CodeHue IndigoA200 { get; } = new CodeHue("Indigo", "A200", "#536DFE");
        public static CodeHue IndigoA400 { get; } = new CodeHue("Indigo", "A400", "#3D5AFE");
        public static CodeHue IndigoA700 { get; } = new CodeHue("Indigo", "A700", "#304FFE");

        public string Name { get; } = "Indigo";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            Indigo50,
            Indigo100,
            Indigo200,
            Indigo300,
            Indigo400,
            Indigo500,
            Indigo600,
            Indigo700,
            Indigo800,
            Indigo900,
            IndigoA100,
            IndigoA200,
            IndigoA400,
            IndigoA700
        };
    }
}
