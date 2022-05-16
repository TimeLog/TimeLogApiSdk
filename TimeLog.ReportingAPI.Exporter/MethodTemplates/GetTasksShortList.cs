namespace TimeLog.ReportingApi.Exporter.MethodTemplates
{
    using System.Reflection;
    using System.Xml;

    using Core.SDK;

    public class GetTasksShortList : IMethod
    {
        public OutputConfiguration GetConfiguration(ExportFormat format)
        {
            var result = new OutputConfiguration(MethodBase.GetCurrentMethod().DeclaringType.Name) { ExportFormat = format };

            result.InternalParameters.Add("ProjectId", Project.All);
            result.InternalParameters.Add("Status", TaskStatus.Active);
            result.InternalParameters.Add("TaskTypeId", TaskType.All);

            return result;
        }

        public XmlNode GetData(OutputConfiguration configuration)
        {
            return ServiceHandler.Instance.Client.GetTasksShortList(
                ServiceHandler.Instance.SiteCode,
                ServiceHandler.Instance.ApiId,
                ServiceHandler.Instance.ApiPassword,
                configuration.GetIntegerSafe("ProjectId"),
                configuration.GetIntegerSafe("Status"),
                configuration.GetIntegerSafe("TaskTypeId"));
        }
    }
}