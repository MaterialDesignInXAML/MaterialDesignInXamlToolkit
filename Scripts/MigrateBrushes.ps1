param(
    [System.IO.DirectoryInfo]$RootDirectory
)

$xamlFiles = Get-ChildItem -Recurse -Path $RootDirectory -Include "*.xaml"
foreach ($xamlFile in $xamlFiles) {
    $xaml = Get-Content $xamlFile -Encoding utf8BOM
    $xaml = $xaml -replace "{DynamicResource PrimaryHueLightBrush}", "{DynamicResource MaterialDesign.Brush.Primary.Light}"
    $xaml = $xaml -replace "{DynamicResource PrimaryHueLightForegroundBrush}", "{DynamicResource MaterialDesign.Brush.Primary.Light.Foreground}"
    $xaml = $xaml -replace "{DynamicResource PrimaryHueMidBrush}", "{DynamicResource MaterialDesign.Brush.Primary}"
    $xaml = $xaml -replace "{DynamicResource PrimaryHueMidForegroundBrush}", "{DynamicResource MaterialDesign.Brush.Primary.Foreground}"
    $xaml = $xaml -replace "{DynamicResource PrimaryHueDarkBrush}", "{DynamicResource MaterialDesign.Brush.Primary.Dark}"
    $xaml = $xaml -replace "{DynamicResource PrimaryHueDarkForegroundBrush}", "{DynamicResource MaterialDesign.Brush.Primary.Dark.Foreground}"

    $xaml = $xaml -replace "{DynamicResource SecondaryHueLightBrush}", "{DynamicResource MaterialDesign.Brush.Secondary.Light}"
    $xaml = $xaml -replace "{DynamicResource SecondaryHueLightForegroundBrush}", "{DynamicResource MaterialDesign.Brush.Secondary.Light.Foreground}"
    $xaml = $xaml -replace "{DynamicResource SecondaryHueMidBrush}", "{DynamicResource MaterialDesign.Brush.Secondary}"
    $xaml = $xaml -replace "{DynamicResource SecondaryHueMidForegroundBrush}", "{DynamicResource MaterialDesign.Brush.Secondary.Foreground}"
    $xaml = $xaml -replace "{DynamicResource SecondaryHueDarkBrush}", "{DynamicResource MaterialDesign.Brush.Secondary.Dark}"
    $xaml = $xaml -replace "{DynamicResource SecondaryHueDarkForegroundBrush}", "{DynamicResource MaterialDesign.Brush.Secondary.Dark.Foreground}"

    $xaml = $xaml -replace "{DynamicResource MaterialDesignPaper}", "{DynamicResource MaterialDesign.Brush.Background}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignFlatButtonClick}", "{DynamicResource MaterialDesign.Brush.Button.FlatClick}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignFlatButtonRipple}", "{DynamicResource MaterialDesign.Brush.Button.Ripple}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignSnackbarRipple}", "{DynamicResource MaterialDesign.Brush.Button.Ripple}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignBackground}", "{DynamicResource MaterialDesign.Brush.Card.Background}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignCardBackground}", "{DynamicResource MaterialDesign.Brush.Card.Background}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignCheckBoxDisabled}", "{DynamicResource MaterialDesign.Brush.CheckBox.Disabled}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignChipBackground}", "{DynamicResource MaterialDesign.Brush.Chip.Background}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignDataGridRowHoverBackground}", "{DynamicResource MaterialDesign.Brush.DataGrid.RowHoverBackground}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignBody}", "{DynamicResource MaterialDesign.Brush.Foreground}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignBodyLight}", "{DynamicResource MaterialDesign.Brush.ForegroundLight}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignCheckBoxOff}", "{DynamicResource MaterialDesign.Brush.ForegroundLight}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignTextBoxBorder}", "{DynamicResource MaterialDesign.Brush.ForegroundLight}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignColumnHeader}", "{DynamicResource MaterialDesign.Brush.Header.Foreground}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignTextAreaBorder}", "{DynamicResource MaterialDesign.Brush.Header.Foreground}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignSnackbarBackground}", "{DynamicResource MaterialDesign.Brush.SnackBar.Background}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignSnackbarMouseOver}", "{DynamicResource MaterialDesign.Brush.SnackBar.MouseOver}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignTextFieldBoxDisabledBackground}", "{DynamicResource MaterialDesign.Brush.TextBox.DisabledBackground}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignTextFieldBoxBackground}", "{DynamicResource MaterialDesign.Brush.TextBox.FilledBackground}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignTextFieldBoxHoverBackground}", "{DynamicResource MaterialDesign.Brush.TextBox.HoverBackground}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignDivider}", "{DynamicResource MaterialDesign.Brush.TextBox.HoverBackground}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignTextAreaInactiveBorder}", "{DynamicResource MaterialDesign.Brush.TextBox.OutlineInactiveBorder}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignToolBarBackground}", "{DynamicResource MaterialDesign.Brush.ToolBar.Background}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignToolBackground}", "{DynamicResource MaterialDesign.Brush.ToolBar.Item.Background}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignToolForeground}", "{DynamicResource MaterialDesign.Brush.ToolBar.Item.Foreground}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignToolTipBackground}", "{DynamicResource MaterialDesign.Brush.ToolTip.Background}"
    $xaml = $xaml -replace "{DynamicResource MaterialDesignValidationErrorBrush}", "{DynamicResource MaterialDesign.Brush.ValidationError}"
    Set-Content -Path $xamlFile -Value $xaml -Encoding utf8BOM
}

