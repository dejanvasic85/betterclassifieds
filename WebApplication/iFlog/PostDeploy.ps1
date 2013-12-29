$configFiles = Get-Childitem web.config
foreach ($file in $configFiles)
{ 
    $content = Get-Content $file.PSPath 
    $content = $content -replace "{version}","$Octopus.Release.Number"
    [IO.File]::WriteAllText($file.FullName, ($content -join "`r`n"))
}