param(
    [System.IO.DirectoryInfo]$RootDirectory
)

#NB: This script requires PowerShell 7.1 or later

$files = Get-ChildItem -Recurse -Path $RootDirectory -File -Filter "*.xaml"
$resourceTypes = ('StaticResource', 'DynamicResource')

foreach ($file in $files) {
    $fileContents = Get-Content $file -Encoding utf8BOM -Raw
    $fileLength = $fileContents.Length

    foreach($resourceType in $resourceTypes) {
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignActionAccentCheckBox}", "{$resourceType MaterialDesignActionSecondaryCheckBox}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignAccentCheckBox}", "{$resourceType MaterialDesignSecondaryCheckBox}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignAccentRadioButton}", "{$resourceType MaterialDesignSecondaryRadioButton}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignActionAccentToggleButton}", "{$resourceType MaterialDesignActionSecondaryToggleButton}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignChoiceChipAccentListBox}", "{$resourceType MaterialDesignChoiceChipSecondaryListBox}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignChoiceChipAccentListBoxItem}", "{$resourceType MaterialDesignChoiceChipSecondaryListBoxItem}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignChoiceChipAccentOutlineListBox}", "{$resourceType MaterialDesignChoiceChipSecondaryOutlineListBox}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignChoiceChipAccentOutlineListBoxItem}", "{$resourceType MaterialDesignChoiceChipSecondaryOutlineListBoxItem}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignChoiceChipAccentOutlineRadioButton}", "{$resourceType MaterialDesignChoiceChipSecondaryOutlineRadioButton}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignChoiceChipAccentRadioButton}", "{$resourceType MaterialDesignChoiceChipSecondaryRadioButton}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignFilterChipAccentCheckBox}", "{$resourceType MaterialDesignFilterChipSecondaryCheckBox}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignFilterChipAccentListBoxItem}", "{$resourceType MaterialDesignFilterChipSecondaryListBoxItem}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignFilterChipAccentListBox}", "{$resourceType MaterialDesignFilterChipSecondaryListBox}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignFilterChipAccentOutlineCheckBox}", "{$resourceType MaterialDesignFilterChipSecondaryOutlineCheckBox}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignFilterChipAccentOutlineListBox}", "{$resourceType MaterialDesignFilterChipSecondaryOutlineListBox}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignFilterChipAccentOutlineListBoxItem}", "{$resourceType MaterialDesignFilterChipSecondaryOutlineListBoxItem}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignFlatAccentButton}", "{$resourceType MaterialDesignFlatSecondaryMidBgButton}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignFlatAccentBgButton}", "{$resourceType MaterialDesignFlatSecondaryBgButton}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignFloatingActionMiniAccentButton}", "{$resourceType MaterialDesignFloatingActionMiniSecondaryButton}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignFloatingActionAccentButton}", "{$resourceType MaterialDesignFloatingActionSecondaryButton}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignMultiFloatingActionAccentPopupBox}", "{$resourceType MaterialDesignMultiFloatingActionSecondaryPopupBox}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignNavigationAccentListBoxItem}", "{$resourceType MaterialDesignNavigationSecondaryListBoxItem}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignNavigationAccentListBox}", "{$resourceType MaterialDesignNavigationSecondaryListBox}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignRaisedAccentButton}", "{$resourceType MaterialDesignRaisedSecondaryButton}"
        $fileContents = $fileContents -replace "\{$resourceType\ MaterialDesignSwitchAccentToggleButton}", "{$resourceType MaterialDesignSwitchSecondaryToggleButton}"
    }

    if ($fileContents.Length -ne $fileLength) {
        Set-Content -Path $file -Value $fileContents -Encoding utf8BOM -NoNewline
    }
}

