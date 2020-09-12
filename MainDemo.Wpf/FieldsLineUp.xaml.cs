using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignDemo
{
    public partial class FieldsLineUp
    {
        private IEnumerable<FrameworkElement> Elements => FieldsLineUpGrid.Children
            .Cast<FrameworkElement>()
            .Where(e => !(e is TextBlock));

        public FieldsLineUp()
        {
            InitializeComponent();

            foreach (var element in Elements)
            {
                element.SetValue(HintAssist.HintProperty, "Hint");
                element.VerticalAlignment = VerticalAlignment.Top;
                element.Margin = new Thickness(1, 5, 1, 5);
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