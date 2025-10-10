using static System.Math;
using static MaterialColorUtilities.MathUtils;

namespace MaterialColorUtilities;

/// <summary>
/// Design utilities using color temperature theory: analogous colors, complementary color, and a cache.
/// </summary>
public sealed class TemperatureCache
{
    private readonly Hct input;
    private Hct? precomputedComplement;
    private List<Hct>? precomputedHctsByTemp;
    private List<Hct>? precomputedHctsByHue;
    private Dictionary<Hct, double>? precomputedTempsByHct;

    /// <summary>
    /// Create a cache that allows calculation of complementary and analogous colors.
    /// Any returned colors will have the same tone and chroma as the input (subject to gamut limits).
    /// </summary>
    public TemperatureCache(Hct input) => this.input = input;

    /// <summary>
    /// A color that complements the input color aesthetically.
    /// </summary>
    public Hct GetComplement()
    {
        if (precomputedComplement != null)
        {
            return precomputedComplement;
        }

        double coldestHue = GetColdest().Hue;
        double coldestTemp = GetTempsByHct()[GetColdest()];

        double warmestHue = GetWarmest().Hue;
        double warmestTemp = GetTempsByHct()[GetWarmest()];
        double range = warmestTemp - coldestTemp;
        bool startHueIsColdestToWarmest = IsBetween(input.Hue, coldestHue, warmestHue);
        double startHue = startHueIsColdestToWarmest ? warmestHue : coldestHue;
        double endHue = startHueIsColdestToWarmest ? coldestHue : warmestHue;
        double directionOfRotation = 1.0;
        double smallestError = 1000.0;
        var answer = GetHctsByHue()[(int)Round(input.Hue)];

        double complementRelativeTemp = (1.0 - GetRelativeTemperature(input));
        for (double hueAddend = 0.0; hueAddend <= 360.0; hueAddend += 1.0)
        {
            double hue = SanitizeDegreesDouble(startHue + directionOfRotation * hueAddend);
            if (!IsBetween(hue, startHue, endHue))
            {
                continue;
            }
            var possibleAnswer = GetHctsByHue()[(int)Round(hue)];
            double relativeTemp = (GetTempsByHct()[possibleAnswer] - coldestTemp) / range;
            double error = Abs(complementRelativeTemp - relativeTemp);
            if (error < smallestError)
            {
                smallestError = error;
                answer = possibleAnswer;
            }
        }
        precomputedComplement = answer;
        return precomputedComplement;
    }

    /// <summary>
    /// 5 colors that pair well with the input color (equidistant in temperature and adjacent in hue).
    /// </summary>
    public List<Hct> GetAnalogousColors() => GetAnalogousColors(5, 12);

    /// <summary>
    /// A set of colors with differing hues, equidistant in temperature.
    /// </summary>
    /// <param name="count">The number of colors to return, includes the input color.</param>
    /// <param name="divisions">The number of divisions on the color wheel.</param>
    public List<Hct> GetAnalogousColors(int count, int divisions)
    {
        int startHue = (int)Round(input.Hue);
        var startHct = GetHctsByHue()[startHue];
        double lastTemp = GetRelativeTemperature(startHct);

        List<Hct> allColors =
        [
            startHct
        ];

        double absoluteTotalTempDelta = 0.0;
        for (int i = 0; i < 360; i++)
        {
            int hue = SanitizeDegreesInt(startHue + i);
            var hct = GetHctsByHue()[hue];
            double temp = GetRelativeTemperature(hct);
            double tempDelta = Abs(temp - lastTemp);
            lastTemp = temp;
            absoluteTotalTempDelta += tempDelta;
        }

        int hueAddend = 1;
        double tempStep = absoluteTotalTempDelta / (double)divisions;
        double totalTempDelta = 0.0;
        lastTemp = GetRelativeTemperature(startHct);
        while (allColors.Count < divisions)
        {
            int hue = SanitizeDegreesInt(startHue + hueAddend);
            var hct = GetHctsByHue()[hue];
            double temp = GetRelativeTemperature(hct);
            double tempDelta = Abs(temp - lastTemp);
            totalTempDelta += tempDelta;

            double desiredTotalTempDeltaForIndex = (allColors.Count * tempStep);
            bool indexSatisfied = totalTempDelta >= desiredTotalTempDeltaForIndex;
            int indexAddend = 1;
            while (indexSatisfied && allColors.Count < divisions)
            {
                allColors.Add(hct);
                desiredTotalTempDeltaForIndex = ((allColors.Count + indexAddend) * tempStep);
                indexSatisfied = totalTempDelta >= desiredTotalTempDeltaForIndex;
                indexAddend++;
            }
            lastTemp = temp;
            hueAddend++;

            if (hueAddend > 360)
            {
                while (allColors.Count < divisions)
                {
                    allColors.Add(hct);
                }
                break;
            }
        }

        List<Hct> answers =
        [
            input
        ];

        int ccwCount = (int)Floor(((double)count - 1.0) / 2.0);
        for (int i = 1; i < (ccwCount + 1); i++)
        {
            int index = 0 - i;
            while (index < 0)
            {
                index = allColors.Count + index;
            }
            if (index >= allColors.Count)
            {
                index %= allColors.Count;
            }
            answers.Insert(0, allColors[index]);
        }

        int cwCount = count - ccwCount - 1;
        for (int i = 1; i < (cwCount + 1); i++)
        {
            int index = i;
            while (index < 0)
            {
                index = allColors.Count + index;
            }
            if (index >= allColors.Count)
            {
                index %= allColors.Count;
            }
            answers.Add(allColors[index]);
        }

        return answers;
    }

