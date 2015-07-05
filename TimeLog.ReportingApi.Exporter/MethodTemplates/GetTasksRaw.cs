namespace TimeLog.ReportingApi.Exporter.MethodTemplates
{
    using System.Reflection;
    using System.Xml;

    using TimeLog.ReportingApi.SDK;

    public class GetTasksRaw : IMethod
    {
        public OutputConfiguration GetConfiguration()
        {
            var result = new OutputConfiguration(MethodBase.GetCurrentMethod().DeclaringType.Name);

            result.InternalParameters.Add("TaskId", Task.All);
            result.InternalParameters.Add("ProjectId", Project.All);
            result.InternalParameters.Add("Status", TaskStatus.Active);
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
                configuration.GetIntegerSafe("TaskTypeId")
                );
        }
    }
}