using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class DeepOrangeSwatch : ISwatch
    {
        public static CodeHue DeepOrange50 { get; } = new CodeHue("Deep Orange", "50", "#FBE9E7");
        public static CodeHue DeepOrange100 { get; } = new CodeHue("Deep Orange", "100", "#FFCCBC");
        public static CodeHue DeepOrange200 { get; } = new CodeHue("Deep Orange", "200", "#FFAB91");
        public static CodeHue DeepOrange300 { get; } = new CodeHue("Deep Orange", "300", "#FF8A65");
        public static CodeHue DeepOrange400 { get; } = new CodeHue("Deep Orange", "400", "#FF7043");
        public static CodeHue DeepOrange500 { get; } = new CodeHue("Deep Orange", "500", "#FF5722");
        public static CodeHue DeepOrange600 { get; } = new CodeHue("Deep Orange", "600", "#F4511E");
        public static CodeHue DeepOrange700 { get; } = new CodeHue("Deep Orange", "700", "#E64A19");
        public static CodeHue DeepOrange800 { get; } = new CodeHue("Deep Orange", "800", "#D84315");
        public static CodeHue DeepOrange900 { get; } = new CodeHue("Deep Orange", "900", "#BF360C");
        public static CodeHue DeepOrangeA100 { get; } = new CodeHue("Deep Orange", "A100", "#FF9E80");
        public static CodeHue DeepOrangeA200 { get; } = new CodeHue("Deep Orange", "A200", "#FF6E40");
        public static CodeHue DeepOrangeA400 { get; } = new CodeHue("Deep Orange", "A400", "#FF3D00");
        public static CodeHue DeepOrangeA700 { get; } = new CodeHue("Deep Orange", "A700", "#DD2C00");

        public string Name { get; } = "Deep Orange";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            DeepOrange50,
            DeepOrange100,
            DeepOrange200,
            DeepOrange300,
            DeepOrange400,
            DeepOrange500,
            DeepOrange600,
            DeepOrange700,
            DeepOrange800,
            DeepOrange900,
            DeepOrangeA100,
            DeepOrangeA200,
            DeepOrangeA400,
            DeepOrangeA700
        };
    }
}
