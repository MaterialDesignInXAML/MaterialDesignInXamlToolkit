using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class BlueGreySwatch : ISwatch
    {
        public static Color BlueGrey50 { get; } = (Color)ColorConverter.ConvertFromString("#ECEFF1");
        public static Color BlueGrey100 { get; } = (Color)ColorConverter.ConvertFromString("#CFD8DC");
        public static Color BlueGrey200 { get; } = (Color)ColorConverter.ConvertFromString("#B0BEC5");
        public static Color BlueGrey300 { get; } = (Color)ColorConverter.ConvertFromString("#90A4AE");
        public static Color BlueGrey400 { get; } = (Color)ColorConverter.ConvertFromString("#78909C");
        public static Color BlueGrey500 { get; } = (Color)ColorConverter.ConvertFromString("#607D8B");
        public static Color BlueGrey600 { get; } = (Color)ColorConverter.ConvertFromString("#546E7A");
        public static Color BlueGrey700 { get; } = (Color)ColorConverter.ConvertFromString("#455A64");
        public static Color BlueGrey800 { get; } = (Color)ColorConverter.ConvertFromString("#37474F");
        public static Color BlueGrey900 { get; } = (Color)ColorConverter.ConvertFromString("#263238");

        public string Name { get; } = "Blue Grey";

        public IDictionary<MaterialDesignColor, Color> Lookup { get; } = new Dictionary<MaterialDesignColor, Color>
        {
            { MaterialDesignColor.BlueGrey50, BlueGrey50 },
            { MaterialDesignColor.BlueGrey100, BlueGrey100 },
            { MaterialDesignColor.BlueGrey200, BlueGrey200 },
            { MaterialDesignColor.BlueGrey300, BlueGrey300 },
            { MaterialDesignColor.BlueGrey400, BlueGrey400 },
            { MaterialDesignColor.BlueGrey500, BlueGrey500 },
            { MaterialDesignColor.BlueGrey600, BlueGrey600 },
            { MaterialDesignColor.BlueGrey700, BlueGrey700 },
            { MaterialDesignColor.BlueGrey800, BlueGrey800 },
            { MaterialDesignColor.BlueGrey900, BlueGrey900 },
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}
