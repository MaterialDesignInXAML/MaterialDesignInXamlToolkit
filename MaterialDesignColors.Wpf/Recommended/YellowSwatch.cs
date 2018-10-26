using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
	public class YellowSwatch : ISwatch
	{
		public static Color Yellow50 { get; } = (Color)ColorConverter.ConvertFromString("#FFFDE7");
		public static Color Yellow100 { get; } = (Color)ColorConverter.ConvertFromString("#FFF9C4");
		public static Color Yellow200 { get; } = (Color)ColorConverter.ConvertFromString("#FFF59D");
		public static Color Yellow300 { get; } = (Color)ColorConverter.ConvertFromString("#FFF176");
		public static Color Yellow400 { get; } = (Color)ColorConverter.ConvertFromString("#FFEE58");
		public static Color Yellow500 { get; } = (Color)ColorConverter.ConvertFromString("#FFEB3B");
		public static Color Yellow600 { get; } = (Color)ColorConverter.ConvertFromString("#FDD835");
		public static Color Yellow700 { get; } = (Color)ColorConverter.ConvertFromString("#FBC02D");
		public static Color Yellow800 { get; } = (Color)ColorConverter.ConvertFromString("#F9A825");
		public static Color Yellow900 { get; } = (Color)ColorConverter.ConvertFromString("#F57F17");
		public static Color YellowA100 { get; } = (Color)ColorConverter.ConvertFromString("#FFFF8D");
		public static Color YellowA200 { get; } = (Color)ColorConverter.ConvertFromString("#FFFF00");
		public static Color YellowA400 { get; } = (Color)ColorConverter.ConvertFromString("#FFEA00");
		public static Color YellowA700 { get; } = (Color)ColorConverter.ConvertFromString("#FFD600");

		public string Name { get; } = "Yellow";

		public IEnumerable<Color> Hues { get; } = new[]
		{
			Yellow50,
			Yellow100,
			Yellow200,
			Yellow300,
			Yellow400,
			Yellow500,
			Yellow600,
			Yellow700,
			Yellow800,
			Yellow900,
			YellowA100,
			YellowA200,
			YellowA400,
			YellowA700,
		};
	};
};
