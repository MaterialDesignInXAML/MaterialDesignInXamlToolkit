namespace MaterialColorUtilities.Tests;

public sealed class MaterialDynamicColorsTests
{
    private static readonly Hct[] SeedColors =
    [
        Hct.FromInt(unchecked((int)0xFFFF0000)),
        Hct.FromInt(unchecked((int)0xFFFFFF00)),
        Hct.FromInt(unchecked((int)0xFF00FF00)),
        Hct.FromInt(unchecked((int)0xFF0000FF))
    ];

    private static readonly double[] ContrastLevels = [-1.0, -0.5, 0.0, 0.5, 1.0];

    private sealed record Pair(string ForegroundName, string BackgroundName);

    [Test]
    [DisplayName("MaterialDynamicColors: Values are correct (spot checks)")]
    public async Task Values_Are_Correct_SpotChecks()
    {
        var mdc = new MaterialDynamicColors();

        var onPrimaryContainerArgb = mdc.OnPrimaryContainer.GetArgb(
            new SchemeFidelity(
                sourceColorHct: Hct.FromInt(unchecked((int)0xFFFF0000)),
                isDark: false,
                contrastLevel: 0.5));
        await Assert.That(onPrimaryContainerArgb).IsEqualTo(unchecked((int)0xFFFFFFFF));

        var onSecondaryContainerArgb = mdc.OnSecondaryContainer.GetArgb(
            new SchemeContent(
                sourceColorHct: Hct.FromInt(unchecked((int)0xFF0000FF)),
                isDark: false,
                contrastLevel: 0.5));
        await Assert.That(onSecondaryContainerArgb).IsEqualTo(unchecked((int)0xFFFFFFFF));

        var onTertiaryContainerArgb = mdc.OnTertiaryContainer.GetArgb(
            new SchemeContent(
                sourceColorHct: Hct.FromInt(unchecked((int)0xFFFFFF00)),
                isDark: true,
                contrastLevel: -0.5));
        await Assert.That(onTertiaryContainerArgb).IsEqualTo(unchecked((int)0xFF959B1A));

        var inverseSurfaceArgb = mdc.InverseSurface.GetArgb(
            new SchemeContent(
                sourceColorHct: Hct.FromInt(unchecked((int)0xFF0000FF)),
                isDark: false,
                contrastLevel: 0.0));
        await Assert.That(inverseSurfaceArgb).IsEqualTo(unchecked((int)0xFF2F2F3B));

        var inversePrimaryArgb = mdc.InversePrimary.GetArgb(
            new SchemeContent(
                sourceColorHct: Hct.FromInt(unchecked((int)0xFFFF0000)),
                isDark: false,
                contrastLevel: -0.5));
        await Assert.That(inversePrimaryArgb).IsEqualTo(unchecked((int)0xFFFF422F));

        var outlineVariantArgb = mdc.OutlineVariant.GetArgb(
            new SchemeContent(
                sourceColorHct: Hct.FromInt(unchecked((int)0xFFFFFF00)),
                isDark: true,
                contrastLevel: 0.0));
        await Assert.That(outlineVariantArgb).IsEqualTo(unchecked((int)0xFF484831));
    }

