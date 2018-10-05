using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class DeepPurpleSwatch : ISwatch
    {
        public static CodeHue DeepPurple50 { get; } = new CodeHue("Deep Purple", "50", "#EDE7F6");
        public static CodeHue DeepPurple100 { get; } = new CodeHue("Deep Purple", "100", "#D1C4E9");
        public static CodeHue DeepPurple200 { get; } = new CodeHue("Deep Purple", "200", "#B39DDB");
        public static CodeHue DeepPurple300 { get; } = new CodeHue("Deep Purple", "300", "#9575CD");
        public static CodeHue DeepPurple400 { get; } = new CodeHue("Deep Purple", "400", "#7E57C2");
        public static CodeHue DeepPurple500 { get; } = new CodeHue("Deep Purple", "500", "#673AB7");
        public static CodeHue DeepPurple600 { get; } = new CodeHue("Deep Purple", "600", "#5E35B1");
        public static CodeHue DeepPurple700 { get; } = new CodeHue("Deep Purple", "700", "#512DA8");
        public static CodeHue DeepPurple800 { get; } = new CodeHue("Deep Purple", "800", "#4527A0");
        public static CodeHue DeepPurple900 { get; } = new CodeHue("Deep Purple", "900", "#311B92");
        public static CodeHue DeepPurpleA100 { get; } = new CodeHue("Deep Purple", "A100", "#B388FF");
        public static CodeHue DeepPurpleA200 { get; } = new CodeHue("Deep Purple", "A200", "#7C4DFF");
        public static CodeHue DeepPurpleA400 { get; } = new CodeHue("Deep Purple", "A400", "#651FFF");
        public static CodeHue DeepPurpleA700 { get; } = new CodeHue("Deep Purple", "A700", "#6200EA");

        public string Name { get; } = "Deep Purple";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            DeepPurple50,
            DeepPurple100,
            DeepPurple200,
            DeepPurple300,
            DeepPurple400,
            DeepPurple500,
            DeepPurple600,
            DeepPurple700,
            DeepPurple800,
            DeepPurple900,
            DeepPurpleA100,
            DeepPurpleA200,
            DeepPurpleA400,
            DeepPurpleA700
        };
    }
}
