namespace MaterialDesignThemes.Wpf
{
    [Obsolete("Use MaterialDesignThemes.Wpf.Theming.ThemeManager")]
    public interface IThemeManager
    {
        event EventHandler<ThemeChangedEventArgs>? ThemeChanged;
    }
}
