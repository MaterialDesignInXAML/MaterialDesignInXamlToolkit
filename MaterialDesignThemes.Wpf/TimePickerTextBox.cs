namespace MaterialDesignThemes.Wpf;

public class TimePickerTextBox : TextBox
{
    static TimePickerTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TimePickerTextBox), new FrameworkPropertyMetadata(typeof(TimePickerTextBox)));
    }
}
