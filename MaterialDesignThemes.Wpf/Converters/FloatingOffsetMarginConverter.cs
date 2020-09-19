using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class FloatingOffsetMarginConverter : IMultiValueConverter
    {
        private readonly FloatingOffsetCalculationConverter _calculator = new FloatingOffsetCalculationConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (_calculator.Convert(values, targetType, parameter, culture) is Point point)
                return new Thickness(0, -point.Y, 0, 0);
            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}