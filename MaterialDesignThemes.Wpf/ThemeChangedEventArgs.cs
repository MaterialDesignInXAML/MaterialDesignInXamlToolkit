namespace MaterialDesignThemes.Wpf;

public class ThemeChangedEventArgs : EventArgs
{
    public ThemeChangedEventArgs(ResourceDictionary resourceDictionary, Theme oldTheme, Theme newTheme)
    {
        ResourceDictionary = resourceDictionary;
        OldTheme = oldTheme;
        NewTheme = newTheme;
    }

    public ResourceDictionary ResourceDictionary { get; }
    public Theme NewTheme { get; }
    public Theme OldTheme { get; }
}
