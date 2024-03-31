using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

public readonly record struct ColorReference(ThemeColorReference ThemeReference, Color? Color)
{
    public static ColorReference SecondaryLight { get; } = new ColorReference(ThemeColorReference.SecondaryLight, null);
    public static ColorReference SecondaryMid { get; } = new ColorReference(ThemeColorReference.SecondaryMid, null);
    public static ColorReference SecondaryDark { get; } = new ColorReference(ThemeColorReference.SecondaryDark, null);
    public static ColorReference PrimaryLight { get; } = new ColorReference(ThemeColorReference.PrimaryLight, null);
    public static ColorReference PrimaryMid { get; } = new ColorReference(ThemeColorReference.PrimaryMid, null);
    public static ColorReference PrimaryDark { get; } = new ColorReference(ThemeColorReference.PrimaryDark, null);

    public static implicit operator ColorReference(Color color) => new(ThemeColorReference.None, color);
    public static implicit operator ColorReference(ThemeColorReference @ref) => new(@ref, null);
    public static implicit operator Color(ColorReference color) => color.Color ??
        throw new InvalidOperationException($"{nameof(ColorReference)} does not contain any color");
}
