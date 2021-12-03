using System;
using System.Xml;
using System.Xml.Serialization;

namespace TimeLog.ReportingAPI.SDK;

/// <summary>
///     Placeholder for time off registrations related constants
/// </summary>
public class TimeOffRegistration
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TimeOffRegistration" /> class.
    /// </summary>
    public TimeOffRegistration()
    {
        CreatedAt = DateTime.Now;
        CreatedBy = string.Empty;
        CreatedByEmployeeID = -1;
        Date = DateTime.Now;
        EmployeeFirstName = string.Empty;
        EmployeeID = -1;
        EmployeeInitials = string.Empty;
        EmployeeLastName = string.Empty;
        Id = -1;
        LastModifiedAt = DateTime.Now;
        LastModifiedBy = string.Empty;
        LastModifiedByEmployeeID = -1;
        RegHours = 0;
        TimeOffCode = 0;
        TimeOffName = string.Empty;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="TimeOffRegistration" /> class.
    /// </summary>
    /// <param name="node">The XML node to initialize from</param>
    /// <param name="namespaceManager">The namespace manager</param>
    public TimeOffRegistration(XmlNode node, XmlNamespaceManager namespaceManager)
    {
        CreatedAt = node.GetDateTimeSafe("tlp:CreatedAt", namespaceManager);
        CreatedBy = node.GetStringSafe("tlp:CreatedBy", namespaceManager);
        CreatedByEmployeeID = node.GetIntSafe("tlp:CreatedByEmployeeID", namespaceManager);
        Date = node.GetDateTimeSafe("tlp:Date", namespaceManager);
        EmployeeFirstName = node.GetStringSafe("tlp:EmployeeFirstName", namespaceManager);
        EmployeeID = node.GetIntSafe("tlp:EmployeeID", namespaceManager);
        EmployeeInitials = node.GetStringSafe("tlp:EmployeeInitials", namespaceManager);
        EmployeeLastName = node.GetStringSafe("tlp:EmployeeLastName", namespaceManager);
        Id = int.Parse(node.Attributes["ID"].InnerText);
        LastModifiedAt = node.GetDateTimeSafe("tlp:LastModifiedAt", namespaceManager);
        LastModifiedBy = node.GetStringSafe("tlp:LastModifiedBy", namespaceManager);
        LastModifiedByEmployeeID = node.GetIntSafe("tlp:LastModifiedByEmployeeID", namespaceManager);
        RegHours = node.GetDoubleSafe("tlp:RegHours", namespaceManager, ServiceHandler.DataCulture);
        TimeOffCode = node.GetIntSafe("tlp:TimeOffCode", namespaceManager);
        TimeOffName = node.GetStringSafe("tlp:TimeOffName", namespaceManager);
    }

    /// <summary>
    ///     Gets the default parameter value for filtering for all time off registrations
    /// </summary>
    [XmlIgnore]
    public static int All => 0;

    /// <summary>
    ///     Gets or sets the identifier
    /// </summary>
    public int Id { get; set; }

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
    ///     Gets or sets the salary account code
    /// </summary>
    public int TimeOffCode { get; set; }

    /// <summary>
    ///     Gets or sets the salary account name
    /// </summary>
    public string TimeOffName { get; set; }

    /// <summary>
    ///     Gets or sets the registration date
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    ///     Gets or sets the registered hours
    /// </summary>
    public double RegHours { get; set; }

    /// <summary>
    ///     Gets or sets the creation date
    /// </summary>
    public DateTime CreatedAt { get; set; }

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
}