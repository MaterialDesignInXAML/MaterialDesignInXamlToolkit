using System.Windows.Media;

namespace MaterialDesignThemes.UITests.WPF.Severities;

public class SeverityBrushProviderTests : TestBase
{
    [Test]
    [DoesNotRequireInteraction]
    [Arguments(Severity.Info, "MaterialDesign.Brush.Info")]
    [Arguments(Severity.Success, "MaterialDesign.Brush.Success")]
    [Arguments(Severity.Warning, "MaterialDesign.Brush.Warning")]
    [Arguments(Severity.Error, "MaterialDesign.Brush.Error")]
    public async Task GetBrush_ReturnsCurrentThemeBrush(Severity severity, string brushName)
    {
        IVisualElement<TextBlock> textBlock = await LoadXaml<TextBlock>("""<TextBlock Text="Hello" />""");
        Color expected = await GetThemeColor(brushName);

        Color? actual = await textBlock.RemoteExecute(GetBrushColor, severity);

        await Assert.That(actual).IsEqualTo(expected);
    }

    [Test]
    [DoesNotRequireInteraction]
    public async Task GetBrush_None_ReturnsNull()
    {
        IVisualElement<TextBlock> textBlock = await LoadXaml<TextBlock>("""<TextBlock Text="Hello" />""");

        await Assert.That(await textBlock.RemoteExecute(GetBrushColor, Severity.None)).IsNull();
    }

    private static Color? GetBrushColor(TextBlock _, Severity severity) => SeverityBrushProvider.GetBrush(severity)?.Color;
}
