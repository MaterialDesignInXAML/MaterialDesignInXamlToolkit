using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Effects;

namespace MaterialDesignThemes.Wpf.Converters
{
    internal static class ShadowInfo
    {
        private static readonly IDictionary<ShadowDepth, DropShadowEffect?> ShadowsDictionary;

        static ShadowInfo()
        {
            var resourceDictionary = new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Shadows.xaml", UriKind.Absolute) };

            ShadowsDictionary = new Dictionary<ShadowDepth, DropShadowEffect?>
            {
                { ShadowDepth.Depth0, null },
                { ShadowDepth.Depth1, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth1"] },
                { ShadowDepth.Depth2, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth2"] },
                { ShadowDepth.Depth3, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth3"] },
                { ShadowDepth.Depth4, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth4"] },
                { ShadowDepth.Depth5, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth5"] },
                { ShadowDepth.Depth6, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth6"] },
                { ShadowDepth.Depth7, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth7"] },
                { ShadowDepth.Depth8, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth8"] },
            };
        }

        public static DropShadowEffect? GetDropShadow(ShadowDepth depth)
            => ShadowsDictionary[depth];
    }
}