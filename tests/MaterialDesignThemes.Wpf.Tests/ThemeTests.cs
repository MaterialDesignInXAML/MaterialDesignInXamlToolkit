using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Tests;

public class ThemeTests
{
    [Fact]
    public void CanSetForegroundWithColor()
    {
        var theme = Theme.Create(BaseTheme.Dark, Colors.Red, Colors.Blue);
        theme.Foreground = Colors.Green;

        Assert.Equal<Color>(Colors.Green, theme.Foreground);
    }

    [Fact]
    public void CanSetForegroundWithThemeColorReference()
    {
        var theme = Theme.Create(BaseTheme.Dark, Colors.Red, Colors.Blue);
        theme.Foreground = ThemeColorReference.PrimaryMid;

        Assert.Equal<Color>(Colors.Red, theme.Foreground);
    }

    [Fact]
    public void CanSetForegroundWithColorReference()
    {
        var theme = Theme.Create(BaseTheme.Dark, Colors.Red, Colors.Blue);
        theme.Foreground = ColorReference.PrimaryMid;

        Assert.Equal<Color>(Colors.Red, theme.Foreground);
    }
}
