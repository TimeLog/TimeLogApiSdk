namespace TimeLog.ReportingApi.Exporter.MethodTemplates
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Xml;

    using Core.SDK;

    public class GetTimeOffRegistrationsRaw : IMethod
    {
        public OutputConfiguration GetConfiguration(ExportFormat format)
        {
            var _result = new OutputConfiguration(MethodBase.GetCurrentMethod().DeclaringType?.Name)
            {
                ExportFormat = format,
                ListElementType = typeof(TimeOffRegistration).FullName
            };

            _result.InternalParameters.Add("EmployeeId", Employee.All);
            _result.InternalParameters.Add("DepartmentId", Department.All);
            _result.InternalParameters.Add("FromDate", DateTime.Now.AddMonths(-3));
            _result.InternalParameters.Add("ToDate", DateTime.Now);

            return _result;
        }

        public XmlNode GetData(OutputConfiguration configuration)
        {
            return ServiceHandler.Instance.Client.GetTimeOffRegistrationsRaw(
                ServiceHandler.Instance.SiteCode,
                ServiceHandler.Instance.ApiId,
                ServiceHandler.Instance.ApiPassword,
                configuration.GetIntegerSafe("EmployeeId"),
                configuration.GetIntegerSafe("DepartmentId"),
                configuration.GetDateTimeSafe("FromDate"),
                configuration.GetDateTimeSafe("ToDate"));
        }
    }
}