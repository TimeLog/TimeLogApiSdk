Clear-Host

$rootFolder = (Get-ChildItem -Path ".")[0].Directory.Parent.FullName;
$job = Start-Job -ArgumentList $rootFolder -ScriptBlock {

    param($sdkRoot)

    Add-Type -AssemblyName System.Configuration
    $sdkPath = "$sdkRoot\Deploy\TimeLog.TransactionalApi.SDK.dll"
    $configPath = "$sdkRoot\TimeLog.PowerShell\App.config"
    Write-Host "#########################################"
    Write-Host SDK Path: $sdkPath
    Write-Host Config Path: $configPath
    Write-Host "#########################################"

    [System.AppDomain]::CurrentDomain.SetData("APP_CONFIG_FILE", $configPath)

    #[Reflection.Assembly]::LoadFrom("..\Deploy\TimeLog.TransactionalApi.SDK.dll")
    Add-Type -Path $sdkPath

    $messages = $null

    Write-Host "Trying to authenticate with transactional API..."
    if ([TimeLog.TransactionalApi.SDK.SecurityHandler]::Instance.TryAuthenticate([ref]$messages) -eq $true) {
        Write-Host("Successfully authenticated on transactional API!")

        $startDate = [DateTime]::Parse('2018-05-10 00:00:00')
        $endDate = [DateTime]::Parse('2018-05-15 23:59:59')

        Write-Host StartDate: $startDate
        Write-Host EndDate: $endDate

        $page = 1
        $fetchMorePages = $true

        while ($fetchMorePages) {

            $getTimeRegistrationsModifiedInPeriodPagedRaw = [TimeLog.TransactionalApi.SDK.TimeTrackingHandler]::Instance.TimeTrackingClient.GetTimeRegistrationsModifiedInPeriodPaged(
                $startDate, 
                $endDate, 
                $page,
                3,
                [TimeLog.TransactionalApi.SDK.TimeTrackingHandler]::Instance.Token,
                $true,
                '')

            if ($getTimeRegistrationsModifiedInPeriodPagedRaw.ResponseState -eq "Success") {
                Write-Host "Successfully fetched the list for page $page!"

                Write-Host Results: $getTimeRegistrationsModifiedInPeriodPagedRaw.Return.Length
                Write-Host Total pages: $getTimeRegistrationsModifiedInPeriodPagedRaw.TotalPageCount

                foreach ($obj in $getTimeRegistrationsModifiedInPeriodPagedRaw.Return) {
                    Write-Host ($obj.ID.ToString() + " (Registration date: " + $obj.StartAt + ", Created: " + $obj.DeltaCreatedAt + ") >> " + ($obj.Delta / 60) + " hours (Comment: " + $obj.TimeRegistrationDetails.Task.Name + ")")
                }

                Write-Host Messages:
                foreach ($message in $getTimeRegistrationsModifiedInPeriodPagedRaw.Messages) {
                    Write-Host (">> " + $message.Message)
                }                
            }
            else {
                Write-Host ResponseState: $getTimeRegistrationsModifiedInPeriodPagedRaw.ResponseState
                Write-Host Messages:
                foreach ($message in $getTimeRegistrationsModifiedInPeriodPagedRaw.Messages) {
                    Write-Host (">> " + $message.Message)
                }                
            }

            $page = $page + 1
            if ($getTimeRegistrationsModifiedInPeriodPagedRaw.Return.Length -gt 0) {
                $fetchMorePages = $true
            } else {
                $fetchMorePages = $false
            }
        }
    }
    else {
        Write-Host "Failed with the following messages: "
        foreach ($message in $messages) {
            Write-Host ">> $message"
        }
    }
}
Wait-Job $job
Receive-Job $job