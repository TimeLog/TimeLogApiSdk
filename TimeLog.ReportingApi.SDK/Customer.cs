namespace TimeLog.ReportingApi.SDK
{
    using System.Xml;

    /// <summary>
    ///     Placeholder for customer related constants
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets the default parameter value for filtering for all customers
        /// </summary>
        public static int All
        {
            get
            {
                return 0;
            }
        }

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
            this.CustomerStatus = string.Empty;
            this.CustomerStatusID = -1;
            this.Email = string.Empty;
            this.Fax = string.Empty;
            this.Id = -1;
            this.Name = string.Empty;
            this.NickName = string.Empty;
            this.No = string.Empty;
            this.Phone = string.Empty;
            this.State = string.Empty;
            this.VATNo = string.Empty;
            this.WebPage = string.Empty;
            this.ZipCode = string.Empty;
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
            this.Country = node.GetStringSafe("tlp:Country", namespaceManager);
            this.CustomerStatus = node.GetStringSafe("tlp:CustomerStatus", namespaceManager);
            this.CustomerStatusID = node.GetIntSafe("tlp:CustomerStatusID", namespaceManager);
            this.Email = node.GetStringSafe("tlp:Email", namespaceManager);
            this.Fax = node.GetStringSafe("tlp:Fax", namespaceManager);
            this.Id = int.Parse(node.Attributes["ID"].InnerText);
            this.Name = node.GetStringSafe("tlp:Name", namespaceManager);
            this.NickName = node.GetStringSafe("tlp:NickName", namespaceManager);
            this.No = node.GetStringSafe("tlp:No", namespaceManager);
            this.Phone = node.GetStringSafe("tlp:Phone", namespaceManager);
            this.State = node.GetStringSafe("tlp:State", namespaceManager);
            this.VATNo = node.GetStringSafe("tlp:VATNo", namespaceManager);
            this.WebPage = node.GetStringSafe("tlp:WebPage", namespaceManager);
            this.ZipCode = node.GetStringSafe("tlp:ZipCode", namespaceManager);
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

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
        /// Gets or sets the account manager id.
        /// </summary>
        public int AccountManagerID { get; set; }

        /// <summary>
        /// Gets or sets the account manager full name.
        /// </summary>
        public string AccountManagerFullName { get; set; }
    }
}