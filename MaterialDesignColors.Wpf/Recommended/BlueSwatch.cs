using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
	public class BlueSwatch : ISwatch
	{
		public static Color Blue50 { get; } = (Color)ColorConverter.ConvertFromString("#E3F2FD");
		public static Color Blue100 { get; } = (Color)ColorConverter.ConvertFromString("#BBDEFB");
		public static Color Blue200 { get; } = (Color)ColorConverter.ConvertFromString("#90CAF9");
		public static Color Blue300 { get; } = (Color)ColorConverter.ConvertFromString("#64B5F6");
		public static Color Blue400 { get; } = (Color)ColorConverter.ConvertFromString("#42A5F5");
		public static Color Blue500 { get; } = (Color)ColorConverter.ConvertFromString("#2196F3");
		public static Color Blue600 { get; } = (Color)ColorConverter.ConvertFromString("#1E88E5");
		public static Color Blue700 { get; } = (Color)ColorConverter.ConvertFromString("#1976D2");
		public static Color Blue800 { get; } = (Color)ColorConverter.ConvertFromString("#1565C0");
		public static Color Blue900 { get; } = (Color)ColorConverter.ConvertFromString("#0D47A1");
		public static Color BlueA100 { get; } = (Color)ColorConverter.ConvertFromString("#82B1FF");
		public static Color BlueA200 { get; } = (Color)ColorConverter.ConvertFromString("#448AFF");
		public static Color BlueA400 { get; } = (Color)ColorConverter.ConvertFromString("#2979FF");
		public static Color BlueA700 { get; } = (Color)ColorConverter.ConvertFromString("#2962FF");

		public string Name { get; } = "Blue";

		public IEnumerable<Color> Hues { get; } = new[]
		{
			Blue50,
			Blue100,
			Blue200,
			Blue300,
			Blue400,
			Blue500,
			Blue600,
			Blue700,
			Blue800,
			Blue900,
			BlueA100,
			BlueA200,
			BlueA400,
			BlueA700,
		};
	};
};
