namespace TimeLog.ApiConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using log4net;

    using TimeLog.TransactionalApi.SDK;
    using TimeLog.TransactionalApi.SDK.CrmService;
    using TimeLog.TransactionalApi.SDK.ProjectManagementService;
    using TimeLog.TransactionalApi.SDK.RawHelper;

    using ExecutionStatus = TimeLog.TransactionalApi.SDK.ProjectManagementService.ExecutionStatus;

    /// <summary>
    /// Template class for consuming the transactional API
    /// </summary>
    public class InsertCustomerTest
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(InsertCustomerTest));

        public static void Consume()
        {
            // For getting the raw XML
            SecurityHandler.Instance.CollectRawRequestResponse = true;
            CrmHandler.Instance.CollectRawRequestResponse = true;
            ProjectManagementHandler.Instance.CollectRawRequestResponse = true;

            IEnumerable<string> messages;
            if (SecurityHandler.Instance.TryAuthenticate(out messages))
            {
                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\TryAuthenticate.txt");
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Sucessfully authenticated on transactional API");
                }

                // Get a customer

                var newContactHeader = new ContactHeader();
                newContactHeader.FirstName = "FirstTest";
                newContactHeader.LastName = "LastTest";
                newContactHeader.GUID = Guid.NewGuid();

                var newCustomer = new Customer()
                {
                    Name = "NewCustomerDDDDD",
                    AccountNo = String.Empty,
                    Action = TransactionalApi.SDK.CrmService.DataAction.Created,
                    Address = new Address() { Address1 = "123", Country = "Denmark", City = "Copenhagen", State = "dunno", ZipCode = "68000"},
                    CalculateVAT = true,
                    Contacts = new ContactHeader[] { newContactHeader },
                    CreditorID = string.Empty,
                    Currency = "DKK",
                    DefaultDiscountPercent = 2,
                    Status = "50. Kunde 123456",
                    ID = 11078
                };

                var customersResult = CrmHandler.Instance.CrmClient.InsertCustomer(newCustomer, 2,
                    CrmHandler.Instance.Token);
                RawMessageHelper.Instance.SaveRecentRequestResponsePair("c:\\temp\\InsertCustomer.txt");
                if (customersResult.ResponseState == TransactionalApi.SDK.CrmService.ExecutionStatus.Success)
                {
                    var customer = customersResult.Return.FirstOrDefault();
                    if (customer != null)
                    {
                        
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
}