namespace MaterialDesignThemes.Motion;

/// <summary>
/// The repeatable mode to be used for specifying how many times animation will be repeated.
/// </summary>
public sealed class Repeatable
{
    /// <summary>
    /// The number specifying how many times animation will be repeated.
    /// </summary>
    /// <remarks>
    /// If not set, defaults to 0, i.e. repeat infinitely.
    /// </remarks>
    public uint Iterations { get; set; } = 0;

    /// <summary>
    /// The repeat mode to specify how animation will behave when repeated.
    /// </summary>
    /// <remarks>
    /// If not set, defaults to restart.
    /// </remarks>
    public RepeatMode RepeatMode { get; set; } = RepeatMode.Reverse;

    /// <summary>
    /// Optional custom parameters for the forward passes of animation.
    /// </summary>
    /// <remarks>
    /// If not set, use the main animation parameters set outside of Repeatable.
    /// </remarks>
    public AnimationParameters? ForwardRepeatOverride { get; set; }

    /// <summary>
    /// Optional custom parameters for the reverse passes of animation.
    /// </summary>
    /// <remarks>
    /// If not set, use the main animation parameters set outside of Repeatable.
    /// </remarks>
    public AnimationParameters? ReverseRepeatOverride { get; set; }
}
