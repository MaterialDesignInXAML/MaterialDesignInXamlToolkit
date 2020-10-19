using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public static class MdixHelper
    {
        static MdixHelper()
        {
            var _ = Application.Current;
        }

        private static ResourceDictionary DefaultResourceDictionary => GetResourceDictionary("MaterialDesignTheme.Defaults.xaml");

        private static ResourceDictionary GenericResourceDictionary => GetResourceDictionary("Generic.xaml");

        public static void ApplyStyle<T>(this T control, object styleKey, bool applyTemplate = true) where T : FrameworkElement
        {
            var style = GetStyle(styleKey);
            Assert.True(style != null, $"Could not find style with key '{styleKey}' for control type {typeof(T).FullName}");
            control.Style = style;
            if (applyTemplate)
            {
                Assert.True(control.ApplyTemplate(), "Failed to apply template");
            }
        }

        public static void ApplyDefaultStyle<T>(this T control) where T : FrameworkElement => control.ApplyStyle(typeof(T));

        public static IEnumerable<object> GetStyleKeysFor<T>()
            => DefaultResourceDictionary.GetStyleKeysFor<T>()
                .Concat(GetResourceDictionary($"MaterialDesignTheme.{typeof(T).Name}.xaml").GetStyleKeysFor<T>());

        private static IEnumerable<object> GetStyleKeysFor<T>(this IEnumerable dictionary)
            => dictionary
                .Cast<DictionaryEntry>()
                .Where(e => e.Value is Style style && style.TargetType is T)
                .Select(e => e.Key);

        private static Style GetStyle(object key)
            => DefaultResourceDictionary[key] as Style
               ?? GenericResourceDictionary[key] as Style;

        public static ResourceDictionary GetResourceDictionary(string xamlName)
        {
            var uri = new Uri($"/MaterialDesignThemes.Wpf;component/Themes/{xamlName}", UriKind.Relative);
            return new ResourceDictionary
            {
                Source = uri
            };
        }

        public static ResourceDictionary GetPrimaryColorResourceDictionary(string color)
        {
            var uri = new Uri($"/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.{color}.xaml", UriKind.Relative);
            return new ResourceDictionary
            {
                Source = uri
            };
        }

        public static ResourceDictionary GetSecondaryColorResourceDictionary(string color)
        {
            var uri = new Uri($"/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.{color}.xaml", UriKind.Relative);
            return new ResourceDictionary
            {
                Source = uri
            };
        }
    }
}