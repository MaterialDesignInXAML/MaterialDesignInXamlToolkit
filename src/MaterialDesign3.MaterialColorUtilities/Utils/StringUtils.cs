namespace MaterialColorUtilities;

/// <summary>
/// Utility methods for string representations of colors.
/// </summary>
public static class StringUtils
{
    /// <summary>
    /// Hex string representing color, ex. #ff0000 for red.
    /// </summary>
    public static string HexFromArgb(int argb)
    {
        int red = ColorUtils.RedFromArgb(argb);
        int green = ColorUtils.GreenFromArgb(argb);
        int blue = ColorUtils.BlueFromArgb(argb);
        return $"#{red:X2}{green:X2}{blue:X2}".ToLowerInvariant();
    }
}