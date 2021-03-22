using System;

namespace TimeLog.ReportingApi.Core.SDK
{
    public class SubOpportunity
    {
        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public int Id { get; set; }

        public int OpportunityId { get; set; }

        public string OpportunitySubject { get; set; }

        public string Name { get; set; }

        public int OpportunityTypeId { get; set; }

        public string OpportunityTypeName { get; set; }

        public int OpportunityStatusId { get; set; }

        public string OpportunityStatusName { get; set; }

        public double ForecastAmount { get; set; }

        public int ForecastRate { get; set; }

        public DateTime Date { get; set; }

        public DateTime Created { get; set; }

        public int CreatedBy { get; set; }

        public DateTime LastModified { get; set; }

        public int LastModifiedBy { get; set; }
    }
}
