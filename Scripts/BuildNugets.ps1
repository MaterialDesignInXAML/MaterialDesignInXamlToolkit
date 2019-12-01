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
    Invoke-WebRequest $iconUrl -OutFile "$nugetIconFile"
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

New-Nuget .\MaterialDesignColors.nuspec $MDIXColorsVersion
New-Nuget .\MaterialDesignThemes.nuspec $MDIXVersion
New-Nuget .\MaterialDesignThemes.MahApps.nuspec $MDIXMahAppsVersion

Pop-Location