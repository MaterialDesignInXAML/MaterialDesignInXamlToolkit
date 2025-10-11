namespace MaterialDesignThemes.Motion;

/// <summary>
/// Provides access to the current motion scheme and allows overriding it at runtime.
/// </summary>
public static class MotionSchemeContext
{
    private static IMotionScheme _current = MotionSchemes.Standard();

    public static IMotionScheme Current
    {
        get => _current;
        set => _current = value ?? throw new ArgumentNullException(nameof(value));
    }
}
