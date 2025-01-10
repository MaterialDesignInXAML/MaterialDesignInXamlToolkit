using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using MaterialDesignDemo.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo;

/// <summary>
/// Interaction logic for SmartHint.xaml
/// </summary>
public partial class SmartHint : UserControl
{
    // Attached property used for binding in the RichTextBox to enable triggering of validation errors.
    internal static readonly DependencyProperty RichTextBoxTextProperty = DependencyProperty.RegisterAttached(
        "RichTextBoxText", typeof(object), typeof(SmartHint), new PropertyMetadata(null));
    internal static void SetRichTextBoxText(DependencyObject element, object value) => element.SetValue(RichTextBoxTextProperty, value);
    internal static object GetRichTextBoxText(DependencyObject element) => element.GetValue(RichTextBoxTextProperty);

    public SmartHint()
    {
        DataContext = new SmartHintViewModel();
        InitializeComponent();
    }

    private void HasErrors_OnToggled(object sender, RoutedEventArgs e)
    {
        CheckBox c = (CheckBox) sender;

        foreach (InputElementContentControl container in FindVisualChildren<InputElementContentControl>(ControlsPanel))
        {
            FrameworkElement element = (FrameworkElement) container.Content;

            var binding = GetBinding(element);
            if (binding is null)
                continue;

            if (c.IsChecked.GetValueOrDefault(false))
            {
                var error = new ValidationError(new ExceptionValidationRule(), binding)
                {
                    ErrorContent = "Invalid input, please fix it!"
                };
                Validation.MarkInvalid(binding, error);
            }
            else
            {
                Validation.ClearInvalid(binding);
            }
        }
    }

    private static BindingExpression? GetBinding(FrameworkElement element)
    {
        if (element is TextBox textBox)
            return textBox.GetBindingExpression(TextBox.TextProperty);
        if (element is RichTextBox richTextBox)
            return richTextBox.GetBindingExpression(RichTextBoxTextProperty);
        if (element is PasswordBox passwordBox)
            return passwordBox.GetBindingExpression(PasswordBoxAssist.PasswordProperty);
        if (element is ComboBox comboBox)
            return comboBox.GetBindingExpression(ComboBox.TextProperty);
        if (element is DatePicker datePicker)
            return datePicker.GetBindingExpression(DatePicker.TextProperty);
        if (element is TimePicker timePicker)
            return timePicker.GetBindingExpression(TimePicker.TextProperty);
        return default;
    }

    private static IEnumerable<T> FindVisualChildren<T>(DependencyObject? dependencyObject) where T : DependencyObject
    {
        if (dependencyObject is null)
            yield break;

        if (dependencyObject is T obj)
            yield return obj;

        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(dependencyObject, i);
            foreach (T childOfChild in FindVisualChildren<T>(child))
            {
                yield return childOfChild;
            }
        }
    }
}

internal class CustomPaddingConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        Thickness? defaultPadding = values[0] as Thickness?;
        bool applyCustomPadding = (bool) values[1];
        Thickness customPadding = (Thickness) values[2];
        return applyCustomPadding ? customPadding : defaultPadding ?? DependencyProperty.UnsetValue;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

internal class FontSizeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        double fontSize = (double) value!;
        return double.IsNaN(fontSize) ? DependencyProperty.UnsetValue : fontSize;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

internal abstract class OptionToStringConverter<TOptionType> : IValueConverter where TOptionType : notnull 
{
    public TOptionType? DefaultValue { get; set; }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Equals(value, DefaultValue) ? "Default" : ToString((TOptionType)value!, culture);

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();

    protected abstract string ToString(TOptionType value, CultureInfo culture);
}

internal class DoubleOptionToStringConverter : OptionToStringConverter<double>
{
    protected override string ToString(double value, CultureInfo culture) => value.ToString(culture);
}

internal class PointOptionToStringConverter : OptionToStringConverter<Point>
{
    protected override string ToString(Point value, CultureInfo culture) => value.ToString(culture);
}

internal class FontFamilyOptionToStringConverter : OptionToStringConverter<FontFamily>
{
    protected override string ToString(FontFamily value, CultureInfo culture) => value.Source.ToString(culture);
}
