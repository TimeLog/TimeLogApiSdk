using System.Xml;
using log4net;
using TimeLog.ReportingApi.SDK;

namespace TimeLog.ApiConsoleApp
{
    /// <summary>
    /// Template class for consuming the reporting API
    /// </summary>
    public class ConsumeReportingApi
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (ConsumeReportingApi));

        public static void Consume()
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
                                if (Logger.IsDebugEnabled)
                                {
                                    Logger.Debug(customerName.InnerText);
                                }
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
        }
    }
}