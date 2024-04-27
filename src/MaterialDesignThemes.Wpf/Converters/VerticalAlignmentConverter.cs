using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

[Obsolete("This class is obsolete and will be removed in a future version.")]
public class VerticalAlignmentConverter : IValueConverter
{
    public VerticalAlignment StretchReplacement { get; set; } = VerticalAlignment.Top;
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is VerticalAlignment.Stretch ? StretchReplacement : value;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
