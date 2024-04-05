using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class TextFieldPrefixTextVisibilityConverter : IMultiValueConverter
{
    private static readonly object DefaultVisibility = Visibility.Collapsed;

    public Visibility HiddenState { get; set; } = Visibility.Hidden;

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is not
            [
                bool isHintInFloatingPosition,
                string prefixText,
                PrefixSuffixVisibility prefixSuffixVisibility,
                bool isKeyboardFocusWithin,
                bool isEditable
            ])
        {
            return DefaultVisibility;
        }
        if (string.IsNullOrEmpty(prefixText))
        {
            return Visibility.Collapsed;
        }
        if (prefixSuffixVisibility == PrefixSuffixVisibility.Always)
        {
            return Visibility.Visible;
        }
        return isHintInFloatingPosition || isKeyboardFocusWithin || !isEditable ? Visibility.Visible : HiddenState;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
