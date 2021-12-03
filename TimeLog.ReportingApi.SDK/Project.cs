using System;
using System.Xml;
using System.Xml.Serialization;

namespace TimeLog.ReportingAPI.SDK;

/// <summary>
///     Placeholder for project related constants and properties
/// </summary>
public class Project
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Customer" /> class.
    /// </summary>
    public Project()
    {
        Id = 0;
        ProjectGuid = Guid.Empty;
        Name = string.Empty;
        Status = -1;
        OrderedByDepartment = -1;
        ProjectStartDate = DateTime.MinValue;
        ProjectEndDate = DateTime.MinValue;
        Link = string.Empty;
        Description = string.Empty;
        CustomerId = 0;
        CustomerName = string.Empty;
        CustomerNo = string.Empty;
        PMId = -1;
        PMInitials = string.Empty;
        PMFullname = string.Empty;
        ProjectTypeId = -1;
        ProjectTypeName = string.Empty;
        ProjectCategoryId = -1;
        ProjectCategoryName = string.Empty;
        BudgetAmountExpenses = 0;
        BudgetAmountTravel = 0;
        BudgetAmount = 0;
        BudgetHours = 0;
        CreatedAt = DateTime.MinValue;
        CreatedByEmployeeId = 0;
        CreatedBy = string.Empty;
        LastModifiedAt = DateTime.MinValue;
        LastModifiedByEmployeeId = 0;
        LastModifiedBy = string.Empty;
        ProjectStatusName = string.Empty;
        ContactFullName = string.Empty;
        ContactEmail = string.Empty;
        ProjectNo = string.Empty;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Customer" /> class.
    /// </summary>
    /// <param name="node">The XML node to initialize from</param>
    /// <param name="namespaceManager">The namespace manager</param>
    public Project(XmlNode node, XmlNamespaceManager namespaceManager)
    {
        Id = int.Parse(node.Attributes["ID"].InnerText);
        ProjectGuid = Guid.Parse(node.Attributes["GUID"].InnerText);
        Name = node.GetStringSafe("tlp:Name", namespaceManager);
        Status = node.GetIntSafe("tlp:Status", namespaceManager);
        OrderedByDepartment = node.GetIntSafe("tlp:OrderedByDepartment", namespaceManager);
        ProjectStartDate = node.GetDateTimeSafe("tlp:ProjectStartDate", namespaceManager);
        ProjectEndDate = node.GetDateTimeSafe("tlp:ProjectEndDate", namespaceManager);
        Link = node.GetStringSafe("tlp:Link", namespaceManager);
        Description = node.GetStringSafe("tlp:Description", namespaceManager);
        CustomerId = node.GetIntSafe("tlp:CustomerID", namespaceManager);
        CustomerName = node.GetStringSafe("tlp:CustomerName", namespaceManager);
        CustomerNo = node.GetStringSafe("tlp:CustomerNo", namespaceManager);
        PMId = node.GetIntSafe("tlp:PMID", namespaceManager);
        PMInitials = node.GetStringSafe("tlp:PMInitials", namespaceManager);
        PMFullname = node.GetStringSafe("tlp:PMFullName", namespaceManager);
        ProjectTypeId = node.GetIntSafe("tlp:ProjectTypeID", namespaceManager);
        ProjectTypeName = node.GetStringSafe("tlp:ProjectTypeName", namespaceManager);
        ProjectCategoryId = node.GetIntSafe("tlp:ProjectCategoryID", namespaceManager);
        ProjectCategoryName = node.GetStringSafe("tlp:ProjectCategoryName", namespaceManager);
        BudgetAmountExpenses = node.GetIntSafe("tlp:BudgetAmountExpenses", namespaceManager);
        BudgetAmountTravel = node.GetIntSafe("tlp:BudgetAmountTravel", namespaceManager);
        BudgetAmount = node.GetIntSafe("tlp:BudgetAmount", namespaceManager);
        BudgetHours = node.GetIntSafe("tlp:BudgetHours", namespaceManager);
        CreatedAt = node.GetDateTimeSafe("tlp:CreatedAt", namespaceManager);
        CreatedByEmployeeId = node.GetIntSafe("tlp:CreatedByEmployeeId", namespaceManager);
        CreatedBy = node.GetStringSafe("tlp:CreatedBy", namespaceManager);
        LastModifiedAt = node.GetDateTimeSafe("tlp:LastModifiedAt", namespaceManager);
        LastModifiedByEmployeeId = node.GetIntSafe("tlp:LastModifiedByEmployeeId", namespaceManager);
        LastModifiedBy = node.GetStringSafe("tlp:LastModifiedBy", namespaceManager);
        ProjectStatusName = node.GetStringSafe("tlp:ProjectStatusName", namespaceManager);
        ContactFullName = node.GetStringSafe("tlp:ContactFullName", namespaceManager);
        ContactEmail = node.GetStringSafe("tlp:ContactEmail", namespaceManager);
        ProjectNo = node.GetStringSafe("tlp:No", namespaceManager);
    }


    /// <summary>
    ///     Gets the default parameter value for filtering for all projects
    /// </summary>
    [XmlIgnore]
    public static int All => 0;

    /// <summary>
    ///     Gets or sets the project identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the project unique identifier
    /// </summary>
    public Guid ProjectGuid { get; set; }

    /// <summary>
    ///     Gets or set the name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or set the status of the project. 1 = Active, 0 = Inactive
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    ///     Gets or sets the identifier of the department ordering the project
    /// </summary>
    public int OrderedByDepartment { get; set; }

    /// <summary>
    ///     Gets or sets the project start date
    /// </summary>
    public DateTime ProjectStartDate { get; set; }

    /// <summary>
    ///     Gets or sets the project end date
    /// </summary>
    public DateTime ProjectEndDate { get; set; }

    /// <summary>
    ///     Gets or sets the link
    /// </summary>
    public string Link { get; set; }

    /// <summary>
    ///     Gets or sets the description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Gets or sets the customer identifier
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    ///     Gets or sets the customer name
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    ///     Gets or sets the customer number
    /// </summary>
    public string CustomerNo { get; set; }

    /// <summary>
    ///     Gets or sets the project manager identifier
    /// </summary>
    public int PMId { get; set; }

    /// <summary>
    ///     Gets or sets the project manager initials
    /// </summary>
    public string PMInitials { get; set; }

    /// <summary>
    ///     Gets or sets the project manager name
    /// </summary>
    public string PMFullname { get; set; }

    /// <summary>
    ///     Gets or sets the project type identifier
    /// </summary>
    public int ProjectTypeId { get; set; }

    /// <summary>
    ///     Gets or sets the project type name
    /// </summary>
    public string ProjectTypeName { get; set; }

    /// <summary>
    ///     Gets or sets the project category identifier
    /// </summary>
    public int ProjectCategoryId { get; set; }

    /// <summary>
    ///     Gets or sets the project category name
    /// </summary>
    public string ProjectCategoryName { get; set; }

    /// <summary>
    ///     Gets or sets the budgeted amount of expenses
    /// </summary>
    public float BudgetAmountExpenses { get; set; }

    /// <summary>
    ///     Gets or sets the budgeted amount of travel
    /// </summary>
    public float BudgetAmountTravel { get; set; }

    /// <summary>
    ///     Gets or sets the budgeted amount in currency
    /// </summary>
    public float BudgetAmount { get; set; }

    /// <summary>
    ///     Gets or sets the budgeted amount in hours
    /// </summary>
    public float BudgetHours { get; set; }

    /// <summary>
    ///     Gets or sets the created date
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     Gets or sets the identifier of the creator
    /// </summary>
    public int CreatedByEmployeeId { get; set; }

    /// <summary>
    ///     Gets or sets the name of the creator
    /// </summary>
    public string CreatedBy { get; set; }

    /// <summary>
    ///     Gets or sets the last modified date
    /// </summary>
    public DateTime LastModifiedAt { get; set; }

    /// <summary>
    ///     Gets or sets the identifier of the last modifier
    /// </summary>
    public int LastModifiedByEmployeeId { get; set; }

    /// <summary>
    ///     Gets or sets the name of the last modifier
    /// </summary>
    public string LastModifiedBy { get; set; }

    /// <summary>
    ///     Gets or sets the project order status name
    /// </summary>
    public string ProjectStatusName { get; set; }

    /// <summary>
    ///     Gets or sets the full name of the customer contact
    /// </summary>
    public string ContactFullName { get; set; }

    /// <summary>
    ///     Gets or sets the email of the customer contact
    /// </summary>
    public string ContactEmail { get; set; }

    /// <summary>
    ///     Gets or sets the related project number
    /// </summary>
    public string ProjectNo { get; set; }
}