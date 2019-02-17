using System.Collections.Generic;
using System.Windows.Media;
using MaterialDesignColors.Wpf;

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

		public IDictionary<MaterialDesignColor, Color> Lookup { get; } = new Dictionary<MaterialDesignColor, Color>
		{
			{ MaterialDesignColor.DeepOrange50, DeepOrange50 },
			{ MaterialDesignColor.DeepOrange100, DeepOrange100 },
			{ MaterialDesignColor.DeepOrange200, DeepOrange200 },
			{ MaterialDesignColor.DeepOrange300, DeepOrange300 },
			{ MaterialDesignColor.DeepOrange400, DeepOrange400 },
			{ MaterialDesignColor.DeepOrange500, DeepOrange500 },
			{ MaterialDesignColor.DeepOrange600, DeepOrange600 },
			{ MaterialDesignColor.DeepOrange700, DeepOrange700 },
			{ MaterialDesignColor.DeepOrange800, DeepOrange800 },
			{ MaterialDesignColor.DeepOrange900, DeepOrange900 },
			{ MaterialDesignColor.DeepOrangeA100, DeepOrangeA100 },
			{ MaterialDesignColor.DeepOrangeA200, DeepOrangeA200 },
			{ MaterialDesignColor.DeepOrangeA400, DeepOrangeA400 },
			{ MaterialDesignColor.DeepOrangeA700, DeepOrangeA700 },
		};

		public IEnumerable<Color> Hues => Lookup.Values
	}
}
