using ControlzEx.Theming;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using ControlzExTheme = ControlzEx.Theming.Theme;
using Theme = MaterialDesignThemes.Wpf.Theme;

namespace MaterialDesignThemes.MahApps;

public class MahAppsBundledTheme : BundledTheme
{
    private static Guid GeneratedKey { get; } = Guid.NewGuid();

    protected override void ApplyTheme(Theme theme)
    {
        base.ApplyTheme(theme);
        if (TryGetResourceDictionaries(theme, out ResourceDictionary? light, out ResourceDictionary? dark))
        {
            switch (BaseTheme)
            {
                case Wpf.BaseTheme.Light:
                    MergedDictionaries.Add(light);
                    break;
                case Wpf.BaseTheme.Dark:
                    MergedDictionaries.Add(dark);
                    break;
                case Wpf.BaseTheme.Inherit:
                    switch (Theme.GetSystemTheme())
                    {
                        case Wpf.BaseTheme.Dark:
                            MergedDictionaries.Add(dark);
                            break;
                        default:
                            MergedDictionaries.Add(light);
                            break;
                    }
                    break;
            }

            if (this.GetThemeManager() is IThemeManager themeManager)
            {
                themeManager.ThemeChanged += ThemeManagerOnThemeChanged;
            }
        }
    }

    private bool TryGetResourceDictionaries(Theme theme, out ResourceDictionary? light, out ResourceDictionary? dark)
    {
        if (PrimaryColor is PrimaryColor primaryColor &&
            SecondaryColor is SecondaryColor secondaryColor &&
            BaseTheme is BaseTheme)
        {
            light = GetResourceDictionary(theme, primaryColor, secondaryColor, Wpf.BaseTheme.Light);
            dark = GetResourceDictionary(theme, primaryColor, secondaryColor, Wpf.BaseTheme.Dark);
            return true;
        }
        else
        {
            light = null;
            dark = null;
            return false;
        }

        static ResourceDictionary GetResourceDictionary(Theme theme, PrimaryColor primaryColor, SecondaryColor secondaryColor, BaseTheme baseTheme)
        {
            string baseColorScheme = baseTheme.GetMahAppsBaseColorScheme();
            string colorScheme = $"MaterialDesign.{primaryColor}.{secondaryColor}";
            ResourceDictionary rv;
            if (ThemeManager.Current.Themes.FirstOrDefault(x => x.BaseColorScheme == baseColorScheme && x.ColorScheme == primaryColor.ToString()) is ControlzExTheme mahAppsTheme)
            {
                rv = mahAppsTheme.Resources;
                rv.SetMahApps(theme, baseTheme);
                return rv;
            }

            rv = new ResourceDictionary();
            rv[GeneratedKey] = GeneratedKey;
            rv.SetMahApps(theme, baseTheme);

            string themeName = $"MaterialDesign.{primaryColor}.{secondaryColor}.{baseColorScheme}";
            string displayName = $"Material Design {primaryColor} with {secondaryColor}";
            rv[ControlzExTheme.ThemeNameKey] = themeName;
            rv[ControlzExTheme.ThemeDisplayNameKey] = displayName;
            rv[ControlzExTheme.ThemeColorSchemeKey] = colorScheme;
            rv[ControlzExTheme.ThemeBaseColorSchemeKey] = baseColorScheme;
            var themeInstance = new ControlzExTheme(new LibraryTheme(rv, null));
            rv[ControlzExTheme.ThemeInstanceKey] = themeInstance;
            ThemeManager.Current.AddTheme(themeInstance);

            return rv;
        }
    }

    private void ThemeManagerOnThemeChanged(object? sender, Wpf.ThemeChangedEventArgs e)
    {
        ResourceDictionary resourceDictionary = e.ResourceDictionary;

        Theme newTheme = e.NewTheme;

        BaseTheme baseTheme = newTheme.GetBaseTheme();

        if (TryGetResourceDictionaries(newTheme, out ResourceDictionary? light, out ResourceDictionary? dark))
        {
            for (int i = resourceDictionary.MergedDictionaries.Count - 1; i >= 0; i--)
            {
                var dictionary = resourceDictionary.MergedDictionaries[i];
                if (dictionary.Keys.Cast<object>().OfType<Guid>().Any(x => x == GeneratedKey))
                {
                    resourceDictionary.MergedDictionaries.RemoveAt(i);
                }
            }
            switch (baseTheme)
            {
                case Wpf.BaseTheme.Light:
                    resourceDictionary.MergedDictionaries.Add(light);
                    break;
                case Wpf.BaseTheme.Dark:
                    resourceDictionary.MergedDictionaries.Add(dark);
                    break;
            }
        }
    }
}
