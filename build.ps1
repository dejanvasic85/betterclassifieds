$dotNetVersion = "4.0"
$regKey = "HKLM:\software\Microsoft\MSBuild\ToolsVersions\$dotNetVersion"
$regProperty = "MSBuildToolsPath"
$msbuild = Join-Path -Path (Get-ItemProperty $regKey).$regProperty -ChildPath "msbuild.exe"

& $msbuild "Betterclassifieds.sln" "/p:Configuration=Debug" "/t:Clean,Build"

& "Database\MembershipDatabase\bin\Debug\MembershipDatabase.exe"

& "Database\CoreDatabase\bin\Debug\CoreDatabase.exe"

& "Database\ClassifiedsDatabase\bin\Debug\ClassifiedsDatabase.exe"

& "Database\LogDatabase\bin\Debug\LogDatabase.exe"

& "Database\DocumentDatabase\bin\Debug\DocumentDatabase.exe"

& "Database\BroadcastDatabase\bin\Debug\BroadcastDatabase.exe"