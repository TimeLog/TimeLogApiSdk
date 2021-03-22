using System;
using System.Xml;
using System.Xml.Serialization;

namespace TimeLog.ReportingApi.Core.SDK
{
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
            this.CreatedAt = DateTime.Now;
            this.CreatedBy = string.Empty;
            this.CreatedByEmployeeID = -1;
            this.Date = DateTime.Now;
            this.EmployeeFirstName = string.Empty;
            this.EmployeeID = -1;
            this.EmployeeInitials = string.Empty;
            this.EmployeeLastName = string.Empty;
            this.Id = -1;
            this.LastModifiedAt = DateTime.Now;
            this.LastModifiedBy = string.Empty;
            this.LastModifiedByEmployeeID = -1;
            this.RegHours = 0;
            this.TimeOffCode = 0;
            this.TimeOffName = string.Empty;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TimeOffRegistration" /> class.
        /// </summary>
        /// <param name="node">The XML node to initialize from</param>
        /// <param name="namespaceManager">The namespace manager</param>
        public TimeOffRegistration(XmlNode node, XmlNamespaceManager namespaceManager)
        {
            this.CreatedAt = node.GetDateTimeSafe("tlp:CreatedAt", namespaceManager);
            this.CreatedBy = node.GetStringSafe("tlp:CreatedBy", namespaceManager);
            this.CreatedByEmployeeID = node.GetIntSafe("tlp:CreatedByEmployeeID", namespaceManager);
            this.Date = node.GetDateTimeSafe("tlp:Date", namespaceManager);
            this.EmployeeFirstName = node.GetStringSafe("tlp:EmployeeFirstName", namespaceManager);
            this.EmployeeID = node.GetIntSafe("tlp:EmployeeID", namespaceManager);
            this.EmployeeInitials = node.GetStringSafe("tlp:EmployeeInitials", namespaceManager);
            this.EmployeeLastName = node.GetStringSafe("tlp:EmployeeLastName", namespaceManager);
            this.Id = int.Parse(node.Attributes["ID"].InnerText);
            this.LastModifiedAt = node.GetDateTimeSafe("tlp:LastModifiedAt", namespaceManager);
            this.LastModifiedBy = node.GetStringSafe("tlp:LastModifiedBy", namespaceManager);
            this.LastModifiedByEmployeeID = node.GetIntSafe("tlp:LastModifiedByEmployeeID", namespaceManager);
            this.RegHours = node.GetDoubleSafe("tlp:RegHours", namespaceManager, ServiceHandler.DataCulture);
            this.TimeOffCode = node.GetIntSafe("tlp:TimeOffCode", namespaceManager);
            this.TimeOffName = node.GetStringSafe("tlp:TimeOffName", namespaceManager);
        }

        /// <summary>
        ///     Gets the default parameter value for filtering for all time off registrations
        /// </summary>
        [XmlIgnore]
        public static int All
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the employee identifier
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// Gets or sets the employee initials
        /// </summary>
        public string EmployeeInitials { get; set; }

        /// <summary>
        /// Gets or sets the employee first name
        /// </summary>
        public string EmployeeFirstName { get; set; }

        /// <summary>
        /// Gets or sets the employee last name
        /// </summary>
        public string EmployeeLastName { get; set; }

        /// <summary>
        /// Gets or sets the salary account code
        /// </summary>
        public int TimeOffCode { get; set; }

        /// <summary>
        /// Gets or sets the salary account name
        /// </summary>
        public string TimeOffName { get; set; }

        /// <summary>
        /// Gets or sets the registration date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the registered hours
        /// </summary>
        public double RegHours { get; set; }

        /// <summary>
        /// Gets or sets the creation date
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the creation employee identifier
        /// </summary>
        public int CreatedByEmployeeID { get; set; }

        /// <summary>
        /// Gets or sets the creation employee name
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the last modified date
        /// </summary>
        public DateTime LastModifiedAt { get; set; }

        /// <summary>
        /// Gets or sets the last modified employee identifier
        /// </summary>
        public int LastModifiedByEmployeeID { get; set; }

        /// <summary>
        /// Gets or sets the last modified employee name
        /// </summary>
        public string LastModifiedBy { get; set; }
    }
}