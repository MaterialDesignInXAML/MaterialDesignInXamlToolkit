namespace MaterialDesignThemes.Wpf;

public class PaletteHelper
{
    public virtual Theme GetTheme()
    {
        if (Application.Current is null)
            throw new InvalidOperationException($"Cannot get theme outside of a WPF application. Use {nameof(ResourceDictionaryExtensions)}.{nameof(ResourceDictionaryExtensions.GetTheme)} on the appropriate resource dictionary instead.");
        return GetResourceDictionary().GetTheme();
    }

    public virtual void SetTheme(Theme theme)
    {
        if (theme is null) throw new ArgumentNullException(nameof(theme));
        if (Application.Current is null)
            throw new InvalidOperationException($"Cannot set theme outside of a WPF application. Use {nameof(ResourceDictionaryExtensions)}.{nameof(ResourceDictionaryExtensions.SetTheme)} on the appropriate resource dictionary instead.");

        GetResourceDictionary().SetTheme(theme);
        RecreateThemeDictionaries();
    }

    public virtual IThemeManager? GetThemeManager()
    {
        if (Application.Current is null)
            throw new InvalidOperationException($"Cannot get ThemeManager outside of a WPF application. Use {nameof(ResourceDictionaryExtensions)}.{nameof(ResourceDictionaryExtensions.GetThemeManager)} on the appropriate resource dictionary instead.");
        return GetResourceDictionary().GetThemeManager();
    }

    private static ResourceDictionary GetResourceDictionary()
        => Application.Current.Resources.MergedDictionaries.FirstOrDefault(x => x is IMaterialDesignThemeDictionary) ??
            Application.Current.Resources;

    /// <summary>
    /// Removes and readds resource dictionaries with static resource that will be re-evaluated with new theme brushes.
    /// This is primarily here for the obsolete theme brushes.
    /// </summary>
    private static void RecreateThemeDictionaries()
    {
        ResourceDictionary root = Application.Current.Resources;
        for (int i = 0; i < root.MergedDictionaries.Count; i++)
        {
            ResourceDictionary dictionary = root.MergedDictionaries[i];
            if (dictionary["MaterialDesign.Resources.RecreateOnThemeChange"] is bool recreateOnThemeChange && recreateOnThemeChange)
            {
                root.MergedDictionaries.RemoveAt(i);
                root.MergedDictionaries.Insert(i, new ResourceDictionary()
                {
                    Source = dictionary.Source
                });
            }
        }
    }
}
