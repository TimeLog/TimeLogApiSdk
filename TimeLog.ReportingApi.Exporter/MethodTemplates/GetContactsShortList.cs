namespace TimeLog.ReportingApi.Exporter.MethodTemplates
{
    using System.Reflection;
    using System.Xml;

    using TimeLog.ReportingApi.SDK;

    public class GetContactsShortList : IMethod
    {
        public OutputConfiguration GetConfiguration()
        {
            var result = new OutputConfiguration(MethodBase.GetCurrentMethod().DeclaringType.Name);

            result.InternalParameters.Add("CustomerId", Customer.All);

            return result;
        }

        public XmlNode GetData(OutputConfiguration configuration)
        {
            return ServiceHandler.Instance.Client.GetContactsShortList(
                ServiceHandler.Instance.SiteCode,
                ServiceHandler.Instance.ApiId,
                ServiceHandler.Instance.ApiPassword,
                configuration.GetIntegerSafe("CustomerId")
                );
        }
    }
}