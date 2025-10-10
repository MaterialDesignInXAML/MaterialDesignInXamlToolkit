namespace MaterialColorUtilities;

public sealed class QuantizerMap: Quantizer
{
    private Dictionary<int, int> ColorToCount { get; } = new();

    public QuantizerResult Quantize(int[] pixels, int colorCount)
    {
        foreach (int pixel in pixels)
        {
            ColorToCount.TryGetValue(pixel, out int current);
            ColorToCount[pixel] = current + 1;
        }

        return new QuantizerResult(ColorToCount);
    }
}