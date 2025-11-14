namespace MaterialDesignThemes.Motion;

/// <summary>
/// Provides a set of spring-based motion specifications for Material components.
/// </summary>
public interface IMotionScheme
{
    SpringMotionSpec DefaultSpatialSpec { get; }

    SpringMotionSpec FastSpatialSpec { get; }

    SpringMotionSpec SlowSpatialSpec { get; }

    SpringMotionSpec DefaultEffectsSpec { get; }

    SpringMotionSpec FastEffectsSpec { get; }

    SpringMotionSpec SlowEffectsSpec { get; }
}
