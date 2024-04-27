namespace MaterialDesignThemes.Wpf;

public interface IThemeManager
{
    event EventHandler<ThemeChangedEventArgs>? ThemeChanged;
}
