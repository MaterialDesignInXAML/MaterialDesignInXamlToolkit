using System.Windows.Media;
using MaterialDesignColors;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests;

public class BundledThemeTests
{
    [StaFact]
    public void WhenAllValuesAreSetThemeIsSet()
    {
        //Arrange
        BundledTheme bundledTheme = new();
        var themeManager = Theming.ThemeManager.GetThemeManager(bundledTheme);

        //Act
        bundledTheme.BaseTheme = BaseTheme.Light;
        bundledTheme.PrimaryColor = PrimaryColor.Purple;
        bundledTheme.SecondaryColor = SecondaryColor.Lime;

        //Assert
        Theming.Theme theme = themeManager.GetTheme();
        Assert.Equal(SwatchHelper.Lookup[(MaterialDesignColor)PrimaryColor.Purple], theme.PrimaryMid.Color);
        Assert.Equal(SwatchHelper.Lookup[(MaterialDesignColor)SecondaryColor.Lime], theme.SecondaryMid.Color);
        Assert.Equal(Theming.Theme.Light.Foreground, theme.Foreground);
    }

    [StaFact]
    public void BundledThemeContainsAllOfTheExpectedResources()
    {
        //Arrange
        BundledTheme bundledTheme = new();

        var startingResources = bundledTheme.Keys
            .Cast<string>()
            .OrderBy(x => x)
            .ToList();

        //Act
        bundledTheme.BaseTheme = BaseTheme.Light;
        bundledTheme.PrimaryColor = PrimaryColor.Purple;
        bundledTheme.SecondaryColor = SecondaryColor.Lime;

        //Assert
        var endingResources = bundledTheme.Keys
            .Cast<string>()
            .OrderBy(x => x)
            .ToList();

        //Remove meta level resources
        const string currentThemeKey = "MaterialDesignThemes.CurrentThemeKey";
        const string themeManagerKey = "MaterialDesignThemes.ThemeManagerKey";

        startingResources.Remove(currentThemeKey);
        startingResources.Remove(themeManagerKey);
        endingResources.Remove(currentThemeKey);
        endingResources.Remove(themeManagerKey);

        Assert.Equal(startingResources, endingResources);
    }

    [StaFact(Skip = "Only Run Manually")]
    public void GenerateDefaultValueResources()
    {
        BundledTheme bundledTheme = new();

        string xaml = string.Join(Environment.NewLine, bundledTheme.Keys
            .Cast<string>()
            .OrderBy(x => x)
            .Select(x =>
            {
                return bundledTheme[x] switch
                {
                    SolidColorBrush _ => $"<SolidColorBrush x:Key=\"{x}\" />",
                    Color _ => $"<Color x:Key=\"{x}\" />",
                    Theming.ThemeManager _ => "",
                    Theming.Theme _ => "",
#pragma warning disable CS0618 // Type or member is obsolete
                    IThemeManager _ => "",
                    Theme _ => "",
#pragma warning restore CS0618 // Type or member is obsolete
                    _ => throw new Exception("Unknown type " + bundledTheme[x].GetType().FullName)
                };
            })
            .Where(x => !string.IsNullOrEmpty(x)));
        System.Diagnostics.Debugger.Break();
    }
}
