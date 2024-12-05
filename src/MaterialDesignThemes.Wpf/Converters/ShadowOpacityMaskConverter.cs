using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MaterialDesignThemes.Wpf.Converters;

public class ShadowOpacityMaskConverter : IMultiValueConverter
{
    public static readonly ShadowOpacityMaskConverter Instance = new();

    public object? Convert(object[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        static double? GetValidSize(object? value)
            => value is double d && !double.IsNaN(d) && !double.IsInfinity(d) ? d : null;

        static DropShadowEffect? GetDropShadow(object? value) => value switch
        {
            Elevation elevation => ElevationAssist.GetDropShadow(elevation),
            _ => null
        };

        if (values is null
            || values.Length < 3
            || GetValidSize(values[0]) is not { } width
            || GetValidSize(values[1]) is not { } height
            || GetDropShadow(values[2]) is not { } dropShadow)
        {
            return null;
        }

        double blurRadius = dropShadow.BlurRadius;

        var rect = new Rect(
                -blurRadius,
                -blurRadius,
                width + blurRadius + blurRadius,
                height + blurRadius + blurRadius);

        var drawing = new GeometryDrawing(Brushes.White, null, new RectangleGeometry(rect));
        DrawingBrush rv = new(drawing)
        {
            Stretch = Stretch.None,
            TileMode = TileMode.None,
            Viewport = rect,
            ViewportUnits = BrushMappingMode.Absolute,
            Viewbox = rect,
            ViewboxUnits = BrushMappingMode.Absolute
        };
        rv.Freeze();
        return rv;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
