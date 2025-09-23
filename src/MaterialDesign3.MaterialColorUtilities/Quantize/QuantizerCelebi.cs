namespace MaterialColorUtilities;

public sealed class QuantizerCelebi
{
    private QuantizerCelebi() { }

    public static Dictionary<int, int> Quantize(int[] pixels, int maxColors)
    {
        var wu = new QuantizerWu();
        var wuResult = wu.Quantize(pixels, maxColors);

        int[] wuClusters = new int[wuResult.ColorToCount.Count];
        int index = 0;
        foreach (var kvp in wuResult.ColorToCount)
        {
            wuClusters[index++] = kvp.Key;
        }

        return QuantizerWsMeans.Quantize(pixels, wuClusters, maxColors);
    }
}