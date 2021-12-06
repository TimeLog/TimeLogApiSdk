##########################################################################
# PowerShell sample for extracting data from the TimeLog Reporting API
#
# Fill in the credential and url variables ($siteCode, $apiId, $apiPassword, $serviceUrl)
# Adjust the SDK path if necessary ($sdkPath)
#
# Tested in PowerShell 7.0.3
##########################################################################

Clear-Host

$rootFolder = (Get-ChildItem -Path ".")[0].Directory.Parent.FullName;
# Run the Powershell in its own container to avoid the Add-Type to lock the file
$job = Start-Job -ArgumentList $rootFolder -ScriptBlock {

    param($sdkRoot)

    $siteCode = ""
    $apiId = "sox"
    $apiPassword = "sox"
    $serviceUrl = "https://app5.timelog.com/demo"

    Add-Type -AssemblyName System.Configuration
    $sdkPath = "$sdkRoot\Deploy\TimeLog.ReportingApi.SDK.dll"
    Write-Host "#########################################"
    Write-Host SDK Path: $sdkPath
    Write-Host "#########################################"

    Add-Type -Path $sdkPath

    # Optional parameters are for "MaxReceivedMessageSize" and "Timeout" (both for huge XML fetches)
    $instance = New-Object -TypeName TimeLog.ReportingApi.SDK.ServiceHandler($siteCode, $apiId, $apiPassword, $serviceUrl)

    Write-Host("ServiceUrl: " + $instance.ServiceUrl)
    Write-Host("SiteCode: " + $instance.SiteCode)
    Write-Host("ApiId: " + $instance.ApiId)
    Write-Host("ApiPassword: " + $instance.ApiPassword)
    Write-Host("MaxReceivedMessageSize: " + $instance.MaxReceivedMessageSize)
    Write-Host("Timeout: " + $instance.Timeout)

    Write-Host "Trying to authenticate with reporting API..."
    if ($instance.TryAuthenticate() -eq $true) {
        Write-Host("Successfully authenticated on reporting API!")

        # Recommended usage
        # In order to replicate TimeLog data to your datawarehouse or similar we recommend the following procedure to ensure consistent data without heavy API usage:
        # - Every quarter fetch and sync the full data set
        # - Every month fetch and sync the last 3 months of data
        # - Every week fetch and sync the last 4 weeks of data
        # - Every day fetch and sync the last 7 days of data
        # Note that you might need to adjust the intervals and periods to fit the data diciplin in your TimeLog instance.        

        $startDate = [DateTime]::Parse('2000-08-01 00:00:00')
        $endDate = [DateTime]::Parse('2020-08-01 23:59:59')

        Write-Host StartDate: $startDate
        Write-Host EndDate: $endDate

        $workUnits = $instance.Client.GetWorkUnitsRaw(
                    $instance.SiteCode, 
                    $instance.ApiId, 
                    $instance.ApiPassword,
                    [TimeLog.ReportingApi.SDK.WorkUnit]::All,
                    [TimeLog.ReportingApi.SDK.Employee]::All,
                    [TimeLog.ReportingApi.SDK.Allocation]::All,
                    [TimeLog.ReportingApi.SDK.Task]::All,
                    [TimeLog.ReportingApi.SDK.Project]::All,
                    [TimeLog.ReportingApi.SDK.Department]::All,
                    $startDate,
                    $endDate)

        $workUnits.OuterXml | Out-File -Path "./workunits.xml"
        Write-Host("Successfully saved result!")
    }
    else {
        Write-Host "Failed to authenticate!"
    }
}
Wait-Job $job
Receive-Job $job