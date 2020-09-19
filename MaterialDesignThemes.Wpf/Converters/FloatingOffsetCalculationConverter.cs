using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class FloatingOffsetCalculationConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue
                || values[1] == DependencyProperty.UnsetValue
                || values[2] == DependencyProperty.UnsetValue)
                return new Point(0, 0);

            var fontFamily = (FontFamily)values[0];
            var fontSize = (double)values[1];
            var floatingScale = (double)values[2];
            return new Point(0, fontFamily.LineSpacing * fontSize * -floatingScale);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}