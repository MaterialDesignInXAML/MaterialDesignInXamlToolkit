namespace MaterialDesignThemes.Motion;

/// <summary>
/// Possible reasons for <see cref="Animatable{T, V}"/>s to end.
/// </summary>
public enum AnimationEndReason
{
    /// <summary>
    /// Animation will be forced to end when its value reaches upper/lower bound (if they have been
    /// defined, e.g. via <c>Animatable.updateBounds</c>).
    /// Unlike <see cref="Finished"/>, when an animation ends due to <see cref="BoundReached"/>, it often falls
    /// short from its initial target, and the remaining velocity is often non-zero. Both the end value and the
    /// remaining velocity can be obtained via <c>AnimationResult</c>.
    /// </summary>
    BoundReached,

    /// <summary>
    /// Animation has finished successfully without any interruption.
    /// </summary>
    Finished,
}
