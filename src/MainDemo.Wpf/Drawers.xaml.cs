using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo;

public partial class Drawers
{
    public Drawers()
    {
        InitializeComponent();
        Drawer_ResetBlur(null!, null!);
    }

    private void Drawer_ResetBlur(object sender, RoutedEventArgs e)
    {
        BlurRadiusSlider.Value = DrawerHost.DefaultBlurRadius;
    }
}
