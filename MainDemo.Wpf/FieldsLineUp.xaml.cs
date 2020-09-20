using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialDesignDemo
{
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
            VerticalPaddingSlider.Value = Controls.OfType<TextBox>().First().Padding.Top;
            VerticalPaddingSlider.ValueChanged += delegate
            {
                var padding = VerticalPaddingSlider.Value;
                foreach (var element in Controls)
                    element.Padding = new Thickness(element.Padding.Left, padding, element.Padding.Right, padding);
            };
            HorizontalPaddingSlider.Value = Controls.OfType<TextBox>().First().Padding.Left;
            HorizontalPaddingSlider.ValueChanged += delegate
            {
                var padding = HorizontalPaddingSlider.Value;
                foreach (var element in Controls)
                    element.Padding = new Thickness(padding, element.Padding.Top, padding, element.Padding.Bottom);
            };

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
                SetValue(control);
            }
        }

        private static void SetValue(Control control)
        {
            switch (control)
            {
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
                default:
                    throw new NotSupportedException(control.GetType().FullName);
            }
        }
    }
}