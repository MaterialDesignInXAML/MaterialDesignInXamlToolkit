using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    [MarkupExtensionReturnType(typeof(FontFamily))]
    public class MaterialDesignFontExtension : MarkupExtension
    {
        private static readonly Lazy<FontFamily> _roboto
            = new(() => new FontFamily(new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/"), "./#Roboto"));

        public override object ProvideValue(IServiceProvider serviceProvider)
            => _roboto.Value;
    }
}
