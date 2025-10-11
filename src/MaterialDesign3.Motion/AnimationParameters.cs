namespace MaterialDesignThemes.Motion;

/// <summary>
/// Animation specs of duration, easing and repeat delay.
/// </summary>
public sealed class AnimationParameters
{
    /// <summary>
    /// The duration of the animation.
    /// </summary>
    /// <remarks>
    /// If not set, defaults to 300ms.
    /// </remarks>
    public TimeSpan Duration { get; set; } = TimeSpan.FromMilliseconds(300);

    /// <summary>
    /// The easing to be used for adjusting an animation's fraction.
    /// </summary>
    /// <remarks>
    /// If not set, defaults to Linear Interpolation.
    /// </remarks>
    public Easing? Easing { get; set; }

    /// <summary>
    /// Animation delay in millis.
    /// </summary>
    /// <remarks>
    /// When used outside repeatable, this is the delay to start the animation.
    /// When set inside repeatable, this is the delay before repeating animation.
    /// If not set, no delay will be applied.
    /// </remarks>
    public TimeSpan? Delay { get; set; } = TimeSpan.FromMilliseconds(3);
}
