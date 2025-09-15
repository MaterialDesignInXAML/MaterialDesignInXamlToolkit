using static MaterialColorUtilities.MathUtils;

namespace MaterialColorUtilities;

public record ContrastCurve(double Low, double Normal, double Medium, double High)
{
    public double Get(double contrastLevel)
    {
        return contrastLevel switch
        {
            <= -1.0 => Low,
            < 0.0 => Lerp(Low, Normal, (contrastLevel - -1.0) / 1.0),
            < 0.5 => Lerp(Normal, Medium, (contrastLevel - 0.0) / 0.5),
            < 1.0 => Lerp(Medium, High, (contrastLevel - 0.5) / 0.5),
            _ => High
        };
    }
}