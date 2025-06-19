using MaterialDesignDemo.Shared.Domain;

namespace MaterialDesignDemo;

/// <summary>
/// Interaction logic for ToolTips.xaml
/// </summary>
public partial class ToolTips : UserControl
{
    public ToolTips()
    {
        DataContext = new ToolTipsViewModel();
        InitializeComponent();
    }
}
