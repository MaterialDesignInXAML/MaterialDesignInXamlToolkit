using MaterialDesignDemo.Domain;

namespace MaterialDesignDemo;

/// <summary>
/// Interaction logic for SmartHint.xaml
/// </summary>
public partial class SmartHint : UserControl
{
    public SmartHint()
    {
        DataContext = new SmartHintViewModel();
        InitializeComponent();
    }
}
