using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class NullableToVisibilityConverter : IValueConverter
{
    public static readonly NullableToVisibilityConverter Instance = new();
    public static readonly NullableToVisibilityConverter InverseInstance = new() { NullValue = Visibility.Visible, NotNullValue = Visibility.Hidden };

    public Visibility NullValue { get; set; } = Visibility.Collapsed;
    public Visibility NotNullValue { get; set; } = Visibility.Visible;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value == null ? NullValue : NotNullValue;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => Binding.DoNothing;
}
