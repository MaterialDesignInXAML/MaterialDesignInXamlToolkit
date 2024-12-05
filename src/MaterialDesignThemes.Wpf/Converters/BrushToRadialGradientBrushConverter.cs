using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters;

public class BrushToRadialGradientBrushConverter : IValueConverter
{
    public static readonly BrushToRadialGradientBrushConverter Instance = new();
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not SolidColorBrush solidColorBrush) return Binding.DoNothing;

        return new RadialGradientBrush(solidColorBrush.Color, Colors.Transparent)
        {
            Center = new Point(.5, .5),
            GradientOrigin = new Point(.5, .5),
            RadiusX = .75,
            RadiusY = .75,
            Opacity = .39
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => Binding.DoNothing;
}
