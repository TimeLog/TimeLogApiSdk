using System.Xml.Serialization;

namespace TimeLog.ReportingApi.SDK
{
    using System;
    using System.Globalization;
    using System.Xml;

    /// <summary>
    ///     Placeholder for work unit related constants
    /// </summary>
    public class WorkUnit
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Customer" /> class.
        /// </summary>
        public WorkUnit()
        {
            this.ActualExchangeRate = 0;
            this.AdditionalTextField = string.Empty;
            this.AllocationID = -1;
            this.ApprovedStatus = 0;
            this.BARAmount = 0;
            this.BARAmountProjectCurrency = 0;
            this.BARHours = 0;
            this.BARHourlyRate = 0;
            this.BARHourlyRateProjectCurrency = 0;
            this.BillableStatus = -1;
            this.Created = DateTime.Now;
            this.CreatedBy = string.Empty;
            this.CreatedByEmployeeID = -1;
            this.CustomerID = -1;
            this.CustomerName = string.Empty;
            this.Date = DateTime.Now;
            this.DepartmentID = -1;
            this.DepartmentName = string.Empty;
            this.EmployeeFirstName = string.Empty;
            this.EmployeeID = -1;
            this.EmployeeInitials = string.Empty;
            this.EmployeeLastName = string.Empty;
            this.EstimatedAmount = 0;
            this.EstimatedAmountProjectCurrency = 0;
            this.EstimatedHours = 0;
            this.EstimatedHourlyRate = 0;
            this.EstimatedHourlyRateProjectCurrency = 0;
            this.Id = -1;
            this.IsBillable = false;
            this.InvAmount = 0;
            this.InvHours = 0;
            this.InvoiceStatus = 0;
            this.InvoicedAmount = 0;
            this.InvoicedAmountProjectCurrency = 0;
            this.InvoicedHours = 0;
            this.InvoicedHourlyRate = 0;
            this.InvoicedHourlyRateProjectCurrency = 0;
            this.LastModifiedAt = DateTime.Now;
            this.LastModifiedBy = string.Empty;
            this.LastModifiedByEmployeeID = -1;
            this.MonthlyPeriod = string.Empty;
            this.Note = string.Empty;
            this.OvertimeFactor = 0;
            this.ProjectID = -1;
            this.ProjectName = string.Empty;
            this.RegAmount = 0;
            this.RegAmountProjectCurrency = 0;
            this.RegHours = 0;
            this.RegHourlyRate = 0;
            this.RegHourlyRateProjectCurrency = 0;
            this.TaskID = -1;
            this.TaskName = string.Empty;
            this.TimeRegistrationGuid = Guid.Empty;
            this.UserID = -1;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WorkUnit" /> class.
        /// </summary>
        /// <param name="node">The XML node to initialize from</param>
        /// <param name="namespaceManager">The namespace manager</param>
        public WorkUnit(XmlNode node, XmlNamespaceManager namespaceManager)
        {
            this.ActualExchangeRate = node.GetDoubleSafe("tlp:ActualExchangeRate", namespaceManager, ServiceHandler.DataCulture);
            this.AdditionalTextField = node.GetStringSafe("tlp:AdditionalTextField", namespaceManager);
            this.AllocationID = node.GetIntSafe("tlp:AllocationID", namespaceManager);
            this.ApprovedStatus = node.GetIntSafe("tlp:ApprovedStatus", namespaceManager);
            this.BARAmount = node.GetDoubleSafe("tlp:BARAmount", namespaceManager, ServiceHandler.DataCulture);
            this.BARAmountProjectCurrency = node.GetDoubleSafe("tlp:BARAmountProjectCurrency", namespaceManager, ServiceHandler.DataCulture);
            this.BARHours = node.GetDoubleSafe("tlp:EstimatedHours", namespaceManager, ServiceHandler.DataCulture);
            this.BARHourlyRate = node.GetDoubleSafe("tlp:BARHourlyRate", namespaceManager, ServiceHandler.DataCulture);
            this.BARHourlyRateProjectCurrency = node.GetDoubleSafe("tlp:BARHourlyRateProjectCurrency", namespaceManager, ServiceHandler.DataCulture);
            this.BillableStatus = node.GetIntSafe("tlp:BillableStatus", namespaceManager);
            this.CostAmount = node.GetDoubleSafe("tlp:CostAmount", namespaceManager, ServiceHandler.DataCulture);
            this.Created = node.GetDateTimeSafe("tlp:Created", namespaceManager);
            this.CreatedBy = node.GetStringSafe("tlp:CreatedBy", namespaceManager);
            this.CreatedByEmployeeID = node.GetIntSafe("tlp:CreatedByEmployeeID", namespaceManager);
            this.CustomerID = node.GetIntSafe("tlp:CustomerId", namespaceManager);
            this.CustomerName = node.GetStringSafe("tlp:CustomerName", namespaceManager);
            this.Date = node.GetDateTimeSafe("tlp:Date", namespaceManager);
            this.DepartmentID = node.GetIntSafe("tlp:DepartmentID", namespaceManager);
            this.DepartmentName = node.GetStringSafe("tlp:DepartmentName", namespaceManager);
            this.EmployeeFirstName = node.GetStringSafe("tlp:EmployeeFirstName", namespaceManager);
            this.EmployeeID = node.GetIntSafe("tlp:EmployeeID", namespaceManager);
            this.EmployeeInitials = node.GetStringSafe("tlp:EmployeeInitials", namespaceManager);
            this.EmployeeLastName = node.GetStringSafe("tlp:EmployeeLastName", namespaceManager);
            this.EstimatedAmount = node.GetDoubleSafe("tlp:EstimatedAmount", namespaceManager, ServiceHandler.DataCulture);
            this.EstimatedAmountProjectCurrency = node.GetDoubleSafe("tlp:EstimatedAmountProjectCurrency", namespaceManager, ServiceHandler.DataCulture);
            this.EstimatedHours = node.GetDoubleSafe("tlp:EstimatedHours", namespaceManager, ServiceHandler.DataCulture);
            this.EstimatedHourlyRate = node.GetDoubleSafe("tlp:EstimatedHourlyRate", namespaceManager, ServiceHandler.DataCulture);
            this.EstimatedHourlyRateProjectCurrency = node.GetDoubleSafe("tlp:EstimatedHourlyRateProjectCurrency", namespaceManager, ServiceHandler.DataCulture);
            this.Id = int.Parse(node.Attributes["ID"].InnerText);
            this.IsBillable = node.GetBoolTimeSafe("tlp:IsBillable", namespaceManager);
            this.InvAmount = node.GetDoubleSafe("tlp:InvAmount", namespaceManager, ServiceHandler.DataCulture);
            this.InvHours = node.GetDoubleSafe("tlp:InvHours", namespaceManager, ServiceHandler.DataCulture);
            this.InvoiceStatus = node.GetIntSafe("tlp:InvoiceStatus", namespaceManager);
            this.InvoicedAmount = node.GetDoubleSafe("tlp:InvoicedAmount", namespaceManager, ServiceHandler.DataCulture);
            this.InvoicedAmountProjectCurrency = node.GetDoubleSafe("tlp:InvoicedAmountProjectCurrency", namespaceManager, ServiceHandler.DataCulture);
            this.InvoicedHours = node.GetDoubleSafe("tlp:InvoicedHours", namespaceManager, ServiceHandler.DataCulture);
            this.InvoicedHourlyRate = node.GetDoubleSafe("tlp:InvoicedHourlyRate", namespaceManager, ServiceHandler.DataCulture);
            this.InvoicedHourlyRateProjectCurrency = node.GetDoubleSafe("tlp:InvoicedHourlyRateProjectCurrency", namespaceManager, ServiceHandler.DataCulture);
            this.LastModifiedAt = node.GetDateTimeSafe("tlp:LastModifiedAt", namespaceManager);
            this.LastModifiedBy = node.GetStringSafe("tlp:LastModifiedBy", namespaceManager);
            this.LastModifiedByEmployeeID = node.GetIntSafe("tlp:LastModifiedByEmployeeID", namespaceManager);
            this.MonthlyPeriod = node.GetStringSafe("tlp:MonthlyPeriod", namespaceManager);
            this.Note = node.GetStringSafe("tlp:Note", namespaceManager).Trim('\r', '\n');
            this.OvertimeFactor = node.GetIntSafe("tlp:OvertimeFactor", namespaceManager);
            this.ProjectID = node.GetIntSafe("tlp:ProjectID", namespaceManager);
            this.ProjectName = node.GetStringSafe("tlp:ProjectName", namespaceManager);
            this.RegAmount = node.GetDoubleSafe("tlp:RegAmount", namespaceManager, ServiceHandler.DataCulture);
            this.RegAmountProjectCurrency = node.GetDoubleSafe("tlp:RegAmountProjectCurrency", namespaceManager, ServiceHandler.DataCulture);
            this.RegHours = node.GetDoubleSafe("tlp:RegHours", namespaceManager, ServiceHandler.DataCulture);
            this.RegHourlyRate = node.GetDoubleSafe("tlp:RegHourlyRate", namespaceManager, ServiceHandler.DataCulture);
            this.RegHourlyRateProjectCurrency = node.GetDoubleSafe("tlp:RegHourlyRateProjectCurrency", namespaceManager, ServiceHandler.DataCulture);
            this.TaskID = node.GetIntSafe("tlp:TaskID", namespaceManager);
            this.TaskName = node.GetStringSafe("tlp:TaskName", namespaceManager);
            this.TimeRegistrationGuid = Guid.Parse(node.GetStringSafe("tlp:TimeRegistrationGuid", namespaceManager));
            this.UserID = node.GetIntSafe("tlp:UserID", namespaceManager);
        }

        /// <summary>
        ///     Gets the default parameter value for filtering for all work units
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

        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public Guid TimeRegistrationGuid { get; set; }

        /// <summary>
        /// Gets or sets the overtime factor 
        /// </summary>
        public int OvertimeFactor { get; set; }

        /// <summary>
        /// Gets or sets the employee identifier
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// Gets or sets the employee initials
        /// </summary>
        public string EmployeeInitials { get; set; }

        /// <summary>
        /// Gets or sets the employee first name
        /// </summary>
        public string EmployeeFirstName { get; set; }

        /// <summary>
        /// Gets or sets the employee last name
        /// </summary>
        public string EmployeeLastName { get; set; }

        /// <summary>
        /// Gets or sets the allocation identifier
        /// </summary>
        public int AllocationID { get; set; }

        /// <summary>
        /// Gets or sets the task identifier
        /// </summary>
        public int TaskID { get; set; }

        /// <summary>
        /// Gets or sets the task name
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// Gets or sets the project identifier
        /// </summary>
        public int ProjectID { get; set; }

        /// <summary>
        /// Gets or sets the project name
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the customer name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the registration date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the registration note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the employee department identifier based on the registration creation date
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        /// Gets or sets the employee department name based on the registration creation date
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Gets or sets the additional text field
        /// </summary>
        public string AdditionalTextField { get; set; }

        /// <summary>
        /// Gets or sets the actual exchange rate
        /// </summary>
        public double ActualExchangeRate { get; set; }

        /// <summary>
        /// Gets or sets the registered hours
        /// </summary>
        public double RegHours { get; set; }

        /// <summary>
        /// Gets or sets the registered hourly rate
        /// </summary>
        public double RegHourlyRate { get; set; }

        /// <summary>
        /// Gets or sets the registered hourly rate in project currency
        /// </summary>
        public double RegHourlyRateProjectCurrency { get; set; }

        /// <summary>
        /// Gets or sets the registered amount
        /// </summary>
        public double RegAmount { get; set; }

        /// <summary>
        /// Gets or sets the registered amount in project currency
        /// </summary>
        public double RegAmountProjectCurrency { get; set; }

        /// <summary>
        /// Gets or sets the estimated hours
        /// </summary>
        public double EstimatedHours { get; set; }

        /// <summary>
        /// Gets or sets the estimated hourly rate
        /// </summary>
        public double EstimatedHourlyRate { get; set; }

        /// <summary>
        /// Gets or sets the estimated hourly rate in project currency
        /// </summary>
        public double EstimatedHourlyRateProjectCurrency { get; set; }

        /// <summary>
        /// Gets or sets the estimated amount
        /// </summary>
        public double EstimatedAmount { get; set; }

        /// <summary>
        /// Gets or sets the estimated amount in project currency
        /// </summary>
        public double EstimatedAmountProjectCurrency { get; set; }

        /// <summary>
        /// Gets or sets the booked as revenue hours
        /// </summary>
        public double BARHours { get; set; }

        /// <summary>
        /// Gets or sets the booked as revenue hourly rate
        /// </summary>
        public double BARHourlyRate { get; set; }

        /// <summary>
        /// Gets or sets the booked as revenue hourly rate in project currency
        /// </summary>
        public double BARHourlyRateProjectCurrency { get; set; }

        /// <summary>
        /// Gets or sets the booked as revenue amount
        /// </summary>
        public double BARAmount { get; set; }

        /// <summary>
        /// Gets or sets the booked as revenue amount in project currency
        /// </summary>
        public double BARAmountProjectCurrency { get; set; }

        /// <summary>
        /// Gets or sets the invoiced hours
        /// </summary>
        public double InvoicedHours { get; set; }

        /// <summary>
        /// Gets or sets the invoiced hourly rate
        /// </summary>
        public double InvoicedHourlyRate { get; set; }

        /// <summary>
        /// Gets or sets the invoiced hourly rate in project currency
        /// </summary>
        public double InvoicedHourlyRateProjectCurrency { get; set; }

        /// <summary>
        /// Gets or sets the invoiced amount
        /// </summary>
        public double InvoicedAmount { get; set; }

        /// <summary>
        /// Gets or sets the invoiced amount in project currency
        /// </summary>
        public double InvoicedAmountProjectCurrency { get; set; }

        /// <summary>
        /// Gets or sets the invoiced hours (Obsolete)
        /// </summary>
        [Obsolete("Use InvoicedHours or BARHours")]
        public double InvHours { get; set; }

        /// <summary>
        /// Gets or sets the invoiced amount
        /// </summary>
        [Obsolete("Use InvoicedAmount or BARAmount")]
        public double InvAmount { get; set; }

        /// <summary>
        /// Gets or sets the cost amount
        /// </summary>
        public double CostAmount { get; set; }

        /// <summary>
        /// Gets or sets the invoice status (0 = not booked or invoiced, 1 = added to voucher, 2 = added to invoice)
        /// </summary>
        public int InvoiceStatus { get; set; }

        /// <summary>
        /// Gets or sets the billable status (-3 = non-billable registration on billable task with hourly rate of zero, -2 = nillable registration on non-billable task, -1 = non-billable registration on non-billable task, 1 = billable registration on billable task, 2 = non-billable registration on billable task)
        /// </summary>
        public int BillableStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the registration is billable
        /// </summary>
        public bool IsBillable { get; set; }

        /// <summary>
        /// Gets or sets the approval state (0 = time sheet open, 10 = time sheet closed, 20 = project manager approved, 30 = department manager approved)
        /// </summary>
        public int ApprovedStatus { get; set; }

        /// <summary>
        /// Gets or sets the monthly period
        /// </summary>
        public string MonthlyPeriod { get; set; }

        /// <summary>
        /// Gets or sets the creation date
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the creation employee identifier
        /// </summary>
        public int CreatedByEmployeeID { get; set; }

        /// <summary>
        /// Gets or sets the creation employee name
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the last modified date
        /// </summary>
        public DateTime LastModifiedAt { get; set; }

        /// <summary>
        /// Gets or sets the last modified employee identifier
        /// </summary>
        public int LastModifiedByEmployeeID { get; set; }

        /// <summary>
        /// Gets or sets the last modified employee name
        /// </summary>
        public string LastModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the user of the registration
        /// </summary>
        public int UserID { get; set; }
    }
}