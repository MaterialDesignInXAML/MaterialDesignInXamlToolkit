using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class ShadowConverter : IValueConverter
    {
        private static readonly IDictionary<ShadowDepth, object> ShadowsDictionary;
        public static readonly ShadowConverter Instance = new ShadowConverter();

        static ShadowConverter()
        {
            var resourceDictionary = new ResourceDictionary {  Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Shadows.xaml", UriKind.Absolute) };

            ShadowsDictionary = new Dictionary<ShadowDepth, object>
            {
                { ShadowDepth.Depth1, resourceDictionary["MaterialDesignShadowDepth1"] },
                { ShadowDepth.Depth2, resourceDictionary["MaterialDesignShadowDepth2"] },
                { ShadowDepth.Depth3, resourceDictionary["MaterialDesignShadowDepth3"] },
                { ShadowDepth.Depth4, resourceDictionary["MaterialDesignShadowDepth4"] },
                { ShadowDepth.Depth5, resourceDictionary["MaterialDesignShadowDepth5"] },
            };
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ShadowsDictionary[(ShadowDepth) value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
