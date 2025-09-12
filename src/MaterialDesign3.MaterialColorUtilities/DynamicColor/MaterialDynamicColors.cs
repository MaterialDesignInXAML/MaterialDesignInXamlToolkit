namespace MaterialColorUtilities;

public sealed class MaterialDynamicColors
{
    private static readonly ColorSpec colorSpec = new ColorSpec2025();

    public DynamicColor HighestSurface(DynamicScheme s) => colorSpec.HighestSurface(s);

    // Main Palettes
    public DynamicColor PrimaryPaletteKeyColor => colorSpec.PrimaryPaletteKeyColor;
    public DynamicColor SecondaryPaletteKeyColor => colorSpec.SecondaryPaletteKeyColor;
    public DynamicColor TertiaryPaletteKeyColor => colorSpec.TertiaryPaletteKeyColor;
    public DynamicColor NeutralPaletteKeyColor => colorSpec.NeutralPaletteKeyColor;
    public DynamicColor NeutralVariantPaletteKeyColor => colorSpec.NeutralVariantPaletteKeyColor;
    public DynamicColor ErrorPaletteKeyColor => colorSpec.ErrorPaletteKeyColor;

    // Surfaces [S]
    public DynamicColor Background => colorSpec.Background;
    public DynamicColor OnBackground => colorSpec.OnBackground;
    public DynamicColor Surface => colorSpec.Surface;
    public DynamicColor SurfaceDim => colorSpec.SurfaceDim;
    public DynamicColor SurfaceBright => colorSpec.SurfaceBright;
    public DynamicColor SurfaceContainerLowest => colorSpec.SurfaceContainerLowest;
    public DynamicColor SurfaceContainerLow => colorSpec.SurfaceContainerLow;
    public DynamicColor SurfaceContainer => colorSpec.SurfaceContainer;
    public DynamicColor SurfaceContainerHigh => colorSpec.SurfaceContainerHigh;
    public DynamicColor SurfaceContainerHighest => colorSpec.SurfaceContainerHighest;
    public DynamicColor OnSurface => colorSpec.OnSurface;
    public DynamicColor SurfaceVariant => colorSpec.SurfaceVariant;
    public DynamicColor OnSurfaceVariant => colorSpec.OnSurfaceVariant;
    public DynamicColor InverseSurface => colorSpec.InverseSurface;
    public DynamicColor InverseOnSurface => colorSpec.InverseOnSurface;
    public DynamicColor Outline => colorSpec.Outline;
    public DynamicColor OutlineVariant => colorSpec.OutlineVariant;
    public DynamicColor Shadow => colorSpec.Shadow;
    public DynamicColor Scrim => colorSpec.Scrim;
    public DynamicColor SurfaceTint => colorSpec.SurfaceTint;

    // Primaries [P]
    public DynamicColor Primary => colorSpec.Primary;
    public DynamicColor? PrimaryDim => colorSpec.PrimaryDim;
    public DynamicColor OnPrimary => colorSpec.OnPrimary;
    public DynamicColor PrimaryContainer => colorSpec.PrimaryContainer;
    public DynamicColor OnPrimaryContainer => colorSpec.OnPrimaryContainer;
    public DynamicColor InversePrimary => colorSpec.InversePrimary;

    // Secondaries [Q]
    public DynamicColor Secondary => colorSpec.Secondary;
    public DynamicColor? SecondaryDim => colorSpec.SecondaryDim;
    public DynamicColor OnSecondary => colorSpec.OnSecondary;
    public DynamicColor SecondaryContainer => colorSpec.SecondaryContainer;
    public DynamicColor OnSecondaryContainer => colorSpec.OnSecondaryContainer;

    // Secondary Fixed [QF]
    public DynamicColor SecondaryFixed => colorSpec.SecondaryFixed;
    public DynamicColor SecondaryFixedDim => colorSpec.SecondaryFixedDim;
    public DynamicColor OnSecondaryFixed => colorSpec.OnSecondaryFixed;
    public DynamicColor OnSecondaryFixedVariant => colorSpec.OnSecondaryFixedVariant;

    // Tertiaries [T]
    public DynamicColor Tertiary => colorSpec.Tertiary;
    public DynamicColor? TertiaryDim => colorSpec.TertiaryDim;
    public DynamicColor OnTertiary => colorSpec.OnTertiary;
    public DynamicColor TertiaryContainer => colorSpec.TertiaryContainer;
    public DynamicColor OnTertiaryContainer => colorSpec.OnTertiaryContainer;

