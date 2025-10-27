namespace MaterialDesignThemes.Motion;

/// <summary>
/// The cubic polynomial easing that implements third-order Bezier curves.
/// This is equivalent to the Android PathInterpolator.
/// </summary>
/// <param name="X1">
/// The x coordinate of the first control point.
/// The line through the point (0, 0)
/// and the first control point is tangent to the easing at the point (0, 0)
/// </param>
/// <param name="Y1">
/// The y coordinate of the first control point.
/// The line through the point (0, 0)
/// and the first control point is tangent to the easing at the point (0, 0).
/// </param>
/// <param name="X2">
/// The x coordinate of the second control point.
/// The line through the point (1, 1)
/// and the second control point is tangent to the easing at the point (1, 1).
/// </param>
/// <param name="Y2">
/// The y coordinate of the second control point.
/// The line through the point (1, 1)
/// and the second control point is tangent to the easing at the point (1, 1).
/// </param>
public record CubicBezierEasing(float X1, float Y1, float X2, float Y2);
