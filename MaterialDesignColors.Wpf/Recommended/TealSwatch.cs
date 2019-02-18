using System.Collections.Generic;
using System.Windows.Media;
using MaterialDesignColors.Wpf;

namespace MaterialDesignColors.Recommended
{
	public class TealSwatch : ISwatch
	{
		public static Color Teal50 { get; } = (Color)ColorConverter.ConvertFromString("#E0F2F1");
		public static Color Teal100 { get; } = (Color)ColorConverter.ConvertFromString("#B2DFDB");
		public static Color Teal200 { get; } = (Color)ColorConverter.ConvertFromString("#80CBC4");
		public static Color Teal300 { get; } = (Color)ColorConverter.ConvertFromString("#4DB6AC");
		public static Color Teal400 { get; } = (Color)ColorConverter.ConvertFromString("#26A69A");
		public static Color Teal500 { get; } = (Color)ColorConverter.ConvertFromString("#009688");
		public static Color Teal600 { get; } = (Color)ColorConverter.ConvertFromString("#00897B");
		public static Color Teal700 { get; } = (Color)ColorConverter.ConvertFromString("#00796B");
		public static Color Teal800 { get; } = (Color)ColorConverter.ConvertFromString("#00695C");
		public static Color Teal900 { get; } = (Color)ColorConverter.ConvertFromString("#004D40");
		public static Color TealA100 { get; } = (Color)ColorConverter.ConvertFromString("#A7FFEB");
		public static Color TealA200 { get; } = (Color)ColorConverter.ConvertFromString("#64FFDA");
		public static Color TealA400 { get; } = (Color)ColorConverter.ConvertFromString("#1DE9B6");
		public static Color TealA700 { get; } = (Color)ColorConverter.ConvertFromString("#00BFA5");

		public string Name { get; } = "Teal";

		public IDictionary<MaterialDesignColor, Color> Lookup { get; } = new Dictionary<MaterialDesignColor, Color>
		{
			{ MaterialDesignColor.Teal50, Teal50 },
			{ MaterialDesignColor.Teal100, Teal100 },
			{ MaterialDesignColor.Teal200, Teal200 },
			{ MaterialDesignColor.Teal300, Teal300 },
			{ MaterialDesignColor.Teal400, Teal400 },
			{ MaterialDesignColor.Teal500, Teal500 },
			{ MaterialDesignColor.Teal600, Teal600 },
			{ MaterialDesignColor.Teal700, Teal700 },
			{ MaterialDesignColor.Teal800, Teal800 },
			{ MaterialDesignColor.Teal900, Teal900 },
			{ MaterialDesignColor.TealA100, TealA100 },
			{ MaterialDesignColor.TealA200, TealA200 },
			{ MaterialDesignColor.TealA400, TealA400 },
			{ MaterialDesignColor.TealA700, TealA700 },
		};

		public IEnumerable<Color> Hues => Lookup.Values;
	}
}
