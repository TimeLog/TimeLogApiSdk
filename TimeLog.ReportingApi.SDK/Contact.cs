namespace TimeLog.ReportingApi.SDK
{
    using System.Xml;

    /// <summary>
    /// Placeholder for contact related constants
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Contact" /> class.
        /// </summary>
        public Contact()
        {
            this.AccountManagerFullName = string.Empty;
            this.AccountManagerID = -1;
            this.Address1 = string.Empty;
            this.Address2 = string.Empty;
            this.Birthday = string.Empty;
            this.City = string.Empty;
            this.Comment = string.Empty;
            this.Country = string.Empty;
            this.CustomerID = -1;
            this.CustomerName = string.Empty;
            this.CustomerNo = string.Empty;
            this.Email = string.Empty;
            this.FirstName = string.Empty;
            this.FullName = string.Empty;
            this.Id = -1;
            this.LastName = string.Empty;
            this.Mobile = string.Empty;
            this.Phone = string.Empty;
            this.PrivatePhone = string.Empty;
            this.State = string.Empty;
            this.Title = string.Empty;
            this.ZipCode = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Contact" /> class.
        /// </summary>
        /// <param name="node">The XML node to initialize from</param>
        /// <param name="namespaceManager">The namespace manager</param>
        public Contact(XmlNode node, XmlNamespaceManager namespaceManager)
        {
            this.AccountManagerFullName = node.GetStringSafe("tlp:AccountManagerFullName", namespaceManager);
            this.AccountManagerID = node.GetIntSafe("tlp:AccountManagerID", namespaceManager);
            this.Address1 = node.GetStringSafe("tlp:Address1", namespaceManager);
            this.Address2 = node.GetStringSafe("tlp:Address2", namespaceManager);
            this.Birthday = node.GetStringSafe("tlp:Birthday", namespaceManager);
            this.City = node.GetStringSafe("tlp:City", namespaceManager);
            this.Comment = node.GetStringSafe("tlp:Comment", namespaceManager);
            this.Country = node.GetStringSafe("tlp:Country", namespaceManager);
            this.CustomerID = node.GetIntSafe("tlp:CustomerID", namespaceManager);
            this.CustomerName = node.GetStringSafe("tlp:CustomerName", namespaceManager);
            this.CustomerNo = node.GetStringSafe("tlp:CustomerNo", namespaceManager);
            this.Email = node.GetStringSafe("tlp:Email", namespaceManager);
            this.FirstName = node.GetStringSafe("tlp:FirstName", namespaceManager);
            this.FullName = node.GetStringSafe("tlp:FullName", namespaceManager);
            this.Id = int.Parse(node.Attributes["ID"].InnerText);
            this.LastName = node.GetStringSafe("tlp:LastName", namespaceManager);
            this.Mobile = node.GetStringSafe("tlp:Mobile", namespaceManager);
            this.Phone = node.GetStringSafe("tlp:Phone", namespaceManager);
            this.PrivatePhone = node.GetStringSafe("tlp:PrivatePhone", namespaceManager);
            this.State = node.GetStringSafe("tlp:State", namespaceManager);
            this.Title = node.GetStringSafe("tlp:Title", namespaceManager);
            this.ZipCode = node.GetStringSafe("tlp:ZipCode", namespaceManager);
        }

        /// <summary>
        /// Gets the default parameter value for filtering for all contacts
        /// </summary>
        public static int All
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets or sets contact ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets customer ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets customer name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets customer number
        /// </summary>
        public string CustomerNo { get; set; }

        /// <summary>
        /// Gets or sets title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets phone number
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or set mobile phone number
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets private phone number
        /// </summary>
        public string PrivatePhone { get; set; }

        /// <summary>
        /// Gets or sets email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets address 1
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets address 2
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets zip code
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets state
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets birthday
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// Gets or sets country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets account manager ID
        /// </summary>
        public int AccountManagerID { get; set; }

        /// <summary>
        /// Gets or sets account manager name
        /// </summary>
        public string AccountManagerFullName { get; set; }
    }
}