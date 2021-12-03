using System;
using System.Xml;
using System.Xml.Serialization;

namespace TimeLog.ReportingAPI.SDK;

/// <summary>
///     Placeholder for customer related constants and properties
/// </summary>
public class Customer
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Customer" /> class.
    /// </summary>
    public Customer()
    {
        AccountManagerFullName = string.Empty;
        AccountManagerID = -1;
        Address1 = string.Empty;
        Address2 = string.Empty;
        Address3 = string.Empty;
        City = string.Empty;
        Country = string.Empty;
        CountryId = -1;
        Comment = string.Empty;
        CustomerStatus = string.Empty;
        CustomerStatusID = -1;
        Email = string.Empty;
        Fax = string.Empty;
        Id = -1;
        IndustryID = -1;
        IndustryName = string.Empty;
        Name = string.Empty;
        NickName = string.Empty;
        No = string.Empty;
        Phone = string.Empty;
        State = string.Empty;
        VATNo = string.Empty;
        WebPage = string.Empty;
        ZipCode = string.Empty;
        CustomerGuid = Guid.Empty;
        EanNo = string.Empty;
        InvoiceTemplateID = -1;
        AccountManagerInitials = string.Empty;
        SecondaryAccountManagerID = -1;
        SecondaryAccountManagerFullName = string.Empty;
        SecondaryAccountManagerInitials = string.Empty;
        CustomerSince = DateTime.MinValue;
        RegistrationNumber = string.Empty;
        CreatedAt = DateTime.MinValue;
        CreatedByEmployeeID = -1;
        CreatedByEmployeeInitials = string.Empty;
        LastModifiedAt = DateTime.MinValue;
        LastModifiedByEmployeeID = -1;
        LastModifiedByEmployeeInitials = string.Empty;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Customer" /> class.
    /// </summary>
    /// <param name="node">The XML node to initialize from</param>
    /// <param name="namespaceManager">The namespace manager</param>
    public Customer(XmlNode node, XmlNamespaceManager namespaceManager)
    {
        AccountManagerFullName = node.GetStringSafe("tlp:AccountManagerFullName", namespaceManager);
        AccountManagerID = node.GetIntSafe("tlp:AccountManagerID", namespaceManager);
        Address1 = node.GetStringSafe("tlp:Address1", namespaceManager);
        Address2 = node.GetStringSafe("tlp:Address2", namespaceManager);
        Address3 = node.GetStringSafe("tlp:Address3", namespaceManager);
        City = node.GetStringSafe("tlp:City", namespaceManager);
        Comment = node.GetStringSafe("tlp:Comment", namespaceManager);
        Country = node.GetStringSafe("tlp:Country", namespaceManager);
        CountryId = node.GetIntSafe("tlp:CountryID", namespaceManager);
        CustomerStatus = node.GetStringSafe("tlp:CustomerStatus", namespaceManager);
        CustomerStatusID = node.GetIntSafe("tlp:CustomerStatusID", namespaceManager);
        Email = node.GetStringSafe("tlp:Email", namespaceManager);
        Fax = node.GetStringSafe("tlp:Fax", namespaceManager);
        Id = int.Parse(node.Attributes["ID"].InnerText);
        IndustryID = node.GetIntSafe("tlp:IndustryID", namespaceManager);
        IndustryName = node.GetStringSafe("tlp:IndustryName", namespaceManager);
        Name = node.GetStringSafe("tlp:Name", namespaceManager);
        NickName = node.GetStringSafe("tlp:NickName", namespaceManager);
        No = node.GetStringSafe("tlp:No", namespaceManager);
        Phone = node.GetStringSafe("tlp:Phone", namespaceManager);
        State = node.GetStringSafe("tlp:State", namespaceManager);
        VATNo = node.GetStringSafe("tlp:VATNo", namespaceManager);
        WebPage = node.GetStringSafe("tlp:WebPage", namespaceManager);
        ZipCode = node.GetStringSafe("tlp:ZipCode", namespaceManager);
        CustomerGuid = Guid.Parse(node.Attributes["GUID"].InnerText);
        EanNo = node.GetStringSafe("tlp:EanNo", namespaceManager);
        InvoiceTemplateID = node.GetIntSafe("tlp:InvoiceTemplateID", namespaceManager);
        AccountManagerInitials = node.GetStringSafe("tlp:AccountManagerInitials", namespaceManager);
        SecondaryAccountManagerID = node.GetIntSafe("tlp:SecondaryAccountManagerID", namespaceManager);
        SecondaryAccountManagerFullName = node.GetStringSafe("tlp:SecondaryAccountManagerFullName", namespaceManager);
        SecondaryAccountManagerInitials = node.GetStringSafe("tlp:SecondaryAccountManagerInitials", namespaceManager);
        CustomerSince = node.GetDateTimeSafe("tlp:CustomerSince", namespaceManager);
        RegistrationNumber = node.GetStringSafe("tlp:RegistrationNumber", namespaceManager);
        CreatedAt = node.GetDateTimeSafe("tlp:CreatedAt", namespaceManager);
        CreatedByEmployeeID = node.GetIntSafe("tlp:CreatedByEmployeeId", namespaceManager);
        CreatedByEmployeeInitials = node.GetStringSafe("tlp:CreatedBy", namespaceManager);
        LastModifiedAt = node.GetDateTimeSafe("tlp:LastModifiedAt", namespaceManager);
        LastModifiedByEmployeeID = node.GetIntSafe("tlp:LastModifiedByEmployeeId", namespaceManager);
        LastModifiedByEmployeeInitials = node.GetStringSafe("tlp:LastModifiedBy", namespaceManager);
    }

    /// <summary>
    ///     Gets the default parameter value for filtering for all customers
    /// </summary>
    [XmlIgnore]
    public static int All => 0;

    /// <summary>
    ///     Gets or sets the id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the GUID.
    /// </summary>
    public Guid CustomerGuid { get; set; }

    /// <summary>
    ///     Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or sets the nick name.
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    ///     Gets or sets the no.
    /// </summary>
    public string No { get; set; }

    /// <summary>
    ///     Gets or sets the customer status id.
    /// </summary>
    public int CustomerStatusID { get; set; }

    /// <summary>
    ///     Gets or sets the customer status.
    /// </summary>
    public string CustomerStatus { get; set; }

    /// <summary>
    ///     Gets or sets the address 1.
    /// </summary>
    public string Address1 { get; set; }

    /// <summary>
    ///     Gets or sets the address 2.
    /// </summary>
    public string Address2 { get; set; }

    /// <summary>
    ///     Gets or sets the address 3.
    /// </summary>
    public string Address3 { get; set; }

    /// <summary>
    ///     Gets or sets the zip code.
    /// </summary>
    public string ZipCode { get; set; }

    /// <summary>
    ///     Gets or sets the city.
    /// </summary>
    public string City { get; set; }

    /// <summary>
    ///     Gets or sets the state.
    /// </summary>
    public string State { get; set; }

    /// <summary>
    ///     Gets or sets the country identifier
    /// </summary>
    public int CountryId { get; set; }

    /// <summary>
    ///     Gets or sets the country.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    ///     Gets or sets the phone.
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    ///     Gets or sets the fax.
    /// </summary>
    public string Fax { get; set; }

    /// <summary>
    ///     Gets or sets the email.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Gets or sets the web page.
    /// </summary>
    public string WebPage { get; set; }

    /// <summary>
    ///     Gets or sets the vat no.
    /// </summary>
    public string VATNo { get; set; }

    /// <summary>
    ///     Gets or sets the customer comment
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    ///     Gets or sets the account manager id.
    /// </summary>
    public int AccountManagerID { get; set; }

    /// <summary>
    ///     Gets or sets the account manager full name.
    /// </summary>
    public string AccountManagerFullName { get; set; }

    /// <summary>
    ///     Gets or sets the industry id.
    /// </summary>
    public int IndustryID { get; set; }

    /// <summary>
    ///     Gets or sets the industry name.
    /// </summary>
    public string IndustryName { get; set; }

    /// <summary>
    ///     Gets or sets the EAN number.
    /// </summary>
    public string EanNo { get; set; }

    /// <summary>
    ///     Gets or sets the invoice template identifier.
    /// </summary>
    public int InvoiceTemplateID { get; set; }

    /// <summary>
    ///     Gets or sets the account manager initials.
    /// </summary>
    public string AccountManagerInitials { get; set; }

    /// <summary>
    ///     Gets or sets the account manager identifier.
    /// </summary>
    public int SecondaryAccountManagerID { get; set; }

    /// <summary>
    ///     Gets or sets the account manager full name.
    /// </summary>
    public string SecondaryAccountManagerFullName { get; set; }

    /// <summary>
    ///     Gets or sets the account manager initials.
    /// </summary>
    public string SecondaryAccountManagerInitials { get; set; }

    /// <summary>
    ///     Gets or sets the customer since date.
    /// </summary>
    public DateTime CustomerSince { get; set; }

    /// <summary>
    ///     Gets or sets the registration number.
    /// </summary>
    public string RegistrationNumber { get; set; }

    /// <summary>
    ///     Gets or sets the created date.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     Gets or sets the employee identifier who created this customer.
    /// </summary>
    public int CreatedByEmployeeID { get; set; }

    /// <summary>
    ///     Gets or sets the employee initials who created this customer.
    /// </summary>
    public string CreatedByEmployeeInitials { get; set; }

    /// <summary>
    ///     Gets or sets the last modified date.
    /// </summary>
    public DateTime LastModifiedAt { get; set; }

    /// <summary>
    ///     Gets or sets the employee identifier who last modified this customer.
    /// </summary>
    public int LastModifiedByEmployeeID { get; set; }

    /// <summary>
    ///     Gets or sets the employee initials who last modified this customer.
    /// </summary>
    public string LastModifiedByEmployeeInitials { get; set; }
}