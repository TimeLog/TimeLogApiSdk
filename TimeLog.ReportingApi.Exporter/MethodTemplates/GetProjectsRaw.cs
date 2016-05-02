namespace TimeLog.ReportingApi.Exporter.MethodTemplates
{
    using System.Reflection;
    using System.Xml;

    using SDK;

    public class GetProjectsRaw : IMethod
    {
        public OutputConfiguration GetConfiguration(ExportFormat format)
        {
            var result = new OutputConfiguration(MethodBase.GetCurrentMethod().DeclaringType.Name) { ExportFormat = format };

            result.InternalParameters.Add("ProjectId", Project.All);
            result.InternalParameters.Add("CustomerId", Customer.All);
            result.InternalParameters.Add("Status", ProjectStatus.All);
            result.InternalParameters.Add("ProjectManagerId", ProjectManager.All);

            return result;
        }

        public XmlNode GetData(OutputConfiguration configuration)
        {
            return ServiceHandler.Instance.Client.GetProjectsRaw(
                ServiceHandler.Instance.SiteCode,
                ServiceHandler.Instance.ApiId,
                ServiceHandler.Instance.ApiPassword,
                configuration.GetIntegerSafe("ProjectId"),
                configuration.GetIntegerSafe("Status"),
                configuration.GetIntegerSafe("CustomerId"),
                configuration.GetIntegerSafe("ProjectManagerId"));
        }
    }
}