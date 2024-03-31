using System.Reflection;
using System.Windows.Media;
using MaterialDesignColors;

namespace MaterialDesignThemes.UITests.WPF.Theme;

public partial class ThemeTests : TestBase
{
    public ThemeTests(ITestOutputHelper output)
        : base(output)
    { }

    [Fact]
    public async Task WhenUsingBuiltInLightXamlThemeDictionary_AllBrushesApplied()
    {
        IVisualElement<WrapPanel> panel = await Initialize("""
            <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml" />
            <ResourceDictionary Source="pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml" />
            <ResourceDictionary Source="pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Secondary/MaterialDesignColor.Lime.xaml" />
            """);

        await AssertAllThemeBrushesSet(panel);
    }

    [Fact]
    public async Task WhenUsingBuiltInDarkXamlThemeDictionary_AllBrushesApplied()
    {
        IVisualElement<WrapPanel> panel = await Initialize("""
            <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml" />
            <ResourceDictionary Source="pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml" />
            <ResourceDictionary Source="pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Secondary/MaterialDesignColor.Lime.xaml" />
            """);

        await AssertAllThemeBrushesSet(panel);
    }

    [Fact]
    public async Task WhenUsingBuiltInThemeDictionary_AllBrushesApplied()
    {
        IVisualElement<WrapPanel> panel = await Initialize("""
            <materialDesign:BundledTheme BaseTheme="Inherit"
                ColorAdjustment="{materialDesign:ColorAdjustment}"
                PrimaryColor="DeepPurple"
                SecondaryColor="Lime" />
            """);

        await AssertAllThemeBrushesSet(panel);
    }

    [Fact]
    public async Task WhenUsingCustomColorThemeDictionary_AllBrushesApplied()
    {
        IVisualElement<WrapPanel> panel = await Initialize("""
            <materialDesign:CustomColorTheme BaseTheme="Inherit"
                PrimaryColor="Aqua"
                SecondaryColor="DarkGreen" />
            """);

        await AssertAllThemeBrushesSet(panel);
    }

    public static IEnumerable<object[]> ThemeCombinations()
    {
        var baseThemes = new[] { "Light", "Dark" };
        var primaryColors = Enum.GetNames<PrimaryColor>();
        var secondaryColors = Enum.GetNames<SecondaryColor>();

        return (from baseTheme in baseThemes
               from primaryColor in primaryColors
               from secondaryColor in secondaryColors
               select new object[] { baseTheme, primaryColor, secondaryColor });

    }

    [Theory(Skip = "Manual run when theme values change")]
    [MemberData(nameof(ThemeCombinations))]
    public async Task BundledTheme_UsesSameColorsAsXamlResources(string baseTheme, string primaryColor, string secondaryColor)
    {
        IVisualElement<WrapPanel> bundledPanel = await Initialize($"""
            <materialDesign:BundledTheme BaseTheme="{baseTheme}"
                PrimaryColor="{primaryColor}"
                SecondaryColor="{secondaryColor}" />
            """);
        Dictionary<string, Color> bundledColorsByNames = await GetColors();

        //Re-setup the App
        await DisposeAsync();
        await InitializeAsync();

        IVisualElement<WrapPanel> xamlPanel = await Initialize($"""
            <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{baseTheme}.xaml" />
            <ResourceDictionary Source="pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.{primaryColor}.xaml" />
            <ResourceDictionary Source="pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Secondary/MaterialDesignColor.{secondaryColor}.xaml" />
            """);
        Dictionary<string, Color> xamlColorsByNames = await GetColors();

        Assert.Equal(bundledColorsByNames.Count, xamlColorsByNames.Count);

        foreach (string brushName in GetBrushResourceNames())
        {
            bool areEqual = bundledColorsByNames[brushName] == xamlColorsByNames[brushName];
            Assert.True(areEqual, $"Brush {brushName}, Bundled color {bundledColorsByNames[brushName]} does not match XAML color {xamlColorsByNames[brushName]}");
        }

        async Task<Dictionary<string, Color>> GetColors()
        {
            Dictionary<string, Color> rv = new();

            await Task.WhenAll(GetBrushResourceNames().Select(async x =>
            {
                Color color = await GetResourceColor(x);
                lock (rv)
                {
                    rv[x] = color;
                }
            }));

            return rv;
        }
    }


    private partial string GetXamlWrapPanel();
    private partial Task AssertAllThemeBrushesSet(IVisualElement<WrapPanel> panel);

    private async Task<Color> GetResourceColor(string name)
    {
        IResource resource = await App.GetResource(name);
        SolidColorBrush? brush = resource.GetAs<SolidColorBrush>();
        Assert.NotNull(brush);
        return brush.Color;
    }

    protected async Task<IVisualElement<WrapPanel>> Initialize(string themeDictionary)
    {
        string applicationResourceXaml = $"""
            <ResourceDictionary 
                xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                xmlns:materialDesign="http://materialdesigninxaml.net/winfx/xaml/themes">
                <ResourceDictionary.MergedDictionaries>
                    {themeDictionary}

                    <!-- Obsolete brushes are also tested -->
                    <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.ObsoleteBrushes.xaml" />

                    <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesign2.Defaults.xaml" />
                </ResourceDictionary.MergedDictionaries>
            </ResourceDictionary>
            """;

        await App.Initialize(applicationResourceXaml,
            Path.GetFullPath("MaterialDesignColors.dll"),
            Path.GetFullPath("MaterialDesignThemes.Wpf.dll"),
            Assembly.GetExecutingAssembly().Location);
        return await App.CreateWindowWith<WrapPanel>(GetXamlWrapPanel());
    }
}
