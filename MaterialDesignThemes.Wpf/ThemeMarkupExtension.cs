using System;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using MaterialDesignColors;

namespace MaterialDesignThemes.Wpf
{
    [MarkupExtensionReturnType(typeof(ResourceDictionary))]
    public abstract class ThemeMarkupExtension : MarkupExtension
    {
        protected virtual void SetTheme(ITheme theme, ResourceDictionary resourceDictionary)
        {
            resourceDictionary.SetTheme(theme);
        }

        protected abstract ITheme GetTheme();

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var rv = new ResourceDictionary();
            SetTheme(GetTheme(), rv);
            return rv;
        }
    }

    //TODO: Naming of this?
    public class BundledTheme : ThemeMarkupExtension
    {
        public BaseTheme BaseTheme { get; set; }
        public PrimaryColor PrimaryColor { get; set; }
        public AccentColor AccentColor { get; set; }

        protected override ITheme GetTheme()
        {
            return Theme.Create(BaseTheme.GetBaseTheme(),
                SwatchHelper.Lookup[(MaterialDesignColor) PrimaryColor],
                SwatchHelper.Lookup[(MaterialDesignColor) AccentColor]);
        }
    }

    public class CustomColorTheme : ThemeMarkupExtension
    {
        public BaseTheme BaseTheme { get; set; }
        public Color Primary { get; set; }
        public Color Accent { get; set; }

        protected override ITheme GetTheme()
        {
            return Theme.Create(BaseTheme.GetBaseTheme(), Primary, Accent);
        }
    }
}