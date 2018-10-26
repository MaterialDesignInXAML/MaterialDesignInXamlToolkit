using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
	public class PurpleSwatch : ISwatch
	{
		public static Color Purple50 { get; } = (Color)ColorConverter.ConvertFromString("#F3E5F5");
		public static Color Purple100 { get; } = (Color)ColorConverter.ConvertFromString("#E1BEE7");
		public static Color Purple200 { get; } = (Color)ColorConverter.ConvertFromString("#CE93D8");
		public static Color Purple300 { get; } = (Color)ColorConverter.ConvertFromString("#BA68C8");
		public static Color Purple400 { get; } = (Color)ColorConverter.ConvertFromString("#AB47BC");
		public static Color Purple500 { get; } = (Color)ColorConverter.ConvertFromString("#9C27B0");
		public static Color Purple600 { get; } = (Color)ColorConverter.ConvertFromString("#8E24AA");
		public static Color Purple700 { get; } = (Color)ColorConverter.ConvertFromString("#7B1FA2");
		public static Color Purple800 { get; } = (Color)ColorConverter.ConvertFromString("#6A1B9A");
		public static Color Purple900 { get; } = (Color)ColorConverter.ConvertFromString("#4A148C");
		public static Color PurpleA100 { get; } = (Color)ColorConverter.ConvertFromString("#EA80FC");
		public static Color PurpleA200 { get; } = (Color)ColorConverter.ConvertFromString("#E040FB");
		public static Color PurpleA400 { get; } = (Color)ColorConverter.ConvertFromString("#D500F9");
		public static Color PurpleA700 { get; } = (Color)ColorConverter.ConvertFromString("#AA00FF");

		public string Name { get; } = "Purple";

		public IEnumerable<Color> Hues { get; } = new[]
		{
			Purple50,
			Purple100,
			Purple200,
			Purple300,
			Purple400,
			Purple500,
			Purple600,
			Purple700,
			Purple800,
			Purple900,
			PurpleA100,
			PurpleA200,
			PurpleA400,
			PurpleA700,
		};
	};
};
