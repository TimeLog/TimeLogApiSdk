namespace TimeLog.ReportingApi.Exporter.MethodTemplates
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Xml;

    using ReportingAPI.SDK;

    public class GetEmployeesRaw : IMethod
    {
        public OutputConfiguration GetConfiguration(ExportFormat format)
        {
            var _result = new OutputConfiguration(MethodBase.GetCurrentMethod().DeclaringType?.Name)
            {
                ExportFormat = format,
                ListElementType = typeof(Employee).FullName
            };

            _result.InternalParameters.Add("EmployeeId", Employee.All);
            _result.InternalParameters.Add("Initials", string.Empty);
            _result.InternalParameters.Add("DepartmentID", Department.All);
            _result.InternalParameters.Add("Status", EmployeeStatus.All.ToString("D"));

            return _result;
        }

        public XmlNode GetData(OutputConfiguration configuration)
        {
            return ServiceHandler.Instance.Client.GetEmployeesRaw(
                ServiceHandler.Instance.SiteCode,
                ServiceHandler.Instance.ApiId,
                ServiceHandler.Instance.ApiPassword,
                configuration.GetIntegerSafe("EmployeeId"),
                configuration.GetStringSafe("Initials"),
                configuration.GetIntegerSafe("DepartmentID"),
                configuration.GetIntegerSafe("Status"));
        }
    }
}