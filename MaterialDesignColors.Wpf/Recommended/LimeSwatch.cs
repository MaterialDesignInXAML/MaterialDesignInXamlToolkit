using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class LimeSwatch : ISwatch
    {
        public static Color Lime50 { get; } = (Color)ColorConverter.ConvertFromString("#F9FBE7");
        public static Color Lime100 { get; } = (Color)ColorConverter.ConvertFromString("#F0F4C3");
        public static Color Lime200 { get; } = (Color)ColorConverter.ConvertFromString("#E6EE9C");
        public static Color Lime300 { get; } = (Color)ColorConverter.ConvertFromString("#DCE775");
        public static Color Lime400 { get; } = (Color)ColorConverter.ConvertFromString("#D4E157");
        public static Color Lime500 { get; } = (Color)ColorConverter.ConvertFromString("#CDDC39");
        public static Color Lime600 { get; } = (Color)ColorConverter.ConvertFromString("#C0CA33");
        public static Color Lime700 { get; } = (Color)ColorConverter.ConvertFromString("#AFB42B");
        public static Color Lime800 { get; } = (Color)ColorConverter.ConvertFromString("#9E9D24");
        public static Color Lime900 { get; } = (Color)ColorConverter.ConvertFromString("#827717");
        public static Color LimeA100 { get; } = (Color)ColorConverter.ConvertFromString("#F4FF81");
        public static Color LimeA200 { get; } = (Color)ColorConverter.ConvertFromString("#EEFF41");
        public static Color LimeA400 { get; } = (Color)ColorConverter.ConvertFromString("#C6FF00");
        public static Color LimeA700 { get; } = (Color)ColorConverter.ConvertFromString("#AEEA00");

        public string Name { get; } = "Lime";

        public IDictionary<MaterialDesignColor, Color> Lookup { get; } = new Dictionary<MaterialDesignColor, Color>
        {
            { MaterialDesignColor.Lime50, Lime50 },
            { MaterialDesignColor.Lime100, Lime100 },
            { MaterialDesignColor.Lime200, Lime200 },
            { MaterialDesignColor.Lime300, Lime300 },
            { MaterialDesignColor.Lime400, Lime400 },
            { MaterialDesignColor.Lime500, Lime500 },
            { MaterialDesignColor.Lime600, Lime600 },
            { MaterialDesignColor.Lime700, Lime700 },
            { MaterialDesignColor.Lime800, Lime800 },
            { MaterialDesignColor.Lime900, Lime900 },
            { MaterialDesignColor.LimeA100, LimeA100 },
            { MaterialDesignColor.LimeA200, LimeA200 },
            { MaterialDesignColor.LimeA400, LimeA400 },
            { MaterialDesignColor.LimeA700, LimeA700 },
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}
