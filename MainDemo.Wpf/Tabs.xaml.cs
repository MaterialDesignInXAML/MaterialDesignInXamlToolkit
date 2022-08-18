using MaterialDesignDemo.Domain;

namespace MaterialDesignDemo;

/// <summary>
/// Interaction logic for Tabs.xaml
/// </summary>
public partial class Tabs
{
    public Tabs()
    {
        DataContext = new TabsViewModel();
        InitializeComponent();
    }
}