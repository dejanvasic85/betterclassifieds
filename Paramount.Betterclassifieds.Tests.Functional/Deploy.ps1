# Check whether the integration tests should run ( specified in an octopus variable )
if ( $RunIntegrationTests -eq $false ) {
	Write-Host "Integration tests are not enabled... so getting outta here"
}

# Map incoming variables from octopus
$deployPath = $OctopusPackageDirectoryPath

# Hard code some variables
$resultsFileName = "CI-Results.xml"
$reportFileName = "CI-Report.html"

# Concat tools executables
$nunit = ($deployPath + "\NUnit\nunit-console.exe")
$specflow = ($deployPath + "\Specflow\specflow.exe")

Set-Location $deployPath

Write-Host "Starting functional tests using selenium with nunit ..."
& $nunit "Paramount.Betterclassifieds.Tests.Functional.dll" "/noshadow" "/framework:net-4.0" "/include:booking" ("/xml:" + $resultsFileName) # "/include:ci"

Write-Host "Generating report..."
& $specflow "nunitexecutionreport" "Paramount.Betterclassifieds.Tests.Functional.csproj" ("/xmlTestResult:" + $resultsFileName) ("/out:" + $reportFileName)

$xd = New-Object System.Xml.XmlDocument
$xd.Load($deployPath + "\" + $resultsFileName)
   
$node = $xd.selectSingleNode("test-results")
[int]$errorCount = $node.getAttribute("errors")
[int]$failureCount = $node.getAttribute("failures")

if ( ($errorCount -gt 0) -or ($failureCount -gt 0) ) {

	# Send an email with the results and fail the deployment step.
	Write-Host "Smoke test completed with at least 1 error or failure"
	Write-Host "Errored: $($errorCount)"
	Write-Host "Failed: $($failureCount)"

    $smtpFrom = "smoketest@paramountit.com.au"
    $messageSubject = "Smoke Test Failed"
    	
    $message = New-Object System.Net.Mail.MailMessage $smtpFrom, $TestResultsEmail
    $message.Subject = $messageSubject
    $message.IsBodyHTML = $true
    $message.Body = Get-Content ($deployPath + "\" + $reportFileName)

    $smtp = New-Object Net.Mail.SmtpClient -ArgumentList ("smtpcorp.com", 2525)
    $smtp.Credentials = New-Object System.Net.NetworkCredential("support@paramountit.com.au", "rs-101");
    $smtp.Send($message)
		
	if ( $FailDeployOnFailedTests -eq $true ){
		throw "At least 1 functional test has failed";
	}
}

Write-Host "Functional Tests completed..."
