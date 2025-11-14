namespace MaterialDesignThemes.Motion;

/// <summary>
/// The repeat mode to specify how animation will behave when repeated.
/// </summary>
public enum RepeatMode
{
    /// <summary>
    /// The unknown repeat mode.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The repeat mode where animation restarts from the beginning when repeated.
    /// </summary>
    Restart = 1,

    /// <summary>
    /// The repeat mode where animation is played in reverse when repeated.
    /// </summary>
    Reverse = 2
}
