param(
  [string]$MDIXVersion = "1.0.0",
  [string]$MDIXColorsVersion = "1.0.0",
  [string]$MDIXMahAppsVersion = "1.0.0"
)

$year = [System.DateTime]::Now.ToString("yyyy")
$copyright = "Copyright $year James Willock/Mulholland Software Ltd"
$configuration = "Release"

function New-DotNetPackage {
  param (
    [string]$ProjectPath,
    [string]$Version
  )

  $ProjectPath = Resolve-Path $ProjectPath
  Write-Host "Packing $ProjectPath with version $Version"
  dotnet pack "$ProjectPath" -c $configuration -p:PackageVersion="$Version" -p:Copyright="$copyright" --no-build -o "."
}

Push-Location "$(Join-Path $PSScriptRoot "..")"

# Pack the projects
New-DotNetPackage .\src\MaterialDesignColors.Wpf\MaterialDesignColors.Wpf.csproj $MDIXColorsVersion
New-DotNetPackage .\src\MaterialDesignThemes.Wpf\MaterialDesignThemes.Wpf.csproj $MDIXVersion
New-DotNetPackage .\src\MaterialDesignThemes.MahApps\MaterialDesignThemes.MahApps.csproj $MDIXMahAppsVersion

Pop-Location
