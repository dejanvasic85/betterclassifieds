$dotNetVersion = "4.0"
$regKey = "HKLM:\software\Microsoft\MSBuild\ToolsVersions\$dotNetVersion"
$regProperty = "MSBuildToolsPath"
$msbuild = Join-Path -Path (Get-ItemProperty $regKey).$regProperty -ChildPath "msbuild.exe"
$scriptPath = ( Split-Path $MyInvocation.MyCommand.Path ) 

# Compile source code
& $msbuild "Betterclassifieds.sln" "/p:Configuration=Debug" "/t:Clean,Build"
# Create \ Upgrade databases
@("MembershipDatabase", "ClassifiedsDatabase", "DocumentDatabase", "BroadcastDatabase", "LogDatabase") | 
	foreach { 
		$pathToDeploy = "Database\$_\bin\Debug\PostDeploy.ps1" 
		Write-Host "Upgrading $_"
		Invoke-Expression $pathToDeploy
		Set-Location $scriptPath
	}

Write-Host "Done..."