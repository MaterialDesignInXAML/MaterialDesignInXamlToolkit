param(
    [System.IO.DirectoryInfo]$RootDirectory
)

#NB: This script requires PowerShell 7.1 or later

$files = Get-ChildItem -Recurse -Path $RootDirectory -Include "*.xaml"
foreach ($file in $files) {
    $fileContents = Get-Content $file -Encoding utf8BOM -Raw
    $fileLength = $fileContents.Length
    $fileContents = $fileContents -replace "\{StaticResource\ PrimaryHueLightBrush}", "{StaticResource MaterialDesign.Brush.Primary.Light}"
    $fileContents = $fileContents -replace "\{StaticResource\ PrimaryHueLightForegroundBrush}", "{StaticResource MaterialDesign.Brush.Primary.Light.Foreground}"
    $fileContents = $fileContents -replace "\{StaticResource\ PrimaryHueMidBrush}", "{StaticResource MaterialDesign.Brush.Primary}"
    $fileContents = $fileContents -replace "\{StaticResource\ PrimaryHueMidForegroundBrush}", "{StaticResource MaterialDesign.Brush.Primary.Foreground}"
    $fileContents = $fileContents -replace "\{StaticResource\ PrimaryHueDarkBrush}", "{StaticResource MaterialDesign.Brush.Primary.Dark}"
    $fileContents = $fileContents -replace "\{StaticResource\ PrimaryHueDarkForegroundBrush}", "{StaticResource MaterialDesign.Brush.Primary.Dark.Foreground}"
    $fileContents = $fileContents -replace "\{StaticResource\ SecondaryHueLightBrush}", "{StaticResource MaterialDesign.Brush.Secondary.Light}"
    $fileContents = $fileContents -replace "\{StaticResource\ SecondaryHueLightForegroundBrush}", "{StaticResource MaterialDesign.Brush.Secondary.Light.Foreground}"
    $fileContents = $fileContents -replace "\{StaticResource\ SecondaryHueMidBrush}", "{StaticResource MaterialDesign.Brush.Secondary}"
    $fileContents = $fileContents -replace "\{StaticResource\ SecondaryHueMidForegroundBrush}", "{StaticResource MaterialDesign.Brush.Secondary.Foreground}"
    $fileContents = $fileContents -replace "\{StaticResource\ SecondaryHueDarkBrush}", "{StaticResource MaterialDesign.Brush.Secondary.Dark}"
    $fileContents = $fileContents -replace "\{StaticResource\ SecondaryHueDarkForegroundBrush}", "{StaticResource MaterialDesign.Brush.Secondary.Dark.Foreground}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignPaper}", "{StaticResource MaterialDesign.Brush.Background}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignFlatButtonClick}", "{StaticResource MaterialDesign.Brush.Button.FlatClick}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignFlatButtonRipple}", "{StaticResource MaterialDesign.Brush.Button.Ripple}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignSnackbarRipple}", "{StaticResource MaterialDesign.Brush.Button.Ripple}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignBackground}", "{StaticResource MaterialDesign.Brush.Card.Background}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignCardBackground}", "{StaticResource MaterialDesign.Brush.Card.Background}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignCheckBoxDisabled}", "{StaticResource MaterialDesign.Brush.CheckBox.Disabled}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignChipBackground}", "{StaticResource MaterialDesign.Brush.Chip.Background}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignDataGridRowHoverBackground}", "{StaticResource MaterialDesign.Brush.DataGrid.RowHoverBackground}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignBody}", "{StaticResource MaterialDesign.Brush.Foreground}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignBodyLight}", "{StaticResource MaterialDesign.Brush.ForegroundLight}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignCheckBoxOff}", "{StaticResource MaterialDesign.Brush.ForegroundLight}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignTextBoxBorder}", "{StaticResource MaterialDesign.Brush.ForegroundLight}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignColumnHeader}", "{StaticResource MaterialDesign.Brush.Header.Foreground}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignTextAreaBorder}", "{StaticResource MaterialDesign.Brush.Header.Foreground}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignSnackbarBackground}", "{StaticResource MaterialDesign.Brush.SnackBar.Background}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignSnackbarMouseOver}", "{StaticResource MaterialDesign.Brush.SnackBar.MouseOver}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignTextFieldBoxDisabledBackground}", "{StaticResource MaterialDesign.Brush.TextBox.DisabledBackground}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignTextFieldBoxBackground}", "{StaticResource MaterialDesign.Brush.TextBox.FilledBackground}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignTextFieldBoxHoverBackground}", "{StaticResource MaterialDesign.Brush.TextBox.HoverBackground}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignDivider}", "{StaticResource MaterialDesign.Brush.TextBox.HoverBackground}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignTextAreaInactiveBorder}", "{StaticResource MaterialDesign.Brush.TextBox.OutlineInactiveBorder}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignToolBarBackground}", "{StaticResource MaterialDesign.Brush.ToolBar.Background}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignToolBackground}", "{StaticResource MaterialDesign.Brush.ToolBar.Item.Background}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignToolForeground}", "{StaticResource MaterialDesign.Brush.ToolBar.Item.Foreground}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignToolTipBackground}", "{StaticResource MaterialDesign.Brush.ToolTip.Background}"
    $fileContents = $fileContents -replace "\{StaticResource\ MaterialDesignValidationErrorBrush}", "{StaticResource MaterialDesign.Brush.ValidationError}"
    if ($fileContents.Length -ne $fileLength) {
        Set-Content -Path $file -Value $fileContents -Encoding utf8BOM -NoNewline
    }
}

