using System;
using System.Xml.Serialization;

namespace TimeLog.ReportingApi.SDK
{
    /// <summary>
    /// Placeholder for project related constants and properties
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Gets the default parameter value for filtering for all projects
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
        /// Gets or sets the project identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or set the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or set the status of the project. 1 = Active, 0 = Inactive
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the department ordering the project
        /// </summary>
        public int OrderedByDepartment { get; set; }

        /// <summary>
        /// Gets or sets the project start date
        /// </summary>
        public DateTime ProjectStartDate { get; set; }

        /// <summary>
        /// Gets or sets the project end date
        /// </summary>
        public DateTime ProjectEndDate { get; set; }

        /// <summary>
        /// Gets or sets the link
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the customer number
        /// </summary>
        public string CustomerNo { get; set; }

        /// <summary>
        /// Gets or sets the project manager identifier
        /// </summary>
        public int PMId { get; set; }

        /// <summary>
        /// Gets or sets the project manager initials
        /// </summary>
        public string PMInitials { get; set; }

        /// <summary>
        /// Gets or sets the project manager name
        /// </summary>
        public string PMFullname { get; set; }

        /// <summary>
        /// Gets or sets the project type identifier
        /// </summary>
        public int ProjectTypeId { get; set; }

        /// <summary>
        /// Gets or sets the project type name
        /// </summary>
        public string ProjectTypeName { get; set; }

        /// <summary>
        /// Gets or sets the project category identifier
        /// </summary>
        public int ProjectCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the project category name
        /// </summary>
        public string ProjectCategoryName { get; set; }

        /// <summary>
        /// Gets or sets the budgetted amount of expenses
        /// </summary>
        public float BudgetAmountExpenses { get; set; }

        /// <summary>
        /// Gets or sets the budgetted amount of travel
        /// </summary>
        public float BudgetAmountTravel { get; set; }

        /// <summary>
        /// Gets or sets the budgetted amout in currency
        /// </summary>
        public float BudgetAmount { get; set; }

        /// <summary>
        /// Gets or sets the budgetted amount in hours
        /// </summary>
        public float BudgetHours { get; set; }

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

        /// <summary>
        /// Gets or sets the project order status name
        /// </summary>
        public string ProjectStatusName { get; set; }

        /// <summary>
        /// Gets or sets the full name of the customer contact
        /// </summary>
        public string ContactFullName { get; set; }

        /// <summary>
        /// Gets or sets the email of the customer contact
        /// </summary>
        public string ContactEmail { get; set; }
    }
}
