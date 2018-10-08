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

        $page = 1
        $fetchMorePages = $true

        while ($fetchMorePages) {

            $getEmployeesPagedRaw = [TimeLog.TransactionalApi.SDK.OrganisationHandler]::Instance.OrganisationClient.GetEmployeesPaged(
                $page,
                10,
                [TimeLog.TransactionalApi.SDK.OrganisationHandler]::Instance.Token)

            if ($getEmployeesPagedRaw.ResponseState -eq "Success") {
                Write-Host "Successfully fetched the list for page $page!"

                Write-Host Results: $getEmployeesPagedRaw.Return.Length
                Write-Host Total pages: $getEmployeesPagedRaw.TotalPageCount

                #foreach ($obj in $getWorkPagedWithExternalIdsRaw.Return) {
                    #Write-Host ($obj.ID.ToString())
                #}

                Write-Host Messages:
                foreach ($message in $getEmployeesPagedRaw.Messages) {
                    Write-Host (">> " + $message.Message)
                }                
            }
            else {
                Write-Host ResponseState: $getEmployeesPagedRaw.ResponseState
                Write-Host Messages:
                foreach ($message in $getEmployeesPagedRaw.Messages) {
                    Write-Host (">> " + $message.Message)
                }                
            }

            $page = $page + 1
            if ($getEmployeesPagedRaw.Return.Length -gt 0) {
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