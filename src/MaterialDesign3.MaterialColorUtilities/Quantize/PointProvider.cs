namespace MaterialColorUtilities;

public interface PointProvider
{
    // The three components (L*, a*, b* or similar) from an ARGB color.
    double[] FromInt(int argb);

    // Convert a point in the color space back to ARGB.
    int ToInt(double[] point);

    // Squared distance between two points in the color space.
    double Distance(double[] a, double[] b);
}