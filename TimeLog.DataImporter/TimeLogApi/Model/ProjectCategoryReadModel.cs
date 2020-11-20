using System;

namespace TimeLog.DataImporter.TimeLogApi.Model
{
    public class ProjectCategoryReadModel
    {
        /// <summary>
        /// Gets or sets the Project category identifier.
        /// </summary>
        /// <value>
        /// The Project category identifier.
        /// </value>
        public int ProjectCategoryID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the ProductNo.
        /// </summary>
        /// <value>
        /// The ProductNo.
        /// </value>
        public string ProductNo { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid ID { get; set; }
    }
}