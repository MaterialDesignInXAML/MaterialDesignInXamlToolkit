namespace MaterialColorUtilities;

/// <summary>
/// Represents a Material color scheme, a mapping of color roles to colors.
/// </summary>
public record Scheme(
    int Primary,
    int OnPrimary,
    int PrimaryContainer,
    int OnPrimaryContainer,
    int Secondary,
    int OnSecondary,
    int SecondaryContainer,
    int OnSecondaryContainer,
    int Tertiary,
    int OnTertiary,
    int TertiaryContainer,
    int OnTertiaryContainer,
    int Error,
    int OnError,
    int ErrorContainer,
    int OnErrorContainer,
    int Background,
    int OnBackground,
    int Surface,
    int OnSurface,
    int SurfaceVariant,
    int OnSurfaceVariant,
    int Outline,
    int OutlineVariant,
    int Shadow,
    int Scrim,
    int InverseSurface,
    int InverseOnSurface,
    int InversePrimary)
{
    /// <summary>
    /// Creates a light theme Scheme from a source color in ARGB.
    /// </summary>
    public static Scheme Light(int argb)
    {
        var core = CorePalette.Of(argb);
        return LightFromCorePalette(core);
    }

    /// <summary>
    /// Creates a dark theme Scheme from a source color in ARGB.
    /// </summary>
    public static Scheme Dark(int argb)
    {
        var core = CorePalette.Of(argb);
        return DarkFromCorePalette(core);
    }

    /// <summary>
    /// Creates a light theme content-based Scheme from a source color in ARGB.
    /// </summary>
    public static Scheme LightContent(int argb)
    {
        var core = CorePalette.ContentOf(argb);
        return LightFromCorePalette(core);
    }

    /// <summary>
    /// Creates a dark theme content-based Scheme from a source color in ARGB.
    /// </summary>
    public static Scheme DarkContent(int argb)
    {
        var core = CorePalette.ContentOf(argb);
        return DarkFromCorePalette(core);
    }

    private static Scheme LightFromCorePalette(CorePalette core)
    {
        return new Scheme(
            // Primary
            core.a1.Tone(40),
            core.a1.Tone(100),
            core.a1.Tone(90),
            core.a1.Tone(10),
            // Secondary
            core.a2.Tone(40),
            core.a2.Tone(100),
            core.a2.Tone(90),
            core.a2.Tone(10),
            // Tertiary
            core.a3.Tone(40),
            core.a3.Tone(100),
            core.a3.Tone(90),
            core.a3.Tone(10),
            // Error
            core.error.Tone(40),
            core.error.Tone(100),
            core.error.Tone(90),
            core.error.Tone(10),
            // Neutral n1
            core.n1.Tone(99),
            core.n1.Tone(10),
            core.n1.Tone(99),
            core.n1.Tone(10),
            // Neutral variant n2
            core.n2.Tone(90),
            core.n2.Tone(30),
            core.n2.Tone(50),
            core.n2.Tone(80),
            // Shadow, Scrim
            unchecked((int)0xff000000), // shadow (0 with alpha 255)
            unchecked((int)0xff000000), // scrim
            // Inverse
            core.n1.Tone(20),
            core.n1.Tone(95),
            core.a1.Tone(80)
        );
    }

    private static Scheme DarkFromCorePalette(CorePalette core)
    {
        return new Scheme(
            // Primary
            core.a1.Tone(80),
            core.a1.Tone(20),
            core.a1.Tone(30),
            core.a1.Tone(90),
            // Secondary
            core.a2.Tone(80),
            core.a2.Tone(20),
            core.a2.Tone(30),
            core.a2.Tone(90),
            // Tertiary
            core.a3.Tone(80),
            core.a3.Tone(20),
            core.a3.Tone(30),
            core.a3.Tone(90),
            // Error
            core.error.Tone(80),
            core.error.Tone(20),
            core.error.Tone(30),
            core.error.Tone(80),
            // Neutral n1
            core.n1.Tone(10),
            core.n1.Tone(90),
            core.n1.Tone(10),
            core.n1.Tone(90),
            // Neutral variant n2
            core.n2.Tone(30),
            core.n2.Tone(80),
            core.n2.Tone(60),
            core.n2.Tone(30),
            // Shadow, Scrim
            unchecked((int)0xff000000), // shadow
            unchecked((int)0xff000000), // scrim
            // Inverse
            core.n1.Tone(90),
            core.n1.Tone(20),
            core.a1.Tone(40)
        );
    }
}