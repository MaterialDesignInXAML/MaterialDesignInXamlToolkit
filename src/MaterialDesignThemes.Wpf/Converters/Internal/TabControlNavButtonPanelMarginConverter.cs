using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters.Internal;

public class TabControlNavButtonPanelMarginConverter : IMultiValueConverter
{
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is [double scrollableContentWidthDefault, double scrollableContentWidthNonUniform, double controlWidth, StackPanel navPanel, ..])
        {
            double scrollableContentWidth = Math.Max(scrollableContentWidthDefault, scrollableContentWidthNonUniform);
            double xOffset = Math.Min(controlWidth, scrollableContentWidth + navPanel.ActualWidth) - navPanel.ActualWidth;
            return new Thickness(xOffset, 0, 0, 0);
        }
        return new Thickness(0);
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
