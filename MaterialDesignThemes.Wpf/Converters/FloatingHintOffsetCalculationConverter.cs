using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class FloatingHintOffsetCalculationConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double height = 0;
            if (values[0] is FontFamily fontFamily
                && values[1] is double fontSize
                && values[2] is double floatingScale)
                height = fontFamily.LineSpacing * fontSize * floatingScale;

            if (values.Length > 3 && values[3] is Thickness padding)
                height = height / 2 + padding.Top;

            if (targetType == typeof(Point)) // offset
                return new Point(0, -height); 
            if (targetType == typeof(Thickness)) // margin
                return new Thickness(0, height, 0, 0);
            throw new NotSupportedException(targetType.FullName);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}