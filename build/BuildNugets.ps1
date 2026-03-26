param(
  [string]$MDIXVersion = "1.0.0",
  [string]$MDIXMahAppsVersion = "1.0.0"
)

$ErrorActionPreference = 'Stop'

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
  if ($LASTEXITCODE -ne 0) {
    throw "dotnet pack failed for $ProjectPath with exit code $LASTEXITCODE"
  }
}

Push-Location "$(Join-Path $PSScriptRoot "..")"

# Pack the projects
# MaterialDesignThemes includes both Themes and Colors DLLs (Colors ref uses PrivateAssets=all)
New-DotNetPackage .\src\MaterialDesignThemes.Wpf\MaterialDesignThemes.Wpf.csproj $MDIXVersion
New-DotNetPackage .\src\MaterialDesignThemes.MahApps\MaterialDesignThemes.MahApps.csproj $MDIXMahAppsVersion

# Verify all expected packages were created
$themesPackage = ".\MaterialDesignThemes.$MDIXVersion.nupkg"
$mahAppsPackage = ".\MaterialDesignThemes.MahApps.$MDIXMahAppsVersion.nupkg"

foreach ($pkg in @($themesPackage, $mahAppsPackage)) {
  if (-not (Test-Path $pkg)) {
    throw "Expected package not found: $pkg. Ensure the projects are built before running this script (dotnet pack uses --no-build)."
  }
}

Pop-Location
