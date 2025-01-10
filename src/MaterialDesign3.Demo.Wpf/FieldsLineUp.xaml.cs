using System.Globalization;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign3Demo;

public partial class FieldsLineUp
{
    private IEnumerable<Control> Controls => FieldsLineUpGrid.Children.OfType<Control>();

    private class ValidationErrorRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
            => value is string errorMessage && !string.IsNullOrWhiteSpace(errorMessage)
                ? new ValidationResult(false, errorMessage)
                : ValidationResult.ValidResult;
    }

    private static readonly ValidationErrorRule ValidationRule = new ValidationErrorRule();

    public FieldsLineUp()
    {
        InitializeComponent();
        HorizontalPaddingSlider.Value = Controls.OfType<TextBox>().First().Padding.Left;
        HorizontalPaddingSlider.ValueChanged += delegate { UpdateThickness(HorizontalPaddingSlider, PaddingProperty, true); };
        VerticalPaddingSlider.Value = Controls.OfType<TextBox>().First().Padding.Top;
        VerticalPaddingSlider.ValueChanged += delegate { UpdateThickness(VerticalPaddingSlider, PaddingProperty, false); };
        HorizontalTextBoxViewMarginSlider.Value = ((Thickness)Controls.OfType<TextBox>().First().GetValue(TextFieldAssist.TextBoxViewMarginProperty)).Left;
        HorizontalTextBoxViewMarginSlider.ValueChanged += delegate { UpdateThickness(HorizontalTextBoxViewMarginSlider, TextFieldAssist.TextBoxViewMarginProperty, true); };

        ValidationErrorTextBox.TextChanged += delegate
        {
            foreach (var control in Controls)
                control.GetBindingExpression(TagProperty)!.UpdateSource();
        };

        foreach (var control in Controls)
        {
            control.SetBinding(HintAssist.HintProperty, new Binding(nameof(TextBox.Text)) { ElementName = nameof(HintTextBox) });
            control.SetBinding(HintAssist.HelperTextProperty, new Binding(nameof(TextBox.Text)) { ElementName = nameof(HelperTextBox) });
            control.SetBinding(TextFieldAssist.HasClearButtonProperty, new Binding(nameof(CheckBox.IsChecked)) { ElementName = nameof(HasClearButtonCheckBox) });
            control.SetBinding(TextFieldAssist.PrefixTextProperty, new Binding(nameof(TextBox.Text)) { ElementName = nameof(PrefixTextBox) });
            control.SetBinding(TextFieldAssist.SuffixTextProperty, new Binding(nameof(TextBox.Text)) { ElementName = nameof(SuffixTextBox) });
            control.SetBinding(TagProperty, new Binding(nameof(TextBox.Text))
            {
                Mode = BindingMode.TwoWay,
                ElementName = nameof(ValidationErrorTextBox),
                ValidationRules = { ValidationRule },
                ValidatesOnDataErrors = true
            });
            control.VerticalAlignment = VerticalAlignment.Top;
            control.Margin = new Thickness(2, 10, 2, 10);
            if (control is ComboBox comboBox)
                comboBox.SetBinding(ComboBox.IsEditableProperty, new Binding(nameof(CheckBox.IsChecked)) { ElementName = nameof(IsEditableCheckBox) });
            if (control is TextBoxBase tb)
                tb.SetBinding(TextBoxBase.IsReadOnlyProperty, new Binding(nameof(CheckBox.IsChecked)) { ElementName = nameof(IsReadOnlyCheckBox) });
            SetValue(control);
        }
    }

    private void UpdateThickness(RangeBase slider, DependencyProperty property, bool horizontal)
    {
        var newValue = slider.Value;
        foreach (var element in Controls)
        {
            var current = (Thickness)element.GetValue(property);
            var updated = horizontal
                ? new Thickness(newValue, current.Top, newValue, current.Bottom)
                : new Thickness(current.Left, newValue, current.Right, newValue);
            element.SetValue(property, updated);
        }
    }

    private static void SetValue(Control control)
    {
        switch (control)
        {
            case MaterialDesignThemes.Wpf.AutoSuggestBox autoSuggestBox:
                autoSuggestBox.Text = nameof(MaterialDesignThemes.Wpf.AutoSuggestBox.Text);
                break;
            case TextBox textBox:
                textBox.Text = nameof(TextBox.Text);
                break;
            case PasswordBox passwordBox:
                passwordBox.Password = nameof(PasswordBox.Password);
                break;
            case ComboBox comboBox:
                foreach (var number in Enumerable.Range(1, 5))
                    comboBox.Items.Add(new ComboBoxItem { Content = nameof(ComboBox.Text) + number });
                comboBox.SelectedIndex = 0;
                break;
            case DatePicker datePicker:
                datePicker.SelectedDate = DateTime.Now;
                break;
            case TimePicker timePicker:
                timePicker.SelectedTime = DateTime.Now;
                break;
            case NumericUpDown numericUpDown:
                numericUpDown.Value = 0;
                break;
            default:
                throw new NotSupportedException(control.GetType().FullName);
        }
    }
}
