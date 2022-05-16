namespace TimeLog.ReportingApi.Exporter.MethodTemplates
{
    using System.Reflection;
    using System.Xml;

    using ReportingAPI.SDK;

    public class GetTasksRaw : IMethod
    {
        public OutputConfiguration GetConfiguration(ExportFormat format)
        {
            var result = new OutputConfiguration(MethodBase.GetCurrentMethod().DeclaringType.Name)
            {
                ExportFormat = format,
                ListElementType = typeof(Task).FullName
            };

            result.InternalParameters.Add("TaskId", Task.All);
            result.InternalParameters.Add("ProjectId", Project.All);
            result.InternalParameters.Add("Status", TaskStatus.All);
            result.InternalParameters.Add("TaskTypeId", TaskType.All);

            return result;
        }

        public XmlNode GetData(OutputConfiguration configuration)
        {
            return ServiceHandler.Instance.Client.GetTasksRaw(
                ServiceHandler.Instance.SiteCode,
                ServiceHandler.Instance.ApiId,
                ServiceHandler.Instance.ApiPassword,
                configuration.GetIntegerSafe("TaskId"),
                configuration.GetIntegerSafe("ProjectId"),
                configuration.GetIntegerSafe("Status"),
                configuration.GetIntegerSafe("TaskTypeId"));
        }
    }
}