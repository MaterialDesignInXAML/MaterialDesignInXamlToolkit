namespace MaterialDesignThemes.Motion;

internal static class Hermite
{
    public static float Interpolate(float h, float x, float y1, float y2, float t1, float t2)
    {
        var x2 = x * x;
        var x3 = x2 * x;
        return h * t1 * (x - 2 * x2 + x3) + h * t2 * (x3 - x2) + y1 - (3 * x2 - 2 * x3) * (y1 - y2);
    }

    public static float Differential(float h, float x, float y1, float y2, float t1, float t2)
    {
        var x2 = x * x;
        return h * (t1 - 2 * x * (2 * t1 + t2) + 3 * (t1 + t2) * x2) - 6 * (x - x2) * (y1 - y2);
    }
}