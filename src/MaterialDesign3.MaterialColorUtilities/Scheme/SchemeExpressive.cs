namespace MaterialColorUtilities;

public class SchemeExpressive: DynamicScheme
{
    public SchemeExpressive(Hct sourceColorHct, bool isDark, double contrastLevel)
        : this(sourceColorHct, isDark, contrastLevel, DEFAULT_SPEC_VERSION, DEFAULT_PLATFORM)
    {
    }

    public SchemeExpressive(
        Hct sourceColorHct,
        bool isDark,
        double contrastLevel,
        SpecVersion specVersion,
        Platform platform)
        : base(
            sourceColorHct,
            Variant.EXPRESSIVE,
            isDark,
            contrastLevel,
            platform,
            specVersion,
            ColorSpecs.Get(specVersion).GetPrimaryPalette(Variant.EXPRESSIVE, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetSecondaryPalette(Variant.EXPRESSIVE, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetTertiaryPalette(Variant.EXPRESSIVE, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetNeutralPalette(Variant.EXPRESSIVE, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetNeutralVariantPalette(Variant.EXPRESSIVE, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetErrorPalette(Variant.EXPRESSIVE, sourceColorHct, isDark, platform, contrastLevel))
    {
    }
}