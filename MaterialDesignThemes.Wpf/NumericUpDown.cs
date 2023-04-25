namespace MaterialDesignThemes.Wpf;

public class NumericUpDown : Control
{
    static NumericUpDown()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
    }
}
