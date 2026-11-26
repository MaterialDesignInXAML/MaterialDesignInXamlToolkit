namespace MaterialDesignThemes.Wpf.Tests;

public class SeverityBrushProviderTests
{
    [Test]
    [Arguments(Severity.None, null)]
    [Arguments(Severity.Info, "MaterialDesign.Brush.Info")]
    [Arguments(Severity.Success, "MaterialDesign.Brush.Success")]
    [Arguments(Severity.Warning, "MaterialDesign.Brush.Warning")]
    [Arguments(Severity.Error, "MaterialDesign.Brush.Error")]
    public async Task GetResourceKey_ReturnsThemeBrushKey(Severity severity, string? expectedKey)
    {
        await Assert.That(SeverityBrushProvider.GetResourceKey(severity)).IsEqualTo(expectedKey);
    }
}