    /// <summary>
    /// Temperature relative to all colors with the same chroma and tone.
    /// Value from 0 to 1.
    /// </summary>
    public double GetRelativeTemperature(Hct hct)
    {
        double range = GetTempsByHct()[GetWarmest()] - GetTempsByHct()[GetColdest()];
        double differenceFromColdest = GetTempsByHct()[hct] - GetTempsByHct()[GetColdest()];
        if (range == 0.0)
        {
            return 0.5;
        }
        return differenceFromColdest / range;
    }

    /// <summary>
    /// Value representing cool-warm factor of a color. Values below 0 are cool, above are warm.
    /// Implementation of Ou, Woodcock and Wright's algorithm in Lab/LCH.
    /// </summary>
    public static double RawTemperature(Hct color)
    {
        double[] lab = ColorUtils.LabFromArgb(color.Argb);
        double hue = SanitizeDegreesDouble(Atan2(lab[2], lab[1]) * 180.0 / PI);
        double chroma = Sqrt(lab[1] * lab[1] + lab[2] * lab[2]);
        return -0.5 + 0.02 * Pow(chroma, 1.07) * Cos((SanitizeDegreesDouble(hue - 50.0)) * PI / 180.0);
    }

    /// <summary>
    /// Coldest color with same chroma and tone as input.
    /// </summary>
    private Hct GetColdest() => GetHctsByTemp()[0];

    /// <summary>
    /// HCTs for all colors with the same chroma/tone as the input, sorted by hue index (0..360).
    /// </summary>
    private List<Hct> GetHctsByHue()
    {
        if (precomputedHctsByHue != null)
        {
            return precomputedHctsByHue;
        }
        List<Hct> hcts = [];
        for (double hue = 0.0; hue <= 360.0; hue += 1.0)
        {
            var colorAtHue = Hct.From(hue, input.Chroma, input.Tone);
            hcts.Add(colorAtHue);
        }
        precomputedHctsByHue = hcts;
        return precomputedHctsByHue;
    }

    /// <summary>
    /// HCTs for all colors with the same chroma/tone as the input, sorted from coldest to warmest.
    /// </summary>
    private List<Hct> GetHctsByTemp()
    {
        if (precomputedHctsByTemp != null)
        {
            return precomputedHctsByTemp;
        }

        List<Hct> hcts =
        [
            ..GetHctsByHue(),
            input
        ];
        hcts.Sort((a, b) => GetTempsByHct()[a].CompareTo(GetTempsByHct()[b]));
        precomputedHctsByTemp = hcts;
        return precomputedHctsByTemp;
    }

    /// <summary>
    /// Map of HCT to raw temperature for all hues at the input chroma/tone and the input itself.
    /// </summary>
    private Dictionary<Hct, double> GetTempsByHct()
    {
        if (precomputedTempsByHct != null)
        {
            return precomputedTempsByHct;
        }

        List<Hct> allHcts =
        [
            ..GetHctsByHue(),
            input
        ];

        Dictionary<Hct, double> temperaturesByHct = new();
        foreach (var hct in allHcts)
        {
            temperaturesByHct[hct] = RawTemperature(hct);
        }

        precomputedTempsByHct = temperaturesByHct;
        return precomputedTempsByHct;
    }

    /// <summary>
    /// Warmest color with same chroma and tone as input.
    /// </summary>
    private Hct GetWarmest()
    {
        var list = GetHctsByTemp();
        return list[^1];
    }

    /// <summary>
    /// Determines if an angle is between two other angles, rotating clockwise.
    /// </summary>
    private static bool IsBetween(double angle, double a, double b)
    {
        if (a < b)
        {
            return a <= angle && angle <= b;
        }
        return a <= angle || angle <= b;
    }
}