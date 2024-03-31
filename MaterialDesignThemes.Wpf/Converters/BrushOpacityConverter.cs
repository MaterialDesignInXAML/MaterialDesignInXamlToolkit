using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters;

public class BrushOpacityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is SolidColorBrush brush)
        {
            var opacity = System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
            SolidColorBrush rv = new(brush.Color)
            {
                Opacity = opacity
            };
            rv.Freeze();
            return rv;
        }
        return null;
    }

    public object? ConvertBack(object value, Type targetType, object? parameter, CultureInfo culture)
        => Binding.DoNothing;
}
