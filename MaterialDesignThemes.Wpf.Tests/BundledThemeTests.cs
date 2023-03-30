using MaterialDesignColors;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests;

public class BundledThemeTests
{
    [StaTheory]
    [InlineData(null, null, null)]
    [InlineData(BaseTheme.Light, null, null)]
    [InlineData(BaseTheme.Inherit, null, null)]
    [InlineData(null, PrimaryColor.Blue, null)]
    [InlineData(BaseTheme.Light, PrimaryColor.Blue, null)]
    [InlineData(BaseTheme.Inherit, PrimaryColor.Blue, null)]
    [InlineData(null, null, SecondaryColor.Blue)]
    [InlineData(BaseTheme.Light, null, SecondaryColor.Blue)]
    [InlineData(BaseTheme.Inherit, null, SecondaryColor.Blue)]
    public void WhenValueIsMissingThemeIsNotSet(BaseTheme? baseTheme, PrimaryColor? primaryColor, SecondaryColor? secondaryColor)
    {
        //Arrange
        var bundledTheme = new BundledTheme();

        //Act
        bundledTheme.BaseTheme = baseTheme;
        bundledTheme.PrimaryColor = primaryColor;
        bundledTheme.SecondaryColor = secondaryColor;

        //Assert
        Assert.Throws<InvalidOperationException>(() => bundledTheme.GetTheme());
    }

    [StaFact]
    public void WhenAllValuesAreSetThemeIsSet()
    {
        //Arrange
        var bundledTheme = new BundledTheme();

        //Act
        bundledTheme.BaseTheme = BaseTheme.Light;
        bundledTheme.PrimaryColor = PrimaryColor.Purple;
        bundledTheme.SecondaryColor = SecondaryColor.Lime;

        //Assert
        Theme theme = bundledTheme.GetTheme();
        Assert.Equal(SwatchHelper.Lookup[(MaterialDesignColor)PrimaryColor.Purple], theme.PrimaryMid.Color);
        Assert.Equal(SwatchHelper.Lookup[(MaterialDesignColor)SecondaryColor.Lime], theme.SecondaryMid.Color);

        var lightTheme = new Theme();
        lightTheme.SetLightTheme();
        Assert.Equal(lightTheme.Foreground, theme.Foreground);
        Assert.NotEqual(default, theme.Foreground);
    }
}
