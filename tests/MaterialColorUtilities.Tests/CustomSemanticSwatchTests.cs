using System.Windows.Media;

namespace MaterialColorUtilities.Tests;

public sealed class CustomSemanticSwatchTests
{
    [Test]
    [Arguments(true)]
    [Arguments(false)]
    public async Task CustomSemanticSwatches_ExtractExpectedThemeColors(bool isDark)
    {
        var materialDynamicColors = new MaterialDynamicColors();

        foreach (var semanticColor in GetSemanticColorExpectations())
        {
            foreach (var isDark in new[] { false, true })
            {
                var expected = isDark ? semanticColor.Dark : semanticColor.Light;
                var scheme = CreateSchemeWithCustomErrorSwatch(semanticColor.Swatch, isDark);

                await Assert.That(materialDynamicColors.ErrorPaletteKeyColor.GetColor(scheme))
                    .IsEqualTo(expected.PaletteKey)
                    .Because($"{semanticColor.Name} palette key did not match for {(isDark ? "dark" : "light")} mode.");

                await Assert.That(materialDynamicColors.Error.GetColor(scheme))
                    .IsEqualTo(expected.Role)
                    .Because($"{semanticColor.Name} role color did not match for {(isDark ? "dark" : "light")} mode.");

                await Assert.That(materialDynamicColors.OnError.GetColor(scheme))
                    .IsEqualTo(expected.OnRole)
                    .Because($"{semanticColor.Name} on-role color did not match for {(isDark ? "dark" : "light")} mode.");

                await Assert.That(materialDynamicColors.ErrorContainer.GetColor(scheme))
                    .IsEqualTo(expected.Container)
                    .Because($"{semanticColor.Name} container color did not match for {(isDark ? "dark" : "light")} mode.");

                await Assert.That(materialDynamicColors.OnErrorContainer.GetColor(scheme))
                    .IsEqualTo(expected.OnContainer)
                    .Because($"{semanticColor.Name} on-container color did not match for {(isDark ? "dark" : "light")} mode.");
            }
        }
    }

    private static DynamicScheme CreateSchemeWithCustomErrorSwatch(Color semanticSwatch, bool isDark)
    {
        var sourceColor = Color.FromArgb(255, 106, 156, 89);
        var baseScheme = DynamicSchemeFactory.Create(
            sourceColor,
            Variant.TonalSpot,
            isDark,
            0.5,
            Platform.Phone,
            SpecVersion.Spec2025,
            primary: null,
            secondary: null,
            tertiary: null,
            neutral: null,
            neutralVariant: null,
            error: null);

        return new DynamicScheme(
            baseScheme.SourceColorHct,
            baseScheme.Variant,
            baseScheme.IsDark,
            baseScheme.ContrastLevel,
            baseScheme.PlatformType,
            baseScheme.SpecVersion,
            baseScheme.PrimaryPalette,
            baseScheme.SecondaryPalette,
            baseScheme.TertiaryPalette,
            baseScheme.NeutralPalette,
            baseScheme.NeutralVariantPalette,
            TonalPalette.FromInt(ColorUtils.ArgbFromColor(semanticSwatch)));
    }

    private static SemanticColorExpectation[] GetSemanticColorExpectations()
    {
        return
        [
            new SemanticColorExpectation(
                "WarningHigh",
                Color.FromArgb(255, 212, 69, 41),
                new ExtractedSemanticColors(
                    PaletteKey: Color.FromArgb(255, 212, 69, 41),
                    Role: Color.FromArgb(255, 111, 15, 0),
                    OnRole: Colors.White,
                    Container: Color.FromArgb(255, 198, 59, 32),
                    OnContainer: Colors.White),
                new ExtractedSemanticColors(
                    PaletteKey: Color.FromArgb(255, 212, 69, 41),
                    Role: Color.FromArgb(255, 255, 210, 201),
                    OnRole: Color.FromArgb(255, 80, 8, 0),
                    Container: Color.FromArgb(255, 247, 94, 63),
                    OnContainer: Colors.Black)),
            new SemanticColorExpectation(
                "WarningLow",
                Color.FromArgb(255, 252, 163, 41),
                new ExtractedSemanticColors(
                    PaletteKey: Color.FromArgb(255, 252, 163, 41),
                    Role: Color.FromArgb(255, 80, 47, 0),
                    OnRole: Colors.White,
                    Container: Color.FromArgb(255, 156, 95, 0),
                    OnContainer: Colors.White),
                new ExtractedSemanticColors(
                    PaletteKey: Color.FromArgb(255, 252, 163, 41),
                    Role: Color.FromArgb(255, 255, 213, 169),
                    OnRole: Color.FromArgb(255, 57, 32, 0),
                    Container: Color.FromArgb(255, 205, 127, 0),
                    OnContainer: Colors.Black)),
            new SemanticColorExpectation(
                "Information",
                Color.FromArgb(255, 0, 46, 122),
                new ExtractedSemanticColors(
                    PaletteKey: Color.FromArgb(255, 0, 46, 122),
                    Role: Color.FromArgb(255, 5, 49, 125),
                    OnRole: Colors.White,
                    Container: Color.FromArgb(255, 76, 106, 184),
                    OnContainer: Colors.White),
                new ExtractedSemanticColors(
                    PaletteKey: Color.FromArgb(255, 0, 46, 122),
                    Role: Color.FromArgb(255, 210, 219, 255),
                    OnRole: Color.FromArgb(255, 0, 33, 93),
                    Container: Color.FromArgb(255, 112, 142, 222),
                    OnContainer: Colors.Black)),
            new SemanticColorExpectation(
                "Safe",
                Color.FromArgb(255, 41, 138, 64),
                new ExtractedSemanticColors(
                    PaletteKey: Color.FromArgb(255, 41, 138, 64),
                    Role: Color.FromArgb(255, 0, 64, 21),
                    OnRole: Colors.White,
                    Container: Color.FromArgb(255, 24, 126, 53),
                    OnContainer: Colors.White),
                new ExtractedSemanticColors(
                    PaletteKey: Color.FromArgb(255, 41, 138, 64),
                    Role: Color.FromArgb(255, 145, 242, 155),
                    OnRole: Color.FromArgb(255, 0, 45, 12),
                    Container: Color.FromArgb(255, 69, 163, 86),
                    OnContainer: Colors.Black)),
            new SemanticColorExpectation(
                "Error",
                Color.FromArgb(255, 163, 23, 26),
                new ExtractedSemanticColors(
                    PaletteKey: Color.FromArgb(255, 163, 23, 26),
                    Role: Color.FromArgb(255, 115, 0, 9),
                    OnRole: Colors.White,
                    Container: Color.FromArgb(255, 201, 53, 49),
                    OnContainer: Colors.White),
                new ExtractedSemanticColors(
                    PaletteKey: Color.FromArgb(255, 163, 23, 26),
                    Role: Color.FromArgb(255, 255, 210, 205),
                    OnRole: Color.FromArgb(255, 84, 0, 4),
                    Container: Color.FromArgb(255, 251, 89, 80),
                    OnContainer: Colors.Black))
        ];
    }

    private sealed record SemanticColorExpectation(
        string Name,
        Color Swatch,
        ExtractedSemanticColors Light,
        ExtractedSemanticColors Dark);

    private sealed record ExtractedSemanticColors(
        Color PaletteKey,
        Color Role,
        Color OnRole,
        Color Container,
        Color OnContainer);
}
