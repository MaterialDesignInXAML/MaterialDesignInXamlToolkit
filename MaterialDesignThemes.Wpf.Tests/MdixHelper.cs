using System;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Controls;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public static class MdixHelper
    {
        static MdixHelper()
        {
            var _ = Application.Current;

            _defaultResourceDictionary = new Lazy<ResourceDictionary>(() => GetThemeResourceDictionary("Defaults"));
            _genericResourceDictionary = new Lazy<ResourceDictionary>(GetGenericResourceDictionary);
        }

        private static readonly Lazy<ResourceDictionary> _defaultResourceDictionary;
        private static ResourceDictionary DefaultResourceDictionary => _defaultResourceDictionary.Value;

        private static readonly Lazy<ResourceDictionary> _genericResourceDictionary;
        private static ResourceDictionary GenericResourceDictionary => _genericResourceDictionary.Value;

        public static void ApplyDefaultStyle<T>(this T control) where T : FrameworkElement
        {
            Style style = GetDefaultStyle<T>();
            Assert.True(style != null, $"Could not find default style for control type {typeof(T).FullName}");
            control.Style = style;
            Assert.True(control.ApplyTemplate(), "Failed to apply template ");
        }

        private static Style GetDefaultStyle<T>() where T : FrameworkElement
        {
            return DefaultResourceDictionary[typeof(T)] as Style ??
                   GenericResourceDictionary[typeof(T)] as Style;
        }

        private static ResourceDictionary GetThemeResourceDictionary(string name)
        {
            var uri = new Uri(
                $"/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{name}.xaml", UriKind.Relative);
            return new ResourceDictionary {
                Source = uri
            };
        }

        private static ResourceDictionary GetGenericResourceDictionary()
        {
            var uri = new Uri(
                $"/MaterialDesignThemes.Wpf;component/Themes/Generic.xaml", UriKind.Relative);
            return new ResourceDictionary {
                Source = uri
            };
        }
    }
}