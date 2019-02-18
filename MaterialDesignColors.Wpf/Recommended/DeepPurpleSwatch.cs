using System.Collections.Generic;
using System.Windows.Media;
using MaterialDesignColors.Wpf;

namespace MaterialDesignColors.Recommended
{
	public class DeepPurpleSwatch : ISwatch
	{
		public static Color DeepPurple50 { get; } = (Color)ColorConverter.ConvertFromString("#EDE7F6");
		public static Color DeepPurple100 { get; } = (Color)ColorConverter.ConvertFromString("#D1C4E9");
		public static Color DeepPurple200 { get; } = (Color)ColorConverter.ConvertFromString("#B39DDB");
		public static Color DeepPurple300 { get; } = (Color)ColorConverter.ConvertFromString("#9575CD");
		public static Color DeepPurple400 { get; } = (Color)ColorConverter.ConvertFromString("#7E57C2");
		public static Color DeepPurple500 { get; } = (Color)ColorConverter.ConvertFromString("#673AB7");
		public static Color DeepPurple600 { get; } = (Color)ColorConverter.ConvertFromString("#5E35B1");
		public static Color DeepPurple700 { get; } = (Color)ColorConverter.ConvertFromString("#512DA8");
		public static Color DeepPurple800 { get; } = (Color)ColorConverter.ConvertFromString("#4527A0");
		public static Color DeepPurple900 { get; } = (Color)ColorConverter.ConvertFromString("#311B92");
		public static Color DeepPurpleA100 { get; } = (Color)ColorConverter.ConvertFromString("#B388FF");
		public static Color DeepPurpleA200 { get; } = (Color)ColorConverter.ConvertFromString("#7C4DFF");
		public static Color DeepPurpleA400 { get; } = (Color)ColorConverter.ConvertFromString("#651FFF");
		public static Color DeepPurpleA700 { get; } = (Color)ColorConverter.ConvertFromString("#6200EA");

		public string Name { get; } = "Deep Purple";

		public IDictionary<MaterialDesignColor, Color> Lookup { get; } = new Dictionary<MaterialDesignColor, Color>
		{
			{ MaterialDesignColor.DeepPurple50, DeepPurple50 },
			{ MaterialDesignColor.DeepPurple100, DeepPurple100 },
			{ MaterialDesignColor.DeepPurple200, DeepPurple200 },
			{ MaterialDesignColor.DeepPurple300, DeepPurple300 },
			{ MaterialDesignColor.DeepPurple400, DeepPurple400 },
			{ MaterialDesignColor.DeepPurple500, DeepPurple500 },
			{ MaterialDesignColor.DeepPurple600, DeepPurple600 },
			{ MaterialDesignColor.DeepPurple700, DeepPurple700 },
			{ MaterialDesignColor.DeepPurple800, DeepPurple800 },
			{ MaterialDesignColor.DeepPurple900, DeepPurple900 },
			{ MaterialDesignColor.DeepPurpleA100, DeepPurpleA100 },
			{ MaterialDesignColor.DeepPurpleA200, DeepPurpleA200 },
			{ MaterialDesignColor.DeepPurpleA400, DeepPurpleA400 },
			{ MaterialDesignColor.DeepPurpleA700, DeepPurpleA700 },
		};

		public IEnumerable<Color> Hues => Lookup.Values;
	}
}
