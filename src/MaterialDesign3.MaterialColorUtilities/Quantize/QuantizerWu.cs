namespace MaterialColorUtilities;

public sealed class QuantizerWu: Quantizer
{
    private int[] weights = default!;
    private int[] momentsR = default!;
    private int[] momentsG = default!;
    private int[] momentsB = default!;
    private double[] moments = default!;
    private Box[] cubes = default!;

    // 5 bits per channel histogram size.
    private const int INDEX_BITS = 5;
    private const int INDEX_COUNT = (1 << INDEX_BITS) + 1; // 33
    private const int TOTAL_SIZE = INDEX_COUNT * INDEX_COUNT * INDEX_COUNT; // 35937

    public QuantizerResult Quantize(int[] pixels, int colorCount)
    {
        var mapResult = new QuantizerMap().Quantize(pixels, colorCount);
        ConstructHistogram(mapResult.ColorToCount);
        CreateMoments();
        var createBoxesResult = CreateBoxes(colorCount);
        var colors = CreateResult(createBoxesResult.resultCount);
        var resultMap = new Dictionary<int, int>();
        foreach (int color in colors)
        {
            resultMap[color] = 0;
        }
        return new QuantizerResult(resultMap);
    }

    private static int GetIndex(int r, int g, int b) => (r << (INDEX_BITS * 2)) + (r << (INDEX_BITS + 1)) + r + (g << INDEX_BITS) + g + b;

    private void ConstructHistogram(Dictionary<int, int> pixels)
    {
        weights = new int[TOTAL_SIZE];
        momentsR = new int[TOTAL_SIZE];
        momentsG = new int[TOTAL_SIZE];
        momentsB = new int[TOTAL_SIZE];
        moments = new double[TOTAL_SIZE];

        foreach (var pair in pixels)
        {
            int pixel = pair.Key;
            int count = pair.Value;
            int red = ColorUtils.RedFromArgb(pixel);
            int green = ColorUtils.GreenFromArgb(pixel);
            int blue = ColorUtils.BlueFromArgb(pixel);
            int bitsToRemove = 8 - INDEX_BITS;
            int iR = (red >> bitsToRemove) + 1;
            int iG = (green >> bitsToRemove) + 1;
            int iB = (blue >> bitsToRemove) + 1;
            int index = GetIndex(iR, iG, iB);
            weights[index] += count;
            momentsR[index] += red * count;
            momentsG[index] += green * count;
            momentsB[index] += blue * count;
            moments[index] += count * ((red * red) + (green * green) + (blue * blue));
        }
    }

    private void CreateMoments()
    {
        for (int r = 1; r < INDEX_COUNT; ++r)
        {
            int[] area = new int[INDEX_COUNT];
            int[] areaR = new int[INDEX_COUNT];
            int[] areaG = new int[INDEX_COUNT];
            int[] areaB = new int[INDEX_COUNT];
            double[] area2 = new double[INDEX_COUNT];

            for (int g = 1; g < INDEX_COUNT; ++g)
            {
                int line = 0;
                int lineR = 0;
                int lineG = 0;
                int lineB = 0;
                double line2 = 0.0;
                for (int b = 1; b < INDEX_COUNT; ++b)
                {
                    int index = GetIndex(r, g, b);
                    line += weights[index];
                    lineR += momentsR[index];
                    lineG += momentsG[index];
                    lineB += momentsB[index];
                    line2 += moments[index];

                    area[b] += line;
                    areaR[b] += lineR;
                    areaG[b] += lineG;
                    areaB[b] += lineB;
                    area2[b] += line2;

                    int previousIndex = GetIndex(r - 1, g, b);
                    weights[index] = weights[previousIndex] + area[b];
                    momentsR[index] = momentsR[previousIndex] + areaR[b];
                    momentsG[index] = momentsG[previousIndex] + areaG[b];
                    momentsB[index] = momentsB[previousIndex] + areaB[b];
                    moments[index] = moments[previousIndex] + area2[b];
                }
            }
        }
    }

    private CreateBoxesResult CreateBoxes(int maxColorCount)
    {
        cubes = new Box[maxColorCount];
        for (int i = 0; i < maxColorCount; i++) cubes[i] = new Box();
        double[] volumeVariance = new double[maxColorCount];
        var firstBox = cubes[0];
        firstBox.r1 = INDEX_COUNT - 1;
        firstBox.g1 = INDEX_COUNT - 1;
        firstBox.b1 = INDEX_COUNT - 1;

        int generatedColorCount = maxColorCount;
        int next = 0;
        for (int i = 1; i < maxColorCount; i++)
        {
            if (Cut(cubes[next], cubes[i]))
            {
                volumeVariance[next] = (cubes[next].vol > 1) ? Variance(cubes[next]) : 0.0;
                volumeVariance[i] = (cubes[i].vol > 1) ? Variance(cubes[i]) : 0.0;
            }
            else
            {
                volumeVariance[next] = 0.0;
                i--;
            }

            next = 0;
            double temp = volumeVariance[0];
            for (int j = 1; j <= i; j++)
            {
                if (volumeVariance[j] > temp)
                {
                    temp = volumeVariance[j];
                    next = j;
                }
            }
            if (temp <= 0.0)
            {
                generatedColorCount = i + 1;
                break;
            }
        }

        return new CreateBoxesResult(maxColorCount, generatedColorCount);
    }

