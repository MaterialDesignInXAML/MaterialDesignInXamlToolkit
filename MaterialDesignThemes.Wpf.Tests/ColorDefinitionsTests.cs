using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class ColorDefinitionsTests
    {
        [Fact]
        public void VerifyLightXamlColorsInTheme()
        {
            AssertXamlColorsInTheme("MaterialDesignTheme.Light.xaml", new MaterialDesignLightTheme());
        }

        [Fact]
        public void VerifyDarkXamlColorsInTheme()
        {
            AssertXamlColorsInTheme("MaterialDesignTheme.Dark.xaml", new MaterialDesignDarkTheme());
        }

        private static void AssertXamlColorsInTheme(string xaml, IBaseTheme baseTheme)
        {
            var theme = new Theme();
            theme.SetBaseTheme(baseTheme);

            var resourceDictionary = MdixHelper.GetResourceDictionary(xaml);

            foreach (var (key, solidColorBrush) in resourceDictionary
                .Cast<DictionaryEntry>()
                .Where(e => e.Value is SolidColorBrush)
                .Select(e => ((string) e.Key, (SolidColorBrush) e.Value))
                .OrderBy(e => e.Item1))
            {
                var baseThemePropertyName = key == "ValidationErrorBrush"
                    ? "ValidationErrorColor"
                    : key;
                var baseThemeProperty = baseTheme.GetType().GetProperty(baseThemePropertyName);
                Assert.False(baseThemeProperty == null, $"{baseThemePropertyName} from {xaml} not found in {baseTheme.GetType()}");

                var themePropertyName = key == "ValidationErrorBrush"
                    ? "ValidationError"
                    : key.Replace("MaterialDesign", "");
                var themeProperty = theme.GetType().GetProperty(themePropertyName);
                Assert.False(themeProperty == null, $"{themePropertyName} from {xaml} not found in {theme.GetType()}");
                Assert.NotNull(themeProperty);

                Assert.Equal(solidColorBrush.Color, themeProperty.GetValue(theme));
            }
        }

        [Fact]
        public void VerifyLightThemeColorsInXaml()
        {
            AssertThemeColorsInXaml(new MaterialDesignLightTheme(), "MaterialDesignTheme.Light.xaml");
        }

        [Fact]
        public void VerifyDarkThemeColorsInXaml()
        {
            AssertThemeColorsInXaml(new MaterialDesignDarkTheme(), "MaterialDesignTheme.Dark.xaml");
        }

        private static void AssertThemeColorsInXaml(IBaseTheme baseTheme, string xaml)
        {
            var theme = new Theme();
            theme.SetBaseTheme(baseTheme);

            var resourceDictionary = MdixHelper.GetResourceDictionary(xaml);

            var updatedDictionary = new ResourceDictionary();
            updatedDictionary.SetTheme(theme);

            foreach (var property in theme.GetType().GetProperties().Where(p => p.PropertyType == typeof(Color)))
            {
                var propertyColor = (Color) property.GetValue(theme)!;
                var (nameBrush, nameColor) = property.Name == "ValidationError"
                    ? ("ValidationErrorBrush", "ValidationErrorColor")
                    : ("MaterialDesign" + property.Name, "MaterialDesign" + property.Name + "Color");

                Assert.True(resourceDictionary.Contains(nameBrush), $"{nameBrush} from {theme.GetType()} not found in {xaml}");

                var xamlValue = resourceDictionary[nameBrush];
                Assert.True(xamlValue is SolidColorBrush, $"{nameBrush} in {xaml} is no SolidColorBrush");
                Assert.Equal(propertyColor, ((SolidColorBrush) xamlValue).Color);

                Assert.True(updatedDictionary.Contains(nameColor), $"{nameColor} from {theme.GetType()} not set via ResourceDictionaryExtensions.SetTheme");
                Assert.Equal(propertyColor, updatedDictionary[nameColor]);
            }
        }
    }
}