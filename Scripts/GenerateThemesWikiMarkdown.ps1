$defaultStyleText = "(default style)"
$headerMarkdown = "##"
$listMarkdown = "-"
$themesDirectory = "..\MaterialDesignThemes.Wpf\Themes\"
$outputFileName = "ControlStyleList.md"
$themesFullDir = Join-Path $PSScriptRoot $themesDirectory 
$outputFullDir = Join-Path $PSScriptRoot $outputFileName
$DebugPreference = 'Continue' # Log debug messages to terminal.

$discoverdStyles = New-Object System.Collections.ArrayList
$defaults = New-Object System.Collections.ArrayList

Function Main {
    # Get xaml files and loop through.
    Get-ChildItem $themesFullDir -Filter *.xaml | 
        Foreach-Object {
            $xamlString = Get-Content -Path $_.FullName
            $file = Select-ControlNameFromFile($_.Name)
            Read-XamlStyles -xamlString $xamlString -file $file 
        }
    Set-Defaults
    Format-Output
}

Function Format-Output {

    foreach($style in $discoverdStyles | Sort-Object -Property Control,@{Expression = {$_.IsDefault}; Ascending = $false}) {
        if ($previousFile -ne $style.File) {
            Write-OutputFile "`n$headerMarkdown $($style.File)"
        }
        $previousFile = $style.File;
        
        if ($style.IsDefault) {
            Write-OutputFile ("$listMarkdown $($style.Style) $defaultStyleText" -replace '\s+', ' ')
        }
        else {
            Write-OutputFile "$listMarkdown $($style.Style)"
        }
    }
}

Function Write-OutputFile{
    Param ($output)
    Add-content $outputFullDir -value $output
    Write-Debug $output #debug
}

Function Set-Defaults{
    $remainingDefaults = New-Object System.Collections.ArrayList
    ForEach ($default in $defaults) {
        $style = $discoverdStyles.Where({$_.style -match $default.style})
        if ($null -ne $style[0]) {
            $style[0].IsDefault = $true
        }
        else {
            $remainingDefaults.Add($default) 
        }
    }
    $discoverdStyles | Format-Table #debug
    $remainingDefaults | Format-Table #debug
}

Function Select-ControlNameFromFile {
    Param ($fileName)
    return $fileName -replace ".xaml" -replace "MaterialDesignTheme."
}

Function Read-XamlStyles {
    Param ($xamlString, $file)
    [xml]$xaml = $xamlString
    $xaml.selectNodes("//*") | Where-Object {$_.LocalName -eq "Style"} |
    Foreach-Object { 
        if ($file -eq "Defaults") {
            # Special handeling of Defaults
            New-Default -style $_ -file $file 
        }
        elseif ($file -eq "Generic") {
            # Special handeling of Generic
            New-GenericDefault -style $_ -file $file 
        }
        else{
            New-Style -style $_ -file $file
        }
    }
}

Function New-GenericDefault {
    Param ($style, $file)
    $targetType = Read-TargetType($style | Select-Object TargetType)
    $basedOn = Read-BasedOn($style | Select-Object BasedOn)
    $styleNameValue = ($style | Select-Object Key).Key
    $defaultStyleName = if ($null -eq $styleNameValue) { $basedOn } else { $styleNameValue }
    Write-Debug "[$file] [Type: $targetType] [StyleNameValue: $styleNameValue] [BasedOn: $basedOn] [DefaultStyleName: $defaultStyleName]"
    Add-DefaultStyle -file $file -targetType $targetType -styleName $defaultStyleName
}


Function New-Default {
    Param ($style, $file)
    $targetType = Read-TargetType($style | Select-Object TargetType)
    $basedOn = Read-BasedOn($style | Select-Object BasedOn)
    $styleNameValue = ($style | Select-Object Key).Key
    $defaultStyleName = if ($null -eq $styleNameValue) { $basedOn } else { $styleNameValue }
    Write-Debug "[$file] [Type: $targetType] [StyleNameValue: $styleNameValue] [BasedOn: $basedOn] [DefaultStyleName: $defaultStyleName]"
    Add-DefaultStyle -file $file -targetType $targetType -styleName $defaultStyleName
}

Function New-Style {
    Param ($style, $file)
    $targetType = Read-TargetType($style | Select-Object TargetType)
    $styleName = ($style | Select-Object Key).Key
    $splittedFile =  $file.split('.') # Suport for "nested" file names like DataGrid.ComboBox

    if ($targetType -eq $splittedFile[-1]) {
        Write-Debug "[Match  ] [File: $file] [Type: $targetType] [Style: $styleName]"
        Add-Style -targetType $targetType -styleName $styleName -fileName $file
    }
    else {
        Write-Debug "[Skipped] [File: $file] [Type: $targetType] [Style: $styleName] "
    }
}

Function Add-Style {
    Param ($targetType, $styleName, $fileName)
    $temp = "" | Select-Object "Control", "Style", "IsDefault", "File"
    $temp.Control = $targetType
    $temp.Style = $styleName
    $temp.IsDefault = !$styleName
    $temp.File = $fileName
    $discoverdStyles.Add($temp) | Out-Null
}

Function Add-DefaultStyle {
    Param ($file, $targetType, $styleName)
    $temp = "" | Select-Object "File", "Type", "Style"
    $temp.File = $file
    $temp.Type = $targetType
    $temp.Style = $styleName
    $defaults.Add($temp) | Out-Null
}

Function Read-TargetType {
    Param ($targetTypeText)
    return ($targetTypeText.TargetType -replace "{x:Type" -replace "{x:Type" -replace ".*:" -replace "}*" -replace "Base").Trim()
}

Function Read-BasedOn {
    Param ($targetTypeText)
    return ($targetTypeText.BasedOn -replace "{StaticResource" -replace ".*:" -replace "}*").Trim()
}

Main