using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MaterialDesignThemes.Uwp.Converters
{
	public class BrushToRadialGradientBrushConverter : IValueConverter
    { 
	    public object Convert(object value, Type targetType, object parameter, string language)
	    {
            var solidColorBrush = value as SolidColorBrush;
            if (solidColorBrush == null) return null;

            return new SolidColorBrush(solidColorBrush.Color);
        }

	    public object ConvertBack(object value, Type targetType, object parameter, string language)
	    {
            return null;
        }
	}
}
