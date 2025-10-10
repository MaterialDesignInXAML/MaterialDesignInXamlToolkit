namespace MaterialColorUtilities;

/// <summary>
/// Given a large set of colors, remove unsuitable colors and rank the rest based on suitability.
/// </summary>
public static class Score
{
    private const double TargetChroma = 48.0;
    private const double WeightProportion = 0.7;
    private const double WeightChromaAbove = 0.3;
    private const double WeightChromaBelow = 0.1;
    private const double CutoffChroma = 5.0;
    private const double CutoffExcitedProportion = 0.01;

    private const int GoogleBlue = unchecked((int)0xff4285f4);

    /// <summary>
    /// Given a map with keys of colors and values of how often the color appears, rank the colors
    /// based on suitability for being used for a UI theme.
    /// </summary>
    /// <param name="colorsToPopulation">map with keys of colors and values of how often the color appears.</param>
    /// <param name="desired">max count of colors to be returned in the list.</param>
    /// <param name="fallbackColorArgb">color to be returned if no other options available.</param>
    /// <param name="filter">whether to filter out undesireable combinations.</param>
    /// <returns>Colors sorted by suitability for a UI theme.</returns>
    public static IEnumerable<int> ScoreColors(
        Dictionary<int, int> colorsToPopulation,
        int desired = 4,
        int fallbackColorArgb = GoogleBlue,
        bool filter = true)
    {
        // Get the HCT color for each Argb value, while finding the per hue count and total count.
        List<Hct> colorsHct = [];
        int[] huePopulation = new int[360];
        double populationSum = 0.0;
        foreach (var entry in colorsToPopulation)
        {
            var hct = Hct.FromInt(entry.Key);
            colorsHct.Add(hct);
            int hue = (int)Math.Floor(hct.Hue);
            huePopulation[hue] += entry.Value;
            populationSum += entry.Value;
        }

        // Hues with more usage in neighboring 30 degree slice get a larger number.
        double[] hueExcitedProportions = new double[360];
        for (int hue = 0; hue < 360; hue++)
        {
            double proportion = populationSum == 0.0 ? 0.0 : huePopulation[hue] / populationSum;
            for (int i = hue - 14; i < hue + 16; i++)
            {
                int neighborHue = MathUtils.SanitizeDegreesInt(i);
                hueExcitedProportions[neighborHue] += proportion;
            }
        }

        // Scores each HCT color based on usage and chroma, while optionally filtering out
        // values that do not have enough chroma or usage.
        List<ScoredHct> scoredHcts = [];
        foreach (var hct in colorsHct)
        {
            int hue = MathUtils.SanitizeDegreesInt((int)Math.Round(hct.Hue));
            double proportion = hueExcitedProportions[hue];
            if (filter && (hct.Chroma < CutoffChroma || proportion <= CutoffExcitedProportion))
            {
                continue;
            }

            double proportionScore = proportion * 100.0 * WeightProportion;
            double chromaWeight = hct.Chroma < TargetChroma ? WeightChromaBelow : WeightChromaAbove;
            double chromaScore = (hct.Chroma - TargetChroma) * chromaWeight;
            double score = proportionScore + chromaScore;
            scoredHcts.Add(new ScoredHct(hct, score));
        }

        // Sorted so that colors with higher scores come first.
        scoredHcts.Sort(new ScoredComparator());

        // Iterates through potential hue differences in degrees in order to select the colors with
        // the largest distribution of hues possible. Starting at 90 degrees (maximum difference for
        // 4 colors) then decreasing down to a 15 degree minimum.
        List<Hct> chosenColors = [];
        for (int differenceDegrees = 90; differenceDegrees >= 15; differenceDegrees--)
        {
            chosenColors.Clear();
            foreach (var entry in scoredHcts)
            {
                var hct = entry.Hct;
                bool hasDuplicateHue = false;
                foreach (var chosenHct in chosenColors)
                {
                    if (MathUtils.DifferenceDegrees(hct.Hue, chosenHct.Hue) < differenceDegrees)
                    {
                        hasDuplicateHue = true;
                        break;
                    }
                }
                if (!hasDuplicateHue)
                {
                    chosenColors.Add(hct);
                }
                if (chosenColors.Count >= desired)
                {
                    break;
                }
            }
            if (chosenColors.Count >= desired)
            {
                break;
            }
        }

        return chosenColors.Count == 0 ? [fallbackColorArgb] : chosenColors.Select(c => c.Argb);
    }

    private record ScoredHct(Hct Hct, double Score);

    private sealed class ScoredComparator : IComparer<ScoredHct>
    {
        public int Compare(ScoredHct? x, ScoredHct? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return 1;
            if (y == null) return -1;
            // Descending by score
            return Comparer<double>.Default.Compare(y.Score, x.Score);
        }
    }
}