using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters
{
    internal class FloatingHintTransformConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values?.Length == 2 &&
                values[0] is double scale &&
                values[1] is Point floatingOffset)
            {
                return new TranslateTransform {
                    X = scale * floatingOffset.X,
                    Y = scale * floatingOffset.Y
                };
            }
            return Transform.Identity;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
