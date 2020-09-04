using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf
{
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

            SetSolidColorBrush(resourceDictionary, "SecondaryHueLightBrush", theme.SecondaryLight.Color);
            SetSolidColorBrush(resourceDictionary, "SecondaryHueLightForegroundBrush", theme.SecondaryLight.ForegroundColor ?? theme.SecondaryLight.Color.ContrastingForegroundColor());
            SetSolidColorBrush(resourceDictionary, "SecondaryHueMidBrush", theme.SecondaryMid.Color);
            SetSolidColorBrush(resourceDictionary, "SecondaryHueMidForegroundBrush", theme.SecondaryMid.ForegroundColor ?? theme.SecondaryMid.Color.ContrastingForegroundColor());
            SetSolidColorBrush(resourceDictionary, "SecondaryHueDarkBrush", theme.SecondaryDark.Color);
            SetSolidColorBrush(resourceDictionary, "SecondaryHueDarkForegroundBrush", theme.SecondaryDark.ForegroundColor ?? theme.SecondaryDark.Color.ContrastingForegroundColor());

            //NB: These are here for backwards compatibility, and will be removed in a future version.
            //These will be removed in version 4.0.0
            SetSolidColorBrush(resourceDictionary, "SecondaryAccentBrush", theme.SecondaryMid.Color);
            SetSolidColorBrush(resourceDictionary, "SecondaryAccentForegroundBrush", theme.SecondaryMid.ForegroundColor ?? theme.SecondaryMid.Color.ContrastingForegroundColor());

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
            SetSolidColorBrush(resourceDictionary, "MaterialDesignToolForeground", theme.ToolForeground);
            SetSolidColorBrush(resourceDictionary, "MaterialDesignToolBackground", theme.ToolBackground);
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
            SetSolidColorBrush(resourceDictionary, "MaterialDesignDataGridRowHoverBackground", theme.DataGridRowHoverBackground);

            if (!(resourceDictionary.GetThemeManager() is ThemeManager themeManager))
            {
                resourceDictionary[ThemeManagerKey] = themeManager = new ThemeManager(resourceDictionary);
            }
            ITheme oldTheme = resourceDictionary.GetTheme();
            resourceDictionary[CurrentThemeKey] = theme;

            themeManager.OnThemeChange(oldTheme, theme);
        }

        public static ITheme GetTheme(this ResourceDictionary resourceDictionary)
        {
            if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));
            if (resourceDictionary[CurrentThemeKey] is ITheme theme)
            {
                return theme;
            }

            Color secondaryMid = GetColor("SecondaryHueMidBrush");
            Color secondaryMidForeground = GetColor("SecondaryHueMidForegroundBrush");

            if (!TryGetColor("SecondaryHueLightBrush", out Color secondaryLight))
            {
                secondaryLight = secondaryMid.Lighten();
            }
            if (!TryGetColor("SecondaryHueLightForegroundBrush", out Color secondaryLightForeground))
            {
                secondaryLightForeground = secondaryLight.ContrastingForegroundColor();
            }

            if (!TryGetColor("SecondaryHueDarkBrush", out Color secondaryDark))
            {
                secondaryDark = secondaryMid.Darken();
            }
            if (!TryGetColor("SecondaryHueDarkForegroundBrush", out Color secondaryDarkForeground))
            {
                secondaryDarkForeground = secondaryDark.ContrastingForegroundColor();
            }

            //Attempt to simply look up the appropriate resources
            return new Theme
            {
                PrimaryLight = new ColorPair(GetColor("PrimaryHueLightBrush"), GetColor("PrimaryHueLightForegroundBrush")),
                PrimaryMid = new ColorPair(GetColor("PrimaryHueMidBrush"), GetColor("PrimaryHueMidForegroundBrush")),
                PrimaryDark = new ColorPair(GetColor("PrimaryHueDarkBrush"), GetColor("PrimaryHueDarkForegroundBrush")),

                SecondaryLight = new ColorPair(secondaryLight, secondaryLightForeground),
                SecondaryMid = new ColorPair(secondaryMid, secondaryMidForeground),
                SecondaryDark = new ColorPair(secondaryDark, secondaryDarkForeground),

                Background = GetColor("MaterialDesignBackground"),
                Body = GetColor("MaterialDesignBody"),
                BodyLight = GetColor("MaterialDesignBodyLight"),
                CardBackground = GetColor("MaterialDesignCardBackground"),
                CheckBoxDisabled = GetColor("MaterialDesignCheckBoxDisabled"),
                CheckBoxOff = GetColor("MaterialDesignCheckBoxOff"),
                ChipBackground = GetColor("MaterialDesignChipBackground"),
                ColumnHeader = GetColor("MaterialDesignColumnHeader"),
                DataGridRowHoverBackground = GetColor("MaterialDesignDataGridRowHoverBackground"),
                Divider = GetColor("MaterialDesignDivider"),
                FlatButtonClick = GetColor("MaterialDesignFlatButtonClick"),
                FlatButtonRipple = GetColor("MaterialDesignFlatButtonRipple"),
                Selection = GetColor("MaterialDesignSelection"),
                SnackbarBackground = GetColor("MaterialDesignSnackbarBackground"),
                SnackbarMouseOver = GetColor("MaterialDesignSnackbarMouseOver"),
                SnackbarRipple = GetColor("MaterialDesignSnackbarRipple"),
                TextAreaBorder = GetColor("MaterialDesignTextAreaBorder"),
                TextAreaInactiveBorder = GetColor("MaterialDesignTextAreaInactiveBorder"),
                TextBoxBorder = GetColor("MaterialDesignTextBoxBorder"),
                TextFieldBoxBackground = GetColor("MaterialDesignTextFieldBoxBackground"),
                TextFieldBoxDisabledBackground = GetColor("MaterialDesignTextFieldBoxDisabledBackground"),
                TextFieldBoxHoverBackground = GetColor("MaterialDesignTextFieldBoxHoverBackground"),
                ToolBackground = GetColor("MaterialDesignToolBackground"),
                ToolBarBackground = GetColor("MaterialDesignToolBarBackground"),
                ToolForeground = GetColor("MaterialDesignToolForeground"),
                ToolTipBackground = GetColor("MaterialDesignToolTipBackground"),
                Paper = GetColor("MaterialDesignPaper"),
                ValidationError = GetColor("ValidationErrorBrush")
            };

            Color GetColor(params string[] keys)
            {
                foreach (string key in keys)
                {
                    if (TryGetColor(key, out Color color))
                    {
                        return color;
                    }
                }
                throw new InvalidOperationException($"Could not locate required resource with key(s) '{string.Join(", ", keys)}'");
            }

            bool TryGetColor(string key, out Color color)
            {
                if (resourceDictionary[key] is SolidColorBrush brush)
                {
                    color = brush.Color;
                    return true;
                }
                color = default;
                return false;
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

            sourceDictionary[name + "Color"] = value;

            if (sourceDictionary[name] is SolidColorBrush brush)
            {
                if (brush.Color == value) return;

                if (!brush.IsFrozen)
                {
                    var animation = new ColorAnimation
                    {
                        From = brush.Color,
                        To = value,
                        Duration = new Duration(TimeSpan.FromMilliseconds(300))
                    };
                    brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                    return;
                }
            }

            var newBrush = new SolidColorBrush(value);
            newBrush.Freeze();
            sourceDictionary[name] = newBrush;
        }

        private class ThemeManager : IThemeManager
        {
            private ResourceDictionary _ResourceDictionary;

            public ThemeManager(ResourceDictionary resourceDictionary)
            {
                _ResourceDictionary = resourceDictionary ?? throw new ArgumentNullException(nameof(resourceDictionary));
            }

            public event EventHandler<ThemeChangedEventArgs> ThemeChanged;

            public void OnThemeChange(ITheme oldTheme, ITheme newTheme)
            {
                ThemeChanged?.Invoke(this, new ThemeChangedEventArgs(_ResourceDictionary, oldTheme, newTheme));
            }
        }
    }
}