namespace MaterialDesignThemes.Motion;

/// <summary>
/// Factory helpers that expose the built-in Material motion schemes.
/// </summary>
public static class MotionSchemes
{
    public static IMotionScheme Standard() => new DelegateMotionScheme(
        defaultSpatial: new SpringMotionSpec(StandardSpatialDampingRatio, 700.0),
        fastSpatial: new SpringMotionSpec(StandardSpatialDampingRatio, 1400.0),
        slowSpatial: new SpringMotionSpec(StandardSpatialDampingRatio, 300.0),
        defaultEffects: new SpringMotionSpec(EffectsDampingRatio, EffectsDefaultStiffness),
        fastEffects: new SpringMotionSpec(EffectsDampingRatio, EffectsFastStiffness),
        slowEffects: new SpringMotionSpec(EffectsDampingRatio, EffectsSlowStiffness));

    public static IMotionScheme Expressive() => new DelegateMotionScheme(
        defaultSpatial: new SpringMotionSpec(0.8, 380.0),
        fastSpatial: new SpringMotionSpec(0.6, 800.0),
        slowSpatial: new SpringMotionSpec(0.8, 200.0),
        defaultEffects: new SpringMotionSpec(EffectsDampingRatio, EffectsDefaultStiffness),
        fastEffects: new SpringMotionSpec(EffectsDampingRatio, EffectsFastStiffness),
        slowEffects: new SpringMotionSpec(EffectsDampingRatio, EffectsSlowStiffness));

    private sealed class DelegateMotionScheme : IMotionScheme
    {
        public DelegateMotionScheme(
            SpringMotionSpec defaultSpatial,
            SpringMotionSpec fastSpatial,
            SpringMotionSpec slowSpatial,
            SpringMotionSpec defaultEffects,
            SpringMotionSpec fastEffects,
            SpringMotionSpec slowEffects)
        {
            DefaultSpatialSpec = defaultSpatial;
            FastSpatialSpec = fastSpatial;
            SlowSpatialSpec = slowSpatial;
            DefaultEffectsSpec = defaultEffects;
            FastEffectsSpec = fastEffects;
            SlowEffectsSpec = slowEffects;
        }

        public SpringMotionSpec DefaultSpatialSpec { get; }

        public SpringMotionSpec FastSpatialSpec { get; }

        public SpringMotionSpec SlowSpatialSpec { get; }

        public SpringMotionSpec DefaultEffectsSpec { get; }

        public SpringMotionSpec FastEffectsSpec { get; }

        public SpringMotionSpec SlowEffectsSpec { get; }
    }

    /// <summary>
    /// Spring.DampingRatioNoBouncy
    /// </summary>
    private const double EffectsDampingRatio = 1.0;
    private const double EffectsDefaultStiffness = 1600.0;
    private const double EffectsFastStiffness = 3800.0;
    private const double EffectsSlowStiffness = 800.0;
    private const double StandardSpatialDampingRatio = 0.9;
}
