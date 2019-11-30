$version = "1.2.3"
$copywrite = "Copyright 2019 James Willock/Mulholland Software Ltd"
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


Update-Icon .\MaterialDesignColors.nuspec 

nuget pack .\MaterialDesignColors.nuspec -version "$version" -Properties "Configuration=$configuration;Copywrite=$copywrite"

