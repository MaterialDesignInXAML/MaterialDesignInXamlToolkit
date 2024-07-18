namespace MaterialDesignThemes.Wpf;

public class ChipsTextBox : TextBox
{
    static ChipsTextBox()
        => DefaultStyleKeyProperty.OverrideMetadata(typeof(ChipsTextBox), new FrameworkPropertyMetadata(typeof(ChipsTextBox)));


}
