using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using MahApps.Metro;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignThemes.MahApps
{
    public static class MaterialDesignAssist
    {
        public static void SetMahApps(this ResourceDictionary resourceDictionary, ITheme theme, BaseTheme baseTheme)
        {
            resourceDictionary.SetMahAppsBaseTheme(baseTheme);

            resourceDictionary.SetBrush("Theme.ShowcaseBrush", new SolidColorBrush(theme.SecondaryMid.Color));

            resourceDictionary.SetColor("MahApps.Colors.HighlightLight", theme.PrimaryLight.Color);
            resourceDictionary.SetColor("MahApps.Colors.Highlight", theme.PrimaryMid.Color);
            resourceDictionary.SetColor("MahApps.Colors.HighlightDark", theme.PrimaryDark.Color);
            resourceDictionary.SetColor("MahApps.Colors.AccentBase", theme.SecondaryDark.Color);
            resourceDictionary.SetColor("MahApps.Colors.Accent", theme.SecondaryMid.Color);
            resourceDictionary.SetColor("MahApps.Colors.Accent2", theme.SecondaryMid.Color);
            resourceDictionary.SetColor("MahApps.Colors.Accent3", theme.SecondaryLight.Color);
            resourceDictionary.SetColor("MahApps.Colors.Accent4", theme.SecondaryLight.Color);

            resourceDictionary.SetColor("MahApps.Colors.ProgressIndeterminate1", Color.FromArgb(0x33, 0x87, 0x87, 0x87));
            resourceDictionary.SetColor("MahApps.Colors.ProgressIndeterminate2", Color.FromArgb(0x33, 0x95, 0x95, 0x95));
            resourceDictionary.SetColor("MahApps.Colors.ProgressIndeterminate3", Color.FromArgb(0x4C, 0x00, 0x00, 0x00));
            resourceDictionary.SetColor("MahApps.Colors.ProgressIndeterminate4", Color.FromArgb(0x4C, 0x00, 0x00, 0x00));

            resourceDictionary.SetBrush("MahApps.Brushes.Control.Background", (Color)resourceDictionary["MahApps.Colors.ThemeBackground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.White", (Color)resourceDictionary["MahApps.Colors.ThemeBackground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Black", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Text", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Label.Text", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.WhiteColor", (Color)resourceDictionary["MahApps.Colors.ThemeBackground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.BlackColor", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);

            resourceDictionary.SetBrush("MahApps.Brushes.WindowTitle", (Color)resourceDictionary["MahApps.Colors.HighlightDark"]);
            resourceDictionary.SetBrush("MahApps.Brushes.WindowTitle.NonActive", Color.FromRgb(0x80, 0x80, 0x80));
            resourceDictionary.SetBrush("MahApps.Brushes.Border.NonActive", Color.FromRgb(0x80, 0x80, 0x80));

            resourceDictionary.SetBrush("MahApps.Brushes.Highlight", (Color)resourceDictionary["MahApps.Colors.Highlight"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DisabledWhite", (Color)resourceDictionary["MahApps.Colors.ThemeBackground"]);

            resourceDictionary.SetBrush("MahApps.Brushes.TransparentWhite", Color.FromArgb(0x00, 0xFF, 0xFF, 0xFF));
            resourceDictionary.SetBrush("MahApps.Brushes.SemiTransparentWhite", Color.FromArgb(0x55, 0xFF, 0xFF, 0xFF));
            resourceDictionary.SetBrush("MahApps.Brushes.SemiTransparentGray", Color.FromArgb(0x40, 0x80, 0x80, 0x80));
            resourceDictionary.SetBrush("MahApps.Brushes.Controls.Disabled", Color.FromArgb(0xA5, 0xFF, 0xFF, 0xFF));

            resourceDictionary.SetBrush("MahApps.Brushes.AccentBase", (Color)resourceDictionary["MahApps.Colors.AccentBase"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Accent", (Color)resourceDictionary["MahApps.Colors.Accent"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Accent2", (Color)resourceDictionary["MahApps.Colors.Accent2"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Accent3", (Color)resourceDictionary["MahApps.Colors.Accent3"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Accent4", (Color)resourceDictionary["MahApps.Colors.Accent4"]);

            resourceDictionary.SetBrush("MahApps.Brushes.Gray1", (Color)resourceDictionary["MahApps.Colors.Gray1"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Gray2", (Color)resourceDictionary["MahApps.Colors.Gray2"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Gray3", (Color)resourceDictionary["MahApps.Colors.Gray3"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Gray4", (Color)resourceDictionary["MahApps.Colors.Gray4"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Gray5", (Color)resourceDictionary["MahApps.Colors.Gray5"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Gray6", (Color)resourceDictionary["MahApps.Colors.Gray6"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Gray7", (Color)resourceDictionary["MahApps.Colors.Gray7"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Gray8", (Color)resourceDictionary["MahApps.Colors.Gray8"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Gray9", (Color)resourceDictionary["MahApps.Colors.Gray9"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Gray10", (Color)resourceDictionary["MahApps.Colors.Gray10"]);

            resourceDictionary.SetBrush("MahApps.Brushes.GrayNormal", (Color)resourceDictionary["MahApps.Colors.Gray"]);
            resourceDictionary.SetBrush("MahApps.Brushes.GrayHover", (Color)resourceDictionary["MahApps.Colors.Gray.MouseOver"]);

            resourceDictionary.SetBrush("MahApps.Brushes.Controls.Border", (Color)resourceDictionary["MahApps.Colors.Gray6"]);
            resourceDictionary.SetBrush("MahApps.Brushes.TextBox.Border", theme.TextBoxBorder);
            resourceDictionary.SetBrush("MahApps.Brushes.TextBox.MouseOverInnerBorder", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.TextBox.FocusBorder", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.TextBox.Border.MouseOver", theme.PrimaryMid.Color);

            resourceDictionary.SetBrush("MahApps.Brushes.Button.Border.MouseOver", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ComboBox.MouseOverBorder", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ComboBox.MouseOverInnerBorder", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ComboBox.PopupBorder", (Color)resourceDictionary["MahApps.Colors.Gray4"]);

            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox", (Color)resourceDictionary["MahApps.Colors.Gray6"]);
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.MouseOver", (Color)resourceDictionary["MahApps.Colors.Gray2"]);

            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.Background", new LinearGradientBrush {
                StartPoint = new Point(0.5, 0),
                EndPoint = new Point(0.5, 1),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop((Color)resourceDictionary["MahApps.Colors.Gray7"], 0),
                    new GradientStop((Color)resourceDictionary["MahApps.Colors.ThemeBackground"], 1)
                }
            });
            resourceDictionary.SetBrush("MahApps.Brushes.Thumb", (Color)resourceDictionary["MahApps.Colors.Gray5"]);

            resourceDictionary.SetBrush("MahApps.Brushes.Progress", new LinearGradientBrush {
                StartPoint = new Point(1.002, 0.5),
                EndPoint = new Point(0.001, 0.5),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop((Color)resourceDictionary["MahApps.Colors.Highlight"], 0),
                    new GradientStop((Color)resourceDictionary["MahApps.Colors.Accent3"], 1)
                }
            });
            resourceDictionary.SetBrush("MahApps.Brushes.SliderValue.Disabled", (Color)resourceDictionary["MahApps.Colors.SliderValue.Disabled"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SliderTrack.Disabled", (Color)resourceDictionary["MahApps.Colors.SliderTrack.Disabled"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SliderThumb.Disabled", (Color)resourceDictionary["MahApps.Colors.SliderThumb.Disabled"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SliderTrack.Hover", (Color)resourceDictionary["MahApps.Colors.SliderTrack.Hover"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SliderTrack.Normal", (Color)resourceDictionary["MahApps.Colors.SliderTrack.Normal"]);

            resourceDictionary.SetBrush("MahApps.Brushes.Flyout.Background", (Color)resourceDictionary["MahApps.Colors.Flyout"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Flyout.Foreground", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);

            resourceDictionary.SetBrush("MahApps.Brushes.Window.FlyoutOverlay", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Window.Background", (Color)resourceDictionary["MahApps.Colors.ThemeBackground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Separator", Color.FromArgb(0xFF, 0xC4, 0xC4, 0xC5));

            resourceDictionary.SetBrush("MahApps.Brushes.FlatButton.Background", Color.FromRgb(0xD5, 0xD5, 0xD5));
            resourceDictionary.SetBrush("MahApps.Brushes.FlatButton.Foreground", Color.FromRgb(0x22, 0x22, 0x22));
            resourceDictionary.SetBrush("MahApps.Brushes.FlatButton.PressedBackground", (Color)resourceDictionary["MahApps.Colors.Button.Flat.Background.Pressed"]);
            resourceDictionary.SetBrush("MahApps.Brushes.FlatButton.PressedForeground", Colors.White);

            resourceDictionary.SetBrush("MahApps.Brushes.DarkIdealForegroundDisabled", Color.FromRgb(0xAD, 0xAD, 0xAD));
            resourceDictionary.SetBrush("MahApps.Brushes.CleanWindowCloseButton.Background", Color.FromRgb(0xEB, 0x2F, 0x2F));
            resourceDictionary.SetBrush("MahApps.Brushes.CleanWindowCloseButton.PressedBackground", Color.FromRgb(0x7C, 0x00, 0x00));

            //CONTROL VALIDATION BRUSHES
            resourceDictionary.SetBrush("MahApps.Brushes.Controls.Validation", Color.FromArgb(0xFF, 0xDB, 0x00, 0x0C));
            resourceDictionary.SetBrush("MahApps.Brushes.Validation1", Color.FromArgb(0x05, 0x2A, 0x2E, 0x31));
            resourceDictionary.SetBrush("MahApps.Brushes.Validation2", Color.FromArgb(0x15, 0x2A, 0x2E, 0x31));
            resourceDictionary.SetBrush("MahApps.Brushes.Validation3", Color.FromArgb(0x25, 0x2A, 0x2E, 0x31));
            resourceDictionary.SetBrush("MahApps.Brushes.Validation4", Color.FromArgb(0x35, 0x2A, 0x2E, 0x31));
            resourceDictionary.SetBrush("MahApps.Brushes.Validation5", Color.FromArgb(0xFF, 0xDC, 0x00, 0x0C));
            //unused
            resourceDictionary.SetBrush("MahApps.Brushes.ValidationSummary1", Color.FromArgb(0xFF, 0xDC, 0x02, 0x0D));
            resourceDictionary.SetBrush("MahApps.Brushes.ValidationSummary2", Color.FromArgb(0xFF, 0xCA, 0x00, 0x0C));
            resourceDictionary.SetBrush("MahApps.Brushes.ValidationSummary3", Color.FromArgb(0xFF, 0xFF, 0x92, 0x98));
            resourceDictionary.SetBrush("MahApps.Brushes.ValidationSummary4", Color.FromArgb(0xFF, 0xFD, 0xC8, 0xC8));
            resourceDictionary.SetBrush("MahApps.Brushes.ValidationSummary5", Color.FromArgb(0xDD, 0xD4, 0x39, 0x40));
            resourceDictionary.SetBrush("MahApps.Brushes.ValidationSummaryFill1", Color.FromArgb(0x59, 0xF7, 0xD8, 0xDB));
            resourceDictionary.SetBrush("MahApps.Brushes.ValidationSummaryFill2", Color.FromArgb(0xFF, 0xF7, 0xD8, 0xDB));
            //validation text foreground always white
            resourceDictionary.SetBrush("MahApps.Brushes.Text.Validation", Colors.White);

            //WPF default colors
            resourceDictionary.SetBrush("{x:Static SystemColors.WindowBrushKey}", (Color)resourceDictionary["MahApps.Colors.ThemeBackground"]);
            resourceDictionary.SetBrush("{x:Static SystemColors.ControlTextBrushKey}", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);

            //menu default colors
            resourceDictionary.SetBrush("MahApps.Brushes.Menu.Background", (Color)resourceDictionary["MahApps.Colors.ThemeBackground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ContextMenu.Background", (Color)resourceDictionary["MahApps.Colors.ThemeBackground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SubMenu.Background", (Color)resourceDictionary["MahApps.Colors.ThemeBackground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.MenuItem.Background", (Color)resourceDictionary["MahApps.Colors.ThemeBackground"]);

            resourceDictionary.SetBrush("MahApps.Brushes.ContextMenu.Border", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SubMenu.Border", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);

            resourceDictionary.SetBrush("MahApps.Brushes.MenuItem.SelectionFill", (Color)resourceDictionary["MahApps.Colors.MenuItem.SelectionFill"]);
            resourceDictionary.SetBrush("MahApps.Brushes.MenuItem.SelectionStroke", (Color)resourceDictionary["MahApps.Colors.MenuItem.SelectionStroke"]);

            resourceDictionary.SetBrush("MahApps.Brushes.TopMenuItem.PressedFill", (Color)resourceDictionary["MahApps.Colors.TopMenuItem.PressedFill"]);
            resourceDictionary.SetBrush("MahApps.Brushes.TopMenuItem.PressedStroke", (Color)resourceDictionary["MahApps.Colors.TopMenuItem.PressedStroke"]);
            resourceDictionary.SetBrush("MahApps.Brushes.TopMenuItem.SelectionStroke", (Color)resourceDictionary["MahApps.Colors.TopMenuItem.SelectionStroke"]);

            resourceDictionary.SetBrush("MahApps.Brushes.MenuItem.DisabledForeground", (Color)resourceDictionary["MahApps.Colors.MenuItem.DisabledForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.MenuItem.DisabledGlyphPanel", Color.FromRgb(0x84, 0x85, 0x89));

            resourceDictionary.SetBrush("{x:Static SystemColors.MenuTextBrushKey}", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);

            resourceDictionary.SetBrush("MahApps.Brushes.CheckmarkFill", (Color)resourceDictionary["MahApps.Colors.Accent"]);
            resourceDictionary.SetBrush("MahApps.Brushes.RightArrowFill", (Color)resourceDictionary["MahApps.Colors.Accent"]);

            resourceDictionary.SetColor("MahApps.Colors.IdealForeground", theme.PrimaryMid.GetForegroundColor());
            resourceDictionary.SetBrush("MahApps.Brushes.IdealForeground", theme.PrimaryMid.GetForegroundColor());
            resourceDictionary.SetBrush("MahApps.Brushes.IdealForegroundDisabled", (Color)resourceDictionary["MahApps.Colors.IdealForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.AccentSelectedColor", (Color)resourceDictionary["MahApps.Colors.IdealForeground"]);

            //DataGrid brushes

            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.Highlight", (Color)resourceDictionary["MahApps.Colors.Accent"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.DisabledHighlight", (Color)resourceDictionary["MahApps.Colors.Gray7"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.HighlightText", (Color)resourceDictionary["MahApps.Colors.IdealForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.MouseOverHighlight", (Color)resourceDictionary["MahApps.Colors.Accent3"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.FocusBorder", (Color)resourceDictionary["MahApps.Colors.Accent"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.InactiveSelectionHighlight", (Color)resourceDictionary["MahApps.Colors.Accent2"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.InactiveSelectionHighlightText", (Color)resourceDictionary["MahApps.Colors.IdealForeground"]);

            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitchButton.Pressed.Win10", (Color)resourceDictionary["MahApps.Colors.ToggleSwitchButton.Pressed.Win10"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitchButton.OffBorder.Win10", (Color)resourceDictionary["MahApps.Colors.ToggleSwitchButton.OffBorder.Win10"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitchButton.OffMouseOverBorder.Win10", (Color)resourceDictionary["MahApps.Colors.ToggleSwitchButton.OffMouseOverBorder.Win10"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitchButton.OffDisabledBorder.Win10", (Color)resourceDictionary["MahApps.Colors.ToggleSwitchButton.OffDisabledBorder.Win10"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitchButton.OffSwitch.Win10", Colors.Transparent);
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitchButton.OnSwitch.Win10", (Color)resourceDictionary["MahApps.Colors.Accent"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitchButton.OnSwitchDisabled.Win10", (Color)resourceDictionary["MahApps.Colors.ToggleSwitchButton.OnSwitchDisabled.Win10"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitchButton.OnSwitchMouseOver.Win10", (Color)resourceDictionary["MahApps.Colors.Accent2"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitchButton.ThumbIndicator.Win10", (Color)resourceDictionary["MahApps.Colors.ToggleSwitchButton.ThumbIndicator.Win10"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitchButton.ThumbIndicatorMouseOver.Win10", (Color)resourceDictionary["MahApps.Colors.ToggleSwitchButton.ThumbIndicatorMouseOver.Win10"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitchButton.ThumbIndicatorChecked.Win10", (Color)resourceDictionary["MahApps.Colors.IdealForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitchButton.ThumbIndicatorPressed.Win10", (Color)resourceDictionary["MahApps.Colors.ToggleSwitchButton.ThumbIndicatorPressed.Win10"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitchButton.ThumbIndicatorDisabled.Win10", (Color)resourceDictionary["MahApps.Colors.ToggleSwitchButton.ThumbIndicatorDisabled.Win10"]);

            resourceDictionary.SetBrush("MahApps.Brushes.Badged.DisabledBackground", (Color)resourceDictionary["MahApps.Colors.Badged.Background.Disabled"]);

            //HamburgerMenu

            resourceDictionary.SetBrush("MahApps.HamburgerMenu.PaneBackground", Color.FromArgb(0xFF, 0x44, 0x44, 0x44));
            resourceDictionary.SetBrush("MahApps.HamburgerMenu.PaneForeground", Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
        }

        private static void SetBrush(this ResourceDictionary resourceDictionary, string name, Color value, double opacity = 1.0)
        {
            if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));
            if (name == null) throw new ArgumentNullException(nameof(name));

            if (resourceDictionary[name] is SolidColorBrush brush)
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
            resourceDictionary[name] = new SolidColorBrush(value) { Opacity = opacity }; //Set value directly
        }

        private static void SetBrush(this ResourceDictionary resourceDictionary, string name, Brush value)
        {
            if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));
            if (name == null) throw new ArgumentNullException(nameof(name));

            resourceDictionary[name] = value; //Set value directly
        }

        private static void SetColor(this ResourceDictionary resourceDictionary, string key, Color color)
            => resourceDictionary[key] = color;

        internal static void SetMahAppsBaseTheme(this ResourceDictionary resourceDictionary, BaseTheme baseTheme)
        {
            var foo = ThemeManager.Themes;
            switch (baseTheme)
            {
                case BaseTheme.Light:
                    resourceDictionary["Theme.BaseColorScheme"] = ThemeManager.BaseColorLight;
                    resourceDictionary.SetColor("MahApps.Colors.ThemeForeground", Color.FromArgb(0xFF, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.ThemeForeground20", Color.FromArgb(0x51, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.ThemeBackground", Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.ThemeBackground20", Color.FromArgb(0x51, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.Gray1", Color.FromArgb(0xFF, 0x33, 0x33, 0x33));
                    resourceDictionary.SetColor("MahApps.Colors.Gray2", Color.FromArgb(0xFF, 0x7F, 0x7F, 0x7F));
                    resourceDictionary.SetColor("MahApps.Colors.Gray3", Color.FromArgb(0xFF, 0x9D, 0x9D, 0x9D));
                    resourceDictionary.SetColor("MahApps.Colors.Gray4", Color.FromArgb(0xFF, 0xA5, 0x9F, 0x93));
                    resourceDictionary.SetColor("MahApps.Colors.Gray5", Color.FromArgb(0xFF, 0xB9, 0xB9, 0xB9));
                    resourceDictionary.SetColor("MahApps.Colors.Gray6", Color.FromArgb(0xFF, 0xCC, 0xCC, 0xCC));
                    resourceDictionary.SetColor("MahApps.Colors.Gray7", Color.FromArgb(0xFF, 0xD8, 0xD8, 0xD9));
                    resourceDictionary.SetColor("MahApps.Colors.Gray8", Color.FromArgb(0xFF, 0xE0, 0xE0, 0xE0));
                    resourceDictionary.SetColor("MahApps.Colors.Gray9", Color.FromArgb(0x5E, 0xC9, 0xC9, 0xC9));
                    resourceDictionary.SetColor("MahApps.Colors.Gray10", Color.FromArgb(0xFF, 0xF7, 0xF7, 0xF7));
                    resourceDictionary.SetColor("MahApps.Colors.Gray", Color.FromArgb(0xFF, 0xBE, 0xBE, 0xBE));
                    resourceDictionary.SetColor("MahApps.Colors.Gray.MouseOver", Color.FromArgb(0xFF, 0x33, 0x33, 0x33));
                    resourceDictionary.SetColor("MahApps.Colors.Gray.SemiTransparent", Color.FromArgb(0x40, 0x80, 0x80, 0x80));
                    resourceDictionary.SetColor("MahApps.Colors.SemiTransparent", Color.FromArgb(0x55, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.Flyout", Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.MenuShadow", Color.FromArgb(0xFF, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.MenuItem.DisabledForeground", Color.FromArgb(0xFF, 0x7F, 0x7F, 0x7F));
                    resourceDictionary.SetColor("MahApps.Colors.SliderValue.Disabled", Color.FromArgb(0xFF, 0xBA, 0xBA, 0xBA));
                    resourceDictionary.SetColor("MahApps.Colors.SliderTrack.Disabled", Color.FromArgb(0xFF, 0xDB, 0xDB, 0xDB));
                    resourceDictionary.SetColor("MahApps.Colors.SliderThumb.Disabled", Color.FromArgb(0xFF, 0xA0, 0xA0, 0xA0));
                    resourceDictionary.SetColor("MahApps.Colors.SliderTrack.Hover", Color.FromArgb(0xFF, 0xD0, 0xD0, 0xD0));
                    resourceDictionary.SetColor("MahApps.Colors.SliderTrack.Normal", Color.FromArgb(0xFF, 0xC6, 0xC6, 0xC6));
                    resourceDictionary.SetColor("MahApps.Colors.Button.Flat.Background.Pressed", Color.FromRgb(0x33, 0x33, 0x33));
                    resourceDictionary.SetColor("MahApps.Colors.Button.Flat.Foreground.Pressed", Color.FromRgb(0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.MenuItem.SelectionFill", Color.FromRgb(0xDE, 0xDE, 0xDE));
                    resourceDictionary.SetColor("MahApps.Colors.MenuItem.SelectionStroke", Color.FromRgb(0xDE, 0xDE, 0xDE));
                    resourceDictionary.SetColor("MahApps.Colors.TopMenuItem.PressedFill", Color.FromRgb(0xDE, 0xDE, 0xDE));
                    resourceDictionary.SetColor("MahApps.Colors.TopMenuItem.PressedStroke", Color.FromArgb(0xE0, 0x71, 0x70, 0x70));
                    resourceDictionary.SetColor("MahApps.Colors.TopMenuItem.SelectionStroke", Color.FromArgb(0x90, 0x71, 0x70, 0x70));
                    resourceDictionary.SetColor("MahApps.Colors.Badged.Background.Disabled", Color.FromArgb(0xFF, 0x99, 0x99, 0x99));
                    resourceDictionary.SetColor("MahApps.Colors.Badged.Foreground.Disabled", Color.FromArgb(0x99, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemAltHigh", Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.SystemAltLow", Color.FromArgb(0x33, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.SystemAltMedium", Color.FromArgb(0x99, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.SystemAltMediumHigh", Color.FromArgb(0xCC, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.SystemAltMediumLow", Color.FromArgb(0x66, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.SystemBaseHigh", Color.FromArgb(0xFF, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemBaseLow", Color.FromArgb(0x33, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemBaseMedium", Color.FromArgb(0x99, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemBaseMediumHigh", Color.FromArgb(0xCC, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemBaseMediumLow", Color.FromArgb(0x66, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeAltLow", Color.FromArgb(0xFF, 0x17, 0x17, 0x17));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeBlackHigh", Color.FromArgb(0xFF, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeBlackLow", Color.FromArgb(0x33, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeBlackMediumLow", Color.FromArgb(0x66, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeBlackMedium", Color.FromArgb(0xCC, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeDisabledHigh", Color.FromArgb(0xFF, 0xCC, 0xCC, 0xCC));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeDisabledLow", Color.FromArgb(0xFF, 0x7A, 0x7A, 0x7A));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeHigh", Color.FromArgb(0xFF, 0xCC, 0xCC, 0xCC));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeLow", Color.FromArgb(0xFF, 0xF2, 0xF2, 0xF2));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeMedium", Color.FromArgb(0xFF, 0xE6, 0xE6, 0xE6));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeMediumLow", Color.FromArgb(0xFF, 0xF2, 0xF2, 0xF2));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeWhite", Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeGray", Color.FromArgb(0xFF, 0x76, 0x76, 0x76));
                    resourceDictionary.SetColor("MahApps.Colors.SystemListLow", Color.FromArgb(0x19, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemListMedium", Color.FromArgb(0x33, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemErrorText", Color.FromRgb(0xC5, 0x05, 0x00));
                    break;

                case BaseTheme.Dark:
                    resourceDictionary["Theme.BaseColorScheme"] = ThemeManager.BaseColorDark;
                    resourceDictionary.SetColor("MahApps.Colors.ThemeForeground", Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.ThemeForeground20", Color.FromArgb(0x51, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.ThemeBackground", Color.FromArgb(0xFF, 0x25, 0x25, 0x25));
                    resourceDictionary.SetColor("MahApps.Colors.ThemeBackground20", Color.FromArgb(0x51, 0x25, 0x25, 0x25));
                    resourceDictionary.SetColor("MahApps.Colors.Gray1", Color.FromArgb(0xFF, 0xF9, 0xF9, 0xF9));
                    resourceDictionary.SetColor("MahApps.Colors.Gray2", Color.FromArgb(0xFF, 0x7F, 0x7F, 0x7F));
                    resourceDictionary.SetColor("MahApps.Colors.Gray3", Color.FromArgb(0xFF, 0x9D, 0x9D, 0x9D));
                    resourceDictionary.SetColor("MahApps.Colors.Gray4", Color.FromArgb(0xFF, 0xA5, 0x9F, 0x93));
                    resourceDictionary.SetColor("MahApps.Colors.Gray5", Color.FromArgb(0xFF, 0xB9, 0xB9, 0xB9));
                    resourceDictionary.SetColor("MahApps.Colors.Gray6", Color.FromArgb(0xFF, 0xCC, 0xCC, 0xCC));
                    resourceDictionary.SetColor("MahApps.Colors.Gray7", Color.FromArgb(0xFF, 0x7E, 0x7E, 0x7E));
                    resourceDictionary.SetColor("MahApps.Colors.Gray8", Color.FromArgb(0xFF, 0x45, 0x45, 0x45));
                    resourceDictionary.SetColor("MahApps.Colors.Gray9", Color.FromArgb(0x5E, 0xC9, 0xC9, 0xC9));
                    resourceDictionary.SetColor("MahApps.Colors.Gray10", Color.FromArgb(0xFF, 0x2F, 0x2F, 0x2F));
                    resourceDictionary.SetColor("MahApps.Colors.Gray", Color.FromArgb(0xFF, 0x7D, 0x7D, 0x7D));
                    resourceDictionary.SetColor("MahApps.Colors.Gray.MouseOver", Color.FromArgb(0xFF, 0xAA, 0xAA, 0xAA));
                    resourceDictionary.SetColor("MahApps.Colors.Gray.SemiTransparent", Color.FromArgb(0x40, 0x80, 0x80, 0x80));
                    resourceDictionary.SetColor("MahApps.Colors.SemiTransparent", Color.FromArgb(0x55, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.Flyout", Color.FromArgb(0xFF, 0x2B, 0x2B, 0x2B));
                    resourceDictionary.SetColor("MahApps.Colors.MenuShadow", Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.MenuItem.DisabledForeground", Color.FromArgb(0xFF, 0x7E, 0x7E, 0x7E));
                    resourceDictionary.SetColor("MahApps.Colors.SliderValue.Disabled", Color.FromArgb(0xFF, 0x53, 0x53, 0x53));
                    resourceDictionary.SetColor("MahApps.Colors.SliderTrack.Disabled", Color.FromArgb(0xFF, 0x38, 0x38, 0x38));
                    resourceDictionary.SetColor("MahApps.Colors.SliderThumb.Disabled", Color.FromArgb(0xFF, 0x7E, 0x7E, 0x7E));
                    resourceDictionary.SetColor("MahApps.Colors.SliderTrack.Hover", Color.FromArgb(0xFF, 0x73, 0x73, 0x73));
                    resourceDictionary.SetColor("MahApps.Colors.SliderTrack.Normal", Color.FromArgb(0xFF, 0x6C, 0x6C, 0x6C));
                    resourceDictionary.SetColor("MahApps.Colors.Button.Flat.Background.Pressed", Color.FromRgb(0x44, 0x44, 0x44));
                    resourceDictionary.SetColor("MahApps.Colors.Button.Flat.Foreground.Pressed", Color.FromRgb(0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.MenuItem.SelectionFill", Color.FromRgb(0x31, 0x31, 0x31));
                    resourceDictionary.SetColor("MahApps.Colors.MenuItem.SelectionStroke", Color.FromRgb(0x31, 0x31, 0x31));
                    resourceDictionary.SetColor("MahApps.Colors.TopMenuItem.PressedFill", Color.FromRgb(0x31, 0x31, 0x31));
                    resourceDictionary.SetColor("MahApps.Colors.TopMenuItem.PressedStroke", Color.FromArgb(0xE0, 0x71, 0x70, 0x70));
                    resourceDictionary.SetColor("MahApps.Colors.TopMenuItem.SelectionStroke", Color.FromArgb(0x90, 0x71, 0x70, 0x70));
                    resourceDictionary.SetColor("MahApps.Colors.Badged.Background.Disabled", Color.FromArgb(0xFF, 0x66, 0x66, 0x66));
                    resourceDictionary.SetColor("MahApps.Colors.Badged.Foreground.Disabled", Color.FromArgb(0x99, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemAltHigh", Color.FromArgb(0xFF, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemAltLow", Color.FromArgb(0x33, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemAltMedium", Color.FromArgb(0x99, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemAltMediumHigh", Color.FromArgb(0xCC, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemAltMediumLow", Color.FromArgb(0x66, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemBaseHigh", Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.SystemBaseLow", Color.FromArgb(0x33, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.SystemBaseMedium", Color.FromArgb(0x99, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.SystemBaseMediumHigh", Color.FromArgb(0xCC, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.SystemBaseMediumLow", Color.FromArgb(0x66, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeAltLow", Color.FromArgb(0xFF, 0xF2, 0xF2, 0xF2));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeBlackHigh", Color.FromArgb(0xFF, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeBlackLow", Color.FromArgb(0x33, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeBlackMediumLow", Color.FromArgb(0x66, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeBlackMedium", Color.FromArgb(0xCC, 0x00, 0x00, 0x00));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeDisabledHigh", Color.FromArgb(0xFF, 0x33, 0x33, 0x33));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeDisabledLow", Color.FromArgb(0xFF, 0x85, 0x85, 0x85));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeHigh", Color.FromArgb(0xFF, 0x76, 0x76, 0x76));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeLow", Color.FromArgb(0xFF, 0x17, 0x17, 0x17));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeMedium", Color.FromArgb(0xFF, 0x1F, 0x1F, 0x1F));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeMediumLow", Color.FromArgb(0xFF, 0x2B, 0x2B, 0x2B));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeWhite", Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.SystemChromeGray", Color.FromArgb(0xFF, 0x76, 0x76, 0x76));
                    resourceDictionary.SetColor("MahApps.Colors.SystemListLow", Color.FromArgb(0x19, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.SystemListMedium", Color.FromArgb(0x33, 0xFF, 0xFF, 0xFF));
                    resourceDictionary.SetColor("MahApps.Colors.SystemErrorText", Color.FromRgb(0xFF, 0xF0, 0x00));
                    break;
            }
        }

    }
}
