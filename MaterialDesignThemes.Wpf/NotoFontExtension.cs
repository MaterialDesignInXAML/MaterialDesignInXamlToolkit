using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    [MarkupExtensionReturnType(typeof(FontFamily))]
    public class NotoFontExtension : MarkupExtension
    {
        private static readonly Lazy<FontFamily> _noto
            = new Lazy<FontFamily>(() =>
                new FontFamily(new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Noto/"), "./#Noto"));

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _noto.Value;
        }
    }
}