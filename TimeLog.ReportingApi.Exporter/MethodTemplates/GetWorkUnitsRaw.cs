namespace TimeLog.ReportingApi.Exporter.MethodTemplates
{
    using System;
    using System.Reflection;
    using System.Xml;

    using SDK;

    public class GetWorkUnitsRaw : IMethod
    {
        public OutputConfiguration GetConfiguration(ExportFormat format)
        {
            var result = new OutputConfiguration(MethodBase.GetCurrentMethod().DeclaringType.Name) { ExportFormat = format };

            result.InternalParameters.Add("WorkUnitId", WorkUnit.All);
            result.InternalParameters.Add("EmployeeId", Employee.All);
            result.InternalParameters.Add("AllocationId", Allocation.All);
            result.InternalParameters.Add("TaskId", Task.All);
            result.InternalParameters.Add("ProjectId", Project.All);
            result.InternalParameters.Add("DepartmentId", Department.All);
            result.InternalParameters.Add("StartDate", DateTime.Now.AddMonths(-3));
            result.InternalParameters.Add("EndDate", DateTime.Now);

            return result;
        }

        public XmlNode GetData(OutputConfiguration configuration)
        {
            return ServiceHandler.Instance.Client.GetWorkUnitsRaw(
                ServiceHandler.Instance.SiteCode,
                ServiceHandler.Instance.ApiId,
                ServiceHandler.Instance.ApiPassword,
                configuration.GetIntegerSafe("WorkUnitId"),
                configuration.GetIntegerSafe("EmployeeId"),
                configuration.GetIntegerSafe("AllocationId"),
                configuration.GetIntegerSafe("TaskId"),
                configuration.GetIntegerSafe("ProjectId"),
                configuration.GetIntegerSafe("DepartmentId"),
                configuration.GetDateTimeSafe("StartDate"),
                configuration.GetDateTimeSafe("EndDate"));
        }
    }
}