    [Test]
    [DisplayName("Dynamic schemes respect contrast for text/surface pairs")]
    public async Task Dynamic_Schemes_Respect_Contrast_Text_Surface_Pairs()
    {
        var mdc = new MaterialDynamicColors();

        // Name -> DynamicColor map (matches the Dart dictionary)
        var colors = new Dictionary<string, DynamicColor>(StringComparer.Ordinal)
        {
            ["background"] = mdc.Background,
            ["on_background"] = mdc.OnBackground,
            ["surface"] = mdc.Surface,
            ["surface_dim"] = mdc.SurfaceDim,
            ["surface_bright"] = mdc.SurfaceBright,
            ["surface_container_lowest"] = mdc.SurfaceContainerLowest,
            ["surface_container_low"] = mdc.SurfaceContainerLow,
            ["surface_container"] = mdc.SurfaceContainer,
            ["surface_container_high"] = mdc.SurfaceContainerHigh,
            ["surface_container_highest"] = mdc.SurfaceContainerHighest,
            ["on_surface"] = mdc.OnSurface,
            ["surface_variant"] = mdc.SurfaceVariant,
            ["on_surface_variant"] = mdc.OnSurfaceVariant,
            ["inverse_surface"] = mdc.InverseSurface,
            ["inverse_on_surface"] = mdc.InverseOnSurface,
            ["outline"] = mdc.Outline,
            ["outline_variant"] = mdc.OutlineVariant,
            ["shadow"] = mdc.Shadow,
            ["scrim"] = mdc.Scrim,
            ["surface_tint"] = mdc.SurfaceTint,
            ["primary"] = mdc.Primary,
            ["on_primary"] = mdc.OnPrimary,
            ["primary_container"] = mdc.PrimaryContainer,
            ["on_primary_container"] = mdc.OnPrimaryContainer,
            ["inverse_primary"] = mdc.InversePrimary,
            ["secondary"] = mdc.Secondary,
            ["on_secondary"] = mdc.OnSecondary,
            ["secondary_container"] = mdc.SecondaryContainer,
            ["on_secondary_container"] = mdc.OnSecondaryContainer,
            ["tertiary"] = mdc.Tertiary,
            ["on_tertiary"] = mdc.OnTertiary,
            ["tertiary_container"] = mdc.TertiaryContainer,
            ["on_tertiary_container"] = mdc.OnTertiaryContainer,
            ["error"] = mdc.Error,
            ["on_error"] = mdc.OnError,
            ["error_container"] = mdc.ErrorContainer,
            ["on_error_container"] = mdc.OnErrorContainer,
        };

        var textSurfacePairs = new[]
        {
            new Pair("on_primary", "primary"),
            new Pair("on_primary_container", "primary_container"),
            new Pair("on_secondary", "secondary"),
            new Pair("on_secondary_container", "secondary_container"),
            new Pair("on_tertiary", "tertiary"),
            new Pair("on_tertiary_container", "tertiary_container"),
            new Pair("on_error", "error"),
            new Pair("on_error_container", "error_container"),
            new Pair("on_background", "background"),
            new Pair("on_surface_variant", "surface_bright"),
            new Pair("on_surface_variant", "surface_dim"),
            new Pair("inverse_on_surface", "inverse_surface"),
        };

        foreach (var color in SeedColors)
        foreach (var contrastLevel in ContrastLevels)
        foreach (var isDark in new[] { false, true })
        {
            var schemes = new DynamicScheme[]
            {
                new SchemeContent(sourceColorHct: color, isDark: isDark, contrastLevel: contrastLevel),
                new SchemeMonochrome(sourceColorHct: color, isDark: isDark, contrastLevel: contrastLevel),
                new SchemeTonalSpot(sourceColorHct: color, isDark: isDark, contrastLevel: contrastLevel),
                new SchemeFidelity(sourceColorHct: color, isDark: isDark, contrastLevel: contrastLevel),
            };

            foreach (var scheme in schemes)
            {
                foreach (var pair in textSurfacePairs)
                {
                    var fg = colors[pair.ForegroundName];
                    var bg = colors[pair.BackgroundName];

                    var foregroundTone = fg.GetHct(scheme).Tone;
                    var backgroundTone = bg.GetHct(scheme).Tone;
                    var contrast = Contrast.RatioOfTones(foregroundTone, backgroundTone);

                    var minimumRequirement = contrastLevel >= 0.0 ? 4.5 : 3.0;

                    await Assert.That(contrast)
                        .IsGreaterThanOrEqualTo(minimumRequirement)
                        .Because($"Contrast {contrast} is too low between " +
                                 $"foreground ({pair.ForegroundName}; {foregroundTone}) and " +
                                 $"background ({pair.BackgroundName}; {backgroundTone}) " +
                                 $"for scheme={scheme} seed={color} CL={contrastLevel} dark={isDark}");
                }
            }
        }
    }

    [Test]
    [DisplayName("Fixed colors in non-monochrome schemes (TonalSpot, dark)")]
    public async Task Fixed_Colors_NonMonochrome_TonalSpot_Dark()
    {
        var mdc = new MaterialDynamicColors();
        var scheme = new SchemeTonalSpot(
            sourceColorHct: Hct.FromInt(unchecked((int)0xFFFF0000)),
            isDark: true,
            contrastLevel: 0.0);

        await Assert.That(mdc.PrimaryFixed.GetHct(scheme).Tone).IsEqualTo(90.0).Within(1.0);
        await Assert.That(mdc.PrimaryFixedDim.GetHct(scheme).Tone).IsEqualTo(80.0).Within(1.0);
        await Assert.That(mdc.OnPrimaryFixed.GetHct(scheme).Tone).IsEqualTo(10.0).Within(1.0);
        await Assert.That(mdc.OnPrimaryFixedVariant.GetHct(scheme).Tone).IsEqualTo(30.0).Within(1.0);

        await Assert.That(mdc.SecondaryFixed.GetHct(scheme).Tone).IsEqualTo(90.0).Within(1.0);
        await Assert.That(mdc.SecondaryFixedDim.GetHct(scheme).Tone).IsEqualTo(80.0).Within(1.0);
        await Assert.That(mdc.OnSecondaryFixed.GetHct(scheme).Tone).IsEqualTo(10.0).Within(1.0);
        await Assert.That(mdc.OnSecondaryFixedVariant.GetHct(scheme).Tone).IsEqualTo(30.0).Within(1.0);

        await Assert.That(mdc.TertiaryFixed.GetHct(scheme).Tone).IsEqualTo(90.0).Within(1.0);
        await Assert.That(mdc.TertiaryFixedDim.GetHct(scheme).Tone).IsEqualTo(80.0).Within(1.0);
        await Assert.That(mdc.OnTertiaryFixed.GetHct(scheme).Tone).IsEqualTo(10.0).Within(1.0);
        await Assert.That(mdc.OnTertiaryFixedVariant.GetHct(scheme).Tone).IsEqualTo(30.0).Within(1.0);
    }

