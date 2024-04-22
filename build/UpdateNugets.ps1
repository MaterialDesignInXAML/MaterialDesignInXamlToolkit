param(
  [string]$Path,
  [string]$MDIXVersion = "1.0.0",
  [string]$MDIXColorsVersion = "1.0.0",
  [string]$MDIXMahAppsVersion = "1.0.0"
)

function Get-VersionString {
  param (
    [string]$Version
  )

  $incrementCallback = {
    [int]$args[0].Groups[1].Value + 1
  }
  $releaseVersionCallback = {
      "$($args[0].Groups[1].Value).$($args[0].Groups[2].Value)"
  }

  $re = [regex]"^(\d+)\.(\d+\.\d+).*"
  $releaseVersion = $re.Replace($Version, $releaseVersionCallback)
  $nextVersion = $re.Replace($Version, $incrementCallback)
  return "[$releaseVersion,$nextVersion)"
}

Push-Location "$(Join-Path $PSScriptRoot "..")"

$Path = Resolve-Path $Path

nupkgwrench nuspec dependencies modify "$Path" --dependency-id "MaterialDesignThemes" --dependency-version "$(Get-VersionString $MDIXVersion)"
nupkgwrench nuspec dependencies modify "$Path" --dependency-id "MaterialDesignColors" --dependency-version "$(Get-VersionString $MDIXColorsVersion)"
nupkgwrench nuspec dependencies modify "$Path" --dependency-id "MaterialDesignThemes.MahApps" --dependency-version "$(Get-VersionString $MDIXMahAppsVersion)"

Pop-Location