param(
    [System.IO.DirectoryInfo]$RootDirectory
)

#NB: This script requires PowerShell 7.1 or later

$files = Get-ChildItem -Recurse -Path $RootDirectory -File -Filter "*.xaml"
$resourceTypes = ("StaticResource", "DynamicResource")

foreach ($file in $files) {
    $fileContents = Get-Content $file -Encoding utf8BOM -Raw
    $fileLength = $fileContents.Length
    
    foreach($resourceType in $resourceTypes) {
        $fileContents = $fileContents -replace "\{$resourceType\ PrimaryHueLightBrush}", "{$resourceType MaterialDesign.Brush.Primary.Light}"
        $fileContents = $fileContents -replace "\{$resourceType\ PrimaryHueLightForegroundBrush}", "{$resourceType MaterialDesign.Brush.Primary.Light.Foreground}"
        $fileContents = $fileContents -replace "\{$resourceType\ PrimaryHueMidBrush}", "{$resourceType MaterialDesign.Brush.Primary}"
        $fileContents = $fileContents -replace "\{$resourceType\ PrimaryHueMidForegroundBrush}", "{$resourceType MaterialDesign.Brush.Primary.Foreground}"
        $fileContents = $fileContents -replace "\{$resourceType\ PrimaryHueDarkBrush}", "{$resourceType MaterialDesign.Brush.Primary.Dark}"
        $fileContents = $fileContents -replace "\{$resourceType\ PrimaryHueDarkForegroundBrush}", "{$resourceType MaterialDesign.Brush.Primary.Dark.Foreground}"
        $fileContents = $fileContents -replace "\{$resourceType\ SecondaryHueLightBrush}", "{$resourceType MaterialDesign.Brush.Secondary.Light}"
        $fileContents = $fileContents -replace "\{$resourceType\ SecondaryHueLightForegroundBrush}", "{$resourceType MaterialDesign.Brush.Secondary.Light.Foreground}"
        $fileContents = $fileContents -replace "\{$resourceType\ SecondaryHueMidBrush}", "{$resourceType MaterialDesign.Brush.Secondary}"
        $fileContents = $fileContents -replace "\{$resourceType\ SecondaryHueMidForegroundBrush}", "{$resourceType MaterialDesign.Brush.Secondary.Foreground}"
        $fileContents = $fileContents -replace "\{$resourceType\ SecondaryHueDarkBrush}", "{$resourceType MaterialDesign.Brush.Secondary.Dark}"
        $fileContents = $fileContents -replace "\{$resourceType\ SecondaryHueDarkForegroundBrush}", "{$resourceType MaterialDesign.Brush.Secondary.Dark.Foreground}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignPaper}", "{$resourceType MaterialDesign.Brush.Background}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignFlatButtonClick}", "{$resourceType MaterialDesign.Brush.Button.FlatClick}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignFlatButtonRipple}", "{$resourceType MaterialDesign.Brush.Button.Ripple}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignSnackbarRipple}", "{$resourceType MaterialDesign.Brush.Button.Ripple}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignBackground}", "{$resourceType MaterialDesign.Brush.Card.Background}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignCardBackground}", "{$resourceType MaterialDesign.Brush.Card.Background}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignCheckBoxDisabled}", "{$resourceType MaterialDesign.Brush.CheckBox.Disabled}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignChipBackground}", "{$resourceType MaterialDesign.Brush.Chip.Background}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignDataGridRowHoverBackground}", "{$resourceType MaterialDesign.Brush.DataGrid.RowHoverBackground}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignBody}", "{$resourceType MaterialDesign.Brush.Foreground}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignBodyLight}", "{$resourceType MaterialDesign.Brush.ForegroundLight}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignCheckBoxOff}", "{$resourceType MaterialDesign.Brush.ForegroundLight}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignTextBoxBorder}", "{$resourceType MaterialDesign.Brush.ForegroundLight}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignColumnHeader}", "{$resourceType MaterialDesign.Brush.Header.Foreground}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignTextAreaBorder}", "{$resourceType MaterialDesign.Brush.Header.Foreground}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignSnackbarBackground}", "{$resourceType MaterialDesign.Brush.SnackBar.Background}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignSnackbarMouseOver}", "{$resourceType MaterialDesign.Brush.SnackBar.MouseOver}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignTextFieldBoxDisabledBackground}", "{$resourceType MaterialDesign.Brush.TextBox.DisabledBackground}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignTextFieldBoxBackground}", "{$resourceType MaterialDesign.Brush.TextBox.FilledBackground}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignTextFieldBoxHoverBackground}", "{$resourceType MaterialDesign.Brush.TextBox.HoverBackground}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignDivider}", "{$resourceType MaterialDesign.Brush.TextBox.HoverBackground}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignTextAreaInactiveBorder}", "{$resourceType MaterialDesign.Brush.TextBox.OutlineInactiveBorder}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignToolBarBackground}", "{$resourceType MaterialDesign.Brush.ToolBar.Background}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignToolBackground}", "{$resourceType MaterialDesign.Brush.ToolBar.Item.Background}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignToolForeground}", "{$resourceType MaterialDesign.Brush.ToolBar.Item.Foreground}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignToolTipBackground}", "{$resourceType MaterialDesign.Brush.ToolTip.Background}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignValidationErrorBrush}", "{$resourceType MaterialDesign.Brush.ValidationError}"
    }

    if ($fileContents.Length -ne $fileLength) {
        Set-Content -Path $file -Value $fileContents -Encoding utf8BOM -NoNewline
    }
}

