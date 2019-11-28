using System;
using System.IO;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public static class MaterialDesignFonts
    {
        private static readonly Lazy<FontFamily> _roboto 
            = new Lazy<FontFamily>(LoadRobotoFontFamily);

        private static FontFamily LoadRobotoFontFamily()
        {
            string fontDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Roboto");
            return new FontFamily(new Uri($"file:///{fontDirectory}"), "./#Roboto");
        }

        public static FontFamily Roboto => _roboto.Value;
    }
}