using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class GreySwatch : ISwatch
    {
        public static Color Grey50 { get; } = (Color)ColorConverter.ConvertFromString("#FAFAFA");
        public static Color Grey100 { get; } = (Color)ColorConverter.ConvertFromString("#F5F5F5");
        public static Color Grey200 { get; } = (Color)ColorConverter.ConvertFromString("#EEEEEE");
        public static Color Grey300 { get; } = (Color)ColorConverter.ConvertFromString("#E0E0E0");
        public static Color Grey400 { get; } = (Color)ColorConverter.ConvertFromString("#BDBDBD");
        public static Color Grey500 { get; } = (Color)ColorConverter.ConvertFromString("#9E9E9E");
        public static Color Grey600 { get; } = (Color)ColorConverter.ConvertFromString("#757575");
        public static Color Grey700 { get; } = (Color)ColorConverter.ConvertFromString("#616161");
        public static Color Grey800 { get; } = (Color)ColorConverter.ConvertFromString("#424242");
        public static Color Grey900 { get; } = (Color)ColorConverter.ConvertFromString("#212121");

        public string Name { get; } = "Grey";

        public IDictionary<MaterialDesignColor, Color> Lookup { get; } = new Dictionary<MaterialDesignColor, Color>
        {
            { MaterialDesignColor.Grey50, Grey50 },
            { MaterialDesignColor.Grey100, Grey100 },
            { MaterialDesignColor.Grey200, Grey200 },
            { MaterialDesignColor.Grey300, Grey300 },
            { MaterialDesignColor.Grey400, Grey400 },
            { MaterialDesignColor.Grey500, Grey500 },
            { MaterialDesignColor.Grey600, Grey600 },
            { MaterialDesignColor.Grey700, Grey700 },
            { MaterialDesignColor.Grey800, Grey800 },
            { MaterialDesignColor.Grey900, Grey900 },
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}
