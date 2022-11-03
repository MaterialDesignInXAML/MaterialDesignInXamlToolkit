using System.Windows.Media;
using System.Windows.Media.Animation;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf.Theming;

public partial class ThemeManager
{
    private const string CurrentThemeKey = nameof(MaterialDesignThemes) + "." + nameof(CurrentThemeKey);
    private const string ThemeManagerKey = nameof(MaterialDesignThemes) + "." + nameof(ThemeManagerKey);

    public event EventHandler<ThemeChangedEventArgs>? ThemeChanged;

    private readonly ResourceDictionary _resourceDictionary;

    public ThemeManager(ResourceDictionary resourceDictionary)
        => _resourceDictionary = resourceDictionary ?? throw new ArgumentNullException(nameof(resourceDictionary));

    public void SetTheme(Theme theme, ColorAdjustment? colorAdjustment = null)
        => SetTheme(_resourceDictionary, theme, colorAdjustment);

    private void SetTheme(ResourceDictionary resourceDictionary, Theme theme, ColorAdjustment? colorAdjustment)
    {
        if (resourceDictionary is null) throw new ArgumentNullException(nameof(resourceDictionary));

        Theme oldTheme = GetTheme(resourceDictionary);

        if (theme is Theme internalTheme)
        {
            colorAdjustment ??= internalTheme.ColorAdjustment;
            internalTheme.ColorAdjustment = colorAdjustment;
        }

        ColorPair primaryLight = theme.PrimaryLight;
        ColorPair primary = theme.PrimaryMid;
        ColorPair primaryDark = theme.PrimaryDark;

        ColorPair secondaryLight = theme.SecondaryLight;
        ColorPair secondary = theme.SecondaryMid;
        ColorPair secondaryDark = theme.SecondaryDark;

        if (colorAdjustment is not null)
        {
            if (colorAdjustment.Colors.HasFlag(ColorSelection.Primary))
            {
                AdjustColorPairs(theme.Background, colorAdjustment,
                    ref primaryLight, ref primary, ref primaryDark);

            }
            if (colorAdjustment.Colors.HasFlag(ColorSelection.Secondary))
            {
                AdjustColorPairs(theme.Background, colorAdjustment,
                    ref secondaryLight, ref secondary, ref secondaryDark);
            }
        }

        SetThemeBrushes(resourceDictionary, theme);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Primary.Light", primaryLight);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Primary", primary);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Primary.Dark", primaryDark);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Secondary.Light", secondaryLight);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Secondary", secondary);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Secondary.Dark", secondaryDark);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Background", theme.Background);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Foreground", theme.Foreground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.LightForeground", theme.LightForeground);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ValidationError", theme.ValidationError);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Button.FlatClick", theme.Button.FlatClick);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Button.FlatRipple", theme.Button.FlatRipple);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Card.Background", theme.Card.Background);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.CheckBox.Disabled", theme.CheckBox.Disabled);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.CheckBox.Off", theme.CheckBox.Off);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Chip.Background", theme.Chip.Background);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Chip.OutlineCheckedBorder", theme.Chip.OutlineCheckedBorder);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ColorPicker.SliderThumbDisabled", theme.ColorPicker.SliderThumbDisabled);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ComboBox.Border", theme.ComboBox.Border);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ComboBox.HoverBackground", theme.ComboBox.HoverBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ComboBox.ForegroundDisabled", theme.ComboBox.ForegroundDisabled);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ComboBox.FilledBackground", theme.ComboBox.FilledBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ComboBox.OutlineInactiveBorder", theme.ComboBox.OutlineInactiveBorder);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DataGrid.Border", theme.DataGrid.Border);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DataGrid.CellFocusBorder", theme.DataGrid.CellFocusBorder);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DataGrid.CellSelectedBackground", theme.DataGrid.CellSelectedBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DataGrid.ColumnHeaderForeground", theme.DataGrid.ColumnHeaderForeground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DataGrid.PopupBorder", theme.DataGrid.PopupBorder);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DataGrid.RowHoverBackground", theme.DataGrid.RowHoverBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DataGrid.RowSelectedBackground", theme.DataGrid.RowSelectedBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DataGrid.SelectAllButtonPressed", theme.DataGrid.SelectAllButtonPressed);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DataGrid.ComboBox.Border", theme.DataGrid.ComboBox.Border);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DataGrid.ComboBox.ItemHoverBackground", theme.DataGrid.ComboBox.ItemHoverBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DataGrid.ComboBox.ItemSelectedBackground", theme.DataGrid.ComboBox.ItemSelectedBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DataGrid.ComboBox.ToggleDisabled", theme.DataGrid.ComboBox.ToggleDisabled);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DatePicker.Border", theme.DatePicker.Border);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DatePicker.HoverBackground", theme.DatePicker.HoverBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DatePicker.FilledBackground", theme.DatePicker.FilledBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DatePicker.OutlineBorder", theme.DatePicker.OutlineBorder);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DatePicker.OutlineInactiveBorder", theme.DatePicker.OutlineInactiveBorder);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DrawerHost.OverlayBackground", theme.DrawerHost.OverlayBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DrawerHost.LeftDrawerBackground", theme.DrawerHost.LeftDrawerBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DrawerHost.TopDrawerBackground", theme.DrawerHost.TopDrawerBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DrawerHost.RightDrawerBackground", theme.DrawerHost.RightDrawerBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DrawerHost.BottomDrawerBackground", theme.DrawerHost.BottomDrawerBackground);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.GridSplitter.Background", theme.GridSplitter.Background);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.GridSplitter.PreviewBackground", theme.GridSplitter.PreviewBackground);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ListBox.Border", theme.ListBox.Border);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ListBox.SelectedBackground", theme.ListBox.SelectedBackground);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ListView.Border", theme.ListView.Border);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ListView.ColumnHeaderForeground", theme.ListView.ColumnHeaderForeground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ListView.HoverBackground", theme.ListView.HoverBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ListView.FocusBorder", theme.ListView.FocusBorder);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ListView.SelectedBackground", theme.ListView.SelectedBackground);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.PasswordBox.Border", theme.PasswordBox.Border);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.PasswordBox.HoverBackground", theme.PasswordBox.HoverBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.PasswordBox.FilledBackground", theme.PasswordBox.FilledBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.PasswordBox.OutlineBorder", theme.PasswordBox.OutlineBorder);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.PasswordBox.OutlineInactiveBorder", theme.PasswordBox.OutlineInactiveBorder);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.RadioButton.Checked", theme.RadioButton.Checked);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.RadioButton.Disabled", theme.RadioButton.Disabled);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.RadioButton.Off", theme.RadioButton.Off);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.RadioButton.TabRipple", theme.RadioButton.TabRipple);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.RadioButton.TabProgressIndicatorForeground", theme.RadioButton.TabProgressIndicatorForeground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.RadioButton.ToolBorder", theme.RadioButton.ToolBorder);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.RatingBar.Ripple", theme.RatingBar.Ripple);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ScrollBar.Foreground", theme.ScrollBar.Foreground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ScrollBar.Border", theme.ScrollBar.Border);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ScrollBar.Pressed", theme.ScrollBar.Pressed);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Separator.Background", theme.Separator.Background);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Separator.Border", theme.Separator.Border);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Slider.LabelBackground", theme.Slider.LabelBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.Slider.Disabled", theme.Slider.Disabled);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.SnackBar.Background", theme.SnackBar.Background);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.SnackBar.MouseOver", theme.SnackBar.MouseOver);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.SnackBar.Ripple", theme.SnackBar.Ripple);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TabControl.Ripple", theme.TabControl.Ripple);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TabControl.TabDivider", theme.TabControl.TabDivider);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TextBox.Border", theme.TextBox.Border);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TextBox.DisabledBackground", theme.TextBox.DisabledBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TextBox.HoverBackground", theme.TextBox.HoverBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TextBox.FilledBackground", theme.TextBox.FilledBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TextBox.OutlineBorder", theme.TextBox.OutlineBorder);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TextBox.OutlineInactiveBorder", theme.TextBox.OutlineInactiveBorder);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TimePicker.Border", theme.TimePicker.Border);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TimePicker.HoverBackground", theme.TimePicker.HoverBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TimePicker.FilledBackground", theme.TimePicker.FilledBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TimePicker.OutlineBorder", theme.TimePicker.OutlineBorder);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TimePicker.OutlineInactiveBorder", theme.TimePicker.OutlineInactiveBorder);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ToolBar.Background", theme.ToolBar.Background);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ToolBar.HoverBackground", theme.ToolBar.HoverBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ToolBar.Item.Background", theme.ToolBar.Item.Background);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ToolBar.Item.Foreground", theme.ToolBar.Item.Foreground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ToolBar.OverflowBackground", theme.ToolBar.OverflowBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ToolBar.OverflowBorder", theme.ToolBar.OverflowBorder);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ToolBar.Separator", theme.ToolBar.Separator);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ToolBar.Ripple", theme.ToolBar.Ripple);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ToolBar.ThumbForeground", theme.ToolBar.ThumbForeground);

        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ToolTip.Background", theme.ToolTip.Background);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ToolTip.Foreground", theme.ToolTip.Foreground);
        //ITheme oldTheme = GetTheme(resourceDictionary);

        resourceDictionary[CurrentThemeKey] = theme;

        ThemeChanged?.Invoke(this, new ThemeChangedEventArgs(resourceDictionary, oldTheme, theme));

