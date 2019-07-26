using System;
using System.Xml;

namespace TimeLog.ReportingApi.SDK
{
    public class Employee
    {
        public Employee()
        {
            this.Id = -1;
            this.Initials = string.Empty;           
        }





        public Employee(XmlNode node, XmlNamespaceManager namespaceManager)
        {
            this.Id = int.Parse(node.Attributes["ID"].InnerText);
            //this.Guid = Guid.Parse(node.Attributes["GUID"].InnerText);
            this.Initials = node.GetStringSafe("tlp:Initials", namespaceManager);            
            this.FirstName = node.GetStringSafe("tlp:FirstName", namespaceManager);            
            this.LastName = node.GetStringSafe("tlp:LastName", namespaceManager);            
            this.FullName = node.GetStringSafe("tlp:FullName", namespaceManager);            
        }

        
        public static int All
        {
            get
            {
                return 0;
            }
        }


        public int Id { get; set; }
        //public Guid Guid { get; set; }

        public string Initials { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }


    }
}
