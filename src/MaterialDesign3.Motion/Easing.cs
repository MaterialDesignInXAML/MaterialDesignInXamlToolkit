namespace MaterialDesignThemes.Motion;

/// <summary>
/// The easing to be used for adjusting an animation's fraction. This allows
/// animation to speed up and slow down, rather than moving at a constant rate.
/// If not set, defaults to Linear Interpolator.
/// </summary>
/// <param name="CubicBezier">
/// The cubic polynomial easing that implements third-order Bezier-curves.
/// </param>
public record Easing(CubicBezierEasing? CubicBezier);