#pragma warning disable CS0618 // Type or member is obsolete
        ITheme legacyTheme = GetLegacyTheme(theme);
        resourceDictionary.SetTheme(legacyTheme, colorAdjustment);
#pragma warning restore CS0618 // Type or member is obsolete
    }

    [Obsolete("Will be removed in 5.0.0")]
    private static ITheme GetLegacyTheme(Theme theme)
    {
        return new Wpf.Theme()
        {
            Background = theme.Background,
            Body = theme.Foreground,
            BodyLight = theme.LightForeground,
            CardBackground = theme.Card.Background,
            CheckBoxDisabled = theme.CheckBox.Disabled,
            CheckBoxOff = theme.CheckBox.Off,
            ChipBackground = theme.Chip.Background,
            ColorAdjustment = theme.ColorAdjustment,
            ColumnHeader = theme.DataGrid.ColumnHeaderForeground,
            DataGridRowHoverBackground = theme.DataGrid.RowHoverBackground,
            Divider = theme.Divider,
            FlatButtonClick = theme.Button.FlatClick,
            FlatButtonRipple = theme.Button.FlatRipple,
            Paper = theme.Background,
            PrimaryDark = theme.PrimaryDark,
            PrimaryLight = theme.PrimaryLight,
            PrimaryMid = theme.PrimaryMid,
            SecondaryDark = theme.SecondaryDark,
            SecondaryLight = theme.SecondaryLight,
            SecondaryMid = theme.SecondaryMid,
            Selection = theme.Selection,
            SnackbarBackground = theme.SnackBar.Background,
            SnackbarMouseOver = theme.SnackBar.MouseOver,
            SnackbarRipple = theme.SnackBar.Ripple,
            TextAreaBorder = theme.TextBox.OutlineBorder,
            TextAreaInactiveBorder = theme.TextBox.OutlineInactiveBorder,
            TextBoxBorder = theme.TextBox.Border,
            TextFieldBoxBackground = theme.TextBox.FilledBackground,
            TextFieldBoxDisabledBackground = theme.TextBox.DisabledBackground,
            TextFieldBoxHoverBackground = theme.TextBox.HoverBackground,
            ToolBackground = theme.ToolBar.Item.Background,
            ToolBarBackground = theme.ToolBar.Background,
            ToolForeground = theme.ToolBar.Item.Foreground,
            ToolTipBackground = theme.ToolTip.Background,
            ValidationError = theme.ValidationError
        };
    }

    private static Theme GetTheme(ResourceDictionary resourceDictionary)
    {
        if (resourceDictionary is null) throw new ArgumentNullException(nameof(resourceDictionary));
        if (resourceDictionary[CurrentThemeKey] is Theme theme)
        {
            return theme;
        }

        return new Theme
        {
            PrimaryLight = GetColor_oldPair("MaterialDesign.Brush.Primary.Light"),
            PrimaryMid = GetColor_oldPair("MaterialDesign.Brush.Primary"),
            PrimaryDark = GetColor_oldPair("MaterialDesign.Brush.Primary.Dark"),

            SecondaryLight = GetColor_oldPair("MaterialDesign.Brush.Secondary.Light"),
            SecondaryMid = GetColor_oldPair("MaterialDesign.Brush.Secondary"),
            SecondaryDark = GetColor_oldPair("MaterialDesign.Brush.Secondary.Dark"),

            Background = GetColor_old("MaterialDesign.Brush.Background"),
            Foreground = GetColor_old("MaterialDesign.Brush.Foreground"),
            LightForeground = GetColor_old("MaterialDesign.Brush.LightForeground"),

            ValidationError = GetColor_old("MaterialDesign.Brush.ValidationError"),

            Button =
            {
                FlatClick = GetColor_old("MaterialDesign.Brush.Button.FlatClick"),
                FlatRipple = GetColor_old("MaterialDesign.Brush.Button.FlatRipple")
            },

            Card =
            {
                Background = GetColor_old("MaterialDesign.Brush.Card.Background"),
            },

            CheckBox =
            {
                Disabled = GetColor_old("MaterialDesign.Brush.CheckBox.Disabled"),
                Off = GetColor_old("MaterialDesign.Brush.CheckBox.Off"),
            },

            Chip =
            {
                Background = GetColor_old("MaterialDesign.Brush.Chip.Background"),
                OutlineCheckedBorder = GetColor_old("MaterialDesign.Brush.Chip.OutlineCheckedBorder")
            },

            ColorPicker =
            {
                SliderThumbDisabled = GetColor_old("MaterialDesign.Brush.ColorPicker.SliderThumbDisabled")
            },

            ComboBox =
            {
                Border = GetColor_old("MaterialDesign.Brush.ComboBox.Border"),
                HoverBackground = GetColor_old("MaterialDesign.Brush.ComboBox.HoverBackground"),
                ForegroundDisabled = GetColor_old("MaterialDesign.Brush.ComboBox.ForegroundDisabled"),
                FilledBackground = GetColor_old("MaterialDesign.Brush.ComboBox.FilledBackground"),
                OutlineInactiveBorder = GetColor_old("MaterialDesign.Brush.ComboBox.OutlineInactiveBorder"),
            },

            DataGrid =
            {
                Border = GetColor_old("MaterialDesign.Brush.DataGrid.Border"),
                CellFocusBorder = GetColor_old("MaterialDesign.Brush.DataGrid.CellFocusBorder"),
                CellSelectedBackground = GetColor_old("MaterialDesign.Brush.DataGrid.CellSelectedBackground"),
                ColumnHeaderForeground = GetColor_old("MaterialDesign.Brush.DataGrid.ColumnHeaderForeground"),
                PopupBorder = GetColor_old("MaterialDesign.Brush.DataGrid.PopupBorder"),
                RowHoverBackground = GetColor_old("MaterialDesign.Brush.DataGrid.RowHoverBackground"),
                RowSelectedBackground = GetColor_old("MaterialDesign.Brush.DataGrid.RowSelectedBackground"),
                SelectAllButtonPressed = GetColor_old("MaterialDesign.Brush.DataGrid.SelectAllButtonPressed"),

                ComboBox =
                {
                    Border = GetColor_old("MaterialDesign.Brush.DataGrid.ComboBox.Border"),
                    ItemHoverBackground = GetColor_old("MaterialDesign.Brush.DataGrid.ComboBox.ItemHoverBackground"),
                    ItemSelectedBackground = GetColor_old("MaterialDesign.Brush.DataGrid.ComboBox.ItemSelectedBackground"),
                    ToggleDisabled = GetColor_old("MaterialDesign.Brush.DataGrid.ComboBox.ToggleDisabled"),
                }
            },

            DatePicker =
            {
                Border = GetColor_old("MaterialDesign.Brush.DatePicker.Border"),
                HoverBackground = GetColor_old("MaterialDesign.Brush.DatePicker.HoverBackground"),
                FilledBackground = GetColor_old("MaterialDesign.Brush.DatePicker.FilledBackground"),
                OutlineBorder = GetColor_old("MaterialDesign.Brush.DatePicker.OutlineBorder"),
                OutlineInactiveBorder = GetColor_old("MaterialDesign.Brush.DatePicker.OutlineInactiveBorder"),
            },

            DrawerHost =
            {
                OverlayBackground = GetColor_old("MaterialDesign.Brush.DrawerHost.OverlayBackground"),
                LeftDrawerBackground = GetColor_old("MaterialDesign.Brush.DrawerHost.LeftDrawerBackground"),
                TopDrawerBackground = GetColor_old("MaterialDesign.Brush.DrawerHost.TopDrawerBackground"),
                RightDrawerBackground = GetColor_old("MaterialDesign.Brush.DrawerHost.RightDrawerBackground"),
                BottomDrawerBackground = GetColor_old("MaterialDesign.Brush.DrawerHost.BottomDrawerBackground"),
            },

            GridSplitter =
            {
                Background = GetColor_old("MaterialDesign.Brush.GridSplitter.Background"),
                PreviewBackground = GetColor_old("MaterialDesign.Brush.GridSplitter.PreviewBackground"),
            },

            ListBox =
            {
                Border = GetColor_old("MaterialDesign.Brush.ListBox.Border"),
                SelectedBackground = GetColor_old("MaterialDesign.Brush.ListBox.SelectedBackground"),
            },

            ListView =
            {
                Border = GetColor_old("MaterialDesign.Brush.ListView.Border"),
                ColumnHeaderForeground = GetColor_old("MaterialDesign.Brush.ListView.ColumnHeaderForeground"),
                HoverBackground = GetColor_old("MaterialDesign.Brush.ListView.HoverBackground"),
                FocusBorder = GetColor_old("MaterialDesign.Brush.ListView.FocusBorder"),
                SelectedBackground = GetColor_old("MaterialDesign.Brush.ListView.SelectedBackground"),
            },

            PasswordBox =
            {
                Border = GetColor_old("MaterialDesign.Brush.PasswordBox.Border"),
                HoverBackground = GetColor_old("MaterialDesign.Brush.PasswordBox.HoverBackground"),
                FilledBackground = GetColor_old("MaterialDesign.Brush.PasswordBox.FilledBackground"),
                OutlineBorder = GetColor_old("MaterialDesign.Brush.PasswordBox.OutlineBorder"),
                OutlineInactiveBorder = GetColor_old("MaterialDesign.Brush.PasswordBox.OutlineInactiveBorder"),
            },

            RadioButton =
            {
                Checked = GetColor_old("MaterialDesign.Brush.RadioButton.Checked"),
                Disabled = GetColor_old("MaterialDesign.Brush.RadioButton.Disabled"),
                Off = GetColor_old("MaterialDesign.Brush.RadioButton.Off"),
                TabRipple = GetColor_old("MaterialDesign.Brush.RadioButton.TabRipple"),
                TabProgressIndicatorForeground = GetColor_old("MaterialDesign.Brush.RadioButton.TabProgressIndicatorForeground"),
                ToolBorder = GetColor_old("MaterialDesign.Brush.RadioButton.ToolBorder"),
            },

            RatingBar =
            {
                Ripple = GetColor_old("MaterialDesign.Brush.RatingBar.Ripple"),
            },

            ScrollBar =
            {
                Foreground = GetColor_old("MaterialDesign.Brush.ScrollBar.Foreground"),
                Border = GetColor_old("MaterialDesign.Brush.ScrollBar.Border"),
                Pressed = GetColor_old("MaterialDesign.Brush.ScrollBar.Pressed"),
            },

            Separator =
            {
                Background = GetColor_old("MaterialDesign.Brush.Separator.Background"),
                Border = GetColor_old("MaterialDesign.Brush.Separator.Border"),
            },

            Slider =
            {
                Disabled = GetColor_old("MaterialDesign.Brush.Slider.Disabled"),
                LabelBackground = GetColor_old("MaterialDesign.Brush.Slider.LabelBackground")
            },

            SnackBar =
            {
                Background = GetColor_old("MaterialDesign.Brush.SnackBar.Background"),
                MouseOver = GetColor_old("MaterialDesign.Brush.SnackBar.MouseOver"),
                Ripple = GetColor_old("MaterialDesign.Brush.SnackBar.Ripple")
            },

            TabControl =
            {
                Ripple = GetColor_old("MaterialDesign.Brush.TabControl.Ripple"),
                TabDivider = GetColor_old("MaterialDesign.Brush.TabControl.TabDivider"),
            },

            TextBox =
            {
                Border = GetColor_old("MaterialDesign.Brush.TextBox.Border"),
                HoverBackground = GetColor_old("MaterialDesign.Brush.TextBox.HoverBackground"),
                FilledBackground = GetColor_old("MaterialDesign.Brush.TextBox.FilledBackground"),
                OutlineBorder = GetColor_old("MaterialDesign.Brush.TextBox.OutlineBorder"),
                OutlineInactiveBorder = GetColor_old("MaterialDesign.Brush.TextBox.OutlineInactiveBorder")
            },

            TimePicker =
            {
                Border = GetColor_old("MaterialDesign.Brush.TimePicker.Border"),
                HoverBackground = GetColor_old("MaterialDesign.Brush.TimePicker.HoverBackground"),
                FilledBackground = GetColor_old("MaterialDesign.Brush.TimePicker.FilledBackground"),
                OutlineBorder = GetColor_old("MaterialDesign.Brush.TimePicker.OutlineBorder"),
                OutlineInactiveBorder = GetColor_old("MaterialDesign.Brush.TimePicker.OutlineInactiveBorder"),
            },

            ToolBar =
            {
                Background = GetColor_old("MaterialDesign.Brush.ToolBar.Background"),
                HoverBackground = GetColor_old("MaterialDesign.Brush.ToolBar.HoverBackground"),

                Item =
                {
                    Background = GetColor_old("MaterialDesign.Brush.ToolBar.Item.Background"),
                    Foreground = GetColor_old("MaterialDesign.Brush.ToolBar.Item.Foreground")
                },

                OverflowBackground = GetColor_old("MaterialDesign.Brush.ToolBar.OverflowBackground"),
                OverflowBorder = GetColor_old("MaterialDesign.Brush.ToolBar.OverflowBorder"),
                Ripple = GetColor_old("MaterialDesign.Brush.ToolBar.Ripple"),
                Separator = GetColor_old("MaterialDesign.Brush.ToolBar.Separator"),
                ThumbForeground = GetColor_old("MaterialDesign.Brush.ToolBar.ThumbForeground"),
            },

            ToolTip =
            {
                Background = GetColor_old("MaterialDesign.Brush.ToolTip.Background"),
                Foreground = GetColor_old("MaterialDesign.Brush.ToolTip.Foreground")
            }
        };

        ColorPair GetColor_oldPair(string name)
            => new(GetColor_old(name), GetColor_old($"{name}.Foreground"));

        Color GetColor_old(string key) => GetColor(resourceDictionary, new[] { key });
    }

    private static Color GetColor(ResourceDictionary resourceDictionary, params string[] keys)
    {
        foreach (string key in keys)
        {
            if (TryGetColor(resourceDictionary, key, out Color color))
            {
                return color;
            }
        }
        throw new InvalidOperationException($"Could not locate required resource with key(s) '{string.Join(", ", keys)}'");
    }

    private static bool TryGetColor(ResourceDictionary resourceDictionary, string key, out Color color)
    {
        if (resourceDictionary[key] is SolidColorBrush brush)
        {
            color = brush.Color;
            return true;
        }
        color = default;
        return false;
    }

    public Theme GetTheme() => GetTheme(_resourceDictionary);

    public static ThemeManager GetThemeManager(ResourceDictionary resourceDictionary)
    {
        if (resourceDictionary is null) throw new ArgumentNullException(nameof(resourceDictionary));
        ThemeManager manager = resourceDictionary[ThemeManagerKey] as ThemeManager ?? new ThemeManager(resourceDictionary);
        resourceDictionary[ThemeManagerKey] = manager;
        return manager;
    }

    public static ThemeManager? GetApplicationThemeManager()
    {
        if (Application.Current is { } app)
        {
            var resourceDictionary = app.Resources.MergedDictionaries.FirstOrDefault(x => x is IMaterialDesignThemeDictionary) ?? app.Resources;
            return GetThemeManager(resourceDictionary);
        }
        return null;
    }

    public static Theme GetApplicationTheme()
    {
        if (GetApplicationThemeManager() is { } themeManager)
        {
            return themeManager.GetTheme();
        }
        throw new InvalidOperationException("No WPF application found. Consider calling ThemeManager.GetThemeManager(ResourceDictionary) instead.");
    }

    public static void SetApplicationTheme(Theme theme, ColorAdjustment? colorAdjustment = null)
    {
        if (GetApplicationThemeManager() is { } themeManager)
        {
            themeManager.SetTheme(theme, colorAdjustment);
            return;
        }
        throw new InvalidOperationException("No WPF application found. Consider calling ThemeManager.GetThemeManager(ResourceDictionary) instead.");
    }

    private static void SetSolidColorBrush(ResourceDictionary resourceDictionary, string name, ColorPair value)
    {
        SetSolidColorBrush(resourceDictionary, name, value.Color);
        SetSolidColorBrush(resourceDictionary, $"{name}.Foreground", value.ForegroundColor ?? value.GetForegroundColor());
    }

    private static void SetSolidColorBrush(ResourceDictionary sourceDictionary, string name, Color value)
    {
        if (sourceDictionary is null) throw new ArgumentNullException(nameof(sourceDictionary));
        if (name is null) throw new ArgumentNullException(nameof(name));

        sourceDictionary[name + ".Color"] = value;

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

    private static void AdjustColorPairs(Color background, ColorAdjustment colorAdjustment,
                ref ColorPair light, ref ColorPair mid, ref ColorPair dark)
    {
        Color lightColor = light.Color;
        Color midColor = mid.Color;
        Color darkColor = dark.Color;

        AdjustColors(background, colorAdjustment,
            ref lightColor, ref midColor, ref darkColor);

        light = new(lightColor, light.ForegroundColor);
        mid = new(midColor, mid.ForegroundColor);
        dark = new(darkColor, dark.ForegroundColor);
    }

    private static void AdjustColors(Color background, ColorAdjustment colorAdjustment,
        ref Color light, ref Color mid, ref Color dark)
    {
        double offset;
        switch (colorAdjustment.Contrast)
        {
            case Contrast.Low:
                if (background.IsLightColor())
                {
                    dark = dark.EnsureContrastRatio(background, colorAdjustment.DesiredContrastRatio, out offset);
                    if (Math.Abs(offset) > 0.0)
                    {
                        mid = mid.ShiftLightness(offset);
                        light = light.ShiftLightness(offset);
                    }
                }
                else
                {
                    light = light.EnsureContrastRatio(background, colorAdjustment.DesiredContrastRatio, out offset);
                    if (Math.Abs(offset) > 0.0)
                    {
                        mid = mid.ShiftLightness(offset);
                        dark = dark.ShiftLightness(offset);
                    }
                }
                break;
            case Contrast.Medium:
                mid = mid.EnsureContrastRatio(background, colorAdjustment.DesiredContrastRatio, out offset);
                if (Math.Abs(offset) > 0.0)
                {
                    dark = dark.ShiftLightness(offset);
                    light = light.ShiftLightness(offset);
                }
                break;
            case Contrast.High:
                if (background.IsLightColor())
                {
                    light = light.EnsureContrastRatio(background, colorAdjustment.DesiredContrastRatio, out offset);
                    if (Math.Abs(offset) > 0.0)
                    {
                        mid = mid.ShiftLightness(offset);
                        dark = dark.ShiftLightness(offset);
                    }
                }
                else
                {
                    dark = dark.EnsureContrastRatio(background, colorAdjustment.DesiredContrastRatio, out offset);
                    if (Math.Abs(offset) > 0.0)
                    {
                        light = light.ShiftLightness(offset);
                        mid = mid.ShiftLightness(offset);
                    }
                }
                break;
        }
    }
}
