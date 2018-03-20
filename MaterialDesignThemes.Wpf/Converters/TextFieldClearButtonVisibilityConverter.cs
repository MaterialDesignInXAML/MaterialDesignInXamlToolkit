using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
	public class TextFieldClearButtonVisibilityConverter : IMultiValueConverter
	{    
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool bAllTrue = true;
            foreach(var v in values)
            {
                if (v is bool && !(bool)v)
                    bAllTrue = false;
            }
            if (bAllTrue)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}