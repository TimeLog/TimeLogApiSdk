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
        Write-Host("Sucessfully authenticated on transactional API!")

        $project = New-Object TimeLog.TransactionalApi.SDK.ProjectManagementService.Project
        $project.ID = New-Guid
        $project.AccountManagerID = 376
        $project.ProjectManagerID = 376
        $project.TypeID = 247
        $project.StartDate = '2018-08-01'
        $project.StageID = 2
        $project.Name = "Projekt PivotPoint"
        $project.CustomerID = 667
        $project.Action = [TimeLog.TransactionalApi.SDK.ProjectManagementService.DataAction]::Created
        $project.BudgetAmountExpenses = 0
        $project.BudgetAmountTravelExpenses = 0
        $project.BudgetWorkAmountFixedPriceProject = 0
        $project.BudgetWorkAmountFixedPriceTasks = 0
        $project.BudgetWorkAmountTimeAndMaterial = 0
        $project.BudgetWorkHoursFixedPriceProject = 0
        $project.BudgetWorkHoursFixedPriceTasks = 0
        $project.BudgetWorkHoursTimeAndMaterial = 0
        $project.CategoryID = 4
        $project.CurrencyID = 35
        $project.MainContractID = 1 # TimeMaterial = 1, FixedPrice = 2, TimeMaterialAccountEndBalancing = 3, TimeMaterialAccountPeriodicBalancing = 4, PrepaidServices = 5, RevenueReqPerTask = 6, ContinuousService = 7, ContinuousItemInvoicing = 8
        $project.DepartmentHandledByID = 0
        $project.DepartmentOrderedByID = 0
        $project.Description = ''
        $project.EndDate = '2018-09-01'
        $project.ExchangeRate = 1
        $project.No = "42"
        $project.LegalEntityID = 1
        $project.PriceGroupID = 1
        $project.PurchaseOrderNumber = "42"
        
        #$project.IsExternalKeysLoaded = $false
        #$externalKey = New-Object TimeLog.TransactionalApi.SDK.ProjectManagementService.ExternalSystemContext 
        #$externalKey.ExternalID = "EXT1234"
        #$externalKey.SystemName = "Jira"

        $project.ExternalKeys = @($externalKey)

        Write-Host "Trying to insert project..."
        $insertResponseRaw = [TimeLog.TransactionalApi.SDK.ProjectManagementHandler]::Instance.ProjectManagementClient.CreateProject($project, [TimeLog.TransactionalApi.SDK.ProjectManagementHandler]::Instance.Token)

        if ($insertResponseRaw.ResponseState -eq "Success") {
            Write-Host "Successfully inserted a project!"
        }
        else {
            Write-Host ResponseState: $insertResponseRaw.ResponseState
            Write-Host Messages:
            foreach ($message in $insertResponseRaw.Messages) {
                Write-Host (">> " + $message.Message)
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