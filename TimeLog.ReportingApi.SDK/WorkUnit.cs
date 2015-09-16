namespace TimeLog.ReportingApi.SDK
{
    using System;
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
            this.AdditionalTextField = string.Empty;
            this.AllocationID = -1;
            this.ApprovedStatus = 0;
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
            this.EstimatedHours = 0;
            this.Id = -1;
            this.InvAmount = 0;
            this.InvHours = 0;
            this.IsBillable = false;
            this.LastModifiedAt = DateTime.Now;
            this.LastModifiedBy = string.Empty;
            this.LastModifiedByEmployeeID = -1;
            this.Note = string.Empty;
            this.ProjectID = -1;
            this.ProjectName = string.Empty;
            this.RegAmount = 0;
            this.RegHours = 0;
            this.TaskID = -1;
            this.TaskName = string.Empty;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WorkUnit" /> class.
        /// </summary>
        /// <param name="node">The XML node to initialize from</param>
        /// <param name="namespaceManager">The namespace manager</param>
        public WorkUnit(XmlNode node, XmlNamespaceManager namespaceManager)
        {
            this.AdditionalTextField = node.GetStringSafe("tlp:AdditionalTextField", namespaceManager);
            this.AllocationID = node.GetIntSafe("tlp:AllocationID", namespaceManager);
            this.ApprovedStatus = node.GetIntSafe("tlp:ApprovedStatus", namespaceManager);
            this.Created = node.GetDateTimeSafe("tlp:Created", namespaceManager);
            this.CreatedBy = node.GetStringSafe("tlp:CreatedBy", namespaceManager);
            this.CreatedByEmployeeID = node.GetIntSafe("tlp:CreatedByEmployeeID", namespaceManager);
            this.CustomerID = node.GetIntSafe("tlp:CustomerID", namespaceManager);
            this.CustomerName = node.GetStringSafe("tlp:CustomerName", namespaceManager);
            this.Date = node.GetDateTimeSafe("tlp:Date", namespaceManager);
            this.DepartmentID = node.GetIntSafe("tlp:DepartmentID", namespaceManager);
            this.DepartmentName = node.GetStringSafe("tlp:DepartmentName", namespaceManager);
            this.EmployeeFirstName = node.GetStringSafe("tlp:EmployeeFirstName", namespaceManager);
            this.EmployeeID = node.GetIntSafe("tlp:EmployeeID", namespaceManager);
            this.EmployeeInitials = node.GetStringSafe("tlp:EmployeeInitials", namespaceManager);
            this.EmployeeLastName = node.GetStringSafe("tlp:EmployeeLastName", namespaceManager);
            this.EstimatedAmount = node.GetDoubleSafe("tlp:EstimatedAmount", namespaceManager);
            this.EstimatedHours = node.GetDoubleSafe("tlp:EstimatedHours", namespaceManager);
            this.Id = int.Parse(node.Attributes["ID"].InnerText);
            this.InvAmount = node.GetDoubleSafe("tlp:InvAmount", namespaceManager);
            this.InvHours = node.GetDoubleSafe("tlp:InvHours", namespaceManager);
            this.IsBillable = node.GetBoolTimeSafe("tlp:IsBillable", namespaceManager);
            this.LastModifiedAt = node.GetDateTimeSafe("tlp:LastModifiedAt", namespaceManager);
            this.LastModifiedBy = node.GetStringSafe("tlp:LastModifiedBy", namespaceManager);
            this.LastModifiedByEmployeeID = node.GetIntSafe("tlp:LastModifiedByEmployeeID", namespaceManager);
            this.Note = node.GetStringSafe("tlp:Note", namespaceManager);
            this.ProjectID = node.GetIntSafe("tlp:ProjectID", namespaceManager);
            this.ProjectName = node.GetStringSafe("tlp:ProjectName", namespaceManager);
            this.RegAmount = node.GetDoubleSafe("tlp:RegAmount", namespaceManager);
            this.RegHours = node.GetDoubleSafe("tlp:RegHours", namespaceManager);
            this.TaskID = node.GetIntSafe("tlp:TaskID", namespaceManager);
            this.TaskName = node.GetStringSafe("tlp:TaskName", namespaceManager);
        }

        /// <summary>
        ///     Gets the default parameter value for filtering for all work units
        /// </summary>
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
        /// Gets or sets the registered hours
        /// </summary>
        public double RegHours { get; set; }

        /// <summary>
        /// Gets or sets the registered amount
        /// </summary>
        public double RegAmount { get; set; }

        /// <summary>
        /// Gets or sets the estimated hours
        /// </summary>
        public double EstimatedHours { get; set; }

        /// <summary>
        /// Gets or sets the estimated amount
        /// </summary>
        public double EstimatedAmount { get; set; }

        /// <summary>
        /// Gets or sets the invoiced hours
        /// </summary>
        public double InvHours { get; set; }

        /// <summary>
        /// Gets or sets the invoiced amount
        /// </summary>
        public double InvAmount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the registration is billable
        /// </summary>
        public bool IsBillable { get; set; }

        /// <summary>
        /// Gets or sets the approval state (0 = time sheet open, 10 = time sheet closed, 20 = project manager approved, 30 = department manager approved)
        /// </summary>
        public int ApprovedStatus { get; set; }

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
    }
}