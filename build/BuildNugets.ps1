param(
  [string]$MDIXVersion = "1.0.0",
  [string]$MDIXColorsVersion = "1.0.0",
  [string]$MDIXMahAppsVersion = "1.0.0"
)

$year = [System.DateTime]::Now.ToString("yyyy")
$copywrite = "Copyright $year James Willock/Mulholland Software Ltd"
$configuration = "Release"

function Update-Icon {
  param (
    [string]$Path
  )
  $Path = Resolve-Path $Path
  [xml] $xml = Get-Content $Path
  [string] $iconUrl = $xml.package.metadata.iconUrl;
  if (![string]::IsNullOrWhiteSpace($iconUrl) -and [string]::IsNullOrWhiteSpace($xml.package.metadata.icon)) {
    $nugetIconFile = "$($xml.package.metadata.id).Icon.png";
    $nugetIconPath = Join-Path (Split-Path $Path -Parent) $nugetIconFile
    Write-Host "Downloading icon from $iconUrl to $nugetIconPath"
    Invoke-WebRequest $iconUrl -OutFile "$nugetIconPath"
    $files = $xml.SelectSingleNode("/package/files")
    $iconFile = $xml.CreateElement("file")
    $iconFile.SetAttribute("src", "$nugetIconFile")
    $iconFile.SetAttribute("target", "images\")
    $files.AppendChild($iconFile) | Out-Null

    $iconElement = $xml.CreateElement("icon")
    $iconElement.InnerText = "images\$nugetIconFile"
    $xml.package.metadata.AppendChild($iconElement) | Out-Null
  }
  $xml.Save($Path)
}

function Update-Versions {
  param (
    [string]$Path
  )
  $Path = Resolve-Path $Path
  [xml] $xml = Get-Content $Path

  foreach ($dependency in $xml.package.metadata.dependencies.group.dependency) {
    if ($dependency.id -eq "MaterialDesignColors") {
      $dependency.version = $MDIXColorsVersion
    }
    elseif ($dependency.id -eq "MaterialDesignThemes") {
      $dependency.version = $MDIXVersion
    }
  }
  $xml.Save($Path)
}

function New-Nuget {
  param (
    [string]$NuSpecPath,
    [string]$Version
  )

  $NuSpecPath = Resolve-Path $NuSpecPath
  Update-Icon "$NuSpecPath" 
  nuget pack "$NuSpecPath" -version "$Version" -Properties "Configuration=$configuration;Copywrite=$copywrite"
}

Push-Location "$(Join-Path $PSScriptRoot "..")"

Update-Versions .\src\MaterialDesignColors.Wpf\MaterialDesignColors.nuspec
Update-Versions .\src\MaterialDesignThemes.Wpf\MaterialDesignThemes.nuspec
Update-Versions .\src\MaterialDesignThemes.MahApps\MaterialDesignThemes.MahApps.nuspec

New-Nuget .\src\MaterialDesignColors.Wpf\MaterialDesignColors.nuspec $MDIXColorsVersion
New-Nuget .\src\MaterialDesignThemes.Wpf\MaterialDesignThemes.nuspec $MDIXVersion
New-Nuget .\src\MaterialDesignThemes.MahApps\MaterialDesignThemes.MahApps.nuspec $MDIXMahAppsVersion

Pop-Location
