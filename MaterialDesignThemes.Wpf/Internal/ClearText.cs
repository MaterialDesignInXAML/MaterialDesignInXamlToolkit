namespace MaterialDesignThemes.Wpf.Internal;

public static class ClearText
{
    public static readonly RoutedCommand ClearCommand = new();

    public static bool GetHandlesClearCommand(DependencyObject obj)
        => (bool)obj.GetValue(HandlesClearCommandProperty);

    public static void SetHandlesClearCommand(DependencyObject obj, bool value)
        => obj.SetValue(HandlesClearCommandProperty, value);

    public static readonly DependencyProperty HandlesClearCommandProperty =
        DependencyProperty.RegisterAttached("HandlesClearCommand", typeof(bool), typeof(ClearText), new PropertyMetadata(false, OnHandlesClearCommandChanged));

    private static void OnHandlesClearCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is UIElement element)
        {
            if ((bool)e.NewValue)
            {
                element.CommandBindings.Add(new CommandBinding(ClearCommand, OnClearCommand));
            }
            else
            {
                for (int i = element.CommandBindings.Count - 1; i >= 0; i--)
                {
                    if (element.CommandBindings[i].Command == ClearCommand)
                    {
                        element.CommandBindings.RemoveAt(i);
                    }
                }
            }
        }

        static void OnClearCommand(object sender, ExecutedRoutedEventArgs e)
        {
            switch (sender)
            {
                case DatePickerTextBox datePickerTextBox:
                    if (datePickerTextBox.GetVisualAncestry().OfType<DatePicker>().FirstOrDefault() is { } datePicker)
                    {
                        datePicker.SetCurrentValue(DatePicker.SelectedDateProperty, null);
                        datePicker.Text = string.Empty; // Clears the text in the DatePickerTextBox which could contain uncommitted text
                    }
                    break;
                case TimePickerTextBox timePickerTextBox:
                    if (timePickerTextBox.GetVisualAncestry().OfType<TimePicker>().FirstOrDefault() is { } timePicker)
                    {
                        timePicker.SetCurrentValue(TimePicker.SelectedTimeProperty, null);
                        timePicker.Clear(); // Clears the text in the TimePickerTextBox which could contain uncommitted text
                    }
                    break;
                case TextBox textBox:
                    textBox.SetCurrentValue(TextBox.TextProperty, null);
                    break;
                case ComboBox comboBox:
                    comboBox.SetCurrentValue(ComboBox.TextProperty, null);
                    comboBox.SetCurrentValue(Selector.SelectedItemProperty, null);
                    break;
                case RichTextBox richTextBox:
                    richTextBox.Document.Blocks.Clear();
                    break;
                case PasswordBox passwordBox:
                    passwordBox.Password = null;
                    break;
            }
            e.Handled = true;
        }
    }

}
