using System.Collections.Generic;
using System.Windows.Media;

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

		public IEnumerable<Color> Hues { get; } = new[]
		{
			Amber50,
			Amber100,
			Amber200,
			Amber300,
			Amber400,
			Amber500,
			Amber600,
			Amber700,
			Amber800,
			Amber900,
			AmberA100,
			AmberA200,
			AmberA400,
			AmberA700,
		};
	};
};
