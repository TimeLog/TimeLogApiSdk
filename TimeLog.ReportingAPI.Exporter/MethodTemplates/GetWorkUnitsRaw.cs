namespace TimeLog.ReportingApi.Exporter.MethodTemplates
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Xml;

    using SDK;

    public class GetWorkUnitsRaw : IMethod
    {
        public OutputConfiguration GetConfiguration(ExportFormat format)
        {
            var _result = new OutputConfiguration(MethodBase.GetCurrentMethod().DeclaringType?.Name)
            {
                ExportFormat = format,
                ListElementType = typeof(WorkUnit).FullName
            };

            _result.InternalParameters.Add("WorkUnitId", WorkUnit.All);
            _result.InternalParameters.Add("EmployeeId", Employee.All);
            _result.InternalParameters.Add("AllocationId", Allocation.All);
            _result.InternalParameters.Add("TaskId", Task.All);
            _result.InternalParameters.Add("ProjectId", Project.All);
            _result.InternalParameters.Add("DepartmentId", Department.All);
            _result.InternalParameters.Add("StartDate", DateTime.Now.AddMonths(-3));
            _result.InternalParameters.Add("EndDate", DateTime.Now);

            return _result;
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
                configuration.GetDateTimeSafe("StartDate").ToString("yyyy-MM-dd"),
                configuration.GetDateTimeSafe("EndDate").ToString("yyyy-MM-dd"));
        }
    }
}