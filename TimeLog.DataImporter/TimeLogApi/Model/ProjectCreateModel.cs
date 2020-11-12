using System;
using System.Collections.Generic;
using System.Text;

namespace TimeLog.DataImporter.TimeLogApi.Model
{
    public class ProjectCreateModel
    {
        /// <summary>
        /// Gets or sets the project name
        /// </summary>
        /// /// <value>
        /// The project name
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the project number
        /// </summary>
        /// <value>
        /// The project number
        /// </value>
        public string ProjectNo { get; set; }

        /// <summary>
        /// Gets or sets the customer ID
        /// </summary>
        /// <value>
        /// The customer ID
        /// </value>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        /// <value>
        /// The description
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the project template ID
        /// </summary>
        /// <value>
        /// The project template ID
        /// </value>
        public int ProjectTemplateID { get; set; }

        /// <summary>
        /// Gets or sets the project manager ID
        /// </summary>
        /// <value>
        /// The project manager ID
        /// </value>
        public int ProjectManagerID { get; set; }

        /// <summary>
        /// Gets or sets the start date of project
        /// </summary>
        /// <value>
        /// The start date of project
        /// </value>
        public DateTime ProjectStartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of project
        /// </summary>
        /// <value>
        /// The end date of project
        /// </value>
        public DateTime ProjectEndDate { get; set; }

        /// <summary>
        /// Gets or sets the project type ID
        /// </summary>
        /// <value>
        /// The project type ID
        /// </value>
        public int ProjectTypeID { get; set; }

        /// <summary>
        /// Gets or sets the project category ID
        /// </summary>
        /// <value>
        /// The project category ID
        /// </value>
        public int ProjectCategoryID { get; set; }

        /// <summary>
        /// Gets or sets the currency ID
        /// </summary>
        /// <value>
        /// The currency ID
        /// </value>
        public int CurrencyID { get; set; }

        /// <summary>
        /// Gets or sets the legal entity ID
        /// </summary>
        /// <value>
        /// The legal entity ID
        /// </value>
        public int LegalEntityID { get; set; }
    }
}