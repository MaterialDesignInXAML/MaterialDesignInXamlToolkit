using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
	public class LimeSwatch : ISwatch
	{
		public static Color Lime50 { get; } = (Color)ColorConverter.ConvertFromString("#F9FBE7");
		public static Color Lime100 { get; } = (Color)ColorConverter.ConvertFromString("#F0F4C3");
		public static Color Lime200 { get; } = (Color)ColorConverter.ConvertFromString("#E6EE9C");
		public static Color Lime300 { get; } = (Color)ColorConverter.ConvertFromString("#DCE775");
		public static Color Lime400 { get; } = (Color)ColorConverter.ConvertFromString("#D4E157");
		public static Color Lime500 { get; } = (Color)ColorConverter.ConvertFromString("#CDDC39");
		public static Color Lime600 { get; } = (Color)ColorConverter.ConvertFromString("#C0CA33");
		public static Color Lime700 { get; } = (Color)ColorConverter.ConvertFromString("#AFB42B");
		public static Color Lime800 { get; } = (Color)ColorConverter.ConvertFromString("#9E9D24");
		public static Color Lime900 { get; } = (Color)ColorConverter.ConvertFromString("#827717");
		public static Color LimeA100 { get; } = (Color)ColorConverter.ConvertFromString("#F4FF81");
		public static Color LimeA200 { get; } = (Color)ColorConverter.ConvertFromString("#EEFF41");
		public static Color LimeA400 { get; } = (Color)ColorConverter.ConvertFromString("#C6FF00");
		public static Color LimeA700 { get; } = (Color)ColorConverter.ConvertFromString("#AEEA00");

		public string Name { get; } = "Lime";

		public IEnumerable<Color> Hues { get; } = new[]
		{
			Lime50,
			Lime100,
			Lime200,
			Lime300,
			Lime400,
			Lime500,
			Lime600,
			Lime700,
			Lime800,
			Lime900,
			LimeA100,
			LimeA200,
			LimeA400,
			LimeA700,
		};
	};
};
