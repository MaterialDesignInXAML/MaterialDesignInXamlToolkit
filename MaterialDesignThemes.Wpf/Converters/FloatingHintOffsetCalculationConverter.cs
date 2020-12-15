using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters
{
    internal class FloatingHintOffsetCalculationConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
        {
            var hintHeight = ((FontFamily)values[0]).LineSpacing
                             * (double)values[1]; // fontSize
            var floatingHintHeight = hintHeight
                                     * (double)values[2]; // floatingScale

            var offset = (values.Length > 3 ? values[3] : null) switch
            {
                Thickness padding => floatingHintHeight / 2 + padding.Top,
                double parentHeight => (parentHeight - hintHeight + floatingHintHeight) / 2,
                _ => floatingHintHeight
            };

            if (targetType == typeof(Point)) // offset
                return new Point(0, -offset);
            if (targetType == typeof(Thickness)) // margin
                return new Thickness(0, offset, 0, 0);
            throw new NotSupportedException(targetType.FullName);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}