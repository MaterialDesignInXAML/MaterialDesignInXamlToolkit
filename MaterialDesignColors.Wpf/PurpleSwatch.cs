using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class PurpleSwatch : ISwatch
    {
        public static CodeHue Purple50 { get; } = new CodeHue("Purple", "50", "#F3E5F5");
        public static CodeHue Purple100 { get; } = new CodeHue("Purple", "100", "#E1BEE7");
        public static CodeHue Purple200 { get; } = new CodeHue("Purple", "200", "#CE93D8");
        public static CodeHue Purple300 { get; } = new CodeHue("Purple", "300", "#BA68C8");
        public static CodeHue Purple400 { get; } = new CodeHue("Purple", "400", "#AB47BC");
        public static CodeHue Purple500 { get; } = new CodeHue("Purple", "500", "#9C27B0");
        public static CodeHue Purple600 { get; } = new CodeHue("Purple", "600", "#8E24AA");
        public static CodeHue Purple700 { get; } = new CodeHue("Purple", "700", "#7B1FA2");
        public static CodeHue Purple800 { get; } = new CodeHue("Purple", "800", "#6A1B9A");
        public static CodeHue Purple900 { get; } = new CodeHue("Purple", "900", "#4A148C");
        public static CodeHue PurpleA100 { get; } = new CodeHue("Purple", "A100", "#EA80FC");
        public static CodeHue PurpleA200 { get; } = new CodeHue("Purple", "A200", "#E040FB");
        public static CodeHue PurpleA400 { get; } = new CodeHue("Purple", "A400", "#D500F9");
        public static CodeHue PurpleA700 { get; } = new CodeHue("Purple", "A700", "#AA00FF");

        public string Name { get; } = "Purple";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            Purple50,
            Purple100,
            Purple200,
            Purple300,
            Purple400,
            Purple500,
            Purple600,
            Purple700,
            Purple800,
            Purple900,
            PurpleA100,
            PurpleA200,
            PurpleA400,
            PurpleA700
        };
    }
}
