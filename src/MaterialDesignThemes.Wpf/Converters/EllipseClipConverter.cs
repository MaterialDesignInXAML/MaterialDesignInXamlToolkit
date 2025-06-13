using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters;

public class EllipseClipConverter : IMultiValueConverter
{
    public static readonly EllipseClipConverter Instance = new();

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is [double width, double height, ..])
        {
            if (width < 1.0 || height < 1.0)
            {
                return Geometry.Empty;
            }

            Point center = new Point(width / 2.0, height / 2.0);
            double radiusX = width / 2.0;
            double radiusY = height / 2.0;

            EllipseGeometry geometry = new EllipseGeometry(center, radiusX, radiusY);
            geometry.Freeze();

            return geometry;
        }

        return DependencyProperty.UnsetValue;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
