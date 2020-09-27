using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class PickerInnerPaddingConverter : IValueConverter, IMultiValueConverter
    {
        /// <summary>
        /// Sets the left padding for the inner picker button to zero
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is Thickness padding
                ? new Thickness(parameter is double d ? d : 0, padding.Top, padding.Right, padding.Bottom)
                : Binding.DoNothing;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();

        /// <summary>
        /// Adds the width of the inner picker button to the right of inner padding 
        /// </summary>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            => values[0] is Thickness padding && values[1] is double rightAddend
                ? new Thickness(padding.Left, padding.Top, padding.Right + rightAddend, padding.Bottom)
                : Binding.DoNothing;

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}