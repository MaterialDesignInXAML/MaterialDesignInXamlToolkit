using System.Windows.Media;

namespace MaterialColorUtilities;

/// <summary>
/// Factory for creating a dynamic color scheme.
/// </summary>
public static class DynamicSchemeFactory
{
    /// <summary>
    /// Factory method for creating a dynamic color scheme.
    /// </summary>
    /// <remarks>
    /// The colors are optional. If any of them are null,
    /// the color will be automatically generated based on the source color.
    /// </remarks>
    public static DynamicScheme Create(
        Color sourceColor,
        Variant variant,
        bool isDark,
        double contrastLevel,
        Platform platform,
        SpecVersion specVersion,
        Color? primary,
        Color? secondary,
        Color? tertiary,
        Color? neutral,
        Color? neutralVariant,
        Color? error)
    {
        var sourceColorHct = Hct.FromInt(ColorUtils.ArgbFromColor(sourceColor));

        TonalPalette primaryPalette = primary == null
            ? ColorSpecs.Get(specVersion).GetPrimaryPalette(variant, sourceColorHct, isDark, platform, contrastLevel)
            : ColorSpecs.Get(specVersion).GetPrimaryPalette(variant, Hct.FromInt(ColorUtils.ArgbFromColor(primary.Value)), isDark, platform, contrastLevel);

        TonalPalette secondaryPalette = secondary == null
            ? ColorSpecs.Get(specVersion).GetSecondaryPalette(variant, sourceColorHct, isDark, platform, contrastLevel)
            : ColorSpecs.Get(specVersion).GetSecondaryPalette(variant, Hct.FromInt(ColorUtils.ArgbFromColor(secondary.Value)), isDark, platform, contrastLevel);

        TonalPalette tertiaryPalette = tertiary == null
            ? ColorSpecs.Get(specVersion).GetTertiaryPalette(variant, sourceColorHct, isDark, platform, contrastLevel)
            : ColorSpecs.Get(specVersion).GetTertiaryPalette(variant, Hct.FromInt(ColorUtils.ArgbFromColor(tertiary.Value)), isDark, platform, contrastLevel);

        TonalPalette neutralPalette = neutral == null
            ? ColorSpecs.Get(specVersion).GetNeutralPalette(variant, sourceColorHct, isDark, platform, contrastLevel)
            : ColorSpecs.Get(specVersion).GetNeutralPalette(variant, Hct.FromInt(ColorUtils.ArgbFromColor(neutral.Value)), isDark, platform, contrastLevel);

        TonalPalette neutralVariantPalette = neutralVariant == null
            ? ColorSpecs.Get(specVersion).GetNeutralVariantPalette(variant, sourceColorHct, isDark, platform, contrastLevel)
            : ColorSpecs.Get(specVersion).GetNeutralVariantPalette(variant, Hct.FromInt(ColorUtils.ArgbFromColor(neutralVariant.Value)), isDark, platform, contrastLevel);

        TonalPalette? errorPalette = error == null
            ? ColorSpecs.Get(specVersion).GetErrorPalette(variant, sourceColorHct, isDark, platform, contrastLevel)
            : ColorSpecs.Get(specVersion).GetErrorPalette(variant, Hct.FromInt(ColorUtils.ArgbFromColor(error.Value)), isDark, platform, contrastLevel);

        return new DynamicScheme(
            sourceColorHct,
            variant,
            isDark,
            contrastLevel,
            platform,
            specVersion,
            primaryPalette,
            secondaryPalette,
            tertiaryPalette,
            neutralPalette,
            neutralVariantPalette,
            errorPalette);
    }
}
