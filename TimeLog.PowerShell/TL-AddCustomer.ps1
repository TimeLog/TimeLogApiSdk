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

        $customer = New-Object TimeLog.TransactionalApi.SDK.CrmService.Customer
        $customer.Name = 'TestingCustomer' 
        $customer.Action = [TimeLog.TransactionalApi.SDK.CrmService.DataAction]::Created 
        $customer.CalculateVAT = $true 
        $customer.Currency = 'DKK' 
        $customer.ID = '254' 
        $customer.Status = 'Kund'

        $address = New-Object TimeLog.TransactionalApi.SDK.CrmService.Address 
        $address.Address1 = "Addr1" 
        $address.City = 'City1' 
        $address.Country = 'Country1' 
        $address.State = ' State1' 
        $address.ZipCode = '111111'

        $customer.Address = $address

        Write-Host "Trying to insert customer..."
        $insertCustomerResponseRaw = [TimeLog.TransactionalApi.SDK.CrmHandler]::Instance.CrmClient.InsertCustomer($customer, 99, [TimeLog.TransactionalApi.SDK.CrmHandler]::Instance.Token)

        if ($insertCustomerResponseRaw.ResponseState -eq "Success") {
            Write-Host "Successfully inserted a customer!"
        }
        else {
            Write-Host ResponseState: $insertCustomerResponseRaw.ResponseState
            Write-Host Messages:
            foreach ($message in $insertCustomerResponseRaw.Messages) {
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