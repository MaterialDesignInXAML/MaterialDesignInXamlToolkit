# Reset to repo root
Push-Location $(Join-Path $PSScriptRoot "..")

# Restore local tool
dotnet tool restore

#Run XAML Styler
xstyler --directory . --config .\Settings.XamlStyler --recursive

# Reset location
Pop-Location