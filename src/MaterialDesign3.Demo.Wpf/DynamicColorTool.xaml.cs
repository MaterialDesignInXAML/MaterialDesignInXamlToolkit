using MaterialDesign3Demo.Domain;

namespace MaterialDesign3Demo;

public partial class DynamicColorTool
{
    public DynamicColorTool()
    {
        DataContext = new DynamicColorToolViewModel();
        InitializeComponent();
    }
}
