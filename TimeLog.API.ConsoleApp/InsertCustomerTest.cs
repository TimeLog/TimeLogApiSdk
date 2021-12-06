using log4net;
using TimeLog.TransactionalAPI.SDK;
using TimeLog.TransactionalAPI.SDK.CRMService;
using TimeLog.TransactionalAPI.SDK.RawHelper;

namespace TimeLog.ApiConsoleApp;

/// <summary>
///     Template class for consuming the transactional API
/// </summary>
public class InsertCustomerTest
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(InsertCustomerTest));

    public static void Consume()
    {
        // For getting the raw XML
        SecurityHandler.Instance.CollectRawRequestResponse = true;
        CRMHandler.Instance.CollectRawRequestResponse = true;
        ProjectManagementHandler.Instance.CollectRawRequestResponse = true;

        if (SecurityHandler.Instance.TryAuthenticate(out var messages))
        {
            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\TryAuthenticate.txt");
            if (Logger.IsInfoEnabled)
            {
                Logger.Info("Sucessfully authenticated on transactional API");
            }

            var customerGuid = Guid.NewGuid();
            var contactGuid = Guid.NewGuid();

            // Get a customer

            var newContact = new Contact
            {
                Firstname = "Peter",
                Lastname = "Hansen",
                Address = new Address
                    {Address1 = "123", Country = "Denmark", City = "Copenhagen", State = "dunno", ZipCode = "68000"},
                Comment = "My new contact",
                CustomerGUID = customerGuid,
                GUID = contactGuid,
                Email = "peter@hansencorp.dk",
                Fax = "00000000",
                Mobile = "11111111",
                Phone = "22222222",
                Title = "CEO",
                IsActive = true,
                ProfessionalTitle1 = "Dear sir",
                ProfessionalTitle2 = "Ms.Eng."
            };

            var newCustomer = new Customer
            {
                Name = "NewCustomerDDDDD",
                AccountNo = string.Empty,
                Action = DataAction.Created,
                Address = new Address
                    {Address1 = "123", Country = "Denmark", City = "Copenhagen", State = "dunno", ZipCode = "68000"},
                CalculateVAT = true,
                CreditorID = string.Empty,
                Currency = "DKK",
                DefaultDiscountPercent = 2,
                Status = "Klant",
                OrganizationNumber = "123456789",
                PaymentTermId = 14,
                GUID = customerGuid,
                IsExternalKeysLoaded = true,
                ExternalKeys = new[]
                {
                    new ExternalSystemContext
                    {
                        ExternalID = Guid.NewGuid().ToString(),
                        SystemName = "DynamicsCRM"
                    }
                }
            };


            var customersResult = CRMHandler.Instance.CrmClient.InsertCustomer(newCustomer, 2,
                CRMHandler.Instance.Token);
            RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\InsertCustomer.txt");
            if (customersResult.ResponseState == ExecutionStatus.Success)
            {
                Logger.Info("Customer created");
                var customer = customersResult.Return.FirstOrDefault();
                if (customer != null)
                {
                    var contactsResult =
                        CRMHandler.Instance.CrmClient.InsertContact(newContact, 99, CRMHandler.Instance.Token);
                    RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\InsertContact.txt");
                    if (contactsResult.ResponseState == ExecutionStatus.Success)
                    {
                        Logger.Info("Contact created");
                    }
                    else
                    {
                        foreach (var apiMessage in contactsResult.Messages)
                        {
                            if (Logger.IsErrorEnabled)
                            {
                                Logger.Error(apiMessage.Message);
                            }
                        }
                    }
                }
                else
                {
                    if (Logger.IsWarnEnabled)
                    {
                        Logger.Warn("No customer found");
                    }
                }
            }
            else
            {
                foreach (var apiMessage in customersResult.Messages)
                {
                    if (Logger.IsErrorEnabled)
                    {
                        Logger.Error(apiMessage.Message);
                    }
                }
            }
        }
        else
        {
            if (Logger.IsWarnEnabled)
            {
                Logger.Warn("Failed to authenticate to transactional API");
                Logger.Warn(string.Join(",", messages));
            }
        }
    }
}