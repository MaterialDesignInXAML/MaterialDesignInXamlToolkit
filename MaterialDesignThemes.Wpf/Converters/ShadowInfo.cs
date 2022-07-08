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
                // Obselete
                #region ObseleteDepth
                { ShadowDepth.Depth0, null },
                { ShadowDepth.Depth1, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth1"] },
                { ShadowDepth.Depth2, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth2"] },
                { ShadowDepth.Depth3, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth3"] },
                { ShadowDepth.Depth4, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth4"] },
                { ShadowDepth.Depth5, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth5"] },
                #endregion
                
                { ShadowDepth.Depth_0dp, null },
                { ShadowDepth.Depth_1dp, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth_1dp"] },
                { ShadowDepth.Depth_2dp, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth_2dp"] },
                { ShadowDepth.Depth_3dp, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth_3dp"] },
                { ShadowDepth.Depth_4dp, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth_4dp"] },
                { ShadowDepth.Depth_5dp, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth_5dp"] },
                { ShadowDepth.Depth_6dp, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth_6dp"] },
                { ShadowDepth.Depth_7dp, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth_7dp"] },
                { ShadowDepth.Depth_8dp, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth_8dp"] },
                { ShadowDepth.Depth_12dp, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth_12dp"] },
                { ShadowDepth.Depth_16dp, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth_16dp"] },
                { ShadowDepth.Depth_24dp, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth_24dp"] }
            };
        }

        public static DropShadowEffect? GetDropShadow(ShadowDepth depth)
            => ShadowsDictionary[depth];
    }
}