using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class ThemeAssist
    {
        public static readonly DependencyProperty ThemeProperty = DependencyProperty.RegisterAttached(
            "Theme", typeof(BaseTheme), typeof(ThemeAssist), new PropertyMetadata(default(BaseTheme), OnThemeChanged));

        private static void OnThemeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is FrameworkElement element)
            {
                element.Resources.WithTheme((BaseTheme)e.NewValue);
            }
        }

        public static void SetTheme(DependencyObject element, BaseTheme value)
        {
            element.SetValue(ThemeProperty, value);
        }

        public static BaseTheme GetTheme(DependencyObject element)
        {
            return (BaseTheme)element.GetValue(ThemeProperty);
        }

        public static readonly DependencyProperty PrimaryColorProperty = DependencyProperty.RegisterAttached(
            "PrimaryColor", typeof(PrimaryColor), typeof(ThemeAssist), new PropertyMetadata(default(PrimaryColor), OnPrimaryColorChanged));

        private static void OnPrimaryColorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is FrameworkElement element)
            {
                if (e.OldValue is BaseTheme oldTheme && 
                    GetResourceDictionarySource(oldTheme) is string oldSource)
                {
                    foreach(ResourceDictionary resourceDictionary in element.Resources.MergedDictionaries)
                    {
                        if (string.Equals(resourceDictionary.Source?.ToString(), oldSource, StringComparison.OrdinalIgnoreCase))
                        {
                            element.Resources.MergedDictionaries.(resourceDictionary);
                            break;
                        }
                    }
                }

                if (e.NewValue is BaseTheme newTheme &&
                    GetResourceDictionarySource(newTheme) is string newThemeSource)
                {
                    element.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(newThemeSource) });
                }
            }
        }

        public static void SetPrimaryColor(DependencyObject element, PrimaryColor value)
        {
            element.SetValue(PrimaryColorProperty, value);
        }

        public static PrimaryColor GetPrimaryColor(DependencyObject element)
        {
            return (PrimaryColor)element.GetValue(PrimaryColorProperty);
        }

        public static readonly DependencyProperty AccentColorProperty = DependencyProperty.RegisterAttached(
            "AccentColor", typeof(AccentColor), typeof(ThemeAssist), new PropertyMetadata(default(AccentColor), OnAccentColorChanged));

        private static void OnAccentColorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is FrameworkElement element)
            {
                element.Resources.WithAccentColor((AccentColor)e.NewValue);
            }
        }

        public static void SetAccentColor(DependencyObject element, AccentColor value)
        {
            element.SetValue(AccentColorProperty, value);
        }

        public static AccentColor GetAccentColor(DependencyObject element)
        {
            return (AccentColor)element.GetValue(AccentColorProperty);
        }
    }
}