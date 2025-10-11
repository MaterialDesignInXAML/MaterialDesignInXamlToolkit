namespace MaterialDesignThemes.Motion;

/// <summary>
/// Represents a simple spring-based motion specification matching the Material 3 motion tokens.
/// </summary>
/// <param name="DampingRatio">Gets the damping ratio to use for the spring animation.</param>
/// <param name="Stiffness">Gets the spring stiffness to use for the animation.</param>
public record class SpringMotionSpec(double DampingRatio, double Stiffness);
