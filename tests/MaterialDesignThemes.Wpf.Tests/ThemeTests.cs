using System.Windows.Media;
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests;

public class ThemeTests
{
    [Test]
    public async Task CanSetForegroundWithColor()
    {
        var theme = Theme.Create(BaseTheme.Dark, Colors.Red, Colors.Blue);
        theme.Foreground = Colors.Green;

        await Assert.That(theme.Foreground).IsEqualTo(Colors.Green);
    }

    [Test]
    public async Task CanSetForegroundWithThemeColorReference()
    {
        var theme = Theme.Create(BaseTheme.Dark, Colors.Red, Colors.Blue);
        theme.Foreground = ThemeColorReference.PrimaryMid;

        await Assert.That(theme.Foreground).IsEqualTo(Colors.Red);
    }

    [Test]
    public void CanSetForegroundWithColorReference()
    {
        var theme = Theme.Create(BaseTheme.Dark, Colors.Red, Colors.Blue);
        theme.Foreground = ColorReference.PrimaryMid;

        Assert.Equal<Color>(Colors.Red, theme.Foreground);
    }
}
