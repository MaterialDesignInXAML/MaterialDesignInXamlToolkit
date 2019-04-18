using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf
{
    public class ThemeChangedEventArgs : EventArgs
    {
        public ThemeChangedEventArgs(ResourceDictionary resourceDictionary, ITheme oldTheme, ITheme newTheme)
        {
            ResourceDictionary = resourceDictionary;
            OldTheme = oldTheme;
            NewTheme = newTheme;
        }

        public ResourceDictionary ResourceDictionary { get; }
        public ITheme NewTheme { get; }
        public ITheme OldTheme { get; }
    }

    public interface IThemeManager
    {
        event EventHandler<ThemeChangedEventArgs> ThemeChanged;
    }

    internal class ThemeManager : IThemeManager
    {
        public event EventHandler<ThemeChangedEventArgs> ThemeChanged;

        public void OnThemeChange(ResourceDictionary resourceDictionary, ITheme oldTheme, ITheme newTheme)
        {
            ThemeChanged?.Invoke(this, new ThemeChangedEventArgs(resourceDictionary, oldTheme, newTheme));
        }
    }

    public static class ResourceDictionaryExtensions
    {
        private static Guid CurrentThemeKey { get; } = Guid.NewGuid();
        private static Guid ThemeManagerKey { get; } = Guid.NewGuid();

        public static void SetTheme(this ResourceDictionary resourceDictionary, ITheme theme)
        {
            if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));

            SetSolidColorBrush(resourceDictionary, "PrimaryHueLightBrush", theme.PrimaryLight.Color);
            SetSolidColorBrush(resourceDictionary, "PrimaryHueLightForegroundBrush", theme.PrimaryLight.ForegroundColor ?? theme.PrimaryLight.Color.ContrastingForegroundColor());
            SetSolidColorBrush(resourceDictionary, "PrimaryHueMidBrush", theme.PrimaryMid.Color);
            SetSolidColorBrush(resourceDictionary, "PrimaryHueMidForegroundBrush", theme.PrimaryMid.ForegroundColor ?? theme.PrimaryMid.Color.ContrastingForegroundColor());
            SetSolidColorBrush(resourceDictionary, "PrimaryHueDarkBrush", theme.PrimaryDark.Color);
            SetSolidColorBrush(resourceDictionary, "PrimaryHueDarkForegroundBrush", theme.PrimaryDark.ForegroundColor ?? theme.PrimaryDark.Color.ContrastingForegroundColor());

            SetSolidColorBrush(resourceDictionary, "SecondaryAccentBrush", theme.Accent.Color);
            SetSolidColorBrush(resourceDictionary, "SecondaryAccentForegroundBrush", theme.Accent.ForegroundColor ?? theme.Accent.Color.ContrastingForegroundColor());

            SetSolidColorBrush(resourceDictionary, "ValidationErrorBrush", theme.ValidationError);
            resourceDictionary["ValidationErrorColor"] = theme.ValidationError;

            SetSolidColorBrush(resourceDictionary, "MaterialDesignBackground", theme.Background);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignPaper", theme.Paper);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignCardBackground", theme.CardBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignToolBarBackground", theme.ToolBarBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignBody", theme.Body);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignBodyLight", theme.BodyLight);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignColumnHeader", theme.ColumnHeader);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignCheckBoxOff", theme.CheckBoxOff);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignCheckBoxDisabled", theme.CheckBoxDisabled);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextBoxBorder", theme.TextBoxBorder);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignDivider", theme.Divider);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignSelection", theme.Selection);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignFlatButtonClick", theme.FlatButtonClick);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignFlatButtonRipple", theme.FlatButtonRipple);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignToolTipBackground", theme.ToolTipBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignChipBackground", theme.ChipBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignSnackbarBackground", theme.SnackbarBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignSnackbarMouseOver", theme.SnackbarMouseOver);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignSnackbarRipple", theme.SnackbarRipple);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextFieldBoxBackground", theme.TextFieldBoxBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextFieldBoxHoverBackground", theme.TextFieldBoxHoverBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextFieldBoxDisabledBackground", theme.TextFieldBoxDisabledBackground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextAreaBorder", theme.TextAreaBorder);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignTextAreaInactiveBorder", theme.TextAreaInactiveBorder);

            if (!(resourceDictionary.GetThemeManager() is ThemeManager themeManager))
            {
                resourceDictionary[ThemeManagerKey] = themeManager = new ThemeManager();
            }
            ITheme oldTheme = resourceDictionary.GetTheme();
            resourceDictionary[CurrentThemeKey] = theme;

            themeManager.OnThemeChange(resourceDictionary, oldTheme, theme);
        }

        public static ITheme GetTheme(this ResourceDictionary resourceDictionary)
        {
            if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));
            if (resourceDictionary[CurrentThemeKey] is ITheme theme)
            {
                return theme;
            }
            //Attempt to simply look up the appropriate resources
            return new Theme {
                PrimaryLight = new PairedColor(GetColor("PrimaryHueLightBrush"), GetColor("PrimaryHueLightForegroundBrush")),
                PrimaryMid = new PairedColor(GetColor("PrimaryHueMidBrush"), GetColor("PrimaryHueMidForegroundBrush")),
                PrimaryDark = new PairedColor(GetColor("PrimaryHueDarkBrush"), GetColor("PrimaryHueDarkForegroundBrush")),

                Accent = new PairedColor(GetColor("SecondaryAccentBrush"), GetColor("SecondaryAccentForegroundBrush")),

                ValidationError = GetColor("ValidationErrorBrush"),
                Background = GetColor("MaterialDesignBackground"),
                Paper = GetColor("MaterialDesignPaper"),
                CardBackground = GetColor("MaterialDesignCardBackground"),
                ToolBarBackground = GetColor("MaterialDesignToolBarBackground"),
                Body = GetColor("MaterialDesignBody"),
                BodyLight = GetColor("MaterialDesignBodyLight"),
                ColumnHeader = GetColor("MaterialDesignColumnHeader"),
                CheckBoxOff = GetColor("MaterialDesignCheckBoxOff"),
                CheckBoxDisabled = GetColor("MaterialDesignCheckBoxDisabled"),
                TextBoxBorder = GetColor("MaterialDesignTextBoxBorder"),
                Divider = GetColor("MaterialDesignDivider"),
                Selection = GetColor("MaterialDesignSelection"),
                FlatButtonClick = GetColor("MaterialDesignFlatButtonClick"),
                FlatButtonRipple = GetColor("MaterialDesignFlatButtonRipple"),
                ToolTipBackground = GetColor("MaterialDesignToolTipBackground"),
                ChipBackground = GetColor("MaterialDesignChipBackground"),
                SnackbarBackground = GetColor("MaterialDesignSnackbarBackground"),
                SnackbarMouseOver = GetColor("MaterialDesignSnackbarMouseOver"),
                SnackbarRipple = GetColor("MaterialDesignSnackbarRipple"),
                TextFieldBoxBackground = GetColor("MaterialDesignTextFieldBoxBackground"),
                TextFieldBoxHoverBackground = GetColor("MaterialDesignTextFieldBoxHoverBackground"),
                TextFieldBoxDisabledBackground = GetColor("MaterialDesignTextFieldBoxDisabledBackground"),
                TextAreaBorder = GetColor("MaterialDesignTextAreaBorder"),
                TextAreaInactiveBorder = GetColor("MaterialDesignTextAreaInactiveBorder")
            };


            Color GetColor(string key)
            {
                if (resourceDictionary[key] is SolidColorBrush brush)
                {
                    return brush.Color;
                }
                throw new InvalidOperationException($"Could not locate required resource with key '{key}'");
            }
        }

        public static IThemeManager GetThemeManager(this ResourceDictionary resourceDictionary)
        {
            if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));
            return resourceDictionary[ThemeManagerKey] as IThemeManager;
        }

        internal static void SetSolidColorBrush(this ResourceDictionary sourceDictionary, string name, Color value)
        {
            if (sourceDictionary == null) throw new ArgumentNullException(nameof(sourceDictionary));
            if (name == null) throw new ArgumentNullException(nameof(name));

            if (sourceDictionary[name] is SolidColorBrush brush)
            {
                if (brush.Color == value) return;

                if (!brush.IsFrozen)
                {
                    var animation = new ColorAnimation {
                        From = brush.Color,
                        To = value,
                        Duration = new Duration(TimeSpan.FromMilliseconds(300))
                    };
                    brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                    return;
                }
            }
            sourceDictionary[name] = new SolidColorBrush(value); //Set value directly
        }
    }
}