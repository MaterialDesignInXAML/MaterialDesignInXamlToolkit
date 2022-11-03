using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Theming;

partial class DarkTheme
{
    public DarkTheme()
    {
        Background = Color.FromArgb(0xFF, 0x0, 0x0, 0x0);
        Button.FlatClick = Color.FromArgb(0x19, 0x75, 0x75, 0x75);
    }
}
