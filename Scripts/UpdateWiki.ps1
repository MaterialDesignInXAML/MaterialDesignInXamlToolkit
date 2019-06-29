git clone https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit.wiki.git Wiki

Push-Location .\Scripts
.\GenerateThemesWikiMarkdown.ps1
Pop-Location
Move-Item .\Scripts\ControlStyleList.md .\Wiki\ControlStyleList.md -Force

Push-Location Wiki
$status = git status
$has_changed = [string]$status -match "modified:\s*ControlStyleList.md"
if ($has_changed) {
  git add "ControlStyleList.md"
  git commit -m "Automatic update of ControlStyleList.md from Azure pipeline"
  #git push
  Write-Host "Wiki content updated"
} else {
  Write-Host "No updates"
}
Pop-Location

#Remove-Item Wiki -Recurse -Force