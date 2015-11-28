$configFiles = Get-Childitem web.config
foreach ($file in $configFiles)
{ 
    $content = Get-Content $file.PSPath 
    $content = $content -replace "{version}","$ReleaseNumber"
    [IO.File]::WriteAllText($file.FullName, ($content -join "`r`n"))
}