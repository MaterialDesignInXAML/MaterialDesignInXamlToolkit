using System.Windows.Media;
using System.Windows.Media.Animation;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf;

public static partial class ResourceDictionaryExtensions
{
    private const string CurrentThemeKey = nameof(MaterialDesignThemes) + "." + nameof(CurrentThemeKey);
    private const string ThemeManagerKey = nameof(MaterialDesignThemes) + "." + nameof(ThemeManagerKey);

    public static void SetTheme(this ResourceDictionary resourceDictionary, Theme theme)
    {
        if (resourceDictionary is null) throw new ArgumentNullException(nameof(resourceDictionary));
        if (theme is null) throw new ArgumentNullException(nameof(theme));

        Color primaryLight = theme.PrimaryLight.Color;
        Color primaryMid = theme.PrimaryMid.Color;
        Color primaryDark = theme.PrimaryDark.Color;

        Color secondaryLight = theme.SecondaryLight.Color;
        Color secondaryMid = theme.SecondaryMid.Color;
        Color secondaryDark = theme.SecondaryDark.Color;

        if (theme.ColorAdjustment is { } colorAdjustment)
        {
            if (colorAdjustment.Colors.HasFlag(ColorSelection.Primary))
            {
                AdjustColors(theme.Background, colorAdjustment, ref primaryLight, ref primaryMid, ref primaryDark);
            }
            if (colorAdjustment.Colors.HasFlag(ColorSelection.Secondary))
            {
                AdjustColors(theme.Background, colorAdjustment, ref secondaryLight, ref secondaryMid, ref secondaryDark);
            }
        }

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Primary.Light", primaryLight);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Primary.Light.Foreground", theme.PrimaryLight.ForegroundColor ?? primaryLight.ContrastingForegroundColor());
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Primary", primaryMid);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Primary.Foreground", theme.PrimaryMid.ForegroundColor ?? primaryMid.ContrastingForegroundColor());
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Primary.Dark", primaryDark);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Primary.Dark.Foreground", theme.PrimaryDark.ForegroundColor ?? primaryDark.ContrastingForegroundColor());

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Secondary.Light", secondaryLight);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Secondary.Light.Foreground", theme.SecondaryLight.ForegroundColor ?? secondaryLight.ContrastingForegroundColor());
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Secondary", secondaryMid);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Secondary.Foreground", theme.SecondaryMid.ForegroundColor ?? secondaryMid.ContrastingForegroundColor());
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Secondary.Dark", secondaryDark);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Secondary.Dark.Foreground", theme.SecondaryDark.ForegroundColor ?? secondaryDark.ContrastingForegroundColor());

        ApplyThemeColors(resourceDictionary, theme);

        if (resourceDictionary.GetThemeManager() is not ThemeManager themeManager)
        {
            resourceDictionary[ThemeManagerKey] = themeManager = new ThemeManager(resourceDictionary);
        }
        Theme oldTheme = resourceDictionary.GetTheme();
        resourceDictionary[CurrentThemeKey] = theme;

        themeManager.OnThemeChange(oldTheme, theme);
    }
    private static partial void ApplyThemeColors(ResourceDictionary resourceDictionary, Theme theme);

    public static void SetObsoleteBrushes(this ResourceDictionary resourceDictionary, Theme theme)
    {
        if (resourceDictionary is null) throw new ArgumentNullException(nameof(resourceDictionary));
        if (theme is null) throw new ArgumentNullException(nameof(theme));

        foreach (object? key in resourceDictionary.Keys)
        {
            if (resourceDictionary[key] is MaterialDesignColors.StaticResourceExtension staticResource)
            {
                resourceDictionary[key] = new MaterialDesignColors.StaticResourceExtension(staticResource.ResourceKey);
            }
        }
    }


    public static Theme GetTheme(this ResourceDictionary resourceDictionary)
    {
        if (resourceDictionary is null) throw new ArgumentNullException(nameof(resourceDictionary));
        if (resourceDictionary[CurrentThemeKey] is Theme theme)
        {
            return theme;
        }

        theme = new Theme
        {
            PrimaryLight = new ColorPair(GetColor(resourceDictionary, "MaterialDesign.Brush.Primary.Light"),
                                         GetColor(resourceDictionary, "MaterialDesign.Brush.Primary.Light.Foreground")),
            PrimaryMid = new ColorPair(GetColor(resourceDictionary, "MaterialDesign.Brush.Primary"),
                                       GetColor(resourceDictionary, "MaterialDesign.Brush.Primary.Foreground")),
            PrimaryDark = new ColorPair(GetColor(resourceDictionary, "MaterialDesign.Brush.Primary.Dark"),
                                        GetColor(resourceDictionary, "MaterialDesign.Brush.Primary.Dark.Foreground")),

            SecondaryLight = new ColorPair(GetColor(resourceDictionary, "MaterialDesign.Brush.Secondary.Light"),
                                           GetColor(resourceDictionary, "MaterialDesign.Brush.Secondary.Light.Foreground")),
            SecondaryMid = new ColorPair(GetColor(resourceDictionary, "MaterialDesign.Brush.Secondary"),
                                         GetColor(resourceDictionary, "MaterialDesign.Brush.Secondary.Foreground")),
            SecondaryDark = new ColorPair(GetColor(resourceDictionary, "MaterialDesign.Brush.Secondary.Dark"),
                                          GetColor(resourceDictionary, "MaterialDesign.Brush.Secondary.Dark.Foreground")),
        };
        LoadThemeColors(resourceDictionary, theme);
        return theme;
    }

    private static partial void LoadThemeColors(ResourceDictionary resourceDictionary, Theme theme);

    private static Color GetColor(ResourceDictionary resourceDictionary, params string[] keys)
    {
        foreach (string key in keys)
        {
            if (TryGetColor(resourceDictionary, key, out Color color))
            {
                return color;
            }
        }
        throw new InvalidOperationException($"Could not locate required resource with key(s) '{string.Join(", ", keys)}'");
    }

    private static bool TryGetColor(ResourceDictionary resourceDictionary, string key, out Color color)
    {
        if (resourceDictionary[key] is SolidColorBrush brush)
        {
            color = brush.Color;
            return true;
        }
        color = default;
        return false;
    }

    public static IThemeManager? GetThemeManager(this ResourceDictionary resourceDictionary)
    {
        if (resourceDictionary is null) throw new ArgumentNullException(nameof(resourceDictionary));
        return resourceDictionary[ThemeManagerKey] as IThemeManager;
    }

    internal static void SetSolidColorBrush(this ResourceDictionary sourceDictionary, string name, Color value)
    {
        if (sourceDictionary is null) throw new ArgumentNullException(nameof(sourceDictionary));
        if (name is null) throw new ArgumentNullException(nameof(name));

        if (sourceDictionary[name] is SolidColorBrush brush)
        {
            if (brush.Color == value) return;

            if (!brush.IsFrozen)
            {
                var animation = new ColorAnimation
                {
                    From = brush.Color,
                    To = value,
                    Duration = new Duration(TimeSpan.FromMilliseconds(300))
                };
                brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                return;
            }
        }

        var newBrush = new SolidColorBrush(value);
        newBrush.Freeze();
        sourceDictionary[name] = newBrush;
    }

    private static void AdjustColors(Color background, ColorAdjustment colorAdjustment, ref Color light, ref Color mid, ref Color dark)
    {
        double offset;
        switch (colorAdjustment.Contrast)
        {
            case Contrast.Low:
                if (background.IsLightColor())
                {
                    dark = dark.EnsureContrastRatio(background, colorAdjustment.DesiredContrastRatio, out offset);
                    if (Math.Abs(offset) > 0.0)
                    {
                        mid = mid.ShiftLightness(offset);
                        light = light.ShiftLightness(offset);
                    }
                }
                else
                {
                    light = light.EnsureContrastRatio(background, colorAdjustment.DesiredContrastRatio, out offset);
                    if (Math.Abs(offset) > 0.0)
                    {
                        mid = mid.ShiftLightness(offset);
                        dark = dark.ShiftLightness(offset);
                    }
                }
                break;
            case Contrast.Medium:
                mid = mid.EnsureContrastRatio(background, colorAdjustment.DesiredContrastRatio, out offset);
                if (Math.Abs(offset) > 0.0)
                {
                    dark = dark.ShiftLightness(offset);
                    light = light.ShiftLightness(offset);
                }
                break;
            case Contrast.High:
                if (background.IsLightColor())
                {
                    light = light.EnsureContrastRatio(background, colorAdjustment.DesiredContrastRatio, out offset);
                    if (Math.Abs(offset) > 0.0)
                    {
                        mid = mid.ShiftLightness(offset);
                        dark = dark.ShiftLightness(offset);
                    }
                }
                else
                {
                    dark = dark.EnsureContrastRatio(background, colorAdjustment.DesiredContrastRatio, out offset);
                    if (Math.Abs(offset) > 0.0)
                    {
                        light = light.ShiftLightness(offset);
                        mid = mid.ShiftLightness(offset);
                    }
                }
                break;
        }
    }

    private class ThemeManager(ResourceDictionary resourceDictionary) : IThemeManager
    {
        private readonly ResourceDictionary _resourceDictionary = resourceDictionary ?? throw new ArgumentNullException(nameof(resourceDictionary));

        public event EventHandler<ThemeChangedEventArgs>? ThemeChanged;

        public void OnThemeChange(Theme oldTheme, Theme newTheme)
            => ThemeChanged?.Invoke(this, new ThemeChangedEventArgs(_resourceDictionary, oldTheme, newTheme));
    }
}
