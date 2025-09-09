using static System.Math;

namespace MaterialColorUtilities;

public class DynamicScheme
{
    public static readonly SpecVersion DEFAULT_SPEC_VERSION = SpecVersion.SPEC_2021;
    public static readonly Platform DEFAULT_PLATFORM = Platform.PHONE;

    // Core properties
    public int SourceColorArgb { get; private set; }
    public Hct SourceColorHct { get; }
    public Variant Variant { get; }
    public bool IsDark { get; }
    public Platform PlatformType { get; }
    public double ContrastLevel { get; }
    public SpecVersion SpecVersion { get; }

    public TonalPalette PrimaryPalette { get; }
    public TonalPalette SecondaryPalette { get; }
    public TonalPalette TertiaryPalette { get; }
    public TonalPalette NeutralPalette { get; }
    public TonalPalette NeutralVariantPalette { get; }
    public TonalPalette ErrorPalette { get; }

    public DynamicScheme()
    {
        throw new NotImplementedException();
    }

    public DynamicScheme(
        Hct sourceColorHct,
        Variant variant,
        bool isDark,
        double contrastLevel,
        TonalPalette primaryPalette,
        TonalPalette secondaryPalette,
        TonalPalette tertiaryPalette,
        TonalPalette neutralPalette,
        TonalPalette neutralVariantPalette)
        : this(
            sourceColorHct,
            variant,
            isDark,
            contrastLevel,
            Platform.PHONE,
            SpecVersion.SPEC_2021,
            primaryPalette,
            secondaryPalette,
            tertiaryPalette,
            neutralPalette,
            neutralVariantPalette,
            null)
    {
    }

    public DynamicScheme(
        Hct sourceColorHct,
        Variant variant,
        bool isDark,
        double contrastLevel,
        TonalPalette primaryPalette,
        TonalPalette secondaryPalette,
        TonalPalette tertiaryPalette,
        TonalPalette neutralPalette,
        TonalPalette neutralVariantPalette,
        TonalPalette? errorPalette)
        : this(
            sourceColorHct,
            variant,
            isDark,
            contrastLevel,
            Platform.PHONE,
            SpecVersion.SPEC_2021,
            primaryPalette,
            secondaryPalette,
            tertiaryPalette,
            neutralPalette,
            neutralVariantPalette,
            errorPalette)
    {
    }

    public DynamicScheme(
        Hct sourceColorHct,
        Variant variant,
        bool isDark,
        double contrastLevel,
        Platform platform,
        SpecVersion specVersion,
        TonalPalette primaryPalette,
        TonalPalette secondaryPalette,
        TonalPalette tertiaryPalette,
        TonalPalette neutralPalette,
        TonalPalette neutralVariantPalette,
        TonalPalette? errorPalette)
    {
        SourceColorArgb = sourceColorHct.Argb;
        SourceColorHct = sourceColorHct;
        Variant = variant;
        IsDark = isDark;
        ContrastLevel = contrastLevel;
        PlatformType = platform;
        SpecVersion = specVersion;

        PrimaryPalette = primaryPalette;
        SecondaryPalette = secondaryPalette;
        TertiaryPalette = tertiaryPalette;
        NeutralPalette = neutralPalette;
        NeutralVariantPalette = neutralVariantPalette;
        ErrorPalette = errorPalette ?? TonalPalette.FromHueAndChroma(25.0, 84.0);
    }

    public static DynamicScheme From(DynamicScheme other, bool isDark)
    {
        return From(other, isDark, other.ContrastLevel);
    }

    public static DynamicScheme From(DynamicScheme other, bool isDark, double contrastLevel)
    {
        return new DynamicScheme(
            other.SourceColorHct,
            other.Variant,
            isDark,
            contrastLevel,
            other.PlatformType,
            other.SpecVersion,
            other.PrimaryPalette,
            other.SecondaryPalette,
            other.TertiaryPalette,
            other.NeutralPalette,
            other.NeutralVariantPalette,
            other.ErrorPalette);
    }

    public static double GetPiecewiseValue(Hct sourceColorHct, double[] hueBreakpoints, double[] hues)
    {
        var size = Min(hueBreakpoints.Length - 1, hues.Length);
        var sourceHue = sourceColorHct.Hue;
        for (var i = 0; i < size; i++)
        {
            if (sourceHue >= hueBreakpoints[i] && sourceHue < hueBreakpoints[i + 1])
            {
                return MathUtils.SanitizeDegreesDouble(hues[i]);
            }
        }
        // No condition matched, return the source value.
        return sourceHue;
    }

    public static double GetRotatedHue(Hct sourceColorHct, double[] hueBreakpoints, double[] rotations)
    {
        var rotation = GetPiecewiseValue(sourceColorHct, hueBreakpoints, rotations);
        if (Min(hueBreakpoints.Length - 1, rotations.Length) <= 0)
        {
            rotation = 0;
        }
        return MathUtils.SanitizeDegreesDouble(sourceColorHct.Hue + rotation);
    }

    public Hct GetHct(DynamicColor dynamicColor)
    {
        return dynamicColor.GetHct(this);
    }

    public int GetArgb(DynamicColor dynamicColor)
    {
        return dynamicColor.GetArgb(this);
    }

    public override string ToString()
    {
        var mode = IsDark ? "dark" : "light";
        return $"Scheme: variant={Variant}, mode={mode}, platform={PlatformType.ToString().ToLowerInvariant()}, contrastLevel={ContrastLevel:0.0}, seed={SourceColorHct}, specVersion={SpecVersion}";
    }
}