using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
	public class GreySwatch : ISwatch
	{
		public static Color Grey50 { get; } = (Color)ColorConverter.ConvertFromString("#FAFAFA");
		public static Color Grey100 { get; } = (Color)ColorConverter.ConvertFromString("#F5F5F5");
		public static Color Grey200 { get; } = (Color)ColorConverter.ConvertFromString("#EEEEEE");
		public static Color Grey300 { get; } = (Color)ColorConverter.ConvertFromString("#E0E0E0");
		public static Color Grey400 { get; } = (Color)ColorConverter.ConvertFromString("#BDBDBD");
		public static Color Grey500 { get; } = (Color)ColorConverter.ConvertFromString("#9E9E9E");
		public static Color Grey600 { get; } = (Color)ColorConverter.ConvertFromString("#757575");
		public static Color Grey700 { get; } = (Color)ColorConverter.ConvertFromString("#616161");
		public static Color Grey800 { get; } = (Color)ColorConverter.ConvertFromString("#424242");
		public static Color Grey900 { get; } = (Color)ColorConverter.ConvertFromString("#212121");

		public string Name { get; } = "Grey";

		public IEnumerable<Color> Hues { get; } = new[]
		{
			Grey50,
			Grey100,
			Grey200,
			Grey300,
			Grey400,
			Grey500,
			Grey600,
			Grey700,
			Grey800,
			Grey900,
		};
	};
};
