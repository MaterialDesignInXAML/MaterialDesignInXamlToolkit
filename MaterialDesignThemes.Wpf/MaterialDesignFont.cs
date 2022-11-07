﻿using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    [MarkupExtensionReturnType(typeof(FontFamily))]
    public class MaterialDesignFontExtension : MarkupExtension
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
