using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

internal class TextFieldClearButtonVisibilityConverter : IMultiValueConverter
{
    public Visibility ContentEmptyVisibility { get; set; } = Visibility.Hidden;

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(bool)values[0]) // TextFieldAssist.HasClearButton
            return Visibility.Collapsed;

        return (bool)values[1] // Hint.IsContentNullOrEmpty
            ? ContentEmptyVisibility
            : Visibility.Visible;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
