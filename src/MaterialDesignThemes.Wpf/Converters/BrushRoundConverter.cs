using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf.Converters;

public class BrushRoundConverter : IValueConverter
{
    public static readonly BrushRoundConverter Instance = new();
    public Brush? HighValue { get; set; } = Brushes.White;

    public Brush? LowValue { get; set; } = Brushes.Black;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not SolidColorBrush solidColorBrush) return null;

        return solidColorBrush.Color.IsLightColor()
            ? HighValue
            : LowValue;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Binding.DoNothing;
}
