namespace MaterialDesign3Demo;

/// <summary>
/// Interaction logic for ToolTips.xaml
/// </summary>
public partial class ToolTips : UserControl
{
    public ToolTips()
    {
        DataContext = new MaterialDesignDemo.Shared.Domain.ToolTipsViewModel();
        InitializeComponent();
    }
}
