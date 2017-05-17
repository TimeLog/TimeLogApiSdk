namespace TimeLog.ReportingApi.SDK
{
    using System;
    using System.Xml.Serialization;

    public class Opportunity
    {
        /// <summary>
        /// Gets the default parameter value for filtering for all opportunities
        /// </summary>
        [XmlIgnore]
        public static int All
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int ContactId { get; set; }

        public string ContactName { get; set; }

        public string Subject { get; set; }

        public int OpportunityTypeId { get; set; }

        public string OpportunityTypeName { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public int BarrierId { get; set; }

        public double Potential { get; set; }

        public int ForecastRate { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public int CreatedBy { get; set; }

        public DateTime LastModified { get; set; }

        public int LastModifiedBy { get; set; }

        public DateTime QuotationSent { get; set; }

        public string Quotation { get; set; }

        public int CurrencyId { get; set; }

        public string CurrencyISOCode { get; set; }
    }
}
