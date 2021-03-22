using System;
using System.Xml;

namespace TimeLog.ReportingApi.Core.SDK
{
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
            this.Address = string.Empty;
            this.City = string.Empty;
            this.CostPrice = 0;
            this.DepartmentName = string.Empty;
            this.DepartmentNameID = 0;
            this.Email = string.Empty;
            this.EmployeeNo = string.Empty;
            this.EmployeeTypeId = -1;
            this.EmployeeTypeName = string.Empty;
            this.EmployeeUserID = -1;
            this.FirstName = string.Empty;
            this.FullName = string.Empty;
            this.HiredDate = DateTime.Now;
            this.Id = -1;
            this.Initials = string.Empty;
            this.LastName = string.Empty;
            this.Mobile = string.Empty;
            this.Phone = string.Empty;
            this.PrivatePhone = string.Empty;
            this.Status = 0;
            this.TerminatedDate = DateTime.Now;
            this.Title = string.Empty;
            this.Username = string.Empty;
            this.WorkWeek = string.Empty;
            this.ZipCode = string.Empty;
        }

        public Employee(XmlNode node, XmlNamespaceManager namespaceManager)
        {
            this.Address = node.GetStringSafe("tlp:Address", namespaceManager);            
            this.City = node.GetStringSafe("tlp:City", namespaceManager);            
            this.CostPrice = node.GetFloatSafe("tlp:CostPrice", namespaceManager);            
            this.DepartmentName = node.GetStringSafe("tlp:DepartmentName", namespaceManager);            
            this.DepartmentNameID = node.GetIntSafe("tlp:DepartmentNameID", namespaceManager);            
            this.Email = node.GetStringSafe("tlp:Email", namespaceManager);            
            this.EmployeeNo = node.GetStringSafe("tlp:EmployeeNo", namespaceManager);            
            this.EmployeeTypeId = node.GetIntSafe("tlp:EmployeeTypeId", namespaceManager);            
            this.EmployeeTypeName = node.GetStringSafe("tlp:EmployeeTypeName", namespaceManager);            
            this.EmployeeUserID = node.GetIntSafe("tlp:EmployeeUserID", namespaceManager);            
            this.FirstName = node.GetStringSafe("tlp:FirstName", namespaceManager);            
            this.FullName = node.GetStringSafe("tlp:FullName", namespaceManager);            
            this.HiredDate = node.GetDateTimeSafe("tlp:HiredDate", namespaceManager);            
            this.Id = int.Parse(node.Attributes["ID"].InnerText);
            this.Initials = node.GetStringSafe("tlp:Initials", namespaceManager);
            this.LastName = node.GetStringSafe("tlp:LastName", namespaceManager);
            this.Mobile = node.GetStringSafe("tlp:Mobile", namespaceManager);
            this.Phone = node.GetStringSafe("tlp:Phone", namespaceManager);
            this.PrivatePhone = node.GetStringSafe("tlp:PrivatePhone", namespaceManager);
            this.Status = node.GetIntSafe("tlp:Status", namespaceManager);
            this.TerminatedDate = node.GetDateTimeSafe("tlp:TerminatedDate", namespaceManager);
            this.Title = node.GetStringSafe("tlp:Title", namespaceManager);
            this.Username = node.GetStringSafe("tlp:Username", namespaceManager);            
            this.WorkWeek = node.GetStringSafe("tlp:WorkWeek", namespaceManager);            
            this.ZipCode = node.GetStringSafe("tlp:ZipCode", namespaceManager);            
        }

        public static int All
        {
            get
            {
                return 0;
            }
        }


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
}
