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
    public class BundledThemeMarkupExtension : ThemeMarkupExtension
    {
        public BaseTheme BaseTheme { get; set; }
        public PrimaryColor PrimaryColor { get; set; }
        public SecondaryColor SecondaryColor { get; set; }

        protected override ITheme GetTheme()
        {
            return Theme.Create(BaseTheme.GetBaseTheme(),
                SwatchHelper.Lookup[(MaterialDesignColor) PrimaryColor],
                SwatchHelper.Lookup[(MaterialDesignColor) SecondaryColor]);
        }
    }

    //Implementation of BundledTheme that works during design time
    //Markup extensions to provide ResourceDictionary cause XAML designer exception
    public class BundledTheme : ResourceDictionary
    {
        private BaseTheme _baseTheme;
        public BaseTheme BaseTheme
        {
            get { return _baseTheme; }
            set
            {
                _baseTheme = value;
                SetTheme();
            }
        }

        private PrimaryColor _primaryColor;

        public PrimaryColor PrimaryColor 
        {
            get { return _primaryColor; }
            set
            {
                _primaryColor = value;
                SetTheme();
            }
        }

        private SecondaryColor _secondaryColor;
        public SecondaryColor SecondaryColor
        {
            get { return _secondaryColor; }
            set
            {
                _secondaryColor = value;
                SetTheme();
            }
        }

        private void SetTheme()
        {
            var theme = Theme.Create(BaseTheme.GetBaseTheme(),
            SwatchHelper.Lookup[(MaterialDesignColor)PrimaryColor],
            SwatchHelper.Lookup[(MaterialDesignColor)SecondaryColor]);

            this.SetTheme(theme);
        }

        public BundledTheme() { }
    }

    public class CustomColorThemeMarkupExtension : ThemeMarkupExtension
    {
        public BaseTheme BaseTheme { get; set; }
        public Color Primary { get; set; }
        public Color Secondary { get; set; }

        protected override ITheme GetTheme()
        {
            return Theme.Create(BaseTheme.GetBaseTheme(), Primary, Secondary);
        }
    }

    //Implementation of CustomColorTheme that works during design time
    //Markup extensions to provide ResourceDictionary cause XAML designer exception
    public class CustomColorTheme : ResourceDictionary
    {
        private BaseTheme _baseTheme;
        public BaseTheme BaseTheme
        {
            get { return _baseTheme; }
            set
            {
                _baseTheme = value;
                TrySetTheme();
            }
        }

        private Color _primary;
        public Color Primary
        {
            get { return _primary; }
            set
            {
                _primary = value;
                TrySetTheme();
            }
        }

        private Color _secondary;
        public Color Secondary
        {
            get { return _secondary; }
            set
            {
                _secondary = value;
                TrySetTheme();
            }
        }

        private void TrySetTheme()
        {
            if(Primary != null && Secondary != null)
            {
                var theme = Theme.Create(BaseTheme.GetBaseTheme(), Primary, Secondary);
                this.SetTheme(theme);
            }
        }

        public CustomColorTheme()
        {
        }
    }
}