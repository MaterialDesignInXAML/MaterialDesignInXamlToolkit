using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
    public class RedSwatch : ISwatch
    {
        public static Color Red50 { get; } = (Color)ColorConverter.ConvertFromString("#FFEBEE");
        public static Color Red100 { get; } = (Color)ColorConverter.ConvertFromString("#FFCDD2");
        public static Color Red200 { get; } = (Color)ColorConverter.ConvertFromString("#EF9A9A");
        public static Color Red300 { get; } = (Color)ColorConverter.ConvertFromString("#E57373");
        public static Color Red400 { get; } = (Color)ColorConverter.ConvertFromString("#EF5350");
        public static Color Red500 { get; } = (Color)ColorConverter.ConvertFromString("#F44336");
        public static Color Red600 { get; } = (Color)ColorConverter.ConvertFromString("#E53935");
        public static Color Red700 { get; } = (Color)ColorConverter.ConvertFromString("#D32F2F");
        public static Color Red800 { get; } = (Color)ColorConverter.ConvertFromString("#C62828");
        public static Color Red900 { get; } = (Color)ColorConverter.ConvertFromString("#B71C1C");
        public static Color RedA100 { get; } = (Color)ColorConverter.ConvertFromString("#FF8A80");
        public static Color RedA200 { get; } = (Color)ColorConverter.ConvertFromString("#FF5252");
        public static Color RedA400 { get; } = (Color)ColorConverter.ConvertFromString("#FF1744");
        public static Color RedA700 { get; } = (Color)ColorConverter.ConvertFromString("#D50000");

        public string Name { get; } = "Red";

        public IDictionary<MaterialDesignColor, Color> Lookup { get; } = new Dictionary<MaterialDesignColor, Color>
        {
            { MaterialDesignColor.Red50, Red50 },
            { MaterialDesignColor.Red100, Red100 },
            { MaterialDesignColor.Red200, Red200 },
            { MaterialDesignColor.Red300, Red300 },
            { MaterialDesignColor.Red400, Red400 },
            { MaterialDesignColor.Red500, Red500 },
            { MaterialDesignColor.Red600, Red600 },
            { MaterialDesignColor.Red700, Red700 },
            { MaterialDesignColor.Red800, Red800 },
            { MaterialDesignColor.Red900, Red900 },
            { MaterialDesignColor.RedA100, RedA100 },
            { MaterialDesignColor.RedA200, RedA200 },
            { MaterialDesignColor.RedA400, RedA400 },
            { MaterialDesignColor.RedA700, RedA700 },
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}
