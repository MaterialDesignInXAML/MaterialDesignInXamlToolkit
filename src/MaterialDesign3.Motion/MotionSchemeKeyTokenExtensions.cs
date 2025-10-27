namespace MaterialDesignThemes.Motion;

internal static class MotionSchemeKeyTokenExtensions
{
    internal static SpringMotionSpec Value(this MotionSchemeKeyTokens token) =>
        token.Value(MotionSchemeContext.Current);

    internal static SpringMotionSpec Value(
        this MotionSchemeKeyTokens token,
        IMotionScheme scheme) =>
        scheme.FromToken(token);
}
