using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
	public class PinkSwatch : ISwatch
	{
		public static Color Pink50 { get; } = (Color)ColorConverter.ConvertFromString("#FCE4EC");
		public static Color Pink100 { get; } = (Color)ColorConverter.ConvertFromString("#F8BBD0");
		public static Color Pink200 { get; } = (Color)ColorConverter.ConvertFromString("#F48FB1");
		public static Color Pink300 { get; } = (Color)ColorConverter.ConvertFromString("#F06292");
		public static Color Pink400 { get; } = (Color)ColorConverter.ConvertFromString("#EC407A");
		public static Color Pink500 { get; } = (Color)ColorConverter.ConvertFromString("#E91E63");
		public static Color Pink600 { get; } = (Color)ColorConverter.ConvertFromString("#D81B60");
		public static Color Pink700 { get; } = (Color)ColorConverter.ConvertFromString("#C2185B");
		public static Color Pink800 { get; } = (Color)ColorConverter.ConvertFromString("#AD1457");
		public static Color Pink900 { get; } = (Color)ColorConverter.ConvertFromString("#880E4F");
		public static Color PinkA100 { get; } = (Color)ColorConverter.ConvertFromString("#FF80AB");
		public static Color PinkA200 { get; } = (Color)ColorConverter.ConvertFromString("#FF4081");
		public static Color PinkA400 { get; } = (Color)ColorConverter.ConvertFromString("#F50057");
		public static Color PinkA700 { get; } = (Color)ColorConverter.ConvertFromString("#C51162");

		public string Name { get; } = "Pink";

		public IEnumerable<Color> Hues { get; } = new[]
		{
			Pink50,
			Pink100,
			Pink200,
			Pink300,
			Pink400,
			Pink500,
			Pink600,
			Pink700,
			Pink800,
			Pink900,
			PinkA100,
			PinkA200,
			PinkA400,
			PinkA700,
		};
	};
};
