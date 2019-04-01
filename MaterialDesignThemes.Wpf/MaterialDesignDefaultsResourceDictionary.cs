using System;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public class MaterialDesignDefaultsResourceDictionary : ResourceDictionary
    {
        public MaterialDesignDefaultsResourceDictionary()
        {
            Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml");
        }
    }
}
