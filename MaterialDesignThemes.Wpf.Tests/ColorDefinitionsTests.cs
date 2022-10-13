using System.Collections;
using System.Windows.Media;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class ColorDefinitionsTests
    {
        [Fact]
        public void LightAndDarkThemeBothContainSameResources()
        {
            var light = MdixHelper.GetResourceDictionary("MaterialDesignTheme.Light.xaml");
            var dark = MdixHelper.GetResourceDictionary("MaterialDesignTheme.Dark.xaml");

            var lightKeys = light.Keys.OfType<string>().OrderBy(x => x).ToList();
            var darkKeys = dark.Keys.OfType<string>().OrderBy(x => x).ToList();

            foreach (var (key, solidColorBrush) in resourceDictionary
                .Cast<DictionaryEntry>()
                .Where(e => e.Value is SolidColorBrush)
                .Select(e => ((string)e.Key, e.Value as SolidColorBrush))
                .OrderBy(e => e.Item1))
            {
                var baseThemePropertyName = key == "MaterialDesignValidationErrorBrush"
                    ? "MaterialDesignValidationErrorColor"
                    : key;
                var baseThemeProperty = baseTheme.GetType().GetProperty(baseThemePropertyName);
                Assert.False(baseThemeProperty == null, $"{baseThemePropertyName} from {xaml} not found in {baseTheme.GetType()}");

                var themePropertyName = key == "MaterialDesignValidationErrorBrush"
                    ? "ValidationError"
                    : key.Replace("MaterialDesign", "");
                var themeProperty = theme.GetType().GetProperty(themePropertyName);
                Assert.False(themeProperty is null, $"{themePropertyName} from {xaml} not found in {theme.GetType()}");
                Assert.NotNull(themeProperty);

                Assert.Equal(solidColorBrush!.Color, themeProperty!.GetValue(theme));
            }
        }


        [Theory]
        [InlineData("MaterialDesignTheme.Light.xaml")]
        [InlineData("MaterialDesignTheme.Dark.xaml")]
        public void BrushesUseSameValuesAsColors(string resourceDictionary)
        {
            var dictionary = MdixHelper.GetResourceDictionary(resourceDictionary);

            foreach (var key in dictionary.Keys.OfType<string>().Where(x => x.StartsWith("MaterialDesign.") && !x.EndsWith(".Color")))
            {
                if (dictionary[key] is SolidColorBrush brush)
                {
                    Color color = (Color)dictionary[key + ".Color"];
                    Assert.True(brush.Color == color, $"Brush '{key}' has color {brush.Color} but expected {color}, in {resourceDictionary}");
                }
            }
        }

        [Obsolete]
        private static void AssertThemeColorsReadFromXaml(IBaseTheme baseTheme, string xaml)
        {
            var resourceDictionary = new ResourceDictionary();
            resourceDictionary.MergedDictionaries.Add(MdixHelper.GetResourceDictionary(xaml));
            resourceDictionary.MergedDictionaries.Add(MdixHelper.GetPrimaryColorResourceDictionary("DeepPurple"));
            resourceDictionary.MergedDictionaries.Add(MdixHelper.GetSecondaryColorResourceDictionary("Lime"));

            ITheme theme = resourceDictionary.GetTheme();

            foreach (var property in theme.GetType().GetProperties().Where(p => p.PropertyType == typeof(Color)))
            {
                var propertyColor = (Color)property.GetValue(theme)!;
                var (nameBrush, nameColor) = property.Name == "ValidationError"
                    ? ("MaterialDesignValidationErrorBrush", "MaterialDesignValidationErrorColor")
                    : ("MaterialDesign" + property.Name, "MaterialDesign" + property.Name + "Color");

                var xamlColor = ((SolidColorBrush)resourceDictionary[nameBrush]).Color;

                Assert.Equal(xamlColor, propertyColor);
            }
        }

    }
}
