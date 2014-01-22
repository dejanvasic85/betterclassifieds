# Map incoming variables from octopus
$deployPath = $OctopusPackageDirectoryPath

# Hard code some variables
$resultsFileName = "CI-Results.xml"
$reportFileName = "CI-Report.html"

# Concat tools executables
$nunit = ($deployPath + "\NUnit\nunit-console.exe")
$specflow = ($deployPath + "\Specflow\specflow.exe")

Set-Location $deployPath

if ( $RunIntegrationTests -eq $true ){
	
	Write-Host "Starting functional tests using selenium with nunit ..."
	& $nunit "Paramount.Betterclassifieds.Tests.Functional.dll" "/noshadow" "/framework:net-4.0" ("/xml:" + $resultsFileName) # "/include:ci"

	Write-Host "Generating report..."
	& $specflow "nunitexecutionreport" "Paramount.Betterclassifieds.Tests.Functional.csproj" ("/xmlTestResult:" + $resultsFileName) ("/out:" + $reportFileName)

	$xd = New-Object System.Xml.XmlDocument
    $xd.Load($deployPath + "\" + $resultsFileName)
   
    $node = $xd.selectSingleNode("test-results")
    [int]$errorCount = $node.getAttribute("errors")
	[int]$failureCount = $node.getAttribute("failures")

    if ( ($errorCount -gt 0) -or ($failureCount -gt 0) ) {
		Write-Host "Smoke test completed with at least 1 error or failure"
		Write-Host "Errored: $($errorCount)"
		Write-Host "Failed: $($failureCount)"

    	$smtpFrom = "smoketest@paramountit.com.au"
    	$smtpTo = "dejan.vasic@paramountit.com.au"
    	$messageSubject = "Smoke Test Failed"
    	
    	$message = New-Object System.Net.Mail.MailMessage $smtpfrom, $smtpto
    	$message.Subject = $messageSubject
    	$message.IsBodyHTML = $true
    	$message.Body = Get-Content ($deployPath + "\" + $reportFileName)

    	$smtp = New-Object Net.Mail.SmtpClient -ArgumentList ("smtpcorp.com", 2525)
        $smtp.Credentials = New-Object System.Net.NetworkCredential("support@paramountit.com.au", "rs-101");
    	$smtp.Send($message)
        
	}

	Write-Host "Functional Tests completed..."
}
else {

	Write-Host "Functional Tests have not been configured to run..."
}