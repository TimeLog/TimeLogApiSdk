using System;
using System.Xml;
using System.Xml.Serialization;

namespace TimeLog.ReportingApi.SDK
{
    /// <summary>
    /// Placeholder for project related constants and properties
    /// </summary>
    public class Project
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        public Project()
        {
            this.Id = 0;
            this.ProjectGuid = Guid.Empty;
            this.Name = String.Empty;
            this.Status = -1;
            this.OrderedByDepartment = -1;
            this.ProjectStartDate = DateTime.MinValue;
            this.ProjectEndDate = DateTime.MinValue;
            this.Link = string.Empty;
            this.Description = String.Empty;
            this.CustomerId = 0;
            this.CustomerName = string.Empty;
            this.CustomerNo = String.Empty;
            this.PMId = -1;
            this.PMInitials = string.Empty;
            this.PMFullname = String.Empty;
            this.ProjectTypeId = -1;
            this.ProjectTypeName = string.Empty;
            this.ProjectCategoryId = -1;
            this.ProjectCategoryName = string.Empty;
            this.BudgetAmountExpenses = 0;
            this.BudgetAmountTravel = 0;
            this.BudgetAmount = 0;
            this.BudgetHours = 0;
            this.CreatedAt = DateTime.MinValue;
            this.CreatedByEmployeeId = 0;
            this.CreatedBy = string.Empty;
            this.LastModifiedAt = DateTime.MinValue;
            this.LastModifiedByEmployeeId = 0;
            this.LastModifiedBy = string.Empty;
            this.ProjectStatusName = string.Empty;
            this.ContactFullName = string.Empty;
            this.ContactEmail = string.Empty;
            this.No = String.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="node">The XML node to initialize from</param>
        /// <param name="namespaceManager">The namespace manager</param>
        public Project(XmlNode node, XmlNamespaceManager namespaceManager)
        {
            this.Id = int.Parse(node.Attributes["ID"].InnerText);
            this.ProjectGuid = Guid.Parse(node.Attributes["GUID"].InnerText);
            this.Name = node.GetStringSafe("tlp:Name", namespaceManager);
            this.Status = node.GetIntSafe("tlp:Status", namespaceManager);
            this.OrderedByDepartment = node.GetIntSafe("tlp:OrderedByDepartment", namespaceManager);
            this.ProjectStartDate = node.GetDateTimeSafe("tlp:ProjectStartDate", namespaceManager);
            this.ProjectEndDate = node.GetDateTimeSafe("tlp:ProjectEndDate", namespaceManager);
            this.Link = node.GetStringSafe("tlp:Link", namespaceManager);
            this.Description = node.GetStringSafe("tlp:Description", namespaceManager);
            this.CustomerId = node.GetIntSafe("tlp:CustomerID", namespaceManager);
            this.CustomerName = node.GetStringSafe("tlp:CustomerName", namespaceManager);
            this.CustomerNo = node.GetStringSafe("tlp:CustomerNo", namespaceManager);
            this.PMId = node.GetIntSafe("tlp:PMID", namespaceManager);
            this.PMInitials = node.GetStringSafe("tlp:PMInitials", namespaceManager);
            this.PMFullname = node.GetStringSafe("tlp:PMFullName", namespaceManager);
            this.ProjectTypeId = node.GetIntSafe("tlp:ProjectTypeID", namespaceManager);
            this.ProjectTypeName = node.GetStringSafe("tlp:ProjectTypeName", namespaceManager);
            this.ProjectCategoryId = node.GetIntSafe("tlp:ProjectCategoryID", namespaceManager);
            this.ProjectCategoryName = node.GetStringSafe("tlp:ProjectCategoryName", namespaceManager);
            this.BudgetAmountExpenses = node.GetIntSafe("tlp:BudgetAmountExpenses", namespaceManager);
            this.BudgetAmountTravel = node.GetIntSafe("tlp:BudgetAmountTravel", namespaceManager);
            this.BudgetAmount = node.GetIntSafe("tlp:BudgetAmount", namespaceManager);
            this.BudgetHours = node.GetIntSafe("tlp:BudgetHours", namespaceManager);
            this.CreatedAt = node.GetDateTimeSafe("tlp:CreatedAt", namespaceManager);
            this.CreatedByEmployeeId = node.GetIntSafe("tlp:CreatedByEmployeeId", namespaceManager);
            this.CreatedBy = node.GetStringSafe("tlp:CreatedBy", namespaceManager);
            this.LastModifiedAt = node.GetDateTimeSafe("tlp:LastModifiedAt", namespaceManager);
            this.LastModifiedByEmployeeId = node.GetIntSafe("tlp:LastModifiedByEmployeeId", namespaceManager);
            this.LastModifiedBy = node.GetStringSafe("tlp:LastModifiedBy", namespaceManager);
            this.ProjectStatusName = node.GetStringSafe("tlp:ProjectStatusName", namespaceManager);
            this.ContactFullName = node.GetStringSafe("tlp:ContactFullName", namespaceManager);
            this.ContactEmail = node.GetStringSafe("tlp:ContactEmail", namespaceManager);
            this.No = node.GetStringSafe("tlp:No", namespaceManager);
        }


        /// <summary>
        /// Gets the default parameter value for filtering for all projects
        /// </summary>
        [XmlIgnore]
        public static int All => 0;

        /// <summary>
        /// Gets or sets the project identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the project unique identifier
        /// </summary>
        public Guid ProjectGuid { get; set; }

        /// <summary>
        /// Gets or set the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or set the status of the project. 1 = Active, 0 = Inactive
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the department ordering the project
        /// </summary>
        public int OrderedByDepartment { get; set; }

        /// <summary>
        /// Gets or sets the project start date
        /// </summary>
        public DateTime ProjectStartDate { get; set; }

        /// <summary>
        /// Gets or sets the project end date
        /// </summary>
        public DateTime ProjectEndDate { get; set; }

        /// <summary>
        /// Gets or sets the link
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the customer number
        /// </summary>
        public string CustomerNo { get; set; }

        /// <summary>
        /// Gets or sets the project manager identifier
        /// </summary>
        public int PMId { get; set; }

        /// <summary>
        /// Gets or sets the project manager initials
        /// </summary>
        public string PMInitials { get; set; }

        /// <summary>
        /// Gets or sets the project manager name
        /// </summary>
        public string PMFullname { get; set; }

        /// <summary>
        /// Gets or sets the project type identifier
        /// </summary>
        public int ProjectTypeId { get; set; }

        /// <summary>
        /// Gets or sets the project type name
        /// </summary>
        public string ProjectTypeName { get; set; }

        /// <summary>
        /// Gets or sets the project category identifier
        /// </summary>
        public int ProjectCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the project category name
        /// </summary>
        public string ProjectCategoryName { get; set; }

        /// <summary>
        /// Gets or sets the budgeted amount of expenses
        /// </summary>
        public float BudgetAmountExpenses { get; set; }

        /// <summary>
        /// Gets or sets the budgeted amount of travel
        /// </summary>
        public float BudgetAmountTravel { get; set; }

        /// <summary>
        /// Gets or sets the budgeted amount in currency
        /// </summary>
        public float BudgetAmount { get; set; }

        /// <summary>
        /// Gets or sets the budgeted amount in hours
        /// </summary>
        public float BudgetHours { get; set; }

        /// <summary>
        /// Gets or sets the created date
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the creator
        /// </summary>
        public int CreatedByEmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the creator
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the last modified date
        /// </summary>
        public DateTime LastModifiedAt { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the last modifier
        /// </summary>
        public int LastModifiedByEmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the last modifier
        /// </summary>
        public string LastModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the project order status name
        /// </summary>
        public string ProjectStatusName { get; set; }

        /// <summary>
        /// Gets or sets the full name of the customer contact
        /// </summary>
        public string ContactFullName { get; set; }

        /// <summary>
        /// Gets or sets the email of the customer contact
        /// </summary>
        public string ContactEmail { get; set; }
        
        /// <summary>
        /// Gets or sets the related project number
        /// </summary>
        public string No { get; set; }
    }
}
