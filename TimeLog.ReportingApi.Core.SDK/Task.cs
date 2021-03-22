using System;
using System.Xml;
using System.Xml.Serialization;

namespace TimeLog.ReportingApi.Core.SDK
{
    /// <summary>
    /// Placeholder for task related constants and properties
    /// </summary>
    public class Task
    {
        public Task()
        {

        }

        public Task(XmlNode node, XmlNamespaceManager namespaceManager)
        {
            this.Id = int.Parse(node.Attributes["ID"].InnerText);
            this.Guid = Guid.Parse(node.Attributes["GUID"].InnerText);
            this.Name = node.GetStringSafe("tlp:Name", namespaceManager);
            this.Wbs = node.GetStringSafe("tlp:WBS", namespaceManager);
            this.Status = node.GetIntSafe("tlp:Status", namespaceManager);
            this.ProjectId = node.GetIntSafe("tlp:ProjectId", namespaceManager);
            this.Status = node.GetIntSafe("tlp:Status", namespaceManager);
            this.StatusDetailed = node.GetIntSafe("tlp:StatusDetailed", namespaceManager);
            this.ParentId = node.GetIntSafe("tlp:ParentID", namespaceManager);
            this.IsParent = node.GetBoolTimeSafe("tlp:IsParent", namespaceManager);
            this.TaskTypeId = node.GetIntSafe("tlp:TaskTypeId", namespaceManager);
            this.TaskType = node.GetStringSafe("tlp:TaskType", namespaceManager);
            this.TaskCategoryId = node.GetIntSafe("tlp:TaskCategoryId", namespaceManager);
            this.TaskCategory = node.GetStringSafe("tlp:TaskCategory", namespaceManager);
            this.BudgetHours = node.GetIntSafe("tlp:BudgetHours", namespaceManager);
            this.BudgetAmount = node.GetIntSafe("tlp:BudgetAmount", namespaceManager);
            this.IsFixedPrice = node.GetBoolTimeSafe("tlp:IsFixedPrice", namespaceManager);
            this.StartDate = node.GetDateTimeSafe("tlp:StartDate", namespaceManager);
            this.EndDate = node.GetDateTimeSafe("tlp:EndDate", namespaceManager);
            this.InvoicingType = node.GetStringSafe("tlp:InvoicingType", namespaceManager);
            this.CreatedAt = node.GetDateTimeSafe("tlp:CreatedAt", namespaceManager);
            this.CreatedByEmployeeId = node.GetIntSafe("tlp:CreatedByEmployeeId", namespaceManager);
            this.CreatedBy = node.GetStringSafe("tlp:CreatedBy", namespaceManager);
            this.LastModifiedAt = node.GetDateTimeSafe("tlp:LastModifiedAt", namespaceManager);
            this.LastModifiedByEmployeeId = node.GetIntSafe("tlp:LastModifiedByEmployeeId", namespaceManager);
            this.LastModifiedBy = node.GetStringSafe("tlp:LastModifiedBy", namespaceManager);
        }



        /// <summary>
        /// Gets the default parameter value for filtering for all tasks
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
        /// Gets or sets the unique identifier
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the WBS
        /// </summary>
        public string Wbs { get; set; }

        /// <summary>
        /// Gets or sets the project id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the status. 1 = active, 0 = inactive
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the detailed status information.
        /// </summary>
        public int StatusDetailed { get; set; }

        /// <summary>
        /// Gets or sets the parent task id, if any
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Gets or sets a value whether the task is parent
        /// </summary>
        public bool IsParent { get; set; }

        /// <summary>
        /// Gets or sets the task type identifier
        /// </summary>
        public int TaskTypeId { get; set; }

        /// <summary>
        /// Gets or sets the task type
        /// </summary>
        public string TaskType { get; set; }

        /// <summary>
        /// Gets or sets the task identifier
        /// </summary>
        public int TaskCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the task category
        /// </summary>
        public string TaskCategory { get; set; }

        /// <summary>
        /// Gets or sets the budgetted amount in hours
        /// </summary>
        public float BudgetHours { get; set; }

        /// <summary>
        /// Gets or sets the budgetted amount in currency
        /// </summary>
        public float BudgetAmount { get; set; }

        /// <summary>
        /// Gets or sets whether the task is fixed price
        /// </summary>
        public bool IsFixedPrice { get; set; }

        /// <summary>
        /// Gets or sets the start date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the invoicing type
        /// </summary>
        public string InvoicingType { get; set; }

        /// <summary>
        /// Gets or sets the created date
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the creator
        /// </summary>
        public int CreatedByEmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the creator
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the last modified date
        /// </summary>
        public DateTime LastModifiedAt { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the last modifier
        /// </summary>
        public int LastModifiedByEmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the last modifier
        /// </summary>
        public string LastModifiedBy { get; set; }



    }
}
