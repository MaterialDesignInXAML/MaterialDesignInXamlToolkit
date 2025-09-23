namespace MaterialColorUtilities;

public class ColorSpec2025 : ColorSpec2021
{
    // Surfaces [S]
    public new DynamicColor Background
    {
        get
        {
            // Remap to surface for 2025
            var color2025 = Surface.ToBuilder().SetName("background").Build();
            return base.Background.ToBuilder()
                .ExtendSpecVersion(SpecVersion.Spec2025, color2025)
                .Build();
        }
    }

    public new DynamicColor OnBackground
    {
        get
        {
            // Remap to onSurface for 2025; watch tweaks tone to 100
            var color2025Builder = OnSurface.ToBuilder().SetName("on_background");
            color2025Builder.SetTone(s => s.PlatformType == Platform.Watch ? 100.0 : OnSurface.GetTone(s));
            return base.OnBackground.ToBuilder()
                .ExtendSpecVersion(SpecVersion.Spec2025, color2025Builder.Build())
                .Build();
        }
    }

    public new DynamicColor Surface
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("surface")
                .SetPalette(s => s.NeutralPalette)
                .SetTone(s =>
                {
                    if (s.PlatformType == Platform.Phone)
                    {
                        if (s.IsDark) return 4.0;
                        if (Hct.IsYellow(s.NeutralPalette.GetHue())) return 99.0;
                        if (s.Variant == Variant.Vibrant) return 97.0;
                        return 98.0;
                    }
                    else
                    {
                        return 0.0;
                    }
                })
                .SetIsBackground(true)
                .Build();
            return base.Surface.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor SurfaceDim
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("surface_dim")
                .SetPalette(s => s.NeutralPalette)
                .SetTone(s =>
                {
                    if (s.IsDark) return 4.0;
                    if (Hct.IsYellow(s.NeutralPalette.GetHue())) return 90.0;
                    if (s.Variant == Variant.Vibrant) return 85.0;
                    return 87.0;
                })
                .SetIsBackground(true)
                .SetChromaMultiplier(s =>
                {
                    if (!s.IsDark)
                    {
                        if (s.Variant == Variant.Neutral) return 2.5;
                        if (s.Variant == Variant.TonalSpot) return 1.7;
                        if (s.Variant == Variant.Expressive) return Hct.IsYellow(s.NeutralPalette.GetHue()) ? 2.7 : 1.75;
                        if (s.Variant == Variant.Vibrant) return 1.36;
                    }
                    return 1.0;
                })
                .Build();
            return base.SurfaceDim.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor SurfaceBright
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("surface_bright")
                .SetPalette(s => s.NeutralPalette)
                .SetTone(s =>
                {
                    if (s.IsDark) return 18.0;
                    if (Hct.IsYellow(s.NeutralPalette.GetHue())) return 99.0;
                    if (s.Variant == Variant.Vibrant) return 97.0;
                    return 98.0;
                })
                .SetIsBackground(true)
                .SetChromaMultiplier(s =>
                {
                    if (s.IsDark)
                    {
                        if (s.Variant == Variant.Neutral) return 2.5;
                        if (s.Variant == Variant.TonalSpot) return 1.7;
                        if (s.Variant == Variant.Expressive) return Hct.IsYellow(s.NeutralPalette.GetHue()) ? 2.7 : 1.75;
                        if (s.Variant == Variant.Vibrant) return 1.36;
                    }
                    return 1.0;
                })
                .Build();
            return base.SurfaceBright.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor SurfaceContainerLowest
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("surface_container_lowest")
                .SetPalette(s => s.NeutralPalette)
                .SetTone(s => s.IsDark ? 0.0 : 100.0)
                .SetIsBackground(true)
                .Build();
            return base.SurfaceContainerLowest.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor SurfaceContainerLow
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("surface_container_low")
                .SetPalette(s => s.NeutralPalette)
                .SetTone(s =>
                {
                    if (s.PlatformType == Platform.Phone)
                    {
                        if (s.IsDark) return 6.0;
                        if (Hct.IsYellow(s.NeutralPalette.GetHue())) return 98.0;
                        if (s.Variant == Variant.Vibrant) return 95.0;
                        return 96.0;
                    }
                    return 15.0;
                })
                .SetIsBackground(true)
                .SetChromaMultiplier(s =>
                {
                    if (s.PlatformType == Platform.Phone)
                    {
                        if (s.Variant == Variant.Neutral) return 1.3;
                        if (s.Variant == Variant.TonalSpot) return 1.25;
                        if (s.Variant == Variant.Expressive) return Hct.IsYellow(s.NeutralPalette.GetHue()) ? 1.3 : 1.15;
                        if (s.Variant == Variant.Vibrant) return 1.08;
                    }
                    return 1.0;
                })
                .Build();
            return base.SurfaceContainerLow.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor SurfaceContainer
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("surface_container")
                .SetPalette(s => s.NeutralPalette)
                .SetTone(s =>
                {
                    if (s.PlatformType == Platform.Phone)
                    {
                        if (s.IsDark) return 9.0;
                        if (Hct.IsYellow(s.NeutralPalette.GetHue())) return 96.0;
                        if (s.Variant == Variant.Vibrant) return 92.0;
                        return 94.0;
                    }
                    return 20.0;
                })
                .SetIsBackground(true)
                .SetChromaMultiplier(s =>
                {
                    if (s.PlatformType == Platform.Phone)
                    {
                        if (s.Variant == Variant.Neutral) return 1.6;
                        if (s.Variant == Variant.TonalSpot) return 1.4;
                        if (s.Variant == Variant.Expressive) return Hct.IsYellow(s.NeutralPalette.GetHue()) ? 1.6 : 1.3;
                        if (s.Variant == Variant.Vibrant) return 1.15;
                    }
                    return 1.0;
                })
                .Build();
            return base.SurfaceContainer.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor SurfaceContainerHigh
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("surface_container_high")
                .SetPalette(s => s.NeutralPalette)
                .SetTone(s =>
                {
                    if (s.PlatformType == Platform.Phone)
                    {
                        if (s.IsDark) return 12.0;
                        if (Hct.IsYellow(s.NeutralPalette.GetHue())) return 94.0;
                        if (s.Variant == Variant.Vibrant) return 90.0;
                        return 92.0;
                    }
                    return 25.0;
                })
                .SetIsBackground(true)
                .SetChromaMultiplier(s =>
                {
                    if (s.PlatformType == Platform.Phone)
                    {
                        if (s.Variant == Variant.Neutral) return 1.9;
                        if (s.Variant == Variant.TonalSpot) return 1.5;
                        if (s.Variant == Variant.Expressive) return Hct.IsYellow(s.NeutralPalette.GetHue()) ? 1.95 : 1.45;
                        if (s.Variant == Variant.Vibrant) return 1.22;
                    }
                    return 1.0;
                })
                .Build();
            return base.SurfaceContainerHigh.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor SurfaceContainerHighest
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("surface_container_highest")
                .SetPalette(s => s.NeutralPalette)
                .SetTone(s =>
                {
                    if (s.IsDark) return 15.0;
                    if (Hct.IsYellow(s.NeutralPalette.GetHue())) return 92.0;
                    if (s.Variant == Variant.Vibrant) return 88.0;
                    return 90.0;
                })
                .SetIsBackground(true)
                .SetChromaMultiplier(s =>
                {
                    if (s.Variant == Variant.Neutral) return 2.2;
                    if (s.Variant == Variant.TonalSpot) return 1.7;
                    if (s.Variant == Variant.Expressive) return Hct.IsYellow(s.NeutralPalette.GetHue()) ? 2.3 : 1.6;
                    if (s.Variant == Variant.Vibrant) return 1.29;
                    return 1.0;
                })
                .Build();
            return base.SurfaceContainerHighest.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor OnSurface
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("on_surface")
                .SetPalette(s => s.NeutralPalette)
                .SetTone(s =>
                {
                    if (s.Variant == Variant.Vibrant)
                    {
                        return TMaxC(s.NeutralPalette, 0, 100, 1.1);
                    }
                    else
                    {
                        return DynamicColor.GetInitialToneFromBackground(
                            scheme =>
                            {
                                if (scheme.PlatformType == Platform.Phone)
                                {
                                    return scheme.IsDark ? SurfaceBright : SurfaceDim;
                                }
                                else
                                {
                                    return SurfaceContainerHigh;
                                }
                            }
                        )(s);
                    }
                })
                .SetChromaMultiplier(s =>
                {
                    if (s.PlatformType == Platform.Phone)
                    {
                        if (s.Variant == Variant.Neutral) return 2.2;
                        if (s.Variant == Variant.TonalSpot) return 1.7;
                        if (s.Variant == Variant.Expressive) return Hct.IsYellow(s.NeutralPalette.GetHue()) ? (s.IsDark ? 3.0 : 2.3) : 1.6;
                    }
                    return 1.0;
                })
                .SetBackground(s =>
                {
                    if (s.PlatformType == Platform.Phone)
                    {
                        return s.IsDark ? SurfaceBright : SurfaceDim;
                    }
                    else
                    {
                        return SurfaceContainerHigh;
                    }
                })
                .SetContrastCurve(s => s.IsDark ? GetContrastCurve(11) : GetContrastCurve(9))
                .Build();
            return base.OnSurface.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor SurfaceVariant
    {
        get
        {
            var color2025 = SurfaceContainerHighest.ToBuilder().SetName("surface_variant").Build();
            return base.SurfaceVariant.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor OnSurfaceVariant
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("on_surface_variant")
                .SetPalette(s => s.NeutralPalette)
                .SetChromaMultiplier(s =>
                {
                    if (s.PlatformType == Platform.Phone)
                    {
                        if (s.Variant == Variant.Neutral) return 2.2;
                        if (s.Variant == Variant.TonalSpot) return 1.7;
                        if (s.Variant == Variant.Expressive) return Hct.IsYellow(s.NeutralPalette.GetHue()) ? (s.IsDark ? 3.0 : 2.3) : 1.6;
                    }
                    return 1.0;
                })
                .SetBackground(s =>
                {
                    if (s.PlatformType == Platform.Phone)
                    {
                        return s.IsDark ? SurfaceBright : SurfaceDim;
                    }
                    else
                    {
                        return SurfaceContainerHigh;
                    }
                })
                .SetContrastCurve(s =>
                    s.PlatformType == Platform.Phone
                        ? (s.IsDark ? GetContrastCurve(6) : GetContrastCurve(4.5))
                        : GetContrastCurve(7))
                .Build();
            return base.OnSurfaceVariant.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor InverseSurface
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("inverse_surface")
                .SetPalette(s => s.NeutralPalette)
                .SetTone(s => s.IsDark ? 98.0 : 4.0)
                .SetIsBackground(true)
                .Build();
            return base.InverseSurface.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor InverseOnSurface
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("inverse_on_surface")
                .SetPalette(s => s.NeutralPalette)
                .SetBackground(s => InverseSurface)
                .SetContrastCurve(s => GetContrastCurve(7))
                .Build();
            return base.InverseOnSurface.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor Outline
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("outline")
                .SetPalette(s => s.NeutralPalette)
                .SetChromaMultiplier(s =>
                {
                    if (s.PlatformType == Platform.Phone)
                    {
                        if (s.Variant == Variant.Neutral) return 2.2;
                        if (s.Variant == Variant.TonalSpot) return 1.7;
                        if (s.Variant == Variant.Expressive) return Hct.IsYellow(s.NeutralPalette.GetHue()) ? (s.IsDark ? 3.0 : 2.3) : 1.6;
                    }
                    return 1.0;
                })
                .SetBackground(s =>
                {
                    if (s.PlatformType == Platform.Phone)
                    {
                        return s.IsDark ? SurfaceBright : SurfaceDim;
                    }
                    else
                    {
                        return SurfaceContainerHigh;
                    }
                })
                .SetContrastCurve(s => s.PlatformType == Platform.Phone ? GetContrastCurve(3) : GetContrastCurve(4.5))
                .Build();
            return base.Outline.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor OutlineVariant
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("outline_variant")
                .SetPalette(s => s.NeutralPalette)
                .SetChromaMultiplier(s =>
                {
                    if (s.PlatformType == Platform.Phone)
                    {
                        if (s.Variant == Variant.Neutral) return 2.2;
                        if (s.Variant == Variant.TonalSpot) return 1.7;
                        if (s.Variant == Variant.Expressive) return Hct.IsYellow(s.NeutralPalette.GetHue()) ? (s.IsDark ? 3.0 : 2.3) : 1.6;
                    }
                    return 1.0;
                })
                .SetBackground(s =>
                {
                    if (s.PlatformType == Platform.Phone)
                    {
                        return s.IsDark ? SurfaceBright : SurfaceDim;
                    }
                    else
                    {
                        return SurfaceContainerHigh;
                    }
                })
                .SetContrastCurve(s => s.PlatformType == Platform.Phone ? GetContrastCurve(1.5) : GetContrastCurve(3))
                .Build();
            return base.OutlineVariant.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor SurfaceTint
    {
        get
        {
            // Remapped to primary
            var color2025 = Primary.ToBuilder().SetName("surface_tint").Build();
            return base.SurfaceTint.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    // Primaries [P]
    public new DynamicColor Primary
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("primary")
                .SetPalette(s => s.PrimaryPalette)
                .SetTone(s =>
                {
                    if (s.Variant == Variant.Neutral)
                    {
                        if (s.PlatformType == Platform.Phone) return s.IsDark ? 80.0 : 40.0;
                        return 90.0;
                    }
                    else if (s.Variant == Variant.TonalSpot)
                    {
                        if (s.PlatformType == Platform.Phone)
                        {
                            if (s.IsDark) return 80.0;
                            return TMaxC(s.PrimaryPalette);
                        }
                        else
                        {
                            return TMaxC(s.PrimaryPalette, 0, 90);
                        }
                    }
                    else if (s.Variant == Variant.Expressive)
                    {
                        return TMaxC(
                            s.PrimaryPalette,
                            0,
                            Hct.IsYellow(s.PrimaryPalette.GetHue()) ? 25 : (Hct.IsCyan(s.PrimaryPalette.GetHue()) ? 88 : 98));
                    }
                    else // VIBRANT
                    {
                        return TMaxC(s.PrimaryPalette, 0, Hct.IsCyan(s.PrimaryPalette.GetHue()) ? 88 : 98);
                    }
                })
                .SetIsBackground(true)
                .SetBackground(s =>
                {
                    if (s.PlatformType == Platform.Phone) return s.IsDark ? SurfaceBright : SurfaceDim;
                    return SurfaceContainerHigh;
                })
                .SetContrastCurve(s => s.PlatformType == Platform.Phone ? GetContrastCurve(4.5) : GetContrastCurve(7))
                .SetToneDeltaPair(s => s.PlatformType == Platform.Phone
                    ? new ToneDeltaPair(PrimaryContainer, Primary, 5.0, TonePolarity.RelativeLighter, DeltaConstraint.Farther)
                    : null!)
                .Build();
            return base.Primary.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor? PrimaryDim
    {
        get
        {
            return new DynamicColor.Builder()
                .SetName("primary_dim")
                .SetPalette(s => s.PrimaryPalette)
                .SetTone(s =>
                {
                    if (s.Variant == Variant.Neutral) return 85.0;
                    if (s.Variant == Variant.TonalSpot) return TMaxC(s.PrimaryPalette, 0, 90);
                    return TMaxC(s.PrimaryPalette);
                })
                .SetIsBackground(true)
                .SetBackground(s => SurfaceContainerHigh)
                .SetContrastCurve(s => GetContrastCurve(4.5))
                .SetToneDeltaPair(s => new ToneDeltaPair(PrimaryDim!, Primary, 5.0, TonePolarity.Darker, DeltaConstraint.Farther))
                .Build();
        }
    }

    public new DynamicColor OnPrimary
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("on_primary")
                .SetPalette(s => s.PrimaryPalette)
                .SetBackground(s => s.PlatformType == Platform.Phone ? Primary : PrimaryDim!)
                .SetContrastCurve(s => s.PlatformType == Platform.Phone ? GetContrastCurve(6) : GetContrastCurve(7))
                .Build();
            return base.OnPrimary.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor PrimaryContainer
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("primary_container")
                .SetPalette(s => s.PrimaryPalette)
                .SetTone(s =>
                {
                    if (s.PlatformType == Platform.Watch) return 30.0;
                    if (s.Variant == Variant.Neutral) return s.IsDark ? 30.0 : 90.0;
                    if (s.Variant == Variant.TonalSpot) return s.IsDark ? TMinC(s.PrimaryPalette, 35, 93) : TMaxC(s.PrimaryPalette, 0, 90);
                    if (s.Variant == Variant.Expressive) return s.IsDark ? TMaxC(s.PrimaryPalette, 30, 93) : TMaxC(s.PrimaryPalette, 78, Hct.IsCyan(s.PrimaryPalette.GetHue()) ? 88 : 90);
                    // VIBRANT
                    return s.IsDark ? TMinC(s.PrimaryPalette, 66, 93) : TMaxC(s.PrimaryPalette, 66, Hct.IsCyan(s.PrimaryPalette.GetHue()) ? 88 : 93);
                })
                .SetIsBackground(true)
                .SetBackground(s =>
                {
                    if (s.PlatformType == Platform.Phone) return s.IsDark ? SurfaceBright : SurfaceDim;
                    return null!;
                })
                .SetToneDeltaPair(s => s.PlatformType == Platform.Watch ? new ToneDeltaPair(PrimaryContainer, PrimaryDim!, 10.0, TonePolarity.Darker, DeltaConstraint.Farther) : null!)
                .SetContrastCurve(s => s.PlatformType == Platform.Phone && s.ContrastLevel > 0 ? GetContrastCurve(1.5) : null!)
                .Build();
            return base.PrimaryContainer.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor OnPrimaryContainer
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("on_primary_container")
                .SetPalette(s => s.PrimaryPalette)
                .SetBackground(s => PrimaryContainer)
                .SetContrastCurve(s => s.PlatformType == Platform.Phone ? GetContrastCurve(6) : GetContrastCurve(7))
                .Build();
            return base.OnPrimaryContainer.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor InversePrimary
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("inverse_primary")
                .SetPalette(s => s.PrimaryPalette)
                .SetTone(s => TMaxC(s.PrimaryPalette))
                .SetBackground(s => InverseSurface)
                .SetContrastCurve(s => s.PlatformType == Platform.Phone ? GetContrastCurve(6) : GetContrastCurve(7))
                .Build();
            return base.InversePrimary.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    // Secondaries [Q]
    public new DynamicColor Secondary
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("secondary")
                .SetPalette(s => s.SecondaryPalette)
                .SetTone(s =>
                {
                    if (s.PlatformType == Platform.Watch) return s.Variant == Variant.Neutral ? 90.0 : TMaxC(s.SecondaryPalette, 0, 90);
                    if (s.Variant == Variant.Neutral) return s.IsDark ? TMinC(s.SecondaryPalette, 0, 98) : TMaxC(s.SecondaryPalette);
                    if (s.Variant == Variant.Vibrant) return TMaxC(s.SecondaryPalette, 0, s.IsDark ? 90 : 98);
                    // EXPRESSIVE, TONAL_SPOT
                    return s.IsDark ? 80.0 : TMaxC(s.SecondaryPalette);
                })
                .SetIsBackground(true)
                .SetBackground(s =>
                {
                    if (s.PlatformType == Platform.Phone) return s.IsDark ? SurfaceBright : SurfaceDim;
                    return SurfaceContainerHigh;
                })
                .SetContrastCurve(s => s.PlatformType == Platform.Phone ? GetContrastCurve(4.5) : GetContrastCurve(7))
                .SetToneDeltaPair(s => s.PlatformType == Platform.Phone
                    ? new ToneDeltaPair(SecondaryContainer, Secondary, 5.0, TonePolarity.RelativeLighter, DeltaConstraint.Farther)
                    : null!)
                .Build();
            return base.Secondary.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor? SecondaryDim
    {
        get
        {
            return new DynamicColor.Builder()
                .SetName("secondary_dim")
                .SetPalette(s => s.SecondaryPalette)
                .SetTone(s =>
                {
                    if (s.Variant == Variant.Neutral) return 85.0;
                    return TMaxC(s.SecondaryPalette, 0, 90);
                })
                .SetIsBackground(true)
                .SetBackground(s => SurfaceContainerHigh)
                .SetContrastCurve(s => GetContrastCurve(4.5))
                .SetToneDeltaPair(s => new ToneDeltaPair(SecondaryDim!, Secondary, 5.0, TonePolarity.Darker, DeltaConstraint.Farther))
                .Build();
        }
    }

    public new DynamicColor OnSecondary
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("on_secondary")
                .SetPalette(s => s.SecondaryPalette)
                .SetBackground(s => s.PlatformType == Platform.Phone ? Secondary : SecondaryDim!)
                .SetContrastCurve(s => s.PlatformType == Platform.Phone ? GetContrastCurve(6) : GetContrastCurve(7))
                .Build();
            return base.OnSecondary.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor SecondaryContainer
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("secondary_container")
                .SetPalette(s => s.SecondaryPalette)
                .SetTone(s =>
                {
                    if (s.PlatformType == Platform.Watch) return 30.0;
                    if (s.Variant == Variant.Vibrant) return s.IsDark ? TMinC(s.SecondaryPalette, 30, 40) : TMaxC(s.SecondaryPalette, 84, 90);
                    if (s.Variant == Variant.Expressive) return s.IsDark ? 15.0 : TMaxC(s.SecondaryPalette, 90, 95);
                    return s.IsDark ? 25.0 : 90.0;
                })
                .SetIsBackground(true)
                .SetBackground(s =>
                {
                    if (s.PlatformType == Platform.Phone) return s.IsDark ? SurfaceBright : SurfaceDim;
                    return null!;
                })
                .SetToneDeltaPair(s => s.PlatformType == Platform.Watch ? new ToneDeltaPair(SecondaryContainer, SecondaryDim!, 10.0, TonePolarity.Darker, DeltaConstraint.Farther) : null!)
                .SetContrastCurve(s => s.PlatformType == Platform.Phone && s.ContrastLevel > 0 ? GetContrastCurve(1.5) : null!)
                .Build();
            return base.SecondaryContainer.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor OnSecondaryContainer
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("on_secondary_container")
                .SetPalette(s => s.SecondaryPalette)
                .SetBackground(s => SecondaryContainer)
                .SetContrastCurve(s => s.PlatformType == Platform.Phone ? GetContrastCurve(6) : GetContrastCurve(7))
                .Build();
            return base.OnSecondaryContainer.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    // Tertiaries [T]
    public new DynamicColor Tertiary
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("tertiary")
                .SetPalette(s => s.TertiaryPalette)
                .SetTone(s =>
                {
                    if (s.PlatformType == Platform.Watch)
                    {
                        return s.Variant == Variant.TonalSpot ? TMaxC(s.TertiaryPalette, 0, 90) : TMaxC(s.TertiaryPalette);
                    }
                    else if (s.Variant == Variant.Expressive || s.Variant == Variant.Vibrant)
                    {
                        return TMaxC(s.TertiaryPalette, 0, Hct.IsCyan(s.TertiaryPalette.GetHue()) ? 88 : (s.IsDark ? 98 : 100));
                    }
                    else // NEUTRAL, TONAL_SPOT
                    {
                        return s.IsDark ? TMaxC(s.TertiaryPalette, 0, 98) : TMaxC(s.TertiaryPalette);
                    }
                })
                .SetIsBackground(true)
                .SetBackground(s =>
                {
                    if (s.PlatformType == Platform.Phone) return s.IsDark ? SurfaceBright : SurfaceDim;
                    return SurfaceContainerHigh;
                })
                .SetContrastCurve(s => s.PlatformType == Platform.Phone ? GetContrastCurve(4.5) : GetContrastCurve(7))
                .SetToneDeltaPair(s => s.PlatformType == Platform.Phone ? new ToneDeltaPair(TertiaryContainer, Tertiary, 5.0, TonePolarity.RelativeLighter, DeltaConstraint.Farther) : null!)
                .Build();
            return base.Tertiary.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor? TertiaryDim
    {
        get
        {
            return new DynamicColor.Builder()
                .SetName("tertiary_dim")
                .SetPalette(s => s.TertiaryPalette)
                .SetTone(s => s.Variant == Variant.TonalSpot ? TMaxC(s.TertiaryPalette, 0, 90) : TMaxC(s.TertiaryPalette))
                .SetIsBackground(true)
                .SetBackground(s => SurfaceContainerHigh)
                .SetContrastCurve(s => GetContrastCurve(4.5))
                .SetToneDeltaPair(s => new ToneDeltaPair(TertiaryDim!, Tertiary, 5.0, TonePolarity.Darker, DeltaConstraint.Farther))
                .Build();
        }
    }

    public new DynamicColor OnTertiary
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("on_tertiary")
                .SetPalette(s => s.TertiaryPalette)
                .SetBackground(s => s.PlatformType == Platform.Phone ? Tertiary : TertiaryDim!)
                .SetContrastCurve(s => s.PlatformType == Platform.Phone ? GetContrastCurve(6) : GetContrastCurve(7))
                .Build();
            return base.OnTertiary.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor TertiaryContainer
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("tertiary_container")
                .SetPalette(s => s.TertiaryPalette)
                .SetTone(s =>
                {
                    if (s.PlatformType == Platform.Watch)
                    {
                        return s.Variant == Variant.TonalSpot ? TMaxC(s.TertiaryPalette, 0, 90) : TMaxC(s.TertiaryPalette);
                    }
                    else
                    {
                        if (s.Variant == Variant.Neutral)
                        {
                            return s.IsDark ? TMaxC(s.TertiaryPalette, 0, 93) : TMaxC(s.TertiaryPalette, 0, 96);
                        }
                        else if (s.Variant == Variant.TonalSpot)
                        {
                            return TMaxC(s.TertiaryPalette, 0, s.IsDark ? 93 : 100);
                        }
                        else if (s.Variant == Variant.Expressive)
                        {
                            return TMaxC(s.TertiaryPalette, 75, Hct.IsCyan(s.TertiaryPalette.GetHue()) ? 88 : (s.IsDark ? 93 : 100));
                        }
                        else // VIBRANT
                        {
                            return s.IsDark ? TMaxC(s.TertiaryPalette, 0, 93) : TMaxC(s.TertiaryPalette, 72, 100);
                        }
                    }
                })
                .SetIsBackground(true)
                .SetBackground(s =>
                {
                    if (s.PlatformType == Platform.Phone) return s.IsDark ? SurfaceBright : SurfaceDim;
                    return null!;
                })
                .SetToneDeltaPair(s => s.PlatformType == Platform.Watch ? new ToneDeltaPair(TertiaryContainer, TertiaryDim!, 10.0, TonePolarity.Darker, DeltaConstraint.Farther) : null!)
                .SetContrastCurve(s => s.PlatformType == Platform.Phone && s.ContrastLevel > 0 ? GetContrastCurve(1.5) : null!)
                .Build();
            return base.TertiaryContainer.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor OnTertiaryContainer
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("on_tertiary_container")
                .SetPalette(s => s.TertiaryPalette)
                .SetBackground(s => TertiaryContainer)
                .SetContrastCurve(s => s.PlatformType == Platform.Phone ? GetContrastCurve(6) : GetContrastCurve(7))
                .Build();
            return base.OnTertiaryContainer.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    // Errors [E]
    public new DynamicColor Error
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("error")
                .SetPalette(s => s.ErrorPalette)
                .SetTone(s =>
                {
                    if (s.PlatformType == Platform.Phone) return s.IsDark ? TMinC(s.ErrorPalette, 0, 98) : TMaxC(s.ErrorPalette);
                    return TMinC(s.ErrorPalette);
                })
                .SetIsBackground(true)
                .SetBackground(s => s.PlatformType == Platform.Phone ? (s.IsDark ? SurfaceBright : SurfaceDim) : SurfaceContainerHigh)
                .SetContrastCurve(s => s.PlatformType == Platform.Phone ? GetContrastCurve(4.5) : GetContrastCurve(7))
                .SetToneDeltaPair(s => s.PlatformType == Platform.Phone ? new ToneDeltaPair(ErrorContainer, Error, 5.0, TonePolarity.RelativeLighter, DeltaConstraint.Farther) : null!)
                .Build();
            return base.Error.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor? ErrorDim
    {
        get
        {
            return new DynamicColor.Builder()
                .SetName("error_dim")
                .SetPalette(s => s.ErrorPalette)
                .SetTone(s => TMinC(s.ErrorPalette))
                .SetIsBackground(true)
                .SetBackground(s => SurfaceContainerHigh)
                .SetContrastCurve(s => GetContrastCurve(4.5))
                .SetToneDeltaPair(s => new ToneDeltaPair(ErrorDim!, Error, 5.0, TonePolarity.Darker, DeltaConstraint.Farther))
                .Build();
        }
    }

    public new DynamicColor OnError
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("on_error")
                .SetPalette(s => s.ErrorPalette)
                .SetBackground(s => s.PlatformType == Platform.Phone ? Error : ErrorDim!)
                .SetContrastCurve(s => s.PlatformType == Platform.Phone ? GetContrastCurve(6) : GetContrastCurve(7))
                .Build();
            return base.OnError.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor ErrorContainer
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("error_container")
                .SetPalette(s => s.ErrorPalette)
                .SetTone(s =>
                {
                    if (s.PlatformType == Platform.Watch) return 30.0;
                    return s.IsDark ? TMinC(s.ErrorPalette, 30, 93) : TMaxC(s.ErrorPalette, 0, 90);
                })
                .SetIsBackground(true)
                .SetBackground(s => s.PlatformType == Platform.Phone ? (s.IsDark ? SurfaceBright : SurfaceDim) : null!)
                .SetToneDeltaPair(s => s.PlatformType == Platform.Watch ? new ToneDeltaPair(ErrorContainer, ErrorDim!, 10.0, TonePolarity.Darker, DeltaConstraint.Farther) : null!)
                .SetContrastCurve(s => s.PlatformType == Platform.Phone && s.ContrastLevel > 0 ? GetContrastCurve(1.5) : null!)
                .Build();
            return base.ErrorContainer.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor OnErrorContainer
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("on_error_container")
                .SetPalette(s => s.ErrorPalette)
                .SetBackground(s => ErrorContainer)
                .SetContrastCurve(s => s.PlatformType == Platform.Phone ? GetContrastCurve(4.5) : GetContrastCurve(7))
                .Build();
            return base.OnErrorContainer.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    // Primary Fixed Colors [PF]
    public new DynamicColor PrimaryFixed
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("primary_fixed")
                .SetPalette(s => s.PrimaryPalette)
                .SetTone(s =>
                {
                    var tempS = DynamicScheme.From(s, false, 0.0);
                    return PrimaryContainer.GetTone(tempS);
                })
                .SetIsBackground(true)
                .SetBackground(s => s.PlatformType == Platform.Phone ? (s.IsDark ? SurfaceBright : SurfaceDim) : null!)
                .SetContrastCurve(s => s.PlatformType == Platform.Phone && s.ContrastLevel > 0 ? GetContrastCurve(1.5) : null!)
                .Build();
            return base.PrimaryFixed.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor PrimaryFixedDim
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("primary_fixed_dim")
                .SetPalette(s => s.PrimaryPalette)
                .SetTone(s => PrimaryFixed.GetTone(s))
                .SetIsBackground(true)
                .SetToneDeltaPair(s => new ToneDeltaPair(PrimaryFixedDim, PrimaryFixed, 5.0, TonePolarity.Darker, DeltaConstraint.Exact))
                .Build();
            return base.PrimaryFixedDim.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor OnPrimaryFixed
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("on_primary_fixed")
                .SetPalette(s => s.PrimaryPalette)
                .SetBackground(s => PrimaryFixedDim)
                .SetContrastCurve(s => GetContrastCurve(7))
                .Build();
            return base.OnPrimaryFixed.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor OnPrimaryFixedVariant
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("on_primary_fixed_variant")
                .SetPalette(s => s.PrimaryPalette)
                .SetBackground(s => PrimaryFixedDim)
                .SetContrastCurve(s => GetContrastCurve(4.5))
                .Build();
            return base.OnPrimaryFixedVariant.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    // Secondary Fixed Colors [QF]
    public new DynamicColor SecondaryFixed
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("secondary_fixed")
                .SetPalette(s => s.SecondaryPalette)
                .SetTone(s =>
                {
                    var tempS = DynamicScheme.From(s, false, 0.0);
                    return SecondaryContainer.GetTone(tempS);
                })
                .SetIsBackground(true)
                .SetBackground(s => s.PlatformType == Platform.Phone ? (s.IsDark ? SurfaceBright : SurfaceDim) : null!)
                .SetContrastCurve(s => s.PlatformType == Platform.Phone && s.ContrastLevel > 0 ? GetContrastCurve(1.5) : null!)
                .Build();
            return base.SecondaryFixed.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor SecondaryFixedDim
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("secondary_fixed_dim")
                .SetPalette(s => s.SecondaryPalette)
                .SetTone(s => SecondaryFixed.GetTone(s))
                .SetIsBackground(true)
                .SetToneDeltaPair(s => new ToneDeltaPair(SecondaryFixedDim, SecondaryFixed, 5.0, TonePolarity.Darker, DeltaConstraint.Exact))
                .Build();
            return base.SecondaryFixedDim.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor OnSecondaryFixed
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("on_secondary_fixed")
                .SetPalette(s => s.SecondaryPalette)
                .SetBackground(s => SecondaryFixedDim)
                .SetContrastCurve(s => GetContrastCurve(7))
                .Build();
            return base.OnSecondaryFixed.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor OnSecondaryFixedVariant
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("on_secondary_fixed_variant")
                .SetPalette(s => s.SecondaryPalette)
                .SetBackground(s => SecondaryFixedDim)
                .SetContrastCurve(s => GetContrastCurve(4.5))
                .Build();
            return base.OnSecondaryFixedVariant.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    // Tertiary Fixed Colors [TF]
    public new DynamicColor TertiaryFixed
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("tertiary_fixed")
                .SetPalette(s => s.TertiaryPalette)
                .SetTone(s =>
                {
                    var tempS = DynamicScheme.From(s, false, 0.0);
                    return TertiaryContainer.GetTone(tempS);
                })
                .SetIsBackground(true)
                .SetBackground(s => s.PlatformType == Platform.Phone ? (s.IsDark ? SurfaceBright : SurfaceDim) : null!)
                .SetContrastCurve(s => s.PlatformType == Platform.Phone && s.ContrastLevel > 0 ? GetContrastCurve(1.5) : null!)
                .Build();
            return base.TertiaryFixed.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor TertiaryFixedDim
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("tertiary_fixed_dim")
                .SetPalette(s => s.TertiaryPalette)
                .SetTone(s => TertiaryFixed.GetTone(s))
                .SetIsBackground(true)
                .SetToneDeltaPair(s => new ToneDeltaPair(TertiaryFixedDim, TertiaryFixed, 5.0, TonePolarity.Darker, DeltaConstraint.Exact))
                .Build();
            return base.TertiaryFixedDim.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor OnTertiaryFixed
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("on_tertiary_fixed")
                .SetPalette(s => s.TertiaryPalette)
                .SetBackground(s => TertiaryFixedDim)
                .SetContrastCurve(s => GetContrastCurve(7))
                .Build();
            return base.OnTertiaryFixed.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor OnTertiaryFixedVariant
    {
        get
        {
            var color2025 = new DynamicColor.Builder()
                .SetName("on_tertiary_fixed_variant")
                .SetPalette(s => s.TertiaryPalette)
                .SetBackground(s => TertiaryFixedDim)
                .SetContrastCurve(s => GetContrastCurve(4.5))
                .Build();
            return base.OnTertiaryFixedVariant.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    // Android-only colors
    public new DynamicColor ControlActivated
    {
        get
        {
            var color2025 = PrimaryContainer.ToBuilder().SetName("control_activated").Build();
            return base.ControlActivated.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor ControlNormal
    {
        get
        {
            var color2025 = OnSurfaceVariant.ToBuilder().SetName("control_normal").Build();
            return base.ControlNormal.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    public new DynamicColor TextPrimaryInverse
    {
        get
        {
            var color2025 = InverseOnSurface.ToBuilder().SetName("text_primary_inverse").Build();
            return base.TextPrimaryInverse.ToBuilder().ExtendSpecVersion(SpecVersion.Spec2025, color2025).Build();
        }
    }

    // Other helpers
    private static double FindBestToneForChroma(double hue, double chroma, double tone, bool byDecreasingTone)
    {
        double answer = tone;
        var bestCandidate = Hct.From(hue, chroma, answer);
        while (bestCandidate.Chroma < chroma)
        {
            if (tone < 0 || tone > 100) break;
            tone += byDecreasingTone ? -1.0 : 1.0;
            var newCandidate = Hct.From(hue, chroma, tone);
            if (bestCandidate.Chroma < newCandidate.Chroma)
            {
                bestCandidate = newCandidate;
                answer = tone;
            }
        }
        return answer;
    }

    private static double TMaxC(TonalPalette palette) => TMaxC(palette, 0, 100);

    private static double TMaxC(TonalPalette palette, double lowerBound, double upperBound) => TMaxC(palette, lowerBound, upperBound, 1.0);

    private static double TMaxC(TonalPalette palette, double lowerBound, double upperBound, double chromaMultiplier)
    {
        double answer = FindBestToneForChroma(palette.GetHue(), palette.GetChroma() * chromaMultiplier, 100, true);
        return MathUtils.ClampDouble(lowerBound, upperBound, answer);
    }

    private static double TMinC(TonalPalette palette) => TMinC(palette, 0, 100);

    private static double TMinC(TonalPalette palette, double lowerBound, double upperBound)
    {
        double answer = FindBestToneForChroma(palette.GetHue(), palette.GetChroma(), 0, false);
        return MathUtils.ClampDouble(lowerBound, upperBound, answer);
    }

    private static ContrastCurve GetContrastCurve(double @default)
    {
        if (@default == 1.5) return new ContrastCurve(1.5, 1.5, 3, 4.5);
        if (@default == 3.0) return new ContrastCurve(3, 3, 4.5, 7);
        if (@default == 4.5) return new ContrastCurve(4.5, 4.5, 7, 11);
        if (@default == 6.0) return new ContrastCurve(6, 6, 7, 11);
        if (@default == 7.0) return new ContrastCurve(7, 7, 11, 21);
        if (@default == 9.0) return new ContrastCurve(9, 9, 11, 21);
        if (@default == 11.0) return new ContrastCurve(11, 11, 21, 21);
        if (@default == 21.0) return new ContrastCurve(21, 21, 21, 21);
        return new ContrastCurve(@default, @default, 7, 21);
    }

    // Color value calculations (2025 overrides)
    public new Hct GetHct(DynamicScheme scheme, DynamicColor color)
    {
        var palette = color.palette(scheme);
        double tone = GetTone(scheme, color);
        double hue = palette.GetHue();
        double chromaMultiplier = color.chromaMultiplier?.Invoke(scheme) ?? 1.0;
        double chroma = palette.GetChroma() * chromaMultiplier;
        return Hct.From(hue, chroma, tone);
    }

    public new double GetTone(DynamicScheme scheme, DynamicColor color)
    {
        var toneDeltaPair = color.toneDeltaPair == null ? null : color.toneDeltaPair(scheme);

        if (toneDeltaPair != null)
        {
            var roleA = toneDeltaPair.RoleA;
            var roleB = toneDeltaPair.RoleB;
            var polarity = toneDeltaPair.Polarity;
            var constraint = toneDeltaPair.Constraint;
            double absoluteDelta = (polarity == TonePolarity.Darker || (polarity == TonePolarity.RelativeLighter && scheme.IsDark) || (polarity == TonePolarity.RelativeDarker && !scheme.IsDark))
                ? -toneDeltaPair.Delta : toneDeltaPair.Delta;

            bool amRoleA = color.name.Equals(roleA.name);
            var selfRole = amRoleA ? roleA : roleB;
            var referenceRole = amRoleA ? roleB : roleA;
            double selfTone = selfRole.tone(scheme);
            double referenceTone = referenceRole.GetTone(scheme);
            double relativeDelta = absoluteDelta * (amRoleA ? 1 : -1);

            switch (constraint)
            {
                case DeltaConstraint.Exact:
                    selfTone = MathUtils.ClampDouble(0, 100, referenceTone + relativeDelta);
                    break;
                case DeltaConstraint.Nearer:
                    if (relativeDelta > 0)
                    {
                        selfTone = MathUtils.ClampDouble(0, 100, MathUtils.ClampDouble(referenceTone, referenceTone + relativeDelta, selfTone));
                    }
                    else
                    {
                        selfTone = MathUtils.ClampDouble(0, 100, MathUtils.ClampDouble(referenceTone + relativeDelta, referenceTone, selfTone));
                    }
                    break;
                case DeltaConstraint.Farther:
                    if (relativeDelta > 0)
                    {
                        selfTone = MathUtils.ClampDouble(referenceTone + relativeDelta, 100, selfTone);
                    }
                    else
                    {
                        selfTone = MathUtils.ClampDouble(0, referenceTone + relativeDelta, selfTone);
                    }
                    break;
            }

            if (color.background != null && color.contrastCurve != null)
            {
                var background = color.background(scheme);
                var contrastCurve = color.contrastCurve(scheme);
                if (background != null && contrastCurve != null)
                {
                    double bgTone = background.GetTone(scheme);
                    double selfContrast = contrastCurve.Get(scheme.ContrastLevel);
                    selfTone = (Contrast.RatioOfTones(bgTone, selfTone) >= selfContrast && scheme.ContrastLevel >= 0)
                        ? selfTone
                        : DynamicColor.ForegroundTone(bgTone, selfContrast);
                }
            }

            // Avoid awkward tones for backgrounds (except *_fixed_dim)
            if (color.isBackground && !color.name.EndsWith("_fixed_dim"))
            {
                if (selfTone >= 57)
                {
                    selfTone = MathUtils.ClampDouble(65, 100, selfTone);
                }
                else
                {
                    selfTone = MathUtils.ClampDouble(0, 49, selfTone);
                }
            }

            return selfTone;
        }
        else
        {
            double answer = color.tone(scheme);

            if (color.background == null || color.background(scheme) == null || color.contrastCurve == null || color.contrastCurve(scheme) == null)
            {
                return answer;
            }

            double bgTone = color.background(scheme).GetTone(scheme);
            double desiredRatio = color.contrastCurve(scheme).Get(scheme.ContrastLevel);

            answer = (Contrast.RatioOfTones(bgTone, answer) >= desiredRatio && scheme.ContrastLevel >= 0)
                ? answer
                : DynamicColor.ForegroundTone(bgTone, desiredRatio);

            // Avoid awkward tones for backgrounds (except *_fixed_dim)
            if (color.isBackground && !color.name.EndsWith("_fixed_dim"))
            {
                if (answer >= 57)
                {
                    answer = MathUtils.ClampDouble(65, 100, answer);
                }
                else
                {
                    answer = MathUtils.ClampDouble(0, 49, answer);
                }
            }

            if (color.secondBackground == null || color.secondBackground(scheme) == null)
            {
                return answer;
            }

            double bgTone1 = color.background(scheme).GetTone(scheme);
            double bgTone2 = color.secondBackground(scheme).GetTone(scheme);
            double upper = System.Math.Max(bgTone1, bgTone2);
            double lower = System.Math.Min(bgTone1, bgTone2);

            if (Contrast.RatioOfTones(upper, answer) >= desiredRatio && Contrast.RatioOfTones(lower, answer) >= desiredRatio)
            {
                return answer;
            }

            double lightOption = Contrast.Lighter(upper, desiredRatio);
            double darkOption = Contrast.Darker(lower, desiredRatio);

            bool prefersLight = DynamicColor.TonePrefersLightForeground(bgTone1) || DynamicColor.TonePrefersLightForeground(bgTone2);
            if (prefersLight)
            {
                return (lightOption < 0) ? 100 : lightOption;
            }
            if (lightOption >= 0 && darkOption < 0)
            {
                return lightOption;
            }
            return (darkOption < 0) ? 0 : darkOption;
        }
    }

    // Scheme Palettes
    public new TonalPalette GetPrimaryPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel)
    {
        return variant switch
        {
            Variant.Neutral => TonalPalette.FromHueAndChroma(
                sourceColorHct.Hue,
                platform == Platform.Phone ? (Hct.IsBlue(sourceColorHct.Hue) ? 12 : 8) : (Hct.IsBlue(sourceColorHct.Hue) ? 16 : 12)),
            Variant.TonalSpot => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, platform == Platform.Phone && isDark ? 26 : 32),
            Variant.Expressive => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, platform == Platform.Phone ? (isDark ? 36 : 48) : 40),
            Variant.Vibrant => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, platform == Platform.Phone ? 74 : 56),
            _ => base.GetPrimaryPalette(variant, sourceColorHct, isDark, platform, contrastLevel),
        };
    }

    public new TonalPalette GetSecondaryPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel)
    {
        return variant switch
        {
            Variant.Neutral => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, platform == Platform.Phone ? (Hct.IsBlue(sourceColorHct.Hue) ? 6 : 4) : (Hct.IsBlue(sourceColorHct.Hue) ? 10 : 6)),
            Variant.TonalSpot => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, 16),
            Variant.Expressive => TonalPalette.FromHueAndChroma(
                DynamicScheme.GetRotatedHue(
                    sourceColorHct,
                    [0.0, 105.0, 140.0, 204.0, 253.0, 278.0, 300.0, 333.0, 360.0],
                    [-160.0, 155.0, -100.0, 96.0, -96.0, -156.0, -165.0, -160.0]
                ), platform == Platform.Phone ? (isDark ? 16 : 24) : 24),
            Variant.Vibrant => TonalPalette.FromHueAndChroma(
                DynamicScheme.GetRotatedHue(
                    sourceColorHct,
                    [0.0, 38.0, 105.0, 140.0, 333.0, 360.0],
                    [-14.0, 10.0, -14.0, 10.0, -14.0]
                ), platform == Platform.Phone ? 56 : 36),
            _ => base.GetSecondaryPalette(variant, sourceColorHct, isDark, platform, contrastLevel),
        };
    }

    public new TonalPalette GetTertiaryPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel)
    {
        return variant switch
        {
            Variant.Neutral => TonalPalette.FromHueAndChroma(
                DynamicScheme.GetRotatedHue(
                    sourceColorHct,
                    [0.0, 38.0, 105.0, 161.0, 204.0, 278.0, 333.0, 360.0],
                    [-32.0, 26.0, 10.0, -39.0, 24.0, -15.0, -32.0]
                ), platform == Platform.Phone ? 20 : 36),
            Variant.TonalSpot => TonalPalette.FromHueAndChroma(
                DynamicScheme.GetRotatedHue(
                    sourceColorHct,
                    [0.0, 20.0, 71.0, 161.0, 333.0, 360.0],
                    [-40.0, 48.0, -32.0, 40.0, -32.0]
                ), platform == Platform.Phone ? 28 : 32),
            Variant.Expressive => TonalPalette.FromHueAndChroma(
                DynamicScheme.GetRotatedHue(
                    sourceColorHct,
                    [0.0, 105.0, 140.0, 204.0, 253.0, 278.0, 300.0, 333.0, 360.0],
                    [-165.0, 160.0, -105.0, 101.0, -101.0, -160.0, -170.0, -165.0]
                ), 48),
            Variant.Vibrant => TonalPalette.FromHueAndChroma(
                DynamicScheme.GetRotatedHue(
                    sourceColorHct,
                    [0.0, 38.0, 71.0, 105.0, 140.0, 161.0, 253.0, 333.0, 360.0],
                    [-72.0, 35.0, 24.0, -24.0, 62.0, 50.0, 62.0, -72.0]
                ), 56),
            _ => base.GetTertiaryPalette(variant, sourceColorHct, isDark, platform, contrastLevel),
        };
    }

    public new TonalPalette GetNeutralPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel)
    {
        return variant switch
        {
            Variant.Neutral => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, platform == Platform.Phone ? 1.4 : 6.0),
            Variant.TonalSpot => TonalPalette.FromHueAndChroma(sourceColorHct.Hue, platform == Platform.Phone ? 5.0 : 10.0),
            Variant.Expressive => TonalPalette.FromHueAndChroma(GetExpressiveNeutralHue(sourceColorHct), GetExpressiveNeutralChroma(sourceColorHct, isDark, platform)),
            Variant.Vibrant => TonalPalette.FromHueAndChroma(GetVibrantNeutralHue(sourceColorHct), GetVibrantNeutralChroma(sourceColorHct, platform)),
            _ => base.GetNeutralPalette(variant, sourceColorHct, isDark, platform, contrastLevel),
        };
    }

