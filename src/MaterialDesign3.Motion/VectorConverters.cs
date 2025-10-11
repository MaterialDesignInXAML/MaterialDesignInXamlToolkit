namespace MaterialDesignThemes.Motion;

internal static class VectorConverters
{
    public static float Lerp(float start, float stop, float fraction) =>
        (start * (1f - fraction)) + (stop * fraction);
}
