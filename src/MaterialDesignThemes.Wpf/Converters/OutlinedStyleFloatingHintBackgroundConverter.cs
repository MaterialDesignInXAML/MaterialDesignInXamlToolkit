using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters;

[Obsolete("This class is obsolete and will be removed in a future version.")]
public class OutlinedStyleFloatingHintBackgroundConverter : IMultiValueConverter
{
    public object? Convert(object[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is { Length: 2 } &&
            values[0] is Brush hintAssistBrush &&
            values[1] is Brush defaultBackgroundBrush)
        {
            return Equals(HintAssist.DefaultBackground, hintAssistBrush) ? defaultBackgroundBrush : hintAssistBrush;
        }
        return Binding.DoNothing;
    }

    public object[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
