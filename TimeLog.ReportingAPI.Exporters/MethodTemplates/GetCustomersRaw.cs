namespace TimeLog.ReportingApi.Exporter.MethodTemplates
{
    using System.Reflection;
    using System.Xml;

    using SDK;

    public class GetCustomersRaw : IMethod
    {
        public OutputConfiguration GetConfiguration(ExportFormat format)
        {
            var result = new OutputConfiguration(MethodBase.GetCurrentMethod().DeclaringType.Name)
            {
                ExportFormat = format,
                ListElementType = typeof(Customer).FullName
            };

            result.InternalParameters.Add("CustomerId", Customer.All);
            result.InternalParameters.Add("CustomerStatusId", CustomerStatus.All);
            result.InternalParameters.Add("AccountManagerId", AccountManager.All);
            result.InternalParameters.Add("ForeignId", string.Empty);

            return result;
        }

        public XmlNode GetData(OutputConfiguration configuration)
        {
            return ServiceHandler.Instance.Client.GetCustomersRaw(
                ServiceHandler.Instance.SiteCode,
                ServiceHandler.Instance.ApiId,
                ServiceHandler.Instance.ApiPassword,
                configuration.GetIntegerSafe("CustomerId"),
                configuration.GetIntegerSafe("CustomerStatusId"),
                configuration.GetIntegerSafe("AccountManagerId"));
        }
    }
}