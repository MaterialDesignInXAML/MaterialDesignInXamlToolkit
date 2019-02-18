using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignColors.Recommended;
using MaterialDesignColors.Wpf;

namespace MaterialDesignThemes.Wpf
{
    // TODO
    public class MaterialDesignResourceDictionary : ResourceDictionary
    {
        public MaterialDesignResourceDictionary()
        {
            Application.Current.Resources = this;
            MergedDictionaries.Add(CreateDefaultThemeDictionary());
            MergedDictionaries.Add(CreateEmptyThemeDictionary());
            MergedDictionaries.Add(CreateEmptyPaletteDictionary());

            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(MaterialDesignTheme.ChangeThemeCommand, ChangeThemeCommandExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(MaterialDesignTheme.ChangePaletteCommand, ChangePaletteCommandExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(MaterialDesignTheme.ChangeColorCommand, ChangeColorCommandExecuted));
        }

        public virtual void ChangeThemeCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ApplyThemeChange(e.Parameter);
        }

        public virtual void ChangePaletteCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ApplyPaletteChange(e.Parameter);
        }

        public virtual void ChangeColorCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ApplyColorChange(e.Parameter);
        }

        public void ApplyThemeChange(object parameter)
        {
            if (parameter is ThemeManagerThemeChange themeManagerChange)
            {
                themeManagerChange.OwningResourceDictionary.WithTheme((IBaseTheme)themeManagerChange.OriginalParameter);
            }
            else
            {
                this.WithTheme((IBaseTheme)parameter);
            }
        }

        public void ApplyPaletteChange(object parameter)
        {
            if (parameter is ThemeManagerThemeChange themeManagerChange)
            {
                themeManagerChange.OwningResourceDictionary.WithPalette((ColorPalette)themeManagerChange.OriginalParameter);
            }
            else
            {
                this.WithPalette((ColorPalette)parameter);
            }
        }

        public void ApplyColorChange(object parameter)
        {
            if (parameter is ThemeManagerThemeChange themeManagerChange)
            {
                var colorChange = (ColorChange)themeManagerChange.OriginalParameter;
                themeManagerChange.OwningResourceDictionary.WithColor(colorChange.Name, colorChange.Color);
            }
            else
            {
                var colorChange = (ColorChange)parameter;
                this.WithColor(colorChange.Name, colorChange.Color);
            }
        }
        private BaseTheme _theme = BaseTheme.Light;

        public BaseTheme Theme
        {
            get => _theme;
            set => this.WithTheme(_theme = value);
        }

        private MaterialDesignColor _primaryColor = MaterialDesignColor.DeepPurple;
        public MaterialDesignColor PrimaryColor
        {
            get => _primaryColor;
            set => this.WithPrimaryColor(_primaryColor = value);
        }

        private MaterialDesignColor _secondaryColor = MaterialDesignColor.DeepPurple;
        public MaterialDesignColor SecondaryColor
        {
            get => _secondaryColor;
            set => this.WithSecondaryColor(_secondaryColor = value);
        }

        public static ResourceDictionary CreateEmptyThemeDictionary()
        {
            return new ResourceDictionary {
                ["ValidationErrorColor"] = new SolidColorBrush(),
                ["MaterialDesignBackground"] = new SolidColorBrush(),
                ["MaterialDesignPaper"] = new SolidColorBrush(),
                ["MaterialDesignCardBackground"] = new SolidColorBrush(),
                ["MaterialDesignToolBarBackground"] = new SolidColorBrush(),
                ["MaterialDesignBody"] = new SolidColorBrush(),
                ["MaterialDesignBodyLight"] = new SolidColorBrush(),
                ["MaterialDesignColumnHeader"] = new SolidColorBrush(),
                ["MaterialDesignCheckBoxOff"] = new SolidColorBrush(),
                ["MaterialDesignCheckBoxDisabled"] = new SolidColorBrush(),
                ["MaterialDesignTextBoxBorder"] = new SolidColorBrush(),
                ["MaterialDesignDivider"] = new SolidColorBrush(),
                ["MaterialDesignSelection"] = new SolidColorBrush(),
                ["MaterialDesignFlatButtonClick"] = new SolidColorBrush(),
                ["MaterialDesignFlatButtonRipple"] = new SolidColorBrush(),
                ["MaterialDesignToolTipBackground"] = new SolidColorBrush(),
                ["MaterialDesignChipBackground"] = new SolidColorBrush(),
                ["MaterialDesignSnackbarBackground"] = new SolidColorBrush(),
                ["MaterialDesignSnackbarMouseOver"] = new SolidColorBrush(),
                ["MaterialDesignSnackbarRipple"] = new SolidColorBrush(),
                ["MaterialDesignTextFieldBoxBackground"] = new SolidColorBrush(),
                ["MaterialDesignTextFieldBoxHoverBackground"] = new SolidColorBrush(),
                ["MaterialDesignTextFieldBoxDisabledBackground"] = new SolidColorBrush(),
                ["MaterialDesignTextAreaBorder"] = new SolidColorBrush(),
                ["MaterialDesignTextAreaInactiveBorder"] = new SolidColorBrush()
            };
        }

        public static ResourceDictionary CreateEmptyPaletteDictionary()
        {
            return new ResourceDictionary {
                ["PrimaryHueLightBrush"] = new SolidColorBrush(),
                ["PrimaryHueLightForegroundBrush"] = new SolidColorBrush(),
                ["PrimaryHueMidBrush"] = new SolidColorBrush(),
                ["PrimaryHueMidForegroundBrush"] = new SolidColorBrush(),
                ["PrimaryHueDarkBrush"] = new SolidColorBrush(),
                ["PrimaryHueDarkForegroundBrush"] = new SolidColorBrush(),
                ["SecondaryHueLightBrush"] = new SolidColorBrush(),
                ["SecondaryHueLightForegroundBrush"] = new SolidColorBrush(),
                ["SecondaryHueMidBrush"] = new SolidColorBrush(),
                ["SecondaryHueMidForegroundBrush"] = new SolidColorBrush(),
                ["SecondaryHueDarkBrush"] = new SolidColorBrush(),
                ["SecondaryHueDarkForegroundBrush"] = new SolidColorBrush(),
                // Compatability
                ["SecondaryAccentBrush"] = new SolidColorBrush(),
                ["SecondaryAccentForegroundBrush"] = new SolidColorBrush()
            };
        }


        private const string ThemeUriFormat =
            @"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{0}.xaml";

        public static ResourceDictionary CreateDefaultThemeDictionary()
            => new ResourceDictionary { Source = new Uri(string.Format(ThemeUriFormat, "Defaults")) };
    }
}