namespace MaterialDesignThemes.Motion;

/// <summary>
/// Physics class contains a number of recommended configurations for physics animations.
/// </summary>
public static class SpringConstants
{
    /// <summary>
    /// Stiffness constant for extremely stiff spring.
    /// </summary>
    public const float StiffnessHigh = 10_000f;

    /// <summary>
    /// Stiffness constant for medium stiff spring. This is the default stiffness for spring force.
    /// </summary>
    public const float StiffnessMedium = 1_500f;

    /// <summary>
    /// Stiffness constant for medium-low stiff spring. This is the default stiffness for springs used in enter/exit transitions.
    /// </summary>
    public const float StiffnessMediumLow = 400f;

    /// <summary>
    /// Stiffness constant for a spring with low stiffness.
    /// </summary>
    public const float StiffnessLow = 200f;

    /// <summary>
    /// Stiffness constant for a spring with very low stiffness.
    /// </summary>
    public const float StiffnessVeryLow = 50f;

    /// <summary>
    /// Damping ratio for a very bouncy spring.
    /// </summary>
    public const float DampingRatioHighBouncy = 0.2f;

    /// <summary>
    /// Damping ratio for a medium bouncy spring. This is also the default damping ratio for spring force.
    /// </summary>
    public const float DampingRatioMediumBouncy = 0.5f;

    /// <summary>
    /// Damping ratio for a spring with low bounciness.
    /// </summary>
    public const float DampingRatioLowBouncy = 0.75f;

    /// <summary>
    /// Friction ratio for a spring with gentle bounciness.
    /// </summary>
    public const float DampingRatioMediumBouncyFriction = 0.65f;

    /// <summary>
    /// Damping ratio for a spring that is critically damped (i.e. without oscillation).
    /// </summary>
    public const float DampingRatioNoBouncy = 1f;
}
