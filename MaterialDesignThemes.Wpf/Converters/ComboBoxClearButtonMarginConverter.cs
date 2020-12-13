using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    internal class ComboBoxClearButtonMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var padding = (Thickness)value;
            return new Thickness(
                0,
                0,
                padding.Right + Constants.ComboBoxArrowSize + Constants.PickerTextBoxInnerButtonSpacing,
                0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}