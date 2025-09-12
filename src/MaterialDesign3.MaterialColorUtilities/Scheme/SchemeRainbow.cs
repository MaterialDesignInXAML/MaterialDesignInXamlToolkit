namespace MaterialColorUtilities;

public class SchemeRainbow: DynamicScheme
{
    public SchemeRainbow(Hct sourceColorHct, bool isDark, double contrastLevel)
        : this(sourceColorHct, isDark, contrastLevel, DEFAULT_SPEC_VERSION, DEFAULT_PLATFORM)
    {
    }

    public SchemeRainbow(
        Hct sourceColorHct,
        bool isDark,
        double contrastLevel,
        SpecVersion specVersion,
        Platform platform)
        : base(
            sourceColorHct,
            Variant.RAINBOW,
            isDark,
            contrastLevel,
            platform,
            specVersion,
            ColorSpecs.Get(specVersion).GetPrimaryPalette(Variant.RAINBOW, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetSecondaryPalette(Variant.RAINBOW, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetTertiaryPalette(Variant.RAINBOW, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetNeutralPalette(Variant.RAINBOW, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetNeutralVariantPalette(Variant.RAINBOW, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetErrorPalette(Variant.RAINBOW, sourceColorHct, isDark, platform, contrastLevel))
    {
    }
}