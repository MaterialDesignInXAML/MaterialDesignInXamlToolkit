using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class ElevationMarginConverter : IValueConverter
{
    public static readonly ElevationMarginConverter Instance = new();
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Elevation elevation && elevation != Elevation.Dp0)
        {
            return new Thickness(ElevationInfo.GetDropShadow(elevation)!.BlurRadius);
        }
        return new Thickness(0);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
