using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters.Internal;

public class TabControlNavButtonPanelMarginConverter : IMultiValueConverter
{
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is [Thickness margin, Visibility previousButtonVisibility, Visibility nextButtonVisibility])
        {
            return previousButtonVisibility == Visibility.Collapsed && nextButtonVisibility == Visibility.Collapsed ? new Thickness(0) : margin;
        }
        return new Thickness(0);
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
