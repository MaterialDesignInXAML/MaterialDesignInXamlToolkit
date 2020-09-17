using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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

            foreach (var element in Controls)
            {
                element.SetBinding(HintAssist.HintProperty, new Binding(nameof(TextBox.Text)) { ElementName = nameof(HintTextBox) });
                element.SetBinding(TextFieldAssist.HasClearButtonProperty, new Binding(nameof(CheckBox.IsChecked)) { ElementName = nameof(HasClearButtonCheckBox) });
                element.SetBinding(TextFieldAssist.PrefixTextProperty, new Binding(nameof(TextBox.Text)) { ElementName = nameof(PrefixTextBox) });
                element.SetBinding(TextFieldAssist.SuffixTextProperty, new Binding(nameof(TextBox.Text)) { ElementName = nameof(SuffixTextBox) });
                element.VerticalAlignment = VerticalAlignment.Top;
                element.Margin = new Thickness(2, 5, 2, 5);
                SetValue(element);
            }
        }

        private static void SetValue(FrameworkElement element)
        {
            switch (element)
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
            }
        }
    }
}