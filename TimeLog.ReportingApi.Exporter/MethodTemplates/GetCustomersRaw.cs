namespace TimeLog.ReportingApi.Exporter.MethodTemplates
{
    using System.Reflection;
    using System.Xml;

    using TimeLog.ReportingApi.SDK;

    public class GetCustomersRaw : IMethod
    {
        public OutputConfiguration GetConfiguration()
        {
            var result = new OutputConfiguration(MethodBase.GetCurrentMethod().DeclaringType.Name);

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
                configuration.GetIntegerSafe("AccountManagerId"),
                configuration.GetStringSafe("ForeignId"));
        }
    }
}