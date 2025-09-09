namespace MaterialColorUtilities;

public interface ColorSpec
{
    // Main Palettes
    DynamicColor PrimaryPaletteKeyColor { get; }
    DynamicColor SecondaryPaletteKeyColor { get; }
    DynamicColor TertiaryPaletteKeyColor { get; }
    DynamicColor NeutralPaletteKeyColor { get; }
    DynamicColor NeutralVariantPaletteKeyColor { get; }
    DynamicColor ErrorPaletteKeyColor { get; }

    // Surfaces [S]
    DynamicColor Background { get; }
    DynamicColor OnBackground { get; }
    DynamicColor Surface { get; }
    DynamicColor SurfaceDim { get; }
    DynamicColor SurfaceBright { get; }
    DynamicColor SurfaceContainerLowest { get; }
    DynamicColor SurfaceContainerLow { get; }
    DynamicColor SurfaceContainer { get; }
    DynamicColor SurfaceContainerHigh { get; }
    DynamicColor SurfaceContainerHighest { get; }
    DynamicColor OnSurface { get; }
    DynamicColor SurfaceVariant { get; }
    DynamicColor OnSurfaceVariant { get; }
    DynamicColor InverseSurface { get; }
    DynamicColor InverseOnSurface { get; }
    DynamicColor Outline { get; }
    DynamicColor OutlineVariant { get; }
    DynamicColor Shadow { get; }
    DynamicColor Scrim { get; }
    DynamicColor SurfaceTint { get; }

    // Primaries [P]
    DynamicColor Primary { get; }
    DynamicColor? PrimaryDim { get; }
    DynamicColor OnPrimary { get; }
    DynamicColor PrimaryContainer { get; }
    DynamicColor OnPrimaryContainer { get; }
    DynamicColor InversePrimary { get; }

    // Secondaries [Q]
    DynamicColor Secondary { get; }
    DynamicColor? SecondaryDim { get; }
    DynamicColor OnSecondary { get; }
    DynamicColor SecondaryContainer { get; }
    DynamicColor OnSecondaryContainer { get; }

    // Tertiaries [T]
    DynamicColor Tertiary { get; }
    DynamicColor? TertiaryDim { get; }
    DynamicColor OnTertiary { get; }
    DynamicColor TertiaryContainer { get; }
    DynamicColor OnTertiaryContainer { get; }

    // Errors [E]
    DynamicColor Error { get; }
    DynamicColor? ErrorDim { get; }
    DynamicColor OnError { get; }
    DynamicColor ErrorContainer { get; }
    DynamicColor OnErrorContainer { get; }

    // Primary Fixed Colors [PF]
    DynamicColor PrimaryFixed { get; }
    DynamicColor PrimaryFixedDim { get; }
    DynamicColor OnPrimaryFixed { get; }
    DynamicColor OnPrimaryFixedVariant { get; }

    // Secondary Fixed Colors [QF]
    DynamicColor SecondaryFixed { get; }
    DynamicColor SecondaryFixedDim { get; }
    DynamicColor OnSecondaryFixed { get; }
    DynamicColor OnSecondaryFixedVariant { get; }

    // Tertiary Fixed Colors [TF]
    DynamicColor TertiaryFixed { get; }
    DynamicColor TertiaryFixedDim { get; }
    DynamicColor OnTertiaryFixed { get; }
    DynamicColor OnTertiaryFixedVariant { get; }

    // Android-only Colors
    DynamicColor ControlActivated { get; }
    DynamicColor ControlNormal { get; }
    DynamicColor ControlHighlight { get; }
    DynamicColor TextPrimaryInverse { get; }
    DynamicColor TextSecondaryAndTertiaryInverse { get; }
    DynamicColor TextPrimaryInverseDisableOnly { get; }
    DynamicColor TextSecondaryAndTertiaryInverseDisabled { get; }
    DynamicColor TextHintInverse { get; }

    // Other
    DynamicColor HighestSurface(DynamicScheme s);

    // Color value calculations
    Hct GetHct(DynamicScheme scheme, DynamicColor color);
    double GetTone(DynamicScheme scheme, DynamicColor color);

    // Scheme Palettes
    TonalPalette GetPrimaryPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel);
    TonalPalette GetSecondaryPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel);
    TonalPalette GetTertiaryPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel);
    TonalPalette GetNeutralPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel);
    TonalPalette GetNeutralVariantPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel);
    TonalPalette? GetErrorPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel);
}