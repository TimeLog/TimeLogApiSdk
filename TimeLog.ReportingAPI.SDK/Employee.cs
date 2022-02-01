using System;
using System.Xml;

namespace TimeLog.ReportingAPI.SDK;

public enum EmployeeStatus
{
    Inactive = 0,
    Active = 1,
    All = -1
}

public class Employee
{
    public Employee()
    {
        Address = string.Empty;
        City = string.Empty;
        CostPrice = 0;
        DepartmentName = string.Empty;
        DepartmentNameID = 0;
        Email = string.Empty;
        EmployeeNo = string.Empty;
        EmployeeTypeId = -1;
        EmployeeTypeName = string.Empty;
        EmployeeUserID = -1;
        FirstName = string.Empty;
        FullName = string.Empty;
        HiredDate = DateTime.Now;
        Id = -1;
        Initials = string.Empty;
        LastName = string.Empty;
        Mobile = string.Empty;
        Phone = string.Empty;
        PrivatePhone = string.Empty;
        Status = 0;
        TerminatedDate = DateTime.Now;
        Title = string.Empty;
        Username = string.Empty;
        WorkWeek = string.Empty;
        ZipCode = string.Empty;
    }

    public Employee(XmlNode node, XmlNamespaceManager namespaceManager)
    {
        Address = node.GetStringSafe("tlp:Address", namespaceManager);
        City = node.GetStringSafe("tlp:City", namespaceManager);
        CostPrice = node.GetFloatSafe("tlp:CostPrice", namespaceManager);
        DepartmentName = node.GetStringSafe("tlp:DepartmentName", namespaceManager);
        DepartmentNameID = node.GetIntSafe("tlp:DepartmentNameID", namespaceManager);
        Email = node.GetStringSafe("tlp:Email", namespaceManager);
        EmployeeNo = node.GetStringSafe("tlp:EmployeeNo", namespaceManager);
        EmployeeTypeId = node.GetIntSafe("tlp:EmployeeTypeId", namespaceManager);
        EmployeeTypeName = node.GetStringSafe("tlp:EmployeeTypeName", namespaceManager);
        EmployeeUserID = node.GetIntSafe("tlp:EmployeeUserID", namespaceManager);
        FirstName = node.GetStringSafe("tlp:FirstName", namespaceManager);
        FullName = node.GetStringSafe("tlp:FullName", namespaceManager);
        HiredDate = node.GetDateTimeSafe("tlp:HiredDate", namespaceManager);
        Id = int.Parse(node.Attributes["ID"].InnerText);
        Initials = node.GetStringSafe("tlp:Initials", namespaceManager);
        LastName = node.GetStringSafe("tlp:LastName", namespaceManager);
        Mobile = node.GetStringSafe("tlp:Mobile", namespaceManager);
        Phone = node.GetStringSafe("tlp:Phone", namespaceManager);
        PrivatePhone = node.GetStringSafe("tlp:PrivatePhone", namespaceManager);
        Status = node.GetIntSafe("tlp:Status", namespaceManager);
        TerminatedDate = node.GetDateTimeSafe("tlp:TerminatedDate", namespaceManager);
        Title = node.GetStringSafe("tlp:Title", namespaceManager);
        Username = node.GetStringSafe("tlp:Username", namespaceManager);
        WorkWeek = node.GetStringSafe("tlp:WorkWeek", namespaceManager);
        ZipCode = node.GetStringSafe("tlp:ZipCode", namespaceManager);
    }

    public static int All => 0;


    public int Id { get; set; }

    public int EmployeeUserID { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName { get; set; }

    public string Initials { get; set; }

    public string Title { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Mobile { get; set; }

    public string PrivatePhone { get; set; }

    public string Address { get; set; }

    public string ZipCode { get; set; }

    public string City { get; set; }

    public int Status { get; set; }

    public DateTime HiredDate { get; set; }

    public DateTime TerminatedDate { get; set; }

    public int DepartmentNameID { get; set; }

    public string DepartmentName { get; set; }

    public string WorkWeek { get; set; }

    public int EmployeeTypeId { get; set; }

    public string EmployeeTypeName { get; set; }

    public string EmployeeNo { get; set; }

    public string Username { get; set; }

    public float CostPrice { get; set; }
}