using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class ElevationRadiusConverter : IValueConverter
{
    public static readonly ElevationRadiusConverter Instance = new();

    public double Multiplier { get; set; } = 1.0;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Elevation elevation && elevation != Elevation.Dp0)
        {
            return ElevationInfo.GetDropShadow(elevation)!.BlurRadius * Multiplier;
        }
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
