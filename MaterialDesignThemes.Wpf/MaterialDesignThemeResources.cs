using System;
using System.Windows;
using MaterialDesignColors;

namespace MaterialDesignThemes.Wpf
{
    public class MaterialDesignThemeResources : ResourceDictionary
    {
        private const string ColorUriFormat =
            @"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/{0}/MaterialDesignColor.{1}.xaml";

        private const string ThemeUriFormat =
            @"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{0}.xaml";


        public MaterialDesignThemeResources()
        {
            MergedDictionaries.Add(BuildDict(ThemeUriFormat, "Defaults"));
        }

        private ResourceDictionary baseResourceDictionary;
        private BaseTheme baseTheme;
        public BaseTheme BaseTheme
        {
            get => baseTheme;
            set => UpdateResourceDictionary(ref baseResourceDictionary, BuildDict(ThemeUriFormat, (baseTheme = value).ToString()));
        }

        private ResourceDictionary primaryResourceDictionary;
        private PrimaryColor primaryColor;
        public PrimaryColor PrimaryColor
        {
            get => primaryColor;
            set => UpdateResourceDictionary(ref primaryResourceDictionary, BuildDict(ColorUriFormat, "Primary", (primaryColor = value).ToString()));
        }

        private ResourceDictionary accentResourceDictionary;
        private AccentColor accentColor;
        public AccentColor AccentColor
        {
            get => accentColor;
            set => UpdateResourceDictionary(ref accentResourceDictionary, BuildDict(ColorUriFormat, "Accent", (accentColor = value).ToString()));
        }

        private void UpdateResourceDictionary(ref ResourceDictionary existing, ResourceDictionary replacement)
        {
            var index = MergedDictionaries.IndexOf(existing);
            if (index != -1)
            {
                MergedDictionaries.RemoveAt(index);
                MergedDictionaries.Insert(index, replacement);
            }
            else
            {
                MergedDictionaries.Add(replacement);
            }

            existing = replacement;
        }

        private static ResourceDictionary BuildDict(string uriFormatString, params string[] arguments)
        {
            var uri = new Uri(string.Format(uriFormatString, arguments));
            return new ResourceDictionary {Source = uri};
        }
    }
}
