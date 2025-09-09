using static System.Math;

namespace MaterialColorUtilities;

public sealed class DynamicColor
{
    public readonly string name;
    public readonly Func<DynamicScheme, TonalPalette> palette;
    public readonly Func<DynamicScheme, double> tone;
    public readonly bool isBackground;
    public readonly Func<DynamicScheme, double>? chromaMultiplier;
    public readonly Func<DynamicScheme, DynamicColor>? background;
    public readonly Func<DynamicScheme, DynamicColor>? secondBackground;
    public readonly Func<DynamicScheme, ContrastCurve>? contrastCurve;
    public readonly Func<DynamicScheme, ToneDeltaPair>? toneDeltaPair;
    public readonly Func<DynamicScheme, double>? opacity;

    private readonly Dictionary<DynamicScheme, Hct> hctCache = new();

    public DynamicColor(
        string name,
        Func<DynamicScheme, TonalPalette> palette,
        Func<DynamicScheme, double> tone,
        bool isBackground,
        Func<DynamicScheme, DynamicColor>? background,
        Func<DynamicScheme, DynamicColor>? secondBackground,
        ContrastCurve? contrastCurve,
        Func<DynamicScheme, ToneDeltaPair>? toneDeltaPair)
        : this(
            name,
            palette,
            tone,
            isBackground,
            null,
            background,
            secondBackground,
            contrastCurve == null ? null : _ => contrastCurve,
            toneDeltaPair,
            null)
    {
    }

    public DynamicColor(
        string name,
        Func<DynamicScheme, TonalPalette> palette,
        Func<DynamicScheme, double> tone,
        bool isBackground,
        Func<DynamicScheme, DynamicColor>? background,
        Func<DynamicScheme, DynamicColor>? secondBackground,
        ContrastCurve? contrastCurve,
        Func<DynamicScheme, ToneDeltaPair>? toneDeltaPair,
        Func<DynamicScheme, double>? opacity)
        : this(
            name,
            palette,
            tone,
            isBackground,
            null,
            background,
            secondBackground,
            contrastCurve == null ? null : _ => contrastCurve,
            toneDeltaPair,
            opacity)
    {
    }

    public DynamicColor(
        string name,
        Func<DynamicScheme, TonalPalette> palette,
        Func<DynamicScheme, double> tone,
        bool isBackground,
        Func<DynamicScheme, double>? chromaMultiplier,
        Func<DynamicScheme, DynamicColor>? background,
        Func<DynamicScheme, DynamicColor>? secondBackground,
        Func<DynamicScheme, ContrastCurve>? contrastCurve,
        Func<DynamicScheme, ToneDeltaPair>? toneDeltaPair,
        Func<DynamicScheme, double>? opacity)
    {
        this.name = name;
        this.palette = palette;
        this.tone = tone;
        this.isBackground = isBackground;
        this.chromaMultiplier = chromaMultiplier;
        this.background = background;
        this.secondBackground = secondBackground;
        this.contrastCurve = contrastCurve;
        this.toneDeltaPair = toneDeltaPair;
        this.opacity = opacity;
    }

    public static DynamicColor FromPalette(
        string name,
        Func<DynamicScheme, TonalPalette> palette,
        Func<DynamicScheme, double> tone)
    {
        return new DynamicColor(
            name,
            palette,
            tone,
            false,
            null,
            null,
            null,
            null);
    }

    public static DynamicColor FromPalette(
        string name,
        Func<DynamicScheme, TonalPalette> palette,
        Func<DynamicScheme, double> tone,
        bool isBackground)
    {
        return new DynamicColor(
            name,
            palette,
            tone,
            isBackground,
            null,
            null,
            null,
            null);
    }

    public static DynamicColor FromArgb(string name, int argb)
    {
        var hct = Hct.FromInt(argb);
        var palette = TonalPalette.FromInt(argb);
        return FromPalette(name, _ => palette, _ => hct.Tone);
    }

    public int GetArgb(DynamicScheme scheme)
    {
        var argb = GetHct(scheme).Argb;
        if (opacity == null)
        {
            return argb;
        }
        var percentage = opacity(scheme);
        var alpha = MathUtils.ClampInt(0, 255, (int)Round(percentage * 255.0));
        return (argb & 0x00ffffff) | (alpha << 24);
    }

