$dotNetVersion = "4.0"
$regKey = "HKLM:\software\Microsoft\MSBuild\ToolsVersions\$dotNetVersion"
$regProperty = "MSBuildToolsPath"
$msbuild = Join-Path -Path (Get-ItemProperty $regKey).$regProperty -ChildPath "msbuild.exe"

& $msbuild "Betterclassifieds.sln" "/p:Configuration=Debug" "/t:Clean,Build"

& "Database\iFlogAppUserDatabase\bin\Debug\iFlogAppUserDatabase.exe"

& "Database\iFlogCoreDatabase\bin\Debug\iFlogCoreDatabase.exe"

& "Database\iFlogDatabase\bin\Debug\iFlogDatabase.exe"

& "Database\iFlogLogDatabase\bin\Debug\iFlogLogDatabase.exe"