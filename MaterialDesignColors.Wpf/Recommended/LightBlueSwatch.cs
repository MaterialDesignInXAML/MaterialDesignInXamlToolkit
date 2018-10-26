using System.Collections.Generic;
using System.Windows.Media;

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

		public IEnumerable<Color> Hues { get; } = new[]
		{
			LightBlue50,
			LightBlue100,
			LightBlue200,
			LightBlue300,
			LightBlue400,
			LightBlue500,
			LightBlue600,
			LightBlue700,
			LightBlue800,
			LightBlue900,
			LightBlueA100,
			LightBlueA200,
			LightBlueA400,
			LightBlueA700,
		};
	};
};
