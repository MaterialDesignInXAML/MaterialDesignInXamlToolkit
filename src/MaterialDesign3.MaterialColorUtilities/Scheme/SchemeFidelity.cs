namespace MaterialColorUtilities;

public class SchemeFidelity: DynamicScheme
{
    public SchemeFidelity(Hct sourceColorHct, bool isDark, double contrastLevel)
        : this(sourceColorHct, isDark, contrastLevel, DEFAULT_SPEC_VERSION, DEFAULT_PLATFORM)
    {
    }

    public SchemeFidelity(
        Hct sourceColorHct,
        bool isDark,
        double contrastLevel,
        SpecVersion specVersion,
        Platform platform)
        : base(
            sourceColorHct,
            Variant.FIDELITY,
            isDark,
            contrastLevel,
            platform,
            specVersion,
            ColorSpecs.Get(specVersion).GetPrimaryPalette(Variant.FIDELITY, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetSecondaryPalette(Variant.FIDELITY, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetTertiaryPalette(Variant.FIDELITY, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetNeutralPalette(Variant.FIDELITY, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetNeutralVariantPalette(Variant.FIDELITY, sourceColorHct, isDark, platform, contrastLevel),
            ColorSpecs.Get(specVersion).GetErrorPalette(Variant.FIDELITY, sourceColorHct, isDark, platform, contrastLevel))
    {
    }
}