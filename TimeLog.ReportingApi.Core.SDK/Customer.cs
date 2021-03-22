using System;
using System.Xml;
using System.Xml.Serialization;

namespace TimeLog.ReportingApi.Core.SDK
{
    /// <summary>
    /// Placeholder for customer related constants and properties
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets the default parameter value for filtering for all customers
        /// </summary>
        [XmlIgnore]
        public static int All => 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        public Customer()
        {
            this.AccountManagerFullName = string.Empty;
            this.AccountManagerID = -1;
            this.Address1 = string.Empty;
            this.Address2 = string.Empty;
            this.Address3 = string.Empty;
            this.City = string.Empty;
            this.Country = string.Empty;
            this.CountryId = -1;
            this.Comment = string.Empty;
            this.CustomerStatus = string.Empty;
            this.CustomerStatusID = -1;
            this.Email = string.Empty;
            this.Fax = string.Empty;
            this.Id = -1;
            this.IndustryID = -1;
            this.IndustryName = string.Empty;
            this.Name = string.Empty;
            this.NickName = string.Empty;
            this.No = string.Empty;
            this.Phone = string.Empty;
            this.State = string.Empty;
            this.VATNo = string.Empty;
            this.WebPage = string.Empty;
            this.ZipCode = string.Empty;
            this.CustomerGUID = Guid.Empty;
            this.EanNo = string.Empty;
            this.InvoiceTemplateID = -1;
            this.AccountManagerInitials = string.Empty;
            this.SecondaryAccountManagerID = -1;
            this.SecondaryAccountManagerFullName = string.Empty;
            this.SecondaryAccountManagerInitials = string.Empty;
            this.CustomerSince = DateTime.MinValue;
            this.RegistrationNumber = string.Empty;
            this.CreatedAt = DateTime.MinValue;
            this.CreatedByEmployeeID = -1;
            this.CreatedByEmployeeInitials = string.Empty;
            this.LastModifiedAt = DateTime.MinValue;
            this.LastModifiedByEmployeeID = -1;
            this.LastModifiedByEmployeeInitials = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="node">The XML node to initialize from</param>
        /// <param name="namespaceManager">The namespace manager</param>
        public Customer(XmlNode node, XmlNamespaceManager namespaceManager)
        {
            this.AccountManagerFullName = node.GetStringSafe("tlp:AccountManagerFullName", namespaceManager);
            this.AccountManagerID = node.GetIntSafe("tlp:AccountManagerID", namespaceManager);
            this.Address1 = node.GetStringSafe("tlp:Address1", namespaceManager);
            this.Address2 = node.GetStringSafe("tlp:Address2", namespaceManager);
            this.Address3 = node.GetStringSafe("tlp:Address3", namespaceManager);
            this.City = node.GetStringSafe("tlp:City", namespaceManager);
            this.Comment = node.GetStringSafe("tlp:Comment", namespaceManager);
            this.Country = node.GetStringSafe("tlp:Country", namespaceManager);
            this.CountryId = node.GetIntSafe("tlp:CountryID", namespaceManager);
            this.CustomerStatus = node.GetStringSafe("tlp:CustomerStatus", namespaceManager);
            this.CustomerStatusID = node.GetIntSafe("tlp:CustomerStatusID", namespaceManager);
            this.Email = node.GetStringSafe("tlp:Email", namespaceManager);
            this.Fax = node.GetStringSafe("tlp:Fax", namespaceManager);
            this.Id = int.Parse(node.Attributes["ID"].InnerText);
            this.IndustryID = node.GetIntSafe("tlp:IndustryID", namespaceManager);
            this.IndustryName = node.GetStringSafe("tlp:IndustryName", namespaceManager);
            this.Name = node.GetStringSafe("tlp:Name", namespaceManager);
            this.NickName = node.GetStringSafe("tlp:NickName", namespaceManager);
            this.No = node.GetStringSafe("tlp:No", namespaceManager);
            this.Phone = node.GetStringSafe("tlp:Phone", namespaceManager);
            this.State = node.GetStringSafe("tlp:State", namespaceManager);
            this.VATNo = node.GetStringSafe("tlp:VATNo", namespaceManager);
            this.WebPage = node.GetStringSafe("tlp:WebPage", namespaceManager);
            this.ZipCode = node.GetStringSafe("tlp:ZipCode", namespaceManager);
            this.CustomerGUID = Guid.Parse(node.Attributes["GUID"].InnerText);
            this.EanNo = node.GetStringSafe("tlp:EanNo", namespaceManager);
            this.InvoiceTemplateID = node.GetIntSafe("tlp:InvoiceTemplateID", namespaceManager);
            this.AccountManagerInitials = node.GetStringSafe("tlp:AccountManagerInitials", namespaceManager);
            this.SecondaryAccountManagerID = node.GetIntSafe("tlp:SecondaryAccountManagerID", namespaceManager);
            this.SecondaryAccountManagerFullName = node.GetStringSafe("tlp:SecondaryAccountManagerFullName", namespaceManager);
            this.SecondaryAccountManagerInitials = node.GetStringSafe("tlp:SecondaryAccountManagerInitials", namespaceManager);
            this.CustomerSince = node.GetDateTimeSafe("tlp:CustomerSince", namespaceManager);
            this.RegistrationNumber = node.GetStringSafe("tlp:RegistrationNumber", namespaceManager);
            this.CreatedAt = node.GetDateTimeSafe("tlp:CreatedAt", namespaceManager);
            this.CreatedByEmployeeID = node.GetIntSafe("tlp:CreatedByEmployeeId", namespaceManager);
            this.CreatedByEmployeeInitials = node.GetStringSafe("tlp:CreatedBy", namespaceManager);
            this.LastModifiedAt = node.GetDateTimeSafe("tlp:LastModifiedAt", namespaceManager);
            this.LastModifiedByEmployeeID = node.GetIntSafe("tlp:LastModifiedByEmployeeId", namespaceManager);
            this.LastModifiedByEmployeeInitials = node.GetStringSafe("tlp:LastModifiedBy", namespaceManager);
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the GUID. 
        /// </summary>
        public Guid CustomerGUID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the nick name.
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Gets or sets the no.
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// Gets or sets the customer status id.
        /// </summary>
        public int CustomerStatusID { get; set; }

        /// <summary>
        /// Gets or sets the customer status.
        /// </summary>
        public string CustomerStatus { get; set; }

        /// <summary>
        /// Gets or sets the address 1.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address 2.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the address 3.
        /// </summary>
        public string Address3 { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country identifier
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the web page.
        /// </summary>
        public string WebPage { get; set; }

        /// <summary>
        /// Gets or sets the vat no.
        /// </summary>
        public string VATNo { get; set; }

        /// <summary>
        /// Gets or sets the customer comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the account manager id.
        /// </summary>
        public int AccountManagerID { get; set; }

        /// <summary>
        /// Gets or sets the account manager full name.
        /// </summary>
        public string AccountManagerFullName { get; set; }

        /// <summary>
        /// Gets or sets the industry id.
        /// </summary>
        public int IndustryID { get; set; }

        /// <summary>
        /// Gets or sets the industry name.
        /// </summary>
        public string IndustryName { get; set; }

        /// <summary>
        /// Gets or sets the EAN number.
        /// </summary>
        public string EanNo { get; set; }

        /// <summary>
        /// Gets or sets the invoice template identifier. 
        /// </summary>
        public int InvoiceTemplateID { get; set; }

        /// <summary>
        /// Gets or sets the account manager initials. 
        /// </summary>
        public string AccountManagerInitials { get; set; }

        /// <summary>
        /// Gets or sets the account manager identifier.
        /// </summary>
        public int SecondaryAccountManagerID { get; set; }

        /// <summary>
        /// Gets or sets the account manager full name. 
        /// </summary>
        public string SecondaryAccountManagerFullName { get; set; }

        /// <summary>
        /// Gets or sets the account manager initials.
        /// </summary>
        public string SecondaryAccountManagerInitials { get; set; }

        /// <summary>
        /// Gets or sets the customer since date. 
        /// </summary>
        public DateTime CustomerSince { get; set; }

        /// <summary>
        /// Gets or sets the registration number. 
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the employee identifier who created this customer.
        /// </summary>
        public int CreatedByEmployeeID { get; set; }

        /// <summary>
        /// Gets or sets the employee initials who created this customer.
        /// </summary>
        public string CreatedByEmployeeInitials { get; set; }

        /// <summary>
        /// Gets or sets the last modified date. 
        /// </summary>
        public DateTime LastModifiedAt { get; set; }

        /// <summary>
        /// Gets or sets the employee identifier who last modified this customer. 
        /// </summary>
        public int LastModifiedByEmployeeID { get; set; }

        /// <summary>
        /// Gets or sets the employee initials who last modified this customer. 
        /// </summary>
        public string LastModifiedByEmployeeInitials { get; set; }
    }
}