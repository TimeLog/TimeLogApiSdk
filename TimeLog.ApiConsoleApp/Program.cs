using System.Xml;

namespace TimeLog.ApiConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using log4net;
    using log4net.Config;

    using TimeLog.TransactionalApi.SDK;

    public class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));
        private static readonly DirectoryInfo AppPath = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).Directory;

        public static void Main(string[] args)
        {
            XmlConfigurator.Configure(new FileInfo(AppPath + "\\log4net.config"));
            BasicConfigurator.Configure();

            try
            {
                if (Logger.IsInfoEnabled)
                {
                    Logger.Info("Running application");
                }

                if (ReportingApi.SDK.ServiceHandler.Instance.TryAuthenticate())
                {
                    if (Logger.IsInfoEnabled)
                    {
                        Logger.Info("Sucessfully authenticated on reporting API");
                    }

                    var customersRaw = ReportingApi.SDK.ServiceHandler.Instance.Client.GetCustomersShortList(
                        ReportingApi.SDK.ServiceHandler.Instance.SiteCode,
                        ReportingApi.SDK.ServiceHandler.Instance.ApiId,
                        ReportingApi.SDK.ServiceHandler.Instance.ApiPassword,
                        ReportingApi.SDK.CustomerStatus.All,
                        ReportingApi.SDK.AccountManager.All);

                    if (customersRaw.OwnerDocument != null)
                    {
                        var namespaceManager = new XmlNamespaceManager(customersRaw.OwnerDocument.NameTable);
                        namespaceManager.AddNamespace("tlp", "http://www.timelog.com/XML/Schema/tlp/v4_4");
                        var customers = customersRaw.SelectNodes("tlp:Customer", namespaceManager);

                        if (customers != null)
                        {
                            foreach (XmlNode customer in customers)
                            {
                                var customerName = customer.SelectSingleNode("tlp:Name", namespaceManager);
                                if (customerName != null)
                                {
                                    Logger.Debug(customerName.InnerText);
                                }
                            }
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

                IEnumerable<string> messages;
                if (SecurityHandler.Instance.TryAuthenticate(out messages))
                {
                    if (Logger.IsInfoEnabled)
                    {
                        Logger.Info("Sucessfully authenticated on transactional API");
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

                //const string ProjectTemplate = "standard skabelon";
                //string projectName = string.Empty;
                //string projectNo = string.Empty;
                //string customerName = string.Empty;
                //Guid priceListId = Guid.Empty;
                //Guid priceGroupId = Guid.Empty;
                //string projectManagerInitials = string.Empty;
                //string accountManagerInitials = string.Empty;
                //string handledByDepartmentNo = string.Empty;
                //string orderedByDepartmentNo = string.Empty;

                //ProjectManagementHandler.Instance.ProjectManagementClient.CreateProjectFromTemplate(
                //    ProjectTemplate,
                //    projectName,
                //    projectNo,
                //    customerName,
                //    priceListId,
                //    priceGroupId,
                //    projectManagerInitials,
                //    accountManagerInitials,
                //    handledByDepartmentNo,
                //    orderedByDepartmentNo,
                //    true,
                //    true,
                //    true,
                //    false,
                //    false,
                //    99,
                //    ProjectManagementHandler.Instance.Token);
            }
            catch (Exception ex)
            {
                if (Logger.IsErrorEnabled)
                {
                    Logger.Error("Application loop exception", ex);
                }
            }

            if (Logger.IsInfoEnabled)
            {
                Logger.Info("---");
                Logger.Info("Application loop ended. Click to exit");
            }

            Console.ReadKey();
        }
    }
}
