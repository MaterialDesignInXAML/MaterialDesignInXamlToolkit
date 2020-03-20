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

            resourceDictionary.SetBrush("MahApps.Brushes.ThemeBackground", (Color)resourceDictionary["MahApps.Colors.ThemeBackground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ThemeForeground", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Text", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);

            resourceDictionary.SetColor("MahApps.Colors.IdealForeground", theme.PrimaryMid.GetForegroundColor());
            resourceDictionary.SetBrush("MahApps.Brushes.IdealForeground", theme.PrimaryMid.GetForegroundColor());
            resourceDictionary.SetBrush("MahApps.Brushes.IdealForegroundDisabled", Color.FromRgb(0xAD, 0xAD, 0xAD));
            resourceDictionary.SetBrush("MahApps.Brushes.Selected.Foreground", (Color)resourceDictionary["MahApps.Colors.IdealForeground"]);

            resourceDictionary.SetBrush("MahApps.Brushes.WindowTitle", (Color)resourceDictionary["MahApps.Colors.HighlightDark"]);
            resourceDictionary.SetBrush("MahApps.Brushes.WindowTitle.NonActive", Color.FromRgb(0x80, 0x80, 0x80));
            resourceDictionary.SetBrush("MahApps.Brushes.Border.NonActive", Color.FromRgb(0x80, 0x80, 0x80));

            resourceDictionary.SetBrush("MahApps.Brushes.Highlight", (Color)resourceDictionary["MahApps.Colors.Highlight"]);

            resourceDictionary.SetBrush("MahApps.Brushes.Transparent", Colors.Transparent);
            resourceDictionary.SetBrush("MahApps.Brushes.SemiTransparent", (Color)resourceDictionary["MahApps.Colors.SemiTransparent"]);

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

            resourceDictionary.SetBrush("MahApps.Brushes.Gray", (Color)resourceDictionary["MahApps.Colors.Gray"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Gray.MouseOver", (Color)resourceDictionary["MahApps.Colors.Gray.MouseOver"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Gray.SemiTransparent", (Color)resourceDictionary["MahApps.Colors.Gray.SemiTransparent"]);

            resourceDictionary.SetBrush("MahApps.Brushes.TextBox.Border", theme.TextBoxBorder);
            resourceDictionary.SetBrush("MahApps.Brushes.TextBox.Border.Focus", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.TextBox.Border.MouseOver", theme.PrimaryMid.Color);

            resourceDictionary.SetBrush("MahApps.Brushes.Control.Background", (Color)resourceDictionary["MahApps.Colors.ThemeBackground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Control.Border", (Color)resourceDictionary["MahApps.Colors.Gray6"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Control.Disabled", Color.FromArgb(0xA5, 0xFF, 0xFF, 0xFF));
            resourceDictionary.SetBrush("MahApps.Brushes.Control.Validation", Color.FromArgb(0xFF, 0xDB, 0x00, 0x0C));

            resourceDictionary.SetBrush("MahApps.Brushes.Button.Border", (Color)resourceDictionary["MahApps.Colors.Gray6"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Button.Border.Focus", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Button.Border.MouseOver", (Color)resourceDictionary["MahApps.Colors.Gray6"]);

            resourceDictionary.SetBrush("MahApps.Brushes.ComboBox.Border.MouseOver", (Color)resourceDictionary["MahApps.Colors.Gray2"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ComboBox.Border.Focus", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.ComboBox.PopupBorder", (Color)resourceDictionary["MahApps.Colors.Gray4"]);

            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox", (Color)resourceDictionary["MahApps.Colors.Gray5"]);
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.MouseOver", (Color)resourceDictionary["MahApps.Colors.Gray2"]);

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

            resourceDictionary.SetBrush("MahApps.Brushes.Window.FlyoutOverlay", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"], 0.5);
            resourceDictionary.SetBrush("MahApps.Brushes.Window.Background", (Color)resourceDictionary["MahApps.Colors.ThemeBackground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Separator", Color.FromArgb(0xFF, 0xC4, 0xC4, 0xC5));

            resourceDictionary.SetBrush("MahApps.Brushes.Button.Flat.Background", Color.FromRgb(0xD5, 0xD5, 0xD5));
            resourceDictionary.SetBrush("MahApps.Brushes.Button.Flat.Foreground", Color.FromRgb(0x22, 0x22, 0x22));
            resourceDictionary.SetBrush("MahApps.Brushes.Button.Flat.Background.MouseOver", Color.FromRgb(0xA9, 0xA9, 0xA9));
            resourceDictionary.SetBrush("MahApps.Brushes.Button.Flat.Background.Pressed", (Color)resourceDictionary["MahApps.Colors.Button.Flat.Background.Pressed"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Button.Flat.Foreground.Pressed", (Color)resourceDictionary["MahApps.Colors.Button.Flat.Foreground.Pressed"]);

            resourceDictionary.SetBrush("MahApps.Brushes.Button.CleanWindow.Close.Background.MouseOver", Color.FromRgb(0xEB, 0x2F, 0x2F));
            resourceDictionary.SetBrush("MahApps.Brushes.Button.CleanWindow.Close.Foreground.MouseOver", Color.FromRgb(0xFF, 0xFF, 0xFF));
            resourceDictionary.SetBrush("MahApps.Brushes.Button.CleanWindow.Close.Background.Pressed", Color.FromRgb(0x7C, 0x00, 0x00));

            resourceDictionary.SetBrush("MahApps.Brushes.Button.Square.Background.MouseOver", (Color)resourceDictionary["MahApps.Colors.Gray8"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Button.Square.Foreground.MouseOver", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Button.AccentedSquare.Background.MouseOver", Color.FromArgb(0x66, 0x00, 0x00, 0x00));
            resourceDictionary.SetBrush("MahApps.Brushes.Button.AccentedSquare.Foreground.MouseOver", (Color)resourceDictionary["MahApps.Colors.IdealForeground"]);

            //CONTROL VALIDATION BRUSHES
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

            resourceDictionary.SetBrush("MahApps.Brushes.MenuItem.Foreground.Disabled", (Color)resourceDictionary["MahApps.Colors.MenuItem.DisabledForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.MenuItem.GlyphPanel.Disabled", Color.FromRgb(0x84, 0x85, 0x89));

            resourceDictionary.SetBrush("{x:Static SystemColors.MenuTextBrushKey}", (Color)resourceDictionary["MahApps.Colors.ThemeForeground"]);

            resourceDictionary.SetBrush("MahApps.Brushes.CheckmarkFill", (Color)resourceDictionary["MahApps.Colors.Accent"]);
            resourceDictionary.SetBrush("MahApps.Brushes.RightArrowFill", (Color)resourceDictionary["MahApps.Colors.Accent"]);

            resourceDictionary.SetBrush("MahApps.Brushes.WindowButtonCommands.Background.MouseOver", Color.FromArgb(0x66, 0xDC, 0xDC, 0xDC));

            //DataGrid brushes

            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.Selection.Background", (Color)resourceDictionary["MahApps.Colors.Accent"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.Selection.BorderBrush", (Color)resourceDictionary["MahApps.Colors.Accent"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.Selection.Background.Disabled", (Color)resourceDictionary["MahApps.Colors.Gray7"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.Selection.BorderBrush.Disabled", (Color)resourceDictionary["MahApps.Colors.Gray7"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.Selection.Text", (Color)resourceDictionary["MahApps.Colors.IdealForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.Selection.Text.Disabled", (Color)resourceDictionary["MahApps.Colors.IdealForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.Selection.Background.MouseOver", (Color)resourceDictionary["MahApps.Colors.Accent3"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.Selection.BorderBrush.MouseOver", (Color)resourceDictionary["MahApps.Colors.Accent3"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.Selection.BorderBrush.Focus", (Color)resourceDictionary["MahApps.Colors.Accent"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.Selection.Background.Inactive", (Color)resourceDictionary["MahApps.Colors.Accent2"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.Selection.BorderBrush.Inactive", (Color)resourceDictionary["MahApps.Colors.Accent2"]);
            resourceDictionary.SetBrush("MahApps.Brushes.DataGrid.Selection.Text.Inactive", (Color)resourceDictionary["MahApps.Colors.IdealForeground"]);

            resourceDictionary.SetBrush("MahApps.Brushes.Badged.Background", (Color)resourceDictionary["MahApps.Colors.AccentBase"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Badged.Background.Disabled", (Color)resourceDictionary["MahApps.Colors.Badged.Background.Disabled"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Badged.Foreground", (Color)resourceDictionary["MahApps.Colors.IdealForeground"]);
            resourceDictionary.SetBrush("MahApps.Brushes.Badged.Foreground.Disabled", (Color)resourceDictionary["MahApps.Colors.Badged.Foreground.Disabled"]);

            //HamburgerMenu

            resourceDictionary.SetBrush("MahApps.HamburgerMenu.Pane.Background", Color.FromArgb(0xFF, 0x44, 0x44, 0x44));
            resourceDictionary.SetBrush("MahApps.HamburgerMenu.Pane.Foreground", Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));

            // DEFAULT COMMON CONTROL COLORS Win UWP

            resourceDictionary.SetColor("MahApps.Colors.SystemAccent", theme.SecondaryDark.Color);

            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundAccent", (Color)resourceDictionary["MahApps.Colors.SystemAccent"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundAltHigh", (Color)resourceDictionary["MahApps.Colors.SystemAltHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundAltMedium", (Color)resourceDictionary["MahApps.Colors.SystemAltMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundAltMediumHigh", (Color)resourceDictionary["MahApps.Colors.SystemAltMediumHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundAltMediumLow", (Color)resourceDictionary["MahApps.Colors.SystemAltMediumLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundBaseHigh", (Color)resourceDictionary["MahApps.Colors.SystemBaseHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundBaseLow", (Color)resourceDictionary["MahApps.Colors.SystemBaseLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundBaseMedium", (Color)resourceDictionary["MahApps.Colors.SystemBaseMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundBaseMediumHigh", (Color)resourceDictionary["MahApps.Colors.SystemBaseMediumHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundBaseMediumLow", (Color)resourceDictionary["MahApps.Colors.SystemBaseMediumLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundChromeBlackHigh", (Color)resourceDictionary["MahApps.Colors.SystemChromeBlackHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundChromeBlackLow", (Color)resourceDictionary["MahApps.Colors.SystemChromeBlackLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundChromeBlackMedium", (Color)resourceDictionary["MahApps.Colors.SystemChromeBlackMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundChromeBlackMediumLow", (Color)resourceDictionary["MahApps.Colors.SystemChromeBlackMediumLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundChromeMedium", (Color)resourceDictionary["MahApps.Colors.SystemChromeMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundChromeMediumLow", (Color)resourceDictionary["MahApps.Colors.SystemChromeMediumLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundChromeWhite", (Color)resourceDictionary["MahApps.Colors.SystemChromeWhite"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundListLow", (Color)resourceDictionary["MahApps.Colors.SystemListLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlBackgroundListMedium", (Color)resourceDictionary["MahApps.Colors.SystemListMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlDisabledAccent", (Color)resourceDictionary["MahApps.Colors.SystemAccent"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlDisabledBaseHigh", (Color)resourceDictionary["MahApps.Colors.SystemBaseHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlDisabledBaseLow", (Color)resourceDictionary["MahApps.Colors.SystemBaseLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlDisabledBaseMediumLow", (Color)resourceDictionary["MahApps.Colors.SystemBaseMediumLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlDisabledChromeDisabledHigh", (Color)resourceDictionary["MahApps.Colors.SystemChromeDisabledHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlDisabledChromeDisabledLow", (Color)resourceDictionary["MahApps.Colors.SystemChromeDisabledLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlDisabledChromeHigh", (Color)resourceDictionary["MahApps.Colors.SystemChromeHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlDisabledChromeMediumLow", (Color)resourceDictionary["MahApps.Colors.SystemChromeMediumLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlDisabledListMedium", (Color)resourceDictionary["MahApps.Colors.SystemListMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlDisabledTransparent", Colors.Transparent);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlErrorTextForeground", (Color)resourceDictionary["MahApps.Colors.SystemErrorText"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlFocusVisualPrimary", (Color)resourceDictionary["MahApps.Colors.SystemBaseHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlFocusVisualSecondary", (Color)resourceDictionary["MahApps.Colors.SystemAltMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundAccent", (Color)resourceDictionary["MahApps.Colors.SystemAccent"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundAltHigh", (Color)resourceDictionary["MahApps.Colors.SystemAltHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundAltMediumHigh", (Color)resourceDictionary["MahApps.Colors.SystemAltMediumHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundBaseHigh", (Color)resourceDictionary["MahApps.Colors.SystemBaseHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundBaseLow", (Color)resourceDictionary["MahApps.Colors.SystemBaseLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundBaseMedium", (Color)resourceDictionary["MahApps.Colors.SystemBaseMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundBaseMediumHigh", (Color)resourceDictionary["MahApps.Colors.SystemBaseMediumHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundBaseMediumLow", (Color)resourceDictionary["MahApps.Colors.SystemBaseMediumLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundChromeBlackHigh", (Color)resourceDictionary["MahApps.Colors.SystemChromeBlackHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundChromeBlackMedium", (Color)resourceDictionary["MahApps.Colors.SystemChromeBlackMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundChromeBlackMediumLow", (Color)resourceDictionary["MahApps.Colors.SystemChromeBlackMediumLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundChromeDisabledLow", (Color)resourceDictionary["MahApps.Colors.SystemChromeDisabledLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundChromeGray", (Color)resourceDictionary["MahApps.Colors.SystemChromeGray"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundChromeHigh", (Color)resourceDictionary["MahApps.Colors.SystemChromeHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundChromeMedium", (Color)resourceDictionary["MahApps.Colors.SystemChromeMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundChromeWhite", (Color)resourceDictionary["MahApps.Colors.SystemChromeWhite"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundListLow", (Color)resourceDictionary["MahApps.Colors.SystemListLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundListMedium", (Color)resourceDictionary["MahApps.Colors.SystemListMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlForegroundTransparent", Colors.Transparent);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightAccent", (Color)resourceDictionary["MahApps.Colors.SystemAccent"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightAltAccent", (Color)resourceDictionary["MahApps.Colors.SystemAccent"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightAltAltHigh", (Color)resourceDictionary["MahApps.Colors.SystemAltHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightAltAltMediumHigh", (Color)resourceDictionary["MahApps.Colors.SystemAltMediumHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightAltBaseHigh", (Color)resourceDictionary["MahApps.Colors.SystemBaseHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightAltBaseLow", (Color)resourceDictionary["MahApps.Colors.SystemBaseLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightAltBaseMedium", (Color)resourceDictionary["MahApps.Colors.SystemBaseMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightAltBaseMediumHigh", (Color)resourceDictionary["MahApps.Colors.SystemBaseMediumHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightAltBaseMediumLow", (Color)resourceDictionary["MahApps.Colors.SystemBaseMediumLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightAltChromeWhite", (Color)resourceDictionary["MahApps.Colors.SystemChromeWhite"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightAltListAccentHigh", (Color)resourceDictionary["MahApps.Colors.SystemAccent"], baseTheme == BaseTheme.Light ? 0.7 : 0.9);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightAltListAccentLow", (Color)resourceDictionary["MahApps.Colors.SystemAccent"], baseTheme == BaseTheme.Light ? 0.4 : 0.6);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightAltListAccentMedium", (Color)resourceDictionary["MahApps.Colors.SystemAccent"], baseTheme == BaseTheme.Light ? 0.6 : 0.8);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightAltTransparent", Colors.Transparent);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightBaseHigh", (Color)resourceDictionary["MahApps.Colors.SystemBaseHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightBaseLow", (Color)resourceDictionary["MahApps.Colors.SystemBaseLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightBaseMedium", (Color)resourceDictionary["MahApps.Colors.SystemBaseMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightBaseMediumHigh", (Color)resourceDictionary["MahApps.Colors.SystemBaseMediumHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightBaseMediumLow", (Color)resourceDictionary["MahApps.Colors.SystemBaseMediumLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightChromeAltLow", (Color)resourceDictionary["MahApps.Colors.SystemChromeAltLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightChromeHigh", (Color)resourceDictionary["MahApps.Colors.SystemChromeHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightChromeWhite", (Color)resourceDictionary["MahApps.Colors.SystemChromeWhite"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightListAccentHigh", (Color)resourceDictionary["MahApps.Colors.SystemAccent"], baseTheme == BaseTheme.Light ? 0.7 : 0.9);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightListAccentLow", (Color)resourceDictionary["MahApps.Colors.SystemAccent"], baseTheme == BaseTheme.Light ? 0.4 : 0.6);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightListAccentMedium", (Color)resourceDictionary["MahApps.Colors.SystemAccent"], baseTheme == BaseTheme.Light ? 0.6 : 0.8);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightListLow", (Color)resourceDictionary["MahApps.Colors.SystemListLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightListMedium", (Color)resourceDictionary["MahApps.Colors.SystemListMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHighlightTransparent", Colors.Transparent);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHyperlinkBaseHigh", (Color)resourceDictionary["MahApps.Colors.SystemBaseHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHyperlinkBaseMedium", (Color)resourceDictionary["MahApps.Colors.SystemBaseMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHyperlinkBaseMediumHigh", (Color)resourceDictionary["MahApps.Colors.SystemBaseMediumHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlHyperlinkText", (Color)resourceDictionary["MahApps.Colors.SystemAccent"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlPageBackgroundAltHigh", (Color)resourceDictionary["MahApps.Colors.SystemAltHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlPageBackgroundAltMedium", (Color)resourceDictionary["MahApps.Colors.SystemAltMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlPageBackgroundBaseLow", (Color)resourceDictionary["MahApps.Colors.SystemBaseLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlPageBackgroundBaseMedium", (Color)resourceDictionary["MahApps.Colors.SystemBaseMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlPageBackgroundChromeLow", (Color)resourceDictionary["MahApps.Colors.SystemChromeLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlPageBackgroundChromeMediumLow", (Color)resourceDictionary["MahApps.Colors.SystemChromeMediumLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlPageBackgroundListLow", (Color)resourceDictionary["MahApps.Colors.SystemListLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlPageBackgroundMediumAltMedium", (Color)resourceDictionary["MahApps.Colors.SystemAltMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlPageBackgroundTransparent", Colors.Transparent);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlPageTextBaseHigh", (Color)resourceDictionary["MahApps.Colors.SystemBaseHigh"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlPageTextBaseMedium", (Color)resourceDictionary["MahApps.Colors.SystemBaseMedium"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlPageTextChromeBlackMediumLow", (Color)resourceDictionary["MahApps.Colors.SystemChromeBlackMediumLow"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlRevealFocusVisual", (Color)resourceDictionary["MahApps.Colors.SystemAccent"]);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlTransientBorder", Color.FromRgb(0x00, 0x00, 0x00), baseTheme == BaseTheme.Light ? 0.14 : 0.36);
            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlTransparent", Colors.Transparent);

            resourceDictionary.SetBrush("MahApps.Brushes.SystemControlDescriptionTextForeground", "MahApps.Brushes.SystemControlPageTextBaseMedium");

            //  Resources for MahApps.Styles.CheckBox.Win10
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.ForegroundUnchecked", "MahApps.Brushes.SystemControlForegroundBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.ForegroundUncheckedMouseOver", "MahApps.Brushes.SystemControlForegroundBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.ForegroundUncheckedPressed", "MahApps.Brushes.SystemControlForegroundBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.ForegroundUncheckedDisabled", "MahApps.Brushes.SystemControlDisabledBaseMediumLow");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.ForegroundChecked", "MahApps.Brushes.SystemControlForegroundBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.ForegroundCheckedMouseOver", "MahApps.Brushes.SystemControlForegroundBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.ForegroundCheckedPressed", "MahApps.Brushes.SystemControlForegroundBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.ForegroundCheckedDisabled", "MahApps.Brushes.SystemControlDisabledBaseMediumLow");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.ForegroundIndeterminate", "MahApps.Brushes.SystemControlForegroundBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.ForegroundIndeterminateMouseOver", "MahApps.Brushes.SystemControlForegroundBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.ForegroundIndeterminatePressed", "MahApps.Brushes.SystemControlForegroundBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.ForegroundIndeterminateDisabled", "MahApps.Brushes.SystemControlDisabledBaseMediumLow");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BackgroundUnchecked", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BackgroundUncheckedMouseOver", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BackgroundUncheckedPressed", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BackgroundUncheckedDisabled", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BackgroundChecked", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BackgroundCheckedMouseOver", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BackgroundCheckedPressed", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BackgroundCheckedDisabled", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BackgroundIndeterminate", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BackgroundIndeterminateMouseOver", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BackgroundIndeterminatePressed", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BackgroundIndeterminateDisabled", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BorderBrushUnchecked", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BorderBrushUncheckedMouseOver", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BorderBrushUncheckedPressed", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BorderBrushUncheckedDisabled", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BorderBrushChecked", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BorderBrushCheckedMouseOver", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BorderBrushCheckedPressed", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BorderBrushCheckedDisabled", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BorderBrushIndeterminate", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BorderBrushIndeterminateMouseOver", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BorderBrushIndeterminatePressed", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.BorderBrushIndeterminateDisabled", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundStrokeUnchecked", "MahApps.Brushes.SystemControlForegroundBaseMediumHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundStrokeUncheckedMouseOver", "MahApps.Brushes.SystemControlHighlightBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundStrokeUncheckedPressed", "MahApps.Brushes.SystemControlHighlightTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundStrokeUncheckedDisabled", "MahApps.Brushes.SystemControlDisabledBaseMediumLow");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundStrokeChecked", "MahApps.Brushes.SystemControlHighlightTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundStrokeCheckedMouseOver", "MahApps.Brushes.SystemControlHighlightBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundStrokeCheckedPressed", "MahApps.Brushes.SystemControlHighlightTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundStrokeCheckedDisabled", "MahApps.Brushes.SystemControlDisabledBaseMediumLow");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundStrokeIndeterminate", "MahApps.Brushes.SystemControlForegroundAccent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundStrokeIndeterminateMouseOver", "MahApps.Brushes.SystemControlHighlightAccent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundStrokeIndeterminatePressed", "MahApps.Brushes.SystemControlHighlightBaseMedium");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundStrokeIndeterminateDisabled", "MahApps.Brushes.SystemControlDisabledBaseMediumLow");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundFillUnchecked", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundFillUncheckedMouseOver", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundFillUncheckedPressed", "MahApps.Brushes.SystemControlBackgroundBaseMedium");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundFillUncheckedDisabled", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundFillChecked", "MahApps.Brushes.SystemControlHighlightAccent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundFillCheckedMouseOver", "MahApps.Brushes.SystemControlBackgroundAccent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundFillCheckedPressed", "MahApps.Brushes.SystemControlHighlightBaseMedium");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundFillCheckedDisabled", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundFillIndeterminate", "MahApps.Brushes.SystemControlHighlightTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundFillIndeterminateMouseOver", "MahApps.Brushes.SystemControlHighlightTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundFillIndeterminatePressed", "MahApps.Brushes.SystemControlHighlightTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckBackgroundFillIndeterminateDisabled", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckGlyphForegroundUnchecked", "MahApps.Brushes.SystemControlHighlightAltChromeWhite");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckGlyphForegroundUncheckedMouseOver", "MahApps.Brushes.SystemControlHighlightAltChromeWhite");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckGlyphForegroundUncheckedPressed", "MahApps.Brushes.SystemControlHighlightAltChromeWhite");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckGlyphForegroundUncheckedDisabled", "MahApps.Brushes.SystemControlHighlightAltChromeWhite");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckGlyphForegroundChecked", "MahApps.Brushes.SystemControlHighlightAltChromeWhite");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckGlyphForegroundCheckedMouseOver", "MahApps.Brushes.SystemControlForegroundChromeWhite");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckGlyphForegroundCheckedPressed", "MahApps.Brushes.SystemControlForegroundChromeWhite");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckGlyphForegroundCheckedDisabled", "MahApps.Brushes.SystemControlDisabledBaseMediumLow");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckGlyphForegroundIndeterminate", "MahApps.Brushes.SystemControlForegroundBaseMediumHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckGlyphForegroundIndeterminateMouseOver", "MahApps.Brushes.SystemControlForegroundBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckGlyphForegroundIndeterminatePressed", "MahApps.Brushes.SystemControlForegroundBaseMedium");
            resourceDictionary.SetBrush("MahApps.Brushes.CheckBox.CheckGlyphForegroundIndeterminateDisabled", "MahApps.Brushes.SystemControlDisabledBaseMediumLow");

            //  Resources for MahApps.Styles.RadioButton.Win10
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.Foreground", "MahApps.Brushes.SystemControlForegroundBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.ForegroundPointerOver", "MahApps.Brushes.SystemControlForegroundBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.ForegroundPressed", "MahApps.Brushes.SystemControlForegroundBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.ForegroundDisabled", "MahApps.Brushes.SystemControlDisabledBaseMediumLow");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.Background", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.BackgroundPointerOver", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.BackgroundPressed", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.BackgroundDisabled", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.BorderBrush", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.BorderBrushPointerOver", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.BorderBrushPressed", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.BorderBrushDisabled", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.OuterEllipseStroke", "MahApps.Brushes.SystemControlForegroundBaseMediumHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.OuterEllipseStrokePointerOver", "MahApps.Brushes.SystemControlHighlightBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.OuterEllipseStrokePressed", "MahApps.Brushes.SystemControlHighlightBaseMedium");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.OuterEllipseStrokeDisabled", "MahApps.Brushes.SystemControlDisabledBaseMediumLow");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.OuterEllipseFill", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.OuterEllipseFillPointerOver", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.OuterEllipseFillPressed", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.OuterEllipseFillDisabled", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.OuterEllipseCheckedStroke", "MahApps.Brushes.SystemControlHighlightAccent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.OuterEllipseCheckedStrokePointerOver", "MahApps.Brushes.SystemControlHighlightAccent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.OuterEllipseCheckedStrokePressed", "MahApps.Brushes.SystemControlHighlightBaseMedium");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.OuterEllipseCheckedStrokeDisabled", "MahApps.Brushes.SystemControlDisabledBaseMediumLow");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.OuterEllipseCheckedFill", "MahApps.Brushes.SystemControlHighlightAltTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.OuterEllipseCheckedFillPointerOver", "MahApps.Brushes.SystemControlHighlightTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.OuterEllipseCheckedFillPressed", "MahApps.Brushes.SystemControlHighlightTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.OuterEllipseCheckedFillDisabled", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.CheckGlyphFill", "MahApps.Brushes.SystemControlHighlightBaseMediumHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.CheckGlyphFillPointerOver", "MahApps.Brushes.SystemControlHighlightAltBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.CheckGlyphFillPressed", "MahApps.Brushes.SystemControlHighlightAltBaseMedium");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.CheckGlyphFillDisabled", "MahApps.Brushes.SystemControlDisabledBaseMediumLow");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.CheckGlyphStroke", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.CheckGlyphStrokePointerOver", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.CheckGlyphStrokePressed", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.RadioButton.CheckGlyphStrokeDisabled", "MahApps.Brushes.SystemControlTransparent");

            //  Resources for MahApps.Styles.ToggleSwitch
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.ContentForeground", "MahApps.Brushes.SystemControlForegroundBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.ContentForegroundDisabled", "MahApps.Brushes.SystemControlDisabledBaseMediumLow");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.HeaderForeground", "MahApps.Brushes.SystemControlForegroundBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.HeaderForegroundDisabled", "MahApps.Brushes.SystemControlDisabledBaseMediumLow");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.ContainerBackground", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.ContainerBackgroundPointerOver", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.ContainerBackgroundPressed", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.ContainerBackgroundDisabled", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.FillOff", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.FillOffPointerOver", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.FillOffPressed", "MahApps.Brushes.SystemControlHighlightBaseMedium");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.FillOffDisabled", "MahApps.Brushes.SystemControlTransparent");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.StrokeOff", "MahApps.Brushes.SystemControlForegroundBaseMediumHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.StrokeOffPointerOver", "MahApps.Brushes.SystemControlHighlightBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.StrokeOffPressed", "MahApps.Brushes.SystemControlForegroundBaseMediumHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.StrokeOffDisabled", "MahApps.Brushes.SystemControlDisabledBaseMediumLow");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.FillOn", "MahApps.Brushes.SystemControlHighlightAccent");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.FillOnPointerOver", "MahApps.Brushes.SystemControlHighlightAltListAccentHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.FillOnPressed", "MahApps.Brushes.SystemControlHighlightBaseMedium");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.FillOnDisabled", "MahApps.Brushes.SystemControlDisabledBaseLow");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.StrokeOn", "MahApps.Brushes.SystemControlHighlightBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.StrokeOnPointerOver", "MahApps.Brushes.SystemControlHighlightListAccentHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.StrokeOnPressed", "MahApps.Brushes.SystemControlHighlightBaseMedium");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.StrokeOnDisabled", "MahApps.Brushes.SystemControlDisabledBaseMediumLow");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.KnobFillOff", "MahApps.Brushes.SystemControlForegroundBaseMediumHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.KnobFillOffPointerOver", "MahApps.Brushes.SystemControlHighlightBaseHigh");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.KnobFillOffPressed", "MahApps.Brushes.SystemControlHighlightAltChromeWhite");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.KnobFillOffDisabled", "MahApps.Brushes.SystemControlDisabledBaseMediumLow");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.KnobFillOn", "MahApps.Brushes.SystemControlHighlightAltChromeWhite");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.KnobFillOnPointerOver", "MahApps.Brushes.SystemControlHighlightChromeWhite");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.KnobFillOnPressed", "MahApps.Brushes.SystemControlHighlightAltChromeWhite");
            resourceDictionary.SetBrush("MahApps.Brushes.ToggleSwitch.KnobFillOnDisabled", "MahApps.Brushes.SystemControlPageBackgroundBaseLow");
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

        private static void SetBrush(this ResourceDictionary resourceDictionary, string name, string source)
        {
            if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (source == null) throw new ArgumentNullException(nameof(source));

            var sourceBrush = resourceDictionary[source] as SolidColorBrush;
            if (sourceBrush is null) return;

            SetBrush(resourceDictionary, name, sourceBrush.Color, sourceBrush.Opacity);
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
