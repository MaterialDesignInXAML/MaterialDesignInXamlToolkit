using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        private static ResourceDictionary DefaultResourceDictionary => GetThemeResourceDictionary("Defaults");

        private static ResourceDictionary GenericResourceDictionary => GetGenericResourceDictionary();

        public static void ApplyStyle<T>(this T control, object styleKey, bool applyTemplate = true) where T : FrameworkElement
        {
            Style style = GetStyle(styleKey);
            Assert.True(style != null, $"Could not find style with key '{styleKey}' for control type {typeof(T).FullName}");
            control.Style = style;
            if (applyTemplate)
            {
                Assert.True(control.ApplyTemplate(), "Failed to apply template");
            }
        }

        public static void ApplyDefaultStyle<T>(this T control) where T : FrameworkElement => control.ApplyStyle(typeof(T));

        public static IEnumerable<object> GetStyleKeysFor<T>()
        {
            foreach (object key in GetKeysFromResourceDictionary(DefaultResourceDictionary))
            {
                yield return key;
            }
            ResourceDictionary controlResourceDictionary = GetThemeResourceDictionary(typeof(T).Name);
            foreach (object key in GetKeysFromResourceDictionary(controlResourceDictionary))
            {
                yield return key;
            }

            //TODO: Make static once we go to C# 8
            IEnumerable<object> GetKeysFromResourceDictionary(ResourceDictionary resourceDictionary)
            {
                foreach (object key in resourceDictionary.Keys)
                {
                    if (resourceDictionary[key] is Style style)
                    {
                        if (style.TargetType == typeof(T))
                        {
                            yield return key;
                        }
                    }
                }
            }
        }

        private static Style GetStyle(object key)
        {
            return DefaultResourceDictionary[key] as Style ??
                   GenericResourceDictionary[key] as Style;
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