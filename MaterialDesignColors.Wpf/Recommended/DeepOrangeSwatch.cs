using System.Collections.Generic;
using System.Windows.Media;

namespace MaterialDesignColors.Recommended
{
	public class DeepOrangeSwatch : ISwatch
	{
		public static Color DeepOrange50 { get; } = (Color)ColorConverter.ConvertFromString("#FBE9E7");
		public static Color DeepOrange100 { get; } = (Color)ColorConverter.ConvertFromString("#FFCCBC");
		public static Color DeepOrange200 { get; } = (Color)ColorConverter.ConvertFromString("#FFAB91");
		public static Color DeepOrange300 { get; } = (Color)ColorConverter.ConvertFromString("#FF8A65");
		public static Color DeepOrange400 { get; } = (Color)ColorConverter.ConvertFromString("#FF7043");
		public static Color DeepOrange500 { get; } = (Color)ColorConverter.ConvertFromString("#FF5722");
		public static Color DeepOrange600 { get; } = (Color)ColorConverter.ConvertFromString("#F4511E");
		public static Color DeepOrange700 { get; } = (Color)ColorConverter.ConvertFromString("#E64A19");
		public static Color DeepOrange800 { get; } = (Color)ColorConverter.ConvertFromString("#D84315");
		public static Color DeepOrange900 { get; } = (Color)ColorConverter.ConvertFromString("#BF360C");
		public static Color DeepOrangeA100 { get; } = (Color)ColorConverter.ConvertFromString("#FF9E80");
		public static Color DeepOrangeA200 { get; } = (Color)ColorConverter.ConvertFromString("#FF6E40");
		public static Color DeepOrangeA400 { get; } = (Color)ColorConverter.ConvertFromString("#FF3D00");
		public static Color DeepOrangeA700 { get; } = (Color)ColorConverter.ConvertFromString("#DD2C00");

		public string Name { get; } = "Deep Orange";

		public IEnumerable<Color> Hues { get; } = new[]
		{
			DeepOrange50,
			DeepOrange100,
			DeepOrange200,
			DeepOrange300,
			DeepOrange400,
			DeepOrange500,
			DeepOrange600,
			DeepOrange700,
			DeepOrange800,
			DeepOrange900,
			DeepOrangeA100,
			DeepOrangeA200,
			DeepOrangeA400,
			DeepOrangeA700,
		};
	};
};
