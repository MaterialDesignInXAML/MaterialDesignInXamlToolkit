using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class TextFieldPrefixTextVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string prefixText = (string)values[1];
            if(string.IsNullOrEmpty(prefixText))
            {
                return Visibility.Collapsed;
            }

            bool isHintInFloatingPosition = (bool)values[0];
            return isHintInFloatingPosition ? Visibility.Visible : Visibility.Hidden;            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
