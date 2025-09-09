namespace MaterialColorUtilities;

public class ColorSpec2021 : ColorSpec
{
    // Main Palettes
    public DynamicColor PrimaryPaletteKeyColor => new DynamicColor.Builder()
        .SetName("primary_palette_key_color")
        .SetPalette(s => s.PrimaryPalette)
        .SetTone(s => s.PrimaryPalette.GetKeyColor().Tone)
        .Build();

    public DynamicColor SecondaryPaletteKeyColor => new DynamicColor.Builder()
        .SetName("secondary_palette_key_color")
        .SetPalette(s => s.SecondaryPalette)
        .SetTone(s => s.SecondaryPalette.GetKeyColor().Tone)
        .Build();

    public DynamicColor TertiaryPaletteKeyColor => new DynamicColor.Builder()
        .SetName("tertiary_palette_key_color")
        .SetPalette(s => s.TertiaryPalette)
        .SetTone(s => s.TertiaryPalette.GetKeyColor().Tone)
        .Build();

    public DynamicColor NeutralPaletteKeyColor => new DynamicColor.Builder()
        .SetName("neutral_palette_key_color")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.NeutralPalette.GetKeyColor().Tone)
        .Build();

    public DynamicColor NeutralVariantPaletteKeyColor => new DynamicColor.Builder()
        .SetName("neutral_variant_palette_key_color")
        .SetPalette(s => s.NeutralVariantPalette)
        .SetTone(s => s.NeutralVariantPalette.GetKeyColor().Tone)
        .Build();

    public DynamicColor ErrorPaletteKeyColor => new DynamicColor.Builder()
        .SetName("error_palette_key_color")
        .SetPalette(s => s.ErrorPalette)
        .SetTone(s => s.ErrorPalette.GetKeyColor().Tone)
        .Build();

    // Surfaces [S]
    public DynamicColor Background => new DynamicColor.Builder()
        .SetName("background")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? 6.0 : 98.0)
        .SetIsBackground(true)
        .Build();

    public DynamicColor OnBackground => new DynamicColor.Builder()
        .SetName("on_background")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? 90.0 : 10.0)
        .SetBackground(_ => Background)
        .SetContrastCurve(_ => new ContrastCurve(3.0, 3.0, 4.5, 7.0))
        .Build();

    public DynamicColor Surface => new DynamicColor.Builder()
        .SetName("surface")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? 6.0 : 98.0)
        .SetIsBackground(true)
        .Build();

    public DynamicColor SurfaceDim => new DynamicColor.Builder()
        .SetName("surface_dim")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? 6.0 : new ContrastCurve(87.0, 87.0, 80.0, 75.0).Get(s.ContrastLevel))
        .SetIsBackground(true)
        .Build();

    public DynamicColor SurfaceBright => new DynamicColor.Builder()
        .SetName("surface_bright")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? new ContrastCurve(24.0, 24.0, 29.0, 34.0).Get(s.ContrastLevel) : 98.0)
        .SetIsBackground(true)
        .Build();

    public DynamicColor SurfaceContainerLowest => new DynamicColor.Builder()
        .SetName("surface_container_lowest")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? new ContrastCurve(4.0, 4.0, 2.0, 0.0).Get(s.ContrastLevel) : 100.0)
        .SetIsBackground(true)
        .Build();

    public DynamicColor SurfaceContainerLow => new DynamicColor.Builder()
        .SetName("surface_container_low")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? new ContrastCurve(10.0, 10.0, 11.0, 12.0).Get(s.ContrastLevel) : new ContrastCurve(96.0, 96.0, 96.0, 95.0).Get(s.ContrastLevel))
        .SetIsBackground(true)
        .Build();

    public DynamicColor SurfaceContainer => new DynamicColor.Builder()
        .SetName("surface_container")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? new ContrastCurve(12.0, 12.0, 16.0, 20.0).Get(s.ContrastLevel) : new ContrastCurve(94.0, 94.0, 92.0, 90.0).Get(s.ContrastLevel))
        .SetIsBackground(true)
        .Build();

    public DynamicColor SurfaceContainerHigh => new DynamicColor.Builder()
        .SetName("surface_container_high")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? new ContrastCurve(17.0, 17.0, 21.0, 25.0).Get(s.ContrastLevel) : new ContrastCurve(92.0, 92.0, 88.0, 85.0).Get(s.ContrastLevel))
        .SetIsBackground(true)
        .Build();

    public DynamicColor SurfaceContainerHighest => new DynamicColor.Builder()
        .SetName("surface_container_highest")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? new ContrastCurve(22.0, 22.0, 26.0, 30.0).Get(s.ContrastLevel) : new ContrastCurve(90.0, 90.0, 84.0, 80.0).Get(s.ContrastLevel))
        .SetIsBackground(true)
        .Build();

    public DynamicColor OnSurface => new DynamicColor.Builder()
        .SetName("on_surface")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? 90.0 : 10.0)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(4.5, 7.0, 11.0, 21.0))
        .Build();

    public DynamicColor SurfaceVariant => new DynamicColor.Builder()
        .SetName("surface_variant")
        .SetPalette(s => s.NeutralVariantPalette)
        .SetTone(s => s.IsDark ? 30.0 : 90.0)
        .SetIsBackground(true)
        .Build();

    public DynamicColor OnSurfaceVariant => new DynamicColor.Builder()
        .SetName("on_surface_variant")
        .SetPalette(s => s.NeutralVariantPalette)
        .SetTone(s => s.IsDark ? 80.0 : 30.0)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(3.0, 4.5, 7.0, 11.0))
        .Build();

    public DynamicColor InverseSurface => new DynamicColor.Builder()
        .SetName("inverse_surface")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? 90.0 : 20.0)
        .SetIsBackground(true)
        .Build();

    public DynamicColor InverseOnSurface => new DynamicColor.Builder()
        .SetName("inverse_on_surface")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? 20.0 : 95.0)
        .SetBackground(_ => InverseSurface)
        .SetContrastCurve(_ => new ContrastCurve(4.5, 7.0, 11.0, 21.0))
        .Build();

    public DynamicColor Outline => new DynamicColor.Builder()
        .SetName("outline")
        .SetPalette(s => s.NeutralVariantPalette)
        .SetTone(s => s.IsDark ? 60.0 : 50.0)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(1.5, 3.0, 4.5, 7.0))
        .Build();

    public DynamicColor OutlineVariant => new DynamicColor.Builder()
        .SetName("outline_variant")
        .SetPalette(s => s.NeutralVariantPalette)
        .SetTone(s => s.IsDark ? 30.0 : 80.0)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(1.0, 1.0, 3.0, 4.5))
        .Build();

    public DynamicColor Shadow => new DynamicColor.Builder()
        .SetName("shadow")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(_ => 0.0)
        .Build();

    public DynamicColor Scrim => new DynamicColor.Builder()
        .SetName("scrim")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(_ => 0.0)
        .Build();

    public DynamicColor SurfaceTint => new DynamicColor.Builder()
        .SetName("surface_tint")
        .SetPalette(s => s.PrimaryPalette)
        .SetTone(s => s.IsDark ? 80.0 : 40.0)
        .SetIsBackground(true)
        .Build();

    // Primaries [P]
    public DynamicColor Primary => new DynamicColor.Builder()
        .SetName("primary")
        .SetPalette(s => s.PrimaryPalette)
        .SetTone(s => IsMonochrome(s) ? (s.IsDark ? 100.0 : 0.0) : (s.IsDark ? 80.0 : 40.0))
        .SetIsBackground(true)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(3.0, 4.5, 7.0, 7.0))
        .SetToneDeltaPair(_ => new ToneDeltaPair(PrimaryContainer, Primary, 10.0, TonePolarity.NEARER, false))
        .Build();

    public DynamicColor? PrimaryDim => null;

    public DynamicColor OnPrimary => new DynamicColor.Builder()
        .SetName("on_primary")
        .SetPalette(s => s.PrimaryPalette)
        .SetTone(s => IsMonochrome(s) ? (s.IsDark ? 10.0 : 90.0) : (s.IsDark ? 20.0 : 100.0))
        .SetBackground(_ => Primary)
        .SetContrastCurve(_ => new ContrastCurve(4.5, 7.0, 11.0, 21.0))
        .Build();

    public DynamicColor PrimaryContainer => new DynamicColor.Builder()
        .SetName("primary_container")
        .SetPalette(s => s.PrimaryPalette)
        .SetTone(s =>
        {
            if (IsFidelity(s)) return s.SourceColorHct.Tone;
            if (IsMonochrome(s)) return s.IsDark ? 85.0 : 25.0;
            return s.IsDark ? 30.0 : 90.0;
        })
        .SetIsBackground(true)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(1.0, 1.0, 3.0, 4.5))
        .SetToneDeltaPair(_ => new ToneDeltaPair(PrimaryContainer, Primary, 10.0, TonePolarity.NEARER, false))
        .Build();

    public DynamicColor OnPrimaryContainer => new DynamicColor.Builder()
        .SetName("on_primary_container")
        .SetPalette(s => s.PrimaryPalette)
        .SetTone(s =>
        {
            if (IsFidelity(s)) return DynamicColor.ForegroundTone(PrimaryContainer.GetTone(s), 4.5);
            if (IsMonochrome(s)) return s.IsDark ? 0.0 : 100.0;
            return s.IsDark ? 90.0 : 30.0;
        })
        .SetBackground(_ => PrimaryContainer)
        .SetContrastCurve(_ => new ContrastCurve(3.0, 4.5, 7.0, 11.0))
        .Build();

    public DynamicColor InversePrimary => new DynamicColor.Builder()
        .SetName("inverse_primary")
        .SetPalette(s => s.PrimaryPalette)
        .SetTone(s => s.IsDark ? 40.0 : 80.0)
        .SetBackground(_ => InverseSurface)
        .SetContrastCurve(_ => new ContrastCurve(3.0, 4.5, 7.0, 7.0))
        .Build();

    // Secondaries [Q]
    public DynamicColor Secondary => new DynamicColor.Builder()
        .SetName("secondary")
        .SetPalette(s => s.SecondaryPalette)
        .SetTone(s => s.IsDark ? 80.0 : 40.0)
        .SetIsBackground(true)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(3.0, 4.5, 7.0, 7.0))
        .SetToneDeltaPair(_ => new ToneDeltaPair(SecondaryContainer, Secondary, 10.0, TonePolarity.NEARER, false))
        .Build();

    public DynamicColor? SecondaryDim => null;

    public DynamicColor OnSecondary => new DynamicColor.Builder()
        .SetName("on_secondary")
        .SetPalette(s => s.SecondaryPalette)
        .SetTone(s => IsMonochrome(s) ? (s.IsDark ? 10.0 : 100.0) : (s.IsDark ? 20.0 : 100.0))
        .SetBackground(_ => Secondary)
        .SetContrastCurve(_ => new ContrastCurve(4.5, 7.0, 11.0, 21.0))
        .Build();

    public DynamicColor SecondaryContainer => new DynamicColor.Builder()
        .SetName("secondary_container")
        .SetPalette(s => s.SecondaryPalette)
        .SetTone(s =>
        {
            var initialTone = s.IsDark ? 30.0 : 90.0;
            if (IsMonochrome(s)) return s.IsDark ? 30.0 : 85.0;
            if (!IsFidelity(s)) return initialTone;
            return FindDesiredChromaByTone(s.SecondaryPalette.GetHue(), s.SecondaryPalette.GetChroma(), initialTone, !s.IsDark);
        })
        .SetIsBackground(true)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(1.0, 1.0, 3.0, 4.5))
        .SetToneDeltaPair(_ => new ToneDeltaPair(SecondaryContainer, Secondary, 10.0, TonePolarity.NEARER, false))
        .Build();

    public DynamicColor OnSecondaryContainer => new DynamicColor.Builder()
        .SetName("on_secondary_container")
        .SetPalette(s => s.SecondaryPalette)
        .SetTone(s =>
        {
            if (IsMonochrome(s)) return s.IsDark ? 90.0 : 10.0;
            if (!IsFidelity(s)) return s.IsDark ? 90.0 : 30.0;
            return DynamicColor.ForegroundTone(SecondaryContainer.GetTone(s), 4.5);
        })
        .SetBackground(_ => SecondaryContainer)
        .SetContrastCurve(_ => new ContrastCurve(3.0, 4.5, 7.0, 11.0))
        .Build();

    // Tertiaries [T]
    public DynamicColor Tertiary => new DynamicColor.Builder()
        .SetName("tertiary")
        .SetPalette(s => s.TertiaryPalette)
        .SetTone(s => IsMonochrome(s) ? (s.IsDark ? 90.0 : 25.0) : (s.IsDark ? 80.0 : 40.0))
        .SetIsBackground(true)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(3.0, 4.5, 7.0, 7.0))
        .SetToneDeltaPair(_ => new ToneDeltaPair(TertiaryContainer, Tertiary, 10.0, TonePolarity.NEARER, false))
        .Build();

    public DynamicColor? TertiaryDim => null;

    public DynamicColor OnTertiary => new DynamicColor.Builder()
        .SetName("on_tertiary")
        .SetPalette(s => s.TertiaryPalette)
        .SetTone(s => IsMonochrome(s) ? (s.IsDark ? 10.0 : 90.0) : (s.IsDark ? 20.0 : 100.0))
        .SetBackground(_ => Tertiary)
        .SetContrastCurve(_ => new ContrastCurve(4.5, 7.0, 11.0, 21.0))
        .Build();

    public DynamicColor TertiaryContainer => new DynamicColor.Builder()
        .SetName("tertiary_container")
        .SetPalette(s => s.TertiaryPalette)
        .SetTone(s =>
        {
            if (IsMonochrome(s)) return s.IsDark ? 60.0 : 49.0;
            if (!IsFidelity(s)) return s.IsDark ? 30.0 : 90.0;
            var proposed = s.TertiaryPalette.GetHct(s.SourceColorHct.Tone);
            return DislikeAnalyzer.FixIfDisliked(proposed).Tone;
        })
        .SetIsBackground(true)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(1.0, 1.0, 3.0, 4.5))
        .SetToneDeltaPair(_ => new ToneDeltaPair(TertiaryContainer, Tertiary, 10.0, TonePolarity.NEARER, false))
        .Build();

    public DynamicColor OnTertiaryContainer => new DynamicColor.Builder()
        .SetName("on_tertiary_container")
        .SetPalette(s => s.TertiaryPalette)
        .SetTone(s =>
        {
            if (IsMonochrome(s)) return s.IsDark ? 0.0 : 100.0;
            if (!IsFidelity(s)) return s.IsDark ? 90.0 : 30.0;
            return DynamicColor.ForegroundTone(TertiaryContainer.GetTone(s), 4.5);
        })
        .SetBackground(_ => TertiaryContainer)
        .SetContrastCurve(_ => new ContrastCurve(3.0, 4.5, 7.0, 11.0))
        .Build();

    // Errors [E]
    public DynamicColor Error => new DynamicColor.Builder()
        .SetName("error")
        .SetPalette(s => s.ErrorPalette)
        .SetTone(s => s.IsDark ? 80.0 : 40.0)
        .SetIsBackground(true)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(3.0, 4.5, 7.0, 7.0))
        .SetToneDeltaPair(_ => new ToneDeltaPair(ErrorContainer, Error, 10.0, TonePolarity.NEARER, false))
        .Build();

    public DynamicColor? ErrorDim => null;

    public DynamicColor OnError => new DynamicColor.Builder()
        .SetName("on_error")
        .SetPalette(s => s.ErrorPalette)
        .SetTone(s => s.IsDark ? 20.0 : 100.0)
        .SetBackground(_ => Error)
        .SetContrastCurve(_ => new ContrastCurve(4.5, 7.0, 11.0, 21.0))
        .Build();

    public DynamicColor ErrorContainer => new DynamicColor.Builder()
        .SetName("error_container")
        .SetPalette(s => s.ErrorPalette)
        .SetTone(s => s.IsDark ? 30.0 : 90.0)
        .SetIsBackground(true)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(1.0, 1.0, 3.0, 4.5))
        .SetToneDeltaPair(_ => new ToneDeltaPair(ErrorContainer, Error, 10.0, TonePolarity.NEARER, false))
        .Build();

    public DynamicColor OnErrorContainer => new DynamicColor.Builder()
        .SetName("on_error_container")
        .SetPalette(s => s.ErrorPalette)
        .SetTone(s => IsMonochrome(s) ? (s.IsDark ? 90.0 : 10.0) : (s.IsDark ? 90.0 : 30.0))
        .SetBackground(_ => ErrorContainer)
        .SetContrastCurve(_ => new ContrastCurve(3.0, 4.5, 7.0, 11.0))
        .Build();

    // Primary Fixed Colors [PF]
    public DynamicColor PrimaryFixed => new DynamicColor.Builder()
        .SetName("primary_fixed")
        .SetPalette(s => s.PrimaryPalette)
        .SetTone(s => IsMonochrome(s) ? 40.0 : 90.0)
        .SetIsBackground(true)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(1.0, 1.0, 3.0, 4.5))
        .SetToneDeltaPair(_ => new ToneDeltaPair(PrimaryFixed, PrimaryFixedDim, 10.0, TonePolarity.LIGHTER, true))
        .Build();

    public DynamicColor PrimaryFixedDim => new DynamicColor.Builder()
        .SetName("primary_fixed_dim")
        .SetPalette(s => s.PrimaryPalette)
        .SetTone(s => IsMonochrome(s) ? 30.0 : 80.0)
        .SetIsBackground(true)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(1.0, 1.0, 3.0, 4.5))
        .SetToneDeltaPair(_ => new ToneDeltaPair(PrimaryFixed, PrimaryFixedDim, 10.0, TonePolarity.LIGHTER, true))
        .Build();

    public DynamicColor OnPrimaryFixed => new DynamicColor.Builder()
        .SetName("on_primary_fixed")
        .SetPalette(s => s.PrimaryPalette)
        .SetTone(s => IsMonochrome(s) ? 100.0 : 10.0)
        .SetBackground(_ => PrimaryFixedDim)
        .SetSecondBackground(_ => PrimaryFixed)
        .SetContrastCurve(_ => new ContrastCurve(4.5, 7.0, 11.0, 21.0))
        .Build();

    public DynamicColor OnPrimaryFixedVariant => new DynamicColor.Builder()
        .SetName("on_primary_fixed_variant")
        .SetPalette(s => s.PrimaryPalette)
        .SetTone(s => IsMonochrome(s) ? 90.0 : 30.0)
        .SetBackground(_ => PrimaryFixedDim)
        .SetSecondBackground(_ => PrimaryFixed)
        .SetContrastCurve(_ => new ContrastCurve(3.0, 4.5, 7.0, 11.0))
        .Build();

    // Secondary Fixed Colors [QF]
    public DynamicColor SecondaryFixed => new DynamicColor.Builder()
        .SetName("secondary_fixed")
        .SetPalette(s => s.SecondaryPalette)
        .SetTone(s => IsMonochrome(s) ? 80.0 : 90.0)
        .SetIsBackground(true)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(1.0, 1.0, 3.0, 4.5))
        .SetToneDeltaPair(_ => new ToneDeltaPair(SecondaryFixed, SecondaryFixedDim, 10.0, TonePolarity.LIGHTER, true))
        .Build();

    public DynamicColor SecondaryFixedDim => new DynamicColor.Builder()
        .SetName("secondary_fixed_dim")
        .SetPalette(s => s.SecondaryPalette)
        .SetTone(s => IsMonochrome(s) ? 70.0 : 80.0)
        .SetIsBackground(true)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(1.0, 1.0, 3.0, 4.5))
        .SetToneDeltaPair(_ => new ToneDeltaPair(SecondaryFixed, SecondaryFixedDim, 10.0, TonePolarity.LIGHTER, true))
        .Build();

    public DynamicColor OnSecondaryFixed => new DynamicColor.Builder()
        .SetName("on_secondary_fixed")
        .SetPalette(s => s.SecondaryPalette)
        .SetTone(_ => 10.0)
        .SetBackground(_ => SecondaryFixedDim)
        .SetSecondBackground(_ => SecondaryFixed)
        .SetContrastCurve(_ => new ContrastCurve(4.5, 7.0, 11.0, 21.0))
        .Build();

    public DynamicColor OnSecondaryFixedVariant => new DynamicColor.Builder()
        .SetName("on_secondary_fixed_variant")
        .SetPalette(s => s.SecondaryPalette)
        .SetTone(s => IsMonochrome(s) ? 25.0 : 30.0)
        .SetBackground(_ => SecondaryFixedDim)
        .SetSecondBackground(_ => SecondaryFixed)
        .SetContrastCurve(_ => new ContrastCurve(3.0, 4.5, 7.0, 11.0))
        .Build();

    // Tertiary Fixed Colors [TF]
    public DynamicColor TertiaryFixed => new DynamicColor.Builder()
        .SetName("tertiary_fixed")
        .SetPalette(s => s.TertiaryPalette)
        .SetTone(s => IsMonochrome(s) ? 40.0 : 90.0)
        .SetIsBackground(true)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(1.0, 1.0, 3.0, 4.5))
        .SetToneDeltaPair(_ => new ToneDeltaPair(TertiaryFixed, TertiaryFixedDim, 10.0, TonePolarity.LIGHTER, true))
        .Build();

    public DynamicColor TertiaryFixedDim => new DynamicColor.Builder()
        .SetName("tertiary_fixed_dim")
        .SetPalette(s => s.TertiaryPalette)
        .SetTone(s => IsMonochrome(s) ? 30.0 : 80.0)
        .SetIsBackground(true)
        .SetBackground(HighestSurface)
        .SetContrastCurve(_ => new ContrastCurve(1.0, 1.0, 3.0, 4.5))
        .SetToneDeltaPair(_ => new ToneDeltaPair(TertiaryFixed, TertiaryFixedDim, 10.0, TonePolarity.LIGHTER, true))
        .Build();

    public DynamicColor OnTertiaryFixed => new DynamicColor.Builder()
        .SetName("on_tertiary_fixed")
        .SetPalette(s => s.TertiaryPalette)
        .SetTone(s => IsMonochrome(s) ? 100.0 : 10.0)
        .SetBackground(_ => TertiaryFixedDim)
        .SetSecondBackground(_ => TertiaryFixed)
        .SetContrastCurve(_ => new ContrastCurve(4.5, 7.0, 11.0, 21.0))
        .Build();

    public DynamicColor OnTertiaryFixedVariant => new DynamicColor.Builder()
        .SetName("on_tertiary_fixed_variant")
        .SetPalette(s => s.TertiaryPalette)
        .SetTone(s => IsMonochrome(s) ? 90.0 : 30.0)
        .SetBackground(_ => TertiaryFixedDim)
        .SetSecondBackground(_ => TertiaryFixed)
        .SetContrastCurve(_ => new ContrastCurve(3.0, 4.5, 7.0, 11.0))
        .Build();

    // Android-only Colors
    public DynamicColor ControlActivated => new DynamicColor.Builder()
        .SetName("control_activated")
        .SetPalette(s => s.PrimaryPalette)
        .SetTone(s => s.IsDark ? 30.0 : 90.0)
        .SetIsBackground(true)
        .Build();

    public DynamicColor ControlNormal => new DynamicColor.Builder()
        .SetName("control_normal")
        .SetPalette(s => s.NeutralVariantPalette)
        .SetTone(s => s.IsDark ? 80.0 : 30.0)
        .Build();

    public DynamicColor ControlHighlight => new DynamicColor.Builder()
        .SetName("control_highlight")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? 100.0 : 0.0)
        .SetOpacity(s => s.IsDark ? 0.20 : 0.12)
        .Build();

    public DynamicColor TextPrimaryInverse => new DynamicColor.Builder()
        .SetName("text_primary_inverse")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? 10.0 : 90.0)
        .Build();

    public DynamicColor TextSecondaryAndTertiaryInverse => new DynamicColor.Builder()
        .SetName("text_secondary_and_tertiary_inverse")
        .SetPalette(s => s.NeutralVariantPalette)
        .SetTone(s => s.IsDark ? 30.0 : 80.0)
        .Build();

    public DynamicColor TextPrimaryInverseDisableOnly => new DynamicColor.Builder()
        .SetName("text_primary_inverse_disable_only")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? 10.0 : 90.0)
        .Build();

    public DynamicColor TextSecondaryAndTertiaryInverseDisabled => new DynamicColor.Builder()
        .SetName("text_secondary_and_tertiary_inverse_disabled")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? 10.0 : 90.0)
        .Build();

    public DynamicColor TextHintInverse => new DynamicColor.Builder()
        .SetName("text_hint_inverse")
        .SetPalette(s => s.NeutralPalette)
        .SetTone(s => s.IsDark ? 10.0 : 90.0)
        .Build();

    // Other
    public DynamicColor HighestSurface(DynamicScheme s) => s.IsDark ? SurfaceBright : SurfaceDim;

    private static bool IsFidelity(DynamicScheme scheme) => scheme.Variant == Variant.FIDELITY || scheme.Variant == Variant.CONTENT;
    private static bool IsMonochrome(DynamicScheme scheme) => scheme.Variant == Variant.MONOCHROME;

    private static double FindDesiredChromaByTone(double hue, double chroma, double tone, bool byDecreasingTone)
    {
        var answer = tone;
        var closest = Hct.From(hue, chroma, tone);
        if (closest.Chroma < chroma)
        {
            var chromaPeak = closest.Chroma;
            while (closest.Chroma < chroma)
            {
                answer += byDecreasingTone ? -1.0 : 1.0;
                var potential = Hct.From(hue, chroma, answer);
                if (chromaPeak > potential.Chroma)
                {
                    break;
                }
                if (System.Math.Abs(potential.Chroma - chroma) < 0.4)
                {
                    break;
                }
                var potentialDelta = System.Math.Abs(potential.Chroma - chroma);
                var currentDelta = System.Math.Abs(closest.Chroma - chroma);
                if (potentialDelta < currentDelta)
                {
                    closest = potential;
                }
                chromaPeak = System.Math.Max(chromaPeak, potential.Chroma);
            }
        }
        return answer;
    }

    // Color value calculations
    public Hct GetHct(DynamicScheme scheme, DynamicColor color)
    {
        var tone = GetTone(scheme, color);
        return color.palette(scheme).GetHct(tone);
    }

    public double GetTone(DynamicScheme scheme, DynamicColor color)
    {
        var decreasingContrast = scheme.ContrastLevel < 0;
        var toneDeltaPair = color.toneDeltaPair == null ? null : color.toneDeltaPair(scheme);

        if (toneDeltaPair != null)
        {
            var roleA = toneDeltaPair.RoleA;
            var roleB = toneDeltaPair.RoleB;
            var delta = toneDeltaPair.Delta;
            var polarity = toneDeltaPair.Polarity;
            var stayTogether = toneDeltaPair.StayTogether;

            var aIsNearer = (polarity == TonePolarity.NEARER
                || (polarity == TonePolarity.LIGHTER && !scheme.IsDark)
                || (polarity == TonePolarity.DARKER && !scheme.IsDark));
            var nearer = aIsNearer ? roleA : roleB;
            var farther = aIsNearer ? roleB : roleA;
            var amNearer = color.name.Equals(nearer.name, System.StringComparison.Ordinal);
            var expansionDir = scheme.IsDark ? 1 : -1;
            var nTone = nearer.tone(scheme);
            var fTone = farther.tone(scheme);

            if (color.background != null && nearer.contrastCurve != null && farther.contrastCurve != null)
            {
                var bg = color.background(scheme);
                var nCurve = nearer.contrastCurve(scheme);
                var fCurve = farther.contrastCurve(scheme);
                if (bg != null && nCurve != null && fCurve != null)
                {
                    var nContrast = nCurve.Get(scheme.ContrastLevel);
                    var fContrast = fCurve.Get(scheme.ContrastLevel);
                    var bgTone = bg.GetTone(scheme);
                    if (Contrast.RatioOfTones(bgTone, nTone) < nContrast)
                    {
                        nTone = DynamicColor.ForegroundTone(bgTone, nContrast);
                    }
                    if (Contrast.RatioOfTones(bgTone, fTone) < fContrast)
                    {
                        fTone = DynamicColor.ForegroundTone(bgTone, fContrast);
                    }
                    if (decreasingContrast)
                    {
                        nTone = DynamicColor.ForegroundTone(bgTone, nContrast);
                        fTone = DynamicColor.ForegroundTone(bgTone, fContrast);
                    }
                }
            }

            if ((fTone - nTone) * expansionDir < delta)
            {
                fTone = MathUtils.ClampDouble(0, 100, nTone + delta * expansionDir);
                if ((fTone - nTone) * expansionDir < delta)
                {
                    nTone = MathUtils.ClampDouble(0, 100, fTone - delta * expansionDir);
                }
            }

            if (50 <= nTone && nTone < 60)
            {
                if (expansionDir > 0)
                {
                    nTone = 60;
                    fTone = System.Math.Max(fTone, nTone + delta * expansionDir);
                }
                else
                {
                    nTone = 49;
                    fTone = System.Math.Min(fTone, nTone + delta * expansionDir);
                }
            }
            else if (50 <= fTone && fTone < 60)
            {
                if (stayTogether)
                {
                    if (expansionDir > 0)
                    {
                        nTone = 60;
                        fTone = System.Math.Max(fTone, nTone + delta * expansionDir);
                    }
                    else
                    {
                        nTone = 49;
                        fTone = System.Math.Min(fTone, nTone + delta * expansionDir);
                    }
                }
                else
                {
                    if (expansionDir > 0)
                    {
                        fTone = 60;
                    }
                    else
                    {
                        fTone = 49;
                    }
                }
            }
            return amNearer ? nTone : fTone;
        }
        else
        {
            var answer = color.tone(scheme);
            if (color.background == null || color.background(scheme) == null || color.contrastCurve == null || color.contrastCurve(scheme) == null)
            {
                return answer;
            }
            var bgTone = color.background(scheme).GetTone(scheme);
            var desiredRatio = color.contrastCurve(scheme).Get(scheme.ContrastLevel);
            if (Contrast.RatioOfTones(bgTone, answer) < desiredRatio)
            {
                answer = DynamicColor.ForegroundTone(bgTone, desiredRatio);
            }
            if (decreasingContrast)
            {
                answer = DynamicColor.ForegroundTone(bgTone, desiredRatio);
            }
            if (color.isBackground && 50 <= answer && answer < 60)
            {
                if (Contrast.RatioOfTones(49, bgTone) >= desiredRatio)
                {
                    answer = 49;
                }
                else
                {
                    answer = 60;
                }
            }
            if (color.secondBackground == null || color.secondBackground(scheme) == null)
            {
                return answer;
            }
            var bgTone1 = color.background(scheme).GetTone(scheme);
            var bgTone2 = color.secondBackground(scheme).GetTone(scheme);
            var upper = System.Math.Max(bgTone1, bgTone2);
            var lower = System.Math.Min(bgTone1, bgTone2);
            if (Contrast.RatioOfTones(upper, answer) >= desiredRatio && Contrast.RatioOfTones(lower, answer) >= desiredRatio)
            {
                return answer;
            }
            var lightOption = Contrast.Lighter(upper, desiredRatio);
            var darkOption = Contrast.Darker(lower, desiredRatio);
            var prefersLight = DynamicColor.TonePrefersLightForeground(bgTone1) || DynamicColor.TonePrefersLightForeground(bgTone2);
            if (prefersLight)
            {
                return lightOption == -1 ? 100 : lightOption;
            }
            if (lightOption != -1 && darkOption == -1)
            {
                return lightOption;
            }
            return darkOption == -1 ? 0 : darkOption;
        }
    }

    // Scheme Palettes
    public TonalPalette GetPrimaryPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel)
    {
        return variant switch
        {
            Variant.CONTENT or Variant.FIDELITY => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, sourceColorHct.Chroma),
            Variant.FRUIT_SALAD => TonalPalette.FromHueAndChroma(MathUtils.SanitizeDegreesDouble(sourceColorHct.Hue - 50.0), 48.0),
            Variant.MONOCHROME => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 0.0),
            Variant.NEUTRAL => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 12.0),
            Variant.RAINBOW => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 48.0),
            Variant.TONAL_SPOT => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 36.0),
            Variant.EXPRESSIVE => TonalPalette.FromHueAndChroma(MathUtils.SanitizeDegreesDouble(sourceColorHct.Hue + 240.0), 40.0),
            Variant.VIBRANT => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 200.0),
            _ => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, sourceColorHct.Chroma),
        };
    }

    public TonalPalette GetSecondaryPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel)
    {
        return variant switch
        {
            Variant.CONTENT or Variant.FIDELITY => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, System.Math.Max(sourceColorHct.Chroma - 32.0, sourceColorHct.Chroma * 0.5)),
            Variant.FRUIT_SALAD => TonalPalette.FromHueAndChroma(MathUtils.SanitizeDegreesDouble(sourceColorHct.Hue - 50.0), 36.0),
            Variant.MONOCHROME => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 0.0),
            Variant.NEUTRAL => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 8.0),
            Variant.RAINBOW => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 16.0),
            Variant.TONAL_SPOT => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 16.0),
            Variant.EXPRESSIVE => TonalPalette.FromHueAndChroma(
                DynamicScheme.GetRotatedHue(
                    sourceColorHct,
                    [0.0, 21.0, 51.0, 121.0, 151.0, 191.0, 271.0, 321.0, 360.0],
                    [45.0, 95.0, 45.0, 20.0, 45.0, 90.0, 45.0, 45.0, 45.0]
                ), 24.0),
            Variant.VIBRANT => TonalPalette.FromHueAndChroma(
                DynamicScheme.GetRotatedHue(
                    sourceColorHct,
                    [0.0, 41.0, 61.0, 101.0, 131.0, 181.0, 251.0, 301.0, 360.0],
                    [18.0, 15.0, 10.0, 12.0, 15.0, 18.0, 15.0, 12.0, 12.0]
                ), 24.0),
            _ => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, sourceColorHct.Chroma),
        };
    }

    public TonalPalette GetTertiaryPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel)
    {
        return variant switch
        {
            Variant.CONTENT => TonalPalette.FromHct(DislikeAnalyzer.FixIfDisliked(new TemperatureCache(sourceColorHct).GetAnalogousColors(3, 6)[2])),
            Variant.FIDELITY => TonalPalette.FromHct(DislikeAnalyzer.FixIfDisliked(new TemperatureCache(sourceColorHct).GetComplement())),
            Variant.FRUIT_SALAD => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 36.0),
            Variant.MONOCHROME => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 0.0),
            Variant.NEUTRAL => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 16.0),
            Variant.RAINBOW or Variant.TONAL_SPOT => TonalPalette.FromHueAndChroma(MathUtils.SanitizeDegreesDouble(sourceColorHct.Hue + 60.0), 24.0),
            Variant.EXPRESSIVE => TonalPalette.FromHueAndChroma(
                DynamicScheme.GetRotatedHue(
                    sourceColorHct,
                    [0.0, 21.0, 51.0, 121.0, 151.0, 191.0, 271.0, 321.0, 360.0],
                    [120.0, 120.0, 20.0, 45.0, 20.0, 15.0, 20.0, 120.0, 120.0]
                ), 32.0),
            Variant.VIBRANT => TonalPalette.FromHueAndChroma(
                DynamicScheme.GetRotatedHue(
                    sourceColorHct,
                    [0.0, 41.0, 61.0, 101.0, 131.0, 181.0, 251.0, 301.0, 360.0],
                    [35.0, 30.0, 20.0, 25.0, 30.0, 35.0, 30.0, 25.0, 25.0]
                ), 32.0),
            _ => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, sourceColorHct.Chroma),
        };
    }

    public TonalPalette GetNeutralPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel)
    {
        return variant switch
        {
            Variant.CONTENT or Variant.FIDELITY => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, sourceColorHct.Chroma / 8.0),
            Variant.FRUIT_SALAD => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 10.0),
            Variant.MONOCHROME => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 0.0),
            Variant.NEUTRAL => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 2.0),
            Variant.RAINBOW => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 0.0),
            Variant.TONAL_SPOT => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 6.0),
            Variant.EXPRESSIVE => TonalPalette.FromHueAndChroma(MathUtils.SanitizeDegreesDouble(sourceColorHct.Hue + 15.0), 8.0),
            Variant.VIBRANT => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 10.0),
            _ => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, sourceColorHct.Chroma),
        };
    }

    public TonalPalette GetNeutralVariantPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel)
    {
        return variant switch
        {
            Variant.CONTENT or Variant.FIDELITY => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, (sourceColorHct.Chroma / 8.0) + 4.0),
            Variant.FRUIT_SALAD => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 16.0),
            Variant.MONOCHROME => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 0.0),
            Variant.NEUTRAL => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 2.0),
            Variant.RAINBOW => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 0.0),
            Variant.TONAL_SPOT => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 8.0),
            Variant.EXPRESSIVE => TonalPalette.FromHueAndChroma(MathUtils.SanitizeDegreesDouble(sourceColorHct.Hue + 15.0), 12.0),
            Variant.VIBRANT => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 12.0),
            _ => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, sourceColorHct.Chroma),
        };
    }

    public TonalPalette? GetErrorPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel)
    {
        return null;
    }
}