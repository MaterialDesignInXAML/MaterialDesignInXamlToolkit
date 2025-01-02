$defaultStyleText = "(default style)"
$headerMarkdown = "##"
$listMarkdown = "-"
$themesDirectory = "..\src\MaterialDesignThemes.Wpf\Themes\"
$latestHash = git log -1 --pretty=format:"%H"
$baseURL = "https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/blob"
$filePathURL = "src/MaterialDesignThemes.Wpf/Themes"
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
            $xamlLines = Get-Content -Path $_.FullName
            $file = Select-ControlNameFromFile($_.Name)
            Read-XamlStyles -xamlLines $xamlLines -file $file
        }
    Set-Defaults
    Format-Output
}

Function Format-Output {
    Write-OutputFile "[//]: <> (AUTO GENERATED FILE; DO NOT EDIT)"
    foreach($style in $discoverdStyles | Sort-Object -Property File,@{Expression = {$_.IsDefault}; Ascending = $false}) {
        if ($previousFile -ne $style.File) {
            Write-OutputFile "`n$headerMarkdown $($style.File)"
        }
        $previousFile = $style.File;
        
        $styleLink = "$($baseURL)/$($latestHash)/$($filePathURL)/MaterialDesignTheme.$($style.File).xaml"
        if ($style.LineNumber) {
            $styleLink += "#L$($style.LineNumber)"
        }
        
        $linkAndStyleName = "[$($style.Style)]($styleLink)";
        if ($style.IsDefault) {
            Write-OutputFile ("$listMarkdown $($linkAndStyleName) $defaultStyleText" -replace '\s+', ' ')
        }
        else {
            Write-OutputFile "$listMarkdown $($linkAndStyleName)"
        }
    }
}

Function Write-OutputFile{
    Param ($output)
    Add-content $outputFullDir -value $output
    Write-Debug $output #debug
}

Function Set-Defaults{
    ForEach ($default in $defaults) {
        $style = $discoverdStyles.Where({$_.style -match $default.style -and $_.Control -match $default.Type})
        if ($null -ne $style[0]) {
            $style[0].IsDefault = $true
        }
        else {            
            $temp = Get-Style -targetType $default.Type -styleName $default.Style -fileName $default.Type
            $discoverdStyles.Add($temp) | Out-Null
        }
    }
    $discoverdStyles | Format-Table #debug
}

Function Select-ControlNameFromFile {
    Param ($fileName)
    return $fileName -replace ".xaml" -replace "MaterialDesignTheme."
}

Function Read-XamlStyles {
    Param ($xamlLines, $file)
    [xml]$xaml = $xamlLines
    $lineNum = 1
    $xaml.ResourceDictionary.Style | 
    Foreach-Object {
        Write-Output $_
        # Get line number by Key or TargetType
        $styleLineNumber = $null
        $searchKey = if ($_.Key) { $_.Key } else { $_.TargetType }
        
        for ($i = 0; $i -lt $xamlLines.Length; $i++) {
            if ($xamlLines[$i] -match [regex]::Escape($searchKey)) {
                $styleLineNumber = $i + 1
                break
            }
        }
        
        if ($file -eq "Defaults") {
            New-Default -style $_ -file $file -lineNumber $styleLineNumber
        }
        elseif ($file -eq "Generic") {
            New-GenericDefault -style $_ -file $file -lineNumber $styleLineNumber
        }
        else {
            New-Style -style $_ -file $file -lineNumber $styleLineNumber
        }
        $lineNum++
    }
}

Function New-GenericDefault {
    Param ($style, $file, $lineNumber)
    $targetType = Read-TargetType($style | Select-Object TargetType)
    $basedOn = Read-BasedOn($style | Select-Object BasedOn)
    $styleNameValue = ($style | Select-Object Key).Key
    $defaultStyleName = if ($null -eq $styleNameValue) { $basedOn } else { $styleNameValue }
    Write-Debug "[$file] [Type: $targetType] [StyleNameValue: $styleNameValue] [BasedOn: $basedOn] [DefaultStyleName: $defaultStyleName]"
    Add-DefaultStyle -file $file -targetType $targetType -styleName $defaultStyleName -lineNumber $lineNumber
}

Function New-Default {
    Param ($style, $file, $lineNumber)
    $targetType = Read-TargetType($style | Select-Object TargetType)
    $basedOn = Read-BasedOn($style | Select-Object BasedOn)
    $styleNameValue = ($style | Select-Object Key).Key
    $defaultStyleName = if ($null -eq $styleNameValue) { $basedOn } else { $styleNameValue }
    Write-Debug "[$file] [Type: $targetType] [StyleNameValue: $styleNameValue] [BasedOn: $basedOn] [DefaultStyleName: $defaultStyleName]"
    Add-DefaultStyle -file $file -targetType $targetType -styleName $defaultStyleName -lineNumber $lineNumber
}

Function New-Style {
    Param ($style, $file, $lineNumber)
    $targetType = Read-TargetType($style | Select-Object TargetType)
    $styleName = ($style | Select-Object Key).Key
    $splittedFile =  $file.split('.') # Support for "nested" file names like DataGrid.ComboBox

    if ($targetType -eq $splittedFile[-1]) {
        Write-Debug "[Match  ] [File: $file] [Type: $targetType] [Style: $styleName]"
        Add-Style -targetType $targetType -styleName $styleName -fileName $file -lineNumber $lineNumber
    }
    else {
        Write-Debug "[Skipped] [File: $file] [Type: $targetType] [Style: $styleName]"
    }
}

Function Add-Style {
    Param ($targetType, $styleName, $fileName, $lineNumber)
    $temp = Get-Style -targetType $targetType -styleName $styleName -fileName $file -lineNumber $lineNumber
    $discoverdStyles.Add($temp) | Out-Null
}

Function Get-Style {
    Param ($targetType, $styleName, $fileName, $lineNumber)
    $temp = "" | Select-Object "Control", "Style", "IsDefault", "File", "LineNumber"
    $temp.Control = $targetType
    $temp.Style = $styleName
    $temp.IsDefault = !$styleName
    $temp.File = $fileName
    $temp.LineNumber = $lineNumber
    return $temp
}

Function Add-DefaultStyle {
    Param ($file, $targetType, $styleName, $lineNumber)
    $temp = "" | Select-Object "File", "Type", "Style", "LineNumber"
    $temp.File = $file
    $temp.Type = $targetType
    $temp.Style = $styleName
    $temp.LineNumber = $lineNumber
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
