using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

public enum Severity
{
    None,
    Info,
    Success,
    Warning,
    Error
}

/// <summary>
/// Maps a <see cref="Severity"/> to its semantic theme brush.
/// </summary>
public static class SeverityBrushProvider
{
    /// <summary>
    /// The theme resource key of the brush for <paramref name="severity"/>; null for <see cref="Severity.None"/>.
    /// </summary>
    public static string? GetResourceKey(Severity severity) => severity switch
    {
        Severity.Info => "MaterialDesign.Brush.Info",
        Severity.Success => "MaterialDesign.Brush.Success",
        Severity.Warning => "MaterialDesign.Brush.Warning",
        Severity.Error => "MaterialDesign.Brush.Error",
        _ => null
    };

    /// <summary>
    /// The current application theme's brush for <paramref name="severity"/>; null for <see cref="Severity.None"/> or when not found.
    /// The theme brushes are replaced on theme change, so look it up again (or use a DynamicResource with <see cref="GetResourceKey"/>) instead of caching it.
    /// </summary>
    public static SolidColorBrush? GetBrush(Severity severity)
        => GetResourceKey(severity) is { } key ? Application.Current?.TryFindResource(key) as SolidColorBrush : null;
}
