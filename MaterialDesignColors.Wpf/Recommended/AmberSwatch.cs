using System.Collections.Generic;
using System.Windows.Media;
using MaterialDesignColors.Wpf;

namespace MaterialDesignColors.Recommended
{
	public class AmberSwatch : ISwatch
	{
		public static Color Amber50 { get; } = (Color)ColorConverter.ConvertFromString("#FFF8E1");
		public static Color Amber100 { get; } = (Color)ColorConverter.ConvertFromString("#FFECB3");
		public static Color Amber200 { get; } = (Color)ColorConverter.ConvertFromString("#FFE082");
		public static Color Amber300 { get; } = (Color)ColorConverter.ConvertFromString("#FFD54F");
		public static Color Amber400 { get; } = (Color)ColorConverter.ConvertFromString("#FFCA28");
		public static Color Amber500 { get; } = (Color)ColorConverter.ConvertFromString("#FFC107");
		public static Color Amber600 { get; } = (Color)ColorConverter.ConvertFromString("#FFB300");
		public static Color Amber700 { get; } = (Color)ColorConverter.ConvertFromString("#FFA000");
		public static Color Amber800 { get; } = (Color)ColorConverter.ConvertFromString("#FF8F00");
		public static Color Amber900 { get; } = (Color)ColorConverter.ConvertFromString("#FF6F00");
		public static Color AmberA100 { get; } = (Color)ColorConverter.ConvertFromString("#FFE57F");
		public static Color AmberA200 { get; } = (Color)ColorConverter.ConvertFromString("#FFD740");
		public static Color AmberA400 { get; } = (Color)ColorConverter.ConvertFromString("#FFC400");
		public static Color AmberA700 { get; } = (Color)ColorConverter.ConvertFromString("#FFAB00");

		public string Name { get; } = "Amber";

		public IDictionary<MaterialDesignColor, Color> Lookup { get; } = new Dictionary<MaterialDesignColor, Color>
		{
			{ MaterialDesignColor.Amber50, Amber50 },
			{ MaterialDesignColor.Amber100, Amber100 },
			{ MaterialDesignColor.Amber200, Amber200 },
			{ MaterialDesignColor.Amber300, Amber300 },
			{ MaterialDesignColor.Amber400, Amber400 },
			{ MaterialDesignColor.Amber500, Amber500 },
			{ MaterialDesignColor.Amber600, Amber600 },
			{ MaterialDesignColor.Amber700, Amber700 },
			{ MaterialDesignColor.Amber800, Amber800 },
			{ MaterialDesignColor.Amber900, Amber900 },
			{ MaterialDesignColor.AmberA100, AmberA100 },
			{ MaterialDesignColor.AmberA200, AmberA200 },
			{ MaterialDesignColor.AmberA400, AmberA400 },
			{ MaterialDesignColor.AmberA700, AmberA700 },
		};

		public IEnumerable<Color> Hues => Lookup.Values
	}
}
