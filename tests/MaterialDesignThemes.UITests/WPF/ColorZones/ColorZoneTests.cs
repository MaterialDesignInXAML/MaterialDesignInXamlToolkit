using System.Windows.Media;


namespace MaterialDesignThemes.UITests.WPF.ColorZones;

public class ColorZoneTests : TestBase
{
    [Test]
    [Arguments(ColorZoneMode.Standard, "MaterialDesign.Brush.Background", "MaterialDesign.Brush.Foreground")]
    [Arguments(ColorZoneMode.Inverted, "MaterialDesign.Brush.Foreground", "MaterialDesign.Brush.Background")]
    [Arguments(ColorZoneMode.PrimaryLight, "MaterialDesign.Brush.Primary.Light", "MaterialDesign.Brush.Primary.Light.Foreground")]
    [Arguments(ColorZoneMode.PrimaryMid, "MaterialDesign.Brush.Primary", "MaterialDesign.Brush.Primary.Foreground")]
    [Arguments(ColorZoneMode.PrimaryDark, "MaterialDesign.Brush.Primary.Dark", "MaterialDesign.Brush.Primary.Dark.Foreground")]
    [Arguments(ColorZoneMode.SecondaryLight, "MaterialDesign.Brush.Secondary.Light", "MaterialDesign.Brush.Secondary.Light.Foreground")]
    [Arguments(ColorZoneMode.SecondaryMid, "MaterialDesign.Brush.Secondary", "MaterialDesign.Brush.Secondary.Foreground")]
    [Arguments(ColorZoneMode.SecondaryDark, "MaterialDesign.Brush.Secondary.Dark", "MaterialDesign.Brush.Secondary.Dark.Foreground")]
    [Arguments(ColorZoneMode.Light, "MaterialDesign.Brush.ColorZone.LightBackground", "MaterialDesign.Brush.ColorZone.LightForeground")]
    [Arguments(ColorZoneMode.Dark, "MaterialDesign.Brush.ColorZone.DarkBackground", "MaterialDesign.Brush.ColorZone.DarkForeground")]
    public async Task Mode_SetsThemeColors(ColorZoneMode mode, string backgroundBrush, string foregroundBrush)
    {
        IVisualElement<ColorZone> colorZone = await LoadXaml<ColorZone>(@$"
<materialDesign:ColorZone Mode=""{mode}""/>
");
        Color background = await GetThemeColor(backgroundBrush);
        Color foreground = await GetThemeColor(foregroundBrush);

        await Assert.That(await colorZone.GetBackgroundColor()).IsEqualTo(background);
        await Assert.That(await colorZone.GetForegroundColor()).IsEqualTo(foreground);
    }
}
