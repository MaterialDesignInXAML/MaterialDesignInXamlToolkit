﻿using System.Diagnostics;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignColors;

[DebuggerDisplay($"Color: {{{nameof(Color)}}}; Foreground: {{{nameof(ForegroundColor)}}}")]
public struct ColorPair
{
    public Color Color { get; set; }

    /// <summary>
    /// The foreground or opposite color. If left null, this will be calculated for you.
    /// Calculated by calling ColorAssist.ContrastingForegroundColor()
    /// </summary>
    public Color? ForegroundColor { get; set; }

    public static implicit operator ColorPair(Color color) => new ColorPair(color);

    public ColorPair(Color color)
    {
        Color = color;
        ForegroundColor = null;
    }

    public ColorPair(Color color, Color? foreground)
    {
        Color = color;
        ForegroundColor = foreground;
    }

    public Color GetForegroundColor() => ForegroundColor ?? Color.ContrastingForegroundColor();
}
