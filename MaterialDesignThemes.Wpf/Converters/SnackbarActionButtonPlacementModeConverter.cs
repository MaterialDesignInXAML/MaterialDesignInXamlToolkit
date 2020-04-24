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
            var inlineMaxHeight = (double) values[1];
            var actualHeight = (double) values[2];
            return mode switch
            {
                SnackbarActionButtonPlacementMode.Auto when actualHeight <= inlineMaxHeight => Dock.Right,
                SnackbarActionButtonPlacementMode.Auto => Dock.Bottom,
                SnackbarActionButtonPlacementMode.Inline => Dock.Right,
                SnackbarActionButtonPlacementMode.SeparateLine => Dock.Bottom,
                _ => Dock.Right
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new InvalidOperationException();
    }
}