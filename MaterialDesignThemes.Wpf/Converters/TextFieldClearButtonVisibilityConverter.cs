using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

internal class TextFieldClearButtonVisibilityConverter : IMultiValueConverter
{
    public Visibility ContentEmptyVisibility { get; set; } = Visibility.Hidden;

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is not [bool hasClearButton, bool isContentNullOrEmpty, ..])
        {
            return Visibility.Visible;
        }

        if (!hasClearButton) // TextFieldAssist.HasClearButton
        {
            return Visibility.Collapsed;
        }

        if (isContentNullOrEmpty && values.Length > 2 && values[2] is false) // ComboBox.IsEditable
        {
            return Visibility.Collapsed;
        }

        return isContentNullOrEmpty // Hint.IsContentNullOrEmpty
            ? ContentEmptyVisibility
            : Visibility.Visible;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
