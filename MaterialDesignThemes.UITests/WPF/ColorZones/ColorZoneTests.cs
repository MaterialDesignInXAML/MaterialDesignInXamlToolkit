using System.Windows.Media;

namespace MaterialDesignThemes.UITests.WPF.ColorZones;

public class ColorZoneTests : TestBase
{
    public ColorZoneTests(ITestOutputHelper output)
        : base(output)
    {

    }

    [Theory]
    [InlineData(ColorZoneMode.Standard, "MaterialDesign.Brush.Background", "MaterialDesign.Brush.Foreground")]
    [InlineData(ColorZoneMode.Inverted, "MaterialDesign.Brush.Foreground", "MaterialDesign.Brush.Background")]
    [InlineData(ColorZoneMode.PrimaryLight, "MaterialDesign.Brush.Primary.Light", "MaterialDesign.Brush.Primary.Light.Foreground")]
    [InlineData(ColorZoneMode.PrimaryMid, "MaterialDesign.Brush.Primary", "MaterialDesign.Brush.Primary.Foreground")]
    [InlineData(ColorZoneMode.PrimaryDark, "MaterialDesign.Brush.Primary.Dark", "MaterialDesign.Brush.Primary.Dark.Foreground")]
    [InlineData(ColorZoneMode.SecondaryLight, "MaterialDesign.Brush.Secondary.Light", "MaterialDesign.Brush.Secondary.Light.Foreground")]
    [InlineData(ColorZoneMode.SecondaryMid, "MaterialDesign.Brush.Secondary", "MaterialDesign.Brush.Secondary.Foreground")]
    [InlineData(ColorZoneMode.SecondaryDark, "MaterialDesign.Brush.Secondary.Dark", "MaterialDesign.Brush.Secondary.Dark.Foreground")]
    [InlineData(ColorZoneMode.Light, "MaterialDesign.Brush.ColorZone.LightBackground", "MaterialDesign.Brush.ColorZone.LightForeground")]
    [InlineData(ColorZoneMode.Dark, "MaterialDesign.Brush.ColorZone.DarkBackground", "MaterialDesign.Brush.ColorZone.DarkForeground")]
    public async Task Mode_SetsThemeColors(ColorZoneMode mode, string backgroundBrush, string foregroundBrush)
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<ColorZone> colorZone = await LoadXaml<ColorZone>(@$"
<materialDesign:ColorZone Mode=""{mode}""/>
");
        Color background = await GetThemeColor(backgroundBrush);
        Color foreground = await GetThemeColor(foregroundBrush);

        Assert.Equal(background, await colorZone.GetBackgroundColor());
        Assert.Equal(foreground, await colorZone.GetForegroundColor());

        recorder.Success();
    }
}
