using System;
using System.Xml.Serialization;

namespace TimeLog.ReportingApi.SDK
{
    /// <summary>
    /// Placeholder for task related constants and properties
    /// </summary>
    public class Task
    {
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
