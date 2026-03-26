using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters.Internal;

public class TabControlNavButtonPanelVisibilityConverter : IMultiValueConverter
{
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is [bool useNavigationPanel, bool isOverflowing])
        {
            return useNavigationPanel && isOverflowing ? Visibility.Visible : Visibility.Collapsed;
        }
        return Visibility.Collapsed;
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
