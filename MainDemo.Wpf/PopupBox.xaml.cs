using System.Globalization;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo;

/// <summary>
/// Interaction logic for PopupBox.xaml
/// </summary>
public partial class PopupBox : UserControl
{
    public PopupBox()
    {
        InitializeComponent();
    }
}

internal class ComboBoxItemToDataTemplateConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is ComboBoxItem { Tag: DataTemplate template} ? template : null;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

internal class ComboBoxItemToStyleConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is ComboBoxItem { Tag: Style style } ? style : null;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

internal class ComboBoxItemToHelperTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is ComboBoxItem item  ? HintAssist.GetHelperText(item) : null!;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