    private List<int> CreateResult(int colorCount)
    {
        var colors = new List<int>();
        for (int i = 0; i < colorCount; ++i)
        {
            var cube = cubes[i];
            int weight = Volume(cube, weights);
            if (weight > 0)
            {
                int r = Volume(cube, momentsR) / weight;
                int g = Volume(cube, momentsG) / weight;
                int b = Volume(cube, momentsB) / weight;
                int color = (255 << 24) | ((r & 0x0ff) << 16) | ((g & 0x0ff) << 8) | (b & 0x0ff);
                colors.Add(color);
            }
        }
        return colors;
    }

    private double Variance(Box cube)
    {
        int dr = Volume(cube, momentsR);
        int dg = Volume(cube, momentsG);
        int db = Volume(cube, momentsB);
        double xx =
            moments[GetIndex(cube.r1, cube.g1, cube.b1)]
            - moments[GetIndex(cube.r1, cube.g1, cube.b0)]
            - moments[GetIndex(cube.r1, cube.g0, cube.b1)]
            + moments[GetIndex(cube.r1, cube.g0, cube.b0)]
            - moments[GetIndex(cube.r0, cube.g1, cube.b1)]
            + moments[GetIndex(cube.r0, cube.g1, cube.b0)]
            + moments[GetIndex(cube.r0, cube.g0, cube.b1)]
            - moments[GetIndex(cube.r0, cube.g0, cube.b0)];

        int hypotenuse = dr * dr + dg * dg + db * db;
        int volume = Volume(cube, weights);
        return xx - hypotenuse / (double)volume;
    }

    private bool Cut(Box one, Box two)
    {
        int wholeR = Volume(one, momentsR);
        int wholeG = Volume(one, momentsG);
        int wholeB = Volume(one, momentsB);
        int wholeW = Volume(one, weights);

        var maxRResult = Maximize(one, Direction.RED, one.r0 + 1, one.r1, wholeR, wholeG, wholeB, wholeW);
        var maxGResult = Maximize(one, Direction.GREEN, one.g0 + 1, one.g1, wholeR, wholeG, wholeB, wholeW);
        var maxBResult = Maximize(one, Direction.BLUE, one.b0 + 1, one.b1, wholeR, wholeG, wholeB, wholeW);
        Direction cutDirection;
        double maxR = maxRResult.maximum;
        double maxG = maxGResult.maximum;
        double maxB = maxBResult.maximum;
        if (maxR >= maxG && maxR >= maxB)
        {
            if (maxRResult.cutLocation < 0)
            {
                return false;
            }
            cutDirection = Direction.RED;
        }
        else if (maxG >= maxR && maxG >= maxB)
        {
            cutDirection = Direction.GREEN;
        }
        else
        {
            cutDirection = Direction.BLUE;
        }

        two.r1 = one.r1;
        two.g1 = one.g1;
        two.b1 = one.b1;

        switch (cutDirection)
        {
            case Direction.RED:
                one.r1 = maxRResult.cutLocation;
                two.r0 = one.r1;
                two.g0 = one.g0;
                two.b0 = one.b0;
                break;
            case Direction.GREEN:
                one.g1 = maxGResult.cutLocation;
                two.r0 = one.r0;
                two.g0 = one.g1;
                two.b0 = one.b0;
                break;
            case Direction.BLUE:
                one.b1 = maxBResult.cutLocation;
                two.r0 = one.r0;
                two.g0 = one.g0;
                two.b0 = one.b1;
                break;
        }

        one.vol = (one.r1 - one.r0) * (one.g1 - one.g0) * (one.b1 - one.b0);
        two.vol = (two.r1 - two.r0) * (two.g1 - two.g0) * (two.b1 - two.b0);

        return true;
    }

