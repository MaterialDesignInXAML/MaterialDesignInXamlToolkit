using System.Collections.Generic;
using System.Windows.Media;
using MaterialDesignColors.Wpf;

namespace MaterialDesignColors.Recommended
{
	public class LightBlueSwatch : ISwatch
	{
		public static Color LightBlue50 { get; } = (Color)ColorConverter.ConvertFromString("#E1F5FE");
		public static Color LightBlue100 { get; } = (Color)ColorConverter.ConvertFromString("#B3E5FC");
		public static Color LightBlue200 { get; } = (Color)ColorConverter.ConvertFromString("#81D4FA");
		public static Color LightBlue300 { get; } = (Color)ColorConverter.ConvertFromString("#4FC3F7");
		public static Color LightBlue400 { get; } = (Color)ColorConverter.ConvertFromString("#29B6F6");
		public static Color LightBlue500 { get; } = (Color)ColorConverter.ConvertFromString("#03A9F4");
		public static Color LightBlue600 { get; } = (Color)ColorConverter.ConvertFromString("#039BE5");
		public static Color LightBlue700 { get; } = (Color)ColorConverter.ConvertFromString("#0288D1");
		public static Color LightBlue800 { get; } = (Color)ColorConverter.ConvertFromString("#0277BD");
		public static Color LightBlue900 { get; } = (Color)ColorConverter.ConvertFromString("#01579B");
		public static Color LightBlueA100 { get; } = (Color)ColorConverter.ConvertFromString("#80D8FF");
		public static Color LightBlueA200 { get; } = (Color)ColorConverter.ConvertFromString("#40C4FF");
		public static Color LightBlueA400 { get; } = (Color)ColorConverter.ConvertFromString("#00B0FF");
		public static Color LightBlueA700 { get; } = (Color)ColorConverter.ConvertFromString("#0091EA");

		public string Name { get; } = "Light Blue";

		public IDictionary<MaterialDesignColor, Color> Lookup { get; } = new Dictionary<MaterialDesignColor, Color>
		{
			{ MaterialDesignColor.LightBlue50, LightBlue50 },
			{ MaterialDesignColor.LightBlue100, LightBlue100 },
			{ MaterialDesignColor.LightBlue200, LightBlue200 },
			{ MaterialDesignColor.LightBlue300, LightBlue300 },
			{ MaterialDesignColor.LightBlue400, LightBlue400 },
			{ MaterialDesignColor.LightBlue500, LightBlue500 },
			{ MaterialDesignColor.LightBlue600, LightBlue600 },
			{ MaterialDesignColor.LightBlue700, LightBlue700 },
			{ MaterialDesignColor.LightBlue800, LightBlue800 },
			{ MaterialDesignColor.LightBlue900, LightBlue900 },
			{ MaterialDesignColor.LightBlueA100, LightBlueA100 },
			{ MaterialDesignColor.LightBlueA200, LightBlueA200 },
			{ MaterialDesignColor.LightBlueA400, LightBlueA400 },
			{ MaterialDesignColor.LightBlueA700, LightBlueA700 },
		};

		public IEnumerable<Color> Hues => Lookup.Values;
	}
}
