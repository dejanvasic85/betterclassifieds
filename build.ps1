#Import-Module SQLPS -ErrorAction SilentlyContinue
Import-Module WebAdministration -ErrorAction Continue

function Setup-Upgrade-Databases(){
	# Run each!
	@("MembershipDatabase", "ClassifiedsDatabase", "DocumentDatabase", "LogDatabase") | 
		foreach { 
			$pathToDeploy = ".\Database\$_\bin\Debug\PostDeploy.ps1" 
			Write-Host "Upgrading database $_"
			Write-Host $pathToDeploy
			Invoke-Expression $pathToDeploy
			Set-Location $scriptPath
		}
}

function Build-Solution(){

	$nugetDirectory = "C:\Program Files (x86)\NuGet\NuGet.exe"
	& $nugetDirectory "restore" "Betterclassifieds.sln"

	$regKey = "HKLM:\software\Microsoft\MSBuild\ToolsVersions\14.0"
	$regProperty = "MSBuildToolsPath"
	$msbuild = Join-Path -Path (Get-ItemProperty $regKey).$regProperty -ChildPath "msbuild.exe"

	# Compile source code
	& $msbuild "Betterclassifieds.sln" "/p:Configuration=Debug" "/t:Clean,Build"
}

function Setup-Website(){
	$iisAppPoolName = "betterclassifieds"
	$iisAppPoolDotNetVersion = "v4.0"
	$iisAppName = "betterclassifieds"
	$directoryPath = Join-Path -Path $scriptPath -ChildPath "Paramount.Betterclassifieds.Presentation"
	$localDomain = "betterclassifieds.local"

	#navigate to the app pools root
	Set-Location IIS:\AppPools\

	#check if the app pool exists
	if (!(Test-Path $iisAppPoolName -pathType container))
	{
		#create the app pool
		$appPool = New-Item $iisAppPoolName
		$appPool | Set-ItemProperty -Name "managedRuntimeVersion" -Value $iisAppPoolDotNetVersion
	}

	#navigate to the sites root
	cd IIS:\Sites\

	#check if the site exists
	if ((Test-Path $iisAppName -pathType container) -eq $false)
	{
		Write-Host "setting up site"
		#create the site
		$iisApp = New-Item $iisAppName -bindings @{protocol="http";bindingInformation=":80:" + $localDomain} -physicalPath $directoryPath
		$iisApp | Set-ItemProperty -Name "applicationPool" -Value $iisAppPoolName
	}

	# if the certificate exists then we must have the binding setup already
	$cert = Get-ChildItem -Path "Cert:\LocalMachine\My" | Where { $_.Subject -eq "CN=$($localDomain)" }

	if ( $cert -eq $null ) {
		Write-Host "Creating certificate"
		$cert = New-SelfSignedCertificate -DnsName $localDomain -CertStoreLocation "Cert:\LocalMachine\My" -FriendlyName $localDomain

		Write-Host "Setting up SSL"
		Set-Location "IIS:\SslBindings"
		New-Item -Path "IIS:\SslBindings\*!443!betterclassifieds.local" -Thumbprint $cert.Thumbprint -SslFlags 1
		New-WebBinding -Port 443 -Protocol https -Name $iisAppName -HostHeader $localDomain -SslFlags 1
	}
	
	Write-Host "Site is ready..."
	Set-Location $scriptPath
}

function Setup-AppPool-DbUser(){
	Write-Host "Adding application pool user as sys admin"
	Invoke-Sqlcmd -Query "sp_addsrvrolemember 'IIS APPPOOL\betterclassifieds', 'sysadmin'" -ServerInstance "localhost" -Database "master"
}

function Setup-Hosts() {
	$domainName = "betterclassifieds.local"
	$hostsFileLocation = "C:\Windows\System32\drivers\etc\hosts"
	Get-Content $hostsFileLocation | Select-String $domainName

	$items = Get-Content $hostsFileLocation | Select-String $domainName

	Write-Host $items.Length

	if($items.Length -eq 0) {
		# Add entry
		Write-Host "Adding hosts entry"
        Add-Content $hostsFileLocation "`n127.0.0.1`t$domainName"
	}
	else{
		Write-Host "Hosts file added $domainName already"
	}
}

$scriptPath = ( Split-Path $MyInvocation.MyCommand.Path ) 

# Set the variables for the database setup
Set-Variable -Name 'Brand' -Value 'KandoBay' -Scope Global

if(Test-Path "C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\")
{
	Set-Variable -Name 'SqlFilesPath' -Value 'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\' -Scope Global

}else{
	Set-Variable -Name 'SqlFilesPath' -Value 'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\' -Scope Global
}



Build-Solution
Setup-Upgrade-Databases
Setup-Website
Setup-AppPool-DbUser
Setup-Hosts


Write-Host "Done..."

Set-Location $scriptPath