using System.Collections.Generic;

namespace MaterialDesignColors
{
    public class GreySwatch : ISwatch
    {
        public static CodeHue Grey50 { get; } = new CodeHue("Grey", "50", "#FAFAFA");
        public static CodeHue Grey100 { get; } = new CodeHue("Grey", "100", "#F5F5F5");
        public static CodeHue Grey200 { get; } = new CodeHue("Grey", "200", "#EEEEEE");
        public static CodeHue Grey300 { get; } = new CodeHue("Grey", "300", "#E0E0E0");
        public static CodeHue Grey400 { get; } = new CodeHue("Grey", "400", "#BDBDBD");
        public static CodeHue Grey500 { get; } = new CodeHue("Grey", "500", "#9E9E9E");
        public static CodeHue Grey600 { get; } = new CodeHue("Grey", "600", "#757575");
        public static CodeHue Grey700 { get; } = new CodeHue("Grey", "700", "#616161");
        public static CodeHue Grey800 { get; } = new CodeHue("Grey", "800", "#424242");
        public static CodeHue Grey900 { get; } = new CodeHue("Grey", "900", "#212121");

        public string Name { get; } = "Grey";

        public IEnumerable<CodeHue> Hues { get; } = new[]
        {
            Grey50,
            Grey100,
            Grey200,
            Grey300,
            Grey400,
            Grey500,
            Grey600,
            Grey700,
            Grey800,
            Grey900
        };
    }
}
