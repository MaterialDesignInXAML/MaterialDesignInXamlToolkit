using System.Collections.Generic;
using System.Windows.Media;

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

		public IEnumerable<Color> Hues { get; } = new[]
		{
			LightGreen50,
			LightGreen100,
			LightGreen200,
			LightGreen300,
			LightGreen400,
			LightGreen500,
			LightGreen600,
			LightGreen700,
			LightGreen800,
			LightGreen900,
			LightGreenA100,
			LightGreenA200,
			LightGreenA400,
			LightGreenA700,
		};
	};
};
