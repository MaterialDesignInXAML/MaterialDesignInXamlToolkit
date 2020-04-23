using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class SnackbarActionButtonPlacementModeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var mode = (SnackbarActionButtonPlacementMode) values[0];
            var snackbarHeight = (double) values[1];
            return mode switch
            {
                SnackbarActionButtonPlacementMode.Auto when snackbarHeight > 53 => Dock.Bottom,
                SnackbarActionButtonPlacementMode.Auto => Dock.Right,
                SnackbarActionButtonPlacementMode.Inline => Dock.Right,
                SnackbarActionButtonPlacementMode.SeparateLine => Dock.Bottom,
                _ => Dock.Right
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => new[] { Binding.DoNothing, Binding.DoNothing };
    }
}