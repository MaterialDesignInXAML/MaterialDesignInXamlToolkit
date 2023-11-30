using System.Diagnostics;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignColors;

[DebuggerDisplay($"Color: {{{nameof(Color)}}}; Foreground: {{{nameof(ForegroundColor)}}}")]
public readonly struct ColorPair(Color color, Color? foregroundColor)
{
    public static implicit operator ColorPair(Color color) => new(color, null);

    public Color Color => color;

    public Color? ForegroundColor => foregroundColor;

    public ColorPair(Color color)
        : this(color, null)
    { }

    public Color GetForegroundColor() => ForegroundColor ?? Color.ContrastingForegroundColor();
}
