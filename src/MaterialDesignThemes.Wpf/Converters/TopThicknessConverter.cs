using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

[Obsolete]
internal class TopThicknessConverter : IValueConverter
{
    public static readonly TopThicknessConverter Instance = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is Thickness thickness
            ? new Thickness(0, thickness.Top, 0, 0)
            : Binding.DoNothing;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
