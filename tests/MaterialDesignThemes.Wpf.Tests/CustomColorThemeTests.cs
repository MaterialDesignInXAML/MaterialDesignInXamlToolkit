using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Tests;

public class CustomColorThemeTests
{
    [Test, STAThreadExecutor]
    [MethodDataSource(nameof(GetThemeValues))]
    public async Task WhenValueIsMissingThemeIsNotSet(BaseTheme? baseTheme, Color? primaryColor, Color? secondaryColor)
    {
        //Arrange
        var bundledTheme = new CustomColorTheme();

        //Act
        bundledTheme.BaseTheme = baseTheme;
        bundledTheme.PrimaryColor = primaryColor;
        bundledTheme.SecondaryColor = secondaryColor;

        //Assert
        await Assert.That(bundledTheme.GetTheme).ThrowsExactly<InvalidOperationException>();
    }

    public static IEnumerable<(BaseTheme?, Color?, Color?)> GetThemeValues()
    {
        yield return (null, null, null);
        yield return (BaseTheme.Light, null, null);
        yield return (BaseTheme.Inherit, null, null);
        yield return (null, Colors.Blue, null);
        yield return (BaseTheme.Light, Colors.Blue, null);
        yield return (BaseTheme.Inherit, Colors.Blue, null);
        yield return (null, null, Colors.Blue);
        yield return (BaseTheme.Light, null, Colors.Blue);
        yield return (BaseTheme.Inherit, null, Colors.Blue);
    }

    [Test, STAThreadExecutor]
    public async Task WhenAllValuesAreSetThemeIsSet()
    {
        //Arrange
        var bundledTheme = new CustomColorTheme();

        //Act
        bundledTheme.BaseTheme = BaseTheme.Light;
        bundledTheme.PrimaryColor = Colors.Fuchsia;
        bundledTheme.SecondaryColor = Colors.Lime;

        //Assert
        Theme theme = bundledTheme.GetTheme();
        await Assert.That(theme.PrimaryMid.Color).IsEqualTo(Colors.Fuchsia);
        await Assert.That(theme.SecondaryMid.Color).IsEqualTo(Colors.Lime);

        var lightTheme = new Theme();
        lightTheme.SetLightTheme();
        await Assert.That(theme.Foreground).IsEqualTo(lightTheme.Foreground);
        await Assert.That(theme.Foreground).IsNotDefault();
    }
}
