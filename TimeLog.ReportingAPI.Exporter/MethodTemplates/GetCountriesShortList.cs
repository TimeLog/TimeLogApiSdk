namespace TimeLog.ReportingApi.Exporter.MethodTemplates
{
    using System.Reflection;
    using System.Xml;

    using Core.SDK;

    public class GetCountriesShortList : IMethod
    {
        public OutputConfiguration GetConfiguration(ExportFormat format)
        {
            var result = new OutputConfiguration(MethodBase.GetCurrentMethod().DeclaringType.Name) { ExportFormat = format };

            return result;
        }

        public XmlNode GetData(OutputConfiguration configuration)
        {
            return ServiceHandler.Instance.Client.GetCountriesShortList(ServiceHandler.Instance.SiteCode, ServiceHandler.Instance.ApiId, ServiceHandler.Instance.ApiPassword);
        }
    }
}