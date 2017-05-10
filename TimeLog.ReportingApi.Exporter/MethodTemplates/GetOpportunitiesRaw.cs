namespace TimeLog.ReportingApi.Exporter.MethodTemplates
{
    using System;
    using System.Reflection;
    using System.Xml;

    using SDK;

    public class GetOpportunitiesRaw : IMethod
    {
        public OutputConfiguration GetConfiguration(ExportFormat format)
        {
            var result = new OutputConfiguration(MethodBase.GetCurrentMethod().DeclaringType.Name)
            {
                ExportFormat = format,
                ListElementType = typeof(Opportunity).FullName
            };

            result.InternalParameters.Add("OpportunityID ", Allocation.All);
            result.InternalParameters.Add("CustomerID", Task.All);
            result.InternalParameters.Add("OwnerID", Employee.All);
            result.InternalParameters.Add("OpportunityTypeID", OpportunityType.All);
            result.InternalParameters.Add("BarrierID", Barrier.All);
            result.InternalParameters.Add("StartDate", new DateTime(DateTime.Now.Year, 1, 1));
            result.InternalParameters.Add("EndDate", new DateTime(DateTime.Now.Year, 12, 31));

            return result;
        }

        public XmlNode GetData(OutputConfiguration configuration)
        {
            return ServiceHandler.Instance.Client.GetOpportunitiesRaw(
                ServiceHandler.Instance.SiteCode,
                ServiceHandler.Instance.ApiId,
                ServiceHandler.Instance.ApiPassword,
                configuration.GetIntegerSafe("OpportunityID"),
                configuration.GetIntegerSafe("CustomerID"),
                configuration.GetIntegerSafe("OwnerID"),
                configuration.GetIntegerSafe("OpportunityTypeID"),
                configuration.GetIntegerSafe("BarrierID"),
                configuration.GetDateTimeSafe("StartDate"),
                configuration.GetDateTimeSafe("EndDate"));
        }
    }
}