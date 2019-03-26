using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class CyanSwatch : ISwatch
    {
        public static Color Cyan50 { get; } = (Color)ColorConverter.ConvertFromString("#E0F7FA");
        public static Color Cyan100 { get; } = (Color)ColorConverter.ConvertFromString("#B2EBF2");
        public static Color Cyan200 { get; } = (Color)ColorConverter.ConvertFromString("#80DEEA");
        public static Color Cyan300 { get; } = (Color)ColorConverter.ConvertFromString("#4DD0E1");
        public static Color Cyan400 { get; } = (Color)ColorConverter.ConvertFromString("#26C6DA");
        public static Color Cyan500 { get; } = (Color)ColorConverter.ConvertFromString("#00BCD4");
        public static Color Cyan600 { get; } = (Color)ColorConverter.ConvertFromString("#00ACC1");
        public static Color Cyan700 { get; } = (Color)ColorConverter.ConvertFromString("#0097A7");
        public static Color Cyan800 { get; } = (Color)ColorConverter.ConvertFromString("#00838F");
        public static Color Cyan900 { get; } = (Color)ColorConverter.ConvertFromString("#006064");
        public static Color CyanA100 { get; } = (Color)ColorConverter.ConvertFromString("#84FFFF");
        public static Color CyanA200 { get; } = (Color)ColorConverter.ConvertFromString("#18FFFF");
        public static Color CyanA400 { get; } = (Color)ColorConverter.ConvertFromString("#00E5FF");
        public static Color CyanA700 { get; } = (Color)ColorConverter.ConvertFromString("#00B8D4");

        public string Name { get; } = "Cyan";

        public IDictionary<MaterialDesignColor, Color> Lookup { get; } = new Dictionary<MaterialDesignColor, Color>
        {
            { MaterialDesignColor.Cyan50, Cyan50 },
            { MaterialDesignColor.Cyan100, Cyan100 },
            { MaterialDesignColor.Cyan200, Cyan200 },
            { MaterialDesignColor.Cyan300, Cyan300 },
            { MaterialDesignColor.Cyan400, Cyan400 },
            { MaterialDesignColor.Cyan500, Cyan500 },
            { MaterialDesignColor.Cyan600, Cyan600 },
            { MaterialDesignColor.Cyan700, Cyan700 },
            { MaterialDesignColor.Cyan800, Cyan800 },
            { MaterialDesignColor.Cyan900, Cyan900 },
            { MaterialDesignColor.CyanA100, CyanA100 },
            { MaterialDesignColor.CyanA200, CyanA200 },
            { MaterialDesignColor.CyanA400, CyanA400 },
            { MaterialDesignColor.CyanA700, CyanA700 },
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}
