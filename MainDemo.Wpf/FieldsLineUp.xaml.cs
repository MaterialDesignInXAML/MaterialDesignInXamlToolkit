using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace MaterialDesignDemo
{
    public partial class FieldsLineUp
    {
        private IEnumerable<Control> Controls => FieldsLineUpGrid.Children.OfType<Control>();

        private class ValidationErrorRule : ValidationRule
        {
            public string ErrorMessage { get; set; }

            public override ValidationResult Validate(object value, CultureInfo cultureInfo) => ErrorMessage != null
                ? new ValidationResult(false, ErrorMessage)
                : ValidationResult.ValidResult;
        }

        private static readonly ValidationErrorRule ValidationRule = new ValidationErrorRule();

        public string StringValue { get; set; } = "Text";
        public int IntValue { get; set; } = 0;
        public DateTime? DateTimeValue { get; set; } = DateTime.Now;

        private static readonly Dictionary<Type, string> BindingPaths = new Dictionary<Type, string>
        {
            [typeof(string)] = nameof(StringValue),
            [typeof(int)] = nameof(IntValue),
            [typeof(DateTime?)] = nameof(DateTimeValue)
        };

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
                var error = ValidationErrorTextBox.Text.Trim();
                ValidationRule.ErrorMessage = string.IsNullOrEmpty(error) ? null : error;
                foreach (var control in Controls)
                {
                    var property = GetValueProperty(control);
                    if (property != null)
                        control.GetBindingExpression(property)?.UpdateSource();
                }
            };

            foreach (var control in Controls)
            {
                control.SetBinding(HintAssist.HintProperty, new Binding(nameof(TextBox.Text)) { ElementName = nameof(HintTextBox) });
                control.SetBinding(HintAssist.HelperTextProperty, new Binding(nameof(TextBox.Text)) { ElementName = nameof(HelperTextBox) });
                control.SetBinding(TextFieldAssist.HasClearButtonProperty, new Binding(nameof(CheckBox.IsChecked)) { ElementName = nameof(HasClearButtonCheckBox) });
                control.SetBinding(TextFieldAssist.PrefixTextProperty, new Binding(nameof(TextBox.Text)) { ElementName = nameof(PrefixTextBox) });
                control.SetBinding(TextFieldAssist.SuffixTextProperty, new Binding(nameof(TextBox.Text)) { ElementName = nameof(SuffixTextBox) });
                control.VerticalAlignment = VerticalAlignment.Top;
                control.Margin = new Thickness(2, 5, 2, 5);
                SetValue(control);
            }
        }

        private static void SetValue(Control control)
        {
            switch (control)
            {
                case PasswordBox passwordBox:
                    passwordBox.Password = nameof(PasswordBox.Password);
                    break;
                case ComboBox comboBox:
                    foreach (var number in Enumerable.Range(1, 5))
                        comboBox.Items.Add(new ComboBoxItem { Content = nameof(ComboBox.Text) + number });
                    SetBinding(comboBox);
                    break;
                default:
                    SetBinding(control);
                    break;
            }
        }

        private static DependencyProperty GetValueProperty(Control control) => control switch
        {
            TextBox _ => TextBox.TextProperty,
            PasswordBox _ => null,
            ComboBox _ => Selector.SelectedIndexProperty,
            DatePicker _ => DatePicker.SelectedDateProperty,
            TimePicker _ => TimePicker.SelectedTimeProperty,
            _ => null
        };

        private static void SetBinding(Control control)
        {
            var property = GetValueProperty(control);
            if (property == null)
                return;
            control.SetBinding(property, new Binding(BindingPaths[property.PropertyType])
            {
                Mode = BindingMode.TwoWay,
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(FieldsLineUp), 1),
                ValidationRules = { ValidationRule },
                ValidatesOnDataErrors = true
            });
        }
    }
}