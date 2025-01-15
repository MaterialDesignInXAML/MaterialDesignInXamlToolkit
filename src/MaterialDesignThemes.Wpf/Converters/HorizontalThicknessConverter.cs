using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class HorizontalThicknessConverter : IValueConverter
{
    public static readonly HorizontalThicknessConverter Instance = new();
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Thickness thickness) return Binding.DoNothing;

        return new Thickness(thickness.Left, 0, thickness.Right, 0);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
