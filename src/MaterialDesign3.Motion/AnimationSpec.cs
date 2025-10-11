namespace MaterialDesignThemes.Motion;

/// <summary>
/// Animation parameters that can be added to any animatable node.
/// </summary>
public sealed class AnimationSpec
{
    /// <summary>
    /// Animation parameters including duration, easing and repeat delay.
    /// </summary>
    public AnimationParameters AnimationParameters { get; set; } = new();

    /// <summary>
    /// The repeatable mode to be used for specifying repetition parameters for the animation.
    /// </summary>
    /// <remarks>
    /// If not set, animation won't be repeated.
    /// </remarks>
    public Repeatable? Repeatable { get; set; }
}
