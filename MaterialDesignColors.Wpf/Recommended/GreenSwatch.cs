using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
	public class GreenSwatch : ISwatch
	{
		public static Color Green50 { get; } = (Color)ColorConverter.ConvertFromString("#E8F5E9");
		public static Color Green100 { get; } = (Color)ColorConverter.ConvertFromString("#C8E6C9");
		public static Color Green200 { get; } = (Color)ColorConverter.ConvertFromString("#A5D6A7");
		public static Color Green300 { get; } = (Color)ColorConverter.ConvertFromString("#81C784");
		public static Color Green400 { get; } = (Color)ColorConverter.ConvertFromString("#66BB6A");
		public static Color Green500 { get; } = (Color)ColorConverter.ConvertFromString("#4CAF50");
		public static Color Green600 { get; } = (Color)ColorConverter.ConvertFromString("#43A047");
		public static Color Green700 { get; } = (Color)ColorConverter.ConvertFromString("#388E3C");
		public static Color Green800 { get; } = (Color)ColorConverter.ConvertFromString("#2E7D32");
		public static Color Green900 { get; } = (Color)ColorConverter.ConvertFromString("#1B5E20");
		public static Color GreenA100 { get; } = (Color)ColorConverter.ConvertFromString("#B9F6CA");
		public static Color GreenA200 { get; } = (Color)ColorConverter.ConvertFromString("#69F0AE");
		public static Color GreenA400 { get; } = (Color)ColorConverter.ConvertFromString("#00E676");
		public static Color GreenA700 { get; } = (Color)ColorConverter.ConvertFromString("#00C853");

		public string Name { get; } = "Green";

		public IEnumerable<Color> Hues { get; } = new[]
		{
			Green50,
			Green100,
			Green200,
			Green300,
			Green400,
			Green500,
			Green600,
			Green700,
			Green800,
			Green900,
			GreenA100,
			GreenA200,
			GreenA400,
			GreenA700,
		};
	};
};
