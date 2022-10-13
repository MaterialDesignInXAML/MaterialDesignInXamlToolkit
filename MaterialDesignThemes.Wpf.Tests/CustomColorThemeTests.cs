using System.Windows.Media;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class CustomColorThemeTests
    {
        [StaFact]
        public void WhenAllValuesAreSetThemeIsSet()
        {
            //Arrange
            CustomColorTheme customTheme = new();
            var themeManager = Theming.ThemeManager.GetThemeManager(customTheme);

            //Act
            customTheme.BaseTheme = BaseTheme.Light;
            customTheme.PrimaryColor = Colors.Fuchsia;
            customTheme.SecondaryColor = Colors.Lime;

            //Assert
            Theming.Theme theme = themeManager.GetTheme();
            Assert.Equal(Colors.Fuchsia, theme.PrimaryMid.Color);
            Assert.Equal(Colors.Lime, theme.SecondaryMid.Color);
            Assert.Equal(Theming.Theme.Light.Foreground, theme.Foreground);
        }

        [StaFact]
        public void CustomColorThemeContainsAllOfTheExpectedResources()
        {
            //Arrange
            CustomColorTheme customTheme = new();

            var startingResources = customTheme.Keys
                .Cast<string>()
                .OrderBy(x => x)
                .ToList();

            //Act
            customTheme.BaseTheme = BaseTheme.Light;
            customTheme.PrimaryColor = Colors.Fuchsia;
            customTheme.SecondaryColor = Colors.Lime;

            //Assert
            var endingResources = customTheme.Keys
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
    }
}
