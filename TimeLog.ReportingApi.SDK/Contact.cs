using System.Xml;

namespace TimeLog.ReportingAPI.SDK;

/// <summary>
///     Placeholder for contact related constants
/// </summary>
public class Contact
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Contact" /> class.
    /// </summary>
    public Contact()
    {
        AccountManagerFullName = string.Empty;
        AccountManagerID = -1;
        Address1 = string.Empty;
        Address2 = string.Empty;
        Birthday = string.Empty;
        City = string.Empty;
        Comment = string.Empty;
        Country = string.Empty;
        CustomerID = -1;
        CustomerName = string.Empty;
        CustomerNo = string.Empty;
        Email = string.Empty;
        FirstName = string.Empty;
        FullName = string.Empty;
        Id = -1;
        LastName = string.Empty;
        Mobile = string.Empty;
        Phone = string.Empty;
        PrivatePhone = string.Empty;
        State = string.Empty;
        Title = string.Empty;
        ZipCode = string.Empty;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Contact" /> class.
    /// </summary>
    /// <param name="node">The XML node to initialize from</param>
    /// <param name="namespaceManager">The namespace manager</param>
    public Contact(XmlNode node, XmlNamespaceManager namespaceManager)
    {
        AccountManagerFullName = node.GetStringSafe("tlp:AccountManagerFullName", namespaceManager);
        AccountManagerID = node.GetIntSafe("tlp:AccountManagerID", namespaceManager);
        Address1 = node.GetStringSafe("tlp:Address1", namespaceManager);
        Address2 = node.GetStringSafe("tlp:Address2", namespaceManager);
        Birthday = node.GetStringSafe("tlp:Birthday", namespaceManager);
        City = node.GetStringSafe("tlp:City", namespaceManager);
        Comment = node.GetStringSafe("tlp:Comment", namespaceManager);
        Country = node.GetStringSafe("tlp:Country", namespaceManager);
        CustomerID = node.GetIntSafe("tlp:CustomerID", namespaceManager);
        CustomerName = node.GetStringSafe("tlp:CustomerName", namespaceManager);
        CustomerNo = node.GetStringSafe("tlp:CustomerNo", namespaceManager);
        Email = node.GetStringSafe("tlp:Email", namespaceManager);
        FirstName = node.GetStringSafe("tlp:FirstName", namespaceManager);
        FullName = node.GetStringSafe("tlp:FullName", namespaceManager);
        Id = int.Parse(node.Attributes["ID"].InnerText);
        LastName = node.GetStringSafe("tlp:LastName", namespaceManager);
        Mobile = node.GetStringSafe("tlp:Mobile", namespaceManager);
        Phone = node.GetStringSafe("tlp:Phone", namespaceManager);
        PrivatePhone = node.GetStringSafe("tlp:PrivatePhone", namespaceManager);
        State = node.GetStringSafe("tlp:State", namespaceManager);
        Title = node.GetStringSafe("tlp:Title", namespaceManager);
        ZipCode = node.GetStringSafe("tlp:ZipCode", namespaceManager);
    }

    /// <summary>
    ///     Gets the default parameter value for filtering for all contacts
    /// </summary>
    public static int All => 0;

    /// <summary>
    ///     Gets or sets contact ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets first name
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    ///     Gets or sets last name
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    ///     Gets or sets full name
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    ///     Gets or sets customer ID
    /// </summary>
    public int CustomerID { get; set; }

    /// <summary>
    ///     Gets or sets customer name
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    ///     Gets or sets customer number
    /// </summary>
    public string CustomerNo { get; set; }

    /// <summary>
    ///     Gets or sets title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     Gets or sets phone number
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    ///     Gets or set mobile phone number
    /// </summary>
    public string Mobile { get; set; }

    /// <summary>
    ///     Gets or sets private phone number
    /// </summary>
    public string PrivatePhone { get; set; }

    /// <summary>
    ///     Gets or sets email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Gets or sets address 1
    /// </summary>
    public string Address1 { get; set; }

    /// <summary>
    ///     Gets or sets address 2
    /// </summary>
    public string Address2 { get; set; }

    /// <summary>
    ///     Gets or sets zip code
    /// </summary>
    public string ZipCode { get; set; }

    /// <summary>
    ///     Gets or sets city
    /// </summary>
    public string City { get; set; }

    /// <summary>
    ///     Gets or sets state
    /// </summary>
    public string State { get; set; }

    /// <summary>
    ///     Gets or sets birthday
    /// </summary>
    public string Birthday { get; set; }

    /// <summary>
    ///     Gets or sets country
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    ///     Gets or sets comment
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    ///     Gets or sets account manager ID
    /// </summary>
    public int AccountManagerID { get; set; }

    /// <summary>
    ///     Gets or sets account manager name
    /// </summary>
    public string AccountManagerFullName { get; set; }
}