using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Theming;

partial class LightTheme
{
    public LightTheme()
    {
        Background = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
        Button.FlatClick = Color.FromArgb(0xFF, 0xDE, 0xDE, 0xDE);
    }
}
