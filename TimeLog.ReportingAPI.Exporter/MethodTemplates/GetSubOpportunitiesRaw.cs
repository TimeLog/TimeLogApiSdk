namespace TimeLog.ReportingApi.Exporter.MethodTemplates
{
    using System.Reflection;
    using System.Xml;

    using ReportingAPI.SDK;

    public class GetSubOpportunitiesRaw : IMethod
    {
        public OutputConfiguration GetConfiguration(ExportFormat format)
        {
            var result = new OutputConfiguration(MethodBase.GetCurrentMethod().DeclaringType.Name)
            {
                ExportFormat = format,
                ListElementType = typeof(SubOpportunity).FullName
            };

            result.InternalParameters.Add("OpportunityID", Opportunity.All);

            return result;
        }

        public XmlNode GetData(OutputConfiguration configuration)
        {
            return ServiceHandler.Instance.Client.GetSubOpportunitiesRaw(
                ServiceHandler.Instance.SiteCode,
                ServiceHandler.Instance.ApiId,
                ServiceHandler.Instance.ApiPassword,
                configuration.GetIntegerSafe("OpportunityID"));
        }
    }
}