    [Test]
    [DisplayName("Fixed colors in light monochrome schemes")]
    public async Task Fixed_Colors_Light_Monochrome()
    {
        var mdc = new MaterialDynamicColors();
        var scheme = new SchemeMonochrome(
            sourceColorHct: Hct.FromInt(unchecked((int)0xFFFF0000)),
            isDark: false,
            contrastLevel: 0.0);

        await Assert.That(mdc.PrimaryFixed.GetHct(scheme).Tone).IsEqualTo(40.0).Within(1.0);
        await Assert.That(mdc.PrimaryFixedDim.GetHct(scheme).Tone).IsEqualTo(30.0).Within(1.0);
        await Assert.That(mdc.OnPrimaryFixed.GetHct(scheme).Tone).IsEqualTo(100.0).Within(1.0);
        await Assert.That(mdc.OnPrimaryFixedVariant.GetHct(scheme).Tone).IsEqualTo(90.0).Within(1.0);

        await Assert.That(mdc.SecondaryFixed.GetHct(scheme).Tone).IsEqualTo(80.0).Within(1.0);
        await Assert.That(mdc.SecondaryFixedDim.GetHct(scheme).Tone).IsEqualTo(70.0).Within(1.0);
        await Assert.That(mdc.OnSecondaryFixed.GetHct(scheme).Tone).IsEqualTo(10.0).Within(1.0);
        await Assert.That(mdc.OnSecondaryFixedVariant.GetHct(scheme).Tone).IsEqualTo(25.0).Within(1.0);

        await Assert.That(mdc.TertiaryFixed.GetHct(scheme).Tone).IsEqualTo(40.0).Within(1.0);
        await Assert.That(mdc.TertiaryFixedDim.GetHct(scheme).Tone).IsEqualTo(30.0).Within(1.0);
        await Assert.That(mdc.OnTertiaryFixed.GetHct(scheme).Tone).IsEqualTo(100.0).Within(1.0);
        await Assert.That(mdc.OnTertiaryFixedVariant.GetHct(scheme).Tone).IsEqualTo(90.0).Within(1.0);
    }

    [Test]
    [DisplayName("Fixed colors in dark monochrome schemes")]
    public async Task Fixed_Colors_Dark_Monochrome()
    {
        var mdc = new MaterialDynamicColors();
        var scheme = new SchemeMonochrome(
            sourceColorHct: Hct.FromInt(unchecked((int)0xFFFF0000)),
            isDark: true,
            contrastLevel: 0.0);

        await Assert.That(mdc.PrimaryFixed.GetHct(scheme).Tone).IsEqualTo(40.0).Within(1.0);
        await Assert.That(mdc.PrimaryFixedDim.GetHct(scheme).Tone).IsEqualTo(30.0).Within(1.0);
        await Assert.That(mdc.OnPrimaryFixed.GetHct(scheme).Tone).IsEqualTo(100.0).Within(1.0);
        await Assert.That(mdc.OnPrimaryFixedVariant.GetHct(scheme).Tone).IsEqualTo(90.0).Within(1.0);

        await Assert.That(mdc.SecondaryFixed.GetHct(scheme).Tone).IsEqualTo(80.0).Within(1.0);
        await Assert.That(mdc.SecondaryFixedDim.GetHct(scheme).Tone).IsEqualTo(70.0).Within(1.0);
        await Assert.That(mdc.OnSecondaryFixed.GetHct(scheme).Tone).IsEqualTo(10.0).Within(1.0);
        await Assert.That(mdc.OnSecondaryFixedVariant.GetHct(scheme).Tone).IsEqualTo(25.0).Within(1.0);

        await Assert.That(mdc.TertiaryFixed.GetHct(scheme).Tone).IsEqualTo(40.0).Within(1.0);
        await Assert.That(mdc.TertiaryFixedDim.GetHct(scheme).Tone).IsEqualTo(30.0).Within(1.0);
        await Assert.That(mdc.OnTertiaryFixed.GetHct(scheme).Tone).IsEqualTo(100.0).Within(1.0);
        await Assert.That(mdc.OnTertiaryFixedVariant.GetHct(scheme).Tone).IsEqualTo(90.0).Within(1.0);
    }
}