    public new TonalPalette GetNeutralVariantPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel)
    {
        switch (variant)
        {
            case Variant.Neutral:
                return TonalPalette.FromHueAndChroma(sourceColorHct.Hue, (platform == Platform.Phone ? 1.4 : 6.0) * 2.2);
            case Variant.TonalSpot:
                return TonalPalette.FromHueAndChroma(sourceColorHct.Hue, (platform == Platform.Phone ? 5.0 : 10.0) * 1.7);
            case Variant.Expressive:
                double expressiveNeutralHue = GetExpressiveNeutralHue(sourceColorHct);
                double expressiveNeutralChroma = GetExpressiveNeutralChroma(sourceColorHct, isDark, platform);
                return TonalPalette.FromHueAndChroma(
                    expressiveNeutralHue,
                    expressiveNeutralChroma * (expressiveNeutralHue >= 105 && expressiveNeutralHue < 125 ? 1.6 : 2.3));
            case Variant.Vibrant:
                double vibrantNeutralHue = GetVibrantNeutralHue(sourceColorHct);
                double vibrantNeutralChroma = GetVibrantNeutralChroma(sourceColorHct, platform);
                return TonalPalette.FromHueAndChroma(vibrantNeutralHue, vibrantNeutralChroma * 1.29);
            default:
                return base.GetNeutralVariantPalette(variant, sourceColorHct, isDark, platform, contrastLevel);
        }
    }

    public new TonalPalette? GetErrorPalette(Variant variant, Hct sourceColorHct, bool isDark, Platform platform, double contrastLevel)
    {
        double errorHue = DynamicScheme.GetPiecewiseValue(
            sourceColorHct,
            [0.0, 3.0, 13.0, 23.0, 33.0, 43.0, 153.0, 273.0, 360.0],
            [12.0, 22.0, 32.0, 12.0, 22.0, 32.0, 22.0, 12.0]);
        return variant switch
        {
            Variant.Neutral => TonalPalette.FromHueAndChroma(errorHue, platform == Platform.Phone ? 50.0 : 40.0),
            Variant.TonalSpot => TonalPalette.FromHueAndChroma(errorHue, platform == Platform.Phone ? 60.0 : 48.0),
            Variant.Expressive => TonalPalette.FromHueAndChroma(errorHue, platform == Platform.Phone ? 64.0 : 48.0),
            Variant.Vibrant => TonalPalette.FromHueAndChroma(errorHue, platform == Platform.Phone ? 80.0 : 60.0),
            _ => base.GetErrorPalette(variant, sourceColorHct, isDark, platform, contrastLevel),
        };
    }

    private static double GetExpressiveNeutralHue(Hct sourceColorHct)
    {
        return DynamicScheme.GetRotatedHue(
            sourceColorHct,
            [0.0, 71.0, 124.0, 253.0, 278.0, 300.0, 360.0],
            [10.0, 0.0, 10.0, 0.0, 10.0, 0.0]);
    }

    private static double GetExpressiveNeutralChroma(Hct sourceColorHct, bool isDark, Platform platform)
    {
        double neutralHue = GetExpressiveNeutralHue(sourceColorHct);
        return platform == Platform.Phone ? (isDark ? (Hct.IsYellow(neutralHue) ? 6.0 : 14.0) : 18.0) : 12.0;
    }

    private static double GetVibrantNeutralHue(Hct sourceColorHct)
    {
        return DynamicScheme.GetRotatedHue(
            sourceColorHct,
            [0.0, 38.0, 105.0, 140.0, 333.0, 360.0],
            [-14.0, 10.0, -14.0, 10.0, -14.0]);
    }

    private static double GetVibrantNeutralChroma(Hct sourceColorHct, Platform platform)
    {
        double neutralHue = GetVibrantNeutralHue(sourceColorHct);
        return platform == Platform.Phone ? 28.0 : (Hct.IsBlue(neutralHue) ? 28.0 : 20.0);
    }
}