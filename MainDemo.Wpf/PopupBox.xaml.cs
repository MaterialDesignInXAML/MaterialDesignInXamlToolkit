using System.Collections;
using System.Globalization;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo;

/// <summary>
/// Interaction logic for PopupBox.xaml
/// </summary>
public partial class PopupBox : UserControl
{
    public const string DefaultStyleContentKey = nameof(DefaultStyleContentKey);
    public const string MultiFloatingActionStyleContentKey = nameof(MultiFloatingActionStyleContentKey);

    private readonly IEnumerable _defaultStyleContent;
    private readonly IEnumerable _multiFloatingActionStyleContentKey;

    public PopupBox()
    {
        InitializeComponent();

        _defaultStyleContent = (IEnumerable)FindResource(DefaultStyleContentKey);
        _multiFloatingActionStyleContentKey = (IEnumerable)FindResource(MultiFloatingActionStyleContentKey);

        Loaded += (sender, args) => StyleComboBox.SelectedIndex = 0;
    }

    private void StyleComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ComboBoxItem selectedItem = (ComboBoxItem) StyleComboBox.SelectedItem;
        if (Equals(selectedItem.Content, "MaterialDesignPopupBox"))
        {
            ContentComboBox.ItemsSource = _defaultStyleContent;
        }
        else
        {
            ContentComboBox.ItemsSource = _multiFloatingActionStyleContentKey;
        }
        ContentComboBox.SelectedIndex = 0;
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
