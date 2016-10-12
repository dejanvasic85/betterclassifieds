Import-Module WebAdministration

function Get-ScriptDirectory
{
    $Invocation = (Get-Variable MyInvocation -Scope 1).Value;
    if($Invocation.PSScriptRoot)
    {
        $Invocation.PSScriptRoot;
    }
    Elseif($Invocation.MyCommand.Path)
    {
        Split-Path $Invocation.MyCommand.Path
    }
    else
    {
        $Invocation.InvocationName.Substring(0,$Invocation.InvocationName.LastIndexOf("\"));
    }
}


$iisAppPoolName = "betterclassifieds"
$iisAppPoolDotNetVersion = "v4.0"
$iisAppName = "betterclassifieds"
$currentDirectory = Get-ScriptDirectory
$directoryPath = Join-Path -Path $currentDirectory -ChildPath "Paramount.Betterclassifieds.Presentation"
$localDomain = "betterclassifieds.local"

#navigate to the app pools root
cd IIS:\AppPools\

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
}

#Set-Location "IIS:\SslBindings"
#New-Item -Path "IIS:\SslBindings\*!443!betterclassifieds.local" -Thumbprint $mycert.Thumbprint -SslFlags 1
