
namespace MaterialDesignThemes.UITests.Samples.PopupBox;

/// <summary>
/// Interaction logic for PopupBoxWithTemplateSelector.xaml
/// </summary>
public partial class PopupBoxWithTemplateSelector : UserControl
{
    private int _counter;
    public PopupBoxWithTemplateSelector()
    {
        InitializeComponent();
        MyPopupBox.ToggleContent = _counter;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        MyPopupBox.ToggleContent =  ++_counter;
    }
}

public class ColorTemplateSelector : DataTemplateSelector
{
    public DataTemplate? OddTemplate { get; set; }
    public DataTemplate? EvenTemplate { get; set; }

    public override DataTemplate? SelectTemplate(object? item, DependencyObject container)
    {
        if (item is int intValue)
        {
            return intValue % 2 == 0 ? EvenTemplate : OddTemplate;
        }
        return null;
    }
}
