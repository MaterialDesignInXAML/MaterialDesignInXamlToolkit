#if NET462
namespace System
{
    internal static class MathCompat
    {
        /// <summary>
        /// Returns the cube root of a specified number.
        /// </summary>
        /// <remarks>
        /// Correctly handles negatives (real cube root), NaN, infinities, and signed zero.
        /// </remarks>
        public static double Cbrt(double x)
        {
            if (double.IsNaN(x) || double.IsInfinity(x) || x == 0) return x;
            return x < 0 ? -Math.Pow(-x, 1.0 / 3.0) : Math.Pow(x, 1.0 / 3.0);
        }
    }
}
#endif
