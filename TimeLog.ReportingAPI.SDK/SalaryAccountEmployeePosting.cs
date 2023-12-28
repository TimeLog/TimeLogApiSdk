using System;
using System.Xml;

namespace TimeLog.ReportingAPI.SDK;

/// <summary>
/// Placeholder for salary account employee posting
/// </summary>
public class SalaryAccountEmployeePosting
{
    // C# representation of the XML structure
    // <tlp:SalaryAccountEmployeePosting ID="1" GUID="E33B6EC7-4CBC-4A17-8409-CE923411F441">
    //    <tlp:EmployeeUserId>1</tlp:EmployeeUserId>
    //    <tlp:EmployeeUsername>psu</tlp:EmployeeUsername>
    //    <tlp:EmployeeNo></tlp:EmployeeNo>
    //    <tlp:SalaryAccountID>3</tlp:SalaryAccountID>
    //    <tlp:SalaryAccountName>Flex</tlp:SalaryAccountName>
    //    <tlp:Date>2019-06-13T00:00:00</tlp:Date>
    //    <tlp:EntityType>154</tlp:EntityType>
    //    <tlp:Hours>7.4000</tlp:Hours>
    //    <tlp:TotalHours>-7.4000</tlp:TotalHours>
    //    <tlp:Factor>-1.0000</tlp:Factor>
    //    <tlp:PostingType>0</tlp:PostingType>
    //    <tlp:Minutes>444</tlp:Minutes>
    //    <tlp:TotalMinutes>-444.0000</tlp:TotalMinutes>
    //    <tlp:Days>1.0000</tlp:Days>
    //    <tlp:TotalDays>-1.0000</tlp:TotalDays>
    //    <tlp:CommentToManager></tlp:CommentToManager>
    //    <tlp:CommentToEmployee></tlp:CommentToEmployee>
    //    <tlp:Created>2019-06-13T10:33:02.7696977</tlp:Created>
    //    <tlp:CreatedBy>375</tlp:CreatedBy>
    // </tlp:SalaryAccountEmployeePosting>
    
    /// <summary>
    ///     Initializes a new instance of the <see cref="SalaryAccountEmployeePosting" /> class.
    /// </summary>
    public SalaryAccountEmployeePosting()
    {
        Id = -1;
        Guid = Guid.Empty;
        EmployeeUserId = -1;
        EmployeeUsername = string.Empty;
        EmployeeNo = string.Empty;
        SalaryAccountId = -1;
        SalaryAccountName = string.Empty;
        Date = DateTime.MinValue;
        EntityType = 0;
        Hours = 0;
        TotalHours = 0;
        Factor = 0;
        PostingType = 0;
        Minutes = 0;
        TotalMinutes = 0;
        Days = 0;
        TotalDays = 0;
        CommentToManager = string.Empty;
        CommentToEmployee = string.Empty;
        Created = DateTime.MinValue;
        CreatedBy = -1;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="SalaryAccountEmployeePosting" /> class.
    /// </summary>
    /// <param name="node">The XML node to initialize from</param>
    /// <param name="namespaceManager">The namespace manager</param>
    public SalaryAccountEmployeePosting(XmlNode node, XmlNamespaceManager namespaceManager)
    {
        Id = int.Parse(node.Attributes?["ID"]?.InnerText ?? "-1");
        Guid = Guid.Parse(node.Attributes?["GUID"]?.InnerText ?? Guid.Empty.ToString());
        EmployeeUserId = node.GetIntSafe("tlp:EmployeeUserId", namespaceManager);
        EmployeeUsername = node.GetStringSafe("tlp:EmployeeUsername", namespaceManager);
        EmployeeNo = node.GetStringSafe("tlp:EmployeeNo", namespaceManager);
        SalaryAccountId = node.GetIntSafe("tlp:SalaryAccountID", namespaceManager);
        SalaryAccountName = node.GetStringSafe("tlp:SalaryAccountName", namespaceManager);
        Date = node.GetDateTimeSafe("tlp:Date", namespaceManager);
        EntityType = node.GetIntSafe("tlp:EntityType", namespaceManager);
        Hours = node.GetDecimalSafe("tlp:Hours", namespaceManager);
        TotalHours = node.GetDecimalSafe("tlp:TotalHours", namespaceManager);
        Factor = node.GetDecimalSafe("tlp:Factor", namespaceManager);
        PostingType = node.GetIntSafe("tlp:PostingType", namespaceManager);
        Minutes = node.GetDecimalSafe("tlp:Minutes", namespaceManager);
        TotalMinutes = node.GetDecimalSafe("tlp:TotalMinutes", namespaceManager);
        Days = node.GetDecimalSafe("tlp:Days", namespaceManager);
        TotalDays = node.GetDecimalSafe("tlp:TotalDays", namespaceManager);
        CommentToManager = node.GetStringSafe("tlp:CommentToManager", namespaceManager);
        CommentToEmployee = node.GetStringSafe("tlp:CommentToEmployee", namespaceManager);
        Created = node.GetDateTimeSafe("tlp:Created", namespaceManager);
        CreatedBy = node.GetIntSafe("tlp:CreatedBy", namespaceManager);
    }

    /// <summary>
    ///     Gets or sets the identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the GUID
    /// </summary>
    public Guid Guid { get; set; }
    
    /// <summary>
    ///     Gets or sets the employee user identifier
    /// </summary>
    public int EmployeeUserId { get; set; }
    
    /// <summary>
    ///     Gets or sets the employee username
    /// </summary>
    public string EmployeeUsername { get; set; }
    
    /// <summary>
    ///     Gets or sets the employee number
    /// </summary>
    public string EmployeeNo { get; set; }
    
    /// <summary>
    ///     Gets or sets the salary account identifier
    /// </summary>
    public int SalaryAccountId { get; set; }
    
    /// <summary>
    ///     Gets or sets the salary account name
    /// </summary>
    public string SalaryAccountName { get; set; }
    
    /// <summary>
    ///     Gets or sets the date
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    ///     Gets or sets the entity type
    /// </summary>
    public int EntityType { get; set; }
    
    /// <summary>
    ///     Gets or sets the hours
    /// </summary>
    public decimal Hours { get; set; }
    
    /// <summary>
    ///     Gets or sets the total hours
    /// </summary>
    public decimal TotalHours { get; set; }
    
    /// <summary>
    ///     Gets or sets the factor
    /// </summary>
    public decimal Factor { get; set; }
    
    /// <summary>
    ///     Gets or sets the posting type
    /// </summary>
    public int PostingType { get; set; }
    
    /// <summary>
    ///     Gets or sets the minutes
    /// </summary>
    public decimal Minutes { get; set; }
    
    /// <summary>
    ///     Gets or sets the total minutes
    /// </summary>
    public decimal TotalMinutes { get; set; }
    
    /// <summary>
    ///     Gets or sets the days
    /// </summary>
    public decimal Days { get; set; }
    
    /// <summary>
    ///     Gets or sets the total days
    /// </summary>
    public decimal TotalDays { get; set; }
    
    /// <summary>
    ///     Gets or sets the comment to manager
    /// </summary>
    public string CommentToManager { get; set; }
    
    /// <summary>
    ///     Gets or sets the comment to employee
    /// </summary>
    public string CommentToEmployee { get; set; }
    
    /// <summary>
    ///     Gets or sets the created
    /// </summary>
    public DateTime Created { get; set; }
    
    /// <summary>
    ///     Gets or sets the created by
    /// </summary>
    public int CreatedBy { get; set; }
}