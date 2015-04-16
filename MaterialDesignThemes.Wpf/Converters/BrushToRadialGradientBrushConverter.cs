using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters
{
	public class BrushToRadialGradientBrushConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var solidColorBrush = value as SolidColorBrush;
			if (solidColorBrush == null) return Binding.DoNothing;

			return new RadialGradientBrush(solidColorBrush.Color, Colors.Transparent)
			{
				Center = new Point(.5, .5),
				GradientOrigin = new Point(.5, .5),
				RadiusX = .75,
				RadiusY = .75,
				Opacity = .39
			};
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
