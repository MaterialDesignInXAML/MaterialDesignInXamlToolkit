using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class HintProxyFabricConverter : IValueConverter
    {
        private static readonly Lazy<HintProxyFabricConverter> _instance = new Lazy<HintProxyFabricConverter>();

        public static HintProxyFabricConverter Instance => _instance.Value;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return HintProxyFabric.Get(value as Control);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
