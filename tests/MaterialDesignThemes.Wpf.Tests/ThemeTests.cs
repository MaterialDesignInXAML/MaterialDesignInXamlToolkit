using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Tests;

public class ThemeTests
{
    [Test]
    public async Task CanSetForegroundWithColor()
    {
        var theme = Theme.Create(BaseTheme.Dark, Colors.Red, Colors.Blue);
        theme.Foreground = Colors.Green;

        await Assert.That(theme.Foreground.Color).IsEqualTo(Colors.Green);
    }

    [Test]
    public async Task CanSetForegroundWithThemeColorReference()
    {
        var theme = Theme.Create(BaseTheme.Dark, Colors.Red, Colors.Blue);
        theme.Foreground = ThemeColorReference.PrimaryMid;

        await Assert.That(theme.Foreground.Color).IsEqualTo(Colors.Red);
    }

    [Test]
    public async Task CanSetForegroundWithColorReference()
    {
        var theme = Theme.Create(BaseTheme.Dark, Colors.Red, Colors.Blue);
        theme.Foreground = ColorReference.PrimaryMid;

        await Assert.That(theme.Foreground.Color).IsEqualTo(Colors.Red);
    }
}