    private MaximizeResult Maximize(
        Box cube,
        Direction direction,
        int first,
        int last,
        int wholeR,
        int wholeG,
        int wholeB,
        int wholeW)
    {
        int bottomR = Bottom(cube, direction, momentsR);
        int bottomG = Bottom(cube, direction, momentsG);
        int bottomB = Bottom(cube, direction, momentsB);
        int bottomW = Bottom(cube, direction, weights);

        double max = 0.0;
        int cut = -1;

        int halfR = 0;
        int halfG = 0;
        int halfB = 0;
        int halfW = 0;
        for (int i = first; i < last; i++)
        {
            halfR = bottomR + Top(cube, direction, i, momentsR);
            halfG = bottomG + Top(cube, direction, i, momentsG);
            halfB = bottomB + Top(cube, direction, i, momentsB);
            halfW = bottomW + Top(cube, direction, i, weights);
            if (halfW == 0) continue;

            double tempNumerator = (double)halfR * halfR + (double)halfG * halfG + (double)halfB * halfB;
            double tempDenominator = halfW;
            double temp = tempNumerator / tempDenominator;

            halfR = wholeR - halfR;
            halfG = wholeG - halfG;
            halfB = wholeB - halfB;
            halfW = wholeW - halfW;
            if (halfW == 0) continue;

            tempNumerator = (double)halfR * halfR + (double)halfG * halfG + (double)halfB * halfB;
            tempDenominator = halfW;
            temp += tempNumerator / tempDenominator;

            if (temp > max)
            {
                max = temp;
                cut = i;
            }
        }
        return new MaximizeResult(cut, max);
    }

    private static int Volume(Box cube, int[] moment)
    {
        return (moment[GetIndex(cube.r1, cube.g1, cube.b1)]
            - moment[GetIndex(cube.r1, cube.g1, cube.b0)]
            - moment[GetIndex(cube.r1, cube.g0, cube.b1)]
            + moment[GetIndex(cube.r1, cube.g0, cube.b0)]
            - moment[GetIndex(cube.r0, cube.g1, cube.b1)]
            + moment[GetIndex(cube.r0, cube.g1, cube.b0)]
            + moment[GetIndex(cube.r0, cube.g0, cube.b1)]
            - moment[GetIndex(cube.r0, cube.g0, cube.b0)]);
    }

    private static int Bottom(Box cube, Direction direction, int[] moment)
    {
        return direction switch
        {
            Direction.RED =>
                -moment[GetIndex(cube.r0, cube.g1, cube.b1)]
                + moment[GetIndex(cube.r0, cube.g1, cube.b0)]
                + moment[GetIndex(cube.r0, cube.g0, cube.b1)]
                - moment[GetIndex(cube.r0, cube.g0, cube.b0)],
            Direction.GREEN =>
                -moment[GetIndex(cube.r1, cube.g0, cube.b1)]
                + moment[GetIndex(cube.r1, cube.g0, cube.b0)]
                + moment[GetIndex(cube.r0, cube.g0, cube.b1)]
                - moment[GetIndex(cube.r0, cube.g0, cube.b0)],
            _ =>
                -moment[GetIndex(cube.r1, cube.g1, cube.b0)]
                + moment[GetIndex(cube.r1, cube.g0, cube.b0)]
                + moment[GetIndex(cube.r0, cube.g1, cube.b0)]
                - moment[GetIndex(cube.r0, cube.g0, cube.b0)],
        };
    }

    private static int Top(Box cube, Direction direction, int position, int[] moment)
    {
        return direction switch
        {
            Direction.RED =>
                moment[GetIndex(position, cube.g1, cube.b1)]
                - moment[GetIndex(position, cube.g1, cube.b0)]
                - moment[GetIndex(position, cube.g0, cube.b1)]
                + moment[GetIndex(position, cube.g0, cube.b0)],
            Direction.GREEN =>
                moment[GetIndex(cube.r1, position, cube.b1)]
                - moment[GetIndex(cube.r1, position, cube.b0)]
                - moment[GetIndex(cube.r0, position, cube.b1)]
                + moment[GetIndex(cube.r0, position, cube.b0)],
            _ =>
                moment[GetIndex(cube.r1, cube.g1, position)]
                - moment[GetIndex(cube.r1, cube.g0, position)]
                - moment[GetIndex(cube.r0, cube.g1, position)]
                + moment[GetIndex(cube.r0, cube.g0, position)],
        };
    }

    private enum Direction { RED, GREEN, BLUE }

    private sealed class MaximizeResult
    {
        public int cutLocation;
        public double maximum;
        public MaximizeResult(int cut, double max)
        {
            cutLocation = cut;
            maximum = max;
        }
    }

    private sealed class CreateBoxesResult
    {
        public int requestedCount;
        public int resultCount;
        public CreateBoxesResult(int requestedCount, int resultCount)
        {
            this.requestedCount = requestedCount;
            this.resultCount = resultCount;
        }
    }

    private sealed class Box
    {
        public int r0 = 0;
        public int r1 = 0;
        public int g0 = 0;
        public int g1 = 0;
        public int b0 = 0;
        public int b1 = 0;
        public int vol = 0;
    }
}