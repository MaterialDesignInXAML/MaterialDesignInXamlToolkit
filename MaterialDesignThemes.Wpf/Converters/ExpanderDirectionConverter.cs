using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    internal class ExpanderDirectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ExpandDirection direction &&
                parameter is string values &&
                values.Split(',') is { } directionValues &&
                directionValues.Length == 4)
            {
                int index = direction switch
                {
                    ExpandDirection.Left => 0,
                    ExpandDirection.Up => 1,
                    ExpandDirection.Right => 2,
                    ExpandDirection.Down => 3,
                    _ => throw new InvalidOperationException()
                };
                var converter = TypeDescriptor.GetConverter(targetType);

                return converter.CanConvertFrom(typeof(string)) ?
                       converter.ConvertFromInvariantString(directionValues[index]) :
                       directionValues[index];
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
