using System.Collections.Generic;
using System.Windows.Media;
using MaterialDesignColors.Wpf;

namespace MaterialDesignColors.Recommended
{
	public class LightGreenSwatch : ISwatch
	{
		public static Color LightGreen50 { get; } = (Color)ColorConverter.ConvertFromString("#F1F8E9");
		public static Color LightGreen100 { get; } = (Color)ColorConverter.ConvertFromString("#DCEDC8");
		public static Color LightGreen200 { get; } = (Color)ColorConverter.ConvertFromString("#C5E1A5");
		public static Color LightGreen300 { get; } = (Color)ColorConverter.ConvertFromString("#AED581");
		public static Color LightGreen400 { get; } = (Color)ColorConverter.ConvertFromString("#9CCC65");
		public static Color LightGreen500 { get; } = (Color)ColorConverter.ConvertFromString("#8BC34A");
		public static Color LightGreen600 { get; } = (Color)ColorConverter.ConvertFromString("#7CB342");
		public static Color LightGreen700 { get; } = (Color)ColorConverter.ConvertFromString("#689F38");
		public static Color LightGreen800 { get; } = (Color)ColorConverter.ConvertFromString("#558B2F");
		public static Color LightGreen900 { get; } = (Color)ColorConverter.ConvertFromString("#33691E");
		public static Color LightGreenA100 { get; } = (Color)ColorConverter.ConvertFromString("#CCFF90");
		public static Color LightGreenA200 { get; } = (Color)ColorConverter.ConvertFromString("#B2FF59");
		public static Color LightGreenA400 { get; } = (Color)ColorConverter.ConvertFromString("#76FF03");
		public static Color LightGreenA700 { get; } = (Color)ColorConverter.ConvertFromString("#64DD17");

		public string Name { get; } = "Light Green";

		public IDictionary<MaterialDesignColor, Color> Lookup { get; } = new Dictionary<MaterialDesignColor, Color>
		{
			{ MaterialDesignColor.LightGreen50, LightGreen50 },
			{ MaterialDesignColor.LightGreen100, LightGreen100 },
			{ MaterialDesignColor.LightGreen200, LightGreen200 },
			{ MaterialDesignColor.LightGreen300, LightGreen300 },
			{ MaterialDesignColor.LightGreen400, LightGreen400 },
			{ MaterialDesignColor.LightGreen500, LightGreen500 },
			{ MaterialDesignColor.LightGreen600, LightGreen600 },
			{ MaterialDesignColor.LightGreen700, LightGreen700 },
			{ MaterialDesignColor.LightGreen800, LightGreen800 },
			{ MaterialDesignColor.LightGreen900, LightGreen900 },
			{ MaterialDesignColor.LightGreenA100, LightGreenA100 },
			{ MaterialDesignColor.LightGreenA200, LightGreenA200 },
			{ MaterialDesignColor.LightGreenA400, LightGreenA400 },
			{ MaterialDesignColor.LightGreenA700, LightGreenA700 },
		};

		public IEnumerable<Color> Hues => Lookup.Values
	}
}
