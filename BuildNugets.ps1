$version = "1.2.3"
$copywrite = "Copyright 2019 James Willock/Mulholland Software Ltd"
$configuration = "Release"

$iconUrl = "http://materialdesigninxaml.net/images/MD4XAML32.png"
$nugetIconFile = [System.IO.Path]::GetFullPath("NugetIcon.png")


function Update-Icon {
  param (
    [string]$Path
  )
  
  [xml] $xml = Get-Content $Path
  [string] $iconUrl = $xml.package.metadata.iconUrl;
  if (![string]::IsNullOrWhiteSpace($iconUrl) -and [string]::IsNullOrWhiteSpace($xml.package.metadata.icon)) {
    $nugetIconFile = "$($xml.package.metadata.id).Icon.png";
    Invoke-WebRequest $iconUrl -OutFile "$nugetIconFile"
    [xml] $files = $xml.package.files;
    $iconFile = $files.CreateElement("file");
    $iconFile.target = "images\";
    $iconFile.src = "$nugetIconFile"
    $xml.package.metadata.icon = "images\icon.png"
  }
  $xml.Save($Path)
}


#Push-Location ".."

Update-Icon .\MaterialDesignColors.nuspec 

#nuget pack .\MaterialDesignColors.nuspec -version "$version" -Properties "Configuration=$configuration;Copywrite=$copywrite"



#Pop-Location
