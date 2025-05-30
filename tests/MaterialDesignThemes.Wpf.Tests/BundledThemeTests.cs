using MaterialDesignColors;

namespace MaterialDesignThemes.Wpf.Tests;

public class BundledThemeTests
{
    [Test, STAThreadExecutor]
    [Arguments(null, null, null)]
    [Arguments(BaseTheme.Light, null, null)]
    [Arguments(BaseTheme.Inherit, null, null)]
    [Arguments(null, PrimaryColor.Blue, null)]
    [Arguments(BaseTheme.Light, PrimaryColor.Blue, null)]
    [Arguments(BaseTheme.Inherit, PrimaryColor.Blue, null)]
    [Arguments(null, null, SecondaryColor.Blue)]
    [Arguments(BaseTheme.Light, null, SecondaryColor.Blue)]
    [Arguments(BaseTheme.Inherit, null, SecondaryColor.Blue)]
    public async Task WhenValueIsMissingThemeIsNotSet(BaseTheme? baseTheme, PrimaryColor? primaryColor, SecondaryColor? secondaryColor)
    {
        //Arrange
        var bundledTheme = new BundledTheme();

        //Act
        bundledTheme.BaseTheme = baseTheme;
        bundledTheme.PrimaryColor = primaryColor;
        bundledTheme.SecondaryColor = secondaryColor;

        //Assert
        await Assert.That(() => bundledTheme.GetTheme()).ThrowsExactly<InvalidOperationException>();
    }

    [Test, STAThreadExecutor]
    public async Task WhenAllValuesAreSetThemeIsSet()
    {
        //Arrange
        var bundledTheme = new BundledTheme();

        //Act
        bundledTheme.BaseTheme = BaseTheme.Light;
        bundledTheme.PrimaryColor = PrimaryColor.Purple;
        bundledTheme.SecondaryColor = SecondaryColor.Lime;

        //Assert
        Theme theme = bundledTheme.GetTheme();
        await Assert.That(theme.PrimaryMid.Color).IsEqualTo(SwatchHelper.Lookup[(MaterialDesignColor)PrimaryColor.Purple]);
        await Assert.That(theme.SecondaryMid.Color).IsEqualTo(SwatchHelper.Lookup[(MaterialDesignColor)SecondaryColor.Lime]);

        var lightTheme = new Theme();
        lightTheme.SetLightTheme();
        await Assert.That(theme.Foreground).IsEqualTo(lightTheme.Foreground).And.IsNotEqualTo(default);
    }
}
