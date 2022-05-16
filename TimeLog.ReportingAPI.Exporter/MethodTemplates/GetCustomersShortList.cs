namespace TimeLog.ReportingApi.Exporter.MethodTemplates
{
    using System.Reflection;
    using System.Xml;

    using ReportingAPI.SDK;

    public class GetCustomersShortList : IMethod
    {
        public OutputConfiguration GetConfiguration(ExportFormat format)
        {
            var result = new OutputConfiguration(MethodBase.GetCurrentMethod().DeclaringType.Name) { ExportFormat = format };

            result.InternalParameters.Add("CustomerStatusId", CustomerStatus.All);
            result.InternalParameters.Add("AccountManagerId", AccountManager.All);

            return result;
        }

        public XmlNode GetData(OutputConfiguration configuration)
        {
            return ServiceHandler.Instance.Client.GetCustomersShortList(
                ServiceHandler.Instance.SiteCode,
                ServiceHandler.Instance.ApiId,
                ServiceHandler.Instance.ApiPassword,
                configuration.GetIntegerSafe("CustomerStatusId"),
                configuration.GetIntegerSafe("AccountManagerId"));
        }
    }
}