    public Hct GetHct(DynamicScheme scheme)
    {
        if (hctCache.TryGetValue(scheme, out var cached))
        {
            return cached;
        }
        var answer = ColorSpecs.Get(scheme.SpecVersion).GetHct(scheme, this);
        if (hctCache.Count > 4)
        {
            hctCache.Clear();
        }
        hctCache[scheme] = answer;
        return answer;
    }

    public double GetTone(DynamicScheme scheme)
    {
        return ColorSpecs.Get(scheme.SpecVersion).GetTone(scheme, this);
    }

    public static double ForegroundTone(double bgTone, double ratio)
    {
        var lighterTone = Contrast.LighterUnsafe(bgTone, ratio);
        var darkerTone = Contrast.DarkerUnsafe(bgTone, ratio);
        var lighterRatio = Contrast.RatioOfTones(lighterTone, bgTone);
        var darkerRatio = Contrast.RatioOfTones(darkerTone, bgTone);
        var preferLighter = TonePrefersLightForeground(bgTone);

        if (preferLighter)
        {
            var negligibleDifference =
                Abs(lighterRatio - darkerRatio) < 0.1 && lighterRatio < ratio && darkerRatio < ratio;
            if (lighterRatio >= ratio || lighterRatio >= darkerRatio || negligibleDifference)
            {
                return lighterTone;
            }
            else
            {
                return darkerTone;
            }
        }
        else
        {
            return (darkerRatio >= ratio || darkerRatio >= lighterRatio) ? darkerTone : lighterTone;
        }
    }

    public static double EnableLightForeground(double tone)
    {
        if (TonePrefersLightForeground(tone) && !ToneAllowsLightForeground(tone))
        {
            return 49.0;
        }
        return tone;
    }

    public static bool TonePrefersLightForeground(double tone)
    {
        return Round(tone) < 60.0;
    }

    public static bool ToneAllowsLightForeground(double tone)
    {
        return Round(tone) <= 49.0;
    }

    public static Func<DynamicScheme, double> GetInitialToneFromBackground(
        Func<DynamicScheme, DynamicColor?>? background)
    {
        if (background == null)
        {
            return _ => 50.0;
        }

        return s =>
        {
            var bg = background(s);
            return bg?.GetTone(s) ?? 50.0;
        };
    }

    public Builder ToBuilder()
    {
        return new Builder()
            .SetName(name)
            .SetPalette(palette)
            .SetTone(tone)
            .SetIsBackground(isBackground)
            .SetChromaMultiplier(chromaMultiplier ?? (_ => 1.0))
            .SetBackground(background ?? (_ => null!))
            .SetSecondBackground(secondBackground ?? (_ => null!))
            .SetContrastCurve(contrastCurve ?? (_ => null!))
            .SetToneDeltaPair(toneDeltaPair ?? (_ => null!))
            .SetOpacity(opacity ?? (_ => 1.0));
    }

    public sealed class Builder
    {
        private string? _name;
        private Func<DynamicScheme, TonalPalette>? _palette;
        private Func<DynamicScheme, double>? _tone;
        private bool _isBackground;
        private Func<DynamicScheme, double>? _chromaMultiplier;
        private Func<DynamicScheme, DynamicColor>? _background;
        private Func<DynamicScheme, DynamicColor>? _secondBackground;
        private Func<DynamicScheme, ContrastCurve>? _contrastCurve;
        private Func<DynamicScheme, ToneDeltaPair>? _toneDeltaPair;
        private Func<DynamicScheme, double>? _opacity;

