using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
	public class IndigoSwatch : ISwatch
	{
		public static Color Indigo50 { get; } = (Color)ColorConverter.ConvertFromString("#E8EAF6");
		public static Color Indigo100 { get; } = (Color)ColorConverter.ConvertFromString("#C5CAE9");
		public static Color Indigo200 { get; } = (Color)ColorConverter.ConvertFromString("#9FA8DA");
		public static Color Indigo300 { get; } = (Color)ColorConverter.ConvertFromString("#7986CB");
		public static Color Indigo400 { get; } = (Color)ColorConverter.ConvertFromString("#5C6BC0");
		public static Color Indigo500 { get; } = (Color)ColorConverter.ConvertFromString("#3F51B5");
		public static Color Indigo600 { get; } = (Color)ColorConverter.ConvertFromString("#3949AB");
		public static Color Indigo700 { get; } = (Color)ColorConverter.ConvertFromString("#303F9F");
		public static Color Indigo800 { get; } = (Color)ColorConverter.ConvertFromString("#283593");
		public static Color Indigo900 { get; } = (Color)ColorConverter.ConvertFromString("#1A237E");
		public static Color IndigoA100 { get; } = (Color)ColorConverter.ConvertFromString("#8C9EFF");
		public static Color IndigoA200 { get; } = (Color)ColorConverter.ConvertFromString("#536DFE");
		public static Color IndigoA400 { get; } = (Color)ColorConverter.ConvertFromString("#3D5AFE");
		public static Color IndigoA700 { get; } = (Color)ColorConverter.ConvertFromString("#304FFE");

		public string Name { get; } = "Indigo";

		public IEnumerable<Color> Hues { get; } = new[]
		{
			Indigo50,
			Indigo100,
			Indigo200,
			Indigo300,
			Indigo400,
			Indigo500,
			Indigo600,
			Indigo700,
			Indigo800,
			Indigo900,
			IndigoA100,
			IndigoA200,
			IndigoA400,
			IndigoA700,
		};
	};
};
