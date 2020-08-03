using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters
{
    internal class FloatingHintTransformConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null 
                || values.Length != 4 
                || values.Any(v => v == null)
                || !double.TryParse(values[0].ToString(), out double scale)
                || !double.TryParse(values[1].ToString(), out double lower)
                || !double.TryParse(values[2].ToString(), out double upper)
                || !(values[3] is Point floatingOffset))
            {
                return Transform.Identity;
            }

            double result = upper + (lower - upper) * scale;

            var transformGroup = new TransformGroup();
            transformGroup.Children.Add(new ScaleTransform
            {
                ScaleX = result,
                ScaleY = result
            });
            transformGroup.Children.Add(new TranslateTransform
            {
                X = scale * floatingOffset.X,
                Y = scale * floatingOffset.Y
            });
            return transformGroup;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
