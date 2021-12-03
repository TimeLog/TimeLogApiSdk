using System;
using System.Xml;
using System.Xml.Serialization;

namespace TimeLog.ReportingAPI.SDK;

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
        ActualExchangeRate = 0;
        AdditionalTextField = string.Empty;
        AllocationID = -1;
        ApprovedStatus = 0;
        BarAmount = 0;
        BarAmountProjectCurrency = 0;
        BarHours = 0;
        BarHourlyRate = 0;
        BarHourlyRateProjectCurrency = 0;
        BillableStatus = -1;
        Created = DateTime.Now;
        CreatedBy = string.Empty;
        CreatedByEmployeeID = -1;
        CustomerID = -1;
        CustomerName = string.Empty;
        Date = DateTime.Now;
        DepartmentID = -1;
        DepartmentName = string.Empty;
        EmployeeFirstName = string.Empty;
        EmployeeID = -1;
        EmployeeInitials = string.Empty;
        EmployeeLastName = string.Empty;
        EstimatedAmount = 0;
        EstimatedAmountProjectCurrency = 0;
        EstimatedHours = 0;
        EstimatedHourlyRate = 0;
        EstimatedHourlyRateProjectCurrency = 0;
        Id = -1;
        IsBillable = false;
        InvAmount = 0;
        InvHours = 0;
        InvoiceStatus = 0;
        InvoicedAmount = 0;
        InvoicedAmountProjectCurrency = 0;
        InvoicedHours = 0;
        InvoicedHourlyRate = 0;
        InvoicedHourlyRateProjectCurrency = 0;
        LastModifiedAt = DateTime.Now;
        LastModifiedBy = string.Empty;
        LastModifiedByEmployeeID = -1;
        MonthlyPeriod = string.Empty;
        Note = string.Empty;
        OvertimeFactor = 0;
        ProjectID = -1;
        ProjectName = string.Empty;
        RegAmount = 0;
        RegAmountProjectCurrency = 0;
        RegHours = 0;
        RegHourlyRate = 0;
        RegHourlyRateProjectCurrency = 0;
        TaskID = -1;
        TaskName = string.Empty;
        TimeRegistrationGuid = Guid.Empty;
        UserID = -1;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="WorkUnit" /> class.
    /// </summary>
    /// <param name="node">The XML node to initialize from</param>
    /// <param name="namespaceManager">The namespace manager</param>
    public WorkUnit(XmlNode node, XmlNamespaceManager namespaceManager)
    {
        ActualExchangeRate = node.GetDoubleSafe("tlp:ActualExchangeRate", namespaceManager, ServiceHandler.DataCulture);
        AdditionalTextField = node.GetStringSafe("tlp:AdditionalTextField", namespaceManager);
        AllocationID = node.GetIntSafe("tlp:AllocationID", namespaceManager);
        ApprovedStatus = node.GetIntSafe("tlp:ApprovedStatus", namespaceManager);
        BarAmount = node.GetDoubleSafe("tlp:BARAmount", namespaceManager, ServiceHandler.DataCulture);
        BarAmountProjectCurrency =
            node.GetDoubleSafe("tlp:BARAmountProjectCurrency", namespaceManager, ServiceHandler.DataCulture);
        BarHours = node.GetDoubleSafe("tlp:EstimatedHours", namespaceManager, ServiceHandler.DataCulture);
        BarHourlyRate = node.GetDoubleSafe("tlp:BARHourlyRate", namespaceManager, ServiceHandler.DataCulture);
        BarHourlyRateProjectCurrency = node.GetDoubleSafe("tlp:BARHourlyRateProjectCurrency", namespaceManager,
            ServiceHandler.DataCulture);
        BillableStatus = node.GetIntSafe("tlp:BillableStatus", namespaceManager);
        CostAmount = node.GetDoubleSafe("tlp:CostAmount", namespaceManager, ServiceHandler.DataCulture);
        Created = node.GetDateTimeSafe("tlp:Created", namespaceManager);
        CreatedBy = node.GetStringSafe("tlp:CreatedBy", namespaceManager);
        CreatedByEmployeeID = node.GetIntSafe("tlp:CreatedByEmployeeID", namespaceManager);
        CustomerID = node.GetIntSafe("tlp:CustomerId", namespaceManager);
        CustomerName = node.GetStringSafe("tlp:CustomerName", namespaceManager);
        Date = node.GetDateTimeSafe("tlp:Date", namespaceManager);
        DepartmentID = node.GetIntSafe("tlp:DepartmentID", namespaceManager);
        DepartmentName = node.GetStringSafe("tlp:DepartmentName", namespaceManager);
        EmployeeFirstName = node.GetStringSafe("tlp:EmployeeFirstName", namespaceManager);
        EmployeeID = node.GetIntSafe("tlp:EmployeeID", namespaceManager);
        EmployeeInitials = node.GetStringSafe("tlp:EmployeeInitials", namespaceManager);
        EmployeeLastName = node.GetStringSafe("tlp:EmployeeLastName", namespaceManager);
        EstimatedAmount = node.GetDoubleSafe("tlp:EstimatedAmount", namespaceManager, ServiceHandler.DataCulture);
        EstimatedAmountProjectCurrency = node.GetDoubleSafe("tlp:EstimatedAmountProjectCurrency", namespaceManager,
            ServiceHandler.DataCulture);
        EstimatedHours = node.GetDoubleSafe("tlp:EstimatedHours", namespaceManager, ServiceHandler.DataCulture);
        EstimatedHourlyRate =
            node.GetDoubleSafe("tlp:EstimatedHourlyRate", namespaceManager, ServiceHandler.DataCulture);
        EstimatedHourlyRateProjectCurrency = node.GetDoubleSafe("tlp:EstimatedHourlyRateProjectCurrency",
            namespaceManager, ServiceHandler.DataCulture);
        Id = int.Parse(node.Attributes["ID"].InnerText);
        IsBillable = node.GetBoolTimeSafe("tlp:IsBillable", namespaceManager);
        InvAmount = node.GetDoubleSafe("tlp:InvAmount", namespaceManager, ServiceHandler.DataCulture);
        InvHours = node.GetDoubleSafe("tlp:InvHours", namespaceManager, ServiceHandler.DataCulture);
        InvoiceStatus = node.GetIntSafe("tlp:InvoiceStatus", namespaceManager);
        InvoicedAmount = node.GetDoubleSafe("tlp:InvoicedAmount", namespaceManager, ServiceHandler.DataCulture);
        InvoicedAmountProjectCurrency = node.GetDoubleSafe("tlp:InvoicedAmountProjectCurrency", namespaceManager,
            ServiceHandler.DataCulture);
        InvoicedHours = node.GetDoubleSafe("tlp:InvoicedHours", namespaceManager, ServiceHandler.DataCulture);
        InvoicedHourlyRate = node.GetDoubleSafe("tlp:InvoicedHourlyRate", namespaceManager, ServiceHandler.DataCulture);
        InvoicedHourlyRateProjectCurrency = node.GetDoubleSafe("tlp:InvoicedHourlyRateProjectCurrency",
            namespaceManager, ServiceHandler.DataCulture);
        LastModifiedAt = node.GetDateTimeSafe("tlp:LastModifiedAt", namespaceManager);
        LastModifiedBy = node.GetStringSafe("tlp:LastModifiedBy", namespaceManager);
        LastModifiedByEmployeeID = node.GetIntSafe("tlp:LastModifiedByEmployeeID", namespaceManager);
        MonthlyPeriod = node.GetStringSafe("tlp:MonthlyPeriod", namespaceManager);
        Note = node.GetStringSafe("tlp:Note", namespaceManager).Trim('\r', '\n');
        OvertimeFactor = node.GetIntSafe("tlp:OvertimeFactor", namespaceManager);
        ProjectID = node.GetIntSafe("tlp:ProjectID", namespaceManager);
        ProjectName = node.GetStringSafe("tlp:ProjectName", namespaceManager);
        RegAmount = node.GetDoubleSafe("tlp:RegAmount", namespaceManager, ServiceHandler.DataCulture);
        RegAmountProjectCurrency =
            node.GetDoubleSafe("tlp:RegAmountProjectCurrency", namespaceManager, ServiceHandler.DataCulture);
        RegHours = node.GetDoubleSafe("tlp:RegHours", namespaceManager, ServiceHandler.DataCulture);
        RegHourlyRate = node.GetDoubleSafe("tlp:RegHourlyRate", namespaceManager, ServiceHandler.DataCulture);
        RegHourlyRateProjectCurrency = node.GetDoubleSafe("tlp:RegHourlyRateProjectCurrency", namespaceManager,
            ServiceHandler.DataCulture);
        TaskID = node.GetIntSafe("tlp:TaskID", namespaceManager);
        TaskName = node.GetStringSafe("tlp:TaskName", namespaceManager);
        TimeRegistrationGuid = Guid.Parse(node.GetStringSafe("tlp:TimeRegistrationGuid", namespaceManager));
        UserID = node.GetIntSafe("tlp:UserID", namespaceManager);
    }

    /// <summary>
    ///     Gets the default parameter value for filtering for all work units
    /// </summary>
    [XmlIgnore]
    public static int All => 0;

    /// <summary>
    ///     Gets or sets the identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the identifier
    /// </summary>
    public Guid TimeRegistrationGuid { get; set; }

    /// <summary>
    ///     Gets or sets the overtime factor
    /// </summary>
    public int OvertimeFactor { get; set; }

    /// <summary>
    ///     Gets or sets the employee identifier
    /// </summary>
    public int EmployeeID { get; set; }

    /// <summary>
    ///     Gets or sets the employee initials
    /// </summary>
    public string EmployeeInitials { get; set; }

    /// <summary>
    ///     Gets or sets the employee first name
    /// </summary>
    public string EmployeeFirstName { get; set; }

    /// <summary>
    ///     Gets or sets the employee last name
    /// </summary>
    public string EmployeeLastName { get; set; }

    /// <summary>
    ///     Gets or sets the allocation identifier
    /// </summary>
    public int AllocationID { get; set; }

    /// <summary>
    ///     Gets or sets the task identifier
    /// </summary>
    public int TaskID { get; set; }

    /// <summary>
    ///     Gets or sets the task name
    /// </summary>
    public string TaskName { get; set; }

    /// <summary>
    ///     Gets or sets the project identifier
    /// </summary>
    public int ProjectID { get; set; }

    /// <summary>
    ///     Gets or sets the project name
    /// </summary>
    public string ProjectName { get; set; }

    /// <summary>
    ///     Gets or sets the customer identifier
    /// </summary>
    public int CustomerID { get; set; }

    /// <summary>
    ///     Gets or sets the customer name
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    ///     Gets or sets the registration date
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    ///     Gets or sets the registration note
    /// </summary>
    public string Note { get; set; }

    /// <summary>
    ///     Gets or sets the employee department identifier based on the registration creation date
    /// </summary>
    public int DepartmentID { get; set; }

    /// <summary>
    ///     Gets or sets the employee department name based on the registration creation date
    /// </summary>
    public string DepartmentName { get; set; }

    /// <summary>
    ///     Gets or sets the additional text field
    /// </summary>
    public string AdditionalTextField { get; set; }

    /// <summary>
    ///     Gets or sets the actual exchange rate
    /// </summary>
    public double ActualExchangeRate { get; set; }

    /// <summary>
    ///     Gets or sets the registered hours
    /// </summary>
    public double RegHours { get; set; }

    /// <summary>
    ///     Gets or sets the registered hourly rate
    /// </summary>
    public double RegHourlyRate { get; set; }

    /// <summary>
    ///     Gets or sets the registered hourly rate in project currency
    /// </summary>
    public double RegHourlyRateProjectCurrency { get; set; }

    /// <summary>
    ///     Gets or sets the registered amount
    /// </summary>
    public double RegAmount { get; set; }

    /// <summary>
    ///     Gets or sets the registered amount in project currency
    /// </summary>
    public double RegAmountProjectCurrency { get; set; }

    /// <summary>
    ///     Gets or sets the estimated hours
    /// </summary>
    public double EstimatedHours { get; set; }

    /// <summary>
    ///     Gets or sets the estimated hourly rate
    /// </summary>
    public double EstimatedHourlyRate { get; set; }

    /// <summary>
    ///     Gets or sets the estimated hourly rate in project currency
    /// </summary>
    public double EstimatedHourlyRateProjectCurrency { get; set; }

    /// <summary>
    ///     Gets or sets the estimated amount
    /// </summary>
    public double EstimatedAmount { get; set; }

    /// <summary>
    ///     Gets or sets the estimated amount in project currency
    /// </summary>
    public double EstimatedAmountProjectCurrency { get; set; }

    /// <summary>
    ///     Gets or sets the booked as revenue hours
    /// </summary>
    public double BarHours { get; set; }

    /// <summary>
    ///     Gets or sets the booked as revenue hourly rate
    /// </summary>
    public double BarHourlyRate { get; set; }

    /// <summary>
    ///     Gets or sets the booked as revenue hourly rate in project currency
    /// </summary>
    public double BarHourlyRateProjectCurrency { get; set; }

    /// <summary>
    ///     Gets or sets the booked as revenue amount
    /// </summary>
    public double BarAmount { get; set; }

    /// <summary>
    ///     Gets or sets the booked as revenue amount in project currency
    /// </summary>
    public double BarAmountProjectCurrency { get; set; }

    /// <summary>
    ///     Gets or sets the invoiced hours
    /// </summary>
    public double InvoicedHours { get; set; }

    /// <summary>
    ///     Gets or sets the invoiced hourly rate
    /// </summary>
    public double InvoicedHourlyRate { get; set; }

    /// <summary>
    ///     Gets or sets the invoiced hourly rate in project currency
    /// </summary>
    public double InvoicedHourlyRateProjectCurrency { get; set; }

    /// <summary>
    ///     Gets or sets the invoiced amount
    /// </summary>
    public double InvoicedAmount { get; set; }

    /// <summary>
    ///     Gets or sets the invoiced amount in project currency
    /// </summary>
    public double InvoicedAmountProjectCurrency { get; set; }

    /// <summary>
    ///     Gets or sets the invoiced hours (Obsolete)
    /// </summary>
    [Obsolete("Use InvoicedHours or BARHours")]
    public double InvHours { get; set; }

    /// <summary>
    ///     Gets or sets the invoiced amount
    /// </summary>
    [Obsolete("Use InvoicedAmount or BARAmount")]
    public double InvAmount { get; set; }

    /// <summary>
    ///     Gets or sets the cost amount
    /// </summary>
    public double CostAmount { get; set; }

    /// <summary>
    ///     Gets or sets the invoice status (0 = not booked or invoiced, 1 = added to voucher, 2 = added to invoice)
    /// </summary>
    public int InvoiceStatus { get; set; }

    /// <summary>
    ///     Gets or sets the billable status (-3 = non-billable registration on billable task with hourly rate of zero, -2 =
    ///     nillable registration on non-billable task, -1 = non-billable registration on non-billable task, 1 = billable
    ///     registration on billable task, 2 = non-billable registration on billable task)
    /// </summary>
    public int BillableStatus { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the registration is billable
    /// </summary>
    public bool IsBillable { get; set; }

    /// <summary>
    ///     Gets or sets the approval state (0 = time sheet open, 10 = time sheet closed, 20 = project manager approved, 30 =
    ///     department manager approved)
    /// </summary>
    public int ApprovedStatus { get; set; }

    /// <summary>
    ///     Gets or sets the monthly period
    /// </summary>
    public string MonthlyPeriod { get; set; }

    /// <summary>
    ///     Gets or sets the creation date
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    ///     Gets or sets the creation employee identifier
    /// </summary>
    public int CreatedByEmployeeID { get; set; }

    /// <summary>
    ///     Gets or sets the creation employee name
    /// </summary>
    public string CreatedBy { get; set; }

    /// <summary>
    ///     Gets or sets the last modified date
    /// </summary>
    public DateTime LastModifiedAt { get; set; }

    /// <summary>
    ///     Gets or sets the last modified employee identifier
    /// </summary>
    public int LastModifiedByEmployeeID { get; set; }

    /// <summary>
    ///     Gets or sets the last modified employee name
    /// </summary>
    public string LastModifiedBy { get; set; }

    /// <summary>
    ///     Gets or sets the user of the registration
    /// </summary>
    public int UserID { get; set; }
}