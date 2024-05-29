﻿using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters;

public class FloatingHintClippingGridConverter : IMultiValueConverter
{
    public object? Convert(object?[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is not [double actualWidth, double actualHeight, double floatingScale])
        {
            return null;
        }

        RectangleGeometry geometry = new(new Rect(new Point(0, 0), new Size(actualWidth, actualHeight * floatingScale)));
        geometry.Freeze();
        return geometry;
    }

    public object?[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
