namespace MaterialColorUtilities;

/// <summary>
/// Comprises foundational palettes to build a color scheme.
/// Generated from a source color, these palettes will then be part of a DynamicScheme together with appearance preferences.
/// </summary>
public record CorePalettes(
    TonalPalette Primary,
    TonalPalette Secondary,
    TonalPalette Tertiary,
    TonalPalette Neutral,
    TonalPalette NeutralVariant);