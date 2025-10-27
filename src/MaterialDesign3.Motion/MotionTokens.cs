namespace MaterialDesignThemes.Motion;

public static class MotionTokens
{
    public static readonly TimeSpan DurationExtraLong1 = TimeSpan.FromMilliseconds(700.0);
    public static readonly TimeSpan DurationExtraLong2 = TimeSpan.FromMilliseconds(800.0);
    public static readonly TimeSpan DurationExtraLong3 = TimeSpan.FromMilliseconds(900.0);
    public static readonly TimeSpan DurationExtraLong4 = TimeSpan.FromMilliseconds(1000.0);
    public static readonly TimeSpan DurationLong1 = TimeSpan.FromMilliseconds(450.0);
    public static readonly TimeSpan DurationLong2 = TimeSpan.FromMilliseconds(500.0);
    public static readonly TimeSpan DurationLong3 = TimeSpan.FromMilliseconds(550.0);
    public static readonly TimeSpan DurationLong4 = TimeSpan.FromMilliseconds(600.0);
    public static readonly TimeSpan DurationMedium1 = TimeSpan.FromMilliseconds(250.0);
    public static readonly TimeSpan DurationMedium2 = TimeSpan.FromMilliseconds(300.0);
    public static readonly TimeSpan DurationMedium3 = TimeSpan.FromMilliseconds(350.0);
    public static readonly TimeSpan DurationMedium4 = TimeSpan.FromMilliseconds(400.0);
    public static readonly TimeSpan DurationShort1 = TimeSpan.FromMilliseconds(50.0);
    public static readonly TimeSpan DurationShort2 = TimeSpan.FromMilliseconds(100.0);
    public static readonly TimeSpan DurationShort3 = TimeSpan.FromMilliseconds(150.0);
    public static readonly TimeSpan DurationShort4 = TimeSpan.FromMilliseconds(200.0);

    public static readonly CubicBezierEasing EasingEmphasizedCubicBezier = new(0.2f, 0.0f, 0.0f, 1.0f);
    public static readonly CubicBezierEasing EasingEmphasizedAccelerateCubicBezier = new(0.3f, 0.0f, 0.8f, 0.15f);
    public static readonly CubicBezierEasing EasingEmphasizedDecelerateCubicBezier = new(0.05f, 0.7f, 0.1f, 1.0f);
    public static readonly CubicBezierEasing EasingLegacyCubicBezier = new(0.4f, 0.0f, 0.2f, 1.0f);
    public static readonly CubicBezierEasing EasingLegacyAccelerateCubicBezier = new(0.4f, 0.0f, 1.0f, 1.0f);
    public static readonly CubicBezierEasing EasingLegacyDecelerateCubicBezier = new(0.0f, 0.0f, 0.2f, 1.0f);
    public static readonly CubicBezierEasing EasingLinearCubicBezier = new(0.0f, 0.0f, 1.0f, 1.0f);
    public static readonly CubicBezierEasing EasingStandardCubicBezier = new(0.2f, 0.0f, 0.0f, 1.0f);
    public static readonly CubicBezierEasing EasingStandardAccelerateCubicBezier = new(0.3f, 0.0f, 1.0f, 1.0f);
    public static readonly CubicBezierEasing EasingStandardDecelerateCubicBezier = new(0.0f, 0.0f, 0.0f, 1.0f);
}
