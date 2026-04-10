using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters.Internal;

public sealed class UpDownButtonsPaddingConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is not [Thickness padding, double buttonsWidth, ..])
        {
            return DependencyProperty.UnsetValue;
        }

        if (double.IsNaN(buttonsWidth) || double.IsInfinity(buttonsWidth))
        {
            return padding;
        }

        return new Thickness(padding.Left, padding.Top, padding.Right + buttonsWidth, padding.Bottom);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
