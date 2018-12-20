using System;
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

        private static void OnThemeChanged(DependencyObject @do, DependencyPropertyChangedEventArgs e)
        {
            if (@do is FrameworkElement element)
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
