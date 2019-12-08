using System;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    [MarkupExtensionReturnType(typeof(FontFamily))]
    public class MaterialDesignFont : MarkupExtension
    {
        private static readonly Lazy<FontFamily> _roboto
            = new Lazy<FontFamily>(() => 
                new FontFamily(new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/"), "./#Roboto"));
        
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _roboto.Value;
        }
    }
}