using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Effects;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class ShadowConverter : IValueConverter
    {
        private static readonly IDictionary<ShadowDepth, DropShadowEffect> ShadowsDictionary;
        public static readonly ShadowConverter Instance = new ShadowConverter();

        static ShadowConverter()
        {
            var resourceDictionary = new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Shadows.xaml", UriKind.Absolute) };

            ShadowsDictionary = new Dictionary<ShadowDepth, DropShadowEffect>
            {
                { ShadowDepth.Depth0, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth0"] },
                { ShadowDepth.Depth1, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth1"] },
                { ShadowDepth.Depth2, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth2"] },
                { ShadowDepth.Depth3, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth3"] },
                { ShadowDepth.Depth4, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth4"] },
                { ShadowDepth.Depth5, (DropShadowEffect)resourceDictionary["MaterialDesignShadowDepth5"] },
            };
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ShadowDepth)) return null;

            return Clone(ShadowsDictionary[(ShadowDepth) value]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static DropShadowEffect Clone(DropShadowEffect dropShadowEffect)
        {
            return new DropShadowEffect()
            {
                BlurRadius = dropShadowEffect.BlurRadius,
                Color = dropShadowEffect.Color,
                Direction = dropShadowEffect.Direction,
                Opacity = dropShadowEffect.Opacity,
                RenderingBias = dropShadowEffect.RenderingBias,
                ShadowDepth = dropShadowEffect.ShadowDepth
            };
        }
    }
}