    // Tertiary Fixed [TF]
    public DynamicColor TertiaryFixed => colorSpec.TertiaryFixed;
    public DynamicColor TertiaryFixedDim => colorSpec.TertiaryFixedDim;
    public DynamicColor OnTertiaryFixed => colorSpec.OnTertiaryFixed;
    public DynamicColor OnTertiaryFixedVariant => colorSpec.OnTertiaryFixedVariant;

    // Errors [E]
    public DynamicColor Error => colorSpec.Error;
    public DynamicColor? ErrorDim => colorSpec.ErrorDim;
    public DynamicColor OnError => colorSpec.OnError;
    public DynamicColor ErrorContainer => colorSpec.ErrorContainer;
    public DynamicColor OnErrorContainer => colorSpec.OnErrorContainer;

    // Primary Fixed
    public DynamicColor PrimaryFixed => colorSpec.PrimaryFixed;
    public DynamicColor PrimaryFixedDim => colorSpec.PrimaryFixedDim;
    public DynamicColor OnPrimaryFixed => colorSpec.OnPrimaryFixed;
    public DynamicColor OnPrimaryFixedVariant => colorSpec.OnPrimaryFixedVariant;

    // Android-only
    public DynamicColor ControlActivated => colorSpec.ControlActivated;
    public DynamicColor ControlNormal => colorSpec.ControlNormal;
    public DynamicColor ControlHighlight => colorSpec.ControlHighlight;
    public DynamicColor TextPrimaryInverse => colorSpec.TextPrimaryInverse;
    public DynamicColor TextSecondaryAndTertiaryInverse => colorSpec.TextSecondaryAndTertiaryInverse;
    public DynamicColor TextPrimaryInverseDisableOnly => colorSpec.TextPrimaryInverseDisableOnly;
    public DynamicColor TextSecondaryAndTertiaryInverseDisabled => colorSpec.TextSecondaryAndTertiaryInverseDisabled;
    public DynamicColor TextHintInverse => colorSpec.TextHintInverse;

    // All colors
    public IEnumerable<DynamicColor> AllDynamicColors()
    {
        yield return PrimaryPaletteKeyColor;
        yield return SecondaryPaletteKeyColor;
        yield return TertiaryPaletteKeyColor;
        yield return NeutralPaletteKeyColor;
        yield return NeutralVariantPaletteKeyColor;
        yield return ErrorPaletteKeyColor;
        yield return Background;
        yield return OnBackground;
        yield return Surface;
        yield return SurfaceDim;
        yield return SurfaceBright;
        yield return SurfaceContainerLowest;
        yield return SurfaceContainerLow;
        yield return SurfaceContainer;
        yield return SurfaceContainerHigh;
        yield return SurfaceContainerHighest;
        yield return OnSurface;
        yield return SurfaceVariant;
        yield return OnSurfaceVariant;
        yield return Outline;
        yield return OutlineVariant;
        yield return InverseSurface;
        yield return InverseOnSurface;
        yield return Shadow;
        yield return Scrim;
        yield return SurfaceTint;
        yield return Primary;
        yield return PrimaryDim!;
        yield return OnPrimary;
        yield return PrimaryContainer;
        yield return OnPrimaryContainer;
        yield return PrimaryFixed;
        yield return PrimaryFixedDim;
        yield return OnPrimaryFixed;
        yield return OnPrimaryFixedVariant;
        yield return InversePrimary;
        yield return Secondary;
        yield return SecondaryDim!;
        yield return OnSecondary;
        yield return SecondaryContainer;
        yield return OnSecondaryContainer;
        yield return SecondaryFixed;
        yield return SecondaryFixedDim;
        yield return OnSecondaryFixed;
        yield return OnSecondaryFixedVariant;
        yield return Tertiary;
        yield return TertiaryDim!;
        yield return OnTertiary;
        yield return TertiaryContainer;
        yield return OnTertiaryContainer;
        yield return TertiaryFixed;
        yield return TertiaryFixedDim;
        yield return OnTertiaryFixed;
        yield return OnTertiaryFixedVariant;
        yield return Error;
        yield return ErrorDim!;
        yield return OnError;
        yield return ErrorContainer;
        yield return OnErrorContainer;
        yield return ControlActivated;
        yield return ControlNormal;
        yield return ControlHighlight;
        yield return TextPrimaryInverse;
        yield return TextSecondaryAndTertiaryInverse;
        yield return TextPrimaryInverseDisableOnly;
        yield return TextSecondaryAndTertiaryInverseDisabled;
        yield return TextHintInverse;
    }
}