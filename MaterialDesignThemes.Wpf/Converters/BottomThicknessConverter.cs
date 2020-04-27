using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class BottomThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Thickness thickness))
                return Binding.DoNothing;

            if (parameter == null)
                return thickness.Bottom;

            return thickness.Bottom * System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}