        public Builder SetName(string name) { _name = name; return this; }
        public Builder SetPalette(Func<DynamicScheme, TonalPalette> palette) { _palette = palette; return this; }
        public Builder SetTone(Func<DynamicScheme, double> tone) { _tone = tone; return this; }
        public Builder SetIsBackground(bool isBackground) { _isBackground = isBackground; return this; }
        public Builder SetChromaMultiplier(Func<DynamicScheme, double> chromaMultiplier) { _chromaMultiplier = chromaMultiplier; return this; }
        public Builder SetBackground(Func<DynamicScheme, DynamicColor> background) { _background = background; return this; }
        public Builder SetSecondBackground(Func<DynamicScheme, DynamicColor> secondBackground) { _secondBackground = secondBackground; return this; }
        public Builder SetContrastCurve(Func<DynamicScheme, ContrastCurve> contrastCurve) { _contrastCurve = contrastCurve; return this; }
        public Builder SetToneDeltaPair(Func<DynamicScheme, ToneDeltaPair> toneDeltaPair) { _toneDeltaPair = toneDeltaPair; return this; }
        public Builder SetOpacity(Func<DynamicScheme, double> opacity) { _opacity = opacity; return this; }

        public Builder ExtendSpecVersion(SpecVersion specVersion, DynamicColor extendedColor)
        {
            ValidateExtendedColor(specVersion, extendedColor);

            var prevName = _name;
            var prevIsBg = _isBackground;
            var prevPalette = _palette;
            var prevTone = _tone;
            var prevChromaMultiplier = _chromaMultiplier;
            var prevBackground = _background;
            var prevSecondBackground = _secondBackground;
            var prevContrastCurve = _contrastCurve;
            var prevToneDeltaPair = _toneDeltaPair;
            var prevOpacity = _opacity;

            return new Builder()
                .SetName(prevName!)
                .SetIsBackground(prevIsBg)
                .SetPalette(s => (s.SpecVersion == specVersion ? extendedColor.palette : prevPalette)!.Invoke(s))
                .SetTone(s => (s.SpecVersion == specVersion ? extendedColor.tone : prevTone ?? GetInitialToneFromBackground(prevBackground!))(s))
                .SetChromaMultiplier(s => (s.SpecVersion == specVersion ? extendedColor.chromaMultiplier : prevChromaMultiplier)?.Invoke(s) ?? 1.0)
                .SetBackground(s => (s.SpecVersion == specVersion ? extendedColor.background : prevBackground)?.Invoke(s)!)
                .SetSecondBackground(s => (s.SpecVersion == specVersion ? extendedColor.secondBackground : prevSecondBackground)?.Invoke(s)!)
                .SetContrastCurve(s => (s.SpecVersion == specVersion ? extendedColor.contrastCurve : prevContrastCurve)?.Invoke(s)!)
                .SetToneDeltaPair(s => (s.SpecVersion == specVersion ? extendedColor.toneDeltaPair : prevToneDeltaPair)?.Invoke(s)!)
                .SetOpacity(s => (s.SpecVersion == specVersion ? extendedColor.opacity : prevOpacity)?.Invoke(s) ?? 1.0);
        }

        public DynamicColor Build()
        {
            if (_background == null && _secondBackground != null)
            {
                throw new ArgumentException($"Color {_name} has secondBackground defined, but background is not defined.");
            }
            if (_background == null && _contrastCurve != null)
            {
                throw new ArgumentException($"Color {_name} has contrastCurve defined, but background is not defined.");
            }
            if (_background != null && _contrastCurve == null)
            {
                throw new ArgumentException($"Color {_name} has background defined, but contrastCurve is not defined.");
            }

            var finalTone = _tone ?? GetInitialToneFromBackground(_background);
            return new DynamicColor(
                _name!,
                _palette!,
                finalTone!,
                _isBackground,
                _chromaMultiplier,
                _background,
                _secondBackground,
                _contrastCurve,
                _toneDeltaPair,
                _opacity);
        }

        private void ValidateExtendedColor(SpecVersion specVersion, DynamicColor extendedColor)
        {
            if (!string.Equals(_name, extendedColor.name, StringComparison.Ordinal))
            {
                throw new ArgumentException($"Attempting to extend color {_name} with color {extendedColor.name} of different name for spec version {specVersion}.");
            }
            if (_isBackground != extendedColor.isBackground)
            {
                throw new ArgumentException($"Attempting to extend color {_name} as a {(_isBackground ? "background" : "foreground")} with color {extendedColor.name} as a {(extendedColor.isBackground ? "background" : "foreground")} for spec version {specVersion}.");
            }
        }
    }
}