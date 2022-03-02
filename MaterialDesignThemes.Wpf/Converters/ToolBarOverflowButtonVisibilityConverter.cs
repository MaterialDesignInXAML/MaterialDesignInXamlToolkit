using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class ToolBarOverflowButtonVisibilityConverter : IMultiValueConverter
    {   
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var overflowMode = (OverflowMode)values[0];
            var hasOverflowItems = (bool)values[1];

            if (overflowMode == OverflowMode.AsNeeded && hasOverflowItems)
            {
                return Visibility.Visible;
            }
            else
            {
                return overflowMode switch
                {
                    OverflowMode.Always => Visibility.Visible,
                    OverflowMode.Never => Visibility.Hidden,
                    _ => Visibility.Hidden,
                };      
            }           
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}