using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters.CircularProgressBar;

public class ArcSizeConverter : IValueConverter
{
    public static readonly ArcSizeConverter Instance = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is double v && (v > 0.0) ? new Size(v / 2, v / 2) : new Point();

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
}
