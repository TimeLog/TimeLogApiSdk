namespace TimeLog.ApiConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    using log4net;

    using ReportingApi.SDK;
    using TransactionalApi.SDK;
    using TransactionalApi.SDK.ProjectManagementService;

    public class CreateProjectsForAllCustomers
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CreateProjectsForAllCustomers));

        public static void Comsume()
        {
            if (ServiceHandler.Instance.TryAuthenticate())
            {
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Successfully authenticated on reporting API");
                }

                var customersRaw = ServiceHandler.Instance.Client.GetCustomersShortList(
                    ServiceHandler.Instance.SiteCode,
                    ServiceHandler.Instance.ApiId,
                    ServiceHandler.Instance.ApiPassword,
                    CustomerStatus.All,
                    AccountManager.All);

                IList<Customer> customers = new List<Customer>();
                if (customersRaw.OwnerDocument != null)
                {
                    var namespaceManager = new XmlNamespaceManager(customersRaw.OwnerDocument.NameTable);
                    namespaceManager.AddNamespace("tlp", "http://www.timelog.com/XML/Schema/tlp/v4_4");
                    var customersXml = customersRaw.SelectNodes("tlp:Customer", namespaceManager);

                    if (customersXml != null)
                    {
                        foreach (XmlNode customer in customersXml)
                        {
                            customers.Add(new Customer(customer, namespaceManager));
                        }
                    }
                }

                if (Logger.IsInfoEnabled)
                {
                    Logger.InfoFormat("Fetched {0} customers", customers.Count);
                }

                IEnumerable<string> messages;
                if (SecurityHandler.Instance.TryAuthenticate(out messages))
                {
                    if (Logger.IsInfoEnabled)
                    {
                        Logger.Info("Sucessfully authenticated on transactional API");
                    }

                    foreach (var customer in customers)
                    {
                        var result = ProjectManagementHandler.Instance.ProjectManagementClient.CreateProjectFromTemplate(
                            "Konsulentbistand Skabelon 03",
                            "Konsulentbistand",
                            string.Empty,
                            customer.Name,
                            Guid.Parse("F6B80D70-4355-E511-80D5-005056B220E2"),
                            Guid.Parse("F7B80D70-4355-E511-80D5-005056B220E2"),
                            "ceg",
                            "ceg",
                            string.Empty,
                            string.Empty,
                            true,
                            true,
                            true,
                            true,
                            true,
                            99,
                            ProjectManagementHandler.Instance.Token);
                        if (result.ResponseState == ExecutionStatus.Success)
                        {
                            foreach (var project in result.Return)
                            {
                                if (Logger.IsDebugEnabled)
                                {
                                    Logger.DebugFormat("Project created with ID {0}", project.Item.ProjectID);
                                }
                            }
                        }
                        else
                        {
                            foreach (var apiMessage in result.Messages)
                            {
                                if (Logger.IsErrorEnabled)
                                {
                                    Logger.Error(apiMessage.Message);
                                }
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
            else
            {
                if (Logger.IsWarnEnabled)
                {
                    Logger.Warn("Failed to authenticate to reporting API");
                }
            }
        }
    }
}
