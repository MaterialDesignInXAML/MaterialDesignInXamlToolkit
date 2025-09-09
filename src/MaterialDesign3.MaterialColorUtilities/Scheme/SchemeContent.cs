namespace MaterialColorUtilities;

public class SchemeContent: DynamicScheme
{
    public SchemeContent(Hct sourceColorHct, bool isDark, double contrastLevel)
        : this(sourceColorHct, isDark, contrastLevel, DEFAULT_SPEC_VERSION, DEFAULT_PLATFORM)
    {
    }

    public SchemeContent(
        Hct sourceColorHct,
        bool isDark,
        double contrastLevel,
        SpecVersion specVersion,
        Platform platform)
        : base(
            sourceColorHct,
            Variant.CONTENT,
            isDark,
            contrastLevel,
            platform,
            specVersion,
            ColorSpecs.Get(specVersion).GetPrimaryPalette(Variant.CONTENT, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetSecondaryPalette(Variant.CONTENT, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetTertiaryPalette(Variant.CONTENT, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetNeutralPalette(Variant.CONTENT, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetNeutralVariantPalette(Variant.CONTENT, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetErrorPalette(Variant.CONTENT, sourceColorHct, isDark, platform, contrastLevel))
    {
    }
}