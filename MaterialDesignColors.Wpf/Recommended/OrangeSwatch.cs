using System.Collections.Generic;
using System.Windows.Media;
using MaterialDesignColors.Wpf;

namespace MaterialDesignColors.Recommended
{
	public class OrangeSwatch : ISwatch
	{
		public static Color Orange50 { get; } = (Color)ColorConverter.ConvertFromString("#FFF3E0");
		public static Color Orange100 { get; } = (Color)ColorConverter.ConvertFromString("#FFE0B2");
		public static Color Orange200 { get; } = (Color)ColorConverter.ConvertFromString("#FFCC80");
		public static Color Orange300 { get; } = (Color)ColorConverter.ConvertFromString("#FFB74D");
		public static Color Orange400 { get; } = (Color)ColorConverter.ConvertFromString("#FFA726");
		public static Color Orange500 { get; } = (Color)ColorConverter.ConvertFromString("#FF9800");
		public static Color Orange600 { get; } = (Color)ColorConverter.ConvertFromString("#FB8C00");
		public static Color Orange700 { get; } = (Color)ColorConverter.ConvertFromString("#F57C00");
		public static Color Orange800 { get; } = (Color)ColorConverter.ConvertFromString("#EF6C00");
		public static Color Orange900 { get; } = (Color)ColorConverter.ConvertFromString("#E65100");
		public static Color OrangeA100 { get; } = (Color)ColorConverter.ConvertFromString("#FFD180");
		public static Color OrangeA200 { get; } = (Color)ColorConverter.ConvertFromString("#FFAB40");
		public static Color OrangeA400 { get; } = (Color)ColorConverter.ConvertFromString("#FF9100");
		public static Color OrangeA700 { get; } = (Color)ColorConverter.ConvertFromString("#FF6D00");

		public string Name { get; } = "Orange";

		public IDictionary<MaterialDesignColor, Color> Lookup { get; } = new Dictionary<MaterialDesignColor, Color>
		{
			{ MaterialDesignColor.Orange50, Orange50 },
			{ MaterialDesignColor.Orange100, Orange100 },
			{ MaterialDesignColor.Orange200, Orange200 },
			{ MaterialDesignColor.Orange300, Orange300 },
			{ MaterialDesignColor.Orange400, Orange400 },
			{ MaterialDesignColor.Orange500, Orange500 },
			{ MaterialDesignColor.Orange600, Orange600 },
			{ MaterialDesignColor.Orange700, Orange700 },
			{ MaterialDesignColor.Orange800, Orange800 },
			{ MaterialDesignColor.Orange900, Orange900 },
			{ MaterialDesignColor.OrangeA100, OrangeA100 },
			{ MaterialDesignColor.OrangeA200, OrangeA200 },
			{ MaterialDesignColor.OrangeA400, OrangeA400 },
			{ MaterialDesignColor.OrangeA700, OrangeA700 },
		};

		public IEnumerable<Color> Hues => Lookup.Values
	}
}
