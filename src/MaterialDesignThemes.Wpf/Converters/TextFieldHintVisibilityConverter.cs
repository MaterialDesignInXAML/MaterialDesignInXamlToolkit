using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class TextFieldHintVisibilityConverter : IValueConverter
{
    public static readonly TextFieldHintVisibilityConverter IsNotEmptyInstance = new();
    public static readonly TextFieldHintVisibilityConverter IsEmptyInstance = new() { IsEmptyValue = Visibility.Collapsed, IsNotEmptyValue = Visibility.Visible};

    public Visibility IsEmptyValue { get; set; } = Visibility.Visible;
    public Visibility IsNotEmptyValue { get; set; } = Visibility.Hidden;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => string.IsNullOrEmpty((value ?? "").ToString()) ? IsEmptyValue : IsNotEmptyValue;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => Binding.DoNothing;
}
