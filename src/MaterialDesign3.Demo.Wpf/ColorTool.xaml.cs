using MaterialDesignDemo.Shared.Domain;

namespace MaterialDesign3Demo;

public partial class ColorTool
{
    public ColorTool()
    {
        DataContext = new ColorToolViewModel();
        InitializeComponent();
    }
}
