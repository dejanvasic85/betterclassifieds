$dotNetVersion = "4.0"
$regKey = "HKLM:\software\Microsoft\MSBuild\ToolsVersions\$dotNetVersion"
$regProperty = "MSBuildToolsPath"
$msbuild = Join-Path -Path (Get-ItemProperty $regKey).$regProperty -ChildPath "msbuild.exe"
$scriptPath = ( Split-Path $MyInvocation.MyCommand.Path ) 

# Compile source code
& $msbuild "Betterclassifieds.sln" "/p:Configuration=Debug" "/t:Clean,Build"

# Set the variables for the database setup
Set-Variable -Name 'Brand' -Value 'TheMusic' -Scope Global
#Set-Variable -Name 'SqlFilesPath' -Value 'D:\Dejan\SQLData\MSSQL11.MSSQLSERVER\MSSQL\DATA\' -Scope Global
Set-Variable -Name 'SqlFilesPath' -Value 'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\' -Scope Global

# Run each!
@("MembershipDatabase", "ClassifiedsDatabase", "DocumentDatabase", "BroadcastDatabase", "LogDatabase") | 
	foreach { 
		$pathToDeploy = "Database\$_\bin\Debug\PostDeploy.ps1" 
		Write-Host "Upgrading $_"
		Invoke-Expression $pathToDeploy
		Set-Location $scriptPath
	}

Write-Host "Done..."