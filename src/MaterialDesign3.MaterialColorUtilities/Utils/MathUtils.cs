namespace MaterialColorUtilities;

/// <summary>
/// Utility methods for mathematical operations.
/// </summary>
public static class MathUtils
{
    /// <summary>
    /// The signum function.
    /// </summary>
    public static int Signum(double num)
    {
        if (num < 0) return -1;
        if (num == 0) return 0;
        return 1;
    }

    /// <summary>
    /// Linear interpolation between start and stop by amount.
    /// </summary>
    public static double Lerp(double start, double stop, double amount) => (1.0 - amount) * start + amount * stop;

    /// <summary>
    /// Clamp an int between min and max (inclusive).
    /// </summary>
    public static int ClampInt(int min, int max, int input)
    {
        if (input < min) return min;
        if (input > max) return max;
        return input;
    }

    /// <summary>
    /// Clamp a double between min and max (inclusive).
    /// </summary>
    public static double ClampDouble(double min, double max, double input)
    {
        if (input < min) return min;
        if (input > max) return max;
        return input;
    }

    /// <summary>
    /// Sanitize degrees to [0,360).
    /// </summary>
    public static int SanitizeDegreesInt(int degrees)
    {
        degrees %= 360;
        if (degrees < 0) degrees += 360;
        return degrees;
    }

    /// <summary>
    /// Sanitize degrees to [0.0,360.0).
    /// </summary>
    public static double SanitizeDegreesDouble(double degrees)
    {
        degrees %= 360.0;
        if (degrees < 0) degrees += 360.0;
        return degrees;
    }

    /// <summary>
    /// Sign of shortest rotation from 'from' to 'to' in degrees.
    /// </summary>
    public static double RotationDirection(double from, double to)
    {
        var increasingDifference = SanitizeDegreesDouble(to - from);
        return increasingDifference <= 180.0 ? 1.0 : -1.0;
    }

    /// <summary>
    /// Shortest arc distance between two angles in degrees.
    /// </summary>
    public static double DifferenceDegrees(double a, double b) => 180.0 - Math.Abs(Math.Abs(a - b) - 180.0);

    /// <summary>
    /// Multiply a 1x3 row vector by a 3x3 matrix.
    /// </summary>
    public static double[] MatrixMultiply(double[] row, double[][] matrix)
    {
        var a = row[0] * matrix[0][0] + row[1] * matrix[0][1] + row[2] * matrix[0][2];
        var b = row[0] * matrix[1][0] + row[1] * matrix[1][1] + row[2] * matrix[1][2];
        var c = row[0] * matrix[2][0] + row[1] * matrix[2][1] + row[2] * matrix[2][2];
        return [a, b, c];
    }

    // --- Missing math helpers for cross-platform parity with Java/modern .NET ---

    /// <summary>
    /// sqrt(x^2 + y^2) computed in a numerically stable way.
    /// </summary>
    public static double Hypot(double x, double y)
    {
        x = Math.Abs(x);
        y = Math.Abs(y);
        var max = Math.Max(x, y);
        var min = Math.Min(x, y);
        if (max == 0) return 0;
        var r = min / max;
        return max * Math.Sqrt(1 + r * r);
    }

    /// <summary>
    /// e^x - 1, with better precision for small x.
    /// </summary>
    public static double Expm1(double x)
    {
        if (x == 0.0) return 0.0;
        var ax = Math.Abs(x);
        if (ax < 1e-5)
        {
            // Series expansion: x + x^2/2 + x^3/6
            return x + 0.5 * x * x + (x * x * x) / 6.0;
        }
        return Math.Exp(x) - 1.0;
    }

    /// <summary>
    /// ln(1 + x), with better precision for small x.
    /// </summary>
    public static double Log1p(double x)
    {
        if (x <= -1.0)
        {
            // Domain error maps to -Infinity like Java's log1p for x == -1 and NaN for x < -1.
            return x == -1.0 ? double.NegativeInfinity : double.NaN;
        }
        var ax = Math.Abs(x);
        if (ax < 1e-5)
        {
            // Series expansion: x - x^2/2 + x^3/3
            return x - 0.5 * x * x + (x * x * x) / 3.0;
        }
        return Math.Log(1.0 + x);
    }

    /// <summary>
    /// Cube root that supports negative inputs.
    /// </summary>
    public static double Cbrt(double x) => x < 0 ? -Math.Pow(-x, 1.0 / 3.0) : Math.Pow(x, 1.0 / 3.0);

    public static double Clamp(double min, double max, double value)
        => value < min ? min : (value > max ? max : value);
}
