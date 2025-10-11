namespace MaterialDesignThemes.Motion;

public static class MotionSchemeExtensions
{
    public static SpringMotionSpec RememberDefaultSpatialSpec(this IMotionScheme scheme) => scheme.DefaultSpatialSpec;
    public static SpringMotionSpec RememberFastSpatialSpec(this IMotionScheme scheme) => scheme.FastSpatialSpec;
    public static SpringMotionSpec RememberSlowSpatialSpec(this IMotionScheme scheme) => scheme.SlowSpatialSpec;
    public static SpringMotionSpec RememberDefaultEffectsSpec(this IMotionScheme scheme) => scheme.DefaultEffectsSpec;
    public static SpringMotionSpec RememberFastEffectsSpec(this IMotionScheme scheme) => scheme.FastEffectsSpec;
    public static SpringMotionSpec RememberSlowEffectsSpec(this IMotionScheme scheme) => scheme.SlowEffectsSpec;

    internal static SpringMotionSpec FromToken(this IMotionScheme scheme, MotionSchemeKeyTokens token) =>
        token switch
        {
            MotionSchemeKeyTokens.DefaultSpatial => scheme.RememberDefaultSpatialSpec(),
            MotionSchemeKeyTokens.FastSpatial => scheme.RememberFastSpatialSpec(),
            MotionSchemeKeyTokens.SlowSpatial => scheme.RememberSlowSpatialSpec(),
            MotionSchemeKeyTokens.DefaultEffects => scheme.RememberDefaultEffectsSpec(),
            MotionSchemeKeyTokens.FastEffects => scheme.RememberFastEffectsSpec(),
            MotionSchemeKeyTokens.SlowEffects => scheme.RememberSlowEffectsSpec(),
            _ => throw new ArgumentOutOfRangeException(nameof(token), token, null),
        };
}
