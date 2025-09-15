using static System.Math;

namespace MaterialColorUtilities;

/// <summary>
/// A convenience class for retrieving colors that are constant in hue and chroma, but vary in tone.
/// TonalPalette is intended for use in a single thread due to its stateful caching.
/// </summary>
public sealed class TonalPalette
{
    private readonly Dictionary<int, int> _cache = new();
    private readonly Hct _keyColor;
    private readonly double _hue;
    private readonly double _chroma;

    /// <summary>
    /// Create tones using the HCT hue and chroma from a color.
    /// </summary>
    public static TonalPalette FromInt(int argb) => FromHct(Hct.FromInt(argb));

    /// <summary>
    /// Create tones using a HCT color.
    /// </summary>
    public static TonalPalette FromHct(Hct hct) => new(hct.Hue, hct.Chroma, hct);

    /// <summary>
    /// Create tones from a defined HCT hue and chroma.
    /// </summary>
    public static TonalPalette FromHueAndChroma(double hue, double chroma)
    {
        var keyColor = new KeyColor(hue, chroma).Create();
        return new TonalPalette(hue, chroma, keyColor);
    }

    private TonalPalette(double hue, double chroma, Hct keyColor)
    {
        _hue = hue;
        _chroma = chroma;
        _keyColor = keyColor;
    }

    /// <summary>
    /// Create an ARGB color with HCT hue and chroma of this palette, and the provided HCT tone (0-100).
    /// </summary>
    public int Tone(int tone)
    {
        if (_cache.TryGetValue(tone, out var cached))
        {
            return cached;
        }
        int color;
        if (tone == 99 && Hct.IsYellow(_hue))
        {
            color = AverageArgb(Tone(98), Tone(100));
        }
        else
        {
            color = Hct.From(_hue, _chroma, tone).Argb;
        }
        _cache[tone] = color;
        return color;
    }

    /// <summary>
    /// Given a tone, use hue and chroma of palette to create a color, and return it as HCT.
    /// </summary>
    public Hct GetHct(double tone) => Hct.From(_hue, _chroma, tone);

    /// <summary> The chroma of the Tonal Palette, in HCT. </summary>
    public double GetChroma() => _chroma;

    /// <summary> The hue of the Tonal Palette, in HCT. </summary>
    public double GetHue() => _hue;

    /// <summary> The key color is the first tone, starting from T50, that matches the palette's chroma. </summary>
    public Hct GetKeyColor() => _keyColor;

    private static int AverageArgb(int argb1, int argb2)
    {
        var red1 = (argb1 >> 16) & 0xff;
        var green1 = (argb1 >> 8) & 0xff;
        var blue1 = argb1 & 0xff;
        var red2 = (argb2 >> 16) & 0xff;
        var green2 = (argb2 >> 8) & 0xff;
        var blue2 = argb2 & 0xff;
        var red = (int)Round((red1 + red2) / 2.0);
        var green = (int)Round((green1 + green2) / 2.0);
        var blue = (int)Round((blue1 + blue2) / 2.0);
        return (255 << 24) | ((red & 255) << 16) | ((green & 255) << 8) | (blue & 255);
    }

    /// <summary>
    /// Key color is a color that represents the hue and chroma of a tonal palette.
    /// </summary>
    private sealed class KeyColor
    {
        private readonly double _hue;
        private readonly double _requestedChroma;
        private readonly Dictionary<int, double> _chromaCache = new();
        private const double MAX_CHROMA_VALUE = 200.0;

        public KeyColor(double hue, double requestedChroma)
        {
            _hue = hue;
            _requestedChroma = requestedChroma;
        }

        public Hct Create()
        {
            const int pivotTone = 50;
            const int toneStepSize = 1;
            const double epsilon = 0.01;

            var lowerTone = 0;
            var upperTone = 100;
            while (lowerTone < upperTone)
            {
                var midTone = (lowerTone + upperTone) / 2;
                var isAscending = MaxChroma(midTone) < MaxChroma(midTone + toneStepSize);
                var sufficientChroma = MaxChroma(midTone) >= _requestedChroma - epsilon;

                if (sufficientChroma)
                {
                    if (Abs(lowerTone - pivotTone) < Abs(upperTone - pivotTone))
                    {
                        upperTone = midTone;
                    }
                    else
                    {
                        if (lowerTone == midTone)
                        {
                            return Hct.From(_hue, _requestedChroma, lowerTone);
                        }
                        lowerTone = midTone;
                    }
                }
                else
                {
                    if (isAscending)
                    {
                        lowerTone = midTone + toneStepSize;
                    }
                    else
                    {
                        upperTone = midTone;
                    }
                }
            }
            return Hct.From(_hue, _requestedChroma, lowerTone);
        }

        private double MaxChroma(int tone)
        {
            if (!_chromaCache.TryGetValue(tone, out var cached))
            {
                var newChroma = Hct.From(_hue, MAX_CHROMA_VALUE, tone).Chroma;
                _chromaCache[tone] = newChroma;
                return newChroma;
            }
            return cached;
        }
    }
}