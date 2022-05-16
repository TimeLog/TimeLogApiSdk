namespace TimeLog.ReportingApi.Exporter.MethodTemplates
{
    using System.Reflection;
    using System.Xml;

    using ReportingAPI.SDK;

    public class GetAllocationsRaw : IMethod
    {
        public OutputConfiguration GetConfiguration(ExportFormat format)
        {
            var result = new OutputConfiguration(MethodBase.GetCurrentMethod().DeclaringType.Name)
            {
                ExportFormat = format,
                ListElementType = typeof(Allocation).FullName
            };

            result.InternalParameters.Add("AllocationId", Allocation.All);
            result.InternalParameters.Add("TaskId", Task.All);
            result.InternalParameters.Add("EmployeeId", Employee.All);
            result.InternalParameters.Add("ProjectId", Project.All);

            return result;
        }

        public XmlNode GetData(OutputConfiguration configuration)
        {
            return ServiceHandler.Instance.Client.GetAllocationsRaw(
                ServiceHandler.Instance.SiteCode,
                ServiceHandler.Instance.ApiId,
                ServiceHandler.Instance.ApiPassword,
                configuration.GetIntegerSafe("AllocationId"),
                configuration.GetIntegerSafe("TaskId"),
                configuration.GetIntegerSafe("EmployeeId"),
                configuration.GetIntegerSafe("ProjectId"));
        }
    }
}