using static System.Math;

namespace MaterialColorUtilities;

public static class QuantizerWsMeans
{
    private sealed class Distance : IComparable<Distance>
    {
        public int Index;
        public double D;
        public int CompareTo(Distance? other)
        {
            if (other == null) return 1;
            return D.CompareTo(other.D);
        }
    }

    private const int MAX_ITERATIONS = 10;
    private const double MIN_MOVEMENT_DISTANCE = 3.0;

    public static Dictionary<int, int> Quantize(int[] inputPixels, int[] startingClusters, int maxColors)
    {
        var random = new Random(0x42688);

        var pixelToCount = new Dictionary<int, int>();
        var points = new double[inputPixels.Length][];
        var pixels = new int[inputPixels.Length];
        PointProvider pointProvider = new PointProviderLab();

        int pointCount = 0;
        for (int i = 0; i < inputPixels.Length; i++)
        {
            int inputPixel = inputPixels[i];
            if (!pixelToCount.TryGetValue(inputPixel, out var pixelCount))
            {
                points[pointCount] = pointProvider.FromInt(inputPixel);
                pixels[pointCount] = inputPixel;
                pointCount++;
                pixelToCount[inputPixel] = 1;
            }
            else
            {
                pixelToCount[inputPixel] = pixelCount + 1;
            }
        }

        var counts = new int[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            int pixel = pixels[i];
            counts[i] = pixelToCount[pixel];
        }

        int clusterCount = Min(maxColors, pointCount);
        if (startingClusters.Length != 0)
        {
            clusterCount = Min(clusterCount, startingClusters.Length);
        }

        var clusters = new double[clusterCount][];
        int clustersCreated = 0;
        for (int i = 0; i < startingClusters.Length && i < clusterCount; i++)
        {
            clusters[i] = pointProvider.FromInt(startingClusters[i]);
            clustersCreated++;
        }

        int additionalClustersNeeded = clusterCount - clustersCreated;
        if (startingClusters.Length == 0 && additionalClustersNeeded > 0)
        {
            for (int i = 0; i < additionalClustersNeeded; i++)
            {
                // Random Lab within typical ranges: L [0,100], a [-100,100], b [-100,100]
                double l = random.NextDouble() * 100.0;
                double a = random.NextDouble() * 200.0 - 100.0;
                double b = random.NextDouble() * 200.0 - 100.0;
                clusters[clustersCreated + i] = [l, a, b];
            }
        }

        var clusterIndices = new int[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            clusterIndices[i] = random.Next(clusterCount);
        }

        var indexMatrix = new int[clusterCount][];
        for (int i = 0; i < clusterCount; i++) indexMatrix[i] = new int[clusterCount];

        var distanceToIndexMatrix = new Distance[clusterCount][];
        for (int i = 0; i < clusterCount; i++)
        {
            distanceToIndexMatrix[i] = new Distance[clusterCount];
            for (int j = 0; j < clusterCount; j++)
            {
                distanceToIndexMatrix[i][j] = new Distance();
            }
        }

        var pixelCountSums = new int[clusterCount];
        for (int iteration = 0; iteration < MAX_ITERATIONS; iteration++)
        {
            // Update cluster pair distances and sorted indices
            for (int i = 0; i < clusterCount; i++)
            {
                for (int j = i + 1; j < clusterCount; j++)
                {
                    double distance = pointProvider.Distance(clusters[i], clusters[j]);
                    distanceToIndexMatrix[j][i].D = distance;
                    distanceToIndexMatrix[j][i].Index = i;
                    distanceToIndexMatrix[i][j].D = distance;
                    distanceToIndexMatrix[i][j].Index = j;
                }
                Array.Sort(distanceToIndexMatrix[i]);
                for (int j = 0; j < clusterCount; j++)
                {
                    indexMatrix[i][j] = distanceToIndexMatrix[i][j].Index;
                }
            }

            int pointsMoved = 0;
            for (int i = 0; i < pointCount; i++)
            {
                var point = points[i];
                int previousClusterIndex = clusterIndices[i];
                var previousCluster = clusters[previousClusterIndex];
                double previousDistance = pointProvider.Distance(point, previousCluster);

                double minimumDistance = previousDistance;
                int newClusterIndex = -1;
                for (int j = 0; j < clusterCount; j++)
                {
                    if (distanceToIndexMatrix[previousClusterIndex][j].D >= 4 * previousDistance)
                    {
                        continue;
                    }
                    double distance = pointProvider.Distance(point, clusters[j]);
                    if (distance < minimumDistance)
                    {
                        minimumDistance = distance;
                        newClusterIndex = j;
                    }
                }
                if (newClusterIndex != -1)
                {
                    double distanceChange = Abs(Sqrt(minimumDistance) - Sqrt(previousDistance));
                    if (distanceChange > MIN_MOVEMENT_DISTANCE)
                    {
                        pointsMoved++;
                        clusterIndices[i] = newClusterIndex;
                    }
                }
            }

            if (pointsMoved == 0 && iteration != 0)
            {
                break;
            }

            var componentASums = new double[clusterCount];
            var componentBSums = new double[clusterCount];
            var componentCSums = new double[clusterCount];
            pixelCountSums.AsSpan().Fill(0);
            for (int i = 0; i < pointCount; i++)
            {
                int clusterIndex = clusterIndices[i];
                var point = points[i];
                int count = counts[i];
                pixelCountSums[clusterIndex] += count;
                componentASums[clusterIndex] += point[0] * count;
                componentBSums[clusterIndex] += point[1] * count;
                componentCSums[clusterIndex] += point[2] * count;
            }

            for (int i = 0; i < clusterCount; i++)
            {
                int count = pixelCountSums[i];
                if (count == 0)
                {
                    clusters[i] = [0.0, 0.0, 0.0];
                    continue;
                }
                double a = componentASums[i] / count;
                double b = componentBSums[i] / count;
                double c = componentCSums[i] / count;
                clusters[i][0] = a;
                clusters[i][1] = b;
                clusters[i][2] = c;
            }
        }

        var argbToPopulation = new Dictionary<int, int>();
        for (int i = 0; i < clusterCount; i++)
        {
            int count = pixelCountSums[i];
            if (count == 0) continue;
            int possibleNewCluster = pointProvider.ToInt(clusters[i]);
            if (argbToPopulation.ContainsKey(possibleNewCluster)) continue;
            argbToPopulation[possibleNewCluster] = count;
        }

        return argbToPopulation;
    }
}
