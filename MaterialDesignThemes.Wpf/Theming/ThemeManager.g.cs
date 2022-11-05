namespace MaterialDesignThemes.Wpf.Theming;

partial class ThemeManager
{

    private static void SetThemeBrushes(ResourceDictionary resourceDictionary, Theme theme)
    {
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
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DataGrid.ColumnHeaderForeground", theme.DataGrid.ColumnHeaderForeground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.DataGrid.RowHoverBackground", theme.DataGrid.RowHoverBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.SnackBar.Background", theme.SnackBar.Background);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.SnackBar.MouseOver", theme.SnackBar.MouseOver);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.SnackBar.Ripple", theme.SnackBar.Ripple);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TextBox.Border", theme.TextBox.Border);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TextBox.DisabledBackground", theme.TextBox.DisabledBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TextBox.FilledBackground", theme.TextBox.FilledBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TextBox.HoverBackground", theme.TextBox.HoverBackground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TextBox.OutlineBorder", theme.TextBox.OutlineBorder);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.TextBox.OutlineInactiveBorder", theme.TextBox.OutlineInactiveBorder);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ToolBar.Background", theme.ToolBar.Background);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ToolBar.Item.Background", theme.ToolBar.Item.Background);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ToolBar.Item.Foreground", theme.ToolBar.Item.Foreground);
        SetSolidColorBrush(resourceDictionary, "MaterialDesign.Brush.ToolTip.Background", theme.ToolTip.Background);
    }

    private static Theme GetThemeBrushes(ResourceDictionary resourceDictionary)
    {
        return new Theme
        {
            Background = GetColor(resourceDictionary, "MaterialDesign.Brush.Background"),
            Foreground = GetColor(resourceDictionary, "MaterialDesign.Brush.Foreground"),
            LightForeground = GetColor(resourceDictionary, "MaterialDesign.Brush.LightForeground"),
            ValidationError = GetColor(resourceDictionary, "MaterialDesign.Brush.ValidationError"),
            Button =
            {
                FlatClick = GetColor(resourceDictionary, "MaterialDesign.Brush.Button.FlatClick"),
                FlatRipple = GetColor(resourceDictionary, "MaterialDesign.Brush.Button.FlatRipple"),
            },
            Card =
            {
                Background = GetColor(resourceDictionary, "MaterialDesign.Brush.Card.Background"),
            },
            CheckBox =
            {
                Disabled = GetColor(resourceDictionary, "MaterialDesign.Brush.CheckBox.Disabled"),
                Off = GetColor(resourceDictionary, "MaterialDesign.Brush.CheckBox.Off"),
            },
            Chip =
            {
                Background = GetColor(resourceDictionary, "MaterialDesign.Brush.Chip.Background"),
            },
            DataGrid =
            {
                ColumnHeaderForeground = GetColor(resourceDictionary, "MaterialDesign.Brush.DataGrid.ColumnHeaderForeground"),
                RowHoverBackground = GetColor(resourceDictionary, "MaterialDesign.Brush.DataGrid.RowHoverBackground"),
            },
            SnackBar =
            {
                Background = GetColor(resourceDictionary, "MaterialDesign.Brush.SnackBar.Background"),
                MouseOver = GetColor(resourceDictionary, "MaterialDesign.Brush.SnackBar.MouseOver"),
                Ripple = GetColor(resourceDictionary, "MaterialDesign.Brush.SnackBar.Ripple"),
            },
            TextBox =
            {
                Border = GetColor(resourceDictionary, "MaterialDesign.Brush.TextBox.Border"),
                DisabledBackground = GetColor(resourceDictionary, "MaterialDesign.Brush.TextBox.DisabledBackground"),
                FilledBackground = GetColor(resourceDictionary, "MaterialDesign.Brush.TextBox.FilledBackground"),
                HoverBackground = GetColor(resourceDictionary, "MaterialDesign.Brush.TextBox.HoverBackground"),
                OutlineBorder = GetColor(resourceDictionary, "MaterialDesign.Brush.TextBox.OutlineBorder"),
                OutlineInactiveBorder = GetColor(resourceDictionary, "MaterialDesign.Brush.TextBox.OutlineInactiveBorder"),
            },
            ToolBar =
            {
                Background = GetColor(resourceDictionary, "MaterialDesign.Brush.ToolBar.Background"),
                Item =
                {
                    Background = GetColor(resourceDictionary, "MaterialDesign.Brush.ToolBar.Item.Background"),
                    Foreground = GetColor(resourceDictionary, "MaterialDesign.Brush.ToolBar.Item.Foreground"),
                },
            },
            ToolTip =
            {
                Background = GetColor(resourceDictionary, "MaterialDesign.Brush.ToolTip.Background"),
            },
        };
    }

}
