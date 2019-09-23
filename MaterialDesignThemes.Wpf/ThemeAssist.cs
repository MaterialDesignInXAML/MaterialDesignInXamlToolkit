using System;
using System.Linq;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class ThemeAssist
    {
        public static BaseTheme GetTheme(DependencyObject obj)
        {
            return (BaseTheme)obj.GetValue(ThemeProperty);
        }

        public static void SetTheme(DependencyObject obj, BaseTheme value)
        {
            obj.SetValue(ThemeProperty, value);
        }

        public static readonly DependencyProperty ThemeProperty =
            DependencyProperty.RegisterAttached("Theme", typeof(BaseTheme), typeof(ThemeAssist), new PropertyMetadata(default(BaseTheme), OnThemeChanged));

        public static void ChangeTheme(ResourceDictionary resourceDictionary, BaseTheme newTheme)
        {
            if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));

            string lightSource = GetResourceDictionarySource(BaseTheme.Light);
            string darkSource = GetResourceDictionarySource(BaseTheme.Dark);

            foreach (ResourceDictionary mergedDictionary in resourceDictionary.MergedDictionaries.ToList())
            {
                if (string.Equals(mergedDictionary.Source?.ToString(), lightSource, StringComparison.OrdinalIgnoreCase))
                {
                    resourceDictionary.MergedDictionaries.Remove(mergedDictionary);
                }
                if (string.Equals(mergedDictionary.Source?.ToString(), darkSource, StringComparison.OrdinalIgnoreCase))
                {
                    resourceDictionary.MergedDictionaries.Remove(mergedDictionary);
                }
            }

            if (GetResourceDictionarySource(newTheme) is string newThemeSource)
            {
                resourceDictionary.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(newThemeSource) });
            }
        }

        private static void OnThemeChanged(DependencyObject @do, DependencyPropertyChangedEventArgs e)
        {
            if (@do is FrameworkElement element && e.NewValue is BaseTheme newTheme)
            {
                ChangeTheme(element.Resources, newTheme);
            }
        }

        private static string GetResourceDictionarySource(BaseTheme theme)
        {
            switch (theme)
            {
                case BaseTheme.Light:
                    return "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml";
                case BaseTheme.Dark:
                    return "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml";
            }
            return null;
        }
